using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030EA RID: 12522
	internal class AttributeRequiredConditionToValue : SemanticConstraint
	{
		// Token: 0x0601B2F8 RID: 111352 RVA: 0x00370725 File Offset: 0x0036E925
		public AttributeRequiredConditionToValue(byte requiredAttribute, byte conditionAttribute, params string[] values)
			: base(SemanticValidationLevel.Element)
		{
			this._requiredAttribute = requiredAttribute;
			this._conditionAttribute = conditionAttribute;
			this._values = values;
		}

		// Token: 0x0601B2F9 RID: 111353 RVA: 0x00370744 File Offset: 0x0036E944
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._requiredAttribute];
			if (openXmlSimpleType != null)
			{
				return null;
			}
			OpenXmlSimpleType openXmlSimpleType2 = context.Element.Attributes[(int)this._conditionAttribute];
			if (openXmlSimpleType2 == null)
			{
				return null;
			}
			foreach (string text in this._values)
			{
				if (SemanticConstraint.AttributeValueEquals(openXmlSimpleType2, text, false))
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
						Id = "Sem_AttributeRequiredConditionToValue",
						ErrorType = ValidationErrorType.Semantic,
						Node = context.Element,
						Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_AttributeRequiredConditionToValue, new object[]
						{
							SemanticConstraint.GetAttributeQualifiedName(context.Element, this._requiredAttribute),
							SemanticConstraint.GetAttributeQualifiedName(context.Element, this._conditionAttribute),
							text2
						})
					};
				}
			}
			return null;
		}

		// Token: 0x0400B42E RID: 46126
		private string[] _values;

		// Token: 0x0400B42F RID: 46127
		private byte _requiredAttribute;

		// Token: 0x0400B430 RID: 46128
		private byte _conditionAttribute;
	}
}
