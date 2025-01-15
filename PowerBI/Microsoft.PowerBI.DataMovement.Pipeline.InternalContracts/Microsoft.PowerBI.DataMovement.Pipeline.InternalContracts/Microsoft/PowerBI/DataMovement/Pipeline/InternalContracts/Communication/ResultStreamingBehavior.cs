using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200005A RID: 90
	[DataContract]
	public sealed class ResultStreamingBehavior
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00003005 File Offset: 0x00001205
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000300D File Offset: 0x0000120D
		[DataMember(Name = "behaviorKind", IsRequired = true, EmitDefaultValue = true)]
		public ResultStreamingBehaviorKind BehaviorKind { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00003016 File Offset: 0x00001216
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000301E File Offset: 0x0000121E
		[DataMember(Name = "asyncOperationId", IsRequired = true, EmitDefaultValue = false)]
		public Guid AsyncOperationId { get; set; }
	}
}
