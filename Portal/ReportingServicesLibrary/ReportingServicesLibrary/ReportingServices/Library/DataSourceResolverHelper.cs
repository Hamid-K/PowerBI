using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200017C RID: 380
	internal static class DataSourceResolverHelper
	{
		// Token: 0x06000DED RID: 3565 RVA: 0x00032E14 File Offset: 0x00031014
		public static void ValidateDataSource(DataSourceInfo dataSourceInfo)
		{
			dataSourceInfo.ThrowIfNotUsable(new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity));
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00032E30 File Offset: 0x00031030
		public static DataSourceInfo.CredentialsRetrievalOption ConvertToDataSourceInfoCredentials(DataSourceDefinition2.CredentialRetrievalTypes credentialRetrieval)
		{
			switch (credentialRetrieval)
			{
			case DataSourceDefinition2.CredentialRetrievalTypes.None:
				return DataSourceInfo.CredentialsRetrievalOption.None;
			case DataSourceDefinition2.CredentialRetrievalTypes.Store:
				return DataSourceInfo.CredentialsRetrievalOption.Store;
			case DataSourceDefinition2.CredentialRetrievalTypes.Integrated:
				return DataSourceInfo.CredentialsRetrievalOption.Integrated;
			case DataSourceDefinition2.CredentialRetrievalTypes.ServiceAccount:
				return DataSourceInfo.CredentialsRetrievalOption.ServiceAccount;
			case DataSourceDefinition2.CredentialRetrievalTypes.SecureStore:
				throw new SecureStoreUnsupportedSharePointVersionException();
			}
			return DataSourceInfo.CredentialsRetrievalOption.None;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00032E61 File Offset: 0x00031061
		public static Microsoft.ReportingServices.DataExtensions.SecureStoreLookup.LookupContextOptions ConvertToDataSourceInfoLookupContext(Microsoft.ReportingServices.Common.SecureStoreLookup.LookupContextOptions lookUpContext)
		{
			if (lookUpContext == Microsoft.ReportingServices.Common.SecureStoreLookup.LookupContextOptions.AuthenticatedUser)
			{
				return Microsoft.ReportingServices.DataExtensions.SecureStoreLookup.LookupContextOptions.AuthenticatedUser;
			}
			if (lookUpContext != Microsoft.ReportingServices.Common.SecureStoreLookup.LookupContextOptions.Unattended)
			{
				return Microsoft.ReportingServices.DataExtensions.SecureStoreLookup.LookupContextOptions.AuthenticatedUser;
			}
			return Microsoft.ReportingServices.DataExtensions.SecureStoreLookup.LookupContextOptions.Unattended;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00032E61 File Offset: 0x00031061
		public static Microsoft.ReportingServices.Common.SecureStoreLookup.LookupContextOptions ConvertToSecureStoreLookupContext(Microsoft.ReportingServices.DataExtensions.SecureStoreLookup.LookupContextOptions lookUpContext)
		{
			if (lookUpContext == Microsoft.ReportingServices.DataExtensions.SecureStoreLookup.LookupContextOptions.AuthenticatedUser)
			{
				return Microsoft.ReportingServices.Common.SecureStoreLookup.LookupContextOptions.AuthenticatedUser;
			}
			if (lookUpContext != Microsoft.ReportingServices.DataExtensions.SecureStoreLookup.LookupContextOptions.Unattended)
			{
				return Microsoft.ReportingServices.Common.SecureStoreLookup.LookupContextOptions.AuthenticatedUser;
			}
			return Microsoft.ReportingServices.Common.SecureStoreLookup.LookupContextOptions.Unattended;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00032E71 File Offset: 0x00031071
		public static DataSourceDefinition2.CredentialRetrievalTypes ConvertToCredentialsRetrievalType(DataSourceInfo.CredentialsRetrievalOption credentialsRetrievalOption)
		{
			switch (credentialsRetrievalOption)
			{
			case DataSourceInfo.CredentialsRetrievalOption.Store:
				return DataSourceDefinition2.CredentialRetrievalTypes.Store;
			case DataSourceInfo.CredentialsRetrievalOption.Integrated:
				return DataSourceDefinition2.CredentialRetrievalTypes.Integrated;
			case DataSourceInfo.CredentialsRetrievalOption.None:
				return DataSourceDefinition2.CredentialRetrievalTypes.None;
			case DataSourceInfo.CredentialsRetrievalOption.ServiceAccount:
				return DataSourceDefinition2.CredentialRetrievalTypes.ServiceAccount;
			case DataSourceInfo.CredentialsRetrievalOption.SecureStore:
				return DataSourceDefinition2.CredentialRetrievalTypes.SecureStore;
			default:
				return DataSourceDefinition2.CredentialRetrievalTypes.None;
			}
		}
	}
}
