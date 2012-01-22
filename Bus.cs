using System;
using System.Collections.Generic;
using System.Linq;

namespace MpcDeleter
{
	interface IMessage
	{
	}

	class RegistrationRemover : IDisposable
	{
		readonly Action _callOnDispose;

		public RegistrationRemover(Action toCallOnDispose)
		{
			_callOnDispose = toCallOnDispose;
		}

		public void Dispose()
		{
			_callOnDispose.DynamicInvoke();
		}
	}

	static class Bus
	{
		static List<Delegate> Subscribers;

		public static IDisposable Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IMessage
		{
			if (Subscribers == null)
			{
				Subscribers = new List<Delegate>();
			}

			Subscribers.Add(callback);

			return new RegistrationRemover(() => Subscribers.Remove(callback));
		}

		public static void Publish<TEvent>(TEvent message) where TEvent : IMessage
		{
			if (Subscribers == null)
			{
				return;
			}
			
			foreach (var subscriber in Subscribers.OfType<Action<TEvent>>())
			{
				subscriber(message);
			}
		}
	}
}