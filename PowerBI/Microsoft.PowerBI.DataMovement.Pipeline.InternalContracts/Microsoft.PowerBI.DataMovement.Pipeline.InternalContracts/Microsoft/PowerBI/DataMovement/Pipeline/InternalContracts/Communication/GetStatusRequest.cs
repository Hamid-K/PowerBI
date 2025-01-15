using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000018 RID: 24
	[DataContract]
	public sealed class GetStatusRequest : GatewayRequestBase
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000023DC File Offset: 0x000005DC
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000023E4 File Offset: 0x000005E4
		[DataMember(Name = "connectionId", IsRequired = false, EmitDefaultValue = true)]
		public Guid ConnectionId { get; set; }
	}
}
