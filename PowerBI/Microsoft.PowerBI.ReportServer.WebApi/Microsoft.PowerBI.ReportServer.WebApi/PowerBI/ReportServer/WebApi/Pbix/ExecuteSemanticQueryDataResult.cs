using System;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x0200001F RID: 31
	[DataContract]
	public class ExecuteSemanticQueryDataResult
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000341F File Offset: 0x0000161F
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003427 File Offset: 0x00001627
		[DataMember(Name = "data", Order = 10, EmitDefaultValue = false)]
		public JRaw Data { get; set; }
	}
}
