using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class GatewayHttpWebResult : OperationResultBase
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000026B5 File Offset: 0x000008B5
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000026BD File Offset: 0x000008BD
		[DataMember(Name = "status", IsRequired = true, EmitDefaultValue = false)]
		public HttpStatusCode StatusCode { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000026C6 File Offset: 0x000008C6
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000026CE File Offset: 0x000008CE
		[DataMember(Name = "contentlength", IsRequired = true, EmitDefaultValue = false)]
		public long ResponseLength { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000026D7 File Offset: 0x000008D7
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000026DF File Offset: 0x000008DF
		[IgnoreDataMember]
		public HttpResponseMessage HttpResponseMessage { get; set; }
	}
}
