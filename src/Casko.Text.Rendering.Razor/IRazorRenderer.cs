namespace Casko.Text.Rendering.Razor;

/// <summary>
/// Renders Razor templates from strings without requiring an HTTP context.
/// </summary>
public interface IRazorRenderer
{
    /// <summary>
    /// Renders a Razor template with the supplied model.
    /// </summary>
    /// <typeparam name="T">The template model type.</typeparam>
    /// <param name="template">The Razor template source.</param>
    /// <param name="model">The model exposed to the template through <c>Model</c>.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The rendered HTML.</returns>
    ValueTask<string> RenderAsync<T>(
        string template,
        T model,
        CancellationToken cancellationToken = default);
}
