using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200005D RID: 93
	[EditorBrowsable(1)]
	public sealed class ExtendedPropertyCollection : ComplexPropertyCollection<ExtendedProperty>, ICustomUpdateSerializer
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x0000EEA7 File Offset: 0x0000DEA7
		internal override ExtendedProperty CreateComplexProperty(string xmlElementName)
		{
			return new ExtendedProperty();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000EEAE File Offset: 0x0000DEAE
		internal override ExtendedProperty CreateDefaultComplexProperty()
		{
			return new ExtendedProperty();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000EEB5 File Offset: 0x0000DEB5
		internal override string GetCollectionItemXmlElementName(ExtendedProperty complexProperty)
		{
			return null;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000EEB8 File Offset: 0x0000DEB8
		internal override void LoadFromXml(EwsServiceXmlReader reader, string localElementName)
		{
			ExtendedProperty extendedProperty = new ExtendedProperty();
			extendedProperty.LoadFromXml(reader, reader.LocalName);
			base.InternalAdd(extendedProperty);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000EEE0 File Offset: 0x0000DEE0
		internal override void WriteToXml(EwsServiceXmlWriter writer, string xmlElementName)
		{
			foreach (ExtendedProperty extendedProperty in this)
			{
				extendedProperty.WriteToXml(writer, "ExtendedProperty");
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000EF30 File Offset: 0x0000DF30
		internal override object InternalToJson(ExchangeService service)
		{
			List<object> list = new List<object>();
			foreach (ExtendedProperty extendedProperty in this)
			{
				list.Add(extendedProperty.InternalToJson(service));
			}
			return list.ToArray();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000EF8C File Offset: 0x0000DF8C
		private ExtendedProperty GetOrAddExtendedProperty(ExtendedPropertyDefinition propertyDefinition)
		{
			ExtendedProperty extendedProperty;
			if (!this.TryGetProperty(propertyDefinition, out extendedProperty))
			{
				extendedProperty = new ExtendedProperty(propertyDefinition);
				base.InternalAdd(extendedProperty);
			}
			return extendedProperty;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000EFB4 File Offset: 0x0000DFB4
		internal void SetExtendedProperty(ExtendedPropertyDefinition propertyDefinition, object value)
		{
			ExtendedProperty orAddExtendedProperty = this.GetOrAddExtendedProperty(propertyDefinition);
			orAddExtendedProperty.Value = value;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000EFD0 File Offset: 0x0000DFD0
		internal bool RemoveExtendedProperty(ExtendedPropertyDefinition propertyDefinition)
		{
			EwsUtilities.ValidateParam(propertyDefinition, "propertyDefinition");
			ExtendedProperty extendedProperty;
			return this.TryGetProperty(propertyDefinition, out extendedProperty) && base.InternalRemove(extendedProperty);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000F018 File Offset: 0x0000E018
		private bool TryGetProperty(ExtendedPropertyDefinition propertyDefinition, out ExtendedProperty extendedProperty)
		{
			extendedProperty = base.Items.Find((ExtendedProperty prop) => prop.PropertyDefinition.Equals(propertyDefinition));
			return extendedProperty != null;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000F054 File Offset: 0x0000E054
		internal bool TryGetValue<T>(ExtendedPropertyDefinition propertyDefinition, out T propertyValue)
		{
			ExtendedProperty extendedProperty;
			if (!this.TryGetProperty(propertyDefinition, out extendedProperty))
			{
				propertyValue = default(T);
				return false;
			}
			if (!typeof(T).IsAssignableFrom(propertyDefinition.Type))
			{
				string text = string.Format(Strings.PropertyDefinitionTypeMismatch, EwsUtilities.GetPrintableTypeName(propertyDefinition.Type), EwsUtilities.GetPrintableTypeName(typeof(T)));
				throw new ArgumentException(text, "propertyDefinition");
			}
			propertyValue = (T)((object)extendedProperty.Value);
			return true;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000F0D4 File Offset: 0x0000E0D4
		bool ICustomUpdateSerializer.WriteSetUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject, PropertyDefinition propertyDefinition)
		{
			List<ExtendedProperty> list = new List<ExtendedProperty>();
			list.AddRange(base.AddedItems);
			list.AddRange(base.ModifiedItems);
			foreach (ExtendedProperty extendedProperty in list)
			{
				writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetSetFieldXmlElementName());
				extendedProperty.PropertyDefinition.WriteToXml(writer);
				writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetXmlElementName());
				extendedProperty.WriteToXml(writer, "ExtendedProperty");
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			foreach (ExtendedProperty extendedProperty2 in base.RemovedItems)
			{
				writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetDeleteFieldXmlElementName());
				extendedProperty2.PropertyDefinition.WriteToXml(writer);
				writer.WriteEndElement();
			}
			return true;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000F1D4 File Offset: 0x0000E1D4
		bool ICustomUpdateSerializer.WriteSetUpdateToJson(ExchangeService service, ServiceObject ewsObject, PropertyDefinition propertyDefinition, List<JsonObject> updates)
		{
			List<ExtendedProperty> list = new List<ExtendedProperty>();
			list.AddRange(base.AddedItems);
			list.AddRange(base.ModifiedItems);
			foreach (ExtendedProperty extendedProperty in list)
			{
				updates.Add(PropertyBag.CreateJsonSetUpdate(extendedProperty, service, ewsObject));
			}
			foreach (ExtendedProperty extendedProperty2 in base.RemovedItems)
			{
				updates.Add(PropertyBag.CreateJsonDeleteUpdate(extendedProperty2.PropertyDefinition, service, ewsObject));
			}
			return true;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000F29C File Offset: 0x0000E29C
		bool ICustomUpdateSerializer.WriteDeleteUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject)
		{
			foreach (ExtendedProperty extendedProperty in base.Items)
			{
				writer.WriteStartElement(XmlNamespace.Types, ewsObject.GetDeleteFieldXmlElementName());
				extendedProperty.PropertyDefinition.WriteToXml(writer);
				writer.WriteEndElement();
			}
			return true;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000F308 File Offset: 0x0000E308
		bool ICustomUpdateSerializer.WriteDeleteUpdateToJson(ExchangeService service, ServiceObject ewsObject, List<JsonObject> updates)
		{
			foreach (ExtendedProperty extendedProperty in base.Items)
			{
				updates.Add(PropertyBag.CreateJsonDeleteUpdate(extendedProperty.PropertyDefinition, service, ewsObject));
			}
			return true;
		}
	}
}
