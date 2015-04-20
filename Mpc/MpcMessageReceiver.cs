using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Forms;

using Minimod.RxMessageBroker;

using MpcDeleter.Messages;

namespace MpcDeleter
{
  class MpcMessageReceiver : IObservable<Message>
  {
    sealed class Window : NativeWindow, IDisposable
    {
      readonly Subject<Message> _subject;

      public Window()
      {
        _subject = new Subject<Message>();

        var createParams = new CreateParams { Caption = "Message Exchange Window" };
        CreateHandle(createParams);

        RxMessageBrokerMinimod.Default.Send(new ReceiverCreated(Handle));
      }

      public IObservable<Message> Messages
      {
        get
        {
          return _subject;
        }
      }

      public void Dispose()
      {
        RxMessageBrokerMinimod.Default.Send(new ReceiverDestroyed(Handle));

        _subject.Dispose();
        DestroyHandle();
      }

      protected override void WndProc(ref Message m)
      {
        if (m.Msg == NativeConstants.WM_COPYDATA)
        {
          _subject.OnNext(m);
        }

        base.WndProc(ref m);
      }
    }

    readonly IObservable<Message> _subscription;

    public MpcMessageReceiver()
    {
      _subscription = Observable
        .Using(() => new Window(), window => window.Messages)
        .Publish()
        .RefCount(); // Subscribe once for all subscribers.
    }

    public IDisposable Subscribe(IObserver<Message> observer)
    {
      return _subscription.Subscribe(observer);
    }
  }
}
