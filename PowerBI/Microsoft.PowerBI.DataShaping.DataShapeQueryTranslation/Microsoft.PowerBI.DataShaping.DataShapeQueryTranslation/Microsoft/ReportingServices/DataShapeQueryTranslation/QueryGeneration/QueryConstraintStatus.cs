using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200007A RID: 122
	internal struct QueryConstraintStatus
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x00015360 File Offset: 0x00013560
		private QueryConstraintStatus(UnconstrainedQueryReasons reasons)
		{
			this.m_reasons = reasons;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00015369 File Offset: 0x00013569
		public bool IsConstrained
		{
			get
			{
				return this.m_reasons == UnconstrainedQueryReasons.None;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00015374 File Offset: 0x00013574
		public UnconstrainedQueryReasons Reasons
		{
			get
			{
				return this.m_reasons;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001537C File Offset: 0x0001357C
		public static QueryConstraintStatus Constrained()
		{
			return QueryConstraintStatus.ConstrainedInstance;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00015383 File Offset: 0x00013583
		public static QueryConstraintStatus Unconstrained(UnconstrainedQueryReasons reasons)
		{
			return new QueryConstraintStatus(reasons);
		}

		// Token: 0x040002F6 RID: 758
		private static readonly QueryConstraintStatus ConstrainedInstance = new QueryConstraintStatus(UnconstrainedQueryReasons.None);

		// Token: 0x040002F7 RID: 759
		private readonly UnconstrainedQueryReasons m_reasons;
	}
}
