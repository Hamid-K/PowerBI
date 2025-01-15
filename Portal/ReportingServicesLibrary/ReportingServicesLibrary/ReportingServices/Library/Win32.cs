using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002E6 RID: 742
	public static class Win32
	{
		// Token: 0x06001A8B RID: 6795
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern SafeFileHandle CreateJobObject(IntPtr jobAttributes, string name);

		// Token: 0x06001A8C RID: 6796
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetInformationJobObject(SafeHandle hJob, Win32.JobObjectInfoType infoType, SafeHandle lpJobObjectInfo, uint cbJobObjectInfoLength);

		// Token: 0x06001A8D RID: 6797
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool AssignProcessToJobObject(SafeHandle job, IntPtr process);

		// Token: 0x04000981 RID: 2433
		public const int JobObjectLimitKillOnJobClose = 8192;

		// Token: 0x020004EC RID: 1260
		public struct JobObjectBasicLimitInfo
		{
			// Token: 0x0400114D RID: 4429
			public long PerProcessUserTimeLimit;

			// Token: 0x0400114E RID: 4430
			public long PerJobUserTimeLimit;

			// Token: 0x0400114F RID: 4431
			public uint LimitFlags;

			// Token: 0x04001150 RID: 4432
			public UIntPtr MinimumWorkingSetSize;

			// Token: 0x04001151 RID: 4433
			public UIntPtr MaximumWorkingSetSize;

			// Token: 0x04001152 RID: 4434
			public uint ActiveProcessLimit;

			// Token: 0x04001153 RID: 4435
			public UIntPtr Affinity;

			// Token: 0x04001154 RID: 4436
			public uint PriorityClass;

			// Token: 0x04001155 RID: 4437
			public uint SchedulingClass;
		}

		// Token: 0x020004ED RID: 1261
		public struct JobObjectExtendedLimitInfo
		{
			// Token: 0x04001156 RID: 4438
			public Win32.JobObjectBasicLimitInfo BasicLimitInfo;

			// Token: 0x04001157 RID: 4439
			public Win32.IoCounters IoInfo;

			// Token: 0x04001158 RID: 4440
			public UIntPtr ProcessMemoryLimit;

			// Token: 0x04001159 RID: 4441
			public UIntPtr JobMemoryLimit;

			// Token: 0x0400115A RID: 4442
			public UIntPtr PeakProcessMemoryUsed;

			// Token: 0x0400115B RID: 4443
			public UIntPtr PeakJobMemoryUsed;
		}

		// Token: 0x020004EE RID: 1262
		public struct IoCounters
		{
			// Token: 0x0400115C RID: 4444
			public ulong ReadOperationCount;

			// Token: 0x0400115D RID: 4445
			public ulong WriteOperationCount;

			// Token: 0x0400115E RID: 4446
			public ulong OtherOperationCount;

			// Token: 0x0400115F RID: 4447
			public ulong ReadTransferCount;

			// Token: 0x04001160 RID: 4448
			public ulong WriteTransferCount;

			// Token: 0x04001161 RID: 4449
			public ulong OtherTransferCount;
		}

		// Token: 0x020004EF RID: 1263
		public enum JobObjectInfoType
		{
			// Token: 0x04001163 RID: 4451
			AssociateCompletionPortInformation = 7,
			// Token: 0x04001164 RID: 4452
			BasicLimitInformation = 2,
			// Token: 0x04001165 RID: 4453
			BasicUIRestrictions = 4,
			// Token: 0x04001166 RID: 4454
			EndOfJobTimeInformation = 6,
			// Token: 0x04001167 RID: 4455
			ExtendedLimitInformation = 9,
			// Token: 0x04001168 RID: 4456
			SecurityLimitInformation = 5,
			// Token: 0x04001169 RID: 4457
			GroupInformation = 11
		}
	}
}
