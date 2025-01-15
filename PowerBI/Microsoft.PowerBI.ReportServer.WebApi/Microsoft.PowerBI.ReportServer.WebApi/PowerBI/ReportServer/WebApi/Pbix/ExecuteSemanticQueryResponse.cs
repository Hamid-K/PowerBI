using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x0200001D RID: 29
	[DataContract]
	public class ExecuteSemanticQueryResponse
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000033DB File Offset: 0x000015DB
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000033E3 File Offset: 0x000015E3
		[DataMember(Name = "jobIds", Order = 10, EmitDefaultValue = false)]
		public IList<string> JobIds { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000033EC File Offset: 0x000015EC
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000033F4 File Offset: 0x000015F4
		[DataMember(Name = "results", Order = 20, EmitDefaultValue = false)]
		public IList<ExecuteSemanticQueryResult> Results { get; set; }
	}
}
