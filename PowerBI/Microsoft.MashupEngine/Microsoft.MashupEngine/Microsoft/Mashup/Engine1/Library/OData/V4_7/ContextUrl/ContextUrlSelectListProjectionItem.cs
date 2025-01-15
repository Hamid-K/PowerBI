using System;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl
{
	// Token: 0x0200081A RID: 2074
	internal sealed class ContextUrlSelectListProjectionItem : ContextUrlSelectListItem
	{
		// Token: 0x06003BEB RID: 15339 RVA: 0x000C28DC File Offset: 0x000C0ADC
		public ContextUrlSelectListProjectionItem(EdmPathExpression selectedPath)
		{
			this.selectedPath = selectedPath;
		}

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06003BEC RID: 15340 RVA: 0x000023C4 File Offset: 0x000005C4
		public override SelectListItemKind Kind
		{
			get
			{
				return SelectListItemKind.Projection;
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06003BED RID: 15341 RVA: 0x000C28EB File Offset: 0x000C0AEB
		public EdmPathExpression SelectedPath
		{
			get
			{
				return this.selectedPath;
			}
		}

		// Token: 0x04001F36 RID: 7990
		private readonly EdmPathExpression selectedPath;
	}
}
