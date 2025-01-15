using System;

namespace System.Web.Http.ValueProviders
{
	// Token: 0x0200003E RID: 62
	public interface IValueProvider
	{
		// Token: 0x060001B2 RID: 434
		bool ContainsPrefix(string prefix);

		// Token: 0x060001B3 RID: 435
		ValueProviderResult GetValue(string key);
	}
}
