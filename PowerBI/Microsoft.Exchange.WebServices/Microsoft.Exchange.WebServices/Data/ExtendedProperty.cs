using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200005C RID: 92
	public sealed class ExtendedProperty : ComplexProperty
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0000E97B File Offset: 0x0000D97B
		internal ExtendedProperty()
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000E983 File Offset: 0x0000D983
		internal ExtendedProperty(ExtendedPropertyDefinition propertyDefinition)
			: this()
		{
			EwsUtilities.ValidateParam(propertyDefinition, "propertyDefinition");
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000E9A0 File Offset: 0x0000D9A0
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "ExtendedFieldURI")
				{
					this.propertyDefinition = new ExtendedPropertyDefinition();
					this.propertyDefinition.LoadFromXml(reader);
					return true;
				}
				if (localName == "Value")
				{
					EwsUtilities.Assert(this.PropertyDefinition != null, "ExtendedProperty.TryReadElementFromXml", "PropertyDefintion is missing");
					string text = reader.ReadElementValue();
					this.value = MapiTypeConverter.ConvertToValue(this.PropertyDefinition.MapiType, text);
					return true;
				}
				if (localName == "Values")
				{
					EwsUtilities.Assert(this.PropertyDefinition != null, "ExtendedProperty.TryReadElementFromXml", "PropertyDefintion is missing");
					StringList stringList = new StringList("Value");
					stringList.LoadFromXml(reader, reader.LocalName);
					this.value = MapiTypeConverter.ConvertToValue(this.PropertyDefinition.MapiType, stringList);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000EA88 File Offset: 0x0000DA88
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "ExtendedFieldURI"))
					{
						if (!(text2 == "Value"))
						{
							if (text2 == "Values")
							{
								EwsUtilities.Assert(this.PropertyDefinition != null, "ExtendedProperty.TryReadElementFromXml", "PropertyDefintion is missing");
								StringList stringList = new StringList("Value");
								((IJsonCollectionDeserializer)stringList).CreateFromJsonCollection(jsonProperty.ReadAsArray(text), service);
								this.value = MapiTypeConverter.ConvertToValue(this.PropertyDefinition.MapiType, stringList);
							}
						}
						else
						{
							EwsUtilities.Assert(this.PropertyDefinition != null, "ExtendedProperty.TryReadElementFromXml", "PropertyDefintion is missing");
							string text3 = jsonProperty.ReadAsString(text);
							this.value = MapiTypeConverter.ConvertToValue(this.PropertyDefinition.MapiType, text3);
						}
					}
					else
					{
						this.propertyDefinition = new ExtendedPropertyDefinition();
						this.propertyDefinition.LoadFromJson(jsonProperty.ReadAsJsonObject(text));
					}
				}
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000EBBC File Offset: 0x0000DBBC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.PropertyDefinition.WriteToXml(writer);
			if (MapiTypeConverter.IsArrayType(this.PropertyDefinition.MapiType))
			{
				Array array = this.Value as Array;
				writer.WriteStartElement(XmlNamespace.Types, "Values");
				for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
				{
					writer.WriteElementValue(XmlNamespace.Types, "Value", MapiTypeConverter.ConvertToString(this.PropertyDefinition.MapiType, array.GetValue(i)));
				}
				writer.WriteEndElement();
				return;
			}
			writer.WriteElementValue(XmlNamespace.Types, "Value", MapiTypeConverter.ConvertToString(this.PropertyDefinition.MapiType, this.Value));
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000EC64 File Offset: 0x0000DC64
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			JsonObject jsonObject2 = new JsonObject();
			this.PropertyDefinition.AddJsonProperties(jsonObject2, service);
			jsonObject.Add("ExtendedFieldURI", jsonObject2);
			if (MapiTypeConverter.IsArrayType(this.PropertyDefinition.MapiType))
			{
				List<object> list = new List<object>();
				foreach (object obj in (this.Value as Array))
				{
					list.Add(MapiTypeConverter.ConvertToString(this.PropertyDefinition.MapiType, obj));
				}
				jsonObject.Add("Values", list.ToArray());
			}
			else
			{
				jsonObject.Add("Value", MapiTypeConverter.ConvertToString(this.PropertyDefinition.MapiType, this.Value));
			}
			return jsonObject;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000ED48 File Offset: 0x0000DD48
		public ExtendedPropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000ED50 File Offset: 0x0000DD50
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000ED58 File Offset: 0x0000DD58
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				EwsUtilities.ValidateParam(value, "value");
				this.SetFieldValue<object>(ref this.value, MapiTypeConverter.ChangeType(this.PropertyDefinition.MapiType, value));
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000ED84 File Offset: 0x0000DD84
		private string GetStringValue()
		{
			if (!MapiTypeConverter.IsArrayType(this.PropertyDefinition.MapiType))
			{
				return MapiTypeConverter.ConvertToString(this.PropertyDefinition.MapiType, this.Value);
			}
			Array array = this.Value as Array;
			if (array == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
			{
				stringBuilder.Append(MapiTypeConverter.ConvertToString(this.PropertyDefinition.MapiType, array.GetValue(i)));
				stringBuilder.Append(",");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000EE38 File Offset: 0x0000DE38
		public override bool Equals(object obj)
		{
			ExtendedProperty extendedProperty = obj as ExtendedProperty;
			return extendedProperty != null && extendedProperty.PropertyDefinition.Equals(this.PropertyDefinition) && this.GetStringValue().Equals(extendedProperty.GetStringValue());
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000EE75 File Offset: 0x0000DE75
		public override int GetHashCode()
		{
			return (((this.PropertyDefinition != null) ? this.PropertyDefinition.GetPrintableName() : string.Empty) + this.GetStringValue()).GetHashCode();
		}

		// Token: 0x0400018E RID: 398
		private ExtendedPropertyDefinition propertyDefinition;

		// Token: 0x0400018F RID: 399
		private object value;
	}
}
