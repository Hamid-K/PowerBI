using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200025D RID: 605
	public class MultiEnumerableIterator<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x00037110 File Offset: 0x00035310
		public MultiEnumerableIterator(IEnumerable<IEnumerable<T>> enumerableEnumerable)
		{
			foreach (IEnumerable<T> enumerable in enumerableEnumerable)
			{
				this.m_enumerableCollection.Add(enumerable);
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00037170 File Offset: 0x00035370
		public MultiEnumerableIterator(params IEnumerable<T>[] enumerableParams)
		{
			foreach (IEnumerable<T> enumerable in enumerableParams)
			{
				this.m_enumerableCollection.Add(enumerable);
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000371AE File Offset: 0x000353AE
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new MultiEnumerator<T>(this.m_enumerableCollection);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000371BB File Offset: 0x000353BB
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x040005FC RID: 1532
		private readonly Collection<IEnumerable<T>> m_enumerableCollection = new Collection<IEnumerable<T>>();
	}
}
