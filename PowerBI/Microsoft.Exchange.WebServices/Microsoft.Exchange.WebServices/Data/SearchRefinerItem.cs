using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000282 RID: 642
	public sealed class SearchRefinerItem
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0003E937 File Offset: 0x0003D937
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x0003E93F File Offset: 0x0003D93F
		public string Name { get; set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0003E948 File Offset: 0x0003D948
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x0003E950 File Offset: 0x0003D950
		public string Value { get; set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0003E959 File Offset: 0x0003D959
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x0003E961 File Offset: 0x0003D961
		public long Count { get; set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0003E96A File Offset: 0x0003D96A
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x0003E972 File Offset: 0x0003D972
		public string Token { get; set; }

		// Token: 0x060016A9 RID: 5801 RVA: 0x0003E97C File Offset: 0x0003D97C
		internal static SearchRefinerItem LoadFromXml(EwsServiceXmlReader reader)
		{
			SearchRefinerItem searchRefinerItem = new SearchRefinerItem();
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "Refiner");
			searchRefinerItem.Name = reader.ReadElementValue(XmlNamespace.Types, "Name");
			searchRefinerItem.Value = reader.ReadElementValue(XmlNamespace.Types, "Value");
			searchRefinerItem.Count = reader.ReadElementValue<long>(XmlNamespace.Types, "Count");
			searchRefinerItem.Token = reader.ReadElementValue(XmlNamespace.Types, "Token");
			return searchRefinerItem;
		}
	}
}
