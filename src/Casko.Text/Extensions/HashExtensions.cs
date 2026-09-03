using System.Security.Cryptography;
using System.Text;

namespace Casko.Text.Extensions;

public static class HashExtensions
{
    private const int Sha256HashByteCount = 32;
    private const int Base62HashByteCount = 10;
    private const int Base62HashLength = 14;

    private const string Base32Alphabet = "abcdefghijklmnopqrstuvwxyz234567";
    private const string Base62Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    /// <summary>
    /// Computes a Base62 representation of the first 80 bits of the SHA-256 hash of <paramref name="value" />.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <param name="length">The number of Base62 characters to return, from 1 through 14.</param>
    /// <returns>A fixed-length Base62 hash.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="length" /> is outside the supported range.</exception>
    public static string ToSha256Base62Hash(this string value, int length)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (length is < 1 or > Base62HashLength)
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, $"Length must be between 1 and {Base62HashLength}.");
        }

        var hashBytes = ComputeSha256Bytes(value);
        var encoded = EncodeBase62(hashBytes.AsSpan(0, Base62HashByteCount));

        return encoded.PadLeft(Base62HashLength, '0')[..length];
    }

    /// <summary>
    /// Computes a lowercase Base32 representation of the first bytes of the SHA-256 hash of <paramref name="value" />.
    /// </summary>
    /// <param name="value">The value to hash.</param>
    /// <param name="byteCount">The number of SHA-256 bytes to encode, from 1 through 32.</param>
    /// <returns>A lowercase Base32 hash.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="byteCount" /> is outside the supported range.</exception>
    public static string ToSha256Base32Hash(this string value, int byteCount = 8)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (byteCount is < 1 or > Sha256HashByteCount)
        {
            throw new ArgumentOutOfRangeException(nameof(byteCount), byteCount, $"Byte count must be between 1 and {Sha256HashByteCount}.");
        }

        return Base32Encode(ComputeSha256Bytes(value).AsSpan(0, byteCount));
    }

    private static string Base32Encode(ReadOnlySpan<byte> data)
    {
        var outputLength = (int)Math.Ceiling(data.Length / 5d * 8);
        var result = new char[outputLength];

        var buffer = 0;
        var bitsLeft = 0;
        var index = 0;

        foreach (var b in data)
        {
            buffer = (buffer << 8) | b;
            bitsLeft += 8;

            while (bitsLeft >= 5)
            {
                bitsLeft -= 5;
                result[index++] = Base32Alphabet[(buffer >> bitsLeft) & 0x1F];
            }
        }

        if (bitsLeft > 0)
        {
            buffer <<= (5 - bitsLeft);
            result[index++] = Base32Alphabet[buffer & 0x1F];
        }

        return new string(result, 0, index);
    }

    private static byte[] ComputeSha256Bytes(string value)
    {
        return SHA256.HashData(Encoding.UTF8.GetBytes(value));
    }

    private static string EncodeBase62(ReadOnlySpan<byte> bytes)
    {
        var accumulator = new System.Numerics.BigInteger(bytes, isUnsigned: true, isBigEndian: false);
        var builder = new StringBuilder();

        while (accumulator > 0)
        {
            accumulator = System.Numerics.BigInteger.DivRem(accumulator, Base62Alphabet.Length, out var remainder);
            builder.Insert(0, Base62Alphabet[(int)remainder]);
        }

        return builder.Length == 0 ? Base62Alphabet[0].ToString() : builder.ToString();
    }
}
