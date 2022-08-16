namespace DisposableFactory;

public sealed class DisposableHandle<T> : IDisposable
{
    private readonly Action<T> _disposeAction;

    [CLSCompliant(false)]
    public T Handle { get; }

    internal DisposableHandle(Action<T> disposeAction, T handle)
    {
        _disposeAction = disposeAction;
        Handle = handle;
    }

    public static implicit operator T(DisposableHandle<T> handle) => handle.Handle;

    public void Dispose()
    {
        _disposeAction(Handle);
    }
}