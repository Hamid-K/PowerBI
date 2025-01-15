using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000068 RID: 104
	internal static class AppContextSwitchHelper
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000A510 File Offset: 0x00008710
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
