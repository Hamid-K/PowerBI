using System;
using System.Diagnostics;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x0200001A RID: 26
	internal static class EnvUtils
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003750 File Offset: 0x00001950
		internal static TraceSource GetNewTraceSource(string sourceName)
		{
			if (sourceName == null)
			{
				sourceName = "Microsoft.Identity.Client.Extensions.TraceSource";
			}
			SourceLevels sourceLevels = SourceLevels.Warning;
			string environmentVariable = Environment.GetEnvironmentVariable("IDENTITYEXTENSIONTRACELEVEL");
			SourceLevels sourceLevels2;
			if (!string.IsNullOrEmpty(environmentVariable) && Enum.TryParse<SourceLevels>(environmentVariable, true, out sourceLevels2))
			{
				sourceLevels = sourceLevels2;
			}
			return new TraceSource(sourceName, sourceLevels);
		}

		// Token: 0x04000069 RID: 105
		internal const string TraceLevelEnvVarName = "IDENTITYEXTENSIONTRACELEVEL";

		// Token: 0x0400006A RID: 106
		private const string DefaultTraceSource = "Microsoft.Identity.Client.Extensions.TraceSource";
	}
}
