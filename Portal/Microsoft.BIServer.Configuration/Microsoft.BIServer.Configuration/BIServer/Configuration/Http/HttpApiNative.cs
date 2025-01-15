using System;
using System.Runtime.InteropServices;

namespace Microsoft.BIServer.Configuration.Http
{
	// Token: 0x0200003A RID: 58
	public static class HttpApiNative
	{
		// Token: 0x060001E1 RID: 481
		[DllImport("httpapi.dll", SetLastError = true)]
		public static extern int HttpInitialize(HttpApiNative.HTTPAPI_VERSION version, int flags, IntPtr pReserved);

		// Token: 0x060001E2 RID: 482
		[DllImport("httpapi.dll", SetLastError = true)]
		public static extern int HttpSetServiceConfiguration(IntPtr serviceIntPtr, HttpApiNative.HTTP_SERVICE_CONFIG_ID configId, IntPtr pConfigInformation, int configInformationLength, IntPtr pOverlapped);

		// Token: 0x060001E3 RID: 483
		[DllImport("httpapi.dll", SetLastError = true)]
		public static extern int HttpDeleteServiceConfiguration(IntPtr serviceIntPtr, HttpApiNative.HTTP_SERVICE_CONFIG_ID configId, IntPtr pConfigInformation, int configInformationLength, IntPtr pOverlapped);

		// Token: 0x060001E4 RID: 484
		[DllImport("httpapi.dll", SetLastError = true)]
		public static extern int HttpTerminate(int flags, IntPtr pReserved);

		// Token: 0x0400019E RID: 414
		public const int HTTP_INITIALIZE_CONFIG = 2;

		// Token: 0x0400019F RID: 415
		public const int NO_ERROR = 0;

		// Token: 0x040001A0 RID: 416
		public const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x040001A1 RID: 417
		public const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x040001A2 RID: 418
		public static readonly HttpApiNative.HTTPAPI_VERSION HTTP_API_VERSION = new HttpApiNative.HTTPAPI_VERSION(1, 0);

		// Token: 0x0200005F RID: 95
		public enum HTTP_SERVICE_CONFIG_ID
		{
			// Token: 0x0400022F RID: 559
			HttpServiceConfigIpListenList,
			// Token: 0x04000230 RID: 560
			HttpServiceConfigSslCertInfo,
			// Token: 0x04000231 RID: 561
			HttpServiceConfigUrlAclInfo,
			// Token: 0x04000232 RID: 562
			HttpServiceConfigMax
		}

		// Token: 0x02000060 RID: 96
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct HTTPAPI_VERSION
		{
			// Token: 0x06000236 RID: 566 RVA: 0x0000904E File Offset: 0x0000724E
			public HTTPAPI_VERSION(short majorVersion, short minorVersion)
			{
				this.HttpApiMajorVersion = majorVersion;
				this.HttpApiMinorVersion = minorVersion;
			}

			// Token: 0x04000233 RID: 563
			public short HttpApiMajorVersion;

			// Token: 0x04000234 RID: 564
			public short HttpApiMinorVersion;
		}

		// Token: 0x02000061 RID: 97
		public struct HTTP_SERVICE_CONFIG_URLACL_SET
		{
			// Token: 0x04000235 RID: 565
			public HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_KEY KeyDesc;

			// Token: 0x04000236 RID: 566
			public HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_PARAM ParamDesc;
		}

		// Token: 0x02000062 RID: 98
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct HTTP_SERVICE_CONFIG_URLACL_KEY
		{
			// Token: 0x06000237 RID: 567 RVA: 0x0000905E File Offset: 0x0000725E
			public HTTP_SERVICE_CONFIG_URLACL_KEY(string urlPrefix)
			{
				this.pUrlPrefix = urlPrefix;
			}

			// Token: 0x04000237 RID: 567
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pUrlPrefix;
		}

		// Token: 0x02000063 RID: 99
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct HTTP_SERVICE_CONFIG_URLACL_PARAM
		{
			// Token: 0x06000238 RID: 568 RVA: 0x00009067 File Offset: 0x00007267
			public HTTP_SERVICE_CONFIG_URLACL_PARAM(string securityDescriptor)
			{
				this.pStringSecurityDescriptor = securityDescriptor;
			}

			// Token: 0x04000238 RID: 568
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pStringSecurityDescriptor;
		}
	}
}
