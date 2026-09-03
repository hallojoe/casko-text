using RazorEngineCore;

namespace Casko.Text.Rendering.Razor;

/// <summary>
/// Renders in-memory Razor templates using RazorEngineCore.
/// </summary>
public sealed class RazorEngineCoreRenderer : IRazorRenderer
{
    private readonly IRazorEngine _engine;

    /// <summary>
    /// Initializes a new instance of the <see cref="RazorEngineCoreRenderer"/> class.
    /// </summary>
    public RazorEngineCoreRenderer()
        : this(new RazorEngine())
    {
    }

    internal RazorEngineCoreRenderer(IRazorEngine engine)
    {
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
    }

    /// <inheritdoc />
    public ValueTask<string> RenderAsync<T>(
        string template,
        T model,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(template);
        cancellationToken.ThrowIfCancellationRequested();

        var compiledTemplate = _engine.Compile(template);
        return ValueTask.FromResult(compiledTemplate.Run(model));
    }
}
