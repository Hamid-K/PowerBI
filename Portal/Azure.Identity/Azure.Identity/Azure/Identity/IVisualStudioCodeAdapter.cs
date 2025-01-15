using System;

namespace Azure.Identity
{
	// Token: 0x02000069 RID: 105
	internal interface IVisualStudioCodeAdapter
	{
		// Token: 0x060003A2 RID: 930
		string GetUserSettingsPath();

		// Token: 0x060003A3 RID: 931
		string GetCredentials(string serviceName, string accountName);
	}
}
