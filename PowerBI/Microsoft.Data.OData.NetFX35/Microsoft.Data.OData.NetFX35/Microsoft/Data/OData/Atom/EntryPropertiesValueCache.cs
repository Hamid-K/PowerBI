using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200026A RID: 618
	internal sealed class EntryPropertiesValueCache : EpmValueCache
	{
		// Token: 0x06001346 RID: 4934 RVA: 0x0004845E File Offset: 0x0004665E
		internal EntryPropertiesValueCache(ODataEntry entry)
		{
			if (entry.Properties != null)
			{
				this.entryPropertiesCache = new List<ODataProperty>(entry.Properties);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00048497 File Offset: 0x00046697
		internal IEnumerable<ODataProperty> EntryProperties
		{
			get
			{
				if (this.entryPropertiesCache == null)
				{
					return null;
				}
				return Enumerable.Where<ODataProperty>(this.entryPropertiesCache, (ODataProperty p) => p == null || !(p.Value is ODataStreamReferenceValue));
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x000484E0 File Offset: 0x000466E0
		internal IEnumerable<ODataProperty> EntryStreamProperties
		{
			get
			{
				if (this.entryPropertiesCache == null)
				{
					return null;
				}
				return Enumerable.Where<ODataProperty>(this.entryPropertiesCache, (ODataProperty p) => p != null && p.Value is ODataStreamReferenceValue);
			}
		}

		// Token: 0x0400072D RID: 1837
		private readonly List<ODataProperty> entryPropertiesCache;
	}
}
