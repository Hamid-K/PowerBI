using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000065 RID: 101
	public sealed class ItemAttachment<TItem> : ItemAttachment where TItem : Item
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x00011407 File Offset: 0x00010407
		internal ItemAttachment(Item owner)
			: base(owner)
		{
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00011410 File Offset: 0x00010410
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0001141D File Offset: 0x0001041D
		public new TItem Item
		{
			get
			{
				return (TItem)((object)base.Item);
			}
			internal set
			{
				base.Item = value;
			}
		}
	}
}
