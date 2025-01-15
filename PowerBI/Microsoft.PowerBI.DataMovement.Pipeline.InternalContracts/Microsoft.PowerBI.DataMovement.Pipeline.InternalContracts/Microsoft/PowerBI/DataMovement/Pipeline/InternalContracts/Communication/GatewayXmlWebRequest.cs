using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000030 RID: 48
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class GatewayXmlWebRequest : OperationRequestBase
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002700 File Offset: 0x00000900
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002708 File Offset: 0x00000908
		[DataMember(Name = "request", IsRequired = true, EmitDefaultValue = false)]
		internal byte[] Request { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002711 File Offset: 0x00000911
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002719 File Offset: 0x00000919
		[DataMember(Name = "lobProperties", IsRequired = true, EmitDefaultValue = false)]
		internal Dictionary<string, object> Properties { get; set; }
	}
}
