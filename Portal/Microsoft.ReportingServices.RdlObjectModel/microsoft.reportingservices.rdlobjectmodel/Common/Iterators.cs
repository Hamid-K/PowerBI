using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000063 RID: 99
	internal static class Iterators
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x000162BC File Offset: 0x000144BC
		public static Iterators.ReverseEnumerator<T> Reverse<T>(IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new Iterators.ReverseEnumerator<T>(list);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000162D2 File Offset: 0x000144D2
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

		// Token: 0x060003D5 RID: 981 RVA: 0x000162E9 File Offset: 0x000144E9
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

		// Token: 0x02000327 RID: 807
		public struct ReverseEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600173B RID: 5947 RVA: 0x00037001 File Offset: 0x00035201
			internal ReverseEnumerator(IList<T> list)
			{
				this.m_list = list;
				this.m_index = this.m_list.Count;
				this.m_current = default(T);
			}

			// Token: 0x0600173C RID: 5948 RVA: 0x00037027 File Offset: 0x00035227
			public void Reset()
			{
				this.m_index = this.m_list.Count;
				this.m_current = default(T);
			}

			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x0600173D RID: 5949 RVA: 0x00037046 File Offset: 0x00035246
			public T Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x0600173E RID: 5950 RVA: 0x0003704E File Offset: 0x0003524E
			object IEnumerator.Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x0600173F RID: 5951 RVA: 0x0003705B File Offset: 0x0003525B
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

			// Token: 0x06001740 RID: 5952 RVA: 0x0003708D File Offset: 0x0003528D
			public Iterators.ReverseEnumerator<T> GetEnumerator()
			{
				return new Iterators.ReverseEnumerator<T>(this.m_list);
			}

			// Token: 0x06001741 RID: 5953 RVA: 0x0003709A File Offset: 0x0003529A
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06001742 RID: 5954 RVA: 0x000370A7 File Offset: 0x000352A7
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Iterators.ReverseEnumerator<T>(this.m_list);
			}

			// Token: 0x06001743 RID: 5955 RVA: 0x000370B9 File Offset: 0x000352B9
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000728 RID: 1832
			private readonly IList<T> m_list;

			// Token: 0x04000729 RID: 1833
			private int m_index;

			// Token: 0x0400072A RID: 1834
			private T m_current;
		}
	}
}
