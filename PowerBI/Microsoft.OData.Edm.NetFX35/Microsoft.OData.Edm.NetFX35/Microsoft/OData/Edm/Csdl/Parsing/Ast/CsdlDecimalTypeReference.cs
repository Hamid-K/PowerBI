using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200017C RID: 380
	internal class CsdlDecimalTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x000117C4 File Offset: 0x0000F9C4
		public CsdlDecimalTypeReference(int? precision, int? scale, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.Decimal, typeName, isNullable, location)
		{
			this.precision = precision;
			this.scale = scale;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x000117E0 File Offset: 0x0000F9E0
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000117E8 File Offset: 0x0000F9E8
		public int? Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x040003B7 RID: 951
		private readonly int? precision;

		// Token: 0x040003B8 RID: 952
		private readonly int? scale;
	}
}
