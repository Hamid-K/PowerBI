using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C8 RID: 1224
	public abstract class GlobalItem : MetadataItem
	{
		// Token: 0x06003C7E RID: 15486 RVA: 0x000C8A1F File Offset: 0x000C6C1F
		internal GlobalItem()
		{
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x000C8A27 File Offset: 0x000C6C27
		internal GlobalItem(MetadataItem.MetadataFlags flags)
			: base(flags)
		{
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06003C80 RID: 15488 RVA: 0x000C8A30 File Offset: 0x000C6C30
		// (set) Token: 0x06003C81 RID: 15489 RVA: 0x000C8A38 File Offset: 0x000C6C38
		[MetadataProperty(typeof(DataSpace), false)]
		internal virtual DataSpace DataSpace
		{
			get
			{
				return base.GetDataSpace();
			}
			set
			{
				base.SetDataSpace(value);
			}
		}
	}
}
