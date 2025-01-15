using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning
{
	// Token: 0x0200023C RID: 572
	internal sealed class PlanLimitInfoBuilder
	{
		// Token: 0x06001395 RID: 5013 RVA: 0x0004C366 File Offset: 0x0004A566
		internal PlanLimitInfoBuilder()
		{
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0004C36E File Offset: 0x0004A56E
		internal void AddLimitOverride(LimitOverride limitOverride)
		{
			Util.AddToLazyList<LimitOverride>(ref this.m_limitOverrides, limitOverride);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0004C37C File Offset: 0x0004A57C
		internal void AddTelemetryItem(LimitTelemetryItem item)
		{
			Util.AddToLazyList<LimitTelemetryItem>(ref this.m_telemetryItems, item);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004C38A File Offset: 0x0004A58A
		internal PlanLimitInfo ToLimitInfo()
		{
			if (this.m_limitOverrides.IsNullOrEmpty<LimitOverride>() && this.m_telemetryItems.IsNullOrEmpty<LimitTelemetryItem>())
			{
				return null;
			}
			return new PlanLimitInfo(this.m_limitOverrides, this.m_telemetryItems);
		}

		// Token: 0x040008A7 RID: 2215
		private List<LimitOverride> m_limitOverrides;

		// Token: 0x040008A8 RID: 2216
		private List<LimitTelemetryItem> m_telemetryItems;
	}
}
