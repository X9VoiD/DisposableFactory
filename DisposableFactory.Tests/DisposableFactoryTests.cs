using DisposableFactory;
using Moq;

public class DisposableFactoryTests
{
    [Fact]
    public void SimpleCreate_DisposesProperly()
    {
        var isDisposed = false;
        var disposable = Disposable.Create(() => isDisposed = true);

        disposable.Dispose();

        isDisposed.Should().BeTrue();
    }

    [Fact]
    public void CreateWithOneArg_DisposesProperly()
    {
        var mock = Mock.Of<INothing>();
        var disposable = Disposable.Create(static nothing => nothing.Value = true, mock);

        disposable.Dispose();

        mock.Value.Should().BeTrue();
    }

    [Fact]
    public void CreateWithTwoArg_DisposesProperly()
    {
        var mock1 = Mock.Of<INothing>();
        var mock2 = Mock.Of<INothing>();
        var disposable = Disposable.Create(static (first, second) =>
        {
            first.Value = true;
            first.IntValue = 1;
            second.Value = true;
            second.IntValue = 2;
        }, mock1, mock2);

        disposable.Dispose();

        mock1.Value.Should().BeTrue();
        mock2.Value.Should().BeTrue();
        mock1.IntValue.Should().Be(1);
        mock2.IntValue.Should().Be(2);
    }

    [Fact]
    public void ToDisposable_DisposesProperly()
    {
        var mock = Mock.Of<INothing>();

        {
            using var disposable = mock.ToDisposable(static nothing => nothing.Value = true);
        }

        mock.Value.Should().BeTrue();
    }

    [Fact]
    public void DisposableHandle_IsImplicitlyConverted()
    {
        static void DoSomethingToHandle(IntPtr handle) =>
            handle.Should().Be(new IntPtr(1));

        var disposed = false;

        {
            using var someIntPtr = new IntPtr(1).ToDisposable(_ => disposed = true);
            DoSomethingToHandle(someIntPtr);
        }

        disposed.Should().BeTrue();
    }
}

public interface INothing
{
    bool Value { get; set; }
    int IntValue { get; set; }
}
