using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000CD RID: 205
	[DataContract]
	public sealed class DataShapeExpressions
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000C1DB File Offset: 0x0000A3DB
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0000C1E3 File Offset: 0x0000A3E3
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public DataShapeExpressionsAxis Primary { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataShapeExpressionsAxis Secondary { get; set; }
	}
}
