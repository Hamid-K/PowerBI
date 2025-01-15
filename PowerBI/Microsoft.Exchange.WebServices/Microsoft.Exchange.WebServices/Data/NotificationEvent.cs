using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002B3 RID: 691
	public abstract class NotificationEvent
	{
		// Token: 0x060018B2 RID: 6322 RVA: 0x000435AE File Offset: 0x000425AE
		internal NotificationEvent(EventType eventType, DateTime timestamp)
		{
			this.eventType = eventType;
			this.timestamp = timestamp;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x000435C4 File Offset: 0x000425C4
		internal virtual void InternalLoadFromXml(EwsServiceXmlReader reader)
		{
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x000435C6 File Offset: 0x000425C6
		internal void LoadFromXml(EwsServiceXmlReader reader, string xmlElementName)
		{
			this.InternalLoadFromXml(reader);
			reader.ReadEndElementIfNecessary(XmlNamespace.Types, xmlElementName);
		}

		// Token: 0x060018B5 RID: 6325
		internal abstract void LoadFromJson(JsonObject jsonEvent, ExchangeService service);

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x000435D7 File Offset: 0x000425D7
		public EventType EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x000435DF File Offset: 0x000425DF
		public DateTime TimeStamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x000435E7 File Offset: 0x000425E7
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x000435EF File Offset: 0x000425EF
		public FolderId ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
			internal set
			{
				this.parentFolderId = value;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x000435F8 File Offset: 0x000425F8
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x00043600 File Offset: 0x00042600
		public FolderId OldParentFolderId
		{
			get
			{
				return this.oldParentFolderId;
			}
			internal set
			{
				this.oldParentFolderId = value;
			}
		}

		// Token: 0x040013C9 RID: 5065
		private EventType eventType;

		// Token: 0x040013CA RID: 5066
		private DateTime timestamp;

		// Token: 0x040013CB RID: 5067
		private FolderId parentFolderId;

		// Token: 0x040013CC RID: 5068
		private FolderId oldParentFolderId;
	}
}
