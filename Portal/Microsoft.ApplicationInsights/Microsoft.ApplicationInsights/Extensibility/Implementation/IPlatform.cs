using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200006D RID: 109
	internal interface IPlatform
	{
		// Token: 0x06000359 RID: 857
		string ReadConfigurationXml();

		// Token: 0x0600035A RID: 858
		IDebugOutput GetDebugOutput();

		// Token: 0x0600035B RID: 859
		string GetEnvironmentVariable(string name);

		// Token: 0x0600035C RID: 860
		string GetMachineName();
	}
}
