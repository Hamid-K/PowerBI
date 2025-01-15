using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002D8 RID: 728
	public class NameTable<T>
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x000336CB File Offset: 0x000318CB
		public IEnumerable<NameKey> Keys
		{
			get
			{
				foreach (object obj in this.table.Keys)
				{
					yield return (NameKey)obj;
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x000336DB File Offset: 0x000318DB
		public IEnumerable<T> Values
		{
			get
			{
				foreach (object obj in this.table.Values)
				{
					yield return (T)((object)obj);
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000336EC File Offset: 0x000318EC
		public void Add(string name, string ns, T value)
		{
			NameKey nameKey = new NameKey(name, ns);
			this.table.Add(nameKey, value);
		}

		// Token: 0x17000715 RID: 1813
		public T this[string name, string ns]
		{
			get
			{
				return (T)((object)this.table[new NameKey(name, ns)]);
			}
			set
			{
				this.table[new NameKey(name, ns)] = value;
			}
		}

		// Token: 0x040006ED RID: 1773
		private readonly Hashtable table = new Hashtable();
	}
}
