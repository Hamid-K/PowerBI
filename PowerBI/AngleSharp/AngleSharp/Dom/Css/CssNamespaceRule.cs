using System;
using System.Collections.Generic;
using System.IO;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000300 RID: 768
	internal sealed class CssNamespaceRule : CssRule, ICssNamespaceRule, ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x06001640 RID: 5696 RVA: 0x0004E693 File Offset: 0x0004C893
		internal CssNamespaceRule(CssParser parser)
			: base(CssRuleType.Namespace, parser)
		{
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0004E69E File Offset: 0x0004C89E
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x0004E6A6 File Offset: 0x0004C8A6
		public string NamespaceUri
		{
			get
			{
				return this._namespaceUri;
			}
			set
			{
				this.CheckValidity();
				this._namespaceUri = value ?? string.Empty;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0004E6BE File Offset: 0x0004C8BE
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x0004E6C6 File Offset: 0x0004C8C6
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
			set
			{
				this.CheckValidity();
				this._prefix = value ?? string.Empty;
			}
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0004E6E0 File Offset: 0x0004C8E0
		protected override void ReplaceWith(ICssRule rule)
		{
			CssNamespaceRule cssNamespaceRule = rule as CssNamespaceRule;
			this._namespaceUri = cssNamespaceRule._namespaceUri;
			this._prefix = cssNamespaceRule._prefix;
			base.ReplaceWith(rule);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0004E714 File Offset: 0x0004C914
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			string text = (string.IsNullOrEmpty(this._prefix) ? string.Empty : " ");
			string text2 = this._prefix + text + this._namespaceUri.CssUrl();
			writer.Write(formatter.Rule("@namespace", text2));
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0004E765 File Offset: 0x0004C965
		private static bool IsNotSupported(CssRuleType type)
		{
			return type != CssRuleType.Charset && type != CssRuleType.Import && type != CssRuleType.Namespace;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0004E77C File Offset: 0x0004C97C
		private void CheckValidity()
		{
			ICssStyleSheet owner = base.Owner;
			ICssRuleList cssRuleList = ((owner != null) ? owner.Rules : null);
			if (cssRuleList != null)
			{
				using (IEnumerator<ICssRule> enumerator = cssRuleList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (CssNamespaceRule.IsNotSupported(enumerator.Current.Type))
						{
							throw new DomException(DomError.InvalidState);
						}
					}
				}
			}
		}

		// Token: 0x04000C90 RID: 3216
		private string _namespaceUri;

		// Token: 0x04000C91 RID: 3217
		private string _prefix;
	}
}
