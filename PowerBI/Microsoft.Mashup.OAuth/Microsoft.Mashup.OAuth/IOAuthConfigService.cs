using System;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000012 RID: 18
	public interface IOAuthConfigService
	{
		// Token: 0x06000082 RID: 130
		bool TryLookupConfigValue(string key, out object value);
	}
}
