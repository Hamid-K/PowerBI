using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200010F RID: 271
	[DataContract]
	public sealed class DataWindow
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0000F04D File Offset: 0x0000D24D
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0000F055 File Offset: 0x0000D255
		[DataMember(Name = "Id", IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public string Id { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0000F05E File Offset: 0x0000D25E
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0000F066 File Offset: 0x0000D266
		[DataMember(Name = "IsComplete", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public bool IsComplete { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0000F06F File Offset: 0x0000D26F
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0000F077 File Offset: 0x0000D277
		[DataMember(Name = "RestartTokens", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<RestartToken> RestartTokens { get; set; }
	}
}
