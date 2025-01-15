using System;
using Microsoft.BIServer.HostingEnvironment.Cryptography;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;
using Microsoft.ReportingServices.Portal.ODataClient.V2;

namespace Microsoft.PowerBI.ReportServer.WebApi
{
	// Token: 0x0200000C RID: 12
	public static class DataModelDataSourceExtensions
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002A68 File Offset: 0x00000C68
		public static DataSource ToOData(this DataModelDataSourceEntity dataSource)
		{
			string text;
			string text2;
			string text3;
			if (dataSource.DSType == DataModelDataSourceEntity.DataModelDataSourceType.Live && dataSource.DSKind == DataModelDataSourceEntity.DataModelDataSourceKind.AnalysisServices)
			{
				text = DataModelDataSourceExtensions.TruncateTagIfPresent(HostingState.Current.CatalogCrypto.DecryptToString(dataSource.ConnectionString), CryptoTags.ConnectionString);
				text2 = DataModelDataSourceExtensions.TruncateTagIfPresent(HostingState.Current.CatalogCrypto.DecryptToString(dataSource.Username), CryptoTags.UserName);
				text3 = DataModelDataSourceExtensions.TruncateTagIfPresent(HostingState.Current.CatalogCrypto.DecryptToString(dataSource.Password), CryptoTags.Password);
			}
			else
			{
				text = HostingState.Current.CatalogCrypto.DecryptToString(dataSource.ConnectionString);
				text2 = HostingState.Current.CatalogCrypto.DecryptToString(dataSource.Username);
				text3 = HostingState.Current.CatalogCrypto.DecryptToString(dataSource.Password);
			}
			return new DataSource
			{
				Id = dataSource.DataSourceId,
				DataModelDataSource = new DataModelDataSource
				{
					Type = (DataModelDataSourceType)dataSource.DSType,
					Kind = (DataModelDataSourceKind)dataSource.DSKind,
					AuthType = (DataModelDataSourceAuthType)dataSource.AuthType,
					Username = text2,
					Secret = text3,
					ModelConnectionName = dataSource.ModelConnectionName
				},
				ConnectionString = text
			};
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B8C File Offset: 0x00000D8C
		public static bool IsLocalFile(this DataSource ds)
		{
			if (ds.DataModelDataSource.Kind == DataModelDataSourceKind.File || ds.DataModelDataSource.Kind == DataModelDataSourceKind.Folder)
			{
				string connectionString = ds.ConnectionString;
				return connectionString == null || connectionString.Length < 2 || string.Compare(connectionString.Substring(0, 2), "\\\\", StringComparison.Ordinal) != 0 || connectionString.Substring(2).IndexOf("\\", StringComparison.Ordinal) == 0 || connectionString.Substring(2).IndexOf("\\", StringComparison.Ordinal) == -1 || connectionString.Substring(2).IndexOf("\\\\", StringComparison.Ordinal) != -1;
			}
			return false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C26 File Offset: 0x00000E26
		internal static string TruncateTagIfPresent(string data, string tag)
		{
			if (!string.IsNullOrEmpty(data) && data.StartsWith(tag))
			{
				return data.Substring(tag.Length);
			}
			return data;
		}
	}
}
