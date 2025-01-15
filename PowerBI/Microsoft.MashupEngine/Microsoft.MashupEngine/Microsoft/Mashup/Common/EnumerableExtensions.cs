using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BED RID: 7149
	public static class EnumerableExtensions
	{
		// Token: 0x0600B289 RID: 45705 RVA: 0x00245950 File Offset: 0x00243B50
		public static bool HasAtLeastNElements<T>(this IEnumerable<T> enumerable, int n)
		{
			if (n == 0)
			{
				return true;
			}
			bool flag;
			using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
			{
				int num = 0;
				while (enumerator.MoveNext())
				{
					num++;
					if (num >= n)
					{
						return true;
					}
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600B28A RID: 45706 RVA: 0x002459A0 File Offset: 0x00243BA0
		public static bool HasExactlyOneElement<T>(this IEnumerable<T> enumerable)
		{
			ICollection<T> collection = enumerable as ICollection<T>;
			if (collection != null)
			{
				return collection.Count == 1;
			}
			bool flag;
			using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
			{
				flag = enumerator.MoveNext() && !enumerator.MoveNext();
			}
			return flag;
		}

		// Token: 0x0600B28B RID: 45707 RVA: 0x002459FC File Offset: 0x00243BFC
		public static T SingleOrDefaultIfZeroOrMany<T>(this IEnumerable<T> enumerable)
		{
			bool flag = false;
			T t = default(T);
			foreach (T t2 in enumerable)
			{
				if (flag)
				{
					return default(T);
				}
				flag = true;
				t = t2;
			}
			return t;
		}

		// Token: 0x0600B28C RID: 45708 RVA: 0x00245A60 File Offset: 0x00243C60
		public static IEnumerator<T> OnDispose<T>(this IEnumerator<T> enumerator, Action action)
		{
			return new EnumerableExtensions.NotifyingEnumerator<T>(enumerator, action);
		}

		// Token: 0x0600B28D RID: 45709 RVA: 0x00245A6C File Offset: 0x00243C6C
		public static IEnumerator<T> AfterDispose<T>(this IEnumerator<T> enumerator, Action action)
		{
			return new EnumerableExtensions.NotifyingEnumerator<T>(enumerator, delegate
			{
				try
				{
					enumerator.Dispose();
				}
				finally
				{
					action();
				}
			});
		}

		// Token: 0x0600B28E RID: 45710 RVA: 0x00245AA4 File Offset: 0x00243CA4
		public static bool SetEquals<T>(this IEnumerable<T> set1, IEnumerable<T> set2)
		{
			return new HashSet<T>(set1).SetEquals(set2);
		}

		// Token: 0x0600B28F RID: 45711 RVA: 0x00245AB4 File Offset: 0x00243CB4
		public static int SetGetHashCode<T>(this IEnumerable<T> set)
		{
			int num = 0;
			foreach (T t in set)
			{
				num += t.GetHashCode();
			}
			return num;
		}

		// Token: 0x02001BEE RID: 7150
		private sealed class NotifyingEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600B290 RID: 45712 RVA: 0x00245B08 File Offset: 0x00243D08
			public NotifyingEnumerator(IEnumerator<T> enumerator, Action callback)
			{
				this.enumerator = enumerator;
				this.callback = callback;
			}

			// Token: 0x17002CD2 RID: 11474
			// (get) Token: 0x0600B291 RID: 45713 RVA: 0x00245B1E File Offset: 0x00243D1E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17002CD3 RID: 11475
			// (get) Token: 0x0600B292 RID: 45714 RVA: 0x00245B2B File Offset: 0x00243D2B
			public T Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x0600B293 RID: 45715 RVA: 0x00245B38 File Offset: 0x00243D38
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x0600B294 RID: 45716 RVA: 0x00245B45 File Offset: 0x00243D45
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x0600B295 RID: 45717 RVA: 0x00245B52 File Offset: 0x00243D52
			public void Dispose()
			{
				if (this.callback != null)
				{
					Action action = this.callback;
					this.callback = null;
					action();
				}
				this.enumerator.Dispose();
			}

			// Token: 0x04005B3C RID: 23356
			private readonly IEnumerator<T> enumerator;

			// Token: 0x04005B3D RID: 23357
			private Action callback;
		}
	}
}
