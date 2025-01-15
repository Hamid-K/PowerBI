using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000320 RID: 800
	internal sealed class ShimParagraphInstance : ParagraphInstance
	{
		// Token: 0x06001DC0 RID: 7616 RVA: 0x00075086 File Offset: 0x00073286
		internal ShimParagraphInstance(Paragraph paragraphDef)
			: base(paragraphDef)
		{
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x00075090 File Offset: 0x00073290
		public override string UniqueName
		{
			get
			{
				if (this.m_uniqueName == null)
				{
					ReportItem renderReportItem = this.m_reportElementDef.RenderReportItem;
					this.m_uniqueName = renderReportItem.ID + "x0i" + renderReportItem.UniqueName;
				}
				return this.m_uniqueName;
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x000750D3 File Offset: 0x000732D3
		public override bool IsCompiled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000750D6 File Offset: 0x000732D6
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}
	}
}
