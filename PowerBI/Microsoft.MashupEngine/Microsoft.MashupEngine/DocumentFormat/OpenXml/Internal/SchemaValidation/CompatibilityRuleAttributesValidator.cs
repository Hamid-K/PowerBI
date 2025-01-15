using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x020030D9 RID: 12505
	internal static class CompatibilityRuleAttributesValidator
	{
		// Token: 0x0601B2A2 RID: 111266 RVA: 0x0036F2FC File Offset: 0x0036D4FC
		internal static void ValidateMcAttributes(ValidationContext validationContext)
		{
			OpenXmlElement element = validationContext.Element;
			if (element.MCAttributes == null)
			{
				return;
			}
			HashSet<string> hashSet = null;
			if (element.MCAttributes != null)
			{
				if (!string.IsNullOrEmpty(element.MCAttributes.Ignorable))
				{
					hashSet = new HashSet<string>();
					foreach (StringValue stringValue in new ListValue<StringValue>
					{
						InnerText = element.MCAttributes.Ignorable
					}.Items)
					{
						string text = element.LookupNamespace(stringValue);
						if (string.IsNullOrEmpty(text))
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(element, "MC_InvalidIgnorableAttribute", new string[] { element.MCAttributes.Ignorable });
							validationContext.EmitError(validationErrorInfo);
						}
						else
						{
							hashSet.Add(text);
						}
					}
				}
				if (!string.IsNullOrEmpty(element.MCAttributes.PreserveAttributes))
				{
					if (hashSet == null)
					{
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(element, "MC_InvalidPreserveAttributesAttribute", new string[] { element.MCAttributes.PreserveAttributes });
						validationContext.EmitError(validationErrorInfo);
					}
					else
					{
						string text2 = CompatibilityRuleAttributesValidator.ValidateQNameList(element.MCAttributes.PreserveAttributes, hashSet, validationContext);
						if (!string.IsNullOrEmpty(text2))
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(element, "MC_InvalidPreserveAttributesAttribute", new string[] { element.MCAttributes.PreserveAttributes });
							validationContext.EmitError(validationErrorInfo);
						}
					}
				}
				if (!string.IsNullOrEmpty(element.MCAttributes.PreserveElements))
				{
					if (hashSet == null)
					{
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(element, "MC_InvalidPreserveElementsAttribute", new string[] { element.MCAttributes.PreserveElements });
						validationContext.EmitError(validationErrorInfo);
					}
					else
					{
						string text3 = CompatibilityRuleAttributesValidator.ValidateQNameList(element.MCAttributes.PreserveElements, hashSet, validationContext);
						if (!string.IsNullOrEmpty(text3))
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(element, "MC_InvalidPreserveElementsAttribute", new string[] { element.MCAttributes.PreserveElements });
							validationContext.EmitError(validationErrorInfo);
						}
					}
				}
				if (!string.IsNullOrEmpty(element.MCAttributes.ProcessContent))
				{
					if (hashSet == null)
					{
						ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(element, "MC_InvalidProcessContentAttribute", new string[] { element.MCAttributes.ProcessContent });
						validationContext.EmitError(validationErrorInfo);
					}
					else
					{
						string text4 = CompatibilityRuleAttributesValidator.ValidateQNameList(element.MCAttributes.ProcessContent, hashSet, validationContext);
						if (!string.IsNullOrEmpty(text4))
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(element, "MC_InvalidProcessContentAttribute", new string[] { element.MCAttributes.ProcessContent });
							validationContext.EmitError(validationErrorInfo);
						}
					}
					foreach (OpenXmlAttribute openXmlAttribute in element.ExtendedAttributes)
					{
						if (AlternateContentValidator.IsXmlSpaceOrXmlLangAttribue(openXmlAttribute))
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(element, "MC_InvalidXmlAttributeWithProcessContent", new string[0]);
							validationContext.EmitError(validationErrorInfo);
						}
					}
				}
				if (!string.IsNullOrEmpty(element.MCAttributes.MustUnderstand))
				{
					foreach (StringValue stringValue2 in new ListValue<StringValue>
					{
						InnerText = element.MCAttributes.MustUnderstand
					}.Items)
					{
						string text5 = element.LookupNamespace(stringValue2);
						if (string.IsNullOrEmpty(text5))
						{
							ValidationErrorInfo validationErrorInfo = validationContext.ComposeMcValidationError(element, "MC_InvalidMustUnderstandAttribute", new string[] { element.MCAttributes.MustUnderstand });
							validationContext.EmitError(validationErrorInfo);
						}
					}
				}
			}
		}

		// Token: 0x0601B2A3 RID: 111267 RVA: 0x0036F704 File Offset: 0x0036D904
		internal static string ValidateQNameList(string qnameList, HashSet<string> ignorableNamespaces, ValidationContext validationContext)
		{
			foreach (StringValue stringValue in new ListValue<StringValue>
			{
				InnerText = qnameList
			}.Items)
			{
				string[] array = stringValue.Value.Split(new char[] { ':' });
				if (array.Length != 2)
				{
					return stringValue;
				}
				string text = validationContext.Element.LookupNamespace(array[0]);
				if (string.IsNullOrEmpty(text))
				{
					return stringValue;
				}
				if (!ignorableNamespaces.Contains(text))
				{
					return stringValue;
				}
			}
			return null;
		}
	}
}
