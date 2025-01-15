using System;
using AngleSharp.Dom;

namespace AngleSharp.Parser.Html
{
	// Token: 0x02000070 RID: 112
	public struct HtmlParserOptions
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00014141 File Offset: 0x00012341
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00014149 File Offset: 0x00012349
		public bool IsEmbedded { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00014152 File Offset: 0x00012352
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0001415A File Offset: 0x0001235A
		public bool IsScripting { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00014163 File Offset: 0x00012363
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0001416B File Offset: 0x0001236B
		public bool IsStrictMode { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00014174 File Offset: 0x00012374
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0001417C File Offset: 0x0001237C
		public Action<IElement, TextPosition> OnCreated { get; set; }
	}
}
