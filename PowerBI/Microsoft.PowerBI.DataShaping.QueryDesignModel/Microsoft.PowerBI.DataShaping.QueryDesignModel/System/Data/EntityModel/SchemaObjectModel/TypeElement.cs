using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000056 RID: 86
	internal class TypeElement : SchemaType
	{
		// Token: 0x06000885 RID: 2181 RVA: 0x0001212C File Offset: 0x0001032C
		public TypeElement(Schema parent)
			: base(parent)
		{
			this._primitiveType.NamespaceName = base.Schema.Namespace;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00012164 File Offset: 0x00010364
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
			return false;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00012207 File Offset: 0x00010407
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

		// Token: 0x06000888 RID: 2184 RVA: 0x0001222C File Offset: 0x0001042C
		private void HandlePrecisionElement(XmlReader reader)
		{
			ByteFacetDescriptionElement byteFacetDescriptionElement = new ByteFacetDescriptionElement(this, "Precision");
			byteFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(byteFacetDescriptionElement);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00012258 File Offset: 0x00010458
		private void HandleScaleElement(XmlReader reader)
		{
			ByteFacetDescriptionElement byteFacetDescriptionElement = new ByteFacetDescriptionElement(this, "Scale");
			byteFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(byteFacetDescriptionElement);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00012284 File Offset: 0x00010484
		private void HandleMaxLengthElement(XmlReader reader)
		{
			IntegerFacetDescriptionElement integerFacetDescriptionElement = new IntegerFacetDescriptionElement(this, "MaxLength");
			integerFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(integerFacetDescriptionElement);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000122B0 File Offset: 0x000104B0
		private void HandleUnicodeElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "Unicode");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000122DC File Offset: 0x000104DC
		private void HandleFixedLengthElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "FixedLength");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00012308 File Offset: 0x00010508
		private void HandlePrimitiveTypeKindAttribute(XmlReader reader)
		{
			string value = reader.Value;
			try
			{
				this._primitiveType.PrimitiveTypeKind = (PrimitiveTypeKind)Enum.Parse(typeof(PrimitiveTypeKind), value, false);
				this._primitiveType.BaseType = MetadataItem.EdmProviderManifest.GetPrimitiveType(this._primitiveType.PrimitiveTypeKind);
			}
			catch (ArgumentException)
			{
				base.AddError(ErrorCode.InvalidPrimitiveTypeKind, EdmSchemaErrorSeverity.Error, Strings.InvalidPrimitiveTypeKind(value));
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x00012384 File Offset: 0x00010584
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x00012391 File Offset: 0x00010591
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

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0001239F File Offset: 0x0001059F
		public PrimitiveType PrimitiveType
		{
			get
			{
				return this._primitiveType;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x000123A7 File Offset: 0x000105A7
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

		// Token: 0x06000892 RID: 2194 RVA: 0x000123B8 File Offset: 0x000105B8
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

		// Token: 0x06000893 RID: 2195 RVA: 0x00012438 File Offset: 0x00010638
		internal override void Validate()
		{
			base.Validate();
			if (!this.ValidateSufficientFacets())
			{
				return;
			}
			this.ValidateInterFacetConsistency();
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00012450 File Offset: 0x00010650
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

		// Token: 0x06000895 RID: 2197 RVA: 0x000124E4 File Offset: 0x000106E4
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

		// Token: 0x040006D1 RID: 1745
		private PrimitiveType _primitiveType = new PrimitiveType();

		// Token: 0x040006D2 RID: 1746
		private List<FacetDescriptionElement> _facetDescriptions = new List<FacetDescriptionElement>();
	}
}
