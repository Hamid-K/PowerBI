using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002B8 RID: 696
	public sealed class ItemEvent : NotificationEvent
	{
		// Token: 0x060018D5 RID: 6357 RVA: 0x00043CC5 File Offset: 0x00042CC5
		internal ItemEvent(EventType eventType, DateTime timestamp)
			: base(eventType, timestamp)
		{
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00043CD0 File Offset: 0x00042CD0
		internal override void InternalLoadFromXml(EwsServiceXmlReader reader)
		{
			base.InternalLoadFromXml(reader);
			this.itemId = new ItemId();
			this.itemId.LoadFromXml(reader, reader.LocalName);
			reader.Read();
			base.ParentFolderId = new FolderId();
			base.ParentFolderId.LoadFromXml(reader, "ParentFolderId");
			switch (base.EventType)
			{
			case EventType.Moved:
			case EventType.Copied:
				reader.Read();
				this.oldItemId = new ItemId();
				this.oldItemId.LoadFromXml(reader, reader.LocalName);
				reader.Read();
				base.OldParentFolderId = new FolderId();
				base.OldParentFolderId.LoadFromXml(reader, reader.LocalName);
				return;
			default:
				return;
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00043D84 File Offset: 0x00042D84
		internal override void LoadFromJson(JsonObject jsonEvent, ExchangeService service)
		{
			this.itemId = new ItemId();
			this.itemId.LoadFromJson(jsonEvent.ReadAsJsonObject("ItemId"), service);
			base.ParentFolderId = new FolderId();
			base.ParentFolderId.LoadFromJson(jsonEvent.ReadAsJsonObject("ParentFolderId"), service);
			switch (base.EventType)
			{
			case EventType.Moved:
			case EventType.Copied:
				this.oldItemId = new ItemId();
				this.oldItemId.LoadFromJson(jsonEvent.ReadAsJsonObject("OldItemId"), service);
				base.OldParentFolderId = new FolderId();
				base.OldParentFolderId.LoadFromJson(jsonEvent.ReadAsJsonObject("OldParentFolderId"), service);
				return;
			default:
				return;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x00043E31 File Offset: 0x00042E31
		public ItemId ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00043E39 File Offset: 0x00042E39
		public ItemId OldItemId
		{
			get
			{
				return this.oldItemId;
			}
		}

		// Token: 0x040013DA RID: 5082
		private ItemId itemId;

		// Token: 0x040013DB RID: 5083
		private ItemId oldItemId;
	}
}
