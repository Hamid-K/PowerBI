using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000A8 RID: 168
	internal sealed class RdmParagraph
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000D32B File Offset: 0x0000B52B
		internal RdmParagraph(List<RdmTextRun> textRuns, Style style)
		{
			this._style = style;
			this._textRuns = textRuns;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000D341 File Offset: 0x0000B541
		public List<RdmTextRun> TextRuns
		{
			get
			{
				return this._textRuns;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000D349 File Offset: 0x0000B549
		public Style Style
		{
			get
			{
				return this._style;
			}
		}

		// Token: 0x04000229 RID: 553
		private readonly List<RdmTextRun> _textRuns;

		// Token: 0x0400022A RID: 554
		private readonly Style _style;
	}
}
