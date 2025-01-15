using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000119 RID: 281
	[DataContract]
	public sealed class Limit
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0000F5FC File Offset: 0x0000D7FC
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0000F604 File Offset: 0x0000D804
		[DataMember(Name = "Id", IsRequired = true, Order = 0)]
		public string Id { get; set; }
	}
}
