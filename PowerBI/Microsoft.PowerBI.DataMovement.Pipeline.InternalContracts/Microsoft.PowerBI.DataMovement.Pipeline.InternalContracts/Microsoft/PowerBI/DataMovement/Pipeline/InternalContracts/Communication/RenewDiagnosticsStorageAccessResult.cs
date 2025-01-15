using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200006D RID: 109
	[DataContract]
	public sealed class RenewDiagnosticsStorageAccessResult : GatewayResultBase
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000032E7 File Offset: 0x000014E7
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000032EF File Offset: 0x000014EF
		[DataMember(Name = "accessRenewed", IsRequired = true)]
		public bool AccessRenewed { get; set; }
	}
}
