using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200026D RID: 621
	public sealed class DiscoverySearchConfiguration
	{
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x0003D062 File Offset: 0x0003C062
		// (set) Token: 0x060015DB RID: 5595 RVA: 0x0003D06A File Offset: 0x0003C06A
		public string SearchId { get; set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0003D073 File Offset: 0x0003C073
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x0003D07B File Offset: 0x0003C07B
		public string SearchQuery { get; set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0003D084 File Offset: 0x0003C084
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x0003D08C File Offset: 0x0003C08C
		public SearchableMailbox[] SearchableMailboxes { get; set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x0003D095 File Offset: 0x0003C095
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x0003D09D File Offset: 0x0003C09D
		public string InPlaceHoldIdentity { get; set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0003D0A6 File Offset: 0x0003C0A6
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x0003D0AE File Offset: 0x0003C0AE
		public string ManagedByOrganization { get; set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0003D0B7 File Offset: 0x0003C0B7
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0003D0BF File Offset: 0x0003C0BF
		public string Language { get; set; }

		// Token: 0x060015E6 RID: 5606 RVA: 0x0003D0C8 File Offset: 0x0003C0C8
		internal static DiscoverySearchConfiguration LoadFromXml(EwsServiceXmlReader reader)
		{
			List<SearchableMailbox> list = new List<SearchableMailbox>();
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "DiscoverySearchConfiguration");
			DiscoverySearchConfiguration discoverySearchConfiguration = new DiscoverySearchConfiguration();
			discoverySearchConfiguration.SearchId = reader.ReadElementValue(XmlNamespace.Types, "SearchId");
			discoverySearchConfiguration.SearchQuery = string.Empty;
			discoverySearchConfiguration.InPlaceHoldIdentity = string.Empty;
			discoverySearchConfiguration.ManagedByOrganization = string.Empty;
			discoverySearchConfiguration.Language = string.Empty;
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "SearchQuery"))
				{
					discoverySearchConfiguration.SearchQuery = reader.ReadElementValue(XmlNamespace.Types, "SearchQuery");
					reader.ReadEndElementIfNecessary(XmlNamespace.Types, "SearchQuery");
				}
				else if (reader.IsStartElement(XmlNamespace.Types, "SearchableMailboxes"))
				{
					if (!reader.IsEmptyElement)
					{
						while (!reader.IsEndElement(XmlNamespace.Types, "SearchableMailboxes"))
						{
							reader.Read();
							if (reader.IsStartElement(XmlNamespace.Types, "SearchableMailbox"))
							{
								list.Add(SearchableMailbox.LoadFromXml(reader));
								reader.ReadEndElementIfNecessary(XmlNamespace.Types, "SearchableMailbox");
							}
						}
					}
				}
				else if (reader.IsStartElement(XmlNamespace.Types, "InPlaceHoldIdentity"))
				{
					discoverySearchConfiguration.InPlaceHoldIdentity = reader.ReadElementValue(XmlNamespace.Types, "InPlaceHoldIdentity");
					reader.ReadEndElementIfNecessary(XmlNamespace.Types, "InPlaceHoldIdentity");
				}
				else if (reader.IsStartElement(XmlNamespace.Types, "ManagedByOrganization"))
				{
					discoverySearchConfiguration.ManagedByOrganization = reader.ReadElementValue(XmlNamespace.Types, "ManagedByOrganization");
					reader.ReadEndElementIfNecessary(XmlNamespace.Types, "ManagedByOrganization");
				}
				else
				{
					if (!reader.IsStartElement(XmlNamespace.Types, "Language"))
					{
						break;
					}
					discoverySearchConfiguration.Language = reader.ReadElementValue(XmlNamespace.Types, "Language");
					reader.ReadEndElementIfNecessary(XmlNamespace.Types, "Language");
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Types, "DiscoverySearchConfiguration"));
			discoverySearchConfiguration.SearchableMailboxes = ((list.Count == 0) ? null : list.ToArray());
			return discoverySearchConfiguration;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0003D270 File Offset: 0x0003C270
		internal static DiscoverySearchConfiguration LoadFromJson(JsonObject jsonObject)
		{
			List<SearchableMailbox> list = new List<SearchableMailbox>();
			DiscoverySearchConfiguration discoverySearchConfiguration = new DiscoverySearchConfiguration();
			if (jsonObject.ContainsKey("SearchId"))
			{
				discoverySearchConfiguration.SearchId = jsonObject.ReadAsString("SearchId");
			}
			if (jsonObject.ContainsKey("InPlaceHoldIdentity"))
			{
				discoverySearchConfiguration.InPlaceHoldIdentity = jsonObject.ReadAsString("InPlaceHoldIdentity");
			}
			if (jsonObject.ContainsKey("ManagedByOrganization"))
			{
				discoverySearchConfiguration.ManagedByOrganization = jsonObject.ReadAsString("ManagedByOrganization");
			}
			if (jsonObject.ContainsKey("SearchQuery"))
			{
				discoverySearchConfiguration.SearchQuery = jsonObject.ReadAsString("SearchQuery");
			}
			if (jsonObject.ContainsKey("SearchableMailboxes"))
			{
				foreach (object obj in jsonObject.ReadAsArray("SearchableMailboxes"))
				{
					JsonObject jsonObject2 = obj as JsonObject;
					list.Add(SearchableMailbox.LoadFromJson(jsonObject2));
				}
			}
			if (jsonObject.ContainsKey("Language"))
			{
				discoverySearchConfiguration.Language = jsonObject.ReadAsString("Language");
			}
			discoverySearchConfiguration.SearchableMailboxes = ((list.Count == 0) ? null : list.ToArray());
			return discoverySearchConfiguration;
		}
	}
}
