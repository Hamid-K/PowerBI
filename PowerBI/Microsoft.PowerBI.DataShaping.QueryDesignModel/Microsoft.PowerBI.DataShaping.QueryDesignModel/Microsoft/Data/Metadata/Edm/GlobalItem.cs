using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000096 RID: 150
	public abstract class GlobalItem : MetadataItem
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x00019065 File Offset: 0x00017265
		internal GlobalItem()
		{
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0001906D File Offset: 0x0001726D
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00019075 File Offset: 0x00017275
		[MetadataProperty(typeof(DataSpace), false)]
		internal DataSpace DataSpace
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
