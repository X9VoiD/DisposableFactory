namespace DisposableFactory;

internal sealed class ActionDisposable : IDisposable
{
    private readonly Action _action;
    public ActionDisposable(Action action) => _action = action;

    public void Dispose()
    {
        _action();
    }
}

internal sealed class ActionDisposable<T> : IDisposable
{
    private readonly Action<T> _action;
    private T Value { get; }

    public ActionDisposable(Action<T> func, T value)
    {
        _action = func;
        Value = value;
    }

    public void Dispose()
    {
        _action(Value);
        GC.SuppressFinalize(this);
    }
}

internal sealed class ActionDisposable<T1, T2> : IDisposable
{
    private readonly Action<T1, T2> _action;
    private readonly T1 _value1;
    private readonly T2 _value2;

    public ActionDisposable(Action<T1, T2> func, T1 value1, T2 value2)
    {
        _action = func;
        _value1 = value1;
        _value2 = value2;
    }

    public void Dispose()
    {
        _action(_value1, _value2);
    }
}