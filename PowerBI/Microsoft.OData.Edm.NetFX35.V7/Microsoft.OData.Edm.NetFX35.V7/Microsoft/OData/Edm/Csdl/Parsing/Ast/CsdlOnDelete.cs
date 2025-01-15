using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F1 RID: 497
	internal class CsdlOnDelete : CsdlElementWithDocumentation
	{
		// Token: 0x06000D28 RID: 3368 RVA: 0x000242DB File Offset: 0x000224DB
		public CsdlOnDelete(EdmOnDeleteAction action, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.action = action;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x000242EC File Offset: 0x000224EC
		public EdmOnDeleteAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x04000726 RID: 1830
		private readonly EdmOnDeleteAction action;
	}
}
