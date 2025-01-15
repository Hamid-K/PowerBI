using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007B RID: 123
	public class EdmTypeDefinitionReference : EdmTypeReference, IEdmTypeDefinitionReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x0000C574 File Offset: 0x0000A774
		public EdmTypeDefinitionReference(IEdmTypeDefinition typeDefinition, bool isNullable)
			: base(typeDefinition, isNullable)
		{
			this.IsUnbounded = false;
			this.MaxLength = default(int?);
			this.IsUnicode = EdmTypeDefinitionReference.ComputeDefaultIsUnicode(typeDefinition);
			this.Precision = EdmTypeDefinitionReference.ComputeDefaultPrecision(typeDefinition);
			this.Scale = EdmTypeDefinitionReference.ComputeDefaultScale(typeDefinition);
			this.SpatialReferenceIdentifier = EdmTypeDefinitionReference.ComputeSrid(typeDefinition);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000C5CF File Offset: 0x0000A7CF
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

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000C608 File Offset: 0x0000A808
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000C610 File Offset: 0x0000A810
		public bool IsUnbounded { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000C619 File Offset: 0x0000A819
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000C621 File Offset: 0x0000A821
		public int? MaxLength { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000C62A File Offset: 0x0000A82A
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000C632 File Offset: 0x0000A832
		public bool? IsUnicode { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000C63B File Offset: 0x0000A83B
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000C643 File Offset: 0x0000A843
		public int? Precision { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000C64C File Offset: 0x0000A84C
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000C654 File Offset: 0x0000A854
		public int? Scale { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000C65D File Offset: 0x0000A85D
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0000C665 File Offset: 0x0000A865
		public int? SpatialReferenceIdentifier { get; private set; }

		// Token: 0x06000442 RID: 1090 RVA: 0x0000C670 File Offset: 0x0000A870
		private static bool? ComputeDefaultIsUnicode(IEdmTypeDefinition typeDefinition)
		{
			if (typeDefinition.UnderlyingType.IsString())
			{
				return new bool?(true);
			}
			return default(bool?);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000C69C File Offset: 0x0000A89C
		private static int? ComputeDefaultPrecision(IEdmTypeDefinition typeDefinition)
		{
			if (typeDefinition.UnderlyingType.IsTemporal())
			{
				return new int?(0);
			}
			return default(int?);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		private static int? ComputeDefaultScale(IEdmTypeDefinition typeDefinition)
		{
			if (typeDefinition.UnderlyingType.IsDecimal())
			{
				return new int?(0);
			}
			return default(int?);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
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
			return default(int?);
		}
	}
}
