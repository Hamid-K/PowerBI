using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200001B RID: 27
	[DataContract]
	public sealed class CloseConnectionRequest : GatewayRequestBase
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000240E File Offset: 0x0000060E
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002416 File Offset: 0x00000616
		[DataMember(Name = "connectionId", IsRequired = false, EmitDefaultValue = false)]
		public Guid ConnectionId { get; set; }
	}
}
