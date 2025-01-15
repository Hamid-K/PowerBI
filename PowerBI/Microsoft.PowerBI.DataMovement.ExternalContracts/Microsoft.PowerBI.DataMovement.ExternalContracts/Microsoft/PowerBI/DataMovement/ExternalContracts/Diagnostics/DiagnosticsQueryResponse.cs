using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Diagnostics
{
	// Token: 0x02000020 RID: 32
	[DataContract]
	public sealed class DiagnosticsQueryResponse
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002D1A File Offset: 0x00000F1A
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002D22 File Offset: 0x00000F22
		[DataMember(Name = "columns", Order = 0)]
		public IList<DiagnosticsColumn> Columns { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002D2B File Offset: 0x00000F2B
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002D33 File Offset: 0x00000F33
		[DataMember(Name = "results", Order = 10)]
		public IList<IList> Results { get; set; }

		// Token: 0x060000A9 RID: 169 RVA: 0x00002D3C File Offset: 0x00000F3C
		public DiagnosticsQueryResponse()
		{
			this.Columns = new List<DiagnosticsColumn>();
			this.Results = new List<IList>();
		}
	}
}
