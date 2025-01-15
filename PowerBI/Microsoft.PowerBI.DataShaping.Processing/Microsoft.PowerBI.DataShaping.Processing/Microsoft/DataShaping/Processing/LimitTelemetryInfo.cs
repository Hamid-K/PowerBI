using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x0200000F RID: 15
	[DataContract]
	internal sealed class LimitTelemetryInfo
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000027BF File Offset: 0x000009BF
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000027C7 File Offset: 0x000009C7
		[DataMember(Name = "I", EmitDefaultValue = false, Order = 0)]
		internal int? TelemetryId { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000027D0 File Offset: 0x000009D0
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000027D8 File Offset: 0x000009D8
		[DataMember(Name = "Role", EmitDefaultValue = false, Order = 10)]
		internal string Role { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000027E1 File Offset: 0x000009E1
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000027E9 File Offset: 0x000009E9
		[DataMember(Name = "Kind", EmitDefaultValue = false, Order = 20)]
		internal string Kind { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000027F2 File Offset: 0x000009F2
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000027FA File Offset: 0x000009FA
		[DataMember(Name = "Cap", EmitDefaultValue = false, Order = 30)]
		internal int Capacity { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002803 File Offset: 0x00000A03
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000280B File Offset: 0x00000A0B
		[DataMember(Name = "Db", EmitDefaultValue = false, Order = 31)]
		internal int DbCount { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002814 File Offset: 0x00000A14
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000281C File Offset: 0x00000A1C
		[DataMember(Name = "N", EmitDefaultValue = false, Order = 40)]
		internal int Count { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002825 File Offset: 0x00000A25
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000282D File Offset: 0x00000A2D
		[DataMember(Name = "Ex", EmitDefaultValue = false, Order = 50)]
		internal bool Exceeded { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002836 File Offset: 0x00000A36
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000283E File Offset: 0x00000A3E
		[DataMember(Name = "WN", EmitDefaultValue = false, Order = 60)]
		internal int WarningCount { get; set; }

		// Token: 0x0600006A RID: 106 RVA: 0x00002848 File Offset: 0x00000A48
		internal void Populate(DataLimit limit)
		{
			this.TelemetryId = limit.TelemetryId;
			this.Role = limit.Role.ToString();
			this.Kind = LimitTelemetryInfo.GetKind(limit.LimitOperator);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000288C File Offset: 0x00000A8C
		private static string GetKind(DataLimitOperator op)
		{
			if (op is TopLimitOperator)
			{
				return "Top";
			}
			if (op is BottomLimitOperator)
			{
				return "Bottom";
			}
			if (op is SampleLimitOperator)
			{
				return "Sample";
			}
			if (op is BinnedLineSampleLimitOperator)
			{
				return "BinnedLineSample";
			}
			if (op is OverlappingPointsSampleLimitOperator)
			{
				return "OverlappingPointsSample";
			}
			if (op is TopNPerLevelLimitOperator)
			{
				return "TopNPerLevelSample";
			}
			Contract.RetailFail("Unexpected limit type");
			return null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000028F8 File Offset: 0x00000AF8
		internal void Populate(DataPipelineLimit limit)
		{
			this.Count = limit.InstanceCount;
			this.Capacity = limit.Capacity;
			this.Exceeded = limit.IsExceededByAnyInstance;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000291E File Offset: 0x00000B1E
		internal string ToJson()
		{
			return LimitTelemetryInfo.Serializer.ToJsonString(this);
		}

		// Token: 0x04000058 RID: 88
		private const string Top = "Top";

		// Token: 0x04000059 RID: 89
		private const string Bottom = "Bottom";

		// Token: 0x0400005A RID: 90
		private const string Sample = "Sample";

		// Token: 0x0400005B RID: 91
		private const string BinnedLineSample = "BinnedLineSample";

		// Token: 0x0400005C RID: 92
		private const string OverlappingPointsSample = "OverlappingPointsSample";

		// Token: 0x0400005D RID: 93
		private const string TopNPerLevelSample = "TopNPerLevelSample";

		// Token: 0x0400005E RID: 94
		private static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(LimitTelemetryInfo));
	}
}
