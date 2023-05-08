using BabySounds.Client.MediaApi.Util;
using Microsoft.JSInterop;

namespace BabySounds.Client.MediaApi;

public sealed class MediaFiles : JsModule
{
    public MediaFiles(IJSRuntime js)
        : base(js, "./_content/BabySounds.Client.MediaApi/mediaFiles.js")
    {
    }

    public async ValueTask<JsDirectory> ShowDirectoryPicker()
        => await InvokeAsync<JsDirectory>("showDirectoryPicker");

    public async ValueTask<JsDirectory> ReopenLastDirectory()
        => await InvokeAsync<JsDirectory>("reopenLastDirectory");

    public async ValueTask<JsFile[]> GetFilesAsync(JsDirectory directory)
        => await InvokeAsync<JsFile[]>("getFiles", directory.Instance);

    public async ValueTask<byte[]> DecodeAudioFileAsync(JsFile file)
        => await InvokeAsync<byte[]>("decodeAudioFile", file.Name);

    public async ValueTask<IJSObjectReference> PlayAudioFileAsync(JsFile file)
        => await InvokeAsync<IJSObjectReference>("playAudioFile", file.Name);

    public async ValueTask<IJSObjectReference> PlayAudioDataAsync(byte[] data)
        => await InvokeAsync<IJSObjectReference>("playAudioData", data);

    public record JsDirectory(string Name, IJSObjectReference Instance) : IAsyncDisposable
    {
        // When .NET is done with this JSDirectory, also release the underlying JS object
        public ValueTask DisposeAsync() => Instance.DisposeAsync();
    }

    public record JsFile(
        string Name,
        long Size,
        DateTime LastModified
    );
}
