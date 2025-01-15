using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001F2 RID: 498
	internal class HashSetInternal<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x000214A3 File Offset: 0x0001F6A3
		public HashSetInternal()
		{
			this.wrappedDictionary = new Dictionary<T, object>();
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000214B6 File Offset: 0x0001F6B6
		public bool Add(T thingToAdd)
		{
			if (this.wrappedDictionary.ContainsKey(thingToAdd))
			{
				return false;
			}
			this.wrappedDictionary[thingToAdd] = null;
			return true;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000214D6 File Offset: 0x0001F6D6
		public bool Contains(T item)
		{
			return this.wrappedDictionary.ContainsKey(item);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000214E4 File Offset: 0x0001F6E4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000214EC File Offset: 0x0001F6EC
		public IEnumerator<T> GetEnumerator()
		{
			return this.wrappedDictionary.Keys.GetEnumerator();
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00021503 File Offset: 0x0001F703
		public void Remove(T item)
		{
			this.wrappedDictionary.Remove(item);
		}

		// Token: 0x04000562 RID: 1378
		private readonly Dictionary<T, object> wrappedDictionary;
	}
}
