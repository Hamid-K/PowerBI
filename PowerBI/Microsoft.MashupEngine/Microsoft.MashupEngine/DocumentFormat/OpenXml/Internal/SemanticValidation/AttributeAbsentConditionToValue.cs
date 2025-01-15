using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030E5 RID: 12517
	internal class AttributeAbsentConditionToValue : SemanticConstraint
	{
		// Token: 0x0601B2EE RID: 111342 RVA: 0x0037015A File Offset: 0x0036E35A
		public AttributeAbsentConditionToValue(byte absentAttribute, byte conditionAttribute, params string[] values)
			: base(SemanticValidationLevel.Element)
		{
			this._absentAttribute = absentAttribute;
			this._conditionAttribute = conditionAttribute;
			this._values = values;
		}

		// Token: 0x0601B2EF RID: 111343 RVA: 0x00370178 File Offset: 0x0036E378
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			if (context.Element.Attributes[(int)this._absentAttribute] == null)
			{
				return null;
			}
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._conditionAttribute];
			if (openXmlSimpleType == null)
			{
				return null;
			}
			foreach (string text in this._values)
			{
				if (SemanticConstraint.AttributeValueEquals(openXmlSimpleType, text, false))
				{
					string text2 = "'" + this._values[0] + "'";
					if (this._values.Length > 1)
					{
						for (int j = 1; j < this._values.Length - 1; j++)
						{
							text2 = text2 + ", '" + this._values[j] + "'";
						}
						text2 = text2 + " or '" + this._values[this._values.Length - 1] + "'";
					}
					return new ValidationErrorInfo
					{
						Id = "Sem_AttributeAbsentConditionToValue",
						ErrorType = ValidationErrorType.Semantic,
						Node = context.Element,
						Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_AttributeAbsentConditionToValue, new object[]
						{
							SemanticConstraint.GetAttributeQualifiedName(context.Element, this._absentAttribute),
							SemanticConstraint.GetAttributeQualifiedName(context.Element, this._conditionAttribute),
							text2
						})
					};
				}
			}
			return null;
		}

		// Token: 0x0400B423 RID: 46115
		private byte _absentAttribute;

		// Token: 0x0400B424 RID: 46116
		private byte _conditionAttribute;

		// Token: 0x0400B425 RID: 46117
		private string[] _values;
	}
}
