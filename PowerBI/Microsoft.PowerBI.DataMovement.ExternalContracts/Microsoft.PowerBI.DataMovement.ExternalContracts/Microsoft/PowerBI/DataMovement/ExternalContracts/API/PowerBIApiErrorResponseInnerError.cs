using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200006D RID: 109
	[NullableContext(2)]
	[Nullable(0)]
	[DataContract]
	public sealed class PowerBIApiErrorResponseInnerError
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00004759 File Offset: 0x00002959
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00004761 File Offset: 0x00002961
		[DataMember(IsRequired = false, Order = 10, Name = "trace", EmitDefaultValue = false)]
		public string Trace { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000476A File Offset: 0x0000296A
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00004772 File Offset: 0x00002972
		[DataMember(IsRequired = false, Order = 20, Name = "context", EmitDefaultValue = false)]
		public string Context { get; set; }
	}
}
