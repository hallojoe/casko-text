using ReverseMarkdown;

namespace Casko.Text.Rendering.Markdown;

/// <summary>
/// Converts between HTML markup and Markdown using ReverseMarkdown.
/// </summary>
public sealed class MarkupRenderer : IMarkupRenderer
{
    private static readonly Converter MarkdownConverter =
        new(new Config
        {
            Flavor = Config.MarkdownFlavor.GitHub,
            Formatting =
            {
                RemoveComments = true
            },
            Links =
            {
                SmartHref = true
            }
        });
    
    /// <inheritdoc />
    public ValueTask<string> RenderAsync(
        string utf8EncodedHtmlString, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(utf8EncodedHtmlString);
        cancellationToken.ThrowIfCancellationRequested();

        var utf8EncodedMarkdownString = MarkdownConverter.Convert(utf8EncodedHtmlString);

        cancellationToken.ThrowIfCancellationRequested();
        return ValueTask.FromResult(utf8EncodedMarkdownString);
    }
}
