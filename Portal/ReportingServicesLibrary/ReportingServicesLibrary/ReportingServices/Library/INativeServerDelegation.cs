using System;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200022F RID: 559
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("D60FDBD8-BC6C-4ad3-8AF8-4F3D6848F40E")]
	[ComImport]
	internal interface INativeServerDelegation
	{
		// Token: 0x06001405 RID: 5125
		[PreserveSig]
		int ApplyKey([MarshalAs(UnmanagedType.LPWStr)] [In] string encryptPassword, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] [In] byte[] encryptedKey, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] extendedError);

		// Token: 0x06001406 RID: 5126
		[PreserveSig]
		int ExtractKey([MarshalAs(UnmanagedType.LPWStr)] [In] string encryptPassword, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] out byte[] encryptedKey, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] extendedError);

		// Token: 0x06001407 RID: 5127
		[PreserveSig]
		int DeleteKey([MarshalAs(UnmanagedType.LPWStr)] [In] string installationID, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] extendedError);

		// Token: 0x06001408 RID: 5128
		[PreserveSig]
		int DeleteEncryptedContent([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] extendedError);

		// Token: 0x06001409 RID: 5129
		[PreserveSig]
		int ActivateService([MarshalAs(UnmanagedType.LPWStr)] [In] string targetInstallationID, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] extendedError);

		// Token: 0x0600140A RID: 5130
		[PreserveSig]
		int CatalogEncrypt([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] [In] byte[] decryptedData, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] out byte[] encryptedData, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] extendedError);

		// Token: 0x0600140B RID: 5131
		[PreserveSig]
		int CatalogDecrypt([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] [In] byte[] encryptedData, [MarshalAs(UnmanagedType.Bool)] [In] bool useSalt, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] out byte[] decryptedData, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] extendedError);

		// Token: 0x0600140C RID: 5132
		[PreserveSig]
		int ListReportServersInDB([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] machineNames, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] instanceNames, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] installationIDs, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] out byte[] flags, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] extendedError);

		// Token: 0x0600140D RID: 5133
		[PreserveSig]
		int ReencryptSecureInformation([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] extendedError);

		// Token: 0x0600140E RID: 5134
		[PreserveSig]
		int GetCredentials([In] int credentialsType, [In] Guid dataSourceId, [MarshalAs(UnmanagedType.BStr)] out string userName, [MarshalAs(UnmanagedType.BStr)] out string domainName, [MarshalAs(UnmanagedType.BStr)] out string password);

		// Token: 0x0600140F RID: 5135
		[PreserveSig]
		int GetInstanceVersion([MarshalAs(UnmanagedType.BStr)] out string instanceVersion);

		// Token: 0x06001410 RID: 5136
		[PreserveSig]
		int ListInstalledSharePointVersions([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] versionTokens);

		// Token: 0x06001411 RID: 5137
		[PreserveSig]
		int GetDatabaseVersionDisplayName([MarshalAs(UnmanagedType.BStr)] [In] string version, [MarshalAs(UnmanagedType.BStr)] out string displayName);

		// Token: 0x06001412 RID: 5138
		[PreserveSig]
		int GetAdminSiteUrl([MarshalAs(UnmanagedType.BStr)] out string AdminSiteURL);

		// Token: 0x06001413 RID: 5139
		[PreserveSig]
		int GetSharePointIntegration(out bool isSharePointIntegrated);

		// Token: 0x06001414 RID: 5140
		[PreserveSig]
		int GetInstanceBuildVersion([MarshalAs(UnmanagedType.BStr)] out string instanceBuildVersion);

		// Token: 0x06001415 RID: 5141
		[PreserveSig]
		int GetInstanceUpgradeScript([MarshalAs(UnmanagedType.BStr)] [In] string databaseName, [MarshalAs(UnmanagedType.BStr)] out string instanceUpgradeScript);

		// Token: 0x06001416 RID: 5142
		[PreserveSig]
		int SavePowerBIInformation([MarshalAs(UnmanagedType.LPWStr)] [In] string clientId, [MarshalAs(UnmanagedType.LPWStr)] [In] string clientSecret, [MarshalAs(UnmanagedType.LPWStr)] [In] string appObjectId, [MarshalAs(UnmanagedType.LPWStr)] [In] string tenantName, [MarshalAs(UnmanagedType.LPWStr)] [In] string tenantId, [MarshalAs(UnmanagedType.LPWStr)] [In] string resourceUrl, [MarshalAs(UnmanagedType.LPWStr)] [In] string authUrl, [MarshalAs(UnmanagedType.LPWStr)] [In] string tokenUrl, [MarshalAs(UnmanagedType.LPWStr)] [In] string redirectUrls);

		// Token: 0x06001417 RID: 5143
		[PreserveSig]
		int GetPowerBIInformation([MarshalAs(UnmanagedType.BStr)] out string clientId, [MarshalAs(UnmanagedType.BStr)] out string appObjectId, [MarshalAs(UnmanagedType.BStr)] out string tenantName, [MarshalAs(UnmanagedType.BStr)] out string tenantId, [MarshalAs(UnmanagedType.BStr)] out string resourceUrl, [MarshalAs(UnmanagedType.BStr)] out string authUrl, [MarshalAs(UnmanagedType.BStr)] out string tokenUrl, [MarshalAs(UnmanagedType.BStr)] out string redirectUrls);

		// Token: 0x06001418 RID: 5144
		[PreserveSig]
		int UpdatePowerBIInformation([MarshalAs(UnmanagedType.LPWStr)] [In] string redirectUrls);

		// Token: 0x06001419 RID: 5145
		[PreserveSig]
		int DeletePowerBIInformation();
	}
}
