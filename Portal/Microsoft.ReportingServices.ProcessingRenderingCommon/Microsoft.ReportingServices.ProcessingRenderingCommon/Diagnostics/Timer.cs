using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x020000A6 RID: 166
	public sealed class Timer
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000100EC File Offset: 0x0000E2EC
		private long Value
		{
			get
			{
				long num = 0L;
				Timer.QueryPerformanceCounter(ref num);
				return num;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00010108 File Offset: 0x0000E308
		private long Frequency
		{
			get
			{
				long num = 0L;
				Timer.QueryPerformanceFrequency(ref num);
				return num;
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00010121 File Offset: 0x0000E321
		public void StartTimer()
		{
			this.m_start = true;
			this.m_LastValue = this.Value;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00010138 File Offset: 0x0000E338
		public long ElapsedTimeMs()
		{
			if (!this.m_start)
			{
				return 0L;
			}
			this.m_start = false;
			long lastValue = this.m_LastValue;
			long num = this.Value - lastValue;
			long num2 = (long)(1000f * (float)num / (float)this.Frequency);
			if (num2 < 0L && RSTrace.RunningJobsTrace.TraceWarning && Interlocked.Increment(ref Timer.m_valuesLessThanZero) == 1)
			{
				RSTrace.RunningJobsTrace.Trace(TraceLevel.Warning, "Timestamp values retrieved from current CPU are not synchronized with other CPUs");
			}
			return num2;
		}

		// Token: 0x0600051B RID: 1307
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool QueryPerformanceCounter([In] [Out] ref long lpPerformanceCount);

		// Token: 0x0600051C RID: 1308
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool QueryPerformanceFrequency([In] [Out] ref long lpFrequency);

		// Token: 0x040002F3 RID: 755
		private static int m_valuesLessThanZero;

		// Token: 0x040002F4 RID: 756
		private bool m_start;

		// Token: 0x040002F5 RID: 757
		private long m_LastValue;
	}
}
