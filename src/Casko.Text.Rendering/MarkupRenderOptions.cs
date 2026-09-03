namespace Casko.Text.Rendering;

/// <summary>
/// Defines options for converting Markdown to HTML markup.
/// </summary>
public sealed record MarkupRenderOptions
{
    /// <summary>
    /// Gets whether Mermaid diagram blocks should be enabled.
    /// </summary>
    public bool EnableMermaid { get; init; } = true;
}
