using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x0200012C RID: 300
	internal static class NativeMethods
	{
		// Token: 0x0600105C RID: 4188
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ActivateActCtx(IntPtr hCtx, out IntPtr lpCookie);

		// Token: 0x0600105D RID: 4189
		[DllImport("kernel32.dll", EntryPoint = "CreateActCtxW", SetLastError = true)]
		public static extern IntPtr CreateActCtx(ref ACTCTX pActCtx);

		// Token: 0x0600105E RID: 4190
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeactivateActCtx(uint dwFlags, IntPtr lpCookie);

		// Token: 0x0600105F RID: 4191
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FreeLibrary(IntPtr hLibModule);

		// Token: 0x06001060 RID: 4192
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetComputerNameExW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetComputerNameEx(ComputerNameFormat NameType, StringBuilder lpBuffer, ref uint nSize);

		// Token: 0x06001061 RID: 4193
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetConsoleWindow();

		// Token: 0x06001062 RID: 4194
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetCurrentThread();

		// Token: 0x06001063 RID: 4195
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetModuleFileName(IntPtr hModule, [Out] StringBuilder lpBaseName, [MarshalAs(UnmanagedType.U4)] [In] int nSize);

		// Token: 0x06001064 RID: 4196
		[DllImport("kernel32", CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryW", SetLastError = true)]
		public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

		// Token: 0x06001065 RID: 4197
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern void ReleaseActCtx(IntPtr hCtx);

		// Token: 0x06001066 RID: 4198
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern int RtlGetVersion(ref OSVERSIONINFOEX lpVersionInformation);

		// Token: 0x06001067 RID: 4199
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ImpersonateAnonymousToken([In] IntPtr hThread);

		// Token: 0x06001068 RID: 4200
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool RevertToSelf();

		// Token: 0x06001069 RID: 4201
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "AcquireCredentialsHandleW", SetLastError = true)]
		public static extern int AcquireCredentialsHandle([In] string pPrincipal, [In] string pPackage, [In] uint fCredentialUse, [In] IntPtr pvLogonId, [In] IntPtr pAuthData, [In] IntPtr pGetKeyFn, [In] IntPtr pvGetKeyArgument, out SecHandle phCredential, out ulong ptsExpiry);

		// Token: 0x0600106A RID: 4202
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "AcquireCredentialsHandleW", SetLastError = true)]
		public static extern int AcquireCredentialsHandle([In] string pPrincipal, [In] string pPackage, [In] uint fCredentialUse, [In] IntPtr pvLogonId, [In] ref SChannelCred pAuthData, [In] IntPtr pGetKeyFn, [In] IntPtr pvGetKeyArgument, out SecHandle phCredential, out ulong ptsExpiry);

		// Token: 0x0600106B RID: 4203
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int DecryptMessage([In] ref SecHandle phContext, [In] [Out] ref SecBufferDesc pMessage, [In] uint MessageSeqNo, out uint pfQOP);

		// Token: 0x0600106C RID: 4204
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int DeleteSecurityContext([In] ref SecHandle phContext);

		// Token: 0x0600106D RID: 4205
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int EncryptMessage([In] ref SecHandle phContext, [In] uint fQOP, [In] [Out] ref SecBufferDesc pMessage, [In] uint MessageSeqNo);

		// Token: 0x0600106E RID: 4206
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "EnumerateSecurityPackagesW", SetLastError = true)]
		public static extern int EnumerateSecurityPackages(out uint pcPackages, out IntPtr ppPackageInfo);

		// Token: 0x0600106F RID: 4207
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int FreeContextBuffer([In] IntPtr pvContextBuffer);

		// Token: 0x06001070 RID: 4208
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int FreeCredentialsHandle([In] ref SecHandle phCredential);

		// Token: 0x06001071 RID: 4209
		[DllImport("Secur32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetUserNameExW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetUserNameEx(ExtendedNameFormat NameFormat, StringBuilder lpNameBuffer, ref uint nSize);

		// Token: 0x06001072 RID: 4210
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "InitializeSecurityContextW", SetLastError = true)]
		public static extern int InitializeSecurityContext([In] ref SecHandle phCredential, [In] IntPtr phContext, [In] string pTargetName, [In] uint fContextReq, [In] uint Reserved1, [In] int TargetDataRep, [In] IntPtr pInput, [In] uint Reserved2, out SecHandle phNewContext, [In] [Out] ref SecBufferDesc pOutput, out uint pfContextAttr, out ulong ptsExpiry);

		// Token: 0x06001073 RID: 4211
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "InitializeSecurityContextW", SetLastError = true)]
		public static extern int InitializeSecurityContext([In] ref SecHandle phCredential, [In] ref SecHandle phContext, [In] string pTargetName, [In] uint fContextReq, [In] uint Reserved1, [In] int TargetDataRep, [In] ref SecBufferDesc pInput, [In] uint Reserved2, [In] [Out] ref SecHandle phNewContext, [In] [Out] ref SecBufferDesc pOutput, out uint pfContextAttr, out ulong ptsExpiry);

		// Token: 0x06001074 RID: 4212
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int MakeSignature([In] ref SecHandle phContext, [In] uint fQOP, [In] [Out] ref SecBufferDesc pMessage, [In] uint MessageSeqNo);

		// Token: 0x06001075 RID: 4213
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "QueryContextAttributesW", SetLastError = true)]
		public static extern int QueryContextAttributes([In] ref SecHandle phContext, [In] uint ulAttribute, [In] IntPtr pBuffer);

		// Token: 0x06001076 RID: 4214
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int VerifySignature([In] ref SecHandle phContext, [In] [Out] ref SecBufferDesc pMessage, [In] uint MessageSeqNo, out uint pfQOP);

		// Token: 0x06001077 RID: 4215
		[DllImport("ntdsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DsMakeSpnW", SetLastError = true)]
		public static extern int DsMakeSpn([In] string ServiceClass, [In] string ServiceName, [In] string InstanceName, [In] ushort InstancePort, [In] string Referrer, [In] ref uint pcSpnLength, [Out] StringBuilder pszSpn);

		// Token: 0x06001078 RID: 4216
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);
	}
}
