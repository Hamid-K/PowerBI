using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A0 RID: 416
	internal class CsdlSemanticsTypeDefinitionReference : CsdlSemanticsNamedTypeReference, IEdmTypeDefinitionReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000B55 RID: 2901 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
		public CsdlSemanticsTypeDefinitionReference(CsdlSemanticsSchema schema, CsdlNamedTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0001F73B File Offset: 0x0001D93B
		public bool IsUnbounded
		{
			get
			{
				return this.isUnboundedCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputeIsUnboundedFunc, null);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001F74F File Offset: 0x0001D94F
		public int? MaxLength
		{
			get
			{
				return this.maxLengthCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputeMaxLengthFunc, null);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0001F763 File Offset: 0x0001D963
		public bool? IsUnicode
		{
			get
			{
				return this.isUnicodeCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputeIsUnicodeFunc, null);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0001F777 File Offset: 0x0001D977
		public int? Precision
		{
			get
			{
				return this.precisionCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputePrecisionFunc, null);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0001F78B File Offset: 0x0001D98B
		public int? Scale
		{
			get
			{
				return this.scaleCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputeScaleFunc, null);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0001F79F File Offset: 0x0001D99F
		public int? SpatialReferenceIdentifier
		{
			get
			{
				return this.sridCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputeSridFunc, null);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0001F7B3 File Offset: 0x0001D9B3
		private CsdlNamedTypeReference Reference
		{
			get
			{
				return (CsdlNamedTypeReference)this.Element;
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0001F7C0 File Offset: 0x0001D9C0
		private bool ComputeIsUnbounded()
		{
			return this.UnderlyingType().CanSpecifyMaxLength() && this.Reference.IsUnbounded;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001F7DC File Offset: 0x0001D9DC
		private int? ComputeMaxLength()
		{
			if (!this.UnderlyingType().CanSpecifyMaxLength())
			{
				return default(int?);
			}
			return this.Reference.MaxLength;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001F80C File Offset: 0x0001DA0C
		private bool? ComputeIsUnicode()
		{
			if (!this.UnderlyingType().IsString())
			{
				return default(bool?);
			}
			return this.Reference.IsUnicode;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0001F83C File Offset: 0x0001DA3C
		private int? ComputePrecision()
		{
			if (this.UnderlyingType().IsDecimal())
			{
				return this.Reference.Precision;
			}
			if (this.UnderlyingType().IsTemporal())
			{
				return new int?(this.Reference.Precision ?? 0);
			}
			return default(int?);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001F8A0 File Offset: 0x0001DAA0
		private int? ComputeScale()
		{
			if (!this.UnderlyingType().IsDecimal())
			{
				return default(int?);
			}
			return this.Reference.Scale;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
		private int? ComputeSrid()
		{
			if (this.UnderlyingType().IsGeography())
			{
				return this.DefaultSridIfUnspecified(4326);
			}
			if (this.UnderlyingType().IsGeometry())
			{
				return this.DefaultSridIfUnspecified(0);
			}
			return default(int?);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001F914 File Offset: 0x0001DB14
		private int? DefaultSridIfUnspecified(int defaultSrid)
		{
			if (this.Reference.SpatialReferenceIdentifier != -2147483648)
			{
				return this.Reference.SpatialReferenceIdentifier;
			}
			return new int?(defaultSrid);
		}

		// Token: 0x04000683 RID: 1667
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, bool> ComputeIsUnboundedFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputeIsUnbounded();

		// Token: 0x04000684 RID: 1668
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, int?> ComputeMaxLengthFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputeMaxLength();

		// Token: 0x04000685 RID: 1669
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, bool?> ComputeIsUnicodeFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputeIsUnicode();

		// Token: 0x04000686 RID: 1670
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, int?> ComputePrecisionFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputePrecision();

		// Token: 0x04000687 RID: 1671
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, int?> ComputeScaleFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputeScale();

		// Token: 0x04000688 RID: 1672
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, int?> ComputeSridFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputeSrid();

		// Token: 0x04000689 RID: 1673
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, bool> isUnboundedCache = new Cache<CsdlSemanticsTypeDefinitionReference, bool>();

		// Token: 0x0400068A RID: 1674
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, int?> maxLengthCache = new Cache<CsdlSemanticsTypeDefinitionReference, int?>();

		// Token: 0x0400068B RID: 1675
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, bool?> isUnicodeCache = new Cache<CsdlSemanticsTypeDefinitionReference, bool?>();

		// Token: 0x0400068C RID: 1676
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, int?> precisionCache = new Cache<CsdlSemanticsTypeDefinitionReference, int?>();

		// Token: 0x0400068D RID: 1677
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, int?> scaleCache = new Cache<CsdlSemanticsTypeDefinitionReference, int?>();

		// Token: 0x0400068E RID: 1678
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, int?> sridCache = new Cache<CsdlSemanticsTypeDefinitionReference, int?>();
	}
}
