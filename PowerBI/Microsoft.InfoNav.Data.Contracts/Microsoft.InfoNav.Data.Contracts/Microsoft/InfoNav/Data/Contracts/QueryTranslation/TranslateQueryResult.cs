using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000C1 RID: 193
	[DataContract(Name = "TranslateQueryResult")]
	public sealed class TranslateQueryResult
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000BCC3 File Offset: 0x00009EC3
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000BCCB File Offset: 0x00009ECB
		[DataMember(Name = "Results", IsRequired = true, Order = 0)]
		public IList<TranslatedQuery> Results { get; set; }
	}
}
