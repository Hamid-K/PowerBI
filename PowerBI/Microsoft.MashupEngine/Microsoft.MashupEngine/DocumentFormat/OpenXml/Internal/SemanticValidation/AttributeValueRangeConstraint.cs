using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030ED RID: 12525
	internal class AttributeValueRangeConstraint : SemanticConstraint
	{
		// Token: 0x0601B2FE RID: 111358 RVA: 0x00370B16 File Offset: 0x0036ED16
		public AttributeValueRangeConstraint(byte attribute, bool isValid, double minValue, bool minInclusive, double maxValue, bool maxInclusive)
			: base(SemanticValidationLevel.Element)
		{
			this._attribute = attribute;
			this._isValidRange = isValid;
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._minInclusive = minInclusive;
			this._maxInclusive = maxInclusive;
		}

		// Token: 0x0601B2FF RID: 111359 RVA: 0x00370B4C File Offset: 0x0036ED4C
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._attribute];
			if (openXmlSimpleType == null || !openXmlSimpleType.HasValue || string.IsNullOrEmpty(openXmlSimpleType.InnerText))
			{
				return null;
			}
			double num;
			if (!SemanticConstraint.GetAttrNumVal(openXmlSimpleType, out num))
			{
				return null;
			}
			string text;
			string text2;
			if (openXmlSimpleType is HexBinaryValue)
			{
				text = string.Format(CultureInfo.CurrentUICulture, "{0:X}", new object[] { (long)this._minValue });
				text2 = string.Format(CultureInfo.CurrentUICulture, "{0:X}", new object[] { (long)this._maxValue });
			}
			else
			{
				text = this._minValue.ToString(CultureInfo.CurrentUICulture);
				text2 = this._maxValue.ToString(CultureInfo.CurrentUICulture);
			}
			string text3 = null;
			if (this._minInclusive)
			{
				if (this._minValue > num)
				{
					text3 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MinInclusiveConstraintFailed, new object[] { text });
				}
			}
			else if (this._minValue >= num)
			{
				text3 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MinExclusiveConstraintFailed, new object[] { text });
			}
			if (this._maxInclusive)
			{
				if (num > this._maxValue)
				{
					text3 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MaxInclusiveConstraintFailed, new object[] { text2 });
				}
			}
			else if (num >= this._maxValue)
			{
				text3 = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MaxExclusiveConstraintFailed, new object[] { text2 });
			}
			if (text3 == null)
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
					openXmlSimpleType,
					text3
				})
			};
		}

		// Token: 0x0400B436 RID: 46134
		public byte _attribute;

		// Token: 0x0400B437 RID: 46135
		public bool _isValidRange;

		// Token: 0x0400B438 RID: 46136
		public double _minValue;

		// Token: 0x0400B439 RID: 46137
		public double _maxValue;

		// Token: 0x0400B43A RID: 46138
		private bool _minInclusive;

		// Token: 0x0400B43B RID: 46139
		private bool _maxInclusive;
	}
}
