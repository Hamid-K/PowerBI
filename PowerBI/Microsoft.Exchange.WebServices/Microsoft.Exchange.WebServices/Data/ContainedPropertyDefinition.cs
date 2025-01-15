using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002CD RID: 717
	internal class ContainedPropertyDefinition<TComplexProperty> : ComplexPropertyDefinition<TComplexProperty> where TComplexProperty : ComplexProperty, new()
	{
		// Token: 0x06001977 RID: 6519 RVA: 0x00044FE1 File Offset: 0x00043FE1
		internal ContainedPropertyDefinition(string xmlElementName, string uri, string containedXmlElementName, PropertyDefinitionFlags flags, ExchangeVersion version, CreateComplexPropertyDelegate<TComplexProperty> propertyCreationDelegate)
			: base(xmlElementName, uri, flags, version, propertyCreationDelegate)
		{
			this.containedXmlElementName = containedXmlElementName;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00044FF8 File Offset: 0x00043FF8
		internal override void InternalLoadFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag)
		{
			reader.ReadStartElement(XmlNamespace.Types, this.containedXmlElementName);
			base.InternalLoadFromXml(reader, propertyBag);
			reader.ReadEndElementIfNecessary(XmlNamespace.Types, this.containedXmlElementName);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0004501C File Offset: 0x0004401C
		internal override void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation)
		{
			ComplexProperty complexProperty = (ComplexProperty)propertyBag[this];
			if (complexProperty != null)
			{
				writer.WriteStartElement(XmlNamespace.Types, base.XmlElementName);
				complexProperty.WriteToXml(writer, this.containedXmlElementName);
				writer.WriteEndElement();
			}
		}

		// Token: 0x040013F6 RID: 5110
		private string containedXmlElementName;
	}
}
