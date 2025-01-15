using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001FA RID: 506
	public class EnvironmentVariablesSwitches : IApplicationSwitchesProvider
	{
		// Token: 0x170001F4 RID: 500
		public string this[string name]
		{
			get
			{
				return Environment.GetEnvironmentVariable(name);
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000202D2 File Offset: 0x0001E4D2
		public bool GetBoolSwitch(string name, out bool specified)
		{
			return ApplicationSwitchesProviderUtil.GetBoolSwitch(this, name, out specified);
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0002EF73 File Offset: 0x0002D173
		public string Name
		{
			get
			{
				return "Environment Variables";
			}
		}
	}
}
