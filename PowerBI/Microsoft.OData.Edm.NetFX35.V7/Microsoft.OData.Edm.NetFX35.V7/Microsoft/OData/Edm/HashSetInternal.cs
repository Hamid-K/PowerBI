using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000015 RID: 21
	internal class HashSetInternal<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000170 RID: 368 RVA: 0x000074CF File Offset: 0x000056CF
		public HashSetInternal()
		{
			this.wrappedDictionary = new Dictionary<T, object>();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000074E2 File Offset: 0x000056E2
		public bool Add(T thingToAdd)
		{
			if (this.wrappedDictionary.ContainsKey(thingToAdd))
			{
				return false;
			}
			this.wrappedDictionary[thingToAdd] = null;
			return true;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007502 File Offset: 0x00005702
		public bool Contains(T item)
		{
			return this.wrappedDictionary.ContainsKey(item);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007510 File Offset: 0x00005710
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007518 File Offset: 0x00005718
		public IEnumerator<T> GetEnumerator()
		{
			return this.wrappedDictionary.Keys.GetEnumerator();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000752F File Offset: 0x0000572F
		public void Remove(T item)
		{
			this.wrappedDictionary.Remove(item);
		}

		// Token: 0x0400002E RID: 46
		private readonly Dictionary<T, object> wrappedDictionary;
	}
}
