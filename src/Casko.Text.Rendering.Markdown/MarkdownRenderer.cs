using Markdig;

namespace Casko.Text.Rendering.Markdown;

/// <summary>
/// Converts between HTML markup and Markdown using Markdig.
/// </summary>
public sealed class MarkdownRenderer : IMarkdownRenderer
{
    private static readonly MarkdownPipeline MarkdownPipeline =
        new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();
    
    /// <inheritdoc />
    public ValueTask<string> RenderAsync(
        string utf8EncodedMarkdownString, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(utf8EncodedMarkdownString);
        cancellationToken.ThrowIfCancellationRequested();

        var utf8EncodedHtmlString = Markdig.Markdown.ToHtml(utf8EncodedMarkdownString, MarkdownPipeline);

        cancellationToken.ThrowIfCancellationRequested();
        return ValueTask.FromResult(utf8EncodedHtmlString);
    }
}
