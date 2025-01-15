using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.BIServer.HostingEnvironment.Contracts;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer.ProviderManager
{
	// Token: 0x0200002A RID: 42
	internal sealed class OleDbProviderManager : IProviderManager
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public bool CanCreateCredentials(ProviderDataSource providerDataSource)
		{
			OleDbConnectionStringBuilder oleDbConnectionStringBuilder = new OleDbConnectionStringBuilder
			{
				ConnectionString = providerDataSource.ConnectionString
			};
			return oleDbConnectionStringBuilder.Provider != null && (oleDbConnectionStringBuilder.Provider.ToString().Equals("MSDASQL", StringComparison.InvariantCultureIgnoreCase) || oleDbConnectionStringBuilder.Provider.ToString().Equals("MSDASQL.1", StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004E14 File Offset: 0x00003014
		public ProviderDataSourceCredentials CreateCredentials(ProviderDataSource providerDataSource, IEnumerable<PbixDataSource> dataSources)
		{
			ContractExtensions.NotNull<IEnumerable<PbixDataSource>>(dataSources, "dataSources");
			PbixDataSource pbixDataSource = dataSources.FirstOrDefault<PbixDataSource>();
			if (pbixDataSource == null || dataSources.Count<PbixDataSource>() > 1)
			{
				throw new InvalidDataSourceException("OleDb Direct Query connection should have exacly one DataModelDatasource");
			}
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
			{
				ConnectionString = pbixDataSource.ConnectionString
			};
			dbConnectionStringBuilder.Remove("Trusted_Connection");
			dbConnectionStringBuilder.Remove("UID");
			dbConnectionStringBuilder.Remove("PWD");
			switch (pbixDataSource.AuthType)
			{
			case AuthorizationType.Integrated:
				dbConnectionStringBuilder["Trusted_Connection"] = "yes";
				return new ProviderDataSourceCredentials
				{
					ConnectionString = dbConnectionStringBuilder.ToString(),
					ImpersonationMode = new ImpersonationMode?(ImpersonationMode.ImpersonateCurrentUser)
				};
			case AuthorizationType.Windows:
				dbConnectionStringBuilder["Trusted_Connection"] = "yes";
				return new ProviderDataSourceCredentials
				{
					Account = pbixDataSource.Username,
					Password = pbixDataSource.Secret,
					ConnectionString = dbConnectionStringBuilder.ToString(),
					AuthenticatedUser = pbixDataSource.Username,
					ImpersonationMode = new ImpersonationMode?(ImpersonationMode.ImpersonateAccount)
				};
			case AuthorizationType.UsernamePassword:
				dbConnectionStringBuilder["UID"] = pbixDataSource.Username;
				dbConnectionStringBuilder["PWD"] = pbixDataSource.Secret;
				return new ProviderDataSourceCredentials
				{
					Account = pbixDataSource.Username,
					Password = pbixDataSource.Secret,
					ConnectionString = dbConnectionStringBuilder.ToString(),
					ImpersonationMode = new ImpersonationMode?(ImpersonationMode.ImpersonateServiceAccount)
				};
			default:
				throw new InvalidDataSourceException("OleDb Query connection Only Supports Basic, Windows or Integrated credentials");
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004F82 File Offset: 0x00003182
		public IEnumerable<PbixDataSource> BuildDataModelDataSources(ProviderDataSourceInfo providerDataSourceInfo)
		{
			ContractExtensions.NotNull<ProviderDataSource>(providerDataSourceInfo.providerDataSource, "providerDataSource");
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
			{
				ConnectionString = providerDataSourceInfo.providerDataSource.ConnectionString
			};
			yield return new PbixDataSource
			{
				ConnectionString = providerDataSourceInfo.providerDataSource.ConnectionString,
				DataSourceIdentifier = providerDataSourceInfo.providerDataSource.Name,
				Type = AccessType.DirectQuery,
				Kind = this.GetSourceFromDriverName((string)dbConnectionStringBuilder["driver"]),
				AuthType = AuthorizationType.Integrated
			};
			yield break;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004F99 File Offset: 0x00003199
		private SourceKind GetSourceFromDriverName(string driverName)
		{
			if (driverName != null && driverName.Equals("HDBODBC", StringComparison.InvariantCultureIgnoreCase))
			{
				return SourceKind.SapHana;
			}
			return SourceKind.OleDb;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004FB1 File Offset: 0x000031B1
		public ProviderDataSourceCredentials RemoveCredentials(ProviderDataSource providerDataSource)
		{
			throw new NotSupportedException("Removing credentials is not supported by OleDbProviderManager");
		}

		// Token: 0x04000072 RID: 114
		public const string DataProviderName = "MSDASQL";

		// Token: 0x04000073 RID: 115
		public const string DataProviderNameAlternate = "MSDASQL.1";
	}
}
