namespace Casko.Text.Rendering.Markdown;

/// <summary>
/// String renderer.
/// </summary>
public interface IStringRenderer
{
    /// <summary>
    /// Renders string(ex. Markdown) to string(ex. HTML).
    /// </summary>
    /// <param name="utf8EncodedString">The string to render.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The rendered result string.</returns>
    ValueTask<string> RenderAsync(
        string utf8EncodedString,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Render Markdown string to HTML string.
/// </summary>
public interface IMarkdownRenderer : IStringRenderer
{
}

/// <summary>
/// Render HTML string to Markdown string.
/// </summary>
public interface IMarkupRenderer : IStringRenderer
{
}
