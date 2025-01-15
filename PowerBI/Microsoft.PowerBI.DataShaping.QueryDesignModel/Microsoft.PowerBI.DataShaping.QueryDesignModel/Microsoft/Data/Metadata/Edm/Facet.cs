using System;
using System.Data;
using System.Diagnostics;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200008F RID: 143
	[DebuggerDisplay("{Name,nq}={Value}")]
	public sealed class Facet : MetadataItem
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x00018562 File Offset: 0x00016762
		private Facet(FacetDescription facetDescription, object value)
			: base(MetadataItem.MetadataFlags.Readonly)
		{
			EntityUtil.GenericCheckArgumentNull<FacetDescription>(facetDescription, "facetDescription");
			this._facetDescription = facetDescription;
			this._value = value;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00018585 File Offset: 0x00016785
		internal static Facet Create(FacetDescription facetDescription, object value)
		{
			return Facet.Create(facetDescription, value, false);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00018590 File Offset: 0x00016790
		internal static Facet Create(FacetDescription facetDescription, object value, bool bypassKnownValues)
		{
			EntityUtil.CheckArgumentNull<FacetDescription>(facetDescription, "facetDescription");
			if (!bypassKnownValues)
			{
				if (value == null)
				{
					return facetDescription.NullValueFacet;
				}
				if (object.Equals(facetDescription.DefaultValue, value))
				{
					return facetDescription.DefaultValueFacet;
				}
				if (facetDescription.FacetType.Identity == "Edm.Boolean")
				{
					bool flag = (bool)value;
					return facetDescription.GetBooleanFacet(flag);
				}
			}
			Facet facet = new Facet(facetDescription, value);
			if (value != null && !Helper.IsUnboundedFacetValue(facet) && facet.FacetType.ClrType != null)
			{
				value.GetType();
			}
			return facet;
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001861F File Offset: 0x0001681F
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.Facet;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00018623 File Offset: 0x00016823
		public FacetDescription Description
		{
			get
			{
				return this._facetDescription;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0001862B File Offset: 0x0001682B
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._facetDescription.FacetName;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00018638 File Offset: 0x00016838
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public EdmType FacetType
		{
			get
			{
				return this._facetDescription.FacetType;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00018645 File Offset: 0x00016845
		[MetadataProperty(typeof(object), false)]
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0001864D File Offset: 0x0001684D
		internal override string Identity
		{
			get
			{
				return this._facetDescription.FacetName;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0001865A File Offset: 0x0001685A
		public bool IsUnbounded
		{
			get
			{
				return this.Value == EdmConstants.UnboundedValue;
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00018669 File Offset: 0x00016869
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000834 RID: 2100
		private readonly FacetDescription _facetDescription;

		// Token: 0x04000835 RID: 2101
		private readonly object _value;
	}
}
