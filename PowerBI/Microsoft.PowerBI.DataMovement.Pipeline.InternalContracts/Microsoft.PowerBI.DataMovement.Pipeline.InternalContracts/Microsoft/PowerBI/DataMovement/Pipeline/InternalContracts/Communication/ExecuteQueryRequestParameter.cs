using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class ExecuteQueryRequestParameter : OperationDataContract
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000021A7 File Offset: 0x000003A7
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000021AF File Offset: 0x000003AF
		[DataMember(Name = "parameterName", IsRequired = true, EmitDefaultValue = false)]
		public string ParameterName { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000021B8 File Offset: 0x000003B8
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000021C0 File Offset: 0x000003C0
		[DataMember(Name = "dbType", IsRequired = true, EmitDefaultValue = true)]
		public DbType DbType { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000021C9 File Offset: 0x000003C9
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000021D1 File Offset: 0x000003D1
		[DataMember(Name = "value", IsRequired = true, EmitDefaultValue = false)]
		[JsonConverter(typeof(OperationDataContract.TypedValueConverter))]
		public object Value { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000021DA File Offset: 0x000003DA
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000021E2 File Offset: 0x000003E2
		[DataMember(Name = "parameterDirection", IsRequired = false, EmitDefaultValue = false)]
		public ParameterDirection ParameterDirection { get; set; }
	}
}
