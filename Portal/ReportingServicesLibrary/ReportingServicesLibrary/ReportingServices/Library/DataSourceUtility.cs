using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000F3 RID: 243
	internal static class DataSourceUtility
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x00026F6B File Offset: 0x0002516B
		internal static bool GoodForUnattendedExecution(RuntimeDataSourceInfoCollection dataSources)
		{
			return dataSources.TrueForAll((DataSourceInfo dataSource) => DataSourceUtility.GoodForUnattendedExecution(dataSource));
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00026F92 File Offset: 0x00025192
		internal static bool GoodForUnattendedExecution(DataSourceInfo dataSource)
		{
			if (!dataSource.ReferenceIsValid || !dataSource.Enabled)
			{
				return false;
			}
			if (Globals.Configuration.IsSurrogatePresent)
			{
				return DataSourceUtility.GoodForUnattendedSurrogateExecution(dataSource);
			}
			return DataSourceUtility.GoodForExecutionUnderServiceAccount(dataSource);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00026FC0 File Offset: 0x000251C0
		internal static bool GoodForExecutionUnderServiceAccount(DataSourceInfo dataSource)
		{
			return dataSource.ReferenceIsValid && dataSource.Enabled && !dataSource.ImpersonateUser && !dataSource.IsModelSecurityUsed && (dataSource.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Store || (dataSource.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Integrated && DataSourceUtility.CanUseTokenAuthentication(dataSource)));
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00027010 File Offset: 0x00025210
		internal static bool GoodForUnattendedSurrogateExecution(DataSourceInfo dataSource)
		{
			return dataSource.ReferenceIsValid && dataSource.Enabled && !dataSource.ImpersonateUser && !dataSource.IsModelSecurityUsed && dataSource.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Prompt && (dataSource.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Integrated || DataSourceUtility.CanUseTokenAuthentication(dataSource));
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00027062 File Offset: 0x00025262
		internal static void ThrowIfNotGoodForRdlx(DataSourceInfo info, string nameOrPath)
		{
			if (!DataSourceUtility.ExtensionImplementsInterface(info, typeof(IModelDataExtension)))
			{
				throw new InvalidDataSourceTypeException(nameOrPath);
			}
			if (info.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Prompt)
			{
				throw new InvalidDataSourceCredentialSettingException();
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00005BEF File Offset: 0x00003DEF
		private static bool CanUseTokenAuthentication(DataSourceInfo dataSource)
		{
			return false;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002708C File Offset: 0x0002528C
		internal static bool ExtensionImplementsInterface(DataSourceInfo dataSource, Type interfaceType)
		{
			Global.m_Tracer.Assert(interfaceType != null, "interfaceType cannot be null.");
			Type extensionType = ExtensionClassFactory.GetExtensionType(dataSource.Extension, "Data");
			return interfaceType.IsAssignableFrom(extensionType);
		}
	}
}
