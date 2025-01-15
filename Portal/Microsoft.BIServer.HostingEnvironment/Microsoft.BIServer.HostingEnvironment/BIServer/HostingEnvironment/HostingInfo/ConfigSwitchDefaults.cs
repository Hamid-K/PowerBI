using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.BIServer.HostingEnvironment.HostingInfo
{
	// Token: 0x02000045 RID: 69
	public static class ConfigSwitchDefaults
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00005F10 File Offset: 0x00004110
		static ConfigSwitchDefaults()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableCDNVisuals, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableClientPrinting, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableCustomVisuals, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableExecutionLogging, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableIntegratedSecurity, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableListHistorySnapshotsSize, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableLoadReportDefinition, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableLocalRedirectRestriction, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableMyReports, false);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnablePowerBIReportExportData, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnablePowerBIReportExportUnderlyingData, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableRemoteErrors, false);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.EnableTestConnectionDetailedErrors, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.RequireIntune, false);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.SharePointIntegrated, false);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.UseSessionCookies, true);
			ConfigSwitchDefaults.AddDefault(dictionary, ConfigSwitches.LogClientIPAddress, false);
			ConfigSwitchDefaults._current = dictionary;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00005FB8 File Offset: 0x000041B8
		private static void AddDefault(Dictionary<string, string> defaults, ConfigSwitches configKey, bool configValue)
		{
			defaults.Add(configKey.ToString(CultureInfo.InvariantCulture), configValue.ToString());
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00005FD9 File Offset: 0x000041D9
		public static IReadOnlyDictionary<string, string> Current
		{
			get
			{
				return ConfigSwitchDefaults._current;
			}
		}

		// Token: 0x040000F7 RID: 247
		private static IReadOnlyDictionary<string, string> _current;
	}
}
