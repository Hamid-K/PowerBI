using System;
using System.Collections;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200000B RID: 11
	internal sealed class NameTable
	{
		// Token: 0x17000005 RID: 5
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

		// Token: 0x04000053 RID: 83
		private Hashtable table = new Hashtable();
	}
}
