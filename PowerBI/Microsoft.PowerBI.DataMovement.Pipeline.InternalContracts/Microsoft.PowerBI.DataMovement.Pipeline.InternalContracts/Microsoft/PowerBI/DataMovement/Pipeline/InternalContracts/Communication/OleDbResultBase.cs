using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000054 RID: 84
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal abstract class OleDbResultBase : DatabaseResultBase
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00002F66 File Offset: 0x00001166
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00002F6E File Offset: 0x0000116E
		[DataMember(Name = "sessionId", IsRequired = false, EmitDefaultValue = false)]
		public OleDbSessionId SessionId { get; set; }
	}
}
