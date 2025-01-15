using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000092 RID: 146
	internal sealed class DataShapeLimit
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x000268C1 File Offset: 0x00024AC1
		internal DataShapeLimit(string id, LimitOperator op, string target, string within)
		{
			this.m_id = id;
			this.m_operator = op;
			this.m_target = target;
			this.m_within = within;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x000268E6 File Offset: 0x00024AE6
		public string ClientID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x000268EE File Offset: 0x00024AEE
		public LimitOperator Operator
		{
			get
			{
				return this.m_operator;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x000268F6 File Offset: 0x00024AF6
		public bool Exceeded
		{
			get
			{
				return this.m_exceeded;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x000268FE File Offset: 0x00024AFE
		public string Target
		{
			get
			{
				return this.m_target;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00026906 File Offset: 0x00024B06
		public string Within
		{
			get
			{
				return this.m_within;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0002690E File Offset: 0x00024B0E
		public bool CurrentCountExhausted
		{
			get
			{
				return this.m_current >= this.Operator.Count;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00026926 File Offset: 0x00024B26
		public int CurrentCount
		{
			get
			{
				return this.m_current;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0002692E File Offset: 0x00024B2E
		public bool Increment()
		{
			if (!this.CurrentCountExhausted)
			{
				this.m_current++;
				return true;
			}
			this.SetExceeded();
			return this.m_operator.IgnoreLimitCount;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00026959 File Offset: 0x00024B59
		public void ResetCounter()
		{
			this.m_current = 0;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00026962 File Offset: 0x00024B62
		public void ResetExceeded()
		{
			this.m_exceeded = false;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0002696B File Offset: 0x00024B6B
		public void SetExceeded()
		{
			this.m_exceeded = true;
		}

		// Token: 0x04000255 RID: 597
		private readonly string m_id;

		// Token: 0x04000256 RID: 598
		private readonly string m_target;

		// Token: 0x04000257 RID: 599
		private readonly string m_within;

		// Token: 0x04000258 RID: 600
		private readonly LimitOperator m_operator;

		// Token: 0x04000259 RID: 601
		private bool m_exceeded;

		// Token: 0x0400025A RID: 602
		private int m_current;
	}
}
