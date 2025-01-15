using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200027D RID: 637
	public sealed class NonIndexableItemStatistic
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0003DC5F File Offset: 0x0003CC5F
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x0003DC67 File Offset: 0x0003CC67
		public string Mailbox { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0003DC70 File Offset: 0x0003CC70
		// (set) Token: 0x06001652 RID: 5714 RVA: 0x0003DC78 File Offset: 0x0003CC78
		public long ItemCount { get; set; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0003DC81 File Offset: 0x0003CC81
		// (set) Token: 0x06001654 RID: 5716 RVA: 0x0003DC89 File Offset: 0x0003CC89
		public string ErrorMessage { get; set; }

		// Token: 0x06001655 RID: 5717 RVA: 0x0003DC94 File Offset: 0x0003CC94
		internal static List<NonIndexableItemStatistic> LoadFromXml(EwsServiceXmlReader reader)
		{
			List<NonIndexableItemStatistic> list = new List<NonIndexableItemStatistic>();
			reader.Read();
			if (reader.IsStartElement(XmlNamespace.Messages, "NonIndexableItemStatistics"))
			{
				do
				{
					reader.Read();
					if (reader.IsStartElement(XmlNamespace.Types, "NonIndexableItemStatistic"))
					{
						string text = reader.ReadElementValue(XmlNamespace.Types, "Mailbox");
						int num = reader.ReadElementValue<int>(XmlNamespace.Types, "ItemCount");
						string text2 = null;
						if (reader.IsStartElement(XmlNamespace.Types, "ErrorMessage"))
						{
							text2 = reader.ReadElementValue(XmlNamespace.Types, "ErrorMessage");
						}
						list.Add(new NonIndexableItemStatistic
						{
							Mailbox = text,
							ItemCount = (long)num,
							ErrorMessage = text2
						});
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "NonIndexableItemStatistics"));
			}
			return list;
		}
	}
}
