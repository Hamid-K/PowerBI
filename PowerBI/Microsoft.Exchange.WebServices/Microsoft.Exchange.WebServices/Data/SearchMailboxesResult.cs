using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000281 RID: 641
	public sealed class SearchMailboxesResult
	{
		// Token: 0x06001684 RID: 5764 RVA: 0x0003E0A0 File Offset: 0x0003D0A0
		internal static SearchMailboxesResult LoadFromXml(EwsServiceXmlReader reader)
		{
			SearchMailboxesResult searchMailboxesResult = new SearchMailboxesResult();
			reader.ReadStartElement(XmlNamespace.Messages, "SearchMailboxesResult");
			List<MailboxQuery> list = new List<MailboxQuery>();
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "SearchQueries"))
				{
					reader.ReadStartElement(XmlNamespace.Types, "MailboxQuery");
					string text = reader.ReadElementValue(XmlNamespace.Types, "Query");
					reader.ReadStartElement(XmlNamespace.Types, "MailboxSearchScopes");
					List<MailboxSearchScope> list2 = new List<MailboxSearchScope>();
					do
					{
						reader.Read();
						if (reader.IsStartElement(XmlNamespace.Types, "MailboxSearchScope"))
						{
							string text2 = reader.ReadElementValue(XmlNamespace.Types, "Mailbox");
							reader.ReadStartElement(XmlNamespace.Types, "SearchScope");
							string text3 = reader.ReadElementValue(XmlNamespace.Types, "SearchScope");
							reader.ReadEndElement(XmlNamespace.Types, "MailboxSearchScope");
							list2.Add(new MailboxSearchScope(text2, (MailboxSearchLocation)Enum.Parse(typeof(MailboxSearchLocation), text3)));
						}
					}
					while (!reader.IsEndElement(XmlNamespace.Types, "MailboxSearchScopes"));
					reader.ReadEndElementIfNecessary(XmlNamespace.Types, "MailboxSearchScopes");
					list.Add(new MailboxQuery(text, list2.ToArray()));
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Types, "SearchQueries"));
			reader.ReadEndElementIfNecessary(XmlNamespace.Types, "SearchQueries");
			searchMailboxesResult.SearchQueries = list.ToArray();
			searchMailboxesResult.ResultType = (SearchResultType)Enum.Parse(typeof(SearchResultType), reader.ReadElementValue(XmlNamespace.Types, "ResultType"));
			searchMailboxesResult.ItemCount = (long)int.Parse(reader.ReadElementValue(XmlNamespace.Types, "ItemCount"));
			searchMailboxesResult.Size = ulong.Parse(reader.ReadElementValue(XmlNamespace.Types, "Size"));
			searchMailboxesResult.PageItemCount = int.Parse(reader.ReadElementValue(XmlNamespace.Types, "PageItemCount"));
			searchMailboxesResult.PageItemSize = ulong.Parse(reader.ReadElementValue(XmlNamespace.Types, "PageItemSize"));
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "KeywordStats"))
				{
					searchMailboxesResult.KeywordStats = SearchMailboxesResult.LoadKeywordStatsXml(reader);
				}
				if (reader.IsStartElement(XmlNamespace.Types, "Items"))
				{
					searchMailboxesResult.PreviewItems = SearchMailboxesResult.LoadPreviewItemsXml(reader);
				}
				if (reader.IsStartElement(XmlNamespace.Types, "FailedMailboxes"))
				{
					searchMailboxesResult.FailedMailboxes = FailedSearchMailbox.LoadFailedMailboxesXml(XmlNamespace.Types, reader);
				}
				if (reader.IsStartElement(XmlNamespace.Types, "Refiners"))
				{
					List<SearchRefinerItem> list3 = new List<SearchRefinerItem>();
					do
					{
						reader.Read();
						if (reader.IsStartElement(XmlNamespace.Types, "Refiner"))
						{
							list3.Add(SearchRefinerItem.LoadFromXml(reader));
						}
					}
					while (!reader.IsEndElement(XmlNamespace.Types, "Refiners"));
					if (list3.Count > 0)
					{
						searchMailboxesResult.Refiners = list3.ToArray();
					}
				}
				if (reader.IsStartElement(XmlNamespace.Types, "MailboxStats"))
				{
					List<MailboxStatisticsItem> list4 = new List<MailboxStatisticsItem>();
					do
					{
						reader.Read();
						if (reader.IsStartElement(XmlNamespace.Types, "MailboxStat"))
						{
							list4.Add(MailboxStatisticsItem.LoadFromXml(reader));
						}
					}
					while (!reader.IsEndElement(XmlNamespace.Types, "MailboxStats"));
					if (list4.Count > 0)
					{
						searchMailboxesResult.MailboxStats = list4.ToArray();
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Messages, "SearchMailboxesResult"));
			return searchMailboxesResult;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0003E368 File Offset: 0x0003D368
		internal static SearchMailboxesResult LoadFromJson(JsonObject jsonObject)
		{
			return new SearchMailboxesResult();
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0003E37C File Offset: 0x0003D37C
		private static KeywordStatisticsSearchResult[] LoadKeywordStatsXml(EwsServiceXmlReader reader)
		{
			List<KeywordStatisticsSearchResult> list = new List<KeywordStatisticsSearchResult>();
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "KeywordStats");
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "KeywordStat"))
				{
					list.Add(new KeywordStatisticsSearchResult
					{
						Keyword = reader.ReadElementValue(XmlNamespace.Types, "Keyword"),
						ItemHits = int.Parse(reader.ReadElementValue(XmlNamespace.Types, "ItemHits")),
						Size = ulong.Parse(reader.ReadElementValue(XmlNamespace.Types, "Size"))
					});
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Types, "KeywordStats"));
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0003E41C File Offset: 0x0003D41C
		private static SearchPreviewItem[] LoadPreviewItemsXml(EwsServiceXmlReader reader)
		{
			List<SearchPreviewItem> list = new List<SearchPreviewItem>();
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "Items");
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "SearchPreviewItem"))
				{
					SearchPreviewItem searchPreviewItem = new SearchPreviewItem();
					do
					{
						reader.Read();
						if (reader.IsStartElement(XmlNamespace.Types, "Id"))
						{
							searchPreviewItem.Id = new ItemId();
							searchPreviewItem.Id.ReadAttributesFromXml(reader);
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "ParentId"))
						{
							searchPreviewItem.ParentId = new ItemId();
							searchPreviewItem.ParentId.ReadAttributesFromXml(reader);
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "Mailbox"))
						{
							searchPreviewItem.Mailbox = new PreviewItemMailbox();
							searchPreviewItem.Mailbox.MailboxId = reader.ReadElementValue(XmlNamespace.Types, "MailboxId");
							searchPreviewItem.Mailbox.PrimarySmtpAddress = reader.ReadElementValue(XmlNamespace.Types, "PrimarySmtpAddress");
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "UniqueHash"))
						{
							searchPreviewItem.UniqueHash = reader.ReadElementValue(XmlNamespace.Types, "UniqueHash");
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "SortValue"))
						{
							searchPreviewItem.SortValue = reader.ReadElementValue(XmlNamespace.Types, "SortValue");
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "OwaLink"))
						{
							searchPreviewItem.OwaLink = reader.ReadElementValue(XmlNamespace.Types, "OwaLink");
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "Sender"))
						{
							searchPreviewItem.Sender = reader.ReadElementValue(XmlNamespace.Types, "Sender");
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "ToRecipients"))
						{
							searchPreviewItem.ToRecipients = SearchMailboxesResult.GetRecipients(reader, "ToRecipients");
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "CcRecipients"))
						{
							searchPreviewItem.CcRecipients = SearchMailboxesResult.GetRecipients(reader, "CcRecipients");
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "BccRecipients"))
						{
							searchPreviewItem.BccRecipients = SearchMailboxesResult.GetRecipients(reader, "BccRecipients");
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "CreatedTime"))
						{
							searchPreviewItem.CreatedTime = DateTime.Parse(reader.ReadElementValue(XmlNamespace.Types, "CreatedTime"));
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "ReceivedTime"))
						{
							searchPreviewItem.ReceivedTime = DateTime.Parse(reader.ReadElementValue(XmlNamespace.Types, "ReceivedTime"));
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "SentTime"))
						{
							searchPreviewItem.SentTime = DateTime.Parse(reader.ReadElementValue(XmlNamespace.Types, "SentTime"));
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "Subject"))
						{
							searchPreviewItem.Subject = reader.ReadElementValue(XmlNamespace.Types, "Subject");
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "Preview"))
						{
							searchPreviewItem.Preview = reader.ReadElementValue(XmlNamespace.Types, "Preview");
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "Size"))
						{
							searchPreviewItem.Size = ulong.Parse(reader.ReadElementValue(XmlNamespace.Types, "Size"));
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "Importance"))
						{
							searchPreviewItem.Importance = (Importance)Enum.Parse(typeof(Importance), reader.ReadElementValue(XmlNamespace.Types, "Importance"));
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "Read"))
						{
							searchPreviewItem.Read = bool.Parse(reader.ReadElementValue(XmlNamespace.Types, "Read"));
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "HasAttachment"))
						{
							searchPreviewItem.HasAttachment = bool.Parse(reader.ReadElementValue(XmlNamespace.Types, "HasAttachment"));
						}
						else if (reader.IsStartElement(XmlNamespace.Types, "ExtendedProperties"))
						{
							searchPreviewItem.ExtendedProperties = SearchMailboxesResult.LoadExtendedPropertiesXml(reader);
						}
					}
					while (!reader.IsEndElement(XmlNamespace.Types, "SearchPreviewItem"));
					list.Add(searchPreviewItem);
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Types, "Items"));
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0003E7C8 File Offset: 0x0003D7C8
		private static string[] GetRecipients(EwsServiceXmlReader reader, string elementName)
		{
			List<string> list = new List<string>();
			do
			{
				if (reader.IsStartElement(XmlNamespace.Types, "SmtpAddress"))
				{
					list.Add(reader.ReadElementValue(XmlNamespace.Types, "SmtpAddress"));
				}
				reader.Read();
			}
			while (!reader.IsEndElement(XmlNamespace.Types, elementName));
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0003E81C File Offset: 0x0003D81C
		private static ExtendedPropertyCollection LoadExtendedPropertiesXml(EwsServiceXmlReader reader)
		{
			ExtendedPropertyCollection extendedPropertyCollection = new ExtendedPropertyCollection();
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "ExtendedProperties");
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "ExtendedProperty"))
				{
					extendedPropertyCollection.LoadFromXml(reader, "ExtendedProperty");
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Types, "ExtendedProperties"));
			if (extendedPropertyCollection.Count != 0)
			{
				return extendedPropertyCollection;
			}
			return null;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0003E874 File Offset: 0x0003D874
		// (set) Token: 0x0600168B RID: 5771 RVA: 0x0003E87C File Offset: 0x0003D87C
		public MailboxQuery[] SearchQueries { get; set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0003E885 File Offset: 0x0003D885
		// (set) Token: 0x0600168D RID: 5773 RVA: 0x0003E88D File Offset: 0x0003D88D
		public SearchResultType ResultType { get; set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x0003E896 File Offset: 0x0003D896
		// (set) Token: 0x0600168F RID: 5775 RVA: 0x0003E89E File Offset: 0x0003D89E
		public long ItemCount { get; set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0003E8A7 File Offset: 0x0003D8A7
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x0003E8AF File Offset: 0x0003D8AF
		[CLSCompliant(false)]
		public ulong Size { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0003E8B8 File Offset: 0x0003D8B8
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x0003E8C0 File Offset: 0x0003D8C0
		public int PageItemCount { get; set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0003E8C9 File Offset: 0x0003D8C9
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x0003E8D1 File Offset: 0x0003D8D1
		[CLSCompliant(false)]
		public ulong PageItemSize { get; set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x0003E8DA File Offset: 0x0003D8DA
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x0003E8E2 File Offset: 0x0003D8E2
		public KeywordStatisticsSearchResult[] KeywordStats { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0003E8EB File Offset: 0x0003D8EB
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x0003E8F3 File Offset: 0x0003D8F3
		public SearchPreviewItem[] PreviewItems { get; set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0003E8FC File Offset: 0x0003D8FC
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x0003E904 File Offset: 0x0003D904
		public FailedSearchMailbox[] FailedMailboxes { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0003E90D File Offset: 0x0003D90D
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x0003E915 File Offset: 0x0003D915
		public SearchRefinerItem[] Refiners { get; set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x0003E91E File Offset: 0x0003D91E
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x0003E926 File Offset: 0x0003D926
		public MailboxStatisticsItem[] MailboxStats { get; set; }
	}
}
