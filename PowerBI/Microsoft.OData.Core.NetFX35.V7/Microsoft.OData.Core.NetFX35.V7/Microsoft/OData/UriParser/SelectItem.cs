using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015A RID: 346
	public abstract class SelectItem
	{
		// Token: 0x06000EFC RID: 3836
		public abstract T TranslateWith<T>(SelectItemTranslator<T> translator);

		// Token: 0x06000EFD RID: 3837
		public abstract void HandleWith(SelectItemHandler handler);
	}
}
