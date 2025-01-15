using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace Microsoft.Data.Common
{
	// Token: 0x02000185 RID: 389
	[SuppressUnmanagedCodeSecurity]
	internal static class SafeNativeMethods
	{
		// Token: 0x06001D05 RID: 7429
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("ole32.dll")]
		internal static extern IntPtr CoTaskMemAlloc(IntPtr cb);

		// Token: 0x06001D06 RID: 7430
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("ole32.dll")]
		internal static extern void CoTaskMemFree(IntPtr handle);

		// Token: 0x06001D07 RID: 7431
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetUserDefaultLCID();

		// Token: 0x06001D08 RID: 7432
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		internal static extern void ZeroMemory(IntPtr dest, IntPtr length);

		// Token: 0x06001D09 RID: 7433 RVA: 0x000764F4 File Offset: 0x000746F4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal unsafe static IntPtr InterlockedExchangePointer(IntPtr lpAddress, IntPtr lpValue)
		{
			IntPtr intPtr = *(IntPtr*)lpAddress.ToPointer();
			IntPtr intPtr2;
			do
			{
				intPtr2 = intPtr;
				intPtr = Interlocked.CompareExchange(ref *(IntPtr*)lpAddress.ToPointer(), lpValue, intPtr2);
			}
			while (intPtr != intPtr2);
			return intPtr;
		}

		// Token: 0x06001D0A RID: 7434
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetComputerNameExW", SetLastError = true)]
		internal static extern int GetComputerNameEx(int nameType, StringBuilder nameBuffer, ref int bufferSize);

		// Token: 0x06001D0B RID: 7435
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetCurrentProcessId();

		// Token: 0x06001D0C RID: 7436
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPTStr)] [In] string moduleName);

		// Token: 0x06001D0D RID: 7437
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr GetProcAddress(IntPtr HModule, [MarshalAs(UnmanagedType.LPStr)] [In] string funcName);

		// Token: 0x06001D0E RID: 7438
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LocalAlloc(int flags, IntPtr countOfBytes);

		// Token: 0x06001D0F RID: 7439
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x06001D10 RID: 7440
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr SysAllocStringLen(string src, int len);

		// Token: 0x06001D11 RID: 7441
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("oleaut32.dll")]
		internal static extern void SysFreeString(IntPtr bstr);

		// Token: 0x06001D12 RID: 7442
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		private static extern void SetErrorInfo(int dwReserved, IntPtr pIErrorInfo);

		// Token: 0x06001D13 RID: 7443
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int ReleaseSemaphore(IntPtr handle, int releaseCount, IntPtr previousCount);

		// Token: 0x06001D14 RID: 7444
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int WaitForMultipleObjectsEx(uint nCount, IntPtr lpHandles, bool bWaitAll, uint dwMilliseconds, bool bAlertable);

		// Token: 0x06001D15 RID: 7445
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll")]
		internal static extern int WaitForSingleObjectEx(IntPtr lpHandles, uint dwMilliseconds, bool bAlertable);

		// Token: 0x06001D16 RID: 7446
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("ole32.dll", PreserveSig = false)]
		internal static extern void PropVariantClear(IntPtr pObject);

		// Token: 0x06001D17 RID: 7447
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("oleaut32.dll", PreserveSig = false)]
		internal static extern void VariantClear(IntPtr pObject);

		// Token: 0x02000280 RID: 640
		internal sealed class Wrapper
		{
			// Token: 0x06001F4B RID: 8011 RVA: 0x000027D1 File Offset: 0x000009D1
			private Wrapper()
			{
			}

			// Token: 0x06001F4C RID: 8012 RVA: 0x0007FCB0 File Offset: 0x0007DEB0
			internal static void ClearErrorInfo()
			{
				SafeNativeMethods.SetErrorInfo(0, ADP.s_ptrZero);
			}
		}
	}
}
