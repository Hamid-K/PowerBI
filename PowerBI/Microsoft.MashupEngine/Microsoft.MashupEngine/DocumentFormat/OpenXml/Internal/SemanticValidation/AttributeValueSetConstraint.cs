using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030EE RID: 12526
	internal class AttributeValueSetConstraint : SemanticConstraint
	{
		// Token: 0x0601B300 RID: 111360 RVA: 0x00370D3D File Offset: 0x0036EF3D
		public AttributeValueSetConstraint(byte attribute, bool isValid, string[] valueSet)
			: base(SemanticValidationLevel.Element)
		{
			this._attribute = attribute;
			this._isValidValueSet = isValid;
			this._valueSet = valueSet;
		}

		// Token: 0x0601B301 RID: 111361 RVA: 0x00370D5C File Offset: 0x0036EF5C
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._attribute];
			if (openXmlSimpleType == null || string.IsNullOrEmpty(openXmlSimpleType.InnerText))
			{
				return null;
			}
			bool flag = false;
			foreach (string text in this._valueSet)
			{
				if (SemanticConstraint.AttributeValueEquals(openXmlSimpleType, text, false))
				{
					flag = true;
				}
			}
			if (!this._isValidValueSet ^ flag)
			{
				return null;
			}
			string sch_EnumerationConstraintFailed = ValidationResources.Sch_EnumerationConstraintFailed;
			string text2 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_AttributeValueDataTypeDetailed, new object[]
			{
				SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute),
				openXmlSimpleType,
				sch_EnumerationConstraintFailed
			});
			return new ValidationErrorInfo
			{
				Id = "Sem_AttributeValueDataTypeDetailed",
				ErrorType = ValidationErrorType.Schema,
				Node = context.Element,
				Description = text2
			};
		}

		// Token: 0x0400B43C RID: 46140
		private byte _attribute;

		// Token: 0x0400B43D RID: 46141
		private bool _isValidValueSet;

		// Token: 0x0400B43E RID: 46142
		private string[] _valueSet;
	}
}
