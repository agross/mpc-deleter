using System;
using System.Collections.Generic;

namespace MpcDeleter
{
	public static class EnumerableExtensions
	{
		public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
		{
			foreach (var x in instance)
			{
				action(x);
			}
		}
		
		public static IEnumerable<T> Append<T>(this IEnumerable<T> instance, T appended)
		{
			foreach (var x in instance)
			{
				yield return x;
			}

			yield return appended;
		}
	}
}