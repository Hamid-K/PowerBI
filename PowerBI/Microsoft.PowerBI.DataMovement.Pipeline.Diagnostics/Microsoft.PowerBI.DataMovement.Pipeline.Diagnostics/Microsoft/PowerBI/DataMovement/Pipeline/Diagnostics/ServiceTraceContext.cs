using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x0200001C RID: 28
	[DataContract]
	public class ServiceTraceContext
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004177 File Offset: 0x00002377
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000417F File Offset: 0x0000237F
		[DataMember(Name = "serviceName")]
		public string ServiceName { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004188 File Offset: 0x00002388
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004190 File Offset: 0x00002390
		[DataMember(Name = "traceIds")]
		public KeyValue[] TraceIds { get; set; }
	}
}
