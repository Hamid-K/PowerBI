using System;
using System.Globalization;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030EC RID: 12524
	internal class AttributeValuePatternConstraint : SemanticConstraint
	{
		// Token: 0x0601B2FC RID: 111356 RVA: 0x003709E4 File Offset: 0x0036EBE4
		public AttributeValuePatternConstraint(byte attribute, string pattern)
			: base(SemanticValidationLevel.Element)
		{
			this._attribute = attribute;
			if (pattern.StartsWith("^", StringComparison.Ordinal) && pattern.EndsWith("$", StringComparison.Ordinal))
			{
				this._pattern = pattern;
				return;
			}
			this._pattern = "^" + pattern + "$";
		}

		// Token: 0x0601B2FD RID: 111357 RVA: 0x00370A3C File Offset: 0x0036EC3C
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._attribute];
			if (openXmlSimpleType == null || string.IsNullOrEmpty(openXmlSimpleType.InnerText))
			{
				return null;
			}
			Regex regex = new Regex(this._pattern);
			if (regex.IsMatch(openXmlSimpleType.InnerText))
			{
				return null;
			}
			string text = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_PatternConstraintFailed, new object[] { this._pattern });
			return new ValidationErrorInfo
			{
				Id = "Sem_AttributeValueDataTypeDetailed",
				ErrorType = ValidationErrorType.Schema,
				Node = context.Element,
				Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_AttributeValueDataTypeDetailed, new object[]
				{
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute),
					openXmlSimpleType.InnerText,
					text
				})
			};
		}

		// Token: 0x0400B434 RID: 46132
		private byte _attribute;

		// Token: 0x0400B435 RID: 46133
		private string _pattern;
	}
}
