using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200002C RID: 44
	internal sealed class CardRdlReportItemConverter : TableRdlReportItemConverter
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00006A5C File Offset: 0x00004C5C
		public LayoutContext CreateLayoutContext(Tablix tablix)
		{
			string text;
			if (!string.IsNullOrEmpty(tablix.CardStyle) || "Card" == tablix.CardStyle)
			{
				text = "Card";
			}
			else
			{
				text = "CallOut";
			}
			return new LayoutContext
			{
				Columns = base.GetColumnsInfo(tablix),
				CardStyles = text
			};
		}
	}
}
