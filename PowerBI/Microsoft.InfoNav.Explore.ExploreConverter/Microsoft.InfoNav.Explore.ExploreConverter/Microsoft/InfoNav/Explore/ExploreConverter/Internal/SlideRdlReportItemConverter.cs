using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200003F RID: 63
	internal sealed class SlideRdlReportItemConverter : BaseRdlReportItemConverter
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x00008BE5 File Offset: 0x00006DE5
		public override void Load(IReportDeserializationContext ctx, ReportItem reportItem, PVVisual visual)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public override PVVisual GetParentForFilter(PVVisual visual, Filter filter)
		{
			List<PVVisual> list = visual.ParentVisual.Visuals.Where((PVVisual v) => v.Type == "FilterVisual").ToList<PVVisual>();
			Contract.Check(list.Count == 1, "Expecting only one visual");
			return list[0];
		}
	}
}
