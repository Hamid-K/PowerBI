using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200000D RID: 13
	[DataContract]
	public sealed class CancelResult : DatabaseResultBase
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020DC File Offset: 0x000002DC
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020E4 File Offset: 0x000002E4
		[DataMember(Name = "requestId", IsRequired = true, EmitDefaultValue = true)]
		public Guid RequestId { get; set; }
	}
}
