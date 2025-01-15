using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000D5 RID: 213
	internal class DataCacheNamedClientCollection : ConfigurationElementCollection
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x00017ED8 File Offset: 0x000160D8
		protected override ConfigurationElement CreateNewElement()
		{
			return new DataCacheNamedClient();
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00017EDF File Offset: 0x000160DF
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DataCacheNamedClient)element).ClientName;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00017EEC File Offset: 0x000160EC
		internal void Add(DataCacheNamedClient element)
		{
			base.BaseAdd(element, true);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00017EF6 File Offset: 0x000160F6
		internal DataCacheNamedClient Get(string name)
		{
			return (DataCacheNamedClient)base.BaseGet(name);
		}
	}
}
