using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000188 RID: 392
	internal class CsdlOnDelete : CsdlElementWithDocumentation
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00011A3E File Offset: 0x0000FC3E
		public CsdlOnDelete(EdmOnDeleteAction action, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.action = action;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00011A4F File Offset: 0x0000FC4F
		public EdmOnDeleteAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x040003D2 RID: 978
		private readonly EdmOnDeleteAction action;
	}
}
