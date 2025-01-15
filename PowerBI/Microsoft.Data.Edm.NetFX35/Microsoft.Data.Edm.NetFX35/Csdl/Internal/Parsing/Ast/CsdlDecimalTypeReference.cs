using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000137 RID: 311
	internal class CsdlDecimalTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0000F4C5 File Offset: 0x0000D6C5
		public CsdlDecimalTypeReference(int? precision, int? scale, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.Decimal, typeName, isNullable, location)
		{
			this.precision = precision;
			this.scale = scale;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000F4E1 File Offset: 0x0000D6E1
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0000F4E9 File Offset: 0x0000D6E9
		public int? Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x04000321 RID: 801
		private readonly int? precision;

		// Token: 0x04000322 RID: 802
		private readonly int? scale;
	}
}
