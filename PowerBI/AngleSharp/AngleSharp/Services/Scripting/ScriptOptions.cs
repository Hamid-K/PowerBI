using System;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;

namespace AngleSharp.Services.Scripting
{
	// Token: 0x0200003E RID: 62
	public sealed class ScriptOptions
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00007329 File Offset: 0x00005529
		public ScriptOptions(IDocument document)
		{
			this.Document = document;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00007338 File Offset: 0x00005538
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00007340 File Offset: 0x00005540
		public IHtmlScriptElement Element { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00007349 File Offset: 0x00005549
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00007351 File Offset: 0x00005551
		public Encoding Encoding { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000735A File Offset: 0x0000555A
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00007362 File Offset: 0x00005562
		public IDocument Document { get; private set; }
	}
}
