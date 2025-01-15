using System;
using System.Runtime.InteropServices;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x020006FC RID: 1788
	internal class NativeMethods
	{
		// Token: 0x060038D3 RID: 14547
		[DllImport("kernel32")]
		internal static extern bool SetDllDirectory(string newDirectory);

		// Token: 0x060038D4 RID: 14548
		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		internal static extern int AddDllDirectory([MarshalAs(UnmanagedType.LPWStr)] string newDirectory);

		// Token: 0x060038D5 RID: 14549
		[DllImport("MSHISXAC.DLL", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int RegisterRecoveryInformation([MarshalAs(UnmanagedType.BStr)] string recoveryInformation, IntPtr typeHdl, out int cookie);

		// Token: 0x060038D6 RID: 14550
		[DllImport("MSHISXAC.DLL", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int EnlistInXaTransaction(object o, int cookie, out int formatId, out int transactionIdLength, out int branchQualifierLength, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] out byte[] data);

		// Token: 0x060038D7 RID: 14551
		[DllImport("MSHISXAC.DLL", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int InitializeAppDomain([MarshalAs(UnmanagedType.BStr)] string appDomain, out IntPtr typeHdl);

		// Token: 0x060038D8 RID: 14552
		[DllImport("MSHISXAC.DLL", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int UninitializeAppDomain(IntPtr typeHdl);

		// Token: 0x040020F6 RID: 8438
		internal const string UnmanagedCodeDll = "MSHISXAC.DLL";
	}
}
