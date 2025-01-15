using System;
using System.Collections;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003FB RID: 1019
	public sealed class QueryParameters : ArrayList
	{
		// Token: 0x17000919 RID: 2329
		public QueryParameter this[int index]
		{
			get
			{
				return (QueryParameter)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0007F4A0 File Offset: 0x0007D6A0
		internal QueryParameter Find(string name)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (string.Compare(this[i].Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return this[i];
				}
			}
			return null;
		}
	}
}
