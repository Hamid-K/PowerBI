using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002B4 RID: 692
	public class FolderEvent : NotificationEvent
	{
		// Token: 0x060018BC RID: 6332 RVA: 0x00043609 File Offset: 0x00042609
		internal FolderEvent(EventType eventType, DateTime timestamp)
			: base(eventType, timestamp)
		{
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00043614 File Offset: 0x00042614
		internal override void InternalLoadFromXml(EwsServiceXmlReader reader)
		{
			base.InternalLoadFromXml(reader);
			this.folderId = new FolderId();
			this.folderId.LoadFromXml(reader, reader.LocalName);
			reader.Read();
			base.ParentFolderId = new FolderId();
			base.ParentFolderId.LoadFromXml(reader, "ParentFolderId");
			switch (base.EventType)
			{
			case EventType.Modified:
				reader.Read();
				if (reader.IsStartElement())
				{
					reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "UnreadCount");
					this.unreadCount = new int?(int.Parse(reader.ReadValue()));
				}
				return;
			case EventType.Moved:
			case EventType.Copied:
				reader.Read();
				this.oldFolderId = new FolderId();
				this.oldFolderId.LoadFromXml(reader, reader.LocalName);
				reader.Read();
				base.OldParentFolderId = new FolderId();
				base.OldParentFolderId.LoadFromXml(reader, reader.LocalName);
				return;
			default:
				return;
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000436FC File Offset: 0x000426FC
		internal override void LoadFromJson(JsonObject jsonEvent, ExchangeService service)
		{
			this.folderId = new FolderId();
			this.folderId.LoadFromJson(jsonEvent.ReadAsJsonObject("FolderId"), service);
			base.ParentFolderId = new FolderId();
			base.ParentFolderId.LoadFromJson(jsonEvent.ReadAsJsonObject("ParentFolderId"), service);
			switch (base.EventType)
			{
			case EventType.Modified:
				if (jsonEvent.ContainsKey("UnreadCount"))
				{
					this.unreadCount = new int?(jsonEvent.ReadAsInt("UnreadCount"));
				}
				return;
			case EventType.Moved:
			case EventType.Copied:
				this.oldFolderId = new FolderId();
				this.oldFolderId.LoadFromJson(jsonEvent.ReadAsJsonObject("OldFolderId"), service);
				base.OldParentFolderId = new FolderId();
				base.OldParentFolderId.LoadFromJson(jsonEvent.ReadAsJsonObject("OldParentFolderId"), service);
				return;
			default:
				return;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x000437D1 File Offset: 0x000427D1
		public FolderId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x000437D9 File Offset: 0x000427D9
		public FolderId OldFolderId
		{
			get
			{
				return this.oldFolderId;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x000437E1 File Offset: 0x000427E1
		public int? UnreadCount
		{
			get
			{
				return this.unreadCount;
			}
		}

		// Token: 0x040013CD RID: 5069
		private FolderId folderId;

		// Token: 0x040013CE RID: 5070
		private FolderId oldFolderId;

		// Token: 0x040013CF RID: 5071
		private int? unreadCount;
	}
}
