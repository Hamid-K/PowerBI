using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000C0 RID: 192
	[DataContract(Name = "TranslateQueryRequest")]
	public sealed class TranslateQueryRequest
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000BCAA File Offset: 0x00009EAA
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0000BCB2 File Offset: 0x00009EB2
		[DataMember(Name = "Command", IsRequired = true, Order = 0)]
		public IList<TranslateQueryCommand> Commands { get; set; }
	}
}
