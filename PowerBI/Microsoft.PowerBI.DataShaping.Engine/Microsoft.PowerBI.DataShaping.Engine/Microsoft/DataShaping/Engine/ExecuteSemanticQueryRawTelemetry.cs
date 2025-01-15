using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.RawDataProcessing;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000011 RID: 17
	[DataContract]
	internal sealed class ExecuteSemanticQueryRawTelemetry : ExecuteSemanticQueryTelemetry
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E54 File Offset: 0x00001054
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002E5C File Offset: 0x0000105C
		[DataMember(Name = "RAW", EmitDefaultValue = false, Order = 60)]
		public RawDataProcessingTelemetry RawDataProcessing { get; set; }
	}
}
