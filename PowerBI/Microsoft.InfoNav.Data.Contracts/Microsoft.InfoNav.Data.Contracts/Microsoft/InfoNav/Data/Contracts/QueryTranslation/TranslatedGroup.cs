using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000B0 RID: 176
	[DataContract(Name = "TranslatedGroup")]
	public sealed class TranslatedGroup
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000B7D8 File Offset: 0x000099D8
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0000B7E0 File Offset: 0x000099E0
		[DataMember(Name = "SubtotalIndicatorColumnName", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public string SubtotalIndicatorColumnName { get; set; }
	}
}
