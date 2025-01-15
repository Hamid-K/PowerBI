using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008FA RID: 2298
	internal class UserSortFilterTraversalContext : ITraversalContext
	{
		// Token: 0x06007EC8 RID: 32456 RVA: 0x0020BA42 File Offset: 0x00209C42
		public UserSortFilterTraversalContext(RuntimeSortFilterEventInfo eventInfo)
		{
			this.m_eventInfo = eventInfo;
		}

		// Token: 0x17002933 RID: 10547
		// (get) Token: 0x06007EC9 RID: 32457 RVA: 0x0020BA51 File Offset: 0x00209C51
		public RuntimeSortFilterEventInfo EventInfo
		{
			get
			{
				return this.m_eventInfo;
			}
		}

		// Token: 0x17002934 RID: 10548
		// (get) Token: 0x06007ECA RID: 32458 RVA: 0x0020BA59 File Offset: 0x00209C59
		// (set) Token: 0x06007ECB RID: 32459 RVA: 0x0020BA61 File Offset: 0x00209C61
		public RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj ExpressionScope
		{
			get
			{
				return this.m_expressionScope;
			}
			set
			{
				this.m_expressionScope = value;
			}
		}

		// Token: 0x04003E4B RID: 15947
		private RuntimeSortFilterEventInfo m_eventInfo;

		// Token: 0x04003E4C RID: 15948
		private RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj m_expressionScope;
	}
}
