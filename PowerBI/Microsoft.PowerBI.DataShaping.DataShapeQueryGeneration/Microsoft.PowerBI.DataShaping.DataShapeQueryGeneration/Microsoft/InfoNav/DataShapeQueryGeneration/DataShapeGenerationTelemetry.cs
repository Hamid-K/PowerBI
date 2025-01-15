using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200004F RID: 79
	[DataContract]
	internal sealed class DataShapeGenerationTelemetry
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000CCA3 File Offset: 0x0000AEA3
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000CCAB File Offset: 0x0000AEAB
		[DataMember(Name = "NumSelects", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		internal int? NumSelects { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		[DataMember(Name = "NumGroupBy", IsRequired = false, EmitDefaultValue = false, Order = 11)]
		internal int? NumGroupBy { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000CCC5 File Offset: 0x0000AEC5
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000CCCD File Offset: 0x0000AECD
		[DataMember(Name = "NumExtMeasures", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		internal int? NumExtMeasures { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000CCD6 File Offset: 0x0000AED6
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000CCDE File Offset: 0x0000AEDE
		[DataMember(Name = "NumExtColumns", IsRequired = false, EmitDefaultValue = false, Order = 25)]
		internal int? NumExtColumns { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000CCE7 File Offset: 0x0000AEE7
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000CCEF File Offset: 0x0000AEEF
		[DataMember(Name = "InFlattening", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		internal InFilterFlatteningTelemetry InFlattening { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000CD00 File Offset: 0x0000AF00
		[DataMember(Name = "Reduction", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		internal DataReductionTelemetry Reduction { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000CD09 File Offset: 0x0000AF09
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000CD11 File Offset: 0x0000AF11
		[DataMember(Name = "SlicerTargets", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		internal List<SlicerTargetsTelemetry> SlicerTargets { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000CD1A File Offset: 0x0000AF1A
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000CD22 File Offset: 0x0000AF22
		[DataMember(Name = "CorrelatedFilters", IsRequired = false, EmitDefaultValue = false, Order = 60)]
		internal int CorrelatedFiltersCount { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000CD2B File Offset: 0x0000AF2B
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000CD33 File Offset: 0x0000AF33
		[DataMember(Name = "CorrelatedNegatedFilters", IsRequired = false, EmitDefaultValue = false, Order = 70)]
		internal int CorrelatedNegatedFiltersCount { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000CD44 File Offset: 0x0000AF44
		[DataMember(Name = "NumLets", IsRequired = false, EmitDefaultValue = false, Order = 80)]
		internal int NumLets { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000CD4D File Offset: 0x0000AF4D
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000CD55 File Offset: 0x0000AF55
		[DataMember(Name = "Opt", IsRequired = false, EmitDefaultValue = false, Order = 90)]
		internal QueryOptimizerTelemetry Optimization { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000CD5E File Offset: 0x0000AF5E
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000CD66 File Offset: 0x0000AF66
		[DataMember(Name = "NumSubqueries", IsRequired = false, EmitDefaultValue = false, Order = 100)]
		internal int NumSubqueries { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000CD6F File Offset: 0x0000AF6F
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000CD77 File Offset: 0x0000AF77
		[DataMember(Name = "SubqueryRegroup", IsRequired = false, EmitDefaultValue = false, Order = 100)]
		internal bool HasSubqueryRegrouping { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000CD80 File Offset: 0x0000AF80
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000CD88 File Offset: 0x0000AF88
		[DataMember(Name = "SubqueryFilter", IsRequired = false, EmitDefaultValue = false, Order = 110)]
		internal bool HasSubqueryFiltering { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000CD91 File Offset: 0x0000AF91
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000CD99 File Offset: 0x0000AF99
		[DataMember(Name = "NumSync", IsRequired = false, EmitDefaultValue = false, Order = 130)]
		internal int NumGroupSynchronizationBlocks { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000CDA2 File Offset: 0x0000AFA2
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000CDAA File Offset: 0x0000AFAA
		[DataMember(Name = "NumParams", IsRequired = false, EmitDefaultValue = false, Order = 140)]
		internal int NumQueryParameterDeclarations { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000CDB3 File Offset: 0x0000AFB3
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000CDBB File Offset: 0x0000AFBB
		[DataMember(Name = "NumSparkline", IsRequired = false, EmitDefaultValue = false, Order = 150)]
		internal int NumSparklineDataExpression { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		[DataMember(Name = "NumSparklinePoints", IsRequired = false, EmitDefaultValue = false, Order = 160)]
		internal int NumSparklineDataPoints { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000CDD5 File Offset: 0x0000AFD5
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000CDDD File Offset: 0x0000AFDD
		[DataMember(Name = "NumVisualCalcs", IsRequired = false, EmitDefaultValue = false, Order = 170)]
		internal int NumVisualCalcExpressions { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000CDEE File Offset: 0x0000AFEE
		[DataMember(Name = "NumHiddenProjections", IsRequired = false, EmitDefaultValue = false, Order = 180)]
		internal int NumHiddenProjections { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000CDF7 File Offset: 0x0000AFF7
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000CDFF File Offset: 0x0000AFFF
		[DataMember(Name = "NumCollapsedGroups", IsRequired = false, EmitDefaultValue = false, Order = 190)]
		internal int NumCollapsedGroups { get; set; }

		// Token: 0x060003A9 RID: 937 RVA: 0x0000CE08 File Offset: 0x0000B008
		internal void AddSlicerTargetsTelemetry(SlicerTargetsTelemetry slicerTargetsTelemetry)
		{
			if (this.SlicerTargets == null)
			{
				this.SlicerTargets = new List<SlicerTargetsTelemetry>();
			}
			this.SlicerTargets.Add(slicerTargetsTelemetry);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000CE2C File Offset: 0x0000B02C
		internal void RecordFilterCorrelation(bool isNegated)
		{
			int num;
			if (isNegated)
			{
				num = this.CorrelatedNegatedFiltersCount;
				this.CorrelatedNegatedFiltersCount = num + 1;
				return;
			}
			num = this.CorrelatedFiltersCount;
			this.CorrelatedFiltersCount = num + 1;
		}
	}
}
