using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000133 RID: 307
	internal class CsdlBinaryTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x0000F41F File Offset: 0x0000D61F
		public CsdlBinaryTypeReference(bool? isFixedLength, bool isUnbounded, int? maxLength, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.Binary, typeName, isNullable, location)
		{
			this.isFixedLength = isFixedLength;
			this.isUnbounded = isUnbounded;
			this.maxLength = maxLength;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000F443 File Offset: 0x0000D643
		public bool? IsFixedLength
		{
			get
			{
				return this.isFixedLength;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000F44B File Offset: 0x0000D64B
		public bool IsUnbounded
		{
			get
			{
				return this.isUnbounded;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000F453 File Offset: 0x0000D653
		public int? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x0400031A RID: 794
		private readonly bool? isFixedLength;

		// Token: 0x0400031B RID: 795
		private readonly bool isUnbounded;

		// Token: 0x0400031C RID: 796
		private readonly int? maxLength;
	}
}
