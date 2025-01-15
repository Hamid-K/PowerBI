using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class OAuthResourceResult : GatewayResultBase
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00002A30 File Offset: 0x00000C30
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00002A38 File Offset: 0x00000C38
		[DataMember(Name = "resource", IsRequired = true)]
		public string Resource { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00002A41 File Offset: 0x00000C41
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00002A49 File Offset: 0x00000C49
		[DataMember(Name = "scope", IsRequired = false)]
		public string Scope { get; set; }
	}
}
