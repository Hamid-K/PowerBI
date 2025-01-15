using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000074 RID: 116
	public class EdmSpatialTypeReference : EdmPrimitiveTypeReference, IEdmSpatialTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x0000C18C File Offset: 0x0000A38C
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

		// Token: 0x0600040D RID: 1037 RVA: 0x0000C205 File Offset: 0x0000A405
		public EdmSpatialTypeReference(IEdmPrimitiveType definition, bool isNullable, int? spatialReferenceIdentifier)
			: base(definition, isNullable)
		{
			this.spatialReferenceIdentifier = spatialReferenceIdentifier;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000C216 File Offset: 0x0000A416
		public int? SpatialReferenceIdentifier
		{
			get
			{
				return this.spatialReferenceIdentifier;
			}
		}

		// Token: 0x04000100 RID: 256
		private readonly int? spatialReferenceIdentifier;
	}
}
