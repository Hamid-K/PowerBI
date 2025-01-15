using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000123 RID: 291
	public struct TextRange
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x000079A9 File Offset: 0x00005BA9
		public TextRange(TextPosition start, TextPosition end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000079B9 File Offset: 0x00005BB9
		public TextPosition Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000079C1 File Offset: 0x00005BC1
		public TextPosition End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000079CC File Offset: 0x00005BCC
		public override string ToString()
		{
			return this.start.ToString() + "-" + this.end.ToString();
		}

		// Token: 0x040002D0 RID: 720
		private readonly TextPosition start;

		// Token: 0x040002D1 RID: 721
		private readonly TextPosition end;
	}
}
