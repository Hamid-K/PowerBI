using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000042 RID: 66
	[DataContract]
	internal sealed class OleDbCancelRequest : OperationRequestBase
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00002A5A File Offset: 0x00000C5A
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00002A62 File Offset: 0x00000C62
		[DataMember(Name = "requestId", IsRequired = true, EmitDefaultValue = true)]
		public Guid RequestId { get; set; }
	}
}
