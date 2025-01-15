using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000061 RID: 97
	[DataContract]
	public sealed class AcknowledgeAsyncPacketRequest : AsyncOperationRequestBase
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000030EE File Offset: 0x000012EE
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000030F6 File Offset: 0x000012F6
		[DataMember(Name = "packetIndex", IsRequired = true, EmitDefaultValue = true)]
		public int PacketIndex { get; set; }
	}
}
