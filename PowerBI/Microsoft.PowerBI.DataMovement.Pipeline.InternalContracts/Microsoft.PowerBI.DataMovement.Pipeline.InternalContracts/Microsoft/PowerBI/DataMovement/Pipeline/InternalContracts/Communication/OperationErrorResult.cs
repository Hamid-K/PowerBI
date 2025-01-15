using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000069 RID: 105
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class OperationErrorResult : OperationResultBase
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00003264 File Offset: 0x00001464
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000326C File Offset: 0x0000146C
		[DataMember(Name = "exception")]
		public GatewayPipelineException Exception { get; set; }
	}
}
