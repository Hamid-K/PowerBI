using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000009 RID: 9
	internal abstract class QueueItem
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00004260 File Offset: 0x00002460
		public QueueItem()
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000427E File Offset: 0x0000247E
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00004286 File Offset: 0x00002486
		public virtual Guid ID
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000428F File Offset: 0x0000248F
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00004297 File Offset: 0x00002497
		public DateTime TimeEntered
		{
			get
			{
				return this.m_timeEntered;
			}
			set
			{
				this.m_timeEntered = value;
			}
		}

		// Token: 0x06000067 RID: 103
		public abstract string ItemString();

		// Token: 0x04000077 RID: 119
		private Guid m_id = Guid.Empty;

		// Token: 0x04000078 RID: 120
		private DateTime m_timeEntered = DateTime.MinValue;
	}
}
