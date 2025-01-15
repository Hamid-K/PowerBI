using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000308 RID: 776
	public sealed class ItemChange : Change
	{
		// Token: 0x06001B95 RID: 7061 RVA: 0x00049999 File Offset: 0x00048999
		internal ItemChange()
		{
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000499A1 File Offset: 0x000489A1
		internal override ServiceId CreateId()
		{
			return new ItemId();
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000499A8 File Offset: 0x000489A8
		public Item Item
		{
			get
			{
				return (Item)base.ServiceObject;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x000499B5 File Offset: 0x000489B5
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x000499BD File Offset: 0x000489BD
		public bool IsRead
		{
			get
			{
				return this.isRead;
			}
			internal set
			{
				this.isRead = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x000499C6 File Offset: 0x000489C6
		public ItemId ItemId
		{
			get
			{
				return (ItemId)base.Id;
			}
		}

		// Token: 0x0400145D RID: 5213
		private bool isRead;
	}
}
