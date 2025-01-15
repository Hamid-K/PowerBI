using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class OAuthResourceRequest : GatewayRequestBase
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00002A17 File Offset: 0x00000C17
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00002A1F File Offset: 0x00000C1F
		[DataMember(Name = "dataSourceReference", IsRequired = true)]
		public string DataSourceReference { get; set; }
	}
}
