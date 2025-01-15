using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
	// Token: 0x020002BF RID: 703
	internal abstract class ObjectModel
	{
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060015C8 RID: 5576
		internal abstract Fields Fields { get; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060015C9 RID: 5577
		internal abstract Parameters Parameters { get; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060015CA RID: 5578
		internal abstract Globals Globals { get; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060015CB RID: 5579
		internal abstract User User { get; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060015CC RID: 5580
		internal abstract ReportItems ReportItems { get; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060015CD RID: 5581
		internal abstract DataSets DataSets { get; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060015CE RID: 5582
		internal abstract DataSources DataSources { get; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060015CF RID: 5583
		internal abstract Variables Variables { get; }
	}
}
