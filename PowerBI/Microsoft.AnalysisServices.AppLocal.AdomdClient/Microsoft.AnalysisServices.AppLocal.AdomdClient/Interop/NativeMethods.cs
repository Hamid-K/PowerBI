using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000137 RID: 311
	internal static class NativeMethods
	{
		// Token: 0x06000FCE RID: 4046
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ActivateActCtx(IntPtr hCtx, out IntPtr lpCookie);

		// Token: 0x06000FCF RID: 4047
		[DllImport("kernel32.dll", EntryPoint = "CreateActCtxW", SetLastError = true)]
		public static extern IntPtr CreateActCtx(ref ACTCTX pActCtx);

		// Token: 0x06000FD0 RID: 4048
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeactivateActCtx(uint dwFlags, IntPtr lpCookie);

		// Token: 0x06000FD1 RID: 4049
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FreeLibrary(IntPtr hLibModule);

		// Token: 0x06000FD2 RID: 4050
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetComputerNameExW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetComputerNameEx(ComputerNameFormat NameType, StringBuilder lpBuffer, ref uint nSize);

		// Token: 0x06000FD3 RID: 4051
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetConsoleWindow();

		// Token: 0x06000FD4 RID: 4052
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetCurrentThread();

		// Token: 0x06000FD5 RID: 4053
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetModuleFileName(IntPtr hModule, [Out] StringBuilder lpBaseName, [MarshalAs(UnmanagedType.U4)] [In] int nSize);

		// Token: 0x06000FD6 RID: 4054
		[DllImport("kernel32", CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryW", SetLastError = true)]
		public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

		// Token: 0x06000FD7 RID: 4055
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern void ReleaseActCtx(IntPtr hCtx);

		// Token: 0x06000FD8 RID: 4056
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern int RtlGetVersion(ref OSVERSIONINFOEX lpVersionInformation);

		// Token: 0x06000FD9 RID: 4057
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ImpersonateAnonymousToken([In] IntPtr hThread);

		// Token: 0x06000FDA RID: 4058
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool RevertToSelf();

		// Token: 0x06000FDB RID: 4059
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "AcquireCredentialsHandleW", SetLastError = true)]
		public static extern int AcquireCredentialsHandle([In] string pPrincipal, [In] string pPackage, [In] uint fCredentialUse, [In] IntPtr pvLogonId, [In] IntPtr pAuthData, [In] IntPtr pGetKeyFn, [In] IntPtr pvGetKeyArgument, out SecHandle phCredential, out ulong ptsExpiry);

		// Token: 0x06000FDC RID: 4060
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "AcquireCredentialsHandleW", SetLastError = true)]
		public static extern int AcquireCredentialsHandle([In] string pPrincipal, [In] string pPackage, [In] uint fCredentialUse, [In] IntPtr pvLogonId, [In] ref SChannelCred pAuthData, [In] IntPtr pGetKeyFn, [In] IntPtr pvGetKeyArgument, out SecHandle phCredential, out ulong ptsExpiry);

		// Token: 0x06000FDD RID: 4061
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int DecryptMessage([In] ref SecHandle phContext, [In] [Out] ref SecBufferDesc pMessage, [In] uint MessageSeqNo, out uint pfQOP);

		// Token: 0x06000FDE RID: 4062
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int DeleteSecurityContext([In] ref SecHandle phContext);

		// Token: 0x06000FDF RID: 4063
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int EncryptMessage([In] ref SecHandle phContext, [In] uint fQOP, [In] [Out] ref SecBufferDesc pMessage, [In] uint MessageSeqNo);

		// Token: 0x06000FE0 RID: 4064
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "EnumerateSecurityPackagesW", SetLastError = true)]
		public static extern int EnumerateSecurityPackages(out uint pcPackages, out IntPtr ppPackageInfo);

		// Token: 0x06000FE1 RID: 4065
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int FreeContextBuffer([In] IntPtr pvContextBuffer);

		// Token: 0x06000FE2 RID: 4066
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int FreeCredentialsHandle([In] ref SecHandle phCredential);

		// Token: 0x06000FE3 RID: 4067
		[DllImport("Secur32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetUserNameExW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetUserNameEx(ExtendedNameFormat NameFormat, StringBuilder lpNameBuffer, ref uint nSize);

		// Token: 0x06000FE4 RID: 4068
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "InitializeSecurityContextW", SetLastError = true)]
		public static extern int InitializeSecurityContext([In] ref SecHandle phCredential, [In] IntPtr phContext, [In] string pTargetName, [In] uint fContextReq, [In] uint Reserved1, [In] int TargetDataRep, [In] IntPtr pInput, [In] uint Reserved2, out SecHandle phNewContext, [In] [Out] ref SecBufferDesc pOutput, out uint pfContextAttr, out ulong ptsExpiry);

		// Token: 0x06000FE5 RID: 4069
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "InitializeSecurityContextW", SetLastError = true)]
		public static extern int InitializeSecurityContext([In] ref SecHandle phCredential, [In] ref SecHandle phContext, [In] string pTargetName, [In] uint fContextReq, [In] uint Reserved1, [In] int TargetDataRep, [In] ref SecBufferDesc pInput, [In] uint Reserved2, [In] [Out] ref SecHandle phNewContext, [In] [Out] ref SecBufferDesc pOutput, out uint pfContextAttr, out ulong ptsExpiry);

		// Token: 0x06000FE6 RID: 4070
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int MakeSignature([In] ref SecHandle phContext, [In] uint fQOP, [In] [Out] ref SecBufferDesc pMessage, [In] uint MessageSeqNo);

		// Token: 0x06000FE7 RID: 4071
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "QueryContextAttributesW", SetLastError = true)]
		public static extern int QueryContextAttributes([In] ref SecHandle phContext, [In] uint ulAttribute, [In] IntPtr pBuffer);

		// Token: 0x06000FE8 RID: 4072
		[DllImport("secur32.dll", SetLastError = true)]
		public static extern int VerifySignature([In] ref SecHandle phContext, [In] [Out] ref SecBufferDesc pMessage, [In] uint MessageSeqNo, out uint pfQOP);

		// Token: 0x06000FE9 RID: 4073
		[DllImport("ntdsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DsMakeSpnW", SetLastError = true)]
		public static extern int DsMakeSpn([In] string ServiceClass, [In] string ServiceName, [In] string InstanceName, [In] ushort InstancePort, [In] string Referrer, [In] ref uint pcSpnLength, [Out] StringBuilder pszSpn);

		// Token: 0x06000FEA RID: 4074
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);
	}
}
