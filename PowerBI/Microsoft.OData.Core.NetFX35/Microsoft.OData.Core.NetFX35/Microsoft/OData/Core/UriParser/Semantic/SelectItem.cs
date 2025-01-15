using System;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200023D RID: 573
	public abstract class SelectItem : ODataAnnotatable
	{
		// Token: 0x0600147E RID: 5246
		public abstract T TranslateWith<T>(SelectItemTranslator<T> translator);

		// Token: 0x0600147F RID: 5247
		public abstract void HandleWith(SelectItemHandler handler);
	}
}
