using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200018F RID: 399
	internal class CsdlTemporalTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x00011C34 File Offset: 0x0000FE34
		public CsdlTemporalTypeReference(EdmPrimitiveTypeKind kind, int? precision, string typeName, bool isNullable, CsdlLocation location)
			: base(kind, typeName, isNullable, location)
		{
			this.precision = precision;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00011C49 File Offset: 0x0000FE49
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x040003E9 RID: 1001
		private readonly int? precision;
	}
}
