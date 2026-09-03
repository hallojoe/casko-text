namespace Casko.Text.Rendering;

/// <summary>
/// Converts between HTML markup and Markdown.
/// </summary>
public interface IMarkupRenderer
{
    /// <summary>
    /// Converts HTML markup to Markdown.
    /// </summary>
    /// <param name="html">The HTML markup to convert.</param>
    /// <param name="options">Optional settings controlling the Markdown conversion.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The converted Markdown.</returns>
    ValueTask<string> MarkdownAsync(
        string html,
        MarkdownRenderOptions? options = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Converts Markdown to HTML markup.
    /// </summary>
    /// <param name="markdown">The Markdown to convert.</param>
    /// <param name="options">Optional settings controlling the HTML markup conversion.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The converted HTML markup.</returns>
    ValueTask<string> MarkupAsync(
        string markdown,
        MarkupRenderOptions? options = null,
        CancellationToken cancellationToken = default);
}
