using System;
using System.Collections;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x0200039A RID: 922
	internal class NameTable
	{
		// Token: 0x06001E68 RID: 7784 RVA: 0x0007C97C File Offset: 0x0007AB7C
		public void Add(string name, string ns, object value)
		{
			NameKey nameKey = new NameKey(name, ns);
			this.table.Add(nameKey, value);
		}

		// Token: 0x17000882 RID: 2178
		public object this[string name, string ns]
		{
			get
			{
				return this.table[new NameKey(name, ns)];
			}
			set
			{
				this.table[new NameKey(name, ns)] = value;
			}
		}

		// Token: 0x04000CDD RID: 3293
		private Hashtable table = new Hashtable();
	}
}
