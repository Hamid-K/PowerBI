using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer.ProviderManager
{
	// Token: 0x0200002B RID: 43
	internal class TeradataProviderManager : IProviderManager
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00004FC0 File Offset: 0x000031C0
		public ProviderDataSourceCredentials CreateCredentials(ProviderDataSource providerDataSource, IEnumerable<PbixDataSource> dataSources)
		{
			if (dataSources == null)
			{
				throw new ArgumentNullException("dataSources cannot be null");
			}
			PbixDataSource pbixDataSource = dataSources.FirstOrDefault<PbixDataSource>();
			if (pbixDataSource == null || dataSources.Count<PbixDataSource>() > 1)
			{
				throw new InvalidDataSourceException("Teradata Direct Query connection should have exacly one DataModelDatasource");
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
				dbConnectionStringBuilder["Integrated Security"] = "true";
				return new ProviderDataSourceCredentials
				{
					ConnectionString = dbConnectionStringBuilder.ToString(),
					ImpersonationMode = new ImpersonationMode?(ImpersonationMode.ImpersonateCurrentUser)
				};
			case AuthorizationType.Windows:
				dbConnectionStringBuilder["Integrated Security"] = "true";
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
				throw new InvalidDataSourceException("Teradata Direct Query connection Only Supports Basic, Windows or Integrated credentials");
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005131 File Offset: 0x00003331
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
				Kind = SourceKind.Teradata,
				AuthType = AuthorizationType.Integrated
			};
			yield break;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005141 File Offset: 0x00003341
		public bool CanCreateCredentials(ProviderDataSource providerDataSource)
		{
			return providerDataSource.Provider.Equals("Teradata.Client.Provider", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005154 File Offset: 0x00003354
		public ProviderDataSourceCredentials RemoveCredentials(ProviderDataSource providerDataSource)
		{
			throw new NotSupportedException("Removing credentials is not supported by TeradataProviderManager");
		}

		// Token: 0x04000074 RID: 116
		public const string DataProviderName = "Teradata.Client.Provider";

		// Token: 0x04000075 RID: 117
		private const string IntegratedSecurityTrue = "true";
	}
}
