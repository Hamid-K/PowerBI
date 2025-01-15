using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class GatewayXmlWebResult : OperationResultBase
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000272A File Offset: 0x0000092A
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00002732 File Offset: 0x00000932
		[DataMember(Name = "contentlength", IsRequired = true, EmitDefaultValue = false)]
		internal long ResponseLength { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000273B File Offset: 0x0000093B
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00002743 File Offset: 0x00000943
		[IgnoreDataMember]
		internal string XmlResponse { get; set; }
	}
}
