using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B4 RID: 436
	public sealed class WildcardSelectItem : SelectItem
	{
		// Token: 0x0600147C RID: 5244 RVA: 0x0003BC27 File Offset: 0x00039E27
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0003BC30 File Offset: 0x00039E30
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}
