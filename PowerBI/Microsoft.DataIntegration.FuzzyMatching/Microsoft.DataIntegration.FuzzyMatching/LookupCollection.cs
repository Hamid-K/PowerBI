using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public class LookupCollection : List<Lookup>
	{
		// Token: 0x06000165 RID: 357 RVA: 0x000062CC File Offset: 0x000044CC
		public LookupCollection()
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000062D4 File Offset: 0x000044D4
		public LookupCollection(LookupCollection lookupCollection)
			: base(lookupCollection)
		{
		}

		// Token: 0x1700004B RID: 75
		public Lookup this[string lookupName]
		{
			get
			{
				return base.Find((Lookup d) => d.Name.Equals(lookupName));
			}
		}
	}
}
