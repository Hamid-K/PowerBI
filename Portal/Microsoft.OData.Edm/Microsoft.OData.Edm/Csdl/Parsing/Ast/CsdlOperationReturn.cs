using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E8 RID: 488
	internal class CsdlOperationReturn : CsdlElement
	{
		// Token: 0x06000D76 RID: 3446 RVA: 0x00025E09 File Offset: 0x00024009
		public CsdlOperationReturn(CsdlTypeReference returnType, CsdlLocation location)
			: base(location)
		{
			this.returnType = returnType;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00025E19 File Offset: 0x00024019
		public CsdlTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x0400076A RID: 1898
		private readonly CsdlTypeReference returnType;
	}
}
