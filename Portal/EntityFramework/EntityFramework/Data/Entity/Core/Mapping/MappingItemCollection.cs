using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000549 RID: 1353
	public abstract class MappingItemCollection : ItemCollection
	{
		// Token: 0x0600422C RID: 16940 RVA: 0x000E07AB File Offset: 0x000DE9AB
		internal MappingItemCollection(DataSpace dataSpace)
			: base(dataSpace)
		{
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000E07B4 File Offset: 0x000DE9B4
		internal virtual bool TryGetMap(string identity, DataSpace typeSpace, out MappingBase map)
		{
			throw Error.NotSupported();
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000E07BB File Offset: 0x000DE9BB
		internal virtual MappingBase GetMap(GlobalItem item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x000E07C2 File Offset: 0x000DE9C2
		internal virtual bool TryGetMap(GlobalItem item, out MappingBase map)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x000E07C9 File Offset: 0x000DE9C9
		internal virtual MappingBase GetMap(string identity, DataSpace typeSpace, bool ignoreCase)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000E07D0 File Offset: 0x000DE9D0
		internal virtual bool TryGetMap(string identity, DataSpace typeSpace, bool ignoreCase, out MappingBase map)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x000E07D7 File Offset: 0x000DE9D7
		internal virtual MappingBase GetMap(string identity, DataSpace typeSpace)
		{
			throw Error.NotSupported();
		}
	}
}
