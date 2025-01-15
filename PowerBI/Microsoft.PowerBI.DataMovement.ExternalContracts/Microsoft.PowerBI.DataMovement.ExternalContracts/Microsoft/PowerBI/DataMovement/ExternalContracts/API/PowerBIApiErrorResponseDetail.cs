using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200006C RID: 108
	[NullableContext(2)]
	[Nullable(0)]
	[DataContract]
	public sealed class PowerBIApiErrorResponseDetail
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000471E File Offset: 0x0000291E
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00004726 File Offset: 0x00002926
		[DataMember(IsRequired = false, Order = 10, Name = "code", EmitDefaultValue = false)]
		public string Code { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000472F File Offset: 0x0000292F
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00004737 File Offset: 0x00002937
		[DataMember(IsRequired = false, Order = 20, Name = "message", EmitDefaultValue = false)]
		public string Message { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00004740 File Offset: 0x00002940
		// (set) Token: 0x0600031C RID: 796 RVA: 0x00004748 File Offset: 0x00002948
		[DataMember(IsRequired = false, Order = 30, Name = "target", EmitDefaultValue = false)]
		public string Target { get; set; }
	}
}
