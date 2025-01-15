using System;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryPatternSelection
{
	// Token: 0x0200006E RID: 110
	internal sealed class QueryPatternSelectionResult
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x00014088 File Offset: 0x00012288
		internal QueryPatternSelectionResult(QueryPatternKind queryPattern, ReadOnlyCollection<QueryPatternReason> reasons)
		{
			this.m_queryPattern = queryPattern;
			this.m_reasons = reasons;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001409E File Offset: 0x0001229E
		public QueryPatternKind QueryPattern
		{
			get
			{
				return this.m_queryPattern;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000140A6 File Offset: 0x000122A6
		public ReadOnlyCollection<QueryPatternReason> Reasons
		{
			get
			{
				return this.m_reasons;
			}
		}

		// Token: 0x040002D2 RID: 722
		private readonly QueryPatternKind m_queryPattern;

		// Token: 0x040002D3 RID: 723
		private readonly ReadOnlyCollection<QueryPatternReason> m_reasons;
	}
}
