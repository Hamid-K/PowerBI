using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000051 RID: 81
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal abstract class OleDbRequestBase : OperationRequestBase
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00002E1C File Offset: 0x0000101C
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00002E24 File Offset: 0x00001024
		[DataMember(Name = "providerName", IsRequired = true, EmitDefaultValue = false)]
		public string ProviderName { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00002E2D File Offset: 0x0000102D
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00002E35 File Offset: 0x00001035
		[DataMember(Name = "dataSourceProperties", IsRequired = true, EmitDefaultValue = false)]
		public OleDbProperty[] DataSourceProperties { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00002E3E File Offset: 0x0000103E
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00002E46 File Offset: 0x00001046
		[DataMember(Name = "dataSource", EmitDefaultValue = false)]
		[JsonProperty(TypeNameHandling = TypeNameHandling.None)]
		public DataSourceGatewayDetails DataSource { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00002E4F File Offset: 0x0000104F
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00002E57 File Offset: 0x00001057
		[DataMember(Name = "writeColumnOrdinals", IsRequired = false, EmitDefaultValue = false)]
		public bool WriteColumnOrdinals { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00002E60 File Offset: 0x00001060
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00002E68 File Offset: 0x00001068
		[DataMember(Name = "sessionId", IsRequired = false, EmitDefaultValue = false)]
		public OleDbSessionId SessionId { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00002E71 File Offset: 0x00001071
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00002E79 File Offset: 0x00001079
		[DataMember(Name = "rawConnectionImpersonationContextPlainTextCredentials", IsRequired = false, EmitDefaultValue = false)]
		public string RawConnectionImpersonationContextPlainTextCredentials { get; set; }
	}
}
