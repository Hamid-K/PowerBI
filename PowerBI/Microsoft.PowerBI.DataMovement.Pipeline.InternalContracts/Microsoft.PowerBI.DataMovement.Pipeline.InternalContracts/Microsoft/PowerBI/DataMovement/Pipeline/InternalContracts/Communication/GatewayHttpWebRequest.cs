using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200002C RID: 44
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class GatewayHttpWebRequest : OperationRequestBase
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000268B File Offset: 0x0000088B
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002693 File Offset: 0x00000893
		[DataMember(Name = "request", IsRequired = true, EmitDefaultValue = false)]
		public byte[] Request { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000269C File Offset: 0x0000089C
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000026A4 File Offset: 0x000008A4
		[DataMember(Name = "property", IsRequired = true, EmitDefaultValue = false)]
		public Dictionary<string, object> Properties { get; set; }
	}
}
