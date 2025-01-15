using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200006B RID: 107
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class OperationTelemetry : OperationResultBase
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000032B5 File Offset: 0x000014B5
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x000032BD File Offset: 0x000014BD
		[DataMember(Name = "activities")]
		internal IReadOnlyList<ActivityExecutionInfo> Activities { get; set; }
	}
}
