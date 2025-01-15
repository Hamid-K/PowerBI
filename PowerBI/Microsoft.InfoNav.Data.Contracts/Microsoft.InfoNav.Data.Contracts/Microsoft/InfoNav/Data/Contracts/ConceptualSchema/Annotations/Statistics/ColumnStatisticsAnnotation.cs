using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations.Statistics
{
	// Token: 0x0200013B RID: 315
	public readonly struct ColumnStatisticsAnnotation
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x00010CA9 File Offset: 0x0000EEA9
		public ColumnStatisticsAnnotation(ConceptualColumnStatistics statistics)
		{
			this.Statistics = statistics;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00010CB2 File Offset: 0x0000EEB2
		public ConceptualColumnStatistics Statistics { get; }
	}
}
