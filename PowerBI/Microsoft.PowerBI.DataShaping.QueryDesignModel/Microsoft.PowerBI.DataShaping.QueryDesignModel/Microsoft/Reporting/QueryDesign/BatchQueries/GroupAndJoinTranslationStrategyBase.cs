using System;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000266 RID: 614
	internal abstract class GroupAndJoinTranslationStrategyBase
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x00049B9A File Offset: 0x00047D9A
		protected GroupAndJoinTranslationStrategyBase(IConceptualModel model, IConceptualSchema schema)
		{
			this.Model = model;
			this.Schema = schema;
		}

		// Token: 0x06001A9A RID: 6810
		internal abstract QueryTable Translate(out bool hasUnconstrainedJoin, out BatchQueryConstraintTelemetry constraintTelemetry);

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x00049BB0 File Offset: 0x00047DB0
		internal IConceptualModel Model { get; }

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00049BB8 File Offset: 0x00047DB8
		internal IConceptualSchema Schema { get; }
	}
}
