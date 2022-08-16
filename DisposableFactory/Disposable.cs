namespace DisposableFactory;

public static class Disposable
{
    public static IDisposable Create(Action action)
    {
        return new ActionDisposable(action);
    }

    public static IDisposable Create<T>(Action<T> action, T arg)
    {
        return new ActionDisposable<T>(action, arg);
    }

    public static IDisposable Create<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
    {
        return new ActionDisposable<T1, T2>(action, arg1, arg2);
    }

    public static DisposableHandle<T> ToDisposable<T>(this T disposable, Action<T> disposeAction)
    {
        return new DisposableHandle<T>(disposeAction, disposable);
    }
}