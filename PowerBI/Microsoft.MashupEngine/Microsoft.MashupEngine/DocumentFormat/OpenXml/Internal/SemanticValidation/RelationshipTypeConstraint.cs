using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030F3 RID: 12531
	internal class RelationshipTypeConstraint : SemanticConstraint
	{
		// Token: 0x0601B313 RID: 111379 RVA: 0x003715F0 File Offset: 0x0036F7F0
		public RelationshipTypeConstraint(byte attribute, string type)
			: base(SemanticValidationLevel.Part)
		{
			this._attribute = attribute;
			this._type = type;
		}

		// Token: 0x0601B314 RID: 111380 RVA: 0x00371608 File Offset: 0x0036F808
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType attributeValue = context.Element.Attributes[(int)this._attribute];
			if (attributeValue == null || string.IsNullOrEmpty(attributeValue.InnerText))
			{
				return null;
			}
			string text = this._type;
			IEnumerable<ExternalRelationship> enumerable = context.Part.ExternalRelationships.Where((ExternalRelationship r) => r.Id == attributeValue.InnerText);
			if (enumerable.Count<ExternalRelationship>() == 0)
			{
				IEnumerable<IdPartPair> enumerable2 = context.Part.Parts.Where((IdPartPair p) => p.RelationshipId == attributeValue.InnerText);
				if (enumerable2.Count<IdPartPair>() != 0)
				{
					text = enumerable2.First<IdPartPair>().OpenXmlPart.RelationshipType;
				}
			}
			else
			{
				text = enumerable.First<ExternalRelationship>().RelationshipType;
			}
			if (text == this._type)
			{
				return null;
			}
			return new ValidationErrorInfo
			{
				Id = "Sem_IncorrectRelationshipType",
				ErrorType = ValidationErrorType.Semantic,
				Node = context.Element,
				Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_IncorrectRelationshipType, new object[]
				{
					text,
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute),
					this._type
				})
			};
		}

		// Token: 0x0400B454 RID: 46164
		private byte _attribute;

		// Token: 0x0400B455 RID: 46165
		private string _type;
	}
}
