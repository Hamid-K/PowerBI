using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class ExecuteQueryRequestProperty : OperationDataContract
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000021F3 File Offset: 0x000003F3
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000021FB File Offset: 0x000003FB
		[DataMember(Name = "propertyName", IsRequired = true, EmitDefaultValue = false)]
		public string PropertyName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002204 File Offset: 0x00000404
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000220C File Offset: 0x0000040C
		[DataMember(Name = "value", IsRequired = true, EmitDefaultValue = false)]
		[JsonConverter(typeof(OperationDataContract.TypedValueConverter))]
		public object Value { get; set; }
	}
}
