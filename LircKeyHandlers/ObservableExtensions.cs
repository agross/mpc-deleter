using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace MpcDeleter.LircKeyHandlers
{
  public static class ObservableExtensions
  {
    internal static IObservable<T> DelayBetweenValues<T>(this IObservable<T> observable,
                                                         TimeSpan interval,
                                                         IScheduler scheduler,
                                                         Action ignoredValue)
    {
      return Observable.Create<T>(observer => observable
                                                .TimeInterval(scheduler)
                                                .Subscribe
                                                (time =>
                                                {
                                                  if (time.Interval < interval)
                                                  {
                                                    ignoredValue();
                                                    return;
                                                  }

                                                  observer.OnNext(time.Value);
                                                }));
    }
  }
}
