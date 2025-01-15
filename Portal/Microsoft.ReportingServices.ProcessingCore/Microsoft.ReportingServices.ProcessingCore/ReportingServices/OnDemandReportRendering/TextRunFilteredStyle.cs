using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000349 RID: 841
	internal sealed class TextRunFilteredStyle : Style
	{
		// Token: 0x06002070 RID: 8304 RVA: 0x0007E06A File Offset: 0x0007C26A
		internal TextRunFilteredStyle(ReportItem renderReportItem, RenderingContext renderingContext, bool useRenderStyle)
			: base(renderReportItem, renderingContext, useRenderStyle)
		{
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0007E078 File Offset: 0x0007C278
		protected override bool IsAvailableStyle(StyleAttributeNames styleName)
		{
			switch (styleName)
			{
			case StyleAttributeNames.FontStyle:
			case StyleAttributeNames.FontFamily:
			case StyleAttributeNames.FontSize:
			case StyleAttributeNames.FontWeight:
			case StyleAttributeNames.Format:
			case StyleAttributeNames.TextDecoration:
			case StyleAttributeNames.Color:
			case StyleAttributeNames.Language:
			case StyleAttributeNames.Calendar:
			case StyleAttributeNames.NumeralLanguage:
			case StyleAttributeNames.NumeralVariant:
				break;
			case StyleAttributeNames.TextAlign:
			case StyleAttributeNames.VerticalAlign:
			case StyleAttributeNames.PaddingLeft:
			case StyleAttributeNames.PaddingRight:
			case StyleAttributeNames.PaddingTop:
			case StyleAttributeNames.PaddingBottom:
			case StyleAttributeNames.LineHeight:
			case StyleAttributeNames.Direction:
			case StyleAttributeNames.WritingMode:
			case StyleAttributeNames.UnicodeBiDi:
				return false;
			default:
				if (styleName != StyleAttributeNames.CurrencyLanguage)
				{
					return false;
				}
				break;
			}
			return true;
		}
	}
}
