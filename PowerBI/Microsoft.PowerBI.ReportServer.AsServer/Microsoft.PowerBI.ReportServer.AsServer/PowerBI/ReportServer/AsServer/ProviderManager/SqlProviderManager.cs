using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer.ProviderManager
{
	// Token: 0x0200002E RID: 46
	internal class SqlProviderManager : IProviderManager
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000053EC File Offset: 0x000035EC
		public ProviderDataSourceCredentials CreateCredentials(ProviderDataSource providerDataSource, IEnumerable<PbixDataSource> dataSources)
		{
			if (dataSources == null)
			{
				throw new ArgumentNullException("dataSources cannot be null");
			}
			PbixDataSource pbixDataSource = dataSources.FirstOrDefault<PbixDataSource>();
			if (pbixDataSource == null || dataSources.Count<PbixDataSource>() > 1)
			{
				throw new InvalidDataSourceException("Sql Direct Query connection should have exacly one DataModelDatasource");
			}
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
			{
				ConnectionString = pbixDataSource.ConnectionString
			};
			dbConnectionStringBuilder.Remove("Integrated Security");
			dbConnectionStringBuilder.Remove("User Id");
			dbConnectionStringBuilder.Remove("Password");
			dbConnectionStringBuilder["Application Name"] = "RSPowerBI_msmdsvr";
			switch (pbixDataSource.AuthType)
			{
			case AuthorizationType.Integrated:
				dbConnectionStringBuilder["Integrated Security"] = bool.TrueString;
				return new ProviderDataSourceCredentials
				{
					ConnectionString = dbConnectionStringBuilder.ToString(),
					ImpersonationMode = new ImpersonationMode?(ImpersonationMode.ImpersonateCurrentUser)
				};
			case AuthorizationType.Windows:
				dbConnectionStringBuilder["Integrated Security"] = bool.TrueString;
				return new ProviderDataSourceCredentials
				{
					Account = pbixDataSource.Username,
					Password = pbixDataSource.Secret,
					ConnectionString = dbConnectionStringBuilder.ToString(),
					AuthenticatedUser = pbixDataSource.Username,
					ImpersonationMode = new ImpersonationMode?(ImpersonationMode.ImpersonateAccount)
				};
			case AuthorizationType.UsernamePassword:
				dbConnectionStringBuilder["User Id"] = pbixDataSource.Username;
				dbConnectionStringBuilder["Password"] = pbixDataSource.Secret;
				return new ProviderDataSourceCredentials
				{
					Account = pbixDataSource.Username,
					Password = pbixDataSource.Secret,
					ConnectionString = dbConnectionStringBuilder.ToString(),
					ImpersonationMode = new ImpersonationMode?(ImpersonationMode.ImpersonateServiceAccount)
				};
			default:
				throw new InvalidDataSourceException("Sql Direct Query connection Only Supports SQL or Integrated credentials");
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000556D File Offset: 0x0000376D
		public IEnumerable<PbixDataSource> BuildDataModelDataSources(ProviderDataSourceInfo providerDataSourceInfo)
		{
			if (providerDataSourceInfo.providerDataSource == null)
			{
				throw new ArgumentNullException("providerDataSource cannot be null");
			}
			yield return new PbixDataSource
			{
				ConnectionString = providerDataSourceInfo.providerDataSource.ConnectionString,
				DataSourceIdentifier = providerDataSourceInfo.providerDataSource.Name,
				Type = AccessType.DirectQuery,
				Kind = SourceKind.SQL,
				AuthType = AuthorizationType.Integrated
			};
			yield break;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000557D File Offset: 0x0000377D
		public bool CanCreateCredentials(ProviderDataSource providerDataSource)
		{
			return providerDataSource.Provider.Equals("System.Data.SqlClient", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005590 File Offset: 0x00003790
		public ProviderDataSourceCredentials RemoveCredentials(ProviderDataSource providerDataSource)
		{
			throw new NotSupportedException("Removing credentials is not supported by SqlProviderManager");
		}

		// Token: 0x04000079 RID: 121
		public const string DataProviderName = "System.Data.SqlClient";
	}
}
