using Microsoft.JSInterop;

namespace BabySounds.Client.MediaApi.Util;

/// <inheritdoc />
/// <summary>
/// Helper for loading any JavaScript (ES6) module and calling its exports
/// </summary>
public abstract class JsModule : IAsyncDisposable
{
    private readonly Task<IJSObjectReference> _moduleTask;

    // On construction, we start loading the JS module
    protected JsModule(IJSRuntime js, string moduleUrl)
        => _moduleTask = js.InvokeAsync<IJSObjectReference>("import", moduleUrl).AsTask();

    // Methods for invoking exports from the module
    protected async ValueTask InvokeVoidAsync(string identifier, params object[]? args)
        => await (await _moduleTask).InvokeVoidAsync(identifier, args);

    protected async ValueTask<T> InvokeAsync<T>(string identifier, params object[]? args)
        => await (await _moduleTask).InvokeAsync<T>(identifier, args);

    // On disposal, we release the JS module
    public async ValueTask DisposeAsync()
        => await (await _moduleTask).DisposeAsync();
}
