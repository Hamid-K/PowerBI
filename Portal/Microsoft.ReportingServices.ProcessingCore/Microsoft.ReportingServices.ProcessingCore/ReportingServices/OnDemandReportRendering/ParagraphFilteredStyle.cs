using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000348 RID: 840
	internal sealed class ParagraphFilteredStyle : Style
	{
		// Token: 0x0600206E RID: 8302 RVA: 0x0007E050 File Offset: 0x0007C250
		internal ParagraphFilteredStyle(ReportItem renderReportItem, RenderingContext renderingContext, bool useRenderStyle)
			: base(renderReportItem, renderingContext, useRenderStyle)
		{
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x0007E05B File Offset: 0x0007C25B
		protected override bool IsAvailableStyle(StyleAttributeNames styleName)
		{
			return styleName == StyleAttributeNames.TextAlign || styleName == StyleAttributeNames.LineHeight;
		}
	}
}
