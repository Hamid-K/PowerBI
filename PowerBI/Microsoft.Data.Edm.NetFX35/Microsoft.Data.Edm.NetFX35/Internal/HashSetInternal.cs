using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Internal
{
	// Token: 0x020001C4 RID: 452
	internal class HashSetInternal<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x0001FCEF File Offset: 0x0001DEEF
		public HashSetInternal()
		{
			this.wrappedDictionary = new Dictionary<T, object>();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001FD02 File Offset: 0x0001DF02
		public bool Add(T thingToAdd)
		{
			if (this.wrappedDictionary.ContainsKey(thingToAdd))
			{
				return false;
			}
			this.wrappedDictionary[thingToAdd] = null;
			return true;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001FD22 File Offset: 0x0001DF22
		public bool Contains(T item)
		{
			return this.wrappedDictionary.ContainsKey(item);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001FD30 File Offset: 0x0001DF30
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001FD38 File Offset: 0x0001DF38
		public IEnumerator<T> GetEnumerator()
		{
			return this.wrappedDictionary.Keys.GetEnumerator();
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001FD4F File Offset: 0x0001DF4F
		public void Remove(T item)
		{
			this.wrappedDictionary.Remove(item);
		}

		// Token: 0x04000513 RID: 1299
		private readonly Dictionary<T, object> wrappedDictionary;
	}
}
