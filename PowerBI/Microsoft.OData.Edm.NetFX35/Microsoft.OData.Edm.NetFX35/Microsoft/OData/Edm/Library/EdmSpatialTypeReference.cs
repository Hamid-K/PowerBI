using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000144 RID: 324
	public class EdmSpatialTypeReference : EdmPrimitiveTypeReference, IEdmSpatialTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		public EdmSpatialTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, default(int?))
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveType>(definition, "definition");
			switch (definition.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				this.spatialReferenceIdentifier = new int?(4326);
				return;
			default:
				this.spatialReferenceIdentifier = new int?(0);
				return;
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000E849 File Offset: 0x0000CA49
		public EdmSpatialTypeReference(IEdmPrimitiveType definition, bool isNullable, int? spatialReferenceIdentifier)
			: base(definition, isNullable)
		{
			this.spatialReferenceIdentifier = spatialReferenceIdentifier;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0000E85A File Offset: 0x0000CA5A
		public int? SpatialReferenceIdentifier
		{
			get
			{
				return this.spatialReferenceIdentifier;
			}
		}

		// Token: 0x04000251 RID: 593
		private readonly int? spatialReferenceIdentifier;
	}
}
