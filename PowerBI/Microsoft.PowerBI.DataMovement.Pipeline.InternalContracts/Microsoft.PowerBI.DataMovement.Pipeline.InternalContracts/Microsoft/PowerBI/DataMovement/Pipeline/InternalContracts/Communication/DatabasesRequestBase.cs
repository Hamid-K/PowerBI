using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public abstract class DatabasesRequestBase : DatabaseRequestBase
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000247B File Offset: 0x0000067B
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002483 File Offset: 0x00000683
		[DataMember(Name = "dataSource", EmitDefaultValue = false)]
		[JsonProperty(TypeNameHandling = TypeNameHandling.None)]
		public DataSourceGatewayDetails DataSource { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002494 File Offset: 0x00000694
		[DataMember(Name = "dataSources", EmitDefaultValue = false)]
		public DataSourceGatewayDetails[] DataSources { get; set; }
	}
}
