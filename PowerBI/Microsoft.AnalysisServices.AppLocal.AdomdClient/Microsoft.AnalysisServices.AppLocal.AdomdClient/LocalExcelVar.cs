using System;
using System.IO;
using Microsoft.Win32;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000019 RID: 25
	internal static class LocalExcelVar
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000026C0 File Offset: 0x000008C0
		private static string GetMSMDLocalDefaultPath()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), string.Format("\\Microsoft Analysis Services\\AS OLEDB\\{0}\\msmdlocal.dll", "160"));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026E0 File Offset: 0x000008E0
		private static string GetMSMDLocalPath()
		{
			string text;
			try
			{
				using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("CLSID", false))
				{
					RegistryKey registryKey2 = registryKey.OpenSubKey(string.Format("{{{0}}}\\InprocServer32", "DBC724B0-DD86-4772-BB5A-FCC6CAB2FC1A"), false);
					if (registryKey2 == null)
					{
						text = LocalExcelVar.GetMSMDLocalDefaultPath();
					}
					else
					{
						try
						{
							string text2 = (string)registryKey2.GetValue(null);
							if (string.IsNullOrEmpty(text2))
							{
								text = LocalExcelVar.GetMSMDLocalDefaultPath();
							}
							else
							{
								text = Path.Combine(Path.GetDirectoryName(text2), "msmdlocal.dll");
							}
						}
						finally
						{
							registryKey2.Close();
						}
					}
				}
			}
			catch (Exception)
			{
				text = LocalExcelVar.GetMSMDLocalDefaultPath();
			}
			return text;
		}

		// Token: 0x04000094 RID: 148
		private const string MSOLAP140_CLASSID = "DBC724B0-DD86-4772-BB5A-FCC6CAB2FC1A";

		// Token: 0x04000095 RID: 149
		internal static readonly string MSMDLOCAL_PATH = LocalExcelVar.GetMSMDLocalPath();

		// Token: 0x04000096 RID: 150
		internal static readonly string MSMDLOCAL_FALLBACK_PATH = LocalExcelVar.MSMDLOCAL_PATH;

		// Token: 0x04000097 RID: 151
		internal const string MSMGDSRV = "msmgdsrv.dll";

		// Token: 0x04000098 RID: 152
		internal const bool IsLocalExcel = false;
	}
}
