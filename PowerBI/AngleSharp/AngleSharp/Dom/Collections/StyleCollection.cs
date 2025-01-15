using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Dom.Css;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x02000405 RID: 1029
	internal sealed class StyleCollection : IEnumerable<CssStyleRule>, IEnumerable
	{
		// Token: 0x060020D7 RID: 8407 RVA: 0x0005818D File Offset: 0x0005638D
		public StyleCollection(IEnumerable<CssStyleSheet> sheets, RenderDevice device)
		{
			this._sheets = sheets;
			this._device = device;
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x000581A3 File Offset: 0x000563A3
		public RenderDevice Device
		{
			get
			{
				return this._device;
			}
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000581AB File Offset: 0x000563AB
		public IEnumerator<CssStyleRule> GetEnumerator()
		{
			foreach (CssStyleSheet cssStyleSheet in this._sheets)
			{
				if (!cssStyleSheet.IsDisabled && cssStyleSheet.Media.Validate(this._device))
				{
					IEnumerable<CssStyleRule> rules = this.GetRules(cssStyleSheet.Rules);
					foreach (CssStyleRule cssStyleRule in rules)
					{
						yield return cssStyleRule;
					}
					IEnumerator<CssStyleRule> enumerator2 = null;
				}
			}
			IEnumerator<CssStyleSheet> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000581BA File Offset: 0x000563BA
		private IEnumerable<CssStyleRule> GetRules(CssRuleList rules)
		{
			foreach (ICssRule rule in rules)
			{
				if (rule.Type == CssRuleType.Media)
				{
					CssMediaRule cssMediaRule = (CssMediaRule)rule;
					if (cssMediaRule.IsValid(this._device))
					{
						IEnumerable<CssStyleRule> rules2 = this.GetRules(cssMediaRule.Rules);
						foreach (CssStyleRule cssStyleRule in rules2)
						{
							yield return cssStyleRule;
						}
						IEnumerator<CssStyleRule> enumerator2 = null;
					}
				}
				else if (rule.Type == CssRuleType.Supports)
				{
					CssSupportsRule cssSupportsRule = (CssSupportsRule)rule;
					if (cssSupportsRule.IsValid(this._device))
					{
						IEnumerable<CssStyleRule> rules3 = this.GetRules(cssSupportsRule.Rules);
						foreach (CssStyleRule cssStyleRule2 in rules3)
						{
							yield return cssStyleRule2;
						}
						IEnumerator<CssStyleRule> enumerator2 = null;
					}
				}
				else if (rule.Type == CssRuleType.Style)
				{
					yield return (CssStyleRule)rule;
				}
				rule = null;
			}
			IEnumerator<ICssRule> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000581D1 File Offset: 0x000563D1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000D23 RID: 3363
		private readonly IEnumerable<CssStyleSheet> _sheets;

		// Token: 0x04000D24 RID: 3364
		private readonly RenderDevice _device;
	}
}
