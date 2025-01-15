using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B0A RID: 2826
	internal abstract class GoogleAnalyticsFilterExpression : GoogleAnalyticsExpression
	{
		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x06004E4C RID: 20044
		public abstract GoogleAnalyticsCubeObjectKind ColumnKind { get; }

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x06004E4D RID: 20045 RVA: 0x00103B81 File Offset: 0x00101D81
		public Value V2Filter
		{
			get
			{
				if (this.v2Filter == null)
				{
					this.v2Filter = this.GenerateV2Filter();
				}
				return this.v2Filter;
			}
		}

		// Token: 0x06004E4E RID: 20046
		protected abstract Value GenerateV2Filter();

		// Token: 0x04002A1B RID: 10779
		private Value v2Filter;
	}
}
