using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200016E RID: 366
	internal sealed class PlanLimitInfo : IStructuredToString
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x00036655 File Offset: 0x00034855
		internal PlanLimitInfo(List<LimitOverride> limitOverrides, List<LimitTelemetryItem> telemetryItems)
		{
			this.LimitOverrides = limitOverrides.AsReadOnlyCollection<LimitOverride>();
			this.TelemetryItems = telemetryItems.AsReadOnlyCollection<LimitTelemetryItem>();
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00036675 File Offset: 0x00034875
		internal ReadOnlyCollection<LimitOverride> LimitOverrides { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0003667D File Offset: 0x0003487D
		internal ReadOnlyCollection<LimitTelemetryItem> TelemetryItems { get; }

		// Token: 0x06000D37 RID: 3383 RVA: 0x00036685 File Offset: 0x00034885
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PlanLimitInfo");
			builder.WriteProperty<ReadOnlyCollection<LimitOverride>>("LimitOverrides", this.LimitOverrides, false);
			builder.WriteProperty<ReadOnlyCollection<LimitTelemetryItem>>("Telemetry", this.TelemetryItems, false);
			builder.EndObject();
		}
	}
}
