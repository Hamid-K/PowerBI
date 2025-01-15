using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200006C RID: 108
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class RenewDiagnosticsStorageAccessRequest : GatewayRequestBase
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000032CE File Offset: 0x000014CE
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000032D6 File Offset: 0x000014D6
		[DataMember(Name = "sasUri", IsRequired = true)]
		public Uri SasUri { get; set; }
	}
}
