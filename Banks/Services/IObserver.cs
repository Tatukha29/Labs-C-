namespace Banks.Services
{
    public interface IObserver
    {
        void Update(IObservable observable);
    }
}