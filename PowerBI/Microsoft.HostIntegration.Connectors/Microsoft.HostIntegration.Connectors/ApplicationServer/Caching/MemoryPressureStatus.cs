using System;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200009E RID: 158
	internal class MemoryPressureStatus
	{
		// Token: 0x06000398 RID: 920 RVA: 0x00012B08 File Offset: 0x00010D08
		public MemoryPressureStatus()
		{
			MEMORYSTATUSEX memorystatusex = default(MEMORYSTATUSEX);
			MemoryStatus.GetFullMemoryStatus(ref memorystatusex);
			this._physicalSystemMemory = memorystatusex.ullTotalPhys;
			ulong ullTotalVirtual = memorystatusex.ullTotalVirtual;
			if (IntPtr.Size == 8)
			{
				this._memoryLimit = 1099511627776UL;
			}
			else if (ullTotalVirtual > (ulong)(-2147483648))
			{
				this._memoryLimit = (ulong)(-1825361101);
			}
			else
			{
				this._memoryLimit = 1395864371UL;
			}
			this._memoryLimit = Math.Min(this._physicalSystemMemory, this._memoryLimit);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00012B9A File Offset: 0x00010D9A
		public MemoryPressureStatus(int highMemoryMark)
			: this()
		{
			this._highMemoryMark = highMemoryMark;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00012BAC File Offset: 0x00010DAC
		public ClientMemoryPressureLevel GetMemoryPressure()
		{
			ulong privateMemorySize = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;
			int num = (int)(privateMemorySize * 100UL / this._memoryLimit);
			if (num > this._highMemoryMark)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning("DistributedCache.ClientMemoryMonitor", "Private Bytes {0}, Memory Limit Mark: {1}", new object[] { privateMemorySize, this._memoryLimit });
				}
				return ClientMemoryPressureLevel.High;
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.ClientMemoryMonitor", "Private Bytes {0}, Memory Limit Mark: {1}", new object[] { privateMemorySize, this._memoryLimit });
			}
			return ClientMemoryPressureLevel.Low;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00012C4A File Offset: 0x00010E4A
		internal ulong MemoryLimit
		{
			get
			{
				return this._memoryLimit;
			}
		}

		// Token: 0x040002D1 RID: 721
		private ulong _memoryLimit;

		// Token: 0x040002D2 RID: 722
		private ulong _physicalSystemMemory;

		// Token: 0x040002D3 RID: 723
		private int _highMemoryMark = 85;
	}
}
