using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001FB RID: 507
	internal abstract class CsdlTypeReference : CsdlElement
	{
		// Token: 0x06000D48 RID: 3400 RVA: 0x000244FC File Offset: 0x000226FC
		protected CsdlTypeReference(bool isNullable, CsdlLocation location)
			: base(location)
		{
			this.isNullable = isNullable;
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0002450C File Offset: 0x0002270C
		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
		}

		// Token: 0x0400073B RID: 1851
		private readonly bool isNullable;
	}
}
