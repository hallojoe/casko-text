namespace Casko.Text.Rendering;

/// <summary>
/// Defines options for converting HTML markup to Markdown.
/// </summary>
public sealed record MarkdownRenderOptions
{
    /// <summary>
    /// Gets whether Mermaid diagrams should be preserved as Mermaid Markdown blocks.
    /// </summary>
    public bool PreserveMermaid { get; init; } = true;
}
