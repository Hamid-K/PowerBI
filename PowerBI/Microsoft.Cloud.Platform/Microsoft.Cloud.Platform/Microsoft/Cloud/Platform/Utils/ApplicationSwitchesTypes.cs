using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000178 RID: 376
	[Flags]
	public enum ApplicationSwitchesTypes
	{
		// Token: 0x040003CE RID: 974
		CommandLine = 1,
		// Token: 0x040003CF RID: 975
		AppConfig = 2,
		// Token: 0x040003D0 RID: 976
		WebConfig = 4,
		// Token: 0x040003D1 RID: 977
		EnvironmentVariables = 8,
		// Token: 0x040003D2 RID: 978
		ActivationData = 16
	}
}
