using System;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200007D RID: 125
	internal sealed class QueryDataWindowGenerationResult
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0001576A File Offset: 0x0001396A
		private QueryDataWindowGenerationResult(TopLimitOperator windowLimit, QueryConstraintStatus status)
		{
			this.m_windowLimit = windowLimit;
			this.m_status = status;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00015780 File Offset: 0x00013980
		public TopLimitOperator WindowLimit
		{
			get
			{
				return this.m_windowLimit;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00015788 File Offset: 0x00013988
		public bool NeedsWindowLimit
		{
			get
			{
				return this.m_windowLimit != null;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00015793 File Offset: 0x00013993
		public QueryConstraintStatus Status
		{
			get
			{
				return this.m_status;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001579B File Offset: 0x0001399B
		public static QueryDataWindowGenerationResult ForConstrained()
		{
			return QueryDataWindowGenerationResult.AlreadyConstrainedInstance;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000157A2 File Offset: 0x000139A2
		public static QueryDataWindowGenerationResult ForUnconstrained(UnconstrainedQueryReasons reasons)
		{
			return new QueryDataWindowGenerationResult(null, QueryConstraintStatus.Unconstrained(reasons));
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000157B0 File Offset: 0x000139B0
		public static QueryDataWindowGenerationResult ForWindow(TopLimitOperator limit)
		{
			return new QueryDataWindowGenerationResult(limit, QueryConstraintStatus.Constrained());
		}

		// Token: 0x04000301 RID: 769
		private static readonly QueryDataWindowGenerationResult AlreadyConstrainedInstance = new QueryDataWindowGenerationResult(null, QueryConstraintStatus.Constrained());

		// Token: 0x04000302 RID: 770
		private readonly TopLimitOperator m_windowLimit;

		// Token: 0x04000303 RID: 771
		private readonly QueryConstraintStatus m_status;
	}
}
