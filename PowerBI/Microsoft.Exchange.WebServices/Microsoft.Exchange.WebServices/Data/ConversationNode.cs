using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000048 RID: 72
	public sealed class ConversationNode : ComplexProperty
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0000C6CB File Offset: 0x0000B6CB
		internal ConversationNode(PropertySet propertySet)
		{
			this.propertySet = propertySet;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000C6DA File Offset: 0x0000B6DA
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000C6E2 File Offset: 0x0000B6E2
		public string InternetMessageId { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000C6EB File Offset: 0x0000B6EB
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000C6F3 File Offset: 0x0000B6F3
		public string ParentInternetMessageId { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000C6FC File Offset: 0x0000B6FC
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000C704 File Offset: 0x0000B704
		public List<Item> Items { get; set; }

		// Token: 0x06000341 RID: 833 RVA: 0x0000C710 File Offset: 0x0000B710
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "InternetMessageId")
				{
					this.InternetMessageId = reader.ReadElementValue();
					return true;
				}
				if (localName == "ParentInternetMessageId")
				{
					this.ParentInternetMessageId = reader.ReadElementValue();
					return true;
				}
				if (localName == "Items")
				{
					this.Items = reader.ReadServiceObjectsCollectionFromXml<Item>(XmlNamespace.Types, "Items", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), true, this.propertySet, false);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000C798 File Offset: 0x0000B798
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			this.InternetMessageId = jsonProperty.ReadAsString("ConversationIndex");
			if (jsonProperty.ContainsKey("ParentInternetMessageId"))
			{
				this.ParentInternetMessageId = jsonProperty.ReadAsString("ParentInternetMessageId");
			}
			if (jsonProperty.ContainsKey("Items"))
			{
				EwsServiceJsonReader ewsServiceJsonReader = new EwsServiceJsonReader(service);
				this.Items = ewsServiceJsonReader.ReadServiceObjectsCollectionFromJson<Item>(jsonProperty, "Items", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), false, this.propertySet, false);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000C80E File Offset: 0x0000B80E
		private Item GetObjectInstance(ExchangeService service, string xmlElementName)
		{
			return EwsUtilities.CreateEwsObjectFromXmlElementName<Item>(service, xmlElementName);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000C817 File Offset: 0x0000B817
		internal string GetXmlElementName()
		{
			return "ConversationNode";
		}

		// Token: 0x04000165 RID: 357
		private PropertySet propertySet;
	}
}
