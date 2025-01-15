using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000036 RID: 54
	internal sealed class ImageRdlReportItemConverter : BaseRdlReportItemConverter
	{
		// Token: 0x06000180 RID: 384 RVA: 0x000078E4 File Offset: 0x00005AE4
		public override void Load(IReportDeserializationContext ctx, ReportItem reportItem, PVVisual visual)
		{
			visual.DataContext = null;
			Image image = reportItem as Image;
			Contract.Check(image != null, "Expect reportItem to be Image");
			if (image.DataSetName == null)
			{
				visual.LayoutContext = new LayoutContext
				{
					ResourceId = image.Source.ValueAsString
				};
			}
		}
	}
}
