using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200000C RID: 12
	internal static class Iterators
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002C94 File Offset: 0x00000E94
		public static Iterators.ReverseEnumerator<T> Reverse<T>(IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new Iterators.ReverseEnumerator<T>(list);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CAA File Offset: 0x00000EAA
		public static IEnumerable<T> FilterByType<T>(IEnumerable<T> items, Type returnType)
		{
			IList<T> list = items as IList<T>;
			if (list != null)
			{
				int num;
				for (int i = 0; i < list.Count; i = num + 1)
				{
					if (returnType.IsInstanceOfType(list[i]))
					{
						yield return list[i];
					}
					num = i;
				}
			}
			else
			{
				foreach (T t in items)
				{
					if (returnType.IsInstanceOfType(t))
					{
						yield return t;
					}
				}
				IEnumerator<T> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CC1 File Offset: 0x00000EC1
		public static IEnumerable<T> Filter<T>(IEnumerable<T> items, Predicate<T> match)
		{
			IList<T> list = items as IList<T>;
			if (list != null)
			{
				int num;
				for (int i = 0; i < list.Count; i = num + 1)
				{
					if (match(list[i]))
					{
						yield return list[i];
					}
					num = i;
				}
			}
			else
			{
				foreach (T t in items)
				{
					if (match(t))
					{
						yield return t;
					}
				}
				IEnumerator<T> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x02000109 RID: 265
		public struct ReverseEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator, IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000D32 RID: 3378 RVA: 0x0002C25B File Offset: 0x0002A45B
			internal ReverseEnumerator(IList<T> list)
			{
				this.m_list = list;
				this.m_index = this.m_list.Count;
				this.m_current = default(T);
			}

			// Token: 0x06000D33 RID: 3379 RVA: 0x0002C281 File Offset: 0x0002A481
			public void Reset()
			{
				this.m_index = this.m_list.Count;
				this.m_current = default(T);
			}

			// Token: 0x17000305 RID: 773
			// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0002C2A0 File Offset: 0x0002A4A0
			public T Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x17000306 RID: 774
			// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0002C2A8 File Offset: 0x0002A4A8
			object IEnumerator.Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x06000D36 RID: 3382 RVA: 0x0002C2B5 File Offset: 0x0002A4B5
			public bool MoveNext()
			{
				if (this.m_index == 0)
				{
					return false;
				}
				this.m_index--;
				this.m_current = this.m_list[this.m_index];
				return true;
			}

			// Token: 0x06000D37 RID: 3383 RVA: 0x0002C2E7 File Offset: 0x0002A4E7
			public Iterators.ReverseEnumerator<T> GetEnumerator()
			{
				return new Iterators.ReverseEnumerator<T>(this.m_list);
			}

			// Token: 0x06000D38 RID: 3384 RVA: 0x0002C2F4 File Offset: 0x0002A4F4
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000D39 RID: 3385 RVA: 0x0002C301 File Offset: 0x0002A501
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Iterators.ReverseEnumerator<T>(this.m_list);
			}

			// Token: 0x06000D3A RID: 3386 RVA: 0x0002C313 File Offset: 0x0002A513
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000579 RID: 1401
			private readonly IList<T> m_list;

			// Token: 0x0400057A RID: 1402
			private int m_index;

			// Token: 0x0400057B RID: 1403
			private T m_current;
		}
	}
}
