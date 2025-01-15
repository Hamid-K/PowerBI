using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000055 RID: 85
	[DataContract]
	public sealed class OpenConnectionRequest : DatabasesRequestBase
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00002F7F File Offset: 0x0000117F
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00002F87 File Offset: 0x00001187
		[DataMember(Name = "connectionId", IsRequired = false, EmitDefaultValue = false)]
		public Guid ConnectionId { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00002F90 File Offset: 0x00001190
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00002F98 File Offset: 0x00001198
		[DataMember(Name = "explicitCloseConnectionRequested", IsRequired = false)]
		public bool ExplicitCloseConnectionRequested { get; set; }
	}
}
