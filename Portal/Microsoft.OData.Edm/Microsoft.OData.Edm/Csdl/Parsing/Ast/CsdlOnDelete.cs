using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001FE RID: 510
	internal class CsdlOnDelete : CsdlElement
	{
		// Token: 0x06000DD7 RID: 3543 RVA: 0x00026417 File Offset: 0x00024617
		public CsdlOnDelete(EdmOnDeleteAction action, CsdlLocation location)
			: base(location)
		{
			this.action = action;
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00026427 File Offset: 0x00024627
		public EdmOnDeleteAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x0400079C RID: 1948
		private readonly EdmOnDeleteAction action;
	}
}
