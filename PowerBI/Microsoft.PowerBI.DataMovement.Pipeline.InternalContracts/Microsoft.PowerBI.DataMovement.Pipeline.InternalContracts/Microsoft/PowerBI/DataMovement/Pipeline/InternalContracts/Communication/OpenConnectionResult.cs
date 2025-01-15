using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000056 RID: 86
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class OpenConnectionResult : DatabaseResultBase
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00002FA9 File Offset: 0x000011A9
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00002FB1 File Offset: 0x000011B1
		[DataMember(Name = "serverVersion", IsRequired = true, EmitDefaultValue = false)]
		public string ServerVersion { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00002FBA File Offset: 0x000011BA
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00002FC2 File Offset: 0x000011C2
		[DataMember(Name = "dataSourceProductName", IsRequired = true, EmitDefaultValue = false)]
		public string DataSourceProductName { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00002FCB File Offset: 0x000011CB
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00002FD3 File Offset: 0x000011D3
		[DataMember(Name = "requestId", IsRequired = false, EmitDefaultValue = false)]
		public Guid RequestId { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00002FDC File Offset: 0x000011DC
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00002FE4 File Offset: 0x000011E4
		[DataMember(Name = "explicitCloseConnectionRequired", IsRequired = false)]
		public bool ExplicitCloseConnectionRequired { get; set; }
	}
}
