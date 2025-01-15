using System;
using System.Data.Common;
using System.Security;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.ReportingServicesHost.Utils;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200002F RID: 47
	public sealed class ASConnectionInfo : IASConnectionInfo
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00003E3A File Offset: 0x0000203A
		public static ASConnectionInfo CreateLocalConnectionInfo(string databaseName, string databaseID, string connectionString)
		{
			return new ASConnectionInfo(null, databaseName, databaseID, databaseName, ModelLocation.Internal, connectionString, false);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003E48 File Offset: 0x00002048
		public static ASConnectionInfo CreateLiveConnectionInfo(string cubeName, string databaseName, string databaseID, string connectionString)
		{
			ModelLocation modelLocation = (ASConnectionInfo.IsASAzureConnectionString(connectionString) ? ModelLocation.ExternalAsAzure : ModelLocation.ExternalOnPrem);
			return new ASConnectionInfo(cubeName, databaseName, databaseID, databaseName, modelLocation, connectionString, false);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003E70 File Offset: 0x00002070
		public static ASConnectionInfo CreateServiceLiveConnectionInfo(string cubeName, string databaseName, string databaseID, string serviceCatalogName, string connectionString)
		{
			string text = ((!string.IsNullOrEmpty(serviceCatalogName)) ? serviceCatalogName : databaseName);
			return new ASConnectionInfo(cubeName, databaseName, databaseID, text, ModelLocation.ExternalService, connectionString, true);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003E97 File Offset: 0x00002097
		public string CubeName { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003E9F File Offset: 0x0000209F
		public string DatabaseName { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003EA7 File Offset: 0x000020A7
		public string DatabaseID { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003EAF File Offset: 0x000020AF
		public string ModelMetadataCatalogName { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003EB7 File Offset: 0x000020B7
		public ModelLocation ModelLocation { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003EBF File Offset: 0x000020BF
		public bool EnableTelemetry { get; }

		// Token: 0x060000F0 RID: 240 RVA: 0x00003EC7 File Offset: 0x000020C7
		private ASConnectionInfo(string cubeName, string databaseName, string databaseID, string modelMetadataCatalogName, ModelLocation modelLocation, string connectionString, bool enableTelemetry)
		{
			this.CubeName = cubeName;
			this.DatabaseName = databaseName;
			this.DatabaseID = databaseID;
			this.ModelMetadataCatalogName = modelMetadataCatalogName;
			this.ModelLocation = modelLocation;
			this.connectionString = connectionString;
			this.EnableTelemetry = enableTelemetry;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003F04 File Offset: 0x00002104
		public string GetConnectionString()
		{
			return this.connectionString;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003F0C File Offset: 0x0000210C
		public SecureString GetSecureConnectionString()
		{
			return Encryption.ConvertToSecureString(this.GetConnectionString());
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003F1C File Offset: 0x0000211C
		private static bool IsASAzureConnectionString(string connectionString)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				return false;
			}
			bool flag;
			try
			{
				object obj;
				if (new DbConnectionStringBuilder
				{
					ConnectionString = connectionString
				}.TryGetValue("Data Source", out obj))
				{
					Uri uri = new Uri(obj as string);
					flag = uri.Scheme == "asazure" || uri.Scheme == "link";
				}
				else
				{
					flag = false;
				}
			}
			catch (Exception ex)
			{
				if (ex.IsStoppingException())
				{
					throw;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x040000CE RID: 206
		private const string AsAzureConnectionUriScheme = "asazure";

		// Token: 0x040000CF RID: 207
		private const string AsAzureLinkConnectionUriScheme = "link";

		// Token: 0x040000D0 RID: 208
		private const string DataSourceConnectionStringKey = "Data Source";

		// Token: 0x040000D1 RID: 209
		private readonly string connectionString;
	}
}
