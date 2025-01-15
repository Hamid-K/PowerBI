using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Diagnostics
{
	// Token: 0x0200001F RID: 31
	[DataContract]
	public sealed class DiagnosticsQueryRequest
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002CF0 File Offset: 0x00000EF0
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002CF8 File Offset: 0x00000EF8
		[DataMember(Name = "startDate", Order = 10)]
		public DateTime StartDate { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002D01 File Offset: 0x00000F01
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002D09 File Offset: 0x00000F09
		[DataMember(Name = "endDate", Order = 20)]
		public DateTime EndDate { get; set; }

		// Token: 0x04000093 RID: 147
		[DataMember(Name = "diagnosticsQueryType", Order = 0)]
		public DiagnosticsQueryType diagnosticsQueryType;
	}
}
