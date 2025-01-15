using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F9 RID: 505
	internal abstract class CsdlNamedElement : CsdlElement
	{
		// Token: 0x06000DB6 RID: 3510 RVA: 0x00026205 File Offset: 0x00024405
		protected CsdlNamedElement(string name, CsdlLocation location)
			: base(location)
		{
			this.name = name;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00026215 File Offset: 0x00024415
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000788 RID: 1928
		private readonly string name;
	}
}
