using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000208 RID: 520
	internal abstract class CsdlTypeReference : CsdlElement
	{
		// Token: 0x06000DF7 RID: 3575 RVA: 0x0002662C File Offset: 0x0002482C
		protected CsdlTypeReference(bool isNullable, CsdlLocation location)
			: base(location)
		{
			this.isNullable = isNullable;
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0002663C File Offset: 0x0002483C
		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
		}

		// Token: 0x040007B1 RID: 1969
		private readonly bool isNullable;
	}
}
