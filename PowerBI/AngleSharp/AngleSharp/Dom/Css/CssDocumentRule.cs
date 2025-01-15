using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002F9 RID: 761
	internal sealed class CssDocumentRule : CssGroupingRule, ICssDocumentRule, ICssConditionRule, ICssGroupingRule, ICssRule, ICssNode, IStyleFormattable, ICssRuleCreator
	{
		// Token: 0x060015FE RID: 5630 RVA: 0x0004E070 File Offset: 0x0004C270
		internal CssDocumentRule(CssParser parser)
			: base(CssRuleType.Document, parser)
		{
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0004E07C File Offset: 0x0004C27C
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0004E0C0 File Offset: 0x0004C2C0
		public string ConditionText
		{
			get
			{
				IEnumerable<string> enumerable = this.Conditions.Select((IDocumentFunction m) => m.ToCss());
				return string.Join(", ", enumerable);
			}
			set
			{
				List<DocumentFunction> list = base.Parser.ParseDocumentRules(value);
				if (list == null)
				{
					throw new DomException(DomError.Syntax);
				}
				base.Clear();
				foreach (DocumentFunction documentFunction in list)
				{
					base.AppendChild(documentFunction);
				}
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0004E12C File Offset: 0x0004C32C
		public IEnumerable<IDocumentFunction> Conditions
		{
			get
			{
				return base.Children.OfType<IDocumentFunction>();
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0004E13C File Offset: 0x0004C33C
		internal bool IsValid(Url url)
		{
			return this.Conditions.Any((IDocumentFunction m) => m.Matches(url));
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0004E170 File Offset: 0x0004C370
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			string text = formatter.Block(base.Rules);
			writer.Write(formatter.Rule("@document", this.ConditionText, text));
		}
	}
}
