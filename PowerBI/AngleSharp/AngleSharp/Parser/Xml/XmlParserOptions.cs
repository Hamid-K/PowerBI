using System;
using AngleSharp.Dom;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x0200005C RID: 92
	public struct XmlParserOptions
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000D9F7 File Offset: 0x0000BBF7
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000D9FF File Offset: 0x0000BBFF
		public bool IsSuppressingErrors { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000DA08 File Offset: 0x0000BC08
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000DA10 File Offset: 0x0000BC10
		public Action<IElement, TextPosition> OnCreated { get; set; }
	}
}
