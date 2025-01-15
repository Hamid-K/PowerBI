using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007B9 RID: 1977
	internal sealed class LookupsImpl : Lookups
	{
		// Token: 0x06007035 RID: 28725 RVA: 0x001D389F File Offset: 0x001D1A9F
		internal LookupsImpl()
		{
		}

		// Token: 0x1700263E RID: 9790
		public override Lookup this[string key]
		{
			get
			{
				LookupImpl lookupImpl = null;
				if (key == null || this.m_collection == null || !this.m_collection.TryGetValue(key, out lookupImpl))
				{
					throw new ReportProcessingException_NonExistingLookupReference();
				}
				return lookupImpl;
			}
		}

		// Token: 0x06007037 RID: 28727 RVA: 0x001D38D9 File Offset: 0x001D1AD9
		internal void Add(LookupImpl lookup)
		{
			if (this.m_collection == null)
			{
				this.m_collection = new Dictionary<string, LookupImpl>();
			}
			this.m_collection.Add(lookup.Name, lookup);
		}

		// Token: 0x040039FB RID: 14843
		private Dictionary<string, LookupImpl> m_collection;
	}
}
