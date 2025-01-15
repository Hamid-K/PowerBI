using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000017 RID: 23
	[DataContract]
	public sealed class NextResultResult : DatabaseResultBase
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000023B2 File Offset: 0x000005B2
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000023BA File Offset: 0x000005BA
		[DataMember(Name = "requestId", IsRequired = false, EmitDefaultValue = false)]
		public Guid RequestId { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000023C3 File Offset: 0x000005C3
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000023CB File Offset: 0x000005CB
		[DataMember(Name = "nextResultValue", IsRequired = false, EmitDefaultValue = false)]
		public bool NextResultValue { get; set; }
	}
}
