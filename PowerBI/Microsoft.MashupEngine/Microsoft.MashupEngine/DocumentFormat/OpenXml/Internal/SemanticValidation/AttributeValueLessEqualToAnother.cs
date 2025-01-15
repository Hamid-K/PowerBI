using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030E8 RID: 12520
	internal class AttributeValueLessEqualToAnother : SemanticConstraint
	{
		// Token: 0x0601B2F4 RID: 111348 RVA: 0x003704ED File Offset: 0x0036E6ED
		public AttributeValueLessEqualToAnother(byte attribute, byte otherAttribute, bool canEqual)
			: base(SemanticValidationLevel.Element)
		{
			this._attribute = attribute;
			this._otherAttribute = otherAttribute;
			this._canEqual = canEqual;
		}

		// Token: 0x0601B2F5 RID: 111349 RVA: 0x0037050C File Offset: 0x0036E70C
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._attribute];
			if (openXmlSimpleType == null)
			{
				return null;
			}
			double num;
			if (!SemanticConstraint.GetAttrNumVal(openXmlSimpleType, out num))
			{
				return null;
			}
			OpenXmlSimpleType openXmlSimpleType2 = context.Element.Attributes[(int)this._otherAttribute];
			if (openXmlSimpleType2 == null)
			{
				return null;
			}
			double num2;
			if (!SemanticConstraint.GetAttrNumVal(openXmlSimpleType2, out num2))
			{
				return null;
			}
			if ((num < num2 && !this._canEqual) || (num <= num2 && this._canEqual))
			{
				return null;
			}
			string text = (this._canEqual ? ValidationResources.Sem_AttributeValueLessEqualToAnother : ValidationResources.Sem_AttributeValueLessEqualToAnotherEx);
			return new ValidationErrorInfo
			{
				Id = "Sem_AttributeValueLessEqualToAnother",
				ErrorType = ValidationErrorType.Semantic,
				Node = context.Element,
				Description = string.Format(CultureInfo.CurrentUICulture, text, new object[]
				{
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute),
					openXmlSimpleType.InnerText,
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._otherAttribute),
					openXmlSimpleType2.InnerText
				})
			};
		}

		// Token: 0x0400B42A RID: 46122
		private byte _attribute;

		// Token: 0x0400B42B RID: 46123
		private byte _otherAttribute;

		// Token: 0x0400B42C RID: 46124
		private bool _canEqual;
	}
}
