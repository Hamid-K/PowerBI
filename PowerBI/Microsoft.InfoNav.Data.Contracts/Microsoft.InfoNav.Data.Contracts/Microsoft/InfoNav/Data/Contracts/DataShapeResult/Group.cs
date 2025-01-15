using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000118 RID: 280
	[DataContract]
	public sealed class Group
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0000F5E3 File Offset: 0x0000D7E3
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x0000F5EB File Offset: 0x0000D7EB
		[DataMember(Name = "ScopeID", IsRequired = false, Order = 0)]
		public ScopeId ScopeId { get; set; }
	}
}
