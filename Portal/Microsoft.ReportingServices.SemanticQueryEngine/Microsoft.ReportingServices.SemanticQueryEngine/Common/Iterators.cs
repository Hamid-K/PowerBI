using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000074 RID: 116
	internal static class Iterators
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x00015BD8 File Offset: 0x00013DD8
		public static Iterators.ReverseEnumerator<T> Reverse<T>(IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new Iterators.ReverseEnumerator<T>(list);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00015BEE File Offset: 0x00013DEE
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

		// Token: 0x06000515 RID: 1301 RVA: 0x00015C05 File Offset: 0x00013E05
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

		// Token: 0x020000D7 RID: 215
		public struct ReverseEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator, IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000765 RID: 1893 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
			internal ReverseEnumerator(IList<T> list)
			{
				this.m_list = list;
				this.m_index = this.m_list.Count;
				this.m_current = default(T);
			}

			// Token: 0x06000766 RID: 1894 RVA: 0x0001CAC6 File Offset: 0x0001ACC6
			public void Reset()
			{
				this.m_index = this.m_list.Count;
				this.m_current = default(T);
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001CAE5 File Offset: 0x0001ACE5
			public T Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001CAED File Offset: 0x0001ACED
			object IEnumerator.Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x06000769 RID: 1897 RVA: 0x0001CAFA File Offset: 0x0001ACFA
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

			// Token: 0x0600076A RID: 1898 RVA: 0x0001CB2C File Offset: 0x0001AD2C
			public Iterators.ReverseEnumerator<T> GetEnumerator()
			{
				return new Iterators.ReverseEnumerator<T>(this.m_list);
			}

			// Token: 0x0600076B RID: 1899 RVA: 0x0001CB39 File Offset: 0x0001AD39
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600076C RID: 1900 RVA: 0x0001CB46 File Offset: 0x0001AD46
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Iterators.ReverseEnumerator<T>(this.m_list);
			}

			// Token: 0x0600076D RID: 1901 RVA: 0x00003FB8 File Offset: 0x000021B8
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040003BA RID: 954
			private readonly IList<T> m_list;

			// Token: 0x040003BB RID: 955
			private int m_index;

			// Token: 0x040003BC RID: 956
			private T m_current;
		}
	}
}
