using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200000C RID: 12
	[DataContract]
	public sealed class CancelRequest : OperationRequestBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C3 File Offset: 0x000002C3
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020CB File Offset: 0x000002CB
		[DataMember(Name = "requestId", IsRequired = true, EmitDefaultValue = true)]
		public Guid RequestId { get; set; }
	}
}
