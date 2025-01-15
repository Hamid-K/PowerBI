using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030E7 RID: 12519
	internal class AttributeCannotOmitConstraint : SemanticConstraint
	{
		// Token: 0x0601B2F2 RID: 111346 RVA: 0x00370464 File Offset: 0x0036E664
		public AttributeCannotOmitConstraint(byte attribute)
			: base(SemanticValidationLevel.Element)
		{
			this._attribute = attribute;
		}

		// Token: 0x0601B2F3 RID: 111347 RVA: 0x00370474 File Offset: 0x0036E674
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			if (context.Element.Attributes[(int)this._attribute] != null)
			{
				return null;
			}
			return new ValidationErrorInfo
			{
				Id = "Sem_MissRequiredAttribute",
				ErrorType = ValidationErrorType.Schema,
				Node = context.Element,
				Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sch_MissRequiredAttribute, new object[] { SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute) })
			};
		}

		// Token: 0x0400B429 RID: 46121
		private byte _attribute;
	}
}
