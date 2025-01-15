using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A6 RID: 422
	public abstract class SelectItem
	{
		// Token: 0x06001421 RID: 5153
		public abstract T TranslateWith<T>(SelectItemTranslator<T> translator);

		// Token: 0x06001422 RID: 5154
		public abstract void HandleWith(SelectItemHandler handler);
	}
}
