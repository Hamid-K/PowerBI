using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000010 RID: 16
	internal static class AppContextSwitchHelper
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000240C File Offset: 0x0000060C
		[NullableContext(1)]
		public static bool GetConfigValue(string appContexSwitchName, string environmentVariableName)
		{
			bool flag;
			if (AppContext.TryGetSwitch(appContexSwitchName, out flag))
			{
				return flag;
			}
			string environmentVariable = Environment.GetEnvironmentVariable(environmentVariableName);
			return environmentVariable != null && (environmentVariable.Equals("true", StringComparison.OrdinalIgnoreCase) || environmentVariable.Equals("1"));
		}
	}
}
