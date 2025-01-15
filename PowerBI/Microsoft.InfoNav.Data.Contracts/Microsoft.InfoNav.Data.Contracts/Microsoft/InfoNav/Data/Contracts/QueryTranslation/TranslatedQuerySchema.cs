using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000B5 RID: 181
	[DataContract(Name = "TranslatedQuerySchema")]
	public sealed class TranslatedQuerySchema
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000B8CC File Offset: 0x00009ACC
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000B8D4 File Offset: 0x00009AD4
		[DataMember(Name = "Selects", IsRequired = true, Order = 0)]
		public IList<TranslatedSelect> Selects { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000B8DD File Offset: 0x00009ADD
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000B8E5 File Offset: 0x00009AE5
		[DataMember(Name = "Parameters", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<TranslatedParameter> Parameters { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000B8EE File Offset: 0x00009AEE
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000B8F6 File Offset: 0x00009AF6
		[DataMember(Name = "Groups", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public TranslatedGroups Groups { get; set; }
	}
}
