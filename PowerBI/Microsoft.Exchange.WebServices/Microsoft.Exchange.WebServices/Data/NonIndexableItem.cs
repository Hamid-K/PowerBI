using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000278 RID: 632
	public sealed class NonIndexableItem
	{
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0003D8CD File Offset: 0x0003C8CD
		// (set) Token: 0x06001628 RID: 5672 RVA: 0x0003D8D5 File Offset: 0x0003C8D5
		public ItemId ItemId { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0003D8DE File Offset: 0x0003C8DE
		// (set) Token: 0x0600162A RID: 5674 RVA: 0x0003D8E6 File Offset: 0x0003C8E6
		public ItemIndexError ErrorCode { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0003D8EF File Offset: 0x0003C8EF
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x0003D8F7 File Offset: 0x0003C8F7
		public string ErrorDescription { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x0003D900 File Offset: 0x0003C900
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x0003D908 File Offset: 0x0003C908
		public bool IsPartiallyIndexed { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0003D911 File Offset: 0x0003C911
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x0003D919 File Offset: 0x0003C919
		public bool IsPermanentFailure { get; set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x0003D922 File Offset: 0x0003C922
		// (set) Token: 0x06001632 RID: 5682 RVA: 0x0003D92A File Offset: 0x0003C92A
		public int AttemptCount { get; set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x0003D933 File Offset: 0x0003C933
		// (set) Token: 0x06001634 RID: 5684 RVA: 0x0003D93B File Offset: 0x0003C93B
		public DateTime? LastAttemptTime { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0003D944 File Offset: 0x0003C944
		// (set) Token: 0x06001636 RID: 5686 RVA: 0x0003D94C File Offset: 0x0003C94C
		public string AdditionalInfo { get; set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x0003D955 File Offset: 0x0003C955
		// (set) Token: 0x06001638 RID: 5688 RVA: 0x0003D95D File Offset: 0x0003C95D
		public string SortValue { get; set; }

		// Token: 0x06001639 RID: 5689 RVA: 0x0003D968 File Offset: 0x0003C968
		internal static NonIndexableItem LoadFromXml(EwsServiceXmlReader reader)
		{
			NonIndexableItem nonIndexableItem = null;
			if (reader.IsStartElement(XmlNamespace.Types, "NonIndexableItemDetail"))
			{
				ItemId itemId = null;
				ItemIndexError itemIndexError = ItemIndexError.None;
				string text = null;
				bool flag = false;
				bool flag2 = false;
				int num = 0;
				DateTime? dateTime = default(DateTime?);
				string text2 = null;
				string text3 = null;
				do
				{
					reader.Read();
					if (reader.IsStartElement(XmlNamespace.Types, "ItemId"))
					{
						itemId = new ItemId();
						itemId.ReadAttributesFromXml(reader);
					}
					else if (reader.IsStartElement(XmlNamespace.Types, "ErrorDescription"))
					{
						text = reader.ReadElementValue(XmlNamespace.Types, "ErrorDescription");
					}
					else if (reader.IsStartElement(XmlNamespace.Types, "IsPartiallyIndexed"))
					{
						flag = reader.ReadElementValue<bool>(XmlNamespace.Types, "IsPartiallyIndexed");
					}
					else if (reader.IsStartElement(XmlNamespace.Types, "IsPermanentFailure"))
					{
						flag2 = reader.ReadElementValue<bool>(XmlNamespace.Types, "IsPermanentFailure");
					}
					else if (reader.IsStartElement(XmlNamespace.Types, "AttemptCount"))
					{
						num = reader.ReadElementValue<int>(XmlNamespace.Types, "AttemptCount");
					}
					else if (reader.IsStartElement(XmlNamespace.Types, "LastAttemptTime"))
					{
						dateTime = new DateTime?(reader.ReadElementValue<DateTime>(XmlNamespace.Types, "LastAttemptTime"));
					}
					else if (reader.IsStartElement(XmlNamespace.Types, "AdditionalInfo"))
					{
						text2 = reader.ReadElementValue(XmlNamespace.Types, "AdditionalInfo");
					}
					else if (reader.IsStartElement(XmlNamespace.Types, "SortValue"))
					{
						text3 = reader.ReadElementValue(XmlNamespace.Types, "SortValue");
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Types, "NonIndexableItemDetail"));
				nonIndexableItem = new NonIndexableItem
				{
					ItemId = itemId,
					ErrorCode = itemIndexError,
					ErrorDescription = text,
					IsPartiallyIndexed = flag,
					IsPermanentFailure = flag2,
					AttemptCount = num,
					LastAttemptTime = dateTime,
					AdditionalInfo = text2,
					SortValue = text3
				};
			}
			return nonIndexableItem;
		}
	}
}
