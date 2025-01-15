using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class AdoOutputParameter : OperationDataContract
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000234D File Offset: 0x0000054D
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002355 File Offset: 0x00000555
		[DataMember(Name = "parameterName", IsRequired = true, EmitDefaultValue = false)]
		public string ParameterName { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000235E File Offset: 0x0000055E
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002366 File Offset: 0x00000566
		[DataMember(Name = "dbType", IsRequired = true, EmitDefaultValue = true)]
		public DbType DbType { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000236F File Offset: 0x0000056F
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002377 File Offset: 0x00000577
		[DataMember(Name = "value", IsRequired = true, EmitDefaultValue = false)]
		[JsonConverter(typeof(OperationDataContract.TypedValueConverter))]
		public object Value { get; set; }
	}
}
