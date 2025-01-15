using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000B1 RID: 177
	[DataContract(Name = "TranslatedGroups")]
	public sealed class TranslatedGroups
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000B7F1 File Offset: 0x000099F1
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000B7F9 File Offset: 0x000099F9
		[DataMember(Name = "Primary", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public IList<TranslatedGroup> Primary { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000B802 File Offset: 0x00009A02
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0000B80A File Offset: 0x00009A0A
		[DataMember(Name = "Secondary", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<TranslatedGroup> Secondary { get; set; }
	}
}
