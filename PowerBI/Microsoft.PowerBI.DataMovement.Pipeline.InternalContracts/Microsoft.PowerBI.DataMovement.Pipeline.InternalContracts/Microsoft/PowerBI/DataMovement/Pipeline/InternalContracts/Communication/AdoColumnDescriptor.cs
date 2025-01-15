using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class AdoColumnDescriptor
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000022E8 File Offset: 0x000004E8
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000022F0 File Offset: 0x000004F0
		[DataMember(Name = "columnName", IsRequired = true, EmitDefaultValue = false)]
		public string ColumnName { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000022F9 File Offset: 0x000004F9
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002301 File Offset: 0x00000501
		[DataMember(Name = "columnType", IsRequired = true, EmitDefaultValue = false)]
		public ClrTypeCode ColumnType { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000230A File Offset: 0x0000050A
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002312 File Offset: 0x00000512
		[DataMember(Name = "providerSpecificDataTypeName", IsRequired = false, EmitDefaultValue = false)]
		public string ProviderSpecificDataTypeName { get; set; }
	}
}
