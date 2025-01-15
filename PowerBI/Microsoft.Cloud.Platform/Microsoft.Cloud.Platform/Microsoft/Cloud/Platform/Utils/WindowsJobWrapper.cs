using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200028B RID: 651
	public class WindowsJobWrapper : IDisposable
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x0003D382 File Offset: 0x0003B582
		public WindowsJobWrapper(ulong memoryLimitInMBytes)
			: this(JobOptions.None)
		{
			ExtendedDiagnostics.EnsureArgument(memoryLimitInMBytes, "memoryLimitInMBytes", memoryLimitInMBytes > 0UL);
			this.MemoryLimitInMBytes = new ulong?(memoryLimitInMBytes);
			WindowsJobWrapper.SetMemoryLimit(this.handle, new ulong?(memoryLimitInMBytes));
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0003D3BC File Offset: 0x0003B5BC
		public WindowsJobWrapper(JobOptions options)
		{
			this.handle = new WindowsJobWrapper.SafeJobHandle(WindowsJobWrapper.CreateJobObject(IntPtr.Zero, null));
			WindowsJobWrapper.SetJobOptions(this.handle, options);
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0003D3EA File Offset: 0x0003B5EA
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x0003D3F2 File Offset: 0x0003B5F2
		internal ulong? MemoryLimitInMBytes { get; private set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0003D3FB File Offset: 0x0003B5FB
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x0003D403 File Offset: 0x0003B603
		internal ushort? CpuMinRate { get; private set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0003D40C File Offset: 0x0003B60C
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x0003D414 File Offset: 0x0003B614
		internal ushort? CpuMaxRate { get; private set; }

		// Token: 0x06001198 RID: 4504 RVA: 0x0003D420 File Offset: 0x0003B620
		public void AddProcess(Process process)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Process>(process, "process");
			ulong? num;
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation(string.Format("Job: Adding {0} to windows job({1}) with memory limit == {2}", process.Id, this.handle, ((this.MemoryLimitInMBytes != null) ? num.GetValueOrDefault().ToString() : null) ?? "<NULL>"));
			if (!WindowsJobWrapper.AssignProcessToJobObject(this.handle, process.Handle))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0003D4AC File Offset: 0x0003B6AC
		public bool TryChangeMemoryLimit(ulong newMemoryLimitInMBytes)
		{
			ulong? num = this.MemoryLimitInMBytes;
			if (num != null)
			{
				num = this.MemoryLimitInMBytes;
				if (!((newMemoryLimitInMBytes < num.GetValueOrDefault()) & (num != null)))
				{
					this.IncreaseMemoryLimit(newMemoryLimitInMBytes);
					return true;
				}
			}
			TraceSourceBase<CommonTrace> tracer = TraceSourceBase<CommonTrace>.Tracer;
			string text = "Job: Unable to change memory limit from {0}MB to {1}MB";
			num = this.MemoryLimitInMBytes;
			tracer.TraceInformation(string.Format(text, ((num != null) ? num.GetValueOrDefault().ToString() : null) ?? "<NULL>", newMemoryLimitInMBytes));
			return false;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0003D533 File Offset: 0x0003B733
		public void SetCpuAffinity(ulong affinityMask)
		{
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Job: Setting CPU Affinity Mask to " + affinityMask.ToString("X8"));
			WindowsJobWrapper.SetCpuAffinity(this.handle, new ulong?(affinityMask));
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0003D568 File Offset: 0x0003B768
		public void SetCpuMinMaxRate(ushort minRate, ushort maxRate)
		{
			if (minRate > 10000)
			{
				throw new ArgumentOutOfRangeException("minRate", minRate, "Parameter must be less less than or equal to 10000");
			}
			if (maxRate <= 0 || maxRate > 10000)
			{
				throw new ArgumentOutOfRangeException("maxRate", maxRate, "Parameter must be less less than or equal to 10000");
			}
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation(string.Format("Job: Setting CPU rate min = {0}, max = {1}", minRate, maxRate));
			this.CpuMinRate = new ushort?(minRate);
			this.CpuMaxRate = new ushort?(maxRate);
			WindowsJobWrapper.SetCpuMinMax(this.handle, minRate, maxRate);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0003D5FC File Offset: 0x0003B7FC
		public bool IsProcessInJobObject(Process process)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Process>(process, "process");
			bool flag;
			WindowsJobWrapper.IsProcessInJob(process.Handle, this.handle, out flag);
			return flag;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0003D62C File Offset: 0x0003B82C
		public virtual void Dispose()
		{
			WindowsJobWrapper.SafeJobHandle safeJobHandle = Interlocked.Exchange<WindowsJobWrapper.SafeJobHandle>(ref this.handle, null);
			if (safeJobHandle == null)
			{
				return;
			}
			safeJobHandle.Dispose();
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0003D650 File Offset: 0x0003B850
		protected void RemoveMemoryLimitForTestUse()
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<WindowsJobWrapper.SafeJobHandle>(this.handle, "handle");
			WindowsJobWrapper.SetMemoryLimit(this.handle, null);
		}

		// Token: 0x0600119F RID: 4511
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr CreateJobObject(IntPtr a, string lpName);

		// Token: 0x060011A0 RID: 4512
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool SetInformationJobObject(WindowsJobWrapper.SafeJobHandle job, JobObjectInfoType infoType, IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);

		// Token: 0x060011A1 RID: 4513
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool QueryInformationJobObject(WindowsJobWrapper.SafeJobHandle job, JobObjectInfoType infoType, IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength, UIntPtr lpReturnLength);

		// Token: 0x060011A2 RID: 4514
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool AssignProcessToJobObject(WindowsJobWrapper.SafeJobHandle job, IntPtr process);

		// Token: 0x060011A3 RID: 4515
		[DllImport("kernel32.dll")]
		private static extern bool IsProcessInJob(IntPtr process, WindowsJobWrapper.SafeJobHandle job, out bool result);

		// Token: 0x060011A4 RID: 4516 RVA: 0x0003D688 File Offset: 0x0003B888
		private static void SetJobOptions(WindowsJobWrapper.SafeJobHandle job, JobOptions options)
		{
			JOBOBJECT_EXTENDED_LIMIT_INFORMATION jobobject_EXTENDED_LIMIT_INFORMATION = WindowsJobWrapper.QueryExtendedInformation(job);
			if (options != JobOptions.None)
			{
				if ((options & JobOptions.SilentBreakawayOk) == JobOptions.SilentBreakawayOk)
				{
					jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags = jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags | 4096U;
				}
			}
			else
			{
				jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags = jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags & 4294963199U;
			}
			WindowsJobWrapper.SetExtendedInfo(job, jobobject_EXTENDED_LIMIT_INFORMATION);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0003D6E0 File Offset: 0x0003B8E0
		private static void SetMemoryLimit(WindowsJobWrapper.SafeJobHandle job, ulong? limit)
		{
			JOBOBJECT_EXTENDED_LIMIT_INFORMATION jobobject_EXTENDED_LIMIT_INFORMATION = WindowsJobWrapper.QueryExtendedInformation(job);
			if (limit != null)
			{
				jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags = jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags | 512U;
				jobobject_EXTENDED_LIMIT_INFORMATION.JobMemoryLimit = new UIntPtr(limit.Value * 1024UL * 1024UL);
			}
			else
			{
				jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags = jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags & 4294966783U;
				jobobject_EXTENDED_LIMIT_INFORMATION.JobMemoryLimit = UIntPtr.Zero;
			}
			WindowsJobWrapper.SetExtendedInfo(job, jobobject_EXTENDED_LIMIT_INFORMATION);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0003D768 File Offset: 0x0003B968
		private static void SetCpuAffinity(WindowsJobWrapper.SafeJobHandle job, ulong? affinityMask)
		{
			JOBOBJECT_EXTENDED_LIMIT_INFORMATION jobobject_EXTENDED_LIMIT_INFORMATION = WindowsJobWrapper.QueryExtendedInformation(job);
			if (affinityMask != null)
			{
				jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags = jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags | 16U;
				jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.Affinity = new UIntPtr(affinityMask.Value);
			}
			else
			{
				jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags = jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags & 4294967279U;
				jobobject_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.Affinity = UIntPtr.Zero;
			}
			WindowsJobWrapper.SetExtendedInfo(job, jobobject_EXTENDED_LIMIT_INFORMATION);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0003D7E4 File Offset: 0x0003B9E4
		private static void SetCpuMinMax(WindowsJobWrapper.SafeJobHandle job, ushort minRate, ushort maxRate)
		{
			if (minRate == 0)
			{
				JOBOBJECT_CPU_RATE_CONTROL_INFORMATION_HARD_CAP jobobject_CPU_RATE_CONTROL_INFORMATION_HARD_CAP = new JOBOBJECT_CPU_RATE_CONTROL_INFORMATION_HARD_CAP
				{
					ControlFlags = 5U,
					MaxRate = (uint)maxRate
				};
				WindowsJobWrapper.SetInformationJobObject<JOBOBJECT_CPU_RATE_CONTROL_INFORMATION_HARD_CAP>(job, JobObjectInfoType.CpuRateControlInformation, jobobject_CPU_RATE_CONTROL_INFORMATION_HARD_CAP);
				return;
			}
			JOBOBJECT_CPU_RATE_CONTROL_INFORMATION jobobject_CPU_RATE_CONTROL_INFORMATION = new JOBOBJECT_CPU_RATE_CONTROL_INFORMATION
			{
				ControlFlags = 17U,
				MinRate = minRate,
				MaxRate = maxRate
			};
			WindowsJobWrapper.SetInformationJobObject<JOBOBJECT_CPU_RATE_CONTROL_INFORMATION>(job, JobObjectInfoType.CpuRateControlInformation, jobobject_CPU_RATE_CONTROL_INFORMATION);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003D844 File Offset: 0x0003BA44
		private static void SetExtendedInfo(WindowsJobWrapper.SafeJobHandle job, JOBOBJECT_EXTENDED_LIMIT_INFORMATION extendedInfo)
		{
			WindowsJobWrapper.SetInformationJobObject<JOBOBJECT_EXTENDED_LIMIT_INFORMATION>(job, JobObjectInfoType.ExtendedLimitInformation, extendedInfo);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0003D850 File Offset: 0x0003BA50
		private static void SetInformationJobObject<T>(WindowsJobWrapper.SafeJobHandle job, JobObjectInfoType jobObjectType, T infoObject) where T : struct
		{
			WindowsJobWrapper.ValidateInfoObjectType<T>(jobObjectType);
			int num = Marshal.SizeOf<T>();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.AllocHGlobal(num);
				Marshal.StructureToPtr<T>(infoObject, intPtr, false);
				if (!WindowsJobWrapper.SetInformationJobObject(job, jobObjectType, intPtr, (uint)num))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0003D8B0 File Offset: 0x0003BAB0
		private static void ValidateInfoObjectType<T>(JobObjectInfoType jobObjectType) where T : struct
		{
			if (jobObjectType == JobObjectInfoType.ExtendedLimitInformation)
			{
				ExtendedDiagnostics.EnsureOperation(typeof(T) == typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION), "Expected input of type JOBOBJECT_EXTENDED_LIMIT_INFORMATION");
				return;
			}
			if (jobObjectType != JobObjectInfoType.CpuRateControlInformation)
			{
				ExtendedDiagnostics.EnsureOperation(false, string.Format("Unsupported JobObjectType: {0}", jobObjectType));
				return;
			}
			ExtendedDiagnostics.EnsureOperation(typeof(T) == typeof(JOBOBJECT_CPU_RATE_CONTROL_INFORMATION) || typeof(T) == typeof(JOBOBJECT_CPU_RATE_CONTROL_INFORMATION_HARD_CAP), "Expected input of type JOBOBJECT_CPU_RATE_CONTROL_INFORMATION");
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0003D948 File Offset: 0x0003BB48
		private static JOBOBJECT_EXTENDED_LIMIT_INFORMATION QueryExtendedInformation(WindowsJobWrapper.SafeJobHandle job)
		{
			int num = Marshal.SizeOf<JOBOBJECT_EXTENDED_LIMIT_INFORMATION>();
			IntPtr intPtr = IntPtr.Zero;
			JOBOBJECT_EXTENDED_LIMIT_INFORMATION jobobject_EXTENDED_LIMIT_INFORMATION;
			try
			{
				intPtr = Marshal.AllocHGlobal(num);
				if (!WindowsJobWrapper.QueryInformationJobObject(job, JobObjectInfoType.ExtendedLimitInformation, intPtr, (uint)num, UIntPtr.Zero))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				jobobject_EXTENDED_LIMIT_INFORMATION = (JOBOBJECT_EXTENDED_LIMIT_INFORMATION)Marshal.PtrToStructure(intPtr, typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return jobobject_EXTENDED_LIMIT_INFORMATION;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0003D9B4 File Offset: 0x0003BBB4
		private void IncreaseMemoryLimit(ulong newMemoryLimitInMBytes)
		{
			bool flag;
			if (this.MemoryLimitInMBytes != null)
			{
				ulong? memoryLimitInMBytes = this.MemoryLimitInMBytes;
				flag = (newMemoryLimitInMBytes >= memoryLimitInMBytes.GetValueOrDefault()) & (memoryLimitInMBytes != null);
			}
			else
			{
				flag = false;
			}
			ExtendedDiagnostics.EnsureOperation(flag, "Can only increase memory limit");
			if (newMemoryLimitInMBytes > this.MemoryLimitInMBytes.Value)
			{
				TraceSourceBase<CommonTrace>.Tracer.TraceInformation(string.Format("Job: Increasing memory limit from {0}MB to {1}MB", this.MemoryLimitInMBytes.Value, newMemoryLimitInMBytes));
				WindowsJobWrapper.SetMemoryLimit(this.handle, new ulong?(newMemoryLimitInMBytes));
				this.MemoryLimitInMBytes = new ulong?(newMemoryLimitInMBytes);
				return;
			}
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation(string.Format("Job: Keeping same memory limit = {0}MB", newMemoryLimitInMBytes));
		}

		// Token: 0x04000664 RID: 1636
		public const ushort CpuRateMaximumValue = 10000;

		// Token: 0x04000665 RID: 1637
		private const uint JOB_OBJECT_LIMIT_SILENT_BREAKAWAY_OK = 4096U;

		// Token: 0x04000666 RID: 1638
		private const uint JOB_OBJECT_LIMIT_JOB_MEMORY = 512U;

		// Token: 0x04000667 RID: 1639
		private const uint JOB_OBJECT_LIMIT_AFFINITY = 16U;

		// Token: 0x04000668 RID: 1640
		private const uint JOB_OBJECT_CPU_RATE_CONTROL_ENABLE = 1U;

		// Token: 0x04000669 RID: 1641
		private const uint JOB_OBJECT_CPU_RATE_CONTROL_HARD_CAP = 4U;

		// Token: 0x0400066A RID: 1642
		private const uint JOB_OBJECT_CPU_RATE_CONTROL_MIN_MAX_RATE = 16U;

		// Token: 0x0400066B RID: 1643
		private volatile WindowsJobWrapper.SafeJobHandle handle;

		// Token: 0x0200073E RID: 1854
		private sealed class SafeJobHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06002FE4 RID: 12260 RVA: 0x000A499B File Offset: 0x000A2B9B
			public SafeJobHandle(IntPtr handle)
				: base(true)
			{
				base.SetHandle(handle);
			}

			// Token: 0x06002FE5 RID: 12261 RVA: 0x000A49AB File Offset: 0x000A2BAB
			protected override bool ReleaseHandle()
			{
				return WindowsJobWrapper.SafeJobHandle.CloseHandle(this.handle);
			}

			// Token: 0x06002FE6 RID: 12262
			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			private static extern bool CloseHandle(IntPtr hObject);
		}
	}
}
