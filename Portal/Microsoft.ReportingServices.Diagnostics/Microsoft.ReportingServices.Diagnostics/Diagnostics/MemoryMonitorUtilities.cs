using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000054 RID: 84
	internal sealed class MemoryMonitorUtilities
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000C158 File Offset: 0x0000A358
		public bool PerformGcIfTimePassed(bool useLongInterval, Stopwatch timer, out long kbReleasedFromGc)
		{
			long processPrivateKBytes = MemoryMonitorUtilities.GetProcessPrivateKBytes();
			long num = processPrivateKBytes;
			bool flag = false;
			long num2 = this.m_timeFinishedLastGc - this.m_timeStartedLastGc;
			long elapsedMilliseconds = timer.ElapsedMilliseconds;
			if (this.m_timeFinishedLastGc == 0L)
			{
				flag = true;
			}
			else
			{
				int num3 = 10;
				if (!useLongInterval)
				{
					num3 = 5;
				}
				long num4 = elapsedMilliseconds - this.m_timeFinishedLastGc;
				if (num2 * (long)num3 < num4)
				{
					flag = true;
				}
			}
			if (flag)
			{
				GC.Collect(GC.MaxGeneration);
				this.m_timeStartedLastGc = elapsedMilliseconds;
				this.m_timeFinishedLastGc = timer.ElapsedMilliseconds;
				this.m_numInducedGcs += 1L;
				num = MemoryMonitorUtilities.GetProcessPrivateKBytes();
			}
			kbReleasedFromGc = processPrivateKBytes - num;
			kbReleasedFromGc = Math.Max(0L, kbReleasedFromGc);
			return flag;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		private static long GetProcessPrivateKBytes()
		{
			long num;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				num = currentProcess.PrivateMemorySize64 / 1024L;
			}
			return num;
		}

		// Token: 0x040002C5 RID: 709
		private long m_timeStartedLastGc;

		// Token: 0x040002C6 RID: 710
		private long m_timeFinishedLastGc;

		// Token: 0x040002C7 RID: 711
		private long m_numInducedGcs;
	}
}
