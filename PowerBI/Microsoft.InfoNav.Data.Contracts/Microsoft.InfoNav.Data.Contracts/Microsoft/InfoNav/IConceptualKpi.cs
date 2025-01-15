using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200003B RID: 59
	public interface IConceptualKpi
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000E9 RID: 233
		string StatusGraphic { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000EA RID: 234
		string TrendGraphic { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000EB RID: 235
		IConceptualMeasure Status { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000EC RID: 236
		IConceptualMeasure Goal { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000ED RID: 237
		IConceptualMeasure Trend { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000EE RID: 238
		string Description { get; }
	}
}
