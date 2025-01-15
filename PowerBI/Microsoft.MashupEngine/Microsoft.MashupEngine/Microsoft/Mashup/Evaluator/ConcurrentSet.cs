using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C4E RID: 7246
	internal sealed class ConcurrentSet<T>
	{
		// Token: 0x0600B4C8 RID: 46280 RVA: 0x0024A71D File Offset: 0x0024891D
		public ConcurrentSet()
		{
			this.set = new HashSet<T>();
		}

		// Token: 0x0600B4C9 RID: 46281 RVA: 0x0024A730 File Offset: 0x00248930
		public T[] ToArray()
		{
			HashSet<T> hashSet = this.set;
			T[] array;
			lock (hashSet)
			{
				array = this.set.ToArray<T>();
			}
			return array;
		}

		// Token: 0x0600B4CA RID: 46282 RVA: 0x0024A778 File Offset: 0x00248978
		public void Add(T item)
		{
			HashSet<T> hashSet = this.set;
			lock (hashSet)
			{
				this.set.Add(item);
			}
		}

		// Token: 0x0600B4CB RID: 46283 RVA: 0x0024A7C0 File Offset: 0x002489C0
		public bool Remove(T item)
		{
			HashSet<T> hashSet = this.set;
			bool flag2;
			lock (hashSet)
			{
				flag2 = this.set.Remove(item);
			}
			return flag2;
		}

		// Token: 0x0600B4CC RID: 46284 RVA: 0x0024A808 File Offset: 0x00248A08
		public void AddRange(IEnumerable<T> range)
		{
			HashSet<T> hashSet = this.set;
			lock (hashSet)
			{
				this.set.UnionWith(range);
			}
		}

		// Token: 0x0600B4CD RID: 46285 RVA: 0x0024A850 File Offset: 0x00248A50
		public void RemoveRange(IEnumerable<T> range)
		{
			HashSet<T> hashSet = this.set;
			lock (hashSet)
			{
				this.set.ExceptWith(range);
			}
		}

		// Token: 0x04005BE6 RID: 23526
		private readonly HashSet<T> set;
	}
}
