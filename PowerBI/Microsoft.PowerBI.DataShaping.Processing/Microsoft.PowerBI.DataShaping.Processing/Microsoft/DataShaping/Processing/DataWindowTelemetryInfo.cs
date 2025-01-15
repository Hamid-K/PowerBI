using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x0200000D RID: 13
	[DataContract]
	internal sealed class DataWindowTelemetryInfo
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002693 File Offset: 0x00000893
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000269B File Offset: 0x0000089B
		[DataMember(Name = "I", EmitDefaultValue = false, Order = 0)]
		internal int? TelemetryId { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000026A4 File Offset: 0x000008A4
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000026AC File Offset: 0x000008AC
		[DataMember(Name = "Cap", EmitDefaultValue = false, Order = 10)]
		internal int Capacity { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000026B5 File Offset: 0x000008B5
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000026BD File Offset: 0x000008BD
		[DataMember(Name = "Db", EmitDefaultValue = false, Order = 20)]
		internal int DbCount { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000026C6 File Offset: 0x000008C6
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000026CE File Offset: 0x000008CE
		[DataMember(Name = "N", EmitDefaultValue = false, Order = 30)]
		internal int Count { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000026D7 File Offset: 0x000008D7
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000026DF File Offset: 0x000008DF
		[DataMember(Name = "Ex", EmitDefaultValue = false, Order = 40)]
		internal bool Exceeded { get; set; }

		// Token: 0x0600004E RID: 78 RVA: 0x000026E8 File Offset: 0x000008E8
		internal void Populate(DataWindow window)
		{
			this.TelemetryId = window.TelemetryId;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000026F6 File Offset: 0x000008F6
		internal void Populate(DataPipelineWindow window)
		{
			this.Count = window.DiagnosticInstanceCount;
			this.Capacity = window.Capacity;
			this.Exceeded = !window.IsComplete;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000271F File Offset: 0x0000091F
		internal string ToJson()
		{
			return DataWindowTelemetryInfo.Serializer.ToJsonString(this);
		}

		// Token: 0x0400004E RID: 78
		private static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(DataWindowTelemetryInfo));
	}
}
