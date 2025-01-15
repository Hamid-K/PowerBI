using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Reporting.QueryDesign.BatchQueries;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000043 RID: 67
	[DataContract]
	internal sealed class DataShapeQueryTranslationTelemetry
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00007D88 File Offset: 0x00005F88
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00007D90 File Offset: 0x00005F90
		[DataMember(Name = "QueryPattern", EmitDefaultValue = false, Order = 10)]
		internal string QueryPattern { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00007D99 File Offset: 0x00005F99
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00007DA1 File Offset: 0x00005FA1
		[DataMember(Name = "Reasons", EmitDefaultValue = false, Order = 20)]
		internal string Reasons { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00007DAA File Offset: 0x00005FAA
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x00007DB2 File Offset: 0x00005FB2
		[DataMember(Name = "DynamicLimits", EmitDefaultValue = false, Order = 30)]
		internal bool UsedDynamicLimits { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00007DBB File Offset: 0x00005FBB
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00007DC3 File Offset: 0x00005FC3
		[DataMember(Name = "KeyPointSample", EmitDefaultValue = false, Order = 40)]
		internal bool UsedKeyPointSampling { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00007DCC File Offset: 0x00005FCC
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00007DD4 File Offset: 0x00005FD4
		[DataMember(Name = "KeyPointMeasures", EmitDefaultValue = false, Order = 50)]
		internal int? KeyPointMeasureCount { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00007DDD File Offset: 0x00005FDD
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00007DE5 File Offset: 0x00005FE5
		[DataMember(Name = "BatchComplexSlicer", EmitDefaultValue = false, Order = 60)]
		internal bool UsedBatchComplexSlicer { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00007DEE File Offset: 0x00005FEE
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00007DF6 File Offset: 0x00005FF6
		[DataMember(Name = "ModelCloneDuration", EmitDefaultValue = false, Order = 70)]
		internal long ModelCloneDuration { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00007DFF File Offset: 0x00005FFF
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00007E07 File Offset: 0x00006007
		[DataMember(Name = "InstanceFilters", IsRequired = false, EmitDefaultValue = false, Order = 80)]
		internal List<InstanceFilterTelemetry> InstanceFilters { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00007E10 File Offset: 0x00006010
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00007E18 File Offset: 0x00006018
		[DataMember(Name = "InternalCancel", IsRequired = false, EmitDefaultValue = false, Order = 90)]
		internal bool WasCancelledInternally { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00007E21 File Offset: 0x00006021
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x00007E29 File Offset: 0x00006029
		[DataMember(Name = "Constraints", IsRequired = false, EmitDefaultValue = false, Order = 100)]
		internal List<BatchQueryConstraintTelemetry> BatchQueryConstraints { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00007E32 File Offset: 0x00006032
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00007E3A File Offset: 0x0000603A
		[DataMember(Name = "StaticLimits", EmitDefaultValue = false, Order = 110)]
		internal bool UsedStaticLimits { get; set; }

		// Token: 0x060002C4 RID: 708 RVA: 0x00007E43 File Offset: 0x00006043
		internal void RegisterBatchQueryConstraint(BatchQueryConstraintTelemetry telemetry, string identifier)
		{
			if (this.BatchQueryConstraints == null)
			{
				this.BatchQueryConstraints = new List<BatchQueryConstraintTelemetry>();
			}
			telemetry.Id = identifier;
			this.BatchQueryConstraints.Add(telemetry);
		}
	}
}
