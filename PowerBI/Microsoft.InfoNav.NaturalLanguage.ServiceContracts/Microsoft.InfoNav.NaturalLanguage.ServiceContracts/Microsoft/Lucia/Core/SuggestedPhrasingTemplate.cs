using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000D1 RID: 209
	[DataContract(Name = "SuggestedPhrasingTemplate", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class SuggestedPhrasingTemplate
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00008222 File Offset: 0x00006422
		public SuggestedPhrasingTemplate()
		{
			this.TermIndices = new List<int>();
			this.TemplateItems = new List<PhrasingTemplateItem>();
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00008240 File Offset: 0x00006440
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x00008248 File Offset: 0x00006448
		[DataMember(IsRequired = true, Order = 1)]
		public PhrasingType PhrasingType { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00008251 File Offset: 0x00006451
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00008259 File Offset: 0x00006459
		[DataMember(IsRequired = true, Order = 2)]
		public IList<int> TermIndices { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00008262 File Offset: 0x00006462
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000826A File Offset: 0x0000646A
		[DataMember(IsRequired = true, Order = 3)]
		public IList<PhrasingTemplateItem> TemplateItems { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00008273 File Offset: 0x00006473
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000827B File Offset: 0x0000647B
		[DataMember(IsRequired = true, Order = 4)]
		public string DisplayText { get; set; }
	}
}
