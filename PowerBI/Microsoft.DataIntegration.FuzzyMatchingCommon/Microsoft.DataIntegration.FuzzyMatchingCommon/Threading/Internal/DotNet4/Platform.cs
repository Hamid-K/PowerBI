using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Threading.Internal.DotNet4
{
	// Token: 0x0200002A RID: 42
	internal static class Platform
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000FF94 File Offset: 0x0000E194
		internal static int ProcessorCount
		{
			get
			{
				if (DateTime.UtcNow.CompareTo(Platform.s_nextProcessorCountRefreshTime) >= 0)
				{
					UIntPtr uintPtr;
					UIntPtr uintPtr2;
					NativeMethods.GetProcessAffinityMask(NativeMethods.GetCurrentProcess(), out uintPtr, out uintPtr2);
					NativeMethods.SYSTEM_INFO system_INFO = default(NativeMethods.SYSTEM_INFO);
					NativeMethods.GetSystemInfo(ref system_INFO);
					ulong num = (ulong.MaxValue >> 64 - system_INFO.dwNumberOfProcessors) & uintPtr.ToUInt64();
					int num2 = 0;
					while (num > 0UL)
					{
						num2++;
						num &= num - 1UL;
					}
					Platform.s_processorCount = num2;
					Platform.s_nextProcessorCountRefreshTime = DateTime.UtcNow.AddMilliseconds(30000.0);
				}
				return Platform.s_processorCount;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0001002F File Offset: 0x0000E22F
		internal static bool IsSingleProcessor
		{
			get
			{
				return Platform.ProcessorCount == 1;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00010039 File Offset: 0x0000E239
		internal static void Yield()
		{
			NativeMethods.SwitchToThread();
		}

		// Token: 0x0400002B RID: 43
		private const int PROCESSOR_COUNT_REFRESH_INTERVAL_MS = 30000;

		// Token: 0x0400002C RID: 44
		private static int s_processorCount = -1;

		// Token: 0x0400002D RID: 45
		private static DateTime s_nextProcessorCountRefreshTime = DateTime.MinValue;
	}
}
