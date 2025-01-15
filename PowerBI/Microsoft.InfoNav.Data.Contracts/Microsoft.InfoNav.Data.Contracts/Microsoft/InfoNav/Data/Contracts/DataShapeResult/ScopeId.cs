using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200011E RID: 286
	[DataContract]
	public sealed class ScopeId
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0000F6DF File Offset: 0x0000D8DF
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0000F6E7 File Offset: 0x0000D8E7
		[DataMember(Name = "ScopeValues", IsRequired = true, Order = 0)]
		public IList<ScopeValue> ScopeValues { get; set; }
	}
}
