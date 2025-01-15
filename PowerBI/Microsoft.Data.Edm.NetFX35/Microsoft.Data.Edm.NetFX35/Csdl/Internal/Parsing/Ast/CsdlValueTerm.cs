using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000067 RID: 103
	internal class CsdlValueTerm : CsdlNamedElement
	{
		// Token: 0x060001AF RID: 431 RVA: 0x0000529C File Offset: 0x0000349C
		public CsdlValueTerm(string name, CsdlTypeReference type, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.type = type;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000052AF File Offset: 0x000034AF
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040000B9 RID: 185
		private readonly CsdlTypeReference type;
	}
}
