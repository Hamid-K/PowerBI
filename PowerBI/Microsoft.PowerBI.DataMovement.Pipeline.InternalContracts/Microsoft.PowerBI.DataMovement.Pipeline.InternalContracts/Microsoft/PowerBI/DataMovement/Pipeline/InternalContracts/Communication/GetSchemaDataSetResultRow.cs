using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000039 RID: 57
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class GetSchemaDataSetResultRow : OperationDataContract
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000028AD File Offset: 0x00000AAD
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000028B5 File Offset: 0x00000AB5
		[DataMember(Name = "columns", IsRequired = true, EmitDefaultValue = false)]
		[JsonProperty(ItemConverterType = typeof(OperationDataContract.TypedValueConverter))]
		public object[] Columns { get; set; }
	}
}
