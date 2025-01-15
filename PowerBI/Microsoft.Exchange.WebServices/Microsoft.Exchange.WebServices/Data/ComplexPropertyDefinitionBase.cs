using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002C5 RID: 709
	internal abstract class ComplexPropertyDefinitionBase : PropertyDefinition
	{
		// Token: 0x06001945 RID: 6469 RVA: 0x00044ABD File Offset: 0x00043ABD
		internal ComplexPropertyDefinitionBase(string xmlElementName, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, flags, version)
		{
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00044AC8 File Offset: 0x00043AC8
		internal ComplexPropertyDefinitionBase(string xmlElementName, string uri, ExchangeVersion version)
			: base(xmlElementName, uri, version)
		{
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00044AD3 File Offset: 0x00043AD3
		internal ComplexPropertyDefinitionBase(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x06001948 RID: 6472
		internal abstract ComplexProperty CreatePropertyInstance(ServiceObject owner);

		// Token: 0x06001949 RID: 6473 RVA: 0x00044AE0 File Offset: 0x00043AE0
		internal virtual void InternalLoadFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag)
		{
			object obj;
			if (!this.GetPropertyInstance(propertyBag, out obj) && this.HasFlag(PropertyDefinitionFlags.UpdateCollectionItems, new ExchangeVersion?(propertyBag.Owner.Service.RequestedServerVersion)))
			{
				(obj as ComplexProperty).UpdateFromXml(reader, reader.LocalName);
			}
			else
			{
				(obj as ComplexProperty).LoadFromXml(reader, reader.LocalName);
			}
			propertyBag[this] = obj;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00044B4C File Offset: 0x00043B4C
		internal virtual void InternalLoadFromJson(JsonObject jsonObject, ExchangeService service, PropertyBag propertyBag)
		{
			object obj;
			this.GetPropertyInstance(propertyBag, out obj);
			(obj as ComplexProperty).LoadFromJson(jsonObject, service);
			propertyBag[this] = obj;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00044B78 File Offset: 0x00043B78
		private void InternalLoadCollectionFromJson(object[] jsonCollection, ExchangeService service, PropertyBag propertyBag)
		{
			object obj;
			bool propertyInstance = this.GetPropertyInstance(propertyBag, out obj);
			IJsonCollectionDeserializer jsonCollectionDeserializer = obj as IJsonCollectionDeserializer;
			if (jsonCollectionDeserializer == null)
			{
				throw new ServiceJsonDeserializationException();
			}
			if (!propertyInstance && this.HasFlag(PropertyDefinitionFlags.UpdateCollectionItems, new ExchangeVersion?(propertyBag.Owner.Service.RequestedServerVersion)))
			{
				jsonCollectionDeserializer.UpdateFromJsonCollection(jsonCollection, service);
			}
			else
			{
				jsonCollectionDeserializer.CreateFromJsonCollection(jsonCollection, service);
			}
			propertyBag[this] = jsonCollectionDeserializer;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00044BDE File Offset: 0x00043BDE
		private bool GetPropertyInstance(PropertyBag propertyBag, out object complexProperty)
		{
			complexProperty = null;
			if (!propertyBag.TryGetValue(this, out complexProperty) || !this.HasFlag(PropertyDefinitionFlags.ReuseInstance, new ExchangeVersion?(propertyBag.Owner.Service.RequestedServerVersion)))
			{
				complexProperty = this.CreatePropertyInstance(propertyBag.Owner);
				return true;
			}
			return false;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00044C1C File Offset: 0x00043C1C
		internal sealed override void LoadPropertyValueFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag)
		{
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, base.XmlElementName);
			if (!reader.IsEmptyElement || reader.HasAttributes)
			{
				this.InternalLoadFromXml(reader, propertyBag);
			}
			reader.ReadEndElementIfNecessary(XmlNamespace.Types, base.XmlElementName);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00044C50 File Offset: 0x00043C50
		internal override void LoadPropertyValueFromJson(object value, ExchangeService service, PropertyBag propertyBag)
		{
			JsonObject jsonObject = value as JsonObject;
			if (jsonObject != null)
			{
				this.InternalLoadFromJson(jsonObject, service, propertyBag);
				return;
			}
			if (value.GetType().IsArray)
			{
				this.InternalLoadCollectionFromJson(value as object[], service, propertyBag);
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00044C8C File Offset: 0x00043C8C
		internal override void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation)
		{
			ComplexProperty complexProperty = (ComplexProperty)propertyBag[this];
			if (complexProperty != null)
			{
				complexProperty.WriteToXml(writer, base.XmlElementName);
			}
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00044CB8 File Offset: 0x00043CB8
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
			ComplexProperty complexProperty = (ComplexProperty)propertyBag[this];
			if (complexProperty != null)
			{
				jsonObject.Add(base.XmlElementName, complexProperty.InternalToJson(service));
			}
		}
	}
}
