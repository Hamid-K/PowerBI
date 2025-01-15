using System;
using System.IO;
using System.Linq;
using AngleSharp.Css;
using AngleSharp.Dom.Collections;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002FF RID: 767
	internal sealed class CssMediaRule : CssConditionRule, ICssMediaRule, ICssConditionRule, ICssGroupingRule, ICssRule, ICssNode, IStyleFormattable, ICssRuleCreator
	{
		// Token: 0x06001639 RID: 5689 RVA: 0x0004E613 File Offset: 0x0004C813
		internal CssMediaRule(CssParser parser)
			: base(CssRuleType.Media, parser)
		{
			base.AppendChild(new MediaList(parser));
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0004E629 File Offset: 0x0004C829
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x0004E636 File Offset: 0x0004C836
		public string ConditionText
		{
			get
			{
				return this.Media.MediaText;
			}
			set
			{
				this.Media.MediaText = value;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0004E329 File Offset: 0x0004C529
		public MediaList Media
		{
			get
			{
				return base.Children.OfType<MediaList>().FirstOrDefault<MediaList>();
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x0004E644 File Offset: 0x0004C844
		IMediaList ICssMediaRule.Media
		{
			get
			{
				return this.Media;
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0004E64C File Offset: 0x0004C84C
		internal override bool IsValid(RenderDevice device)
		{
			return this.Media.Validate(device);
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0004E65C File Offset: 0x0004C85C
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			string text = formatter.Block(base.Rules);
			writer.Write(formatter.Rule("@media", this.Media.MediaText, text));
		}
	}
}
