using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000426 RID: 1062
	public sealed class UserSort
	{
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x0008138D File Offset: 0x0007F58D
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x00081395 File Offset: 0x0007F595
		[DefaultValue(typeof(Expression), "")]
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

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0008139E File Offset: 0x0007F59E
		// (set) Token: 0x060021C6 RID: 8646 RVA: 0x000813A6 File Offset: 0x0007F5A6
		[DefaultValue("")]
		public string SortExpressionScope
		{
			get
			{
				return this.m_sortExpressionScope;
			}
			set
			{
				this.m_sortExpressionScope = value;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x000813AF File Offset: 0x0007F5AF
		// (set) Token: 0x060021C8 RID: 8648 RVA: 0x000813B7 File Offset: 0x0007F5B7
		[DefaultValue("")]
		public string SortTarget
		{
			get
			{
				return this.m_sortTarget;
			}
			set
			{
				this.m_sortTarget = value;
			}
		}

		// Token: 0x04000ED2 RID: 3794
		private Expression m_sortExpression;

		// Token: 0x04000ED3 RID: 3795
		private string m_sortExpressionScope;

		// Token: 0x04000ED4 RID: 3796
		private string m_sortTarget;
	}
}
