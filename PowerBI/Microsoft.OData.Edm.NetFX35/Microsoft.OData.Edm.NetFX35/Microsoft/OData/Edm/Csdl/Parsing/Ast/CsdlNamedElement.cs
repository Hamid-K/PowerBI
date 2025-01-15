using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200001D RID: 29
	internal abstract class CsdlNamedElement : CsdlElementWithDocumentation
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003445 File Offset: 0x00001645
		protected CsdlNamedElement(string name, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.name = name;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003456 File Offset: 0x00001656
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400002C RID: 44
		private readonly string name;
	}
}
