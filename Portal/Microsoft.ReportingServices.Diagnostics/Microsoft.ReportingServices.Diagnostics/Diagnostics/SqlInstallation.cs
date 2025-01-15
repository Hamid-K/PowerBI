using System;
using Microsoft.Win32;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000014 RID: 20
	internal static class SqlInstallation
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000273C File Offset: 0x0000093C
		internal static string SqlSharedCodeDirectory
		{
			get
			{
				string text = "SOFTWARE\\Microsoft\\Microsoft SQL Server\\150";
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(text);
				if (registryKey == null)
				{
					throw new SqlInstallation.ConfigurationErrorException("Installation Error: Could not find " + text + " registry key.");
				}
				string text2 = (string)registryKey.GetValue("SharedCode");
				if (text2 == null)
				{
					throw new SqlInstallation.ConfigurationErrorException("Installation Error: Could not find SharedCode on " + text + " registry key.");
				}
				return text2;
			}
		}

		// Token: 0x0200008F RID: 143
		internal class ConfigurationErrorException : Exception
		{
			// Token: 0x0600042C RID: 1068 RVA: 0x00010908 File Offset: 0x0000EB08
			internal ConfigurationErrorException(string message)
				: base(message)
			{
			}
		}
	}
}
