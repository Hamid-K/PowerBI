using System;
using System.Collections.Generic;
using System.Globalization;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030F1 RID: 12529
	internal class ReferenceExistConstraint : SemanticConstraint
	{
		// Token: 0x0601B30B RID: 111371 RVA: 0x0037132C File Offset: 0x0036F52C
		public ReferenceExistConstraint(byte refAttribute, string part, int element, string elementName, byte attribute)
			: base(SemanticValidationLevel.PackageOnly)
		{
			this._refAttribute = refAttribute;
			this._partPath = part;
			this._element = element;
			this._elementName = elementName;
			this._attribute = attribute;
		}

		// Token: 0x1700988C RID: 39052
		// (get) Token: 0x0601B30C RID: 111372 RVA: 0x0000240C File Offset: 0x0000060C
		public override SemanticValidationLevel StateScope
		{
			get
			{
				return SemanticValidationLevel.Part;
			}
		}

		// Token: 0x0601B30D RID: 111373 RVA: 0x0037135C File Offset: 0x0036F55C
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._refAttribute];
			if (openXmlSimpleType == null || string.IsNullOrEmpty(openXmlSimpleType.InnerText))
			{
				return null;
			}
			if (this.GetReferencedAttributes(context).Contains(openXmlSimpleType.InnerText))
			{
				return null;
			}
			return new ValidationErrorInfo
			{
				Id = "Sem_MissingReferenceElement",
				ErrorType = ValidationErrorType.Semantic,
				Node = context.Element,
				RelatedPart = this._relatedPart,
				RelatedNode = null,
				Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_MissingReferenceElement, new object[]
				{
					this._elementName,
					context.Element.LocalName,
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._refAttribute),
					(this._relatedPart == null) ? this._partPath : this._relatedPart.PackagePart.Uri.ToString(),
					openXmlSimpleType.InnerText
				})
			};
		}

		// Token: 0x0601B30E RID: 111374 RVA: 0x00371455 File Offset: 0x0036F655
		public override void ClearState(ValidationContext context)
		{
			this._referencedAttributes = null;
		}

		// Token: 0x0601B30F RID: 111375 RVA: 0x00371460 File Offset: 0x0036F660
		private List<string> GetReferencedAttributes(ValidationContext context)
		{
			if (this._referencedAttributes == null)
			{
				this._referencedAttributes = new List<string>();
				OpenXmlPart referencedPart = SemanticConstraint.GetReferencedPart(context, this._partPath);
				this._relatedPart = referencedPart;
				if (referencedPart != null)
				{
					ValidationTraverser.ValidatingTraverse(new ValidationContext
					{
						FileFormat = context.FileFormat,
						Package = context.Package,
						Part = referencedPart,
						Element = referencedPart.RootElement
					}, new ValidationTraverser.ValidationAction(this.ElementTraverse), null, null);
				}
			}
			return this._referencedAttributes;
		}

		// Token: 0x0601B310 RID: 111376 RVA: 0x003714E4 File Offset: 0x0036F6E4
		private void ElementTraverse(ValidationContext context)
		{
			if (context.Element.ElementTypeId == this._element)
			{
				OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._attribute];
				if (openXmlSimpleType != null && !string.IsNullOrEmpty(openXmlSimpleType.InnerText))
				{
					this._referencedAttributes.Add(openXmlSimpleType.InnerText);
				}
			}
		}

		// Token: 0x0400B44C RID: 46156
		private byte _refAttribute;

		// Token: 0x0400B44D RID: 46157
		private string _partPath;

		// Token: 0x0400B44E RID: 46158
		private int _element;

		// Token: 0x0400B44F RID: 46159
		private string _elementName;

		// Token: 0x0400B450 RID: 46160
		private byte _attribute;

		// Token: 0x0400B451 RID: 46161
		private List<string> _referencedAttributes;

		// Token: 0x0400B452 RID: 46162
		private OpenXmlPart _relatedPart;
	}
}
