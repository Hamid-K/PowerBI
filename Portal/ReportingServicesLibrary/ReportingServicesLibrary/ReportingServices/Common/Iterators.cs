using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000369 RID: 873
	internal static class Iterators
	{
		// Token: 0x06001CBF RID: 7359 RVA: 0x00073EEC File Offset: 0x000720EC
		public static Iterators.ReverseEnumerator<T> Reverse<T>(IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new Iterators.ReverseEnumerator<T>(list);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00073F02 File Offset: 0x00072102
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

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00073F19 File Offset: 0x00072119
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

		// Token: 0x020004F9 RID: 1273
		public struct ReverseEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator, IEnumerable<T>, IEnumerable
		{
			// Token: 0x060024CD RID: 9421 RVA: 0x00086EA4 File Offset: 0x000850A4
			internal ReverseEnumerator(IList<T> list)
			{
				this.m_list = list;
				this.m_index = this.m_list.Count;
				this.m_current = default(T);
			}

			// Token: 0x060024CE RID: 9422 RVA: 0x00086ECA File Offset: 0x000850CA
			public void Reset()
			{
				this.m_index = this.m_list.Count;
				this.m_current = default(T);
			}

			// Token: 0x17000AB0 RID: 2736
			// (get) Token: 0x060024CF RID: 9423 RVA: 0x00086EE9 File Offset: 0x000850E9
			public T Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x17000AB1 RID: 2737
			// (get) Token: 0x060024D0 RID: 9424 RVA: 0x00086EF1 File Offset: 0x000850F1
			object IEnumerator.Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x060024D1 RID: 9425 RVA: 0x00086EFE File Offset: 0x000850FE
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

			// Token: 0x060024D2 RID: 9426 RVA: 0x00086F30 File Offset: 0x00085130
			public Iterators.ReverseEnumerator<T> GetEnumerator()
			{
				return new Iterators.ReverseEnumerator<T>(this.m_list);
			}

			// Token: 0x060024D3 RID: 9427 RVA: 0x00086F3D File Offset: 0x0008513D
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060024D4 RID: 9428 RVA: 0x00086F4A File Offset: 0x0008514A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Iterators.ReverseEnumerator<T>(this.m_list);
			}

			// Token: 0x060024D5 RID: 9429 RVA: 0x00005BF2 File Offset: 0x00003DF2
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040011B5 RID: 4533
			private readonly IList<T> m_list;

			// Token: 0x040011B6 RID: 4534
			private int m_index;

			// Token: 0x040011B7 RID: 4535
			private T m_current;
		}
	}
}
