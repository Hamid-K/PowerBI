using System;
using Microsoft.Win32;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x0200000F RID: 15
	public sealed class SkuStore : ISkuStore
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002DEC File Offset: 0x00000FEC
		public SkuInfo Load(string instanceName)
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server");
			if (registryKey == null)
			{
				return null;
			}
			RegistryKey registryKey2 = registryKey.OpenSubKey(instanceName);
			if (registryKey2 == null)
			{
				return null;
			}
			RegistryKey registryKey3 = registryKey2.OpenSubKey("Setup");
			if (registryKey3 == null)
			{
				return null;
			}
			byte[] array = (byte[])registryKey3.GetValue("checksum");
			if (array == null)
			{
				return null;
			}
			return ChecksumUtility.ConvertRegistryFormatToSkuInfo(instanceName, array);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002E4C File Offset: 0x0000104C
		public SkuInfo LoadFor32BitProcess(string instanceId)
		{
			string text = string.Format("{0}\\{1}\\{2}", "SOFTWARE\\Microsoft\\Microsoft SQL Server", instanceId, "Setup");
			if (text == null)
			{
				return null;
			}
			byte[] regKey = RegistryWow6432.GetRegKey64(RegHive.HkeyLocalMachine, text, "checksum");
			if (regKey == null)
			{
				return null;
			}
			return ChecksumUtility.ConvertRegistryFormatToSkuInfo(instanceId, regKey);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E94 File Offset: 0x00001094
		public void Save(SkuInfo info)
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server", true);
			registryKey.CreateSubKey(info.InstanceId);
			RegistryKey registryKey2 = registryKey.OpenSubKey(info.InstanceId, true);
			registryKey2.CreateSubKey("Setup");
			registryKey2.OpenSubKey("Setup", true).SetValue("checksum", ChecksumUtility.ConvertSkuInfoToRegistryFormat(info));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002EF4 File Offset: 0x000010F4
		public static ProductType GetProductType()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("BI_SERVER");
			if (environmentVariable != null && !string.Equals(environmentVariable, "false", StringComparison.InvariantCultureIgnoreCase))
			{
				return ProductType.PowerBiReportServer;
			}
			return ProductType.SqlServerReportingServices;
		}

		// Token: 0x0400004E RID: 78
		private const string RsRegistryKey = "SOFTWARE\\Microsoft\\Microsoft SQL Server";

		// Token: 0x0400004F RID: 79
		private const string SetupSubKey = "Setup";

		// Token: 0x04000050 RID: 80
		private const string ChecksumValue = "checksum";
	}
}
