using Markdig;
using ReverseMarkdown;

namespace Casko.Text.Rendering;

/// <summary>
/// Converts between HTML markup and Markdown using Markdig and ReverseMarkdown.
/// </summary>
public sealed class MarkupRenderer : IMarkupRenderer
{
    private static readonly MarkdownPipeline MarkdownPipeline =
        new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();

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
    public ValueTask<string> MarkdownAsync(
        string html,
        MarkdownRenderOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(html);
        cancellationToken.ThrowIfCancellationRequested();

        var markdown = MarkdownConverter.Convert(html);

        cancellationToken.ThrowIfCancellationRequested();
        return ValueTask.FromResult(markdown);
    }

    /// <inheritdoc />
    public ValueTask<string> MarkupAsync(
        string markdown,
        MarkupRenderOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(markdown);
        cancellationToken.ThrowIfCancellationRequested();

        var html = Markdown.ToHtml(markdown, MarkdownPipeline);

        cancellationToken.ThrowIfCancellationRequested();
        return ValueTask.FromResult(html);
    }
}
