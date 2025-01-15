using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000283 RID: 643
	public sealed class MailboxStatisticsItem
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0003E9EC File Offset: 0x0003D9EC
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x0003E9F4 File Offset: 0x0003D9F4
		public string MailboxId { get; set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0003E9FD File Offset: 0x0003D9FD
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x0003EA05 File Offset: 0x0003DA05
		public string DisplayName { get; set; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0003EA0E File Offset: 0x0003DA0E
		// (set) Token: 0x060016B0 RID: 5808 RVA: 0x0003EA16 File Offset: 0x0003DA16
		public long ItemCount { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0003EA1F File Offset: 0x0003DA1F
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x0003EA27 File Offset: 0x0003DA27
		[CLSCompliant(false)]
		public ulong Size { get; set; }

		// Token: 0x060016B3 RID: 5811 RVA: 0x0003EA30 File Offset: 0x0003DA30
		internal static MailboxStatisticsItem LoadFromXml(EwsServiceXmlReader reader)
		{
			MailboxStatisticsItem mailboxStatisticsItem = new MailboxStatisticsItem();
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "MailboxStat");
			mailboxStatisticsItem.MailboxId = reader.ReadElementValue(XmlNamespace.Types, "MailboxId");
			mailboxStatisticsItem.DisplayName = reader.ReadElementValue(XmlNamespace.Types, "DisplayName");
			mailboxStatisticsItem.ItemCount = reader.ReadElementValue<long>(XmlNamespace.Types, "ItemCount");
			mailboxStatisticsItem.Size = reader.ReadElementValue<ulong>(XmlNamespace.Types, "Size");
			return mailboxStatisticsItem;
		}
	}
}
