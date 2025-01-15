using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000347 RID: 839
	internal sealed class TextBoxFilteredStyle : Style
	{
		// Token: 0x0600206C RID: 8300 RVA: 0x0007DF6F File Offset: 0x0007C16F
		internal TextBoxFilteredStyle(ReportItem renderReportItem, RenderingContext renderingContext, bool useRenderStyle)
			: base(renderReportItem, renderingContext, useRenderStyle)
		{
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0007DF7C File Offset: 0x0007C17C
		protected override bool IsAvailableStyle(StyleAttributeNames styleName)
		{
			switch (styleName)
			{
			case StyleAttributeNames.BorderColor:
			case StyleAttributeNames.BorderColorTop:
			case StyleAttributeNames.BorderColorLeft:
			case StyleAttributeNames.BorderColorRight:
			case StyleAttributeNames.BorderColorBottom:
			case StyleAttributeNames.BorderStyle:
			case StyleAttributeNames.BorderStyleTop:
			case StyleAttributeNames.BorderStyleLeft:
			case StyleAttributeNames.BorderStyleRight:
			case StyleAttributeNames.BorderStyleBottom:
			case StyleAttributeNames.BorderWidth:
			case StyleAttributeNames.BorderWidthTop:
			case StyleAttributeNames.BorderWidthLeft:
			case StyleAttributeNames.BorderWidthRight:
			case StyleAttributeNames.BorderWidthBottom:
			case StyleAttributeNames.BackgroundColor:
			case StyleAttributeNames.VerticalAlign:
			case StyleAttributeNames.PaddingLeft:
			case StyleAttributeNames.PaddingRight:
			case StyleAttributeNames.PaddingTop:
			case StyleAttributeNames.PaddingBottom:
			case StyleAttributeNames.Direction:
			case StyleAttributeNames.WritingMode:
			case StyleAttributeNames.BackgroundImage:
			case StyleAttributeNames.BackgroundImageRepeat:
				return true;
			}
			return false;
		}
	}
}
