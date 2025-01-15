using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Mashup.Common;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C50 RID: 7248
	internal class ContainerJob : IDisposable
	{
		// Token: 0x0600B4D5 RID: 46293 RVA: 0x0024A997 File Offset: 0x00248B97
		public void Dispose()
		{
			if (this.handle != null)
			{
				this.handle.Dispose();
				this.handle = null;
			}
		}

		// Token: 0x0600B4D6 RID: 46294 RVA: 0x0024A9B3 File Offset: 0x00248BB3
		private static bool MaxCommitIsApplicable(int maxCommitInMB)
		{
			return maxCommitInMB >= 0;
		}

		// Token: 0x0600B4D7 RID: 46295 RVA: 0x0024A9BC File Offset: 0x00248BBC
		public ContainerJob(int jobMaxCommitInMB, int processMaxCommitInMB, bool[] processorAffinity)
		{
			if (ContainerJob.MaxCommitIsApplicable(jobMaxCommitInMB) || ContainerJob.MaxCommitIsApplicable(processMaxCommitInMB) || processorAffinity != null)
			{
				IntPtr intPtr = ContainerJob.Interop.CreateJobObject(null, null);
				if (intPtr == IntPtr.Zero)
				{
					throw SystemException.CreateWin32SystemException("ContainerJob/Interop/CreateJobObject", "Cannot create job.");
				}
				this.handle = new ContainerJob.SafeJobHandle(intPtr);
				this.SetLimits(jobMaxCommitInMB, processMaxCommitInMB);
				this.SetProcessAffinity(processorAffinity);
			}
		}

		// Token: 0x17002D36 RID: 11574
		// (get) Token: 0x0600B4D8 RID: 46296 RVA: 0x0024AA22 File Offset: 0x00248C22
		public bool RequireProcessAttach
		{
			get
			{
				return this.handle != null;
			}
		}

		// Token: 0x0600B4D9 RID: 46297 RVA: 0x0024AA2D File Offset: 0x00248C2D
		public void AssociateProcess(ContainerProcess process)
		{
			if (this.RequireProcessAttach && !ContainerJob.Interop.AssignProcessToJobObject(this.handle.Handle, process.Handle.Handle))
			{
				throw SystemException.CreateWin32SystemException("ContainerJob/Interop/AssignProcessToJobObject", "Cannot associate process with job");
			}
		}

		// Token: 0x0600B4DA RID: 46298 RVA: 0x0024AA64 File Offset: 0x00248C64
		private void SetLimits(int jobMaxCommitInMB, int processMaxCommitInMB)
		{
			ContainerJob.Interop.JobObjectExtendedLimitInformation jobObjectExtendedLimitInformation = new ContainerJob.Interop.JobObjectExtendedLimitInformation
			{
				BasicLimitInformation = default(ContainerJob.Interop.JobObjectBasicLimitInformation)
			};
			if (ContainerJob.MaxCommitIsApplicable(jobMaxCommitInMB))
			{
				jobObjectExtendedLimitInformation.BasicLimitInformation.LimitFlags = jobObjectExtendedLimitInformation.BasicLimitInformation.LimitFlags | ContainerJob.Interop.JobObjectLimitFlags.JOB_MEMORY;
				jobObjectExtendedLimitInformation.JobMemoryLimit = new UIntPtr((ulong)((long)jobMaxCommitInMB * 1048576L));
			}
			if (ContainerJob.MaxCommitIsApplicable(processMaxCommitInMB))
			{
				jobObjectExtendedLimitInformation.BasicLimitInformation.LimitFlags = jobObjectExtendedLimitInformation.BasicLimitInformation.LimitFlags | ContainerJob.Interop.JobObjectLimitFlags.PROCESS_MEMORY;
				jobObjectExtendedLimitInformation.ProcessMemoryLimit = new UIntPtr((ulong)((long)processMaxCommitInMB * 1048576L));
			}
			if (jobObjectExtendedLimitInformation.BasicLimitInformation.LimitFlags != (ContainerJob.Interop.JobObjectLimitFlags)0U)
			{
				int num = Marshal.SizeOf(typeof(ContainerJob.Interop.JobObjectExtendedLimitInformation));
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				Marshal.StructureToPtr<ContainerJob.Interop.JobObjectExtendedLimitInformation>(jobObjectExtendedLimitInformation, intPtr, false);
				try
				{
					if (!ContainerJob.Interop.SetInformationJobObject(this.handle.Handle, ContainerJob.Interop.JobObjectInfoClass.ExtendedLimitInformation, intPtr, (uint)num))
					{
						throw SystemException.CreateWin32SystemException("ContainerJob/Interop/SetInformationJobObject", "Cannot set limit on job.");
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600B4DB RID: 46299 RVA: 0x0024AB58 File Offset: 0x00248D58
		private void SetProcessAffinity(bool[] affinity)
		{
			if (affinity == null)
			{
				return;
			}
			int activeProcessorGroupCount = MachineInfo.GetActiveProcessorGroupCount();
			int num = 0;
			ContainerJob.Interop.GROUP_AFFINITY[] array = new ContainerJob.Interop.GROUP_AFFINITY[activeProcessorGroupCount];
			short num2 = 0;
			while ((int)num2 < activeProcessorGroupCount)
			{
				array[(int)num2].Group = num2;
				int activeProcessorCount = MachineInfo.GetActiveProcessorCount((int)num2);
				long num3 = 0L;
				int num4 = 0;
				while (num4 < activeProcessorCount && num < affinity.Length)
				{
					if (affinity[num])
					{
						num3 |= 1L << num4;
					}
					num4++;
					num++;
				}
				if (IntPtr.Size == 4)
				{
					array[(int)num2].Mask = new IntPtr((int)num3);
				}
				else
				{
					array[(int)num2].Mask = new IntPtr(num3);
				}
				num2 += 1;
			}
			int num5 = Marshal.SizeOf(typeof(ContainerJob.Interop.GROUP_AFFINITY)) * array.Length;
			if (!ContainerJob.Interop.SetInformationJobObject(this.handle.Handle, ContainerJob.Interop.JobObjectInfoClass.GroupInformationEx, array, (uint)num5))
			{
				throw SystemException.CreateWin32SystemException("ContainerJob/Interop/SetInformationJobObject", "Cannot set process affinity on job.");
			}
		}

		// Token: 0x04005BE8 RID: 23528
		private const int MBInBytes = 1048576;

		// Token: 0x04005BE9 RID: 23529
		private ContainerJob.SafeJobHandle handle;

		// Token: 0x02001C51 RID: 7249
		private class SafeJobHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x0600B4DC RID: 46300 RVA: 0x0011064C File Offset: 0x0010E84C
			public SafeJobHandle(IntPtr jobHandle)
				: base(true)
			{
				base.SetHandle(jobHandle);
			}

			// Token: 0x17002D37 RID: 11575
			// (get) Token: 0x0600B4DD RID: 46301 RVA: 0x0024AC45 File Offset: 0x00248E45
			public IntPtr Handle
			{
				get
				{
					return this.handle;
				}
			}

			// Token: 0x0600B4DE RID: 46302 RVA: 0x0024AC4D File Offset: 0x00248E4D
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			protected override bool ReleaseHandle()
			{
				return ContainerJob.Interop.CloseHandle(this.handle);
			}
		}

		// Token: 0x02001C52 RID: 7250
		[SuppressUnmanagedCodeSecurity]
		private static class Interop
		{
			// Token: 0x0600B4DF RID: 46303
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern IntPtr CreateJobObject(ContainerJob.Interop.SecurityAttributes lpJobAttributes, string lpName);

			// Token: 0x0600B4E0 RID: 46304
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool AssignProcessToJobObject(IntPtr hJob, IntPtr hProcess);

			// Token: 0x0600B4E1 RID: 46305
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool SetInformationJobObject(IntPtr hJob, ContainerJob.Interop.JobObjectInfoClass jobObectInfoClass, IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);

			// Token: 0x0600B4E2 RID: 46306
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool SetInformationJobObject(IntPtr hJob, ContainerJob.Interop.JobObjectInfoClass jobObectInfoClass, ContainerJob.Interop.GROUP_AFFINITY[] lpJobObjectInfo, uint cbJobObjectInfoLength);

			// Token: 0x0600B4E3 RID: 46307
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool CloseHandle(IntPtr hObject);

			// Token: 0x02001C53 RID: 7251
			internal enum JobObjectInfoClass
			{
				// Token: 0x04005BEB RID: 23531
				AssociateCompletionPortInformation = 7,
				// Token: 0x04005BEC RID: 23532
				BasicLimitInformation = 2,
				// Token: 0x04005BED RID: 23533
				BasicUIRestrictions = 4,
				// Token: 0x04005BEE RID: 23534
				CpuRateControlInformation = 15,
				// Token: 0x04005BEF RID: 23535
				EndOfJobTimeInformation = 6,
				// Token: 0x04005BF0 RID: 23536
				ExtendedLimitInformation = 9,
				// Token: 0x04005BF1 RID: 23537
				GroupInformation = 11,
				// Token: 0x04005BF2 RID: 23538
				GroupInformationEx = 14,
				// Token: 0x04005BF3 RID: 23539
				LimitViolationInformation2 = 35,
				// Token: 0x04005BF4 RID: 23540
				NetRateControlInformation = 32,
				// Token: 0x04005BF5 RID: 23541
				NotificationLimitInformation,
				// Token: 0x04005BF6 RID: 23542
				NotificationLimitInformation2,
				// Token: 0x04005BF7 RID: 23543
				SecurityLimitInformation = 5
			}

			// Token: 0x02001C54 RID: 7252
			[Flags]
			internal enum JobObjectLimitFlags : uint
			{
				// Token: 0x04005BF9 RID: 23545
				ACTIVE_PROCESS = 8U,
				// Token: 0x04005BFA RID: 23546
				AFFINITY = 16U,
				// Token: 0x04005BFB RID: 23547
				BREAKAWAY_OK = 2048U,
				// Token: 0x04005BFC RID: 23548
				DIE_ON_UNHANDLED_EXCEPTION = 1024U,
				// Token: 0x04005BFD RID: 23549
				JOB_MEMORY = 512U,
				// Token: 0x04005BFE RID: 23550
				JOB_TIME = 4U,
				// Token: 0x04005BFF RID: 23551
				KILL_ON_JOB_CLOSE = 8192U,
				// Token: 0x04005C00 RID: 23552
				PRESERVE_JOB_TIME = 64U,
				// Token: 0x04005C01 RID: 23553
				PRIORITY_CLASS = 32U,
				// Token: 0x04005C02 RID: 23554
				PROCESS_MEMORY = 256U,
				// Token: 0x04005C03 RID: 23555
				PROCESS_TIME = 2U,
				// Token: 0x04005C04 RID: 23556
				SCHEDULING_CLASS = 128U,
				// Token: 0x04005C05 RID: 23557
				SILENT_BREAKAWAY_OK = 4096U,
				// Token: 0x04005C06 RID: 23558
				SUBSET_AFFINITY = 16384U,
				// Token: 0x04005C07 RID: 23559
				WORKINGSET = 1U
			}

			// Token: 0x02001C55 RID: 7253
			internal struct JobObjectBasicLimitInformation
			{
				// Token: 0x04005C08 RID: 23560
				public long PerProcessUserTimeLimit;

				// Token: 0x04005C09 RID: 23561
				public long PerJobUserTimeLimit;

				// Token: 0x04005C0A RID: 23562
				public ContainerJob.Interop.JobObjectLimitFlags LimitFlags;

				// Token: 0x04005C0B RID: 23563
				public UIntPtr MinimumWorkingSetSize;

				// Token: 0x04005C0C RID: 23564
				public UIntPtr MaximumWorkingSetSize;

				// Token: 0x04005C0D RID: 23565
				public uint ActiveProcessLimit;

				// Token: 0x04005C0E RID: 23566
				public UIntPtr Affinity;

				// Token: 0x04005C0F RID: 23567
				public uint PriorityClass;

				// Token: 0x04005C10 RID: 23568
				public uint SchedulingClass;
			}

			// Token: 0x02001C56 RID: 7254
			internal struct JobObjectExtendedLimitInformation
			{
				// Token: 0x04005C11 RID: 23569
				public ContainerJob.Interop.JobObjectBasicLimitInformation BasicLimitInformation;

				// Token: 0x04005C12 RID: 23570
				private ContainerJob.Interop.IoCounters IoInfo;

				// Token: 0x04005C13 RID: 23571
				public UIntPtr ProcessMemoryLimit;

				// Token: 0x04005C14 RID: 23572
				public UIntPtr JobMemoryLimit;

				// Token: 0x04005C15 RID: 23573
				public UIntPtr PeakProcessMemoryUsed;

				// Token: 0x04005C16 RID: 23574
				public UIntPtr PeakJobMemoryUsed;
			}

			// Token: 0x02001C57 RID: 7255
			internal struct GROUP_AFFINITY
			{
				// Token: 0x04005C17 RID: 23575
				public IntPtr Mask;

				// Token: 0x04005C18 RID: 23576
				public short Group;

				// Token: 0x04005C19 RID: 23577
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
				public short[] Reserved;
			}

			// Token: 0x02001C58 RID: 7256
			[StructLayout(LayoutKind.Sequential)]
			internal class SecurityAttributes
			{
				// Token: 0x0600B4E4 RID: 46308 RVA: 0x0024AC5A File Offset: 0x00248E5A
				public SecurityAttributes()
				{
					this.nLength = (uint)Marshal.SizeOf(typeof(ContainerJob.Interop.SecurityAttributes));
					this.lpSecurityDescriptor = IntPtr.Zero;
					this.bInheritHandle = true;
				}

				// Token: 0x04005C1A RID: 23578
				public uint nLength;

				// Token: 0x04005C1B RID: 23579
				public IntPtr lpSecurityDescriptor;

				// Token: 0x04005C1C RID: 23580
				public bool bInheritHandle;
			}

			// Token: 0x02001C59 RID: 7257
			internal struct IoCounters
			{
				// Token: 0x04005C1D RID: 23581
				public ulong ReadOperationCount;

				// Token: 0x04005C1E RID: 23582
				public ulong WriteOperationCount;

				// Token: 0x04005C1F RID: 23583
				public ulong OtherOperationCount;

				// Token: 0x04005C20 RID: 23584
				public ulong ReadTransferCount;

				// Token: 0x04005C21 RID: 23585
				public ulong WriteTransferCount;

				// Token: 0x04005C22 RID: 23586
				public ulong OtherTransferCount;
			}
		}
	}
}
