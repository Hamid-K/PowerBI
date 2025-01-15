using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer.ProviderManager
{
	// Token: 0x0200002C RID: 44
	internal class OracleProviderManager : IProviderManager
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00005160 File Offset: 0x00003360
		public ProviderDataSourceCredentials CreateCredentials(ProviderDataSource providerDataSource, IEnumerable<PbixDataSource> dataSources)
		{
			if (dataSources == null)
			{
				throw new ArgumentNullException("dataSources cannot be null.");
			}
			PbixDataSource pbixDataSource = dataSources.FirstOrDefault<PbixDataSource>();
			if (pbixDataSource == null || dataSources.Count<PbixDataSource>() > 1)
			{
				throw new InvalidDataSourceException("Oracle Direct Query connection should have exacly one datasource");
			}
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
			{
				ConnectionString = pbixDataSource.ConnectionString
			};
			dbConnectionStringBuilder.Remove("Integrated Security");
			dbConnectionStringBuilder.Remove("User Id");
			dbConnectionStringBuilder.Remove("Password");
			switch (pbixDataSource.AuthType)
			{
			case AuthorizationType.Integrated:
				dbConnectionStringBuilder["User Id"] = "/";
				return new ProviderDataSourceCredentials
				{
					ConnectionString = dbConnectionStringBuilder.ToString(),
					ImpersonationMode = new ImpersonationMode?(ImpersonationMode.ImpersonateCurrentUser)
				};
			case AuthorizationType.Windows:
				dbConnectionStringBuilder["User Id"] = "/";
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
				throw new InvalidDataSourceException("Oracle Direct Query connection Only Supports Basic, Windows or Integrated credentials");
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000052D1 File Offset: 0x000034D1
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
				Kind = SourceKind.Oracle,
				AuthType = AuthorizationType.Integrated
			};
			yield break;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000052E1 File Offset: 0x000034E1
		public bool CanCreateCredentials(ProviderDataSource providerDataSource)
		{
			return providerDataSource.Provider.Equals("Oracle.DataAccess.Client", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000052F4 File Offset: 0x000034F4
		public ProviderDataSourceCredentials RemoveCredentials(ProviderDataSource providerDataSource)
		{
			throw new NotSupportedException("Removing credentials is not supported by OracleProviderManager");
		}

		// Token: 0x04000076 RID: 118
		public const string DataProviderName = "Oracle.DataAccess.Client";

		// Token: 0x04000077 RID: 119
		private const string WindowsIntegratedUser = "/";
	}
}
