using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000043 RID: 67
	[DataContract]
	internal sealed class OleDbCancelResult : OleDbResultBase
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00002A73 File Offset: 0x00000C73
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00002A7B File Offset: 0x00000C7B
		[DataMember(Name = "requestId", IsRequired = true, EmitDefaultValue = true)]
		public Guid RequestId { get; set; }
	}
}
