using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200001C RID: 28
	public struct GlobalThreadId : IEquatable<GlobalThreadId>
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002D21 File Offset: 0x00000F21
		public static GlobalThreadId CurrentThreadId
		{
			get
			{
				return new GlobalThreadId(Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002D3C File Offset: 0x00000F3C
		public GlobalThreadId(int processId, int managedThreadId)
		{
			this = default(GlobalThreadId);
			this.ProcessId = processId;
			this.ManagedThreadId = managedThreadId;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002D53 File Offset: 0x00000F53
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002D5B File Offset: 0x00000F5B
		public int ProcessId { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002D64 File Offset: 0x00000F64
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002D6C File Offset: 0x00000F6C
		public int ManagedThreadId { get; private set; }

		// Token: 0x06000072 RID: 114 RVA: 0x00002D75 File Offset: 0x00000F75
		public bool Equals(GlobalThreadId other)
		{
			return this.ProcessId == other.ProcessId && this.ManagedThreadId == other.ManagedThreadId;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D97 File Offset: 0x00000F97
		public override bool Equals(object obj)
		{
			return obj is GlobalThreadId && this.Equals((GlobalThreadId)obj);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public override int GetHashCode()
		{
			return this.ProcessId.GetHashCode() ^ this.ManagedThreadId.GetHashCode();
		}
	}
}
