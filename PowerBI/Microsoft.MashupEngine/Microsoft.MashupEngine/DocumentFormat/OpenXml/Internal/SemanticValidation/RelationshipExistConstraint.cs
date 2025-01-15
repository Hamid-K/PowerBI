using System;
using System.Globalization;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030F2 RID: 12530
	internal class RelationshipExistConstraint : SemanticConstraint
	{
		// Token: 0x0601B311 RID: 111377 RVA: 0x00371538 File Offset: 0x0036F738
		public RelationshipExistConstraint(byte rIdAttribute)
			: base(SemanticValidationLevel.Part)
		{
			this._rIdAttribute = rIdAttribute;
		}

		// Token: 0x0601B312 RID: 111378 RVA: 0x00371548 File Offset: 0x0036F748
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._rIdAttribute];
			if (openXmlSimpleType == null || string.IsNullOrEmpty(openXmlSimpleType.InnerText))
			{
				return null;
			}
			if (context.Part.PackagePart.RelationshipExists(openXmlSimpleType.InnerText))
			{
				return null;
			}
			string text = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_InvalidRelationshipId, new object[]
			{
				openXmlSimpleType,
				SemanticConstraint.GetAttributeQualifiedName(context.Element, this._rIdAttribute)
			});
			return new ValidationErrorInfo
			{
				Id = "Sem_InvalidRelationshipId",
				ErrorType = ValidationErrorType.Semantic,
				Node = context.Element,
				Description = text
			};
		}

		// Token: 0x0400B453 RID: 46163
		private byte _rIdAttribute;
	}
}
