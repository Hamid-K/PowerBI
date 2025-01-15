using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030EF RID: 12527
	internal class AttributeValueConditionToAnother : SemanticConstraint
	{
		// Token: 0x0601B302 RID: 111362 RVA: 0x00370E3F File Offset: 0x0036F03F
		public AttributeValueConditionToAnother(byte attribute, byte conditionAttribute, string[] values, string[] otherValues)
			: base(SemanticValidationLevel.Element)
		{
			this._attribute = attribute;
			this._conditionAttribute = conditionAttribute;
			this._values = values;
			this._otherValues = otherValues;
		}

		// Token: 0x0601B303 RID: 111363 RVA: 0x00370E68 File Offset: 0x0036F068
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._attribute];
			if (openXmlSimpleType == null)
			{
				return null;
			}
			foreach (string text in this._values)
			{
				if (SemanticConstraint.AttributeValueEquals(openXmlSimpleType, text, false))
				{
					return null;
				}
			}
			OpenXmlSimpleType openXmlSimpleType2 = context.Element.Attributes[(int)this._conditionAttribute];
			if (openXmlSimpleType2 == null)
			{
				return null;
			}
			foreach (string text2 in this._otherValues)
			{
				if (SemanticConstraint.AttributeValueEquals(openXmlSimpleType2, text2, false))
				{
					string text3 = "'" + this._values[0] + "'";
					if (this._values.Length > 1)
					{
						for (int k = 1; k < this._values.Length - 1; k++)
						{
							text3 = text3 + ", '" + this._values[k] + "'";
						}
						text3 = text3 + " or '" + this._values[this._values.Length - 1] + "'";
					}
					string text4 = "'" + this._otherValues[0] + "'";
					if (this._otherValues.Length > 1)
					{
						for (int l = 1; l < this._otherValues.Length - 1; l++)
						{
							text4 = text4 + ", '" + this._otherValues[l] + "'";
						}
						text4 = text4 + " or '" + this._otherValues[this._otherValues.Length - 1] + "'";
					}
					return new ValidationErrorInfo
					{
						Id = "Sem_AttributeValueConditionToAnother",
						ErrorType = ValidationErrorType.Semantic,
						Node = context.Element,
						Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_AttributeValueConditionToAnother, new object[]
						{
							SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute),
							text3,
							SemanticConstraint.GetAttributeQualifiedName(context.Element, this._conditionAttribute),
							text4,
							SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute),
							openXmlSimpleType
						})
					};
				}
			}
			return null;
		}

		// Token: 0x0400B43F RID: 46143
		private byte _attribute;

		// Token: 0x0400B440 RID: 46144
		private byte _conditionAttribute;

		// Token: 0x0400B441 RID: 46145
		private string[] _values;

		// Token: 0x0400B442 RID: 46146
		private string[] _otherValues;
	}
}
