using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002C8 RID: 712
	internal abstract class TypedPropertyDefinition : PropertyDefinition
	{
		// Token: 0x0600195A RID: 6490 RVA: 0x00044E05 File Offset: 0x00043E05
		internal TypedPropertyDefinition(string xmlElementName, string uri, ExchangeVersion version)
			: base(xmlElementName, uri, version)
		{
			this.isNullable = false;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00044E17 File Offset: 0x00043E17
		internal TypedPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00044E24 File Offset: 0x00043E24
		internal TypedPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version, bool isNullable)
			: this(xmlElementName, uri, flags, version)
		{
			this.isNullable = isNullable;
		}

		// Token: 0x0600195D RID: 6493
		internal abstract object Parse(string value);

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00044E39 File Offset: 0x00043E39
		internal override bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00044E41 File Offset: 0x00043E41
		internal virtual string ToString(object value)
		{
			return value.ToString();
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00044E4C File Offset: 0x00043E4C
		internal override void LoadPropertyValueFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag)
		{
			string text = reader.ReadElementValue(XmlNamespace.Types, base.XmlElementName);
			if (!string.IsNullOrEmpty(text))
			{
				propertyBag[this] = this.Parse(text);
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00044E80 File Offset: 0x00043E80
		internal override void LoadPropertyValueFromJson(object value, ExchangeService service, PropertyBag propertyBag)
		{
			string text = value as string;
			if (!string.IsNullOrEmpty(text))
			{
				propertyBag[this] = this.Parse(text);
				return;
			}
			if (value != null)
			{
				propertyBag[this] = this.Parse(value.ToString());
			}
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00044EC4 File Offset: 0x00043EC4
		internal override void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation)
		{
			object obj = propertyBag[this];
			if (obj != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, base.XmlElementName, base.Name, obj);
			}
		}

		// Token: 0x040013F5 RID: 5109
		private bool isNullable;
	}
}
