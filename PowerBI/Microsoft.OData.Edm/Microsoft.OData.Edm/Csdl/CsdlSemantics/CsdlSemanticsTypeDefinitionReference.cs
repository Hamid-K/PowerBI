using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000160 RID: 352
	internal class CsdlSemanticsTypeDefinitionReference : CsdlSemanticsNamedTypeReference, IEdmTypeDefinitionReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x0001B064 File Offset: 0x00019264
		public CsdlSemanticsTypeDefinitionReference(CsdlSemanticsSchema schema, CsdlNamedTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0001B0BB File Offset: 0x000192BB
		public bool IsUnbounded
		{
			get
			{
				return this.isUnboundedCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputeIsUnboundedFunc, null);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001B0CF File Offset: 0x000192CF
		public int? MaxLength
		{
			get
			{
				return this.maxLengthCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputeMaxLengthFunc, null);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0001B0E3 File Offset: 0x000192E3
		public bool? IsUnicode
		{
			get
			{
				return this.isUnicodeCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputeIsUnicodeFunc, null);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0001B0F7 File Offset: 0x000192F7
		public int? Precision
		{
			get
			{
				return this.precisionCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputePrecisionFunc, null);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0001B10B File Offset: 0x0001930B
		public int? Scale
		{
			get
			{
				return this.scaleCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputeScaleFunc, null);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0001B11F File Offset: 0x0001931F
		public int? SpatialReferenceIdentifier
		{
			get
			{
				return this.sridCache.GetValue(this, CsdlSemanticsTypeDefinitionReference.ComputeSridFunc, null);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0001B133 File Offset: 0x00019333
		private CsdlNamedTypeReference Reference
		{
			get
			{
				return (CsdlNamedTypeReference)this.Element;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001B140 File Offset: 0x00019340
		private bool ComputeIsUnbounded()
		{
			return this.UnderlyingType().CanSpecifyMaxLength() && this.Reference.IsUnbounded;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001B15C File Offset: 0x0001935C
		private int? ComputeMaxLength()
		{
			if (!this.UnderlyingType().CanSpecifyMaxLength())
			{
				return null;
			}
			return this.Reference.MaxLength;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001B18C File Offset: 0x0001938C
		private bool? ComputeIsUnicode()
		{
			if (!this.UnderlyingType().IsString())
			{
				return null;
			}
			return this.Reference.IsUnicode;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001B1BC File Offset: 0x000193BC
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
			return null;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001B220 File Offset: 0x00019420
		private int? ComputeScale()
		{
			if (!this.UnderlyingType().IsDecimal())
			{
				return null;
			}
			return this.Reference.Scale;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001B250 File Offset: 0x00019450
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
			return null;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001B294 File Offset: 0x00019494
		private int? DefaultSridIfUnspecified(int defaultSrid)
		{
			if (this.Reference.SpatialReferenceIdentifier != -2147483648)
			{
				return this.Reference.SpatialReferenceIdentifier;
			}
			return new int?(defaultSrid);
		}

		// Token: 0x040005D0 RID: 1488
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, bool> ComputeIsUnboundedFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputeIsUnbounded();

		// Token: 0x040005D1 RID: 1489
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, int?> ComputeMaxLengthFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputeMaxLength();

		// Token: 0x040005D2 RID: 1490
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, bool?> ComputeIsUnicodeFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputeIsUnicode();

		// Token: 0x040005D3 RID: 1491
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, int?> ComputePrecisionFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputePrecision();

		// Token: 0x040005D4 RID: 1492
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, int?> ComputeScaleFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputeScale();

		// Token: 0x040005D5 RID: 1493
		private static readonly Func<CsdlSemanticsTypeDefinitionReference, int?> ComputeSridFunc = (CsdlSemanticsTypeDefinitionReference me) => me.ComputeSrid();

		// Token: 0x040005D6 RID: 1494
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, bool> isUnboundedCache = new Cache<CsdlSemanticsTypeDefinitionReference, bool>();

		// Token: 0x040005D7 RID: 1495
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, int?> maxLengthCache = new Cache<CsdlSemanticsTypeDefinitionReference, int?>();

		// Token: 0x040005D8 RID: 1496
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, bool?> isUnicodeCache = new Cache<CsdlSemanticsTypeDefinitionReference, bool?>();

		// Token: 0x040005D9 RID: 1497
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, int?> precisionCache = new Cache<CsdlSemanticsTypeDefinitionReference, int?>();

		// Token: 0x040005DA RID: 1498
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, int?> scaleCache = new Cache<CsdlSemanticsTypeDefinitionReference, int?>();

		// Token: 0x040005DB RID: 1499
		private readonly Cache<CsdlSemanticsTypeDefinitionReference, int?> sridCache = new Cache<CsdlSemanticsTypeDefinitionReference, int?>();
	}
}
