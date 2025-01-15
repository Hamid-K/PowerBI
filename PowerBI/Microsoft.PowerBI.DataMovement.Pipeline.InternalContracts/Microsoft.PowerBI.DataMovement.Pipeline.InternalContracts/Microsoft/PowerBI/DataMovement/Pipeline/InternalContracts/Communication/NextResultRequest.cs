using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000016 RID: 22
	[DataContract]
	public sealed class NextResultRequest : OperationRequestBase
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002388 File Offset: 0x00000588
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002390 File Offset: 0x00000590
		[DataMember(Name = "requestId", IsRequired = false, EmitDefaultValue = false)]
		public Guid RequestId { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002399 File Offset: 0x00000599
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000023A1 File Offset: 0x000005A1
		[DataMember(Name = "resultIndex", IsRequired = false, EmitDefaultValue = false)]
		public int ResultIndex { get; set; }
	}
}
