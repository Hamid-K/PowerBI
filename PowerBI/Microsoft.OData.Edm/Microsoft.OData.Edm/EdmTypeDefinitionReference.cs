using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000036 RID: 54
	public class EdmTypeDefinitionReference : EdmTypeReference, IEdmTypeDefinitionReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00003B20 File Offset: 0x00001D20
		public EdmTypeDefinitionReference(IEdmTypeDefinition typeDefinition, bool isNullable)
			: base(typeDefinition, isNullable)
		{
			this.IsUnbounded = false;
			this.MaxLength = null;
			this.IsUnicode = EdmTypeDefinitionReference.ComputeDefaultIsUnicode(typeDefinition);
			this.Precision = EdmTypeDefinitionReference.ComputeDefaultPrecision(typeDefinition);
			this.Scale = EdmTypeDefinitionReference.ComputeDefaultScale(typeDefinition);
			this.SpatialReferenceIdentifier = EdmTypeDefinitionReference.ComputeSrid(typeDefinition);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003B7B File Offset: 0x00001D7B
		public EdmTypeDefinitionReference(IEdmTypeDefinition typeDefinition, bool isNullable, bool isUnbounded, int? maxLength, bool? isUnicode, int? precision, int? scale, int? spatialReferenceIdentifier)
			: base(typeDefinition, isNullable)
		{
			this.IsUnbounded = isUnbounded;
			this.MaxLength = maxLength;
			this.IsUnicode = isUnicode;
			this.Precision = precision;
			this.Scale = scale;
			this.SpatialReferenceIdentifier = spatialReferenceIdentifier;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003BB4 File Offset: 0x00001DB4
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00003BBC File Offset: 0x00001DBC
		public bool IsUnbounded { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003BC5 File Offset: 0x00001DC5
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00003BCD File Offset: 0x00001DCD
		public int? MaxLength { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003BD6 File Offset: 0x00001DD6
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00003BDE File Offset: 0x00001DDE
		public bool? IsUnicode { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003BE7 File Offset: 0x00001DE7
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00003BEF File Offset: 0x00001DEF
		public int? Precision { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00003BF8 File Offset: 0x00001DF8
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00003C00 File Offset: 0x00001E00
		public int? Scale { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003C09 File Offset: 0x00001E09
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00003C11 File Offset: 0x00001E11
		public int? SpatialReferenceIdentifier { get; private set; }

		// Token: 0x0600010C RID: 268 RVA: 0x00003C1C File Offset: 0x00001E1C
		private static bool? ComputeDefaultIsUnicode(IEdmTypeDefinition typeDefinition)
		{
			if (typeDefinition.UnderlyingType.IsString())
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003C48 File Offset: 0x00001E48
		private static int? ComputeDefaultPrecision(IEdmTypeDefinition typeDefinition)
		{
			if (typeDefinition.UnderlyingType.IsTemporal())
			{
				return new int?(0);
			}
			return null;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003C74 File Offset: 0x00001E74
		private static int? ComputeDefaultScale(IEdmTypeDefinition typeDefinition)
		{
			if (typeDefinition.UnderlyingType.IsDecimal())
			{
				return new int?(0);
			}
			return null;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003CA0 File Offset: 0x00001EA0
		private static int? ComputeSrid(IEdmTypeDefinition typeDefinition)
		{
			if (typeDefinition.UnderlyingType.IsGeography())
			{
				return new int?(4326);
			}
			if (typeDefinition.UnderlyingType.IsGeometry())
			{
				return new int?(0);
			}
			return null;
		}
	}
}
