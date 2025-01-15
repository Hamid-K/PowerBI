using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001FB RID: 507
	internal class CsdlNamedTypeReference : CsdlTypeReference
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x00026268 File Offset: 0x00024468
		public CsdlNamedTypeReference(string fullName, bool isNullable, CsdlLocation location)
			: this(false, null, null, null, null, null, fullName, isNullable, location)
		{
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000262AC File Offset: 0x000244AC
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

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000262F8 File Offset: 0x000244F8
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x00026300 File Offset: 0x00024500
		public bool IsUnbounded { get; protected set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00026309 File Offset: 0x00024509
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x00026311 File Offset: 0x00024511
		public int? MaxLength { get; protected set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0002631A File Offset: 0x0002451A
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x00026322 File Offset: 0x00024522
		public bool? IsUnicode { get; protected set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0002632B File Offset: 0x0002452B
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00026333 File Offset: 0x00024533
		public int? Precision { get; protected set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0002633C File Offset: 0x0002453C
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00026344 File Offset: 0x00024544
		public int? Scale { get; protected set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0002634D File Offset: 0x0002454D
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x00026355 File Offset: 0x00024555
		public int? SpatialReferenceIdentifier { get; protected set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0002635E File Offset: 0x0002455E
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x00026366 File Offset: 0x00024566
		public string FullName { get; protected set; }
	}
}
