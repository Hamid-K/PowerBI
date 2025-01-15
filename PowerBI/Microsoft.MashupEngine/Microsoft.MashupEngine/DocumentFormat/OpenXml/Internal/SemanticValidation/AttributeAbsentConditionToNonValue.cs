using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030E6 RID: 12518
	internal class AttributeAbsentConditionToNonValue : SemanticConstraint
	{
		// Token: 0x0601B2F0 RID: 111344 RVA: 0x003702E2 File Offset: 0x0036E4E2
		public AttributeAbsentConditionToNonValue(byte absentAttribute, byte conditionAttribute, params string[] values)
			: base(SemanticValidationLevel.Element)
		{
			this._absentAttribute = absentAttribute;
			this._conditionAttribute = conditionAttribute;
			this._values = values;
		}

		// Token: 0x0601B2F1 RID: 111345 RVA: 0x00370300 File Offset: 0x0036E500
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
					return null;
				}
			}
			string text2 = "'" + this._values[0] + "'";
			if (this._values.Length > 1)
			{
				for (int j = 1; j < this._values.Length - 1; j++)
				{
					text2 = text2 + ", '" + this._values[j] + "'";
				}
				text2 = text2 + " and '" + this._values[this._values.Length - 1] + "'";
			}
			return new ValidationErrorInfo
			{
				Id = "Sem_AttributeAbsentConditionToNonValue",
				ErrorType = ValidationErrorType.Semantic,
				Node = context.Element,
				Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_AttributeAbsentConditionToNonValue, new object[]
				{
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._absentAttribute),
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._conditionAttribute),
					text2
				})
			};
		}

		// Token: 0x0400B426 RID: 46118
		private byte _absentAttribute;

		// Token: 0x0400B427 RID: 46119
		private byte _conditionAttribute;

		// Token: 0x0400B428 RID: 46120
		private string[] _values;
	}
}
