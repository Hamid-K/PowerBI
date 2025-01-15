using System;
using System.Data.Entity.Utilities;
using System.Diagnostics;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C0 RID: 1216
	[DebuggerDisplay("{Name,nq}={Value}")]
	public class Facet : MetadataItem
	{
		// Token: 0x06003C15 RID: 15381 RVA: 0x000C71C3 File Offset: 0x000C53C3
		internal Facet()
		{
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x000C71CB File Offset: 0x000C53CB
		private Facet(FacetDescription facetDescription, object value)
			: base(MetadataItem.MetadataFlags.Readonly)
		{
			Check.NotNull<FacetDescription>(facetDescription, "facetDescription");
			this._facetDescription = facetDescription;
			this._value = value;
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x000C71EE File Offset: 0x000C53EE
		internal static Facet Create(FacetDescription facetDescription, object value)
		{
			return Facet.Create(facetDescription, value, false);
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x000C71F8 File Offset: 0x000C53F8
		internal static Facet Create(FacetDescription facetDescription, object value, bool bypassKnownValues)
		{
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
			if (value != null && !Helper.IsUnboundedFacetValue(facet) && !Helper.IsVariableFacetValue(facet) && facet.FacetType.ClrType != null)
			{
				value.GetType();
			}
			return facet;
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003C19 RID: 15385 RVA: 0x000C7283 File Offset: 0x000C5483
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.Facet;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003C1A RID: 15386 RVA: 0x000C7287 File Offset: 0x000C5487
		public FacetDescription Description
		{
			get
			{
				return this._facetDescription;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000C728F File Offset: 0x000C548F
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string Name
		{
			get
			{
				return this._facetDescription.FacetName;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x000C729C File Offset: 0x000C549C
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public EdmType FacetType
		{
			get
			{
				return this._facetDescription.FacetType;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x000C72A9 File Offset: 0x000C54A9
		[MetadataProperty(typeof(object), false)]
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000C72B1 File Offset: 0x000C54B1
		internal override string Identity
		{
			get
			{
				return this._facetDescription.FacetName;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x000C72BE File Offset: 0x000C54BE
		public bool IsUnbounded
		{
			get
			{
				return this.Value == EdmConstants.UnboundedValue;
			}
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x000C72CD File Offset: 0x000C54CD
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x040014AC RID: 5292
		private readonly FacetDescription _facetDescription;

		// Token: 0x040014AD RID: 5293
		private readonly object _value;
	}
}
