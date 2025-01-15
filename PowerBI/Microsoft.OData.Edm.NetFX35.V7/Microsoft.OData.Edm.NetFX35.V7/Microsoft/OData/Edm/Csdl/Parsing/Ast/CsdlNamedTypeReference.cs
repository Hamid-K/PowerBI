using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001EE RID: 494
	internal class CsdlNamedTypeReference : CsdlTypeReference
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x00024128 File Offset: 0x00022328
		public CsdlNamedTypeReference(string fullName, bool isNullable, CsdlLocation location)
			: this(false, default(int?), default(bool?), default(int?), default(int?), default(int?), fullName, isNullable, location)
		{
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002416C File Offset: 0x0002236C
		public CsdlNamedTypeReference(bool isUnbounded, int? maxLength, bool? isUnicode, int? precision, int? scale, int? spatialReferenceIdentifier, string fullName, bool isNullable, CsdlLocation location)
			: base(isNullable, location)
		{
			this.IsUnbounded = isUnbounded;
			this.MaxLength = maxLength;
			this.IsUnicode = isUnicode;
			this.Precision = precision;
			this.Scale = scale;
			this.SpatialReferenceIdentifier = spatialReferenceIdentifier;
			this.FullName = fullName;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x000241B8 File Offset: 0x000223B8
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x000241C0 File Offset: 0x000223C0
		public bool IsUnbounded { get; protected set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x000241C9 File Offset: 0x000223C9
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x000241D1 File Offset: 0x000223D1
		public int? MaxLength { get; protected set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x000241DA File Offset: 0x000223DA
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x000241E2 File Offset: 0x000223E2
		public bool? IsUnicode { get; protected set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x000241EB File Offset: 0x000223EB
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x000241F3 File Offset: 0x000223F3
		public int? Precision { get; protected set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x000241FC File Offset: 0x000223FC
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00024204 File Offset: 0x00022404
		public int? Scale { get; protected set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0002420D File Offset: 0x0002240D
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00024215 File Offset: 0x00022415
		public int? SpatialReferenceIdentifier { get; protected set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0002421E File Offset: 0x0002241E
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x00024226 File Offset: 0x00022426
		public string FullName { get; protected set; }
	}
}
