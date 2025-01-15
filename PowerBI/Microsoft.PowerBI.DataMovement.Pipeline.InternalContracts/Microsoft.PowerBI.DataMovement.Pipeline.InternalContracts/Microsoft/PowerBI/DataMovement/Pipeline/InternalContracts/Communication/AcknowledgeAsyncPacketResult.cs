using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000068 RID: 104
	[DataContract]
	public sealed class AcknowledgeAsyncPacketResult : AsyncOperationResultBase
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000324B File Offset: 0x0000144B
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00003253 File Offset: 0x00001453
		[DataMember(Name = "packetIndex", IsRequired = true, EmitDefaultValue = true)]
		public int PacketIndex { get; set; }
	}
}
