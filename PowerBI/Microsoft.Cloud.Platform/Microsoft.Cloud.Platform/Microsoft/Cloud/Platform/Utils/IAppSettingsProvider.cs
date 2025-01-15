using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002D8 RID: 728
	internal interface IAppSettingsProvider : IIdentifiable
	{
		// Token: 0x06001378 RID: 4984
		NameValueDictionary GetAppSettings();
	}
}
