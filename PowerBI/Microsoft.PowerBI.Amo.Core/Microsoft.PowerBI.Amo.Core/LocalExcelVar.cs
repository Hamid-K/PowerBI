using System;
using System.IO;
using Microsoft.Win32;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000031 RID: 49
	internal static class LocalExcelVar
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00005CDC File Offset: 0x00003EDC
		private static string GetMSMDLocalDefaultPath()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), string.Format("\\Microsoft Analysis Services\\AS OLEDB\\{0}\\msmdlocal.dll", "160"));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005CFC File Offset: 0x00003EFC
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

		// Token: 0x040000E9 RID: 233
		private const string MSOLAP140_CLASSID = "DBC724B0-DD86-4772-BB5A-FCC6CAB2FC1A";

		// Token: 0x040000EA RID: 234
		internal static readonly string MSMDLOCAL_PATH = LocalExcelVar.GetMSMDLocalPath();

		// Token: 0x040000EB RID: 235
		internal static readonly string MSMDLOCAL_FALLBACK_PATH = LocalExcelVar.MSMDLOCAL_PATH;

		// Token: 0x040000EC RID: 236
		internal const string MSMGDSRV = "msmgdsrv.dll";

		// Token: 0x040000ED RID: 237
		internal const bool IsLocalExcel = false;
	}
}
