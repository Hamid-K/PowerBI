using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000025 RID: 37
	internal sealed class EntryPropertiesValueCache
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00004EE7 File Offset: 0x000030E7
		internal EntryPropertiesValueCache(ODataEntry entry)
		{
			if (entry.Properties != null)
			{
				this.entryPropertiesCache = new List<ODataProperty>(entry.Properties);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004F20 File Offset: 0x00003120
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004F69 File Offset: 0x00003169
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

		// Token: 0x04000107 RID: 263
		private readonly List<ODataProperty> entryPropertiesCache;
	}
}
