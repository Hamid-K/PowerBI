using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000106 RID: 262
	[DataContract]
	public sealed class DataMember
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0000ED93 File Offset: 0x0000CF93
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0000ED9B File Offset: 0x0000CF9B
		[DataMember(Name = "Id", IsRequired = true, Order = 0)]
		public string Id { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		[DataMember(Name = "Instances", IsRequired = true, Order = 10)]
		public IList<DataMemberInstance> Instances { get; set; }
	}
}
