using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000183 RID: 387
	internal class CsdlOperationParameter : CsdlNamedElement
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x00011907 File Offset: 0x0000FB07
		public CsdlOperationParameter(string name, CsdlTypeReference type, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.type = type;
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001191A File Offset: 0x0000FB1A
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040003C5 RID: 965
		private readonly CsdlTypeReference type;
	}
}
