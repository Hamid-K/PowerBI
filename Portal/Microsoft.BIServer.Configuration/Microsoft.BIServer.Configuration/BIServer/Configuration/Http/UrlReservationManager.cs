using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration.Http
{
	// Token: 0x0200003B RID: 59
	public static class UrlReservationManager
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00008148 File Offset: 0x00006348
		public static void ReserveAndDeleteIfExists(string url, string securityDescriptor)
		{
			Logger.Info("Reserving url {0}", new object[] { url });
			UrlReservationManager.HttpInitialize();
			try
			{
				HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET http_SERVICE_CONFIG_URLACL_SET = UrlReservationManager.InitializeInputConfigInfo(url, securityDescriptor);
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET)));
				Marshal.StructureToPtr<HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET>(http_SERVICE_CONFIG_URLACL_SET, intPtr, false);
				int num = UrlReservationManager.HttpSetServiceConfiguration(intPtr, http_SERVICE_CONFIG_URLACL_SET);
				if (183 == num)
				{
					num = UrlReservationManager.HttpDeleteServiceConfiguration(intPtr, http_SERVICE_CONFIG_URLACL_SET);
					if (num == 0)
					{
						num = UrlReservationManager.HttpSetServiceConfiguration(intPtr, http_SERVICE_CONFIG_URLACL_SET);
					}
				}
				Marshal.FreeCoTaskMem(intPtr);
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
			}
			finally
			{
				HttpApiNative.HttpTerminate(2, IntPtr.Zero);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000081E8 File Offset: 0x000063E8
		public static void DeleteIfExists(string url, string securityDescriptor)
		{
			UrlReservationManager.HttpInitialize();
			try
			{
				Logger.Info("deleting ur: {0}, with secStr: {1}", new object[] { url, securityDescriptor });
				HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET http_SERVICE_CONFIG_URLACL_SET = UrlReservationManager.InitializeInputConfigInfo(url, securityDescriptor);
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET)));
				Marshal.StructureToPtr<HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET>(http_SERVICE_CONFIG_URLACL_SET, intPtr, false);
				int num = UrlReservationManager.HttpDeleteServiceConfiguration(intPtr, http_SERVICE_CONFIG_URLACL_SET);
				Marshal.FreeCoTaskMem(intPtr);
				if (num != 0 && 2 != num)
				{
					throw new Win32Exception(num);
				}
			}
			finally
			{
				HttpApiNative.HttpTerminate(2, IntPtr.Zero);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008274 File Offset: 0x00006474
		private static HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET InitializeInputConfigInfo(string url, string securityDescriptor)
		{
			HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_KEY http_SERVICE_CONFIG_URLACL_KEY = new HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_KEY(url);
			HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_PARAM http_SERVICE_CONFIG_URLACL_PARAM = new HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_PARAM(securityDescriptor);
			return new HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET
			{
				KeyDesc = http_SERVICE_CONFIG_URLACL_KEY,
				ParamDesc = http_SERVICE_CONFIG_URLACL_PARAM
			};
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000082AC File Offset: 0x000064AC
		private static void HttpInitialize()
		{
			int num = HttpApiNative.HttpInitialize(HttpApiNative.HTTP_API_VERSION, 2, IntPtr.Zero);
			if (num != 0)
			{
				throw new Win32Exception(Convert.ToInt32(num));
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000082D9 File Offset: 0x000064D9
		private static int HttpDeleteServiceConfiguration(IntPtr pInputConfigInfo, HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET inputConfigInfoSet)
		{
			return HttpApiNative.HttpDeleteServiceConfiguration(IntPtr.Zero, HttpApiNative.HTTP_SERVICE_CONFIG_ID.HttpServiceConfigUrlAclInfo, pInputConfigInfo, Marshal.SizeOf<HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET>(inputConfigInfoSet), IntPtr.Zero);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000082F2 File Offset: 0x000064F2
		private static int HttpSetServiceConfiguration(IntPtr pInputConfigInfo, HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET inputConfigInfoSet)
		{
			return HttpApiNative.HttpSetServiceConfiguration(IntPtr.Zero, HttpApiNative.HTTP_SERVICE_CONFIG_ID.HttpServiceConfigUrlAclInfo, pInputConfigInfo, Marshal.SizeOf<HttpApiNative.HTTP_SERVICE_CONFIG_URLACL_SET>(inputConfigInfoSet), IntPtr.Zero);
		}
	}
}
