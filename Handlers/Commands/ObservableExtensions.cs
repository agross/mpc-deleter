using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace MpcDeleter.Handlers.Commands
{
  public static class ObservableExtensions
  {
    public static IObservable<T> ObserveLatestOn<T>(this IObservable<T> source, IScheduler scheduler)
    {
      return Observable.Create<T>(observer =>
      {
        Notification<T> outsideNotification;
        var gate = new object();
        var active = false;
        var cancelable = new MultipleAssignmentDisposable();
        var disposable = source.Materialize().Subscribe(thisNotification =>
        {
          bool alreadyActive;
          lock (gate)
          {
            alreadyActive = active;
            active = true;
            outsideNotification = thisNotification;
          }

          if (!alreadyActive)
          {
            cancelable.Disposable = scheduler.Schedule(self =>
            {
              Notification<T> localNotification;
              lock (gate)
              {
                localNotification = outsideNotification;
                outsideNotification = null;
              }
              localNotification.Accept(observer);
              var hasPendingNotification = false;
              lock (gate)
              {
                hasPendingNotification = active = (outsideNotification != null);
              }
              if (hasPendingNotification)
              {
                self();
              }
            });
          }
        });
        return new CompositeDisposable(disposable, cancelable);
      });
    } 
  }
}