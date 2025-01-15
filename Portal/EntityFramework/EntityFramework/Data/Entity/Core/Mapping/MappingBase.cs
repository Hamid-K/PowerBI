using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000545 RID: 1349
	public abstract class MappingBase : GlobalItem
	{
		// Token: 0x06004201 RID: 16897 RVA: 0x000E0236 File Offset: 0x000DE436
		internal MappingBase()
			: base(MetadataItem.MetadataFlags.Readonly)
		{
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x000E023F File Offset: 0x000DE43F
		internal MappingBase(MetadataItem.MetadataFlags flags)
			: base(flags)
		{
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06004203 RID: 16899
		internal abstract MetadataItem EdmItem { get; }
	}
}
