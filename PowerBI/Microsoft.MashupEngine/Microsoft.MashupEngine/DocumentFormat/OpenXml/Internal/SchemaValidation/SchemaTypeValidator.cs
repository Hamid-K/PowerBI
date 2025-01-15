using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200311A RID: 12570
	internal class SchemaTypeValidator
	{
		// Token: 0x0601B428 RID: 111656 RVA: 0x00373EDD File Offset: 0x003720DD
		internal SchemaTypeValidator(SdbSchemaDatas sdbSchemaDatas)
		{
			this._sdbSchemaDatas = sdbSchemaDatas;
		}

		// Token: 0x0601B429 RID: 111657 RVA: 0x00373EEC File Offset: 0x003720EC
		internal void Validate(ValidationContext validationContext)
		{
			OpenXmlElement element = validationContext.Element;
			if (element.ElementTypeId < 10000)
			{
				if (element.ElementTypeId == 9003)
				{
					AlternateContentValidator.Validate(validationContext);
				}
				return;
			}
			CompatibilityRuleAttributesValidator.ValidateMcAttributes(validationContext);
			SchemaTypeData schemaTypeData = this._sdbSchemaDatas.GetSchemaTypeData(element);
			SchemaTypeValidator.ValidateAttributes(validationContext, schemaTypeData);
			if (element is OpenXmlLeafTextElement)
			{
				SchemaTypeValidator.SimpleContentComplexTypeValidator.Validate(validationContext, schemaTypeData.SimpleTypeConstraint);
				return;
			}
			if (element is OpenXmlLeafElement)
			{
				SchemaTypeValidator.EmptyComplexTypeValidator.Validate(validationContext);
				return;
			}
			if (schemaTypeData.ParticleConstraint != null)
			{
				SchemaTypeValidator.CompositeComplexTypeValidator.Validate(validationContext, schemaTypeData.ParticleConstraint);
				return;
			}
			SchemaTypeValidator.EmptyRootComplexTypeValidator.Validate(validationContext);
		}

		// Token: 0x0601B42A RID: 111658 RVA: 0x00373F7C File Offset: 0x0037217C
		private static void ValidateAttributes(ValidationContext validationContext, SchemaTypeData schemaTypeData)
		{
			OpenXmlElement element = validationContext.Element;
			for (int i = 0; i < schemaTypeData.AttributeConstraintsCount; i++)
			{
				AttributeConstraint attributeConstraint = schemaTypeData.AttributeConstraints[i];
				if (attributeConstraint.SupportedVersion.Includes(validationContext.FileFormat))
				{
					switch (attributeConstraint.XsdAttributeUse)
					{
					case XsdAttributeUse.Required:
						if (element.Attributes[i] == null)
						{
							string text = element.GetFixedAttributeQname(i).ToString();
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, "Sch_MissRequiredAttribute", new string[] { text });
							validationContext.EmitError(validationErrorInfo);
						}
						break;
					}
					if (element.Attributes[i] != null)
					{
						OpenXmlSimpleType openXmlSimpleType = element.Attributes[i];
						string text2 = element.GetFixedAttributeQname(i).ToString();
						SchemaTypeValidator.ValidateValue(validationContext, attributeConstraint.SimpleTypeConstraint, openXmlSimpleType, text2, true);
					}
				}
				else if (element.Attributes[i] != null && !validationContext.McContext.IsIgnorableNs(element.AttributeNamespaceIds[i]))
				{
					string text3 = element.GetFixedAttributeQname(i).ToString();
					ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, "Sch_UndeclaredAttribute", new string[] { text3 });
					validationContext.EmitError(validationErrorInfo);
				}
			}
			foreach (OpenXmlAttribute openXmlAttribute in element.ExtendedAttributes)
			{
				if (!validationContext.McContext.IsIgnorableNs(openXmlAttribute.NamespaceUri) && !("http://www.w3.org/XML/1998/namespace" == openXmlAttribute.NamespaceUri))
				{
					string text4 = openXmlAttribute.XmlQualifiedName.ToString();
					ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, "Sch_UndeclaredAttribute", new string[] { text4 });
					validationContext.EmitError(validationErrorInfo);
				}
			}
		}

		// Token: 0x0601B42B RID: 111659 RVA: 0x00374148 File Offset: 0x00372348
		internal static void ValidateValue(ValidationContext validationContext, SimpleTypeRestriction simpleTypeConstraint, OpenXmlSimpleType value, string qname, bool isAttribute)
		{
			OpenXmlElement element = validationContext.Element;
			RedirectedRestriction redirectedRestriction = simpleTypeConstraint as RedirectedRestriction;
			if (redirectedRestriction != null)
			{
				OpenXmlSimpleType openXmlSimpleType = redirectedRestriction.ConvertValue(value);
				SchemaTypeValidator.ValidateValue(validationContext, redirectedRestriction.TargetRestriction, openXmlSimpleType, qname, isAttribute);
				return;
			}
			string text;
			if (isAttribute)
			{
				text = "Sch_AttributeValueDataTypeDetailed";
			}
			else
			{
				text = "Sch_ElementValueDataTypeDetailed";
			}
			if (!simpleTypeConstraint.ValidateValueType(value))
			{
				ValidationErrorInfo validationErrorInfo;
				if (simpleTypeConstraint.IsEnum)
				{
					validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[]
					{
						qname,
						value.InnerText,
						ValidationResources.Sch_EnumerationConstraintFailed
					});
				}
				else if (simpleTypeConstraint.XsdType == XsdType.Union)
				{
					validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, isAttribute ? "Sch_AttributeUnionFailedEx" : "Sch_ElementUnionFailedEx", new string[] { qname, value.InnerText });
				}
				else if (string.IsNullOrEmpty(value.InnerText))
				{
					validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[]
					{
						qname,
						value.InnerText,
						isAttribute ? ValidationResources.Sch_EmptyAttributeValue : ValidationResources.Sch_EmptyElementValue
					});
				}
				else if (simpleTypeConstraint.XsdType == XsdType.SpecialBoolean)
				{
					validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[]
					{
						qname,
						value.InnerText,
						ValidationResources.Sch_EnumerationConstraintFailed
					});
				}
				else if (simpleTypeConstraint.IsList)
				{
					validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[]
					{
						qname,
						value.InnerText,
						string.Empty
					});
				}
				else
				{
					string text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_StringIsNotValidValue, new object[] { value.InnerText, simpleTypeConstraint.ClrTypeName });
					validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[] { qname, value.InnerText, text2 });
				}
				validationContext.EmitError(validationErrorInfo);
				return;
			}
			bool flag = true;
			switch (simpleTypeConstraint.XsdType)
			{
			case XsdType.Boolean:
			case XsdType.DateTime:
			case XsdType.Enum:
			case XsdType.SpecialBoolean:
				flag = false;
				break;
			case XsdType.List:
				flag = false;
				break;
			case XsdType.Union:
				flag = false;
				break;
			}
			if (flag)
			{
				RestrictionField restrictionField = simpleTypeConstraint.Validate(value);
				if (restrictionField != RestrictionField.None)
				{
					if ((byte)(restrictionField & RestrictionField.MinInclusive) == 8)
					{
						string text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MinInclusiveConstraintFailed, new object[] { simpleTypeConstraint.GetRestrictionValue(RestrictionField.MinInclusive) });
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[] { qname, value.InnerText, text2 });
						validationContext.EmitError(validationErrorInfo);
					}
					if ((byte)(restrictionField & RestrictionField.MinExclusive) == 32)
					{
						string text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MinExclusiveConstraintFailed, new object[] { simpleTypeConstraint.GetRestrictionValue(RestrictionField.MinExclusive) });
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[] { qname, value.InnerText, text2 });
						validationContext.EmitError(validationErrorInfo);
					}
					if ((byte)(restrictionField & RestrictionField.MaxInclusive) == 16)
					{
						string text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MaxInclusiveConstraintFailed, new object[] { simpleTypeConstraint.GetRestrictionValue(RestrictionField.MaxInclusive) });
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[] { qname, value.InnerText, text2 });
						validationContext.EmitError(validationErrorInfo);
					}
					if ((byte)(restrictionField & RestrictionField.MaxExclusive) == 64)
					{
						string text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MaxExclusiveConstraintFailed, new object[] { simpleTypeConstraint.GetRestrictionValue(RestrictionField.MaxExclusive) });
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[] { qname, value.InnerText, text2 });
						validationContext.EmitError(validationErrorInfo);
					}
					if ((byte)(restrictionField & RestrictionField.Length) == 1)
					{
						if (string.IsNullOrEmpty(value.InnerText))
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[]
							{
								qname,
								value.InnerText,
								isAttribute ? ValidationResources.Sch_EmptyAttributeValue : ValidationResources.Sch_EmptyElementValue
							});
							validationContext.EmitError(validationErrorInfo);
						}
						else
						{
							string text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_LengthConstraintFailed, new object[]
							{
								simpleTypeConstraint.XsdType.GetXsdDataTypeName(),
								simpleTypeConstraint.GetRestrictionValue(RestrictionField.Length)
							});
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[] { qname, value.InnerText, text2 });
							validationContext.EmitError(validationErrorInfo);
						}
					}
					if ((byte)(restrictionField & RestrictionField.MinLength) == 2)
					{
						if (string.IsNullOrEmpty(value.InnerText))
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[]
							{
								qname,
								value.InnerText,
								isAttribute ? ValidationResources.Sch_EmptyAttributeValue : ValidationResources.Sch_EmptyElementValue
							});
							validationContext.EmitError(validationErrorInfo);
						}
						else
						{
							string text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MinLengthConstraintFailed, new object[]
							{
								simpleTypeConstraint.XsdType.GetXsdDataTypeName(),
								simpleTypeConstraint.GetRestrictionValue(RestrictionField.MinLength)
							});
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[] { qname, value.InnerText, text2 });
							validationContext.EmitError(validationErrorInfo);
						}
					}
					if ((byte)(restrictionField & RestrictionField.MaxLength) == 4)
					{
						string text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MaxLengthConstraintFailed, new object[]
						{
							simpleTypeConstraint.XsdType.GetXsdDataTypeName(),
							simpleTypeConstraint.GetRestrictionValue(RestrictionField.MaxLength)
						});
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[] { qname, value.InnerText, text2 });
						validationContext.EmitError(validationErrorInfo);
					}
					if ((byte)(restrictionField & RestrictionField.Pattern) == 128)
					{
						string text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_PatternConstraintFailed, new object[] { simpleTypeConstraint.GetRestrictionValue(RestrictionField.Pattern) });
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, text, new string[] { qname, value.InnerText, text2 });
						validationContext.EmitError(validationErrorInfo);
					}
				}
			}
		}

		// Token: 0x0400B4CA RID: 46282
		private SdbSchemaDatas _sdbSchemaDatas;

		// Token: 0x0200311B RID: 12571
		private static class EmptyComplexTypeValidator
		{
			// Token: 0x0601B42C RID: 111660 RVA: 0x003747F8 File Offset: 0x003729F8
			internal static void Validate(ValidationContext validationContext)
			{
				OpenXmlLeafElement openXmlLeafElement = (OpenXmlLeafElement)validationContext.Element;
				if (openXmlLeafElement.ShadowElement != null)
				{
					foreach (OpenXmlElement openXmlElement in openXmlLeafElement.ShadowElement.ChildElements)
					{
						if (!(openXmlElement is OpenXmlMiscNode))
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(openXmlLeafElement, null, "Sch_InvalidChildinLeafElement", new string[] { openXmlLeafElement.XmlQualifiedName.ToString() });
							validationContext.EmitError(validationErrorInfo);
							break;
						}
					}
				}
			}
		}

		// Token: 0x0200311C RID: 12572
		private static class EmptyRootComplexTypeValidator
		{
			// Token: 0x0601B42D RID: 111661 RVA: 0x00374890 File Offset: 0x00372A90
			internal static void Validate(ValidationContext validationContext)
			{
				OpenXmlCompositeElement openXmlCompositeElement = (OpenXmlCompositeElement)validationContext.Element;
				foreach (OpenXmlElement openXmlElement in openXmlCompositeElement.ChildElements)
				{
					if (!(openXmlElement is OpenXmlMiscNode))
					{
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(openXmlCompositeElement, null, "Sch_InvalidChildinLeafElement", new string[] { openXmlCompositeElement.XmlQualifiedName.ToString() });
						validationContext.EmitError(validationErrorInfo);
						break;
					}
				}
			}
		}

		// Token: 0x0200311D RID: 12573
		private static class SimpleContentComplexTypeValidator
		{
			// Token: 0x0601B42E RID: 111662 RVA: 0x0037491C File Offset: 0x00372B1C
			internal static void Validate(ValidationContext validationContext, SimpleTypeRestriction simpleTypeConstraint)
			{
				SchemaTypeValidator.EmptyComplexTypeValidator.Validate(validationContext);
				OpenXmlLeafTextElement openXmlLeafTextElement = (OpenXmlLeafTextElement)validationContext.Element;
				OpenXmlSimpleType openXmlSimpleType = openXmlLeafTextElement.InnerTextToValue(openXmlLeafTextElement.Text);
				string text = openXmlLeafTextElement.XmlQualifiedName.ToString();
				SchemaTypeValidator.ValidateValue(validationContext, simpleTypeConstraint, openXmlSimpleType, text, false);
			}
		}

		// Token: 0x0200311E RID: 12574
		private static class CompositeComplexTypeValidator
		{
			// Token: 0x0601B42F RID: 111663 RVA: 0x00374960 File Offset: 0x00372B60
			internal static void Validate(ValidationContext validationContext, ParticleConstraint particleConstraint)
			{
				ParticleType particleType = particleConstraint.ParticleType;
				switch (particleType)
				{
				case ParticleType.Element:
				case ParticleType.Any:
					break;
				case ParticleType.All:
				case ParticleType.Choice:
				case ParticleType.Group:
				case ParticleType.Sequence:
				{
					ParticleValidator particleValidator = (ParticleValidator)particleConstraint.ParticleValidator;
					particleValidator.Validate(validationContext);
					break;
				}
				default:
					if (particleType != ParticleType.Invalid)
					{
						return;
					}
					break;
				}
			}
		}
	}
}
