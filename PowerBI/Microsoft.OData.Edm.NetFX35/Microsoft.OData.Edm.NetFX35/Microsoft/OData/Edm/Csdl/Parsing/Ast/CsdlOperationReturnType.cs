using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020000D1 RID: 209
	internal class CsdlOperationReturnType : CsdlElement
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x000089D2 File Offset: 0x00006BD2
		public CsdlOperationReturnType(CsdlTypeReference returnType, CsdlLocation location)
			: base(location)
		{
			this.returnType = returnType;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000089E2 File Offset: 0x00006BE2
		public CsdlTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x0400018C RID: 396
		private readonly CsdlTypeReference returnType;
	}
}
