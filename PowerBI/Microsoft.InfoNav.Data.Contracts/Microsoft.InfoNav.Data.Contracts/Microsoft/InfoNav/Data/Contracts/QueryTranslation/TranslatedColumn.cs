using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000AE RID: 174
	[DataContract(Name = "TranslatedColumn")]
	public sealed class TranslatedColumn
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000B784 File Offset: 0x00009984
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0000B78C File Offset: 0x0000998C
		[DataMember(IsRequired = true, Order = 0)]
		public string ColumnName { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000B795 File Offset: 0x00009995
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0000B79D File Offset: 0x0000999D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public QueryExpressionContainer Source { get; set; }
	}
}
