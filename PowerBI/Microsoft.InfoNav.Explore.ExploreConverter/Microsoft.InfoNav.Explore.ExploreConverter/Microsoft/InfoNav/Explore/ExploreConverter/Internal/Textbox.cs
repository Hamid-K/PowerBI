using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000AA RID: 170
	internal sealed class Textbox : ReportItem
	{
		// Token: 0x06000346 RID: 838 RVA: 0x0000D377 File Offset: 0x0000B577
		internal Textbox(string name, ReportItemRect rect, int zIndex, ReportParsingDiagnosticContext diagnosticContext, List<RdmParagraph> paragraphs, Style style, FormatType formatType, string customFormatString)
			: base("Textbox", name, rect, zIndex, diagnosticContext)
		{
			this._paragraphs = paragraphs;
			this._style = style;
			this._formatType = formatType;
			this._customFormatString = customFormatString;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000D3AC File Offset: 0x0000B5AC
		public Expression FirstValue
		{
			get
			{
				if (this._paragraphs.Count > 0)
				{
					RdmParagraph rdmParagraph = this._paragraphs[0];
					if (rdmParagraph != null && rdmParagraph.TextRuns.Count > 0)
					{
						RdmTextRun rdmTextRun = rdmParagraph.TextRuns[0];
						if (rdmTextRun != null)
						{
							return new Expression(rdmTextRun.Value);
						}
					}
				}
				return null;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000D402 File Offset: 0x0000B602
		public List<RdmParagraph> Paragraphs
		{
			get
			{
				return this._paragraphs;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000D40A File Offset: 0x0000B60A
		public Style Style
		{
			get
			{
				return this._style;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000D412 File Offset: 0x0000B612
		public string CustomFormatString
		{
			get
			{
				return this._customFormatString;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000D41A File Offset: 0x0000B61A
		public FormatType FormatType
		{
			get
			{
				return this._formatType;
			}
		}

		// Token: 0x0400022D RID: 557
		private readonly List<RdmParagraph> _paragraphs;

		// Token: 0x0400022E RID: 558
		private readonly Style _style;

		// Token: 0x0400022F RID: 559
		private readonly string _customFormatString;

		// Token: 0x04000230 RID: 560
		private readonly FormatType _formatType;
	}
}
