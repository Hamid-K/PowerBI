using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200004C RID: 76
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class OleDbRestriction : OperationDataContract
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00002C5A File Offset: 0x00000E5A
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00002C62 File Offset: 0x00000E62
		[DataMember(Name = "index", IsRequired = true, EmitDefaultValue = true)]
		internal uint Index { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00002C6B File Offset: 0x00000E6B
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00002C73 File Offset: 0x00000E73
		[DataMember(Name = "value", IsRequired = true, EmitDefaultValue = true)]
		[JsonConverter(typeof(OperationDataContract.TypedValueConverter))]
		public object Value { get; set; }
	}
}
