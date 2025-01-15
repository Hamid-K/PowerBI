using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x020000A1 RID: 161
	internal sealed class TestMemoryAuditProxy : MemoryAuditProxy
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000F797 File Offset: 0x0000D997
		public override long CurrentMemoryUsageKBytes
		{
			get
			{
				return this.CurrentFreeableMemoryKBytes;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		public override long CurrentFreeableMemoryKBytes
		{
			get
			{
				long num = 0L;
				List<byte[]> allocations = this.m_allocations;
				lock (allocations)
				{
					foreach (byte[] array in this.m_allocations)
					{
						num += (long)array.Length;
					}
				}
				return num / 1024L;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000F828 File Offset: 0x0000DA28
		public override long PendingFreeKBytes
		{
			get
			{
				if (!this.m_freeOnNextAlloc)
				{
					return 0L;
				}
				return this.m_freeTargetBytes;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000F83D File Offset: 0x0000DA3D
		public override double FreeOverhead
		{
			get
			{
				return Microsoft.ReportingServices.Diagnostics.FreeOverhead.None;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000F844 File Offset: 0x0000DA44
		public override void SetNewMemoryTarget(long memoryTargetKBytes)
		{
			Console.WriteLine("MAP {0} asked to release {1} KBytes", this.GetHashCode(), memoryTargetKBytes);
			object freeSync = this.m_freeSync;
			lock (freeSync)
			{
				this.m_freeOnNextAlloc = true;
				this.m_freeTargetBytes = memoryTargetKBytes * 1024L;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		public override bool CanPerformPartialRelease
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		public void Allocate(int bytes)
		{
			if (this.m_freeOnNextAlloc)
			{
				this.PerformFreeRequest();
			}
			byte[] array = new byte[bytes];
			List<byte[]> allocations = this.m_allocations;
			lock (allocations)
			{
				this.m_allocations.Add(array);
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000F914 File Offset: 0x0000DB14
		private void PerformFreeRequest()
		{
			object freeSync = this.m_freeSync;
			lock (freeSync)
			{
				if (this.m_freeOnNextAlloc)
				{
					List<byte[]> allocations = this.m_allocations;
					lock (allocations)
					{
						long num = this.CurrentFreeableMemoryKBytes * 1024L - this.m_freeTargetBytes;
						while (num > 0L && this.m_allocations.Count > 0)
						{
							byte[] array = this.m_allocations[this.m_allocations.Count - 1];
							num -= (long)array.Length;
							this.m_allocations.RemoveAt(this.m_allocations.Count - 1);
						}
						this.m_allocations.TrimExcess();
					}
					this.m_freeOnNextAlloc = false;
					this.m_freeTargetBytes = 0L;
				}
			}
		}

		// Token: 0x040002E4 RID: 740
		private List<byte[]> m_allocations = new List<byte[]>();

		// Token: 0x040002E5 RID: 741
		private readonly object m_freeSync = new object();

		// Token: 0x040002E6 RID: 742
		private volatile bool m_freeOnNextAlloc;

		// Token: 0x040002E7 RID: 743
		private long m_freeTargetBytes;
	}
}
