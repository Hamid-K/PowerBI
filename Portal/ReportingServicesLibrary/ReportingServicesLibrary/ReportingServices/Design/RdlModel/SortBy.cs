using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003DF RID: 991
	public sealed class SortBy
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0007E926 File Offset: 0x0007CB26
		// (set) Token: 0x06001F99 RID: 8089 RVA: 0x0007E92E File Offset: 0x0007CB2E
		public Expression SortExpression
		{
			get
			{
				return this.m_sortExpression;
			}
			set
			{
				this.m_sortExpression = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0007E937 File Offset: 0x0007CB37
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x0007E93F File Offset: 0x0007CB3F
		[DefaultValue(Sorting.Direction.Ascending)]
		public Sorting.Direction Direction
		{
			get
			{
				return this.m_direction;
			}
			set
			{
				this.m_direction = value;
			}
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000025F4 File Offset: 0x000007F4
		public SortBy()
		{
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x0007E948 File Offset: 0x0007CB48
		internal SortBy(Expression expression, Sorting.Direction direction)
		{
			this.m_sortExpression = expression;
			this.m_direction = direction;
		}

		// Token: 0x04000DC6 RID: 3526
		private Expression m_sortExpression;

		// Token: 0x04000DC7 RID: 3527
		private Sorting.Direction m_direction;
	}
}
