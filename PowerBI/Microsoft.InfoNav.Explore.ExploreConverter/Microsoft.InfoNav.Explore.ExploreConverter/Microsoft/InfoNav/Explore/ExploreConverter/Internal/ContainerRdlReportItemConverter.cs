using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200002F RID: 47
	internal class ContainerRdlReportItemConverter : MatrixRdlReportItemConverterBase
	{
		// Token: 0x06000162 RID: 354 RVA: 0x000070B0 File Offset: 0x000052B0
		public override void Load(IReportDeserializationContext ctx, ReportItem reportItem, PVVisual visual)
		{
			Tablix tablix = reportItem as Tablix;
			Contract.Check(tablix != null, "Expect reportItem to be Tablix");
			this.LoadLayoutContext(tablix, visual);
			this.LoadDataContext(ctx, tablix, visual);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000070E3 File Offset: 0x000052E3
		public virtual void LoadLayoutContext(Tablix tablix, PVVisual visual)
		{
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000070E5 File Offset: 0x000052E5
		public virtual void LoadDataContext(IReportDeserializationContext ctx, Tablix tablix, PVVisual visual)
		{
		}
	}
}
