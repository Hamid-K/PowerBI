using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.SPClient.Interfaces;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000030 RID: 48
	internal static class SPConnectivityManager
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00005B50 File Offset: 0x00003D50
		public static bool IsWorkbookInLegacyFarm(string path)
		{
			return SPConnectivityManager.isEnabled && SPConnectivityManager.IsWorkbookInLegacyFarmImpl(path);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005B61 File Offset: 0x00003D61
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

		// Token: 0x060000C3 RID: 195 RVA: 0x00005B84 File Offset: 0x00003D84
		public static void ConnectToFileInLegacyFarm(string dataSource, string dataSourceVersion, out string timeLastModified, out string databaseId, out string databaseName, out string loginName, out string serverEndpointAddress)
		{
			if (!SPConnectivityManager.isEnabled)
			{
				throw new InvalidOperationException();
			}
			SPConnectivityManager.ConnectToFileInLegacyFarmImpl(dataSource, dataSourceVersion, out timeLastModified, out databaseId, out databaseName, out loginName, out serverEndpointAddress);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005BA2 File Offset: 0x00003DA2
		public static IDisposable CreateLegacyFarmSPSite(string dataSource)
		{
			if (!SPConnectivityManager.isEnabled)
			{
				throw new InvalidOperationException();
			}
			return SPConnectivityManager.CreateLegacyFarmSPSiteImpl(dataSource);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005BB8 File Offset: 0x00003DB8
		public static Stream GetLegacyFarmSPSiteResponseStream(object spSite, Stream inStream, string serverEndpointAddress, string loginName, string databaseId, bool isSpecificVersion, bool isFirstRequest, string userAgent, string applicationName, string userAddress, string requestFlags, string requestDataType, ref string responseFlags, ref string responseDataType, ref bool outdatedVersion)
		{
			if (!SPConnectivityManager.isEnabled)
			{
				throw new InvalidOperationException();
			}
			return SPConnectivityManager.GetLegacyFarmSPSiteResponseStreamImpl(spSite, inStream, serverEndpointAddress, loginName, databaseId, isSpecificVersion, isFirstRequest, userAgent, applicationName, userAddress, requestFlags, requestDataType, ref responseFlags, ref responseDataType, ref outdatedVersion);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005BF1 File Offset: 0x00003DF1
		public static void TraceExceptionToLegacyFarm(Exception e)
		{
			if (SPConnectivityManager.isEnabled)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarmImpl(e);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005C00 File Offset: 0x00003E00
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool IsWorkbookInLegacyFarmImpl(string path)
		{
			return SPLegacyFarmHelper.IsWorkbookInLegacyFarm(path);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005C08 File Offset: 0x00003E08
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool TryGetLinkFileMetadataInLegacyFarmImpl(string path, out string server, out string database, out bool isDelegationAllowed, out bool isFileMalformed)
		{
			return SPLegacyFarmHelper.TryGetLinkFileMetadataInLegacyFarm(path, ref server, ref database, ref isDelegationAllowed, ref isFileMalformed);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005C15 File Offset: 0x00003E15
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ConnectToFileInLegacyFarmImpl(string dataSource, string dataSourceVersion, out string timeLastModified, out string databaseId, out string databaseName, out string loginName, out string serverEndpointAddress)
		{
			SPLegacyFarmHelper.ConnectToFileInLegacyFarm(dataSource, dataSourceVersion, ref timeLastModified, ref databaseId, ref databaseName, ref loginName, ref serverEndpointAddress);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005C26 File Offset: 0x00003E26
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IDisposable CreateLegacyFarmSPSiteImpl(string dataSource)
		{
			return SPLegacyFarmHelper.CreateLegacyFarmSPSite(dataSource);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005C30 File Offset: 0x00003E30
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Stream GetLegacyFarmSPSiteResponseStreamImpl(object spSite, Stream inStream, string serverEndpointAddress, string loginName, string databaseId, bool isSpecificVersion, bool isFirstRequest, string userAgent, string applicationName, string userAddress, string requestFlags, string requestDataType, ref string responseFlags, ref string responseDataType, ref bool outdatedVersion)
		{
			return SPLegacyFarmHelper.GetLegacyFarmSPSiteResponseStream(spSite, inStream, serverEndpointAddress, loginName, databaseId, isSpecificVersion, isFirstRequest, userAgent, applicationName, userAddress, requestFlags, requestDataType, ref responseFlags, ref responseDataType, ref outdatedVersion);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005C5C File Offset: 0x00003E5C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void TraceExceptionToLegacyFarmImpl(Exception e)
		{
			SPLegacyFarmHelper.TraceExceptionToLegacyFarm(e);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005C64 File Offset: 0x00003E64
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

		// Token: 0x040000E8 RID: 232
		private static bool isEnabled = SPConnectivityManager.CheckIfSPConnectivityIsEnabled();
	}
}
