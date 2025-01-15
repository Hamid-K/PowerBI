using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AB RID: 171
	internal class HashSetInternal<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0000A74F File Offset: 0x0000894F
		public HashSetInternal()
		{
			this.wrappedDictionary = new Dictionary<T, object>();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000A762 File Offset: 0x00008962
		public bool Add(T thingToAdd)
		{
			if (this.wrappedDictionary.ContainsKey(thingToAdd))
			{
				return false;
			}
			this.wrappedDictionary[thingToAdd] = null;
			return true;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000A782 File Offset: 0x00008982
		public bool Contains(T item)
		{
			return this.wrappedDictionary.ContainsKey(item);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000A790 File Offset: 0x00008990
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000A798 File Offset: 0x00008998
		public IEnumerator<T> GetEnumerator()
		{
			return this.wrappedDictionary.Keys.GetEnumerator();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000A7AF File Offset: 0x000089AF
		public void Remove(T item)
		{
			this.wrappedDictionary.Remove(item);
		}

		// Token: 0x04000138 RID: 312
		private readonly Dictionary<T, object> wrappedDictionary;
	}
}
