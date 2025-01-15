using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.SPClient.Interfaces;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000018 RID: 24
	internal static class SPConnectivityManager
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002534 File Offset: 0x00000734
		public static bool IsWorkbookInLegacyFarm(string path)
		{
			return SPConnectivityManager.isEnabled && SPConnectivityManager.IsWorkbookInLegacyFarmImpl(path);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002545 File Offset: 0x00000745
		public static bool TryGetLinkFileMetadataInLegacyFarm(string path, out string server, out string database, out bool isDelegationAllowed, out bool isFileMalformed)
		{
			if (!SPConnectivityManager.isEnabled)
			{
				server = null;
				database = null;
				isDelegationAllowed = false;
				isFileMalformed = false;
				return false;
			}
			return SPConnectivityManager.TryGetLinkFileMetadataInLegacyFarmImpl(path, out server, out database, out isDelegationAllowed, out isFileMalformed);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002568 File Offset: 0x00000768
		public static void ConnectToFileInLegacyFarm(string dataSource, string dataSourceVersion, out string timeLastModified, out string databaseId, out string databaseName, out string loginName, out string serverEndpointAddress)
		{
			if (!SPConnectivityManager.isEnabled)
			{
				throw new InvalidOperationException();
			}
			SPConnectivityManager.ConnectToFileInLegacyFarmImpl(dataSource, dataSourceVersion, out timeLastModified, out databaseId, out databaseName, out loginName, out serverEndpointAddress);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002586 File Offset: 0x00000786
		public static IDisposable CreateLegacyFarmSPSite(string dataSource)
		{
			if (!SPConnectivityManager.isEnabled)
			{
				throw new InvalidOperationException();
			}
			return SPConnectivityManager.CreateLegacyFarmSPSiteImpl(dataSource);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000259C File Offset: 0x0000079C
		public static Stream GetLegacyFarmSPSiteResponseStream(object spSite, Stream inStream, string serverEndpointAddress, string loginName, string databaseId, bool isSpecificVersion, bool isFirstRequest, string userAgent, string applicationName, string userAddress, string requestFlags, string requestDataType, ref string responseFlags, ref string responseDataType, ref bool outdatedVersion)
		{
			if (!SPConnectivityManager.isEnabled)
			{
				throw new InvalidOperationException();
			}
			return SPConnectivityManager.GetLegacyFarmSPSiteResponseStreamImpl(spSite, inStream, serverEndpointAddress, loginName, databaseId, isSpecificVersion, isFirstRequest, userAgent, applicationName, userAddress, requestFlags, requestDataType, ref responseFlags, ref responseDataType, ref outdatedVersion);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025D5 File Offset: 0x000007D5
		public static void TraceExceptionToLegacyFarm(Exception e)
		{
			if (SPConnectivityManager.isEnabled)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarmImpl(e);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025E4 File Offset: 0x000007E4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool IsWorkbookInLegacyFarmImpl(string path)
		{
			return SPLegacyFarmHelper.IsWorkbookInLegacyFarm(path);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025EC File Offset: 0x000007EC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool TryGetLinkFileMetadataInLegacyFarmImpl(string path, out string server, out string database, out bool isDelegationAllowed, out bool isFileMalformed)
		{
			return SPLegacyFarmHelper.TryGetLinkFileMetadataInLegacyFarm(path, ref server, ref database, ref isDelegationAllowed, ref isFileMalformed);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025F9 File Offset: 0x000007F9
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ConnectToFileInLegacyFarmImpl(string dataSource, string dataSourceVersion, out string timeLastModified, out string databaseId, out string databaseName, out string loginName, out string serverEndpointAddress)
		{
			SPLegacyFarmHelper.ConnectToFileInLegacyFarm(dataSource, dataSourceVersion, ref timeLastModified, ref databaseId, ref databaseName, ref loginName, ref serverEndpointAddress);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000260A File Offset: 0x0000080A
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IDisposable CreateLegacyFarmSPSiteImpl(string dataSource)
		{
			return SPLegacyFarmHelper.CreateLegacyFarmSPSite(dataSource);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002614 File Offset: 0x00000814
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Stream GetLegacyFarmSPSiteResponseStreamImpl(object spSite, Stream inStream, string serverEndpointAddress, string loginName, string databaseId, bool isSpecificVersion, bool isFirstRequest, string userAgent, string applicationName, string userAddress, string requestFlags, string requestDataType, ref string responseFlags, ref string responseDataType, ref bool outdatedVersion)
		{
			return SPLegacyFarmHelper.GetLegacyFarmSPSiteResponseStream(spSite, inStream, serverEndpointAddress, loginName, databaseId, isSpecificVersion, isFirstRequest, userAgent, applicationName, userAddress, requestFlags, requestDataType, ref responseFlags, ref responseDataType, ref outdatedVersion);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002640 File Offset: 0x00000840
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void TraceExceptionToLegacyFarmImpl(Exception e)
		{
			SPLegacyFarmHelper.TraceExceptionToLegacyFarm(e);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002648 File Offset: 0x00000848
		private static bool CheckIfSPConnectivityIsEnabled()
		{
			bool flag;
			try
			{
				AssemblyName name = Assembly.GetExecutingAssembly().GetName();
				AssemblyName assemblyName = new AssemblyName();
				assemblyName.Name = "Microsoft.PowerBI.SPClient.Interfaces";
				assemblyName.Version = name.Version;
				assemblyName.CultureInfo = name.CultureInfo;
				assemblyName.SetPublicKeyToken(name.GetPublicKeyToken());
				Assembly.Load(assemblyName);
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000093 RID: 147
		private static bool isEnabled = SPConnectivityManager.CheckIfSPConnectivityIsEnabled();
	}
}
