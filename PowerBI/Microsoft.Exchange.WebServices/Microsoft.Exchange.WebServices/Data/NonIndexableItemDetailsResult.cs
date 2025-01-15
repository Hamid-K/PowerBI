using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000279 RID: 633
	public sealed class NonIndexableItemDetailsResult
	{
		// Token: 0x0600163B RID: 5691 RVA: 0x0003DB18 File Offset: 0x0003CB18
		internal static NonIndexableItemDetailsResult LoadFromXml(EwsServiceXmlReader reader)
		{
			NonIndexableItemDetailsResult nonIndexableItemDetailsResult = new NonIndexableItemDetailsResult();
			reader.ReadStartElement(XmlNamespace.Messages, "NonIndexableItemDetailsResult");
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "Items"))
				{
					List<NonIndexableItem> list = new List<NonIndexableItem>();
					if (!reader.IsEmptyElement)
					{
						do
						{
							reader.Read();
							NonIndexableItem nonIndexableItem = NonIndexableItem.LoadFromXml(reader);
							if (nonIndexableItem != null)
							{
								list.Add(nonIndexableItem);
							}
						}
						while (!reader.IsEndElement(XmlNamespace.Types, "Items"));
						nonIndexableItemDetailsResult.Items = list.ToArray();
					}
				}
				if (reader.IsStartElement(XmlNamespace.Types, "FailedMailboxes"))
				{
					nonIndexableItemDetailsResult.FailedMailboxes = FailedSearchMailbox.LoadFailedMailboxesXml(XmlNamespace.Types, reader);
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Messages, "NonIndexableItemDetailsResult"));
			return nonIndexableItemDetailsResult;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0003DBB4 File Offset: 0x0003CBB4
		internal static NonIndexableItemDetailsResult LoadFromJson(JsonObject jsonObject)
		{
			return new NonIndexableItemDetailsResult();
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x0003DBC8 File Offset: 0x0003CBC8
		// (set) Token: 0x0600163E RID: 5694 RVA: 0x0003DBD0 File Offset: 0x0003CBD0
		public NonIndexableItem[] Items { get; set; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0003DBD9 File Offset: 0x0003CBD9
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x0003DBE1 File Offset: 0x0003CBE1
		public FailedSearchMailbox[] FailedMailboxes { get; set; }
	}
}
