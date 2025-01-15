using System;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200026B RID: 619
	public sealed class WildcardSelectItem : SelectItem
	{
		// Token: 0x060015BA RID: 5562 RVA: 0x0004BF9B File Offset: 0x0004A19B
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0004BFA4 File Offset: 0x0004A1A4
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}
