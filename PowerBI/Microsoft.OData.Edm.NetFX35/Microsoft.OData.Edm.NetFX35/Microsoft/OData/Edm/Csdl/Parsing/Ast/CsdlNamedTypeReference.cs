using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200003C RID: 60
	internal class CsdlNamedTypeReference : CsdlTypeReference
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000396B File Offset: 0x00001B6B
		public CsdlNamedTypeReference(string fullName, bool isNullable, CsdlLocation location)
			: base(isNullable, location)
		{
			this.fullName = fullName;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000397C File Offset: 0x00001B7C
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x0400005B RID: 91
		private readonly string fullName;
	}
}
