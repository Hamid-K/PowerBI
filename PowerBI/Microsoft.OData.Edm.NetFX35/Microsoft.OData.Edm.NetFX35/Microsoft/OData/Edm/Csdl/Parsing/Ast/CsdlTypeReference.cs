using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200003B RID: 59
	internal abstract class CsdlTypeReference : CsdlElement
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00003953 File Offset: 0x00001B53
		protected CsdlTypeReference(bool isNullable, CsdlLocation location)
			: base(location)
		{
			this.isNullable = isNullable;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003963 File Offset: 0x00001B63
		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
		}

		// Token: 0x0400005A RID: 90
		private readonly bool isNullable;
	}
}
