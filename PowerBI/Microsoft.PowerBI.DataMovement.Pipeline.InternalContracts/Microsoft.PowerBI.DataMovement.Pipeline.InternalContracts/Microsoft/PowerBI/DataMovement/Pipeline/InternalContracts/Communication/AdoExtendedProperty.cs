using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class AdoExtendedProperty : OperationDataContract
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002323 File Offset: 0x00000523
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000232B File Offset: 0x0000052B
		[DataMember(Name = "propertyName", IsRequired = true, EmitDefaultValue = false)]
		public string PropertyName { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002334 File Offset: 0x00000534
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000233C File Offset: 0x0000053C
		[DataMember(Name = "value", IsRequired = false, EmitDefaultValue = false)]
		[JsonConverter(typeof(OperationDataContract.TypedValueConverter))]
		public object Value { get; set; }
	}
}
