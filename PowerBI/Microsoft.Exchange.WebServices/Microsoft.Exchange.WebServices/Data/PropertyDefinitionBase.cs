using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002C2 RID: 706
	[Serializable]
	public abstract class PropertyDefinitionBase : IJsonSerializable
	{
		// Token: 0x0600191F RID: 6431 RVA: 0x0004480F File Offset: 0x0004380F
		internal PropertyDefinitionBase()
		{
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00044818 File Offset: 0x00043818
		internal static bool TryLoadFromXml(EwsServiceXmlReader reader, ref PropertyDefinitionBase propertyDefinition)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "FieldURI")
				{
					propertyDefinition = ServiceObjectSchema.FindPropertyDefinition(reader.ReadAttributeValue("FieldURI"));
					reader.SkipCurrentElement();
					return true;
				}
				if (localName == "IndexedFieldURI")
				{
					propertyDefinition = new IndexedPropertyDefinition(reader.ReadAttributeValue("FieldURI"), reader.ReadAttributeValue("FieldIndex"));
					reader.SkipCurrentElement();
					return true;
				}
				if (localName == "ExtendedFieldURI")
				{
					propertyDefinition = new ExtendedPropertyDefinition();
					(propertyDefinition as ExtendedPropertyDefinition).LoadFromXml(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x000448B0 File Offset: 0x000438B0
		internal static PropertyDefinitionBase TryLoadFromJson(JsonObject jsonObject)
		{
			string text;
			if ((text = jsonObject.ReadTypeString()) != null)
			{
				if (text == "PropertyUri")
				{
					return ServiceObjectSchema.FindPropertyDefinition(jsonObject.ReadAsString("FieldURI"));
				}
				if (text == "DictionaryPropertyUri")
				{
					return new IndexedPropertyDefinition(jsonObject.ReadAsString("FieldURI"), jsonObject.ReadAsString("FieldIndex"));
				}
				if (text == "ExtendedPropertyUri")
				{
					ExtendedPropertyDefinition extendedPropertyDefinition = new ExtendedPropertyDefinition();
					extendedPropertyDefinition.LoadFromJson(jsonObject);
					return extendedPropertyDefinition;
				}
			}
			return null;
		}

		// Token: 0x06001922 RID: 6434
		internal abstract string GetXmlElementName();

		// Token: 0x06001923 RID: 6435
		protected abstract string GetJsonType();

		// Token: 0x06001924 RID: 6436
		internal abstract void WriteAttributesToXml(EwsServiceXmlWriter writer);

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001925 RID: 6437
		public abstract ExchangeVersion Version { get; }

		// Token: 0x06001926 RID: 6438
		internal abstract string GetPrintableName();

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001927 RID: 6439
		public abstract Type Type { get; }

		// Token: 0x06001928 RID: 6440 RVA: 0x0004492D File Offset: 0x0004392D
		internal virtual void WriteToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, this.GetXmlElementName());
			this.WriteAttributesToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0004494C File Offset: 0x0004394C
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(this.GetJsonType());
			this.AddJsonProperties(jsonObject, service);
			return jsonObject;
		}

		// Token: 0x0600192A RID: 6442
		internal abstract void AddJsonProperties(JsonObject jsonPropertyDefinition, ExchangeService service);

		// Token: 0x0600192B RID: 6443 RVA: 0x00044974 File Offset: 0x00043974
		public override string ToString()
		{
			return this.GetPrintableName();
		}
	}
}
