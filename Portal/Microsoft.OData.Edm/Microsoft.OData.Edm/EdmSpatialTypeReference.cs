using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BF RID: 191
	public class EdmSpatialTypeReference : EdmPrimitiveTypeReference, IEdmSpatialTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x0000BD08 File Offset: 0x00009F08
		public EdmSpatialTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, null)
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

		// Token: 0x0600049F RID: 1183 RVA: 0x0000BD81 File Offset: 0x00009F81
		public EdmSpatialTypeReference(IEdmPrimitiveType definition, bool isNullable, int? spatialReferenceIdentifier)
			: base(definition, isNullable)
		{
			this.spatialReferenceIdentifier = spatialReferenceIdentifier;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000BD92 File Offset: 0x00009F92
		public int? SpatialReferenceIdentifier
		{
			get
			{
				return this.spatialReferenceIdentifier;
			}
		}

		// Token: 0x0400016F RID: 367
		private readonly int? spatialReferenceIdentifier;
	}
}
