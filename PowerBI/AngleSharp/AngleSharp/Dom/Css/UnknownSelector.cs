using System;
using System.IO;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000315 RID: 789
	internal sealed class UnknownSelector : CssNode, ISelector, ICssNode, IStyleFormattable
	{
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0004FC64 File Offset: 0x0004DE64
		public Priority Specifity
		{
			get
			{
				return Priority.Zero;
			}
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0000EE9F File Offset: 0x0000D09F
		public bool Match(IElement element)
		{
			return false;
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0004810B File Offset: 0x0004630B
		public string Text
		{
			get
			{
				return this.ToCss();
			}
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0004EC33 File Offset: 0x0004CE33
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			TextView sourceCode = base.SourceCode;
			writer.Write((sourceCode != null) ? sourceCode.Text : null);
		}
	}
}
