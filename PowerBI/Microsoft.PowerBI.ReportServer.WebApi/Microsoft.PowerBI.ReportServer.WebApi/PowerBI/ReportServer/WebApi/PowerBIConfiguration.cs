using System;
using System.Collections.Generic;
using Microsoft.BIServer.HostingEnvironment.Contracts;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.ReportingServices.CatalogAccess;

namespace Microsoft.PowerBI.ReportServer.WebApi
{
	// Token: 0x0200000A RID: 10
	public class PowerBIConfiguration : IPowerBIConfiguration
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000028C8 File Offset: 0x00000AC8
		public PowerBIConfiguration(IConfigurationInfoDataAccessor configuration)
		{
			ContractExtensions.NotNull<IConfigurationInfoDataAccessor>(configuration, "configuration");
			IDictionary<string, string> result;
			try
			{
				result = configuration.GetConfigInfoValuesAsync().Result;
			}
			finally
			{
				if (configuration != null)
				{
					configuration.Dispose();
				}
			}
			this.ExportDataEnabled = this.GetConfigurationValue(result, ConfigSwitches.EnablePowerBIReportExportData);
			this.ExportUnderlyingDataEnabled = this.GetConfigurationValue(result, ConfigSwitches.EnablePowerBIReportExportUnderlyingData);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002930 File Offset: 0x00000B30
		private bool GetConfigurationValue(IDictionary<string, string> configValues, ConfigSwitches key)
		{
			string text = key.ToString();
			return configValues.ContainsKey(text) && configValues[key.ToString()].Equals(true.ToString(), StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002978 File Offset: 0x00000B78
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002980 File Offset: 0x00000B80
		public bool ExportDataEnabled { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002989 File Offset: 0x00000B89
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002991 File Offset: 0x00000B91
		public bool ExportUnderlyingDataEnabled { get; private set; }
	}
}
