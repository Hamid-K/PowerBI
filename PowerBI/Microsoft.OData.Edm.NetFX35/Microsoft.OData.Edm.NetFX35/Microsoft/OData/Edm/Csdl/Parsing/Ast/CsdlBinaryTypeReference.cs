using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000178 RID: 376
	internal class CsdlBinaryTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x0001171C File Offset: 0x0000F91C
		public CsdlBinaryTypeReference(bool isUnbounded, int? maxLength, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.Binary, typeName, isNullable, location)
		{
			this.isUnbounded = isUnbounded;
			this.maxLength = maxLength;
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00011738 File Offset: 0x0000F938
		public bool IsUnbounded
		{
			get
			{
				return this.isUnbounded;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00011740 File Offset: 0x0000F940
		public int? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x040003B0 RID: 944
		private readonly bool isUnbounded;

		// Token: 0x040003B1 RID: 945
		private readonly int? maxLength;
	}
}
