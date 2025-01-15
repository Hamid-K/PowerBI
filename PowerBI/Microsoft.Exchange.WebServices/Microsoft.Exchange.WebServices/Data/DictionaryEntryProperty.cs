using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000051 RID: 81
	[EditorBrowsable(1)]
	public abstract class DictionaryEntryProperty<TKey> : ComplexProperty
	{
		// Token: 0x06000394 RID: 916 RVA: 0x0000D351 File Offset: 0x0000C351
		internal DictionaryEntryProperty()
		{
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D359 File Offset: 0x0000C359
		internal DictionaryEntryProperty(TKey key)
		{
			this.key = key;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000D368 File Offset: 0x0000C368
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000D370 File Offset: 0x0000C370
		internal TKey Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000D379 File Offset: 0x0000C379
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.key = reader.ReadAttributeValue<TKey>("Key");
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D38C File Offset: 0x0000C38C
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Key", this.Key);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000D3A4 File Offset: 0x0000C3A4
		internal virtual bool WriteSetUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject, string ownerDictionaryXmlElementName)
		{
			return false;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000D3A7 File Offset: 0x0000C3A7
		internal virtual bool WriteSetUpdateToJson(ExchangeService service, ServiceObject ewsObject, PropertyDefinition propertyDefinition, List<JsonObject> updates)
		{
			return false;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D3AA File Offset: 0x0000C3AA
		internal virtual bool WriteDeleteUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject)
		{
			return false;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000D3AD File Offset: 0x0000C3AD
		internal virtual bool WriteDeleteUpdateToJson(ExchangeService service, ServiceObject ewsObject, List<JsonObject> updates)
		{
			return false;
		}

		// Token: 0x0400017A RID: 378
		private TKey key;
	}
}
