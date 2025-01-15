using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000053 RID: 83
	internal static class ResourceUtilities
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000BFC3 File Offset: 0x0000A1C3
		public static int UpdateStatsTimerCycle
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		private static int BitsSet(ulong nr)
		{
			int num = 0;
			ulong num2 = 1UL;
			do
			{
				if ((nr & num2) == num2)
				{
					num++;
				}
				num2 <<= 1;
			}
			while (num2 != 0UL);
			return num;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000BFEC File Offset: 0x0000A1EC
		private static ulong ResetHigherBits(ulong nr, int bitsToKeep)
		{
			int num = 0;
			ulong num2 = 1UL;
			do
			{
				if ((nr & num2) == num2)
				{
					num++;
				}
				if (num > bitsToKeep)
				{
					nr &= ~num2;
				}
				num2 <<= 1;
			}
			while (num2 != 0UL);
			return nr;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000C01C File Offset: 0x0000A21C
		public static long MaxMemoryThresholdMB
		{
			get
			{
				if (ResourceUtilities.m_maxMemoryThresholdMB == -1L)
				{
					ResourceUtilities.m_maxMemoryThresholdMB = ProcessingContext.Configuration.MaxMemoryThresholdMB;
					if (RSTrace.ResourceUtilTrace.TraceInfo)
					{
						RSTrace.ResourceUtilTrace.Trace(TraceLevel.Info, "Maximum memory limit is {0}Mb", new object[] { ResourceUtilities.m_maxMemoryThresholdMB });
					}
				}
				return ResourceUtilities.m_maxMemoryThresholdMB;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000C075 File Offset: 0x0000A275
		public static bool TooMuchMemory
		{
			get
			{
				return !Globals.NoMemoryThrottling && ResourceUtilities.PrivateMBytes > ResourceUtilities.MaxMemoryThresholdMB;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000C08C File Offset: 0x0000A28C
		public static long PrivateMBytes
		{
			get
			{
				object privateMBytesLock = ResourceUtilities.m_privateMBytesLock;
				long privateMBytes;
				lock (privateMBytesLock)
				{
					privateMBytes = ResourceUtilities.m_privateMBytes;
				}
				return privateMBytes;
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		public static void UpdatePrivateMBytes()
		{
			object privateMBytesLock = ResourceUtilities.m_privateMBytesLock;
			lock (privateMBytesLock)
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					ResourceUtilities.m_privateMBytes = currentProcess.PrivateMemorySize64 / 1024L / 1024L;
				}
			}
		}

		// Token: 0x040002C2 RID: 706
		private static long m_maxMemoryThresholdMB = -1L;

		// Token: 0x040002C3 RID: 707
		private static long m_privateMBytes = 0L;

		// Token: 0x040002C4 RID: 708
		private static object m_privateMBytesLock = new object();
	}
}
