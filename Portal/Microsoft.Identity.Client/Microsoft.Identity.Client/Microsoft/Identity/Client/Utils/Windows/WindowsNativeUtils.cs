using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Utils.Windows
{
	// Token: 0x020001D6 RID: 470
	[Obsolete("This workaround for previous WAM broker implementation is not necessary with the improved broker.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class WindowsNativeUtils
	{
		// Token: 0x06001474 RID: 5236 RVA: 0x000457F9 File Offset: 0x000439F9
		public static bool IsElevatedUser()
		{
			return WindowsNativeUtils.IsUserAnAdmin();
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00045800 File Offset: 0x00043A00
		public static void InitializeProcessSecurity()
		{
			int num = WindowsNativeUtils.CoInitializeSecurity(IntPtr.Zero, -1, IntPtr.Zero, IntPtr.Zero, WindowsNativeUtils.RpcAuthnLevel.None, WindowsNativeUtils.RpcImpLevel.Impersonate, IntPtr.Zero, WindowsNativeUtils.EoAuthnCap.None, IntPtr.Zero);
			if (num != 0)
			{
				throw new MsalClientException("initialize_process_security_error", MsalErrorMessage.InitializeProcessSecurityError(string.Format("0x{0:x}", num)));
			}
		}

		// Token: 0x06001476 RID: 5238
		[DllImport("shell32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsUserAnAdmin();

		// Token: 0x06001477 RID: 5239
		[DllImport("ole32.dll")]
		private static extern int CoInitializeSecurity(IntPtr pVoid, int cAuthSvc, IntPtr asAuthSvc, IntPtr pReserved1, WindowsNativeUtils.RpcAuthnLevel level, WindowsNativeUtils.RpcImpLevel impers, IntPtr pAuthList, WindowsNativeUtils.EoAuthnCap dwCapabilities, IntPtr pReserved3);

		// Token: 0x02000463 RID: 1123
		private enum RpcAuthnLevel
		{
			// Token: 0x04001367 RID: 4967
			Default,
			// Token: 0x04001368 RID: 4968
			None,
			// Token: 0x04001369 RID: 4969
			Connect,
			// Token: 0x0400136A RID: 4970
			Call,
			// Token: 0x0400136B RID: 4971
			Pkt,
			// Token: 0x0400136C RID: 4972
			PktIntegrity,
			// Token: 0x0400136D RID: 4973
			PktPrivacy
		}

		// Token: 0x02000464 RID: 1124
		private enum RpcImpLevel
		{
			// Token: 0x0400136F RID: 4975
			Default,
			// Token: 0x04001370 RID: 4976
			Anonymous,
			// Token: 0x04001371 RID: 4977
			Identify,
			// Token: 0x04001372 RID: 4978
			Impersonate,
			// Token: 0x04001373 RID: 4979
			Delegate
		}

		// Token: 0x02000465 RID: 1125
		private enum EoAuthnCap
		{
			// Token: 0x04001375 RID: 4981
			None,
			// Token: 0x04001376 RID: 4982
			MutualAuth,
			// Token: 0x04001377 RID: 4983
			StaticCloaking = 32,
			// Token: 0x04001378 RID: 4984
			DynamicCloaking = 64,
			// Token: 0x04001379 RID: 4985
			AnyAuthority = 128,
			// Token: 0x0400137A RID: 4986
			MakeFullSIC = 256,
			// Token: 0x0400137B RID: 4987
			Default = 2048,
			// Token: 0x0400137C RID: 4988
			SecureRefs = 2,
			// Token: 0x0400137D RID: 4989
			AccessControl = 4,
			// Token: 0x0400137E RID: 4990
			AppID = 8,
			// Token: 0x0400137F RID: 4991
			Dynamic = 16,
			// Token: 0x04001380 RID: 4992
			RequireFullSIC = 512,
			// Token: 0x04001381 RID: 4993
			AutoImpersonate = 1024,
			// Token: 0x04001382 RID: 4994
			NoCustomMarshal = 8192,
			// Token: 0x04001383 RID: 4995
			DisableAAA = 4096
		}
	}
}
