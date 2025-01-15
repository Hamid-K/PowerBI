using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002D9 RID: 729
	public sealed class PageBreakInstance : BaseInstance
	{
		// Token: 0x06001B43 RID: 6979 RVA: 0x0006C88B File Offset: 0x0006AA8B
		internal PageBreakInstance(IReportScope reportScope, PageBreak pageBreakDef)
			: base(reportScope)
		{
			this.m_pageBreakDef = pageBreakDef;
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x0006C89C File Offset: 0x0006AA9C
		public bool Disabled
		{
			get
			{
				if (this.m_disabled == null)
				{
					if (this.m_pageBreakDef.IsOldSnapshot)
					{
						this.m_disabled = new bool?(false);
					}
					else
					{
						ExpressionInfo disabled = this.m_pageBreakDef.PageBreakDef.Disabled;
						if (disabled != null)
						{
							if (disabled.IsExpression)
							{
								this.m_disabled = new bool?(this.m_pageBreakDef.PageBreakDef.EvaluateDisabled(this.ReportScopeInstance, this.m_pageBreakDef.RenderingContext.OdpContext, this.m_pageBreakDef.PageBreakOwner));
							}
							else
							{
								this.m_disabled = new bool?(disabled.BoolValue);
							}
						}
						else
						{
							this.m_disabled = new bool?(false);
						}
					}
				}
				return this.m_disabled.Value;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0006C958 File Offset: 0x0006AB58
		public bool ResetPageNumber
		{
			get
			{
				if (this.m_resetPageNumber == null)
				{
					if (this.m_pageBreakDef.IsOldSnapshot)
					{
						this.m_resetPageNumber = new bool?(false);
					}
					else
					{
						ExpressionInfo resetPageNumber = this.m_pageBreakDef.PageBreakDef.ResetPageNumber;
						if (resetPageNumber != null)
						{
							if (resetPageNumber.IsExpression)
							{
								this.m_resetPageNumber = new bool?(this.m_pageBreakDef.PageBreakDef.EvaluateResetPageNumber(this.ReportScopeInstance, this.m_pageBreakDef.RenderingContext.OdpContext, this.m_pageBreakDef.PageBreakOwner));
							}
							else
							{
								this.m_resetPageNumber = new bool?(resetPageNumber.BoolValue);
							}
						}
						else
						{
							this.m_resetPageNumber = new bool?(false);
						}
					}
				}
				return this.m_resetPageNumber.Value;
			}
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0006CA14 File Offset: 0x0006AC14
		protected override void ResetInstanceCache()
		{
			this.m_resetPageNumber = null;
			this.m_disabled = null;
		}

		// Token: 0x04000D78 RID: 3448
		private bool? m_resetPageNumber;

		// Token: 0x04000D79 RID: 3449
		private bool? m_disabled;

		// Token: 0x04000D7A RID: 3450
		private PageBreak m_pageBreakDef;
	}
}
