using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000042 RID: 66
	internal sealed class TextBoxRdlReportItemConverter : BaseRdlReportItemConverter
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x000095B8 File Offset: 0x000077B8
		public override void Load(IReportDeserializationContext ctx, ReportItem reportItem, PVVisual visual)
		{
			Textbox textbox = reportItem as Textbox;
			Contract.Check(textbox != null, "Expect reportItem to be Textbox");
			this.SetLayoutProperties(textbox, visual);
			visual.LayoutContext.Type = "TextBox";
			visual.DataContext = null;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000095FC File Offset: 0x000077FC
		public void SetLayoutProperties(Textbox textbox, PVVisual visual)
		{
			List<PVParagraph> list = new List<PVParagraph>();
			foreach (RdmParagraph rdmParagraph in textbox.Paragraphs)
			{
				List<PVTextRun> list2 = new List<PVTextRun>();
				foreach (RdmTextRun rdmTextRun in rdmParagraph.TextRuns)
				{
					TextStyleInfo textStyleInfo = rdmTextRun.Style.TextStyleInfo;
					list2.Add(new PVTextRun
					{
						TextStyle = new PVTextStyle
						{
							FontFamily = textStyleInfo.Family,
							FontSize = textStyleInfo.Size,
							FontStyle = textStyleInfo.FontStyle,
							FontWeight = textStyleInfo.Weight,
							TextDecoration = textStyleInfo.TextDecoration
						},
						Value = rdmTextRun.Value
					});
				}
				list.Add(new PVParagraph
				{
					HorizontalTextAlignment = rdmParagraph.Style.GetPropertyValue("TextAlignment"),
					TextRuns = list2
				});
			}
			visual.LayoutContext.Paragraphs = list;
			visual.LayoutContext.VerticalAlignment = textbox.Style.GetPropertyValue("TextVerticalAlignment");
		}
	}
}
