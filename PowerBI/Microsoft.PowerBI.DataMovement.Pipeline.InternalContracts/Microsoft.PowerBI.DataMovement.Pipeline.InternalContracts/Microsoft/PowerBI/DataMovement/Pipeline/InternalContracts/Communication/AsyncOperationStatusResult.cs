using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000066 RID: 102
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class AsyncOperationStatusResult : AsyncOperationResultBase
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000031E6 File Offset: 0x000013E6
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000031EE File Offset: 0x000013EE
		[DataMember(Name = "status", IsRequired = true, EmitDefaultValue = true)]
		public AsyncOperationStatus Status { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000031F7 File Offset: 0x000013F7
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000031FF File Offset: 0x000013FF
		[DataMember(Name = "readyToStreamPacketCount", IsRequired = true, EmitDefaultValue = true)]
		public int ReadyToStreamPacketCount { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00003208 File Offset: 0x00001408
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00003210 File Offset: 0x00001410
		[DataMember(Name = "acknowledgedPacketCount", IsRequired = true, EmitDefaultValue = true)]
		public int AcknowledgedPacketCount { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00003219 File Offset: 0x00001419
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00003221 File Offset: 0x00001421
		[DataMember(Name = "exception", IsRequired = false, EmitDefaultValue = false)]
		public GatewayPipelineException Exception { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000322A File Offset: 0x0000142A
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00003232 File Offset: 0x00001432
		[DataMember(Name = "canStreamBeforeRequestCompletes", IsRequired = false, EmitDefaultValue = false)]
		public bool CanStreamBeforeRequestCompletes { get; set; }
	}
}
