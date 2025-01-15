using System;
using System.Xml;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000014 RID: 20
	internal sealed class AtomInstanceAnnotation
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00002E4E File Offset: 0x0000104E
		private AtomInstanceAnnotation(string target, string term, ODataValue value)
		{
			this.target = target;
			this.term = term;
			this.value = value;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002E6B File Offset: 0x0000106B
		internal string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002E73 File Offset: 0x00001073
		internal string TermName
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002E7B File Offset: 0x0000107B
		internal ODataValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002E83 File Offset: 0x00001083
		internal bool IsTargetingCurrentElement
		{
			get
			{
				return string.IsNullOrEmpty(this.Target) || string.Equals(this.Target, ".", 4);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002EA5 File Offset: 0x000010A5
		internal static AtomInstanceAnnotation CreateFrom(ODataInstanceAnnotation odataInstanceAnnotation, string target)
		{
			return new AtomInstanceAnnotation(target, odataInstanceAnnotation.Name, odataInstanceAnnotation.Value);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002EBC File Offset: 0x000010BC
		internal static AtomInstanceAnnotation CreateFrom(ODataAtomInputContext inputContext, ODataAtomPropertyAndValueDeserializer propertyAndValueDeserializer)
		{
			BufferingXmlReader xmlReader = inputContext.XmlReader;
			string text = null;
			string text2 = null;
			string text3 = null;
			bool flag = false;
			bool flag2 = false;
			string text4 = null;
			string text5 = null;
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = null;
			XmlNameTable nameTable = xmlReader.NameTable;
			string text6 = nameTable.Get("http://docs.oasis-open.org/odata/ns/metadata");
			string text7 = nameTable.Get("null");
			string text8 = nameTable.Get("type");
			string text9 = nameTable.Get(string.Empty);
			string text10 = nameTable.Get("term");
			string text11 = nameTable.Get("target");
			while (xmlReader.MoveToNextAttribute())
			{
				if (xmlReader.NamespaceEquals(text6))
				{
					if (xmlReader.LocalNameEquals(text8))
					{
						text3 = xmlReader.Value;
					}
					else if (xmlReader.LocalNameEquals(text7))
					{
						flag = ODataAtomReaderUtils.ReadMetadataNullAttributeValue(xmlReader.Value);
					}
				}
				else if (xmlReader.NamespaceEquals(text9))
				{
					if (xmlReader.LocalNameEquals(text10))
					{
						text = xmlReader.Value;
						if (propertyAndValueDeserializer.MessageReaderSettings.ShouldSkipAnnotation(text))
						{
							xmlReader.MoveToElement();
							return null;
						}
					}
					else if (xmlReader.LocalNameEquals(text11))
					{
						text2 = xmlReader.Value;
					}
					else
					{
						IEdmPrimitiveTypeReference edmPrimitiveTypeReference2 = AtomInstanceAnnotation.LookupEdmTypeByAttributeValueNotationName(xmlReader.LocalName);
						if (edmPrimitiveTypeReference2 != null)
						{
							if (edmPrimitiveTypeReference != null)
							{
								flag2 = true;
							}
							edmPrimitiveTypeReference = edmPrimitiveTypeReference2;
							text4 = xmlReader.LocalName;
							text5 = xmlReader.Value;
						}
					}
				}
			}
			xmlReader.MoveToElement();
			if (text == null)
			{
				throw new ODataException(Strings.AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement);
			}
			if (flag2)
			{
				throw new ODataException(Strings.AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes);
			}
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfValueTerm(text, propertyAndValueDeserializer.Model);
			ODataValue odataValue;
			if (flag)
			{
				ReaderValidationUtils.ValidateNullValue(propertyAndValueDeserializer.Model, edmTypeReference, propertyAndValueDeserializer.MessageReaderSettings, true, text, default(bool?));
				odataValue = new ODataNullValue();
			}
			else if (edmPrimitiveTypeReference != null)
			{
				odataValue = AtomInstanceAnnotation.GetValueFromAttributeValueNotation(edmTypeReference, edmPrimitiveTypeReference, text4, text5, text3, xmlReader.IsEmptyElement, propertyAndValueDeserializer.Model, propertyAndValueDeserializer.MessageReaderSettings);
			}
			else
			{
				odataValue = AtomInstanceAnnotation.ReadValueFromElementContent(propertyAndValueDeserializer, edmTypeReference);
			}
			xmlReader.Read();
			return new AtomInstanceAnnotation(text2, text, odataValue);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000030A0 File Offset: 0x000012A0
		internal static string LookupAttributeValueNotationNameByEdmTypeKind(EdmPrimitiveTypeKind typeKind)
		{
			switch (typeKind)
			{
			case EdmPrimitiveTypeKind.Boolean:
				return "bool";
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.DateTimeOffset:
				break;
			case EdmPrimitiveTypeKind.Decimal:
				return "decimal";
			case EdmPrimitiveTypeKind.Double:
				return "float";
			default:
				if (typeKind == EdmPrimitiveTypeKind.Int32)
				{
					return "int";
				}
				if (typeKind == EdmPrimitiveTypeKind.String)
				{
					return "string";
				}
				break;
			}
			return null;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000030F8 File Offset: 0x000012F8
		internal static IEdmPrimitiveTypeReference LookupEdmTypeByAttributeValueNotationName(string attributeName)
		{
			if (attributeName != null)
			{
				if (attributeName == "int")
				{
					return EdmCoreModel.Instance.GetInt32(false);
				}
				if (attributeName == "string")
				{
					return EdmCoreModel.Instance.GetString(false);
				}
				if (attributeName == "float")
				{
					return EdmCoreModel.Instance.GetDouble(false);
				}
				if (attributeName == "bool")
				{
					return EdmCoreModel.Instance.GetBoolean(false);
				}
				if (attributeName == "decimal")
				{
					return EdmCoreModel.Instance.GetDecimal(false);
				}
			}
			return null;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000318C File Offset: 0x0000138C
		private static ODataValue ReadValueFromElementContent(ODataAtomPropertyAndValueDeserializer propertyAndValueDeserializer, IEdmTypeReference expectedType)
		{
			object obj = propertyAndValueDeserializer.ReadNonEntityValue(expectedType, null, null, true);
			return obj.ToODataValue();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000031AC File Offset: 0x000013AC
		private static ODataPrimitiveValue GetValueFromAttributeValueNotation(IEdmTypeReference expectedTypeReference, IEdmPrimitiveTypeReference attributeValueNotationTypeReference, string attributeValueNotationAttributeName, string attributeValueNotationAttributeValue, string typeAttributeValue, bool positionedOnEmptyElement, IEdmModel model, ODataMessageReaderSettings messageReaderSettings)
		{
			if (!positionedOnEmptyElement)
			{
				throw new ODataException(Strings.AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement(attributeValueNotationAttributeName));
			}
			if (typeAttributeValue != null && !string.Equals(attributeValueNotationTypeReference.Definition.FullTypeName(), typeAttributeValue, 4))
			{
				throw new ODataException(Strings.AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType(typeAttributeValue, attributeValueNotationAttributeName));
			}
			IEdmTypeReference edmTypeReference = ReaderValidationUtils.ResolveAndValidatePrimitiveTargetType(expectedTypeReference, EdmTypeKind.Primitive, attributeValueNotationTypeReference.Definition, attributeValueNotationTypeReference.FullName(), attributeValueNotationTypeReference.Definition, model, messageReaderSettings);
			return new ODataPrimitiveValue(AtomValueUtils.ConvertStringToPrimitive(attributeValueNotationAttributeValue, edmTypeReference.AsPrimitive()));
		}

		// Token: 0x040000B6 RID: 182
		private readonly string target;

		// Token: 0x040000B7 RID: 183
		private readonly string term;

		// Token: 0x040000B8 RID: 184
		private readonly ODataValue value;
	}
}
