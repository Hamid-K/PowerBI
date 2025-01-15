using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200011D RID: 285
	public sealed class SourceLocation
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0000741F File Offset: 0x0000561F
		public SourceLocation(IDocumentHost host, TextRange range)
		{
			this.host = host;
			this.range = range;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00007435 File Offset: 0x00005635
		public IDocumentHost Document
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000743D File Offset: 0x0000563D
		public TextRange Range
		{
			get
			{
				return this.range;
			}
		}

		// Token: 0x040002C1 RID: 705
		public static readonly SourceLocation None = new SourceLocation(new TextDocumentHost(string.Empty), default(TextRange));

		// Token: 0x040002C2 RID: 706
		private IDocumentHost host;

		// Token: 0x040002C3 RID: 707
		private TextRange range;
	}
}
