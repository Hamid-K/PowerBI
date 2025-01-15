using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200003A RID: 58
	internal class CsdlSingleton : CsdlAbstractNavigationSource
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00003936 File Offset: 0x00001B36
		public CsdlSingleton(string name, string type, IEnumerable<CsdlNavigationPropertyBinding> navigationPropertyBindings, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, navigationPropertyBindings, documentation, location)
		{
			this.type = type;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000394B File Offset: 0x00001B4B
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000059 RID: 89
		private readonly string type;
	}
}
