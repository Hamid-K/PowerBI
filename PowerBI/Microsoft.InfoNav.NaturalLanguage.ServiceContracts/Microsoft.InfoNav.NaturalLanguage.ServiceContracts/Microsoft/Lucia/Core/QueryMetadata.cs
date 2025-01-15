using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000BC RID: 188
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryMetadata
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00006F83 File Offset: 0x00005183
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00006F8B File Offset: 0x0000518B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public SelectMetadata[] Select { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00006F94 File Offset: 0x00005194
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00006F9C File Offset: 0x0000519C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public FilterMetadata[] Filters { get; set; }
	}
}
