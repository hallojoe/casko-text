# Casko.Text

`Casko.Text` is a small .NET library of string extension methods for creating slugs, matching wildcards, and producing compact SHA-256-based identifiers.

## Installation

```bash
dotnet add package Casko.Text
```

Target framework: .NET 10 or later.

## Usage

Import the extensions:

```csharp
using Casko.Text.Extensions;
```

### Slugs

```csharp
var slug = "Caskø Text: Hello, World!".ToSlug();
// caskoe-text-hello-world
```

`ToSlug` lowercases ASCII letters, remaps common international characters, and converts separators to hyphens.

### Wildcard matching

`IsMatch` supports `*` for any number of characters and `?` for one character. Matching is case-insensitive by default.

```csharp
var isImage = "image/jpeg".IsMatch("image/*");
var isSupported = "image/jpeg".IsAnyMatch("text/*,image/*");
```

For hostname-like patterns, an absolute URL is matched against its host:

```csharp
var isCaskoHost = "https://docs.casko.dk/guide".IsMatch("*.casko.dk");
```

### Compact hashes

Create deterministic Base32 or Base62 identifiers from SHA-256 output:

```csharp
var base32 = "example".ToSha256Base32Hash();
var base62 = "example".ToSha256Base62Hash(length: 10);
```

These are truncated hash representations intended for compact identifiers, not password storage.

## License

Licensed under the [MIT License](LICENSE).
