using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003F0 RID: 1008
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		// Token: 0x0600236F RID: 9071
		[DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetWaitableTimer(SafeWaitHandle handle, ref ulong dueTime, int period, IntPtr mustBeZero, IntPtr mustBeZeroAlso, [MarshalAs(UnmanagedType.Bool)] bool resume);

		// Token: 0x06002370 RID: 9072
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CancelWaitableTimer(SafeWaitHandle handle);

		// Token: 0x06002371 RID: 9073
		[DllImport("kernel32", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeWaitHandle CreateWaitableTimer(IntPtr mustBeZero, [MarshalAs(UnmanagedType.Bool)] bool manualReset, string timerName);

		// Token: 0x06002372 RID: 9074
		[DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
		internal static extern void GetSystemTimeAsFileTime(out ulong time);

		// Token: 0x06002373 RID: 9075
		[DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeFileHandle CreateIoCompletionPort(SafeFileHandle mustBeInvalid, SafeFileHandle mustBeNull, IntPtr ignored, int mustBeZero);

		// Token: 0x06002374 RID: 9076
		[DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool PostQueuedCompletionStatus(SafeFileHandle completionPort, int numberOfBytesTransferred, IntPtr completionKey, NativeOverlapped* lpOverlapped);

		// Token: 0x06002375 RID: 9077
		[DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool GetQueuedCompletionStatus(SafeFileHandle completionPort, out int numberOfBytesTransferred, out IntPtr completionKey, out NativeOverlapped* lpOverlapped, int milliseconds);

		// Token: 0x06002376 RID: 9078
		[DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetThreadIOPendingFlag(SafeFileHandle thread, [MarshalAs(UnmanagedType.Bool)] out bool isPending);

		// Token: 0x06002377 RID: 9079
		[DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeFileHandle GetCurrentThread();

		// Token: 0x04001614 RID: 5652
		private const string KERNEL32 = "kernel32";
	}
}
