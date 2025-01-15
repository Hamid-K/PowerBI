using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000033 RID: 51
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal abstract class GetSchemaDataSetValueBase : OperationDataContract
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000027B1 File Offset: 0x000009B1
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000027B9 File Offset: 0x000009B9
		[DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000027C2 File Offset: 0x000009C2
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000027CA File Offset: 0x000009CA
		[DataMember(Name = "ns", IsRequired = false, EmitDefaultValue = false)]
		public string Namespace { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000027D3 File Offset: 0x000009D3
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000027DB File Offset: 0x000009DB
		[DataMember(Name = "value", IsRequired = true, EmitDefaultValue = false)]
		[JsonConverter(typeof(OperationDataContract.TypedValueConverter))]
		public object Value { get; set; }
	}
}
