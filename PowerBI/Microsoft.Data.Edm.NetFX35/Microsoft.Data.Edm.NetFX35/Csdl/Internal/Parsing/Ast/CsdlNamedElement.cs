using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x0200000E RID: 14
	internal abstract class CsdlNamedElement : CsdlElementWithDocumentation
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002A96 File Offset: 0x00000C96
		protected CsdlNamedElement(string name, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.name = name;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002AA7 File Offset: 0x00000CA7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000014 RID: 20
		private readonly string name;
	}
}
