using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000FC RID: 252
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class EntityTermBinding : TermBinding
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00008EEE File Offset: 0x000070EE
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00008EF6 File Offset: 0x000070F6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public string SchemaId { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00008EFF File Offset: 0x000070FF
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00008F07 File Offset: 0x00007107
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string EntityText { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00008F10 File Offset: 0x00007110
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00008F18 File Offset: 0x00007118
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public int EntityNounTokenCount { get; set; }

		// Token: 0x060004E3 RID: 1251 RVA: 0x00008F21 File Offset: 0x00007121
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.SchemaId))
			{
				return base.ToString();
			}
			return StringUtil.FormatInvariant("{0}:{1}", this.SchemaId, base.ToString());
		}
	}
}
