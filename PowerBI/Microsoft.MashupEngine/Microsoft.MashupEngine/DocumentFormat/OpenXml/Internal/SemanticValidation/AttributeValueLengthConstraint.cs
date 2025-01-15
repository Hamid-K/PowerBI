using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030EB RID: 12523
	internal class AttributeValueLengthConstraint : SemanticConstraint
	{
		// Token: 0x0601B2FA RID: 111354 RVA: 0x003708AE File Offset: 0x0036EAAE
		public AttributeValueLengthConstraint(byte attribute, int minLength, int maxLength)
			: base(SemanticValidationLevel.Element)
		{
			this._attribute = attribute;
			this._maxLength = maxLength;
			this._minLength = minLength;
		}

		// Token: 0x0601B2FB RID: 111355 RVA: 0x003708CC File Offset: 0x0036EACC
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._attribute];
			if (openXmlSimpleType == null)
			{
				return null;
			}
			string text = ((openXmlSimpleType.InnerText == null) ? string.Empty : openXmlSimpleType.InnerText);
			string text2 = null;
			if (text.Length < this._minLength)
			{
				text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_MinLengthConstraintFailed, new object[] { this._minLength });
			}
			else if (text.Length > this._maxLength)
			{
				text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_MaxLengthConstraintFailed, new object[] { this._maxLength });
			}
			if (text2 == null)
			{
				return null;
			}
			return new ValidationErrorInfo
			{
				Id = "Sem_AttributeValueDataTypeDetailed",
				ErrorType = ValidationErrorType.Schema,
				Node = context.Element,
				Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_AttributeValueDataTypeDetailed, new object[]
				{
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute),
					text,
					text2
				})
			};
		}

		// Token: 0x0400B431 RID: 46129
		private byte _attribute;

		// Token: 0x0400B432 RID: 46130
		private int _minLength;

		// Token: 0x0400B433 RID: 46131
		private int _maxLength;
	}
}
