using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001EC RID: 492
	internal abstract class CsdlNamedElement : CsdlElementWithDocumentation
	{
		// Token: 0x06000D07 RID: 3335 RVA: 0x000240C2 File Offset: 0x000222C2
		protected CsdlNamedElement(string name, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.name = name;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x000240D3 File Offset: 0x000222D3
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000712 RID: 1810
		private readonly string name;
	}
}
