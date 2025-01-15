using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.PowerBI.Packaging.Storage;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ReportServer.PbixLib.Parsing
{
	// Token: 0x0200000B RID: 11
	public static class PbixReportElementsExtensions
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000028F8 File Offset: 0x00000AF8
		public static bool IsMobileOptimized(this PbixReportElements pbixReportElements)
		{
			string reportDocument = pbixReportElements.ReportDocument;
			bool flag = false;
			JToken jtoken = JObject.Parse(reportDocument).SelectToken("$.layoutOptimization", false);
			if (jtoken != null && jtoken.Type == JTokenType.Integer)
			{
				flag = jtoken.Value<int>() == 1;
			}
			return flag;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002938 File Offset: 0x00000B38
		public static string GetHashedAnalysisServerConnectionString(this PbixReportElements pbixReportElements)
		{
			ConnectionsSettings connectionsSettings = pbixReportElements.ConnectionsSettings;
			string text = null;
			if (connectionsSettings != null && connectionsSettings.Connections != null && connectionsSettings.Connections.ContainsKey("EntityDataSource"))
			{
				text = connectionsSettings.Connections["EntityDataSource"].ConnectionString;
			}
			return PbixReportElementsExtensions.GetSHA256Hash(text);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002988 File Offset: 0x00000B88
		private static string GetSHA256Hash(string connectionString)
		{
			if (connectionString == null)
			{
				connectionString = string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				foreach (byte b in sha256Cng.ComputeHash(Encoding.UTF8.GetBytes(connectionString)))
				{
					stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
				}
			}
			return stringBuilder.ToString();
		}
	}
}
