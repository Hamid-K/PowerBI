using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000021 RID: 33
	internal class CsdlNamedTypeReference : CsdlTypeReference
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00002DBD File Offset: 0x00000FBD
		public CsdlNamedTypeReference(string fullName, bool isNullable, CsdlLocation location)
			: base(isNullable, location)
		{
			this.fullName = fullName;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002DCE File Offset: 0x00000FCE
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x04000033 RID: 51
		private readonly string fullName;
	}
}
