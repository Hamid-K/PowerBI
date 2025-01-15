using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000337 RID: 823
	public sealed class RectangleInstance : ReportItemInstance
	{
		// Token: 0x06001ECA RID: 7882 RVA: 0x00076D9D File Offset: 0x00074F9D
		internal RectangleInstance(Rectangle reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x00076DA6 File Offset: 0x00074FA6
		public override VisibilityInstance Visibility
		{
			get
			{
				if (((Rectangle)this.m_reportElementDef).IsListContentsRectangle)
				{
					return null;
				}
				return base.Visibility;
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x00076DC4 File Offset: 0x00074FC4
		public string PageName
		{
			get
			{
				if (!this.m_pageNameEvaluated)
				{
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_pageName = null;
					}
					else
					{
						this.m_pageNameEvaluated = true;
						Rectangle rectangle = (Rectangle)this.m_reportElementDef.ReportItemDef;
						ExpressionInfo pageName = rectangle.PageName;
						if (pageName != null)
						{
							if (pageName.IsExpression)
							{
								this.m_pageName = rectangle.EvaluatePageName(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext);
							}
							else
							{
								this.m_pageName = pageName.StringValue;
							}
						}
					}
				}
				return this.m_pageName;
			}
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x00076E4F File Offset: 0x0007504F
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_pageNameEvaluated = false;
			this.m_pageName = null;
		}

		// Token: 0x04000FAF RID: 4015
		private bool m_pageNameEvaluated;

		// Token: 0x04000FB0 RID: 4016
		private string m_pageName;
	}
}
