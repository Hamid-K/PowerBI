using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x0200002D RID: 45
	public interface IModel
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000150 RID: 336
		bool HasDirectQueryContent { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000151 RID: 337
		bool SupportsCalculatedColumns { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000152 RID: 338
		bool SupportsGrouping { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000153 RID: 339
		bool SupportsQnA { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000154 RID: 340
		bool SupportsInsights { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000155 RID: 341
		bool SupportsFastRefresh { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000156 RID: 342
		InsightsCapabilities InsightsCapabilities { get; }

		// Token: 0x06000157 RID: 343
		ITable FindTable(string name);

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000158 RID: 344
		bool CanEditChangeDetectionMeasure { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000159 RID: 345
		bool SupportChangeDetectionMeasureRefresh { get; }
	}
}
