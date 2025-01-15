using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000B2 RID: 178
	[DataContract(Name = "TranslatedParameter")]
	public sealed class TranslatedParameter
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000B81B File Offset: 0x00009A1B
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0000B823 File Offset: 0x00009A23
		[DataMember(IsRequired = true, Order = 0)]
		public string Name { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000B82C File Offset: 0x00009A2C
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0000B834 File Offset: 0x00009A34
		[DataMember(IsRequired = true, Order = 10)]
		public string TranslatedName { get; set; }
	}
}
