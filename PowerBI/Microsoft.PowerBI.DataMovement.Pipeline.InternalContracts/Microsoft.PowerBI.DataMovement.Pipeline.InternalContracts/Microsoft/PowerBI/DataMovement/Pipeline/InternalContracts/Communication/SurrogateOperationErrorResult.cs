using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200006A RID: 106
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class SurrogateOperationErrorResult : OperationResultBase
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000327D File Offset: 0x0000147D
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00003285 File Offset: 0x00001485
		[DataMember(Name = "exception")]
		public NonSerializablePipelineExceptionSurrogate ExceptionSurrogate { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000328E File Offset: 0x0000148E
		[IgnoreDataMember]
		public bool IsValid
		{
			get
			{
				return this.ExceptionSurrogate != null && !string.IsNullOrEmpty(this.ExceptionSurrogate.GatewayPipelineErrorCode);
			}
		}
	}
}
