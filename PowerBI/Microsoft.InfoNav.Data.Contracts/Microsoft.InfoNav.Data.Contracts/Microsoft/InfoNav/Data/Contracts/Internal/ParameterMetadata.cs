using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001F4 RID: 500
	[DataContract(Name = "parameterMetadata", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ParameterMetadata
	{
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0001A760 File Offset: 0x00018960
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x0001A768 File Offset: 0x00018968
		[DataMember(Name = "version", IsRequired = true, Order = 0)]
		public int Version { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0001A771 File Offset: 0x00018971
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x0001A779 File Offset: 0x00018979
		[DataMember(Name = "kind", EmitDefaultValue = false, IsRequired = false, Order = 1)]
		public ParameterKind ParameterKind { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0001A782 File Offset: 0x00018982
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0001A78A File Offset: 0x0001898A
		[DataMember(Name = "supportsMultipleValues", EmitDefaultValue = false, IsRequired = false, Order = 2)]
		public bool SupportsMultipleValues { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0001A793 File Offset: 0x00018993
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x0001A79B File Offset: 0x0001899B
		[DataMember(Name = "selectAllValue", EmitDefaultValue = false, IsRequired = false, Order = 3)]
		public string SelectAllValue { get; set; }
	}
}
