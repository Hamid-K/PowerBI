using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000284 RID: 644
	public sealed class SearchPreviewItem
	{
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0003EAA0 File Offset: 0x0003DAA0
		// (set) Token: 0x060016B6 RID: 5814 RVA: 0x0003EAA8 File Offset: 0x0003DAA8
		public ItemId Id { get; set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0003EAB1 File Offset: 0x0003DAB1
		// (set) Token: 0x060016B8 RID: 5816 RVA: 0x0003EAB9 File Offset: 0x0003DAB9
		public PreviewItemMailbox Mailbox { get; set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0003EAC2 File Offset: 0x0003DAC2
		// (set) Token: 0x060016BA RID: 5818 RVA: 0x0003EACA File Offset: 0x0003DACA
		public ItemId ParentId { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x0003EAD3 File Offset: 0x0003DAD3
		// (set) Token: 0x060016BC RID: 5820 RVA: 0x0003EADB File Offset: 0x0003DADB
		public string ItemClass { get; set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x0003EAE4 File Offset: 0x0003DAE4
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x0003EAEC File Offset: 0x0003DAEC
		public string UniqueHash { get; set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x0003EAF5 File Offset: 0x0003DAF5
		// (set) Token: 0x060016C0 RID: 5824 RVA: 0x0003EAFD File Offset: 0x0003DAFD
		public string SortValue { get; set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0003EB06 File Offset: 0x0003DB06
		// (set) Token: 0x060016C2 RID: 5826 RVA: 0x0003EB0E File Offset: 0x0003DB0E
		public string OwaLink { get; set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x0003EB17 File Offset: 0x0003DB17
		// (set) Token: 0x060016C4 RID: 5828 RVA: 0x0003EB1F File Offset: 0x0003DB1F
		public string Sender { get; set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0003EB28 File Offset: 0x0003DB28
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x0003EB30 File Offset: 0x0003DB30
		public string[] ToRecipients { get; set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0003EB39 File Offset: 0x0003DB39
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0003EB41 File Offset: 0x0003DB41
		public string[] CcRecipients { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0003EB4A File Offset: 0x0003DB4A
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x0003EB52 File Offset: 0x0003DB52
		public string[] BccRecipients { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0003EB5B File Offset: 0x0003DB5B
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x0003EB63 File Offset: 0x0003DB63
		public DateTime CreatedTime { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0003EB6C File Offset: 0x0003DB6C
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0003EB74 File Offset: 0x0003DB74
		public DateTime ReceivedTime { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0003EB7D File Offset: 0x0003DB7D
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0003EB85 File Offset: 0x0003DB85
		public DateTime SentTime { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0003EB8E File Offset: 0x0003DB8E
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x0003EB96 File Offset: 0x0003DB96
		public string Subject { get; set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0003EB9F File Offset: 0x0003DB9F
		// (set) Token: 0x060016D4 RID: 5844 RVA: 0x0003EBA7 File Offset: 0x0003DBA7
		[CLSCompliant(false)]
		public ulong Size { get; set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0003EBB0 File Offset: 0x0003DBB0
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x0003EBB8 File Offset: 0x0003DBB8
		public string Preview { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0003EBC1 File Offset: 0x0003DBC1
		// (set) Token: 0x060016D8 RID: 5848 RVA: 0x0003EBC9 File Offset: 0x0003DBC9
		public Importance Importance { get; set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0003EBD2 File Offset: 0x0003DBD2
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x0003EBDA File Offset: 0x0003DBDA
		public bool Read { get; set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x0003EBE3 File Offset: 0x0003DBE3
		// (set) Token: 0x060016DC RID: 5852 RVA: 0x0003EBEB File Offset: 0x0003DBEB
		public bool HasAttachment { get; set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0003EBF4 File Offset: 0x0003DBF4
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x0003EBFC File Offset: 0x0003DBFC
		public ExtendedPropertyCollection ExtendedProperties { get; set; }
	}
}
