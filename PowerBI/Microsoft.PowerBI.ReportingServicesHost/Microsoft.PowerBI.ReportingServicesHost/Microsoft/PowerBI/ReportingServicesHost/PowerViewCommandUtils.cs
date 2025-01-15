using System;
using Microsoft.Win32;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200005C RID: 92
	public static class PowerViewCommandUtils
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00005A94 File Offset: 0x00003C94
		public static string GetResourcePath(string url)
		{
			int num = url.IndexOf("/pv/", StringComparison.Ordinal);
			if (num > -1)
			{
				url = url.Substring(num + 4);
			}
			return url;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public static string GetContentTypeFromExtension(string fileExtension)
		{
			RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(fileExtension);
			if (registryKey != null)
			{
				object value = registryKey.GetValue("Content Type");
				if (value != null)
				{
					return value.ToString();
				}
			}
			return "application/octet-stream";
		}
	}
}
