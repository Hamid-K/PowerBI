using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000046 RID: 70
	public static class ProcessBinder
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000F984 File Offset: 0x0000DB84
		public static void Bind(int processId)
		{
			ProcessBinder.Bind_Internal(processId, ProcessBinder._jobObject);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000F991 File Offset: 0x0000DB91
		private static void Bind_Internal(int processId, SafeHandle jobObject)
		{
			if (!Win32.AssignProcessToJobObject(jobObject, Process.GetProcessById(processId).Handle))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error(), string.Format("Unable to associate process {0} with JobObject", processId));
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		private static SafeHandle GetJobObject()
		{
			SafeHandle safeHandle = Win32.CreateJobObject(IntPtr.Zero, null);
			if (safeHandle.IsInvalid)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error(), "Unable to initialize monitoring of processes");
			}
			Win32.JobObjectBasicLimitInfo jobObjectBasicLimitInfo = new Win32.JobObjectBasicLimitInfo
			{
				LimitFlags = 8192U
			};
			Win32.JobObjectExtendedLimitInfo jobObjectExtendedLimitInfo = new Win32.JobObjectExtendedLimitInfo
			{
				BasicLimitInfo = jobObjectBasicLimitInfo
			};
			int num = Marshal.SizeOf(typeof(Win32.JobObjectExtendedLimitInfo));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr<Win32.JobObjectExtendedLimitInfo>(jobObjectExtendedLimitInfo, intPtr, false);
			SafeUnmanagedMemoryHandle safeUnmanagedMemoryHandle = new SafeUnmanagedMemoryHandle(intPtr, true);
			if (!Win32.SetInformationJobObject(safeHandle, Win32.JobObjectInfoType.ExtendedLimitInformation, safeUnmanagedMemoryHandle, (uint)num))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error(), "Unable to set information");
			}
			ProcessBinder.Bind_Internal(Process.GetCurrentProcess().Id, safeHandle);
			return safeHandle;
		}

		// Token: 0x04000147 RID: 327
		private static readonly SafeHandle _jobObject = ProcessBinder.GetJobObject();
	}
}
