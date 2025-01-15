using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200001E RID: 30
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public abstract class DatabaseRequestBase : OperationRequestBase
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000242F File Offset: 0x0000062F
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002437 File Offset: 0x00000637
		[DataMember(Name = "providerName", IsRequired = true, EmitDefaultValue = false)]
		public string ProviderName { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002440 File Offset: 0x00000640
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002448 File Offset: 0x00000648
		[DataMember(Name = "connectionString", IsRequired = true, EmitDefaultValue = false)]
		public string ConnectionString { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002451 File Offset: 0x00000651
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002459 File Offset: 0x00000659
		[DataMember(Name = "connectTimeout", IsRequired = false, EmitDefaultValue = true)]
		public int ConnectTimeout { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002462 File Offset: 0x00000662
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000246A File Offset: 0x0000066A
		[DataMember(Name = "rawConnectionImpersonationContextPlainTextCredentials", IsRequired = false, EmitDefaultValue = false)]
		public string RawConnectionImpersonationContextPlainTextCredentials { get; set; }
	}
}
