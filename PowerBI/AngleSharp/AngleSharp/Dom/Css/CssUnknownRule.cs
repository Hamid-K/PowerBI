using System;
using System.IO;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000306 RID: 774
	internal sealed class CssUnknownRule : CssRule
	{
		// Token: 0x06001678 RID: 5752 RVA: 0x0004EC1A File Offset: 0x0004CE1A
		public CssUnknownRule(string name, CssParser parser)
			: base(CssRuleType.Unknown, parser)
		{
			this._name = name;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0004EC2B File Offset: 0x0004CE2B
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0004EC33 File Offset: 0x0004CE33
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			TextView sourceCode = base.SourceCode;
			writer.Write((sourceCode != null) ? sourceCode.Text : null);
		}

		// Token: 0x04000C97 RID: 3223
		private readonly string _name;
	}
}
