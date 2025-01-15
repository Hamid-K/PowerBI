using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000AF RID: 175
	[DataContract(Name = "TranslatedDynamicFormat")]
	public sealed class TranslatedDynamicFormat
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000B7AE File Offset: 0x000099AE
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0000B7B6 File Offset: 0x000099B6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public string Format { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000B7BF File Offset: 0x000099BF
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0000B7C7 File Offset: 0x000099C7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Culture { get; set; }
	}
}
