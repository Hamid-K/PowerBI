using System;
using System.Globalization;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030F0 RID: 12528
	internal class IndexReferenceConstraint : SemanticConstraint
	{
		// Token: 0x0601B304 RID: 111364 RVA: 0x003710A8 File Offset: 0x0036F2A8
		public IndexReferenceConstraint(byte attribute, string referencedPart, int referencedElementParent, int referencedElement, string referencedElementName, int indexBase)
			: base(SemanticValidationLevel.PackageOnly)
		{
			this._attribute = attribute;
			this._refPartType = referencedPart;
			this._refElement = referencedElement;
			this._refElementParent = referencedElementParent;
			this._refElementName = referencedElementName;
			this._indexBase = indexBase;
		}

		// Token: 0x1700988B RID: 39051
		// (get) Token: 0x0601B305 RID: 111365 RVA: 0x0000240C File Offset: 0x0000060C
		public override SemanticValidationLevel StateScope
		{
			get
			{
				return SemanticValidationLevel.Part;
			}
		}

		// Token: 0x0601B306 RID: 111366 RVA: 0x003710E8 File Offset: 0x0036F2E8
		public override ValidationErrorInfo Validate(ValidationContext context)
		{
			OpenXmlSimpleType openXmlSimpleType = context.Element.Attributes[(int)this._attribute];
			if (openXmlSimpleType == null || string.IsNullOrEmpty(openXmlSimpleType.InnerText))
			{
				return null;
			}
			int num;
			if (!int.TryParse(openXmlSimpleType, out num))
			{
				return null;
			}
			if (num < this.GetRefElementCount(context) + this._indexBase)
			{
				return null;
			}
			return new ValidationErrorInfo
			{
				Id = "Sem_MissingIndexedElement",
				ErrorType = ValidationErrorType.Semantic,
				Node = context.Element,
				RelatedPart = this._relatedPart,
				RelatedNode = null,
				Description = string.Format(CultureInfo.CurrentUICulture, ValidationResources.Sem_MissingIndexedElement, new object[]
				{
					this._refElementName,
					context.Element.LocalName,
					SemanticConstraint.GetAttributeQualifiedName(context.Element, this._attribute),
					(this._relatedPart == null) ? this._refPartType : this._relatedPart.PackagePart.Uri.ToString(),
					num
				})
			};
		}

		// Token: 0x0601B307 RID: 111367 RVA: 0x003711EF File Offset: 0x0036F3EF
		public override void ClearState(ValidationContext context)
		{
			this._refElementCount = -1;
			this._startCollect = false;
		}

		// Token: 0x0601B308 RID: 111368 RVA: 0x00371200 File Offset: 0x0036F400
		private int GetRefElementCount(ValidationContext context)
		{
			if (this._refElementCount != -1)
			{
				return this._refElementCount;
			}
			this._refElementCount = 0;
			OpenXmlPart referencedPart = SemanticConstraint.GetReferencedPart(context, this._refPartType);
			if (referencedPart != null)
			{
				this._relatedPart = referencedPart;
				ValidationContext validationContext = new ValidationContext();
				validationContext.FileFormat = context.FileFormat;
				validationContext.Package = context.Package;
				validationContext.Part = referencedPart;
				validationContext.Element = referencedPart.RootElement;
				if (this._refElementParent == -1)
				{
					this._startCollect = true;
					ValidationTraverser.ValidatingTraverse(validationContext, new ValidationTraverser.ValidationAction(this.ElementTraverseStart), null, null);
				}
				else
				{
					ValidationTraverser.ValidatingTraverse(validationContext, new ValidationTraverser.ValidationAction(this.ElementTraverseStart), new ValidationTraverser.ValidationAction(this.ElementTraverseEnd), null);
				}
			}
			return this._refElementCount;
		}

		// Token: 0x0601B309 RID: 111369 RVA: 0x003712B8 File Offset: 0x0036F4B8
		private void ElementTraverseStart(ValidationContext context)
		{
			if (!this._startCollect)
			{
				this._startCollect = context.Element.ElementTypeId == this._refElementParent;
				return;
			}
			if (context.Element.ElementTypeId == this._refElement)
			{
				this._refElementCount++;
			}
		}

		// Token: 0x0601B30A RID: 111370 RVA: 0x00371308 File Offset: 0x0036F508
		private void ElementTraverseEnd(ValidationContext context)
		{
			if (this._startCollect && context.Element.ElementTypeId == this._refElementParent)
			{
				this._startCollect = false;
			}
		}

		// Token: 0x0400B443 RID: 46147
		private byte _attribute;

		// Token: 0x0400B444 RID: 46148
		private string _refPartType;

		// Token: 0x0400B445 RID: 46149
		private int _refElementParent;

		// Token: 0x0400B446 RID: 46150
		private int _refElement;

		// Token: 0x0400B447 RID: 46151
		private string _refElementName;

		// Token: 0x0400B448 RID: 46152
		private int _indexBase;

		// Token: 0x0400B449 RID: 46153
		private int _refElementCount = -1;

		// Token: 0x0400B44A RID: 46154
		private OpenXmlPart _relatedPart;

		// Token: 0x0400B44B RID: 46155
		private bool _startCollect;
	}
}
