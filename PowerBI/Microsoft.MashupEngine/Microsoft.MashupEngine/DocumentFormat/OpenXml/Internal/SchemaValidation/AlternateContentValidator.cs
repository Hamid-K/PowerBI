using System;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x020030D8 RID: 12504
	internal class AlternateContentValidator
	{
		// Token: 0x0601B29E RID: 111262 RVA: 0x0036EF74 File Offset: 0x0036D174
		internal static void Validate(ValidationContext validationContext)
		{
			AlternateContent alternateContent = (AlternateContent)validationContext.Element;
			AlternateContentValidator.ValidateMcAttributesOnAcb(validationContext, alternateContent);
			int num = 0;
			if (alternateContent.ChildElements.Count == 0)
			{
				ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(alternateContent, "Sch_IncompleteContentExpectingComplex", new string[] { ValidationResources.MC_ShallContainChoice });
				validationContext.EmitError(validationErrorInfo);
			}
			for (OpenXmlElement openXmlElement = alternateContent.GetFirstNonMiscElementChild(); openXmlElement != null; openXmlElement = openXmlElement.GetNextNonMiscElementSibling())
			{
				if (openXmlElement is AlternateContent)
				{
					ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(alternateContent, "Sch_InvalidElementContentExpectingComplex", new string[]
					{
						openXmlElement.XmlQualifiedName.ToString(),
						ValidationResources.MC_ShallNotContainAlternateContent
					});
					validationContext.EmitError(validationErrorInfo);
				}
				else
				{
					switch (num)
					{
					case 0:
						if (openXmlElement is AlternateContentChoice)
						{
							AlternateContentValidator.ValidateMcAttributesOnAcb(validationContext, openXmlElement);
							num = 1;
						}
						else
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(alternateContent, "Sch_IncompleteContentExpectingComplex", new string[] { ValidationResources.MC_ShallContainChoice });
							validationContext.EmitError(validationErrorInfo);
							if (openXmlElement is AlternateContentFallback)
							{
								AlternateContentValidator.ValidateMcAttributesOnAcb(validationContext, openXmlElement);
							}
						}
						break;
					case 1:
						if (openXmlElement is AlternateContentChoice)
						{
							AlternateContentValidator.ValidateMcAttributesOnAcb(validationContext, openXmlElement);
							num = 1;
						}
						else if (openXmlElement is AlternateContentFallback)
						{
							AlternateContentValidator.ValidateMcAttributesOnAcb(validationContext, openXmlElement);
							num = 2;
						}
						else
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(alternateContent, "Sch_InvalidElementContentExpectingComplex", new string[]
							{
								openXmlElement.XmlQualifiedName.ToString(),
								ValidationResources.MC_ShallContainChoice
							});
							validationContext.EmitError(validationErrorInfo);
						}
						break;
					case 2:
					{
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(alternateContent, "Sch_InvalidElementContentExpectingComplex", new string[]
						{
							openXmlElement.XmlQualifiedName.ToString(),
							ValidationResources.MC_ShallContainChoice
						});
						validationContext.EmitError(validationErrorInfo);
						break;
					}
					}
				}
			}
		}

		// Token: 0x0601B29F RID: 111263 RVA: 0x0036F12C File Offset: 0x0036D32C
		private static void ValidateMcAttributesOnAcb(ValidationContext validationContext, OpenXmlElement acElement)
		{
			if (acElement.ExtendedAttributes != null)
			{
				foreach (OpenXmlAttribute openXmlAttribute in acElement.ExtendedAttributes)
				{
					if (string.IsNullOrEmpty(openXmlAttribute.Prefix))
					{
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(acElement, ValidationResources.MC_ErrorOnUnprefixedAttributeName, new string[] { openXmlAttribute.XmlQualifiedName.ToString() });
						validationContext.EmitError(validationErrorInfo);
					}
					if (AlternateContentValidator.IsXmlSpaceOrXmlLangAttribue(openXmlAttribute))
					{
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(acElement, "MC_InvalidXmlAttribute", new string[] { acElement.LocalName });
						validationContext.EmitError(validationErrorInfo);
					}
				}
			}
			CompatibilityRuleAttributesValidator.ValidateMcAttributes(validationContext);
			AlternateContentChoice alternateContentChoice = acElement as AlternateContentChoice;
			if (alternateContentChoice != null)
			{
				if (alternateContentChoice.Requires == null)
				{
					ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(acElement, "MC_MissedRequiresAttribute", new string[0]);
					validationContext.EmitError(validationErrorInfo);
					return;
				}
				foreach (StringValue stringValue in new ListValue<StringValue>
				{
					InnerText = alternateContentChoice.Requires
				}.Items)
				{
					string text = alternateContentChoice.LookupNamespace(stringValue);
					if (string.IsNullOrEmpty(text))
					{
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(alternateContentChoice, "MC_InvalidRequiresAttribute", new string[] { alternateContentChoice.Requires });
						validationContext.EmitError(validationErrorInfo);
					}
				}
			}
		}

		// Token: 0x0601B2A0 RID: 111264 RVA: 0x0036F2BC File Offset: 0x0036D4BC
		internal static bool IsXmlSpaceOrXmlLangAttribue(OpenXmlAttribute attribute)
		{
			return "http://www.w3.org/XML/1998/namespace" == attribute.NamespaceUri && (attribute.LocalName == "space" || attribute.LocalName == "lang");
		}
	}
}
