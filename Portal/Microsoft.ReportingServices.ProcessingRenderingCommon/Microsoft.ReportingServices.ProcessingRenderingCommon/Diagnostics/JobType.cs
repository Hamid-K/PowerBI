using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200006D RID: 109
	public class JobType
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000AD6A File Offset: 0x00008F6A
		public JobType(JobTypeEnum type)
			: this(type, JobSubTypeEnum.Unknown)
		{
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000AD74 File Offset: 0x00008F74
		public JobType(JobTypeEnum type, JobSubTypeEnum subType)
		{
			this.m_type = type;
			this.m_subType = subType;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000AD8A File Offset: 0x00008F8A
		public JobTypeEnum Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000AD92 File Offset: 0x00008F92
		public JobSubTypeEnum SubType
		{
			get
			{
				return this.m_subType;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000AD9A File Offset: 0x00008F9A
		public static JobType UserJobType
		{
			get
			{
				return new JobType(JobTypeEnum.User);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000ADA2 File Offset: 0x00008FA2
		public static JobType SystemJobType
		{
			get
			{
				return new JobType(JobTypeEnum.System);
			}
		}

		// Token: 0x04000188 RID: 392
		private JobTypeEnum m_type;

		// Token: 0x04000189 RID: 393
		private JobSubTypeEnum m_subType;
	}
}
