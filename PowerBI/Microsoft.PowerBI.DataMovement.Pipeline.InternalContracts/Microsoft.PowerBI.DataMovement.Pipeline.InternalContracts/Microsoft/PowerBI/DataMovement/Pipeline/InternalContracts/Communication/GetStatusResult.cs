using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class GetStatusResult : GatewayResultBase
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000023F5 File Offset: 0x000005F5
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000023FD File Offset: 0x000005FD
		[DataMember(Name = "status", IsRequired = false, EmitDefaultValue = false)]
		public AdoStatus Status { get; set; }
	}
}
