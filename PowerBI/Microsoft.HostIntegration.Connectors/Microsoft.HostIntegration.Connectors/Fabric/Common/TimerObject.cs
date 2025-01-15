using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003E6 RID: 998
	internal abstract class TimerObject
	{
		// Token: 0x06002300 RID: 8960
		protected abstract void OnTimerElapsed();

		// Token: 0x06002301 RID: 8961 RVA: 0x0006BBA0 File Offset: 0x00069DA0
		protected TimerObject(TimeSpan timespan)
			: this(FileTime.FromTimeSpan(timespan))
		{
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x0006BBAE File Offset: 0x00069DAE
		protected TimerObject(FileTime fileTime)
		{
			this.m_expirationTime = fileTime;
			this.m_enqueuedVersion = 0L;
			this.MarkAsDequeued();
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0006BBCB File Offset: 0x00069DCB
		internal void FireTimer()
		{
			this.OnTimerElapsed();
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x0006BBD3 File Offset: 0x00069DD3
		// (set) Token: 0x06002305 RID: 8965 RVA: 0x0006BBDB File Offset: 0x00069DDB
		internal TimerObject Next
		{
			get
			{
				return this.m_next;
			}
			set
			{
				this.m_next = value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x0006BBE4 File Offset: 0x00069DE4
		// (set) Token: 0x06002307 RID: 8967 RVA: 0x0006BBEC File Offset: 0x00069DEC
		internal TimerObject Prev
		{
			get
			{
				return this.m_prev;
			}
			set
			{
				this.m_prev = value;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x0006BBF5 File Offset: 0x00069DF5
		internal long EnqueuedVersion
		{
			get
			{
				return this.m_enqueuedVersion;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x0006BBFD File Offset: 0x00069DFD
		public bool IsEnqueued
		{
			get
			{
				return this.m_next != this && this.m_prev != this;
			}
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0006BC16 File Offset: 0x00069E16
		internal void MarkAsEnqueued()
		{
			this.m_enqueuedVersion += 1L;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0006BC28 File Offset: 0x00069E28
		internal void MarkAsDequeued()
		{
			this.m_prev = this;
			this.m_next = this;
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x0006BC45 File Offset: 0x00069E45
		// (set) Token: 0x0600230D RID: 8973 RVA: 0x0006BC4D File Offset: 0x00069E4D
		public FileTime ExpirationTime
		{
			get
			{
				return this.m_expirationTime;
			}
			set
			{
				if (!this.IsEnqueued)
				{
					this.m_expirationTime = value;
					return;
				}
				throw new InvalidOperationException("Expiration cannot be modified when the timer is enqueued");
			}
		}

		// Token: 0x040015E7 RID: 5607
		private TimerObject m_next;

		// Token: 0x040015E8 RID: 5608
		private TimerObject m_prev;

		// Token: 0x040015E9 RID: 5609
		private FileTime m_expirationTime;

		// Token: 0x040015EA RID: 5610
		private long m_enqueuedVersion;
	}
}
