using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Microsoft.ReportingServices
{
	// Token: 0x02000005 RID: 5
	public static class UpgradeScripts
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string ServerProductVersion
		{
			get
			{
				return "1000";
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public static string GetUpgradeScript(string databaseName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = databaseName + "TempDB";
			string text2 = "TEMPDBNAME";
			stringBuilder.AppendLine(UpgradeScripts.LoadResourceFile("Microsoft.ReportingServices.UpgradeScripts.UpgradeCatalog.sql"));
			stringBuilder.AppendLine(UpgradeScripts.LoadResourceFile("Microsoft.ReportingServices.UpgradeScripts.UpgradeTempDB.sql"));
			stringBuilder.AppendLine(UpgradeScripts.LoadResourceFile("Microsoft.ReportingServices.UpgradeScripts.UpgradeStoredProcedures.sql"));
			if (string.Equals(databaseName, text2, StringComparison.OrdinalIgnoreCase))
			{
				text2 += "1";
			}
			stringBuilder.Replace("ReportServerTempDB", "[" + text2 + "]");
			stringBuilder.Replace("ReportServer", "[" + databaseName + "]");
			stringBuilder.Replace("[" + text2 + "]", "[" + text + "]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000212C File Offset: 0x0000032C
		private static string LoadResourceFile(string filename)
		{
			string text;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
			{
				text = new StreamReader(manifestResourceStream).ReadToEnd();
			}
			return text;
		}
	}
}
