using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005D8 RID: 1496
	internal static class Iterators
	{
		// Token: 0x060053E4 RID: 21476 RVA: 0x0016168C File Offset: 0x0015F88C
		public static Iterators.ReverseEnumerator<T> Reverse<T>(IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new Iterators.ReverseEnumerator<T>(list);
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x001616A2 File Offset: 0x0015F8A2
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

		// Token: 0x060053E6 RID: 21478 RVA: 0x001616B9 File Offset: 0x0015F8B9
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

		// Token: 0x02000C0E RID: 3086
		public struct ReverseEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600864D RID: 34381 RVA: 0x00219A63 File Offset: 0x00217C63
			internal ReverseEnumerator(IList<T> list)
			{
				this.m_list = list;
				this.m_index = this.m_list.Count;
				this.m_current = default(T);
			}

			// Token: 0x0600864E RID: 34382 RVA: 0x00219A89 File Offset: 0x00217C89
			public void Reset()
			{
				this.m_index = this.m_list.Count;
				this.m_current = default(T);
			}

			// Token: 0x170029D4 RID: 10708
			// (get) Token: 0x0600864F RID: 34383 RVA: 0x00219AA8 File Offset: 0x00217CA8
			public T Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x170029D5 RID: 10709
			// (get) Token: 0x06008650 RID: 34384 RVA: 0x00219AB0 File Offset: 0x00217CB0
			object IEnumerator.Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x06008651 RID: 34385 RVA: 0x00219ABD File Offset: 0x00217CBD
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

			// Token: 0x06008652 RID: 34386 RVA: 0x00219AEF File Offset: 0x00217CEF
			public Iterators.ReverseEnumerator<T> GetEnumerator()
			{
				return new Iterators.ReverseEnumerator<T>(this.m_list);
			}

			// Token: 0x06008653 RID: 34387 RVA: 0x00219AFC File Offset: 0x00217CFC
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06008654 RID: 34388 RVA: 0x00219B09 File Offset: 0x00217D09
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Iterators.ReverseEnumerator<T>(this.m_list);
			}

			// Token: 0x06008655 RID: 34389 RVA: 0x00219B1B File Offset: 0x00217D1B
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04004816 RID: 18454
			private readonly IList<T> m_list;

			// Token: 0x04004817 RID: 18455
			private int m_index;

			// Token: 0x04004818 RID: 18456
			private T m_current;
		}
	}
}
