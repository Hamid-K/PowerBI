using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000166 RID: 358
	public sealed class WildcardSelectItem : SelectItem
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x0002BB6B File Offset: 0x00029D6B
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0002BB74 File Offset: 0x00029D74
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}
