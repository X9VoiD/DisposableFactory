# DisposableFactory

Create disposable objects without writing `IDisposable` or `SafeHandle`.

## Usage

```cs
var someHandle = GetSomeHandle();
using (var disposableHandle = someHandle.ToDisposable(h => DisposeSomeHandle(h)))
{
    DoSomethingWithHandle(disposableHandle);
}
```

```cs
using (var someScope = Disposable.Create(() => DoSomethingAfterScopeEnd()))
{
    // do stuff
}
```

### Caveat

C# disallows implicit conversion to an interface from a generic class. If
`.ToDisposable<T>()` is called on an interface, it will not be possible to use
the implicit conversion from `DisposableHandle<T>` to `T`.

Use `DisposableHandle<T>.Handle` to get the underlying handle instead in this
situation.