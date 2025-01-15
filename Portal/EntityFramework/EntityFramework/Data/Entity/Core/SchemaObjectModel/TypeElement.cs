using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000321 RID: 801
	internal class TypeElement : SchemaType
	{
		// Token: 0x06002616 RID: 9750 RVA: 0x0006CE34 File Offset: 0x0006B034
		public TypeElement(Schema parent)
			: base(parent)
		{
			this._primitiveType.NamespaceName = base.Schema.Namespace;
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x0006CE6C File Offset: 0x0006B06C
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "FacetDescriptions"))
			{
				this.SkipThroughElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Precision"))
			{
				this.HandlePrecisionElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Scale"))
			{
				this.HandleScaleElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "MaxLength"))
			{
				this.HandleMaxLengthElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Unicode"))
			{
				this.HandleUnicodeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "FixedLength"))
			{
				this.HandleFixedLengthElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "SRID"))
			{
				this.HandleSridElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "IsStrict"))
			{
				this.HandleIsStrictElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x0006CF3D File Offset: 0x0006B13D
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "PrimitiveTypeKind"))
			{
				this.HandlePrimitiveTypeKindAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x0006CF64 File Offset: 0x0006B164
		private void HandlePrecisionElement(XmlReader reader)
		{
			ByteFacetDescriptionElement byteFacetDescriptionElement = new ByteFacetDescriptionElement(this, "Precision");
			byteFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(byteFacetDescriptionElement);
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x0006CF90 File Offset: 0x0006B190
		private void HandleScaleElement(XmlReader reader)
		{
			ByteFacetDescriptionElement byteFacetDescriptionElement = new ByteFacetDescriptionElement(this, "Scale");
			byteFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(byteFacetDescriptionElement);
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x0006CFBC File Offset: 0x0006B1BC
		private void HandleMaxLengthElement(XmlReader reader)
		{
			IntegerFacetDescriptionElement integerFacetDescriptionElement = new IntegerFacetDescriptionElement(this, "MaxLength");
			integerFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(integerFacetDescriptionElement);
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x0006CFE8 File Offset: 0x0006B1E8
		private void HandleUnicodeElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "Unicode");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x0006D014 File Offset: 0x0006B214
		private void HandleFixedLengthElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "FixedLength");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x0006D040 File Offset: 0x0006B240
		private void HandleSridElement(XmlReader reader)
		{
			SridFacetDescriptionElement sridFacetDescriptionElement = new SridFacetDescriptionElement(this, "SRID");
			sridFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(sridFacetDescriptionElement);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x0006D06C File Offset: 0x0006B26C
		private void HandleIsStrictElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "IsStrict");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x0006D098 File Offset: 0x0006B298
		private void HandlePrimitiveTypeKindAttribute(XmlReader reader)
		{
			string value = reader.Value;
			try
			{
				this._primitiveType.PrimitiveTypeKind = (PrimitiveTypeKind)Enum.Parse(typeof(PrimitiveTypeKind), value);
				this._primitiveType.BaseType = MetadataItem.EdmProviderManifest.GetPrimitiveType(this._primitiveType.PrimitiveTypeKind);
			}
			catch (ArgumentException)
			{
				base.AddError(ErrorCode.InvalidPrimitiveTypeKind, EdmSchemaErrorSeverity.Error, Strings.InvalidPrimitiveTypeKind(value));
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x0006D110 File Offset: 0x0006B310
		// (set) Token: 0x06002622 RID: 9762 RVA: 0x0006D11D File Offset: 0x0006B31D
		public override string Name
		{
			get
			{
				return this._primitiveType.Name;
			}
			set
			{
				this._primitiveType.Name = value;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x0006D12B File Offset: 0x0006B32B
		public PrimitiveType PrimitiveType
		{
			get
			{
				return this._primitiveType;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x0006D133 File Offset: 0x0006B333
		public IEnumerable<FacetDescription> FacetDescriptions
		{
			get
			{
				foreach (FacetDescriptionElement facetDescriptionElement in this._facetDescriptions)
				{
					yield return facetDescriptionElement.FacetDescription;
				}
				List<FacetDescriptionElement>.Enumerator enumerator = default(List<FacetDescriptionElement>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x0006D144 File Offset: 0x0006B344
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			foreach (FacetDescriptionElement facetDescriptionElement in this._facetDescriptions)
			{
				try
				{
					facetDescriptionElement.CreateAndValidateFacetDescription(this.Name);
				}
				catch (ArgumentException ex)
				{
					base.AddError(ErrorCode.InvalidFacetInProviderManifest, EdmSchemaErrorSeverity.Error, ex.Message);
				}
			}
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x0006D1C4 File Offset: 0x0006B3C4
		internal override void Validate()
		{
			base.Validate();
			if (!this.ValidateSufficientFacets())
			{
				return;
			}
			this.ValidateInterFacetConsistency();
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x0006D1DC File Offset: 0x0006B3DC
		private bool ValidateInterFacetConsistency()
		{
			if (this.PrimitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Decimal)
			{
				FacetDescription facet = Helper.GetFacet(this.FacetDescriptions, "Precision");
				FacetDescription facet2 = Helper.GetFacet(this.FacetDescriptions, "Scale");
				if (facet.MaxValue.Value < facet2.MaxValue.Value)
				{
					base.AddError(ErrorCode.BadPrecisionAndScale, EdmSchemaErrorSeverity.Error, Strings.BadPrecisionAndScale(facet.MaxValue.Value, facet2.MaxValue.Value));
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x0006D270 File Offset: 0x0006B470
		private bool ValidateSufficientFacets()
		{
			PrimitiveType primitiveType = this._primitiveType.BaseType as PrimitiveType;
			if (primitiveType == null)
			{
				return false;
			}
			bool flag = false;
			foreach (FacetDescription facetDescription in primitiveType.FacetDescriptions)
			{
				if (Helper.GetFacet(this.FacetDescriptions, facetDescription.FacetName) == null)
				{
					base.AddError(ErrorCode.RequiredFacetMissing, EdmSchemaErrorSeverity.Error, Strings.MissingFacetDescription(this.PrimitiveType.Name, this.PrimitiveType.PrimitiveTypeKind, facetDescription.FacetName));
					flag = true;
				}
			}
			return !flag;
		}

		// Token: 0x04000D5E RID: 3422
		private readonly PrimitiveType _primitiveType = new PrimitiveType();

		// Token: 0x04000D5F RID: 3423
		private readonly List<FacetDescriptionElement> _facetDescriptions = new List<FacetDescriptionElement>();
	}
}
