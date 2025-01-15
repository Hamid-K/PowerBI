using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Util;

namespace RSRemoteRpcClient
{
	// Token: 0x0200001C RID: 28
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class RemoteLogon
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000072DC File Offset: 0x000066DC
		public static SafeToken GetRemoteImpToken(string pRPCEndpointName, int type, Guid dataSourceId, string pUserName, string pDomain, string pPassword)
		{
			SafeToken safeToken = null;
			bool flag = false;
			int num = RemoteLogon.NativeRemoteLogon(pRPCEndpointName, type, dataSourceId, pUserName, pDomain, pPassword, out safeToken, ref flag);
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			return safeToken;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007664 File Offset: 0x00006A64
		public unsafe static void ActivateService(string pRPCEndpointName, string instanceID)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			SafeStringToHGlobalUni safeStringToHGlobalUni2 = null;
			tagSAFEARRAY* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(pRPCEndpointName);
				safeStringToHGlobalUni2 = SafeStringToHGlobalUni.Create(instanceID);
				RemoteLogon.ThrowFriendlyRpcError(<Module>.NativeRpcActivateWebService(safeStringToHGlobalUni.ToPointer(), safeStringToHGlobalUni2.ToPointer(), &ptr), ptr);
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
				if (safeStringToHGlobalUni2 != null)
				{
					safeStringToHGlobalUni2.Close();
				}
				if (ptr != null)
				{
					<Module>.SafeArrayDestroy(ptr);
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000076E8 File Offset: 0x00006AE8
		public unsafe static byte[] ExtractKey(string pRPCEndpointName, string encryptPassword)
		{
			tagSAFEARRAY* ptr = null;
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			SafeStringToHGlobalUni safeStringToHGlobalUni2 = null;
			tagSAFEARRAY* ptr2 = null;
			ulong num = 0UL;
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(pRPCEndpointName);
				safeStringToHGlobalUni2 = SafeStringToHGlobalUni.Create(encryptPassword);
				StringUtilities.GetStringSize(safeStringToHGlobalUni2.DangerousGetHandle(), ref num);
				RemoteLogon.ThrowFriendlyRpcError(<Module>.NativeRpcExtractKey(safeStringToHGlobalUni.ToPointer(), safeStringToHGlobalUni2.ToPointer(), &ptr, &ptr2), ptr2);
				array = RemoteLogon.ConvertNativeArrayToManaged(ptr);
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
				if (safeStringToHGlobalUni2 != null)
				{
					safeStringToHGlobalUni2.ZeroString();
					safeStringToHGlobalUni2.Close();
				}
				if (ptr != null)
				{
					<Module>.SafeArrayDestroy(ptr);
				}
				if (ptr2 != null)
				{
					<Module>.SafeArrayDestroy(ptr2);
				}
			}
			return array;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000077A0 File Offset: 0x00006BA0
		public unsafe static void ApplyKey(string pRPCEndpointName, string encryptPassword, byte[] encryptedKey)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			SafeStringToHGlobalUni safeStringToHGlobalUni2 = null;
			SafeSafeArray safeSafeArray = null;
			tagSAFEARRAY* ptr = null;
			ulong num = 0UL;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeSafeArray = SafeSafeArray.Create(encryptedKey);
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(pRPCEndpointName);
				safeStringToHGlobalUni2 = SafeStringToHGlobalUni.Create(encryptPassword);
				StringUtilities.GetStringSize(safeStringToHGlobalUni2.DangerousGetHandle(), ref num);
				int num2 = <Module>.NativeRpcApplyKey(safeStringToHGlobalUni.ToPointer(), safeStringToHGlobalUni2.ToPointer(), safeSafeArray.ToPointer(), &ptr);
				RemoteLogon.ThrowFriendlyRpcError(num2, ptr);
				RemoteLogon.ThrowFriendlyRpcError(num2, null);
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
				if (safeStringToHGlobalUni2 != null)
				{
					safeStringToHGlobalUni2.ZeroString();
					safeStringToHGlobalUni2.Close();
				}
				if (safeSafeArray != null)
				{
					safeSafeArray.Close();
				}
				if (ptr != null)
				{
					<Module>.SafeArrayDestroy(ptr);
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007860 File Offset: 0x00006C60
		public unsafe static void DeleteEncryptedContent(string pRPCEndpointName)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			tagSAFEARRAY* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(pRPCEndpointName);
				RemoteLogon.ThrowFriendlyRpcError(<Module>.NativeRpcDeleteEncryptedContent(safeStringToHGlobalUni.ToPointer(), &ptr), ptr);
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
				if (ptr != null)
				{
					<Module>.SafeArrayDestroy(ptr);
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007938 File Offset: 0x00006D38
		public unsafe static void DeleteKey(string pRPCEndpointName, string instanceID)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			SafeStringToHGlobalUni safeStringToHGlobalUni2 = null;
			tagSAFEARRAY* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(instanceID);
				safeStringToHGlobalUni2 = SafeStringToHGlobalUni.Create(pRPCEndpointName);
				RemoteLogon.ThrowFriendlyRpcError(<Module>.NativeRpcDeleteKey(safeStringToHGlobalUni2.ToPointer(), safeStringToHGlobalUni.ToPointer(), &ptr), ptr);
			}
			finally
			{
				if (safeStringToHGlobalUni2 != null)
				{
					safeStringToHGlobalUni2.Close();
				}
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
				if (ptr != null)
				{
					<Module>.SafeArrayDestroy(ptr);
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000078CC File Offset: 0x00006CCC
		public unsafe static void ReencryptSecureInformation(string pRPCEndpointName)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			tagSAFEARRAY* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(pRPCEndpointName);
				RemoteLogon.ThrowFriendlyRpcError(<Module>.NativeRpcReencryptSecureInformation(safeStringToHGlobalUni.ToPointer(), &ptr), ptr);
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
				if (ptr != null)
				{
					<Module>.SafeArrayDestroy(ptr);
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007C1C File Offset: 0x0000701C
		public unsafe static void ListReportServersInDB(string pRPCEndpointName, out string[] machineNames, out string[] instanceNames, out string[] installationIDs, out byte[] flags)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			tagSAFEARRAY* ptr = null;
			tagSAFEARRAY* ptr2 = null;
			tagSAFEARRAY* ptr3 = null;
			tagSAFEARRAY* ptr4 = null;
			tagSAFEARRAY* ptr5 = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(pRPCEndpointName);
				RemoteLogon.ThrowFriendlyRpcError(<Module>.NativeRpcListReportServersInDB(safeStringToHGlobalUni.ToPointer(), &ptr, &ptr2, &ptr3, &ptr4, &ptr5), ptr5);
				machineNames = RemoteLogon.ConvertNativeStringArrayToManaged(ptr);
				instanceNames = RemoteLogon.ConvertNativeStringArrayToManaged(ptr2);
				installationIDs = RemoteLogon.ConvertNativeStringArrayToManaged(ptr3);
				flags = RemoteLogon.ConvertNativeArrayToManaged(ptr4);
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
				if (ptr != null)
				{
					<Module>.SafeArrayDestroy(ptr5);
				}
				if (ptr2 != null)
				{
					<Module>.SafeArrayDestroy(ptr5);
				}
				if (ptr3 != null)
				{
					<Module>.SafeArrayDestroy(ptr5);
				}
				if (ptr4 != null)
				{
					<Module>.SafeArrayDestroy(ptr5);
				}
				if (ptr5 != null)
				{
					<Module>.SafeArrayDestroy(ptr5);
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000079BC File Offset: 0x00006DBC
		public unsafe static byte[] CatalogEncrypt(string pRPCEndpointName, byte[] data)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			SafeSafeArray safeSafeArray = null;
			tagSAFEARRAY* ptr = null;
			tagSAFEARRAY* ptr2 = null;
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(pRPCEndpointName);
				safeSafeArray = SafeSafeArray.Create(data);
				RemoteLogon.ThrowFriendlyRpcError(<Module>.NativeRpcCatalogEncrypt(safeStringToHGlobalUni.ToPointer(), safeSafeArray.ToPointer(), &ptr, &ptr2), ptr2);
				array = RemoteLogon.ConvertNativeArrayToManaged(ptr);
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
				if (safeSafeArray != null)
				{
					safeSafeArray.ZeroArray();
					safeSafeArray.Close();
				}
				if (ptr != null)
				{
					<Module>.RtlSecureZeroMemory(*(long*)(ptr + 16L / (long)sizeof(tagSAFEARRAY)), (ulong)(*(int*)(ptr + 24L / (long)sizeof(tagSAFEARRAY))));
					<Module>.SafeArrayDestroy(ptr);
				}
				if (ptr2 != null)
				{
					<Module>.SafeArrayDestroy(ptr2);
				}
			}
			return array;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007A74 File Offset: 0x00006E74
		public unsafe static byte[] CatalogDecrypt(string pRPCEndpointName, byte[] data, [MarshalAs(UnmanagedType.U1)] bool useSalt)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			SafeSafeArray safeSafeArray = null;
			tagSAFEARRAY* ptr = null;
			tagSAFEARRAY* ptr2 = null;
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(pRPCEndpointName);
				safeSafeArray = SafeSafeArray.Create(data);
				RemoteLogon.ThrowFriendlyRpcError(<Module>.NativeRpcCatalogDecrypt(safeStringToHGlobalUni.ToPointer(), safeSafeArray.ToPointer(), useSalt, &ptr, &ptr2), ptr2);
				array = RemoteLogon.ConvertNativeArrayToManaged(ptr);
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
				if (safeSafeArray != null)
				{
					safeSafeArray.ZeroArray();
					safeSafeArray.Close();
				}
				if (ptr != null)
				{
					<Module>.RtlSecureZeroMemory(*(long*)(ptr + 16L / (long)sizeof(tagSAFEARRAY)), (ulong)(*(int*)(ptr + 24L / (long)sizeof(tagSAFEARRAY))));
					<Module>.SafeArrayDestroy(ptr);
				}
				if (ptr2 != null)
				{
					<Module>.SafeArrayDestroy(ptr2);
				}
			}
			return array;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007B30 File Offset: 0x00006F30
		public static void SavePowerBIInformation(string pRPCEndpointName, string clientId, string clientSecret, string appObjectId, string tenantName, string tenantId, string resourceUrl, string authUrl, string tokenUrl, string redirectUrls)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			SafeStringToHGlobalUni safeStringToHGlobalUni2 = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(pRPCEndpointName);
				SafeStringToHGlobalUni safeStringToHGlobalUni3 = SafeStringToHGlobalUni.Create(clientId);
				safeStringToHGlobalUni2 = SafeStringToHGlobalUni.Create(clientSecret);
				SafeStringToHGlobalUni safeStringToHGlobalUni4 = SafeStringToHGlobalUni.Create(clientSecret);
				SafeStringToHGlobalUni safeStringToHGlobalUni5 = SafeStringToHGlobalUni.Create(clientSecret);
				SafeStringToHGlobalUni safeStringToHGlobalUni6 = SafeStringToHGlobalUni.Create(clientSecret);
				SafeStringToHGlobalUni safeStringToHGlobalUni7 = SafeStringToHGlobalUni.Create(clientSecret);
				SafeStringToHGlobalUni safeStringToHGlobalUni8 = SafeStringToHGlobalUni.Create(clientSecret);
				SafeStringToHGlobalUni safeStringToHGlobalUni9 = SafeStringToHGlobalUni.Create(clientSecret);
				SafeStringToHGlobalUni safeStringToHGlobalUni10 = SafeStringToHGlobalUni.Create(clientSecret);
				RemoteLogon.ThrowFriendlyRpcError(<Module>.NativeRpcSavePowerBIInformation(safeStringToHGlobalUni.ToPointer(), safeStringToHGlobalUni3.ToPointer(), safeStringToHGlobalUni2.ToPointer(), safeStringToHGlobalUni4.ToPointer(), safeStringToHGlobalUni5.ToPointer(), safeStringToHGlobalUni6.ToPointer(), safeStringToHGlobalUni7.ToPointer(), safeStringToHGlobalUni8.ToPointer(), safeStringToHGlobalUni9.ToPointer(), safeStringToHGlobalUni10.ToPointer()), null);
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
				if (safeStringToHGlobalUni2 != null)
				{
					safeStringToHGlobalUni2.Close();
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007310 File Offset: 0x00006710
		private unsafe static void ThrowFriendlyRpcError(int mainError, tagSAFEARRAY* pExtendedError)
		{
			if (pExtendedError != null)
			{
				uint num = (uint)(*(int*)(pExtendedError + 24L / (long)sizeof(tagSAFEARRAY)));
				if (num != 0U)
				{
					Exception ex = null;
					ushort** ptr = *(long*)(pExtendedError + 16L / (long)sizeof(tagSAFEARRAY));
					long num2 = (long)(num - 1U);
					long num3 = num2;
					if (num2 >= 0L)
					{
						do
						{
							ex = new Exception(new string(*(long*)(num3 * 8L / (long)sizeof(ushort*) + ptr)), ex);
							num3 -= 1L;
						}
						while (num3 >= 0L);
					}
					throw ex;
				}
			}
			Marshal.ThrowExceptionForHR(mainError);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000736C File Offset: 0x0000676C
		private unsafe static byte[] ConvertNativeArrayToManaged(tagSAFEARRAY* pNativeArray)
		{
			if (pNativeArray == null)
			{
				return null;
			}
			int num = *(int*)(pNativeArray + 24L / (long)sizeof(tagSAFEARRAY));
			byte[] array = new byte[num];
			Marshal.Copy((IntPtr)(*(long*)(pNativeArray + 16L / (long)sizeof(tagSAFEARRAY))), array, 0, num);
			return array;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000073A8 File Offset: 0x000067A8
		private unsafe static string[] ConvertNativeStringArrayToManaged(tagSAFEARRAY* pNativeArray)
		{
			if (pNativeArray == null)
			{
				return null;
			}
			int num = *(int*)(pNativeArray + 24L / (long)sizeof(tagSAFEARRAY));
			string[] array = new string[num];
			int num2 = 0;
			long num3 = 0L;
			long num4 = (long)num;
			if (0L < num4)
			{
				tagSAFEARRAY* ptr = pNativeArray + 16L / (long)sizeof(tagSAFEARRAY);
				do
				{
					array[num2] = new string(*(num3 * 8L + *(long*)ptr));
					num2++;
					num3 += 1L;
				}
				while (num3 < num4);
			}
			return array;
		}

		// Token: 0x060000B5 RID: 181
		[DllImport("ReportingServicesNativeClient.dll")]
		[MethodImpl(MethodImplOptions.ForwardRef)]
		private unsafe static extern int NativeRemoteLogon([MarshalAs(UnmanagedType.LPWStr)] string rpcEndpointName, int credentialsType, Guid dataSourceId, [MarshalAs(UnmanagedType.LPWStr)] string userName, [MarshalAs(UnmanagedType.LPWStr)] string domain, [MarshalAs(UnmanagedType.LPWStr)] string password, out SafeToken pImpToken, bool* remoteLogon);
	}
}
