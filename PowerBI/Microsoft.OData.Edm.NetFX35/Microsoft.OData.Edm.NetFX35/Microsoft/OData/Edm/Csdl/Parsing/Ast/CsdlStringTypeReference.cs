using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200018E RID: 398
	internal class CsdlStringTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x00011BE7 File Offset: 0x0000FDE7
		public CsdlStringTypeReference(bool isUnbounded, int? maxLength, bool? isUnicode, string collation, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.String, typeName, isNullable, location)
		{
			this.isUnbounded = isUnbounded;
			this.maxLength = maxLength;
			this.isUnicode = isUnicode;
			this.collation = collation;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00011C14 File Offset: 0x0000FE14
		public bool IsUnbounded
		{
			get
			{
				return this.isUnbounded;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00011C1C File Offset: 0x0000FE1C
		public int? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00011C24 File Offset: 0x0000FE24
		public bool? IsUnicode
		{
			get
			{
				return this.isUnicode;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00011C2C File Offset: 0x0000FE2C
		public string Collation
		{
			get
			{
				return this.collation;
			}
		}

		// Token: 0x040003E5 RID: 997
		private readonly bool isUnbounded;

		// Token: 0x040003E6 RID: 998
		private readonly int? maxLength;

		// Token: 0x040003E7 RID: 999
		private readonly bool? isUnicode;

		// Token: 0x040003E8 RID: 1000
		private readonly string collation;
	}
}
