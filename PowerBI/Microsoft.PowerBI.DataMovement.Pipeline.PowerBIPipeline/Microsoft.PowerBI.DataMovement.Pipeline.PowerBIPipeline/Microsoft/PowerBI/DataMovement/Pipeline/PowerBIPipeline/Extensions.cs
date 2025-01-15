using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x0200000B RID: 11
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class Extensions
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002FBC File Offset: 0x000011BC
		internal static async Task ForEachItemAsync<[global::System.Runtime.CompilerServices.Nullable(2)] T>(this IEnumerable<T> enumerable, Func<T, Task> func)
		{
			if (enumerable != null)
			{
				if (func == null)
				{
					throw new ArgumentNullException("func");
				}
				foreach (T t in enumerable)
				{
					await func(t);
				}
				IEnumerator<T> enumerator = null;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003008 File Offset: 0x00001208
		internal static List<O> SelectWithSafeDispose<[global::System.Runtime.CompilerServices.Nullable(2)] T, [global::System.Runtime.CompilerServices.Nullable(0)] O>(this IEnumerable<T> enumerable, Func<T, O> func) where O : IDisposable
		{
			if (enumerable == null)
			{
				return new List<O>();
			}
			if (func == null)
			{
				throw new ArgumentNullException();
			}
			List<O> list = new List<O>();
			foreach (T t in enumerable)
			{
				try
				{
					list.Add(func(t));
				}
				catch (Exception)
				{
					try
					{
						list.ForEachDispose<O>();
					}
					catch
					{
					}
					throw;
				}
			}
			return list;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003098 File Offset: 0x00001298
		internal static void ForEachDispose<[global::System.Runtime.CompilerServices.Nullable(0)] T>(this IEnumerable<T> enumerable) where T : IDisposable
		{
			if (enumerable == null)
			{
				return;
			}
			List<Exception> list = new List<Exception>();
			foreach (T t in enumerable)
			{
				try
				{
					t.Dispose();
				}
				catch (Exception ex)
				{
					TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceError("Dispose failed: {0}", new object[] { ex });
					list.Add(ex);
				}
			}
			if (list.Count > 0)
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003134 File Offset: 0x00001334
		internal static IEnumerable<IDisposable> ConcatElement<[global::System.Runtime.CompilerServices.Nullable(0)] T, [global::System.Runtime.CompilerServices.Nullable(0)] O>(this IEnumerable<T> enumerable, O disposable) where T : IDisposable where O : IDisposable
		{
			List<IDisposable> list = new List<IDisposable> { disposable };
			if (enumerable == null)
			{
				return list;
			}
			return new List<IDisposable>(enumerable.Select((T item) => item)).Concat(list);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003188 File Offset: 0x00001388
		internal static IEnumerable<Tuple<T, O>> JoinByOrder<[global::System.Runtime.CompilerServices.Nullable(2)] T, [global::System.Runtime.CompilerServices.Nullable(2)] O>(this IEnumerable<T> left, IEnumerable<O> right)
		{
			if (left == null)
			{
				if (right == null)
				{
					return new List<Tuple<T, O>>();
				}
				throw new ArgumentNullException("left");
			}
			else
			{
				if (right == null)
				{
					throw new ArgumentNullException("right");
				}
				int num = left.Count<T>();
				if (num != right.Count<O>())
				{
					throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "There are {0} items in the left enumerable and {1} in the right enumerable", num, right.Count<O>()));
				}
				List<Tuple<T, O>> list = new List<Tuple<T, O>>(num);
				using (IEnumerator<O> enumerator = right.GetEnumerator())
				{
					foreach (T t in left)
					{
						enumerator.MoveNext();
						list.Add(Tuple.Create<T, O>(t, enumerator.Current));
					}
				}
				return list;
			}
		}
	}
}
