using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x0200001E RID: 30
	[DataContract]
	public class ExecuteSemanticQueryResult
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000033FD File Offset: 0x000015FD
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003405 File Offset: 0x00001605
		[DataMember(Name = "jobId", Order = 10, EmitDefaultValue = false)]
		public string JobId { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000340E File Offset: 0x0000160E
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003416 File Offset: 0x00001616
		[DataMember(Name = "result", Order = 20, EmitDefaultValue = false)]
		public ExecuteSemanticQueryDataResult Result { get; set; }
	}
}
