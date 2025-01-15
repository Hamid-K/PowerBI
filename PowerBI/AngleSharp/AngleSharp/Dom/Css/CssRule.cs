using System;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000302 RID: 770
	internal abstract class CssRule : CssNode, ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x06001651 RID: 5713 RVA: 0x0004E886 File Offset: 0x0004CA86
		internal CssRule(CssRuleType type, CssParser parser)
		{
			this._type = type;
			this._parser = parser;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0004810B File Offset: 0x0004630B
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x0004E89C File Offset: 0x0004CA9C
		public string CssText
		{
			get
			{
				return this.ToCss();
			}
			set
			{
				CssRule cssRule = this._parser.ParseRule(value);
				if (cssRule == null)
				{
					throw new DomException(DomError.Syntax);
				}
				if (cssRule.Type != this._type)
				{
					throw new DomException(DomError.InvalidModification);
				}
				this.ReplaceWith(cssRule);
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0004E8DE File Offset: 0x0004CADE
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x0004E8E6 File Offset: 0x0004CAE6
		public ICssRule Parent
		{
			get
			{
				return this._parentRule;
			}
			internal set
			{
				this._parentRule = value;
				if (value != null)
				{
					this._ownerSheet = this._parentRule.Owner;
				}
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0004E903 File Offset: 0x0004CB03
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x0004E90B File Offset: 0x0004CB0B
		public ICssStyleSheet Owner
		{
			get
			{
				return this._ownerSheet;
			}
			internal set
			{
				this._ownerSheet = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0004E914 File Offset: 0x0004CB14
		public CssRuleType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0004E91C File Offset: 0x0004CB1C
		internal CssParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0004E924 File Offset: 0x0004CB24
		protected virtual void ReplaceWith(ICssRule rule)
		{
			base.ReplaceAll(rule);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0004E92D File Offset: 0x0004CB2D
		protected void ReplaceSingle(ICssNode oldNode, ICssNode newNode)
		{
			if (oldNode == null)
			{
				if (newNode != null)
				{
					base.AppendChild(newNode);
				}
				return;
			}
			if (newNode != null)
			{
				base.ReplaceChild(oldNode, newNode);
				return;
			}
			base.RemoveChild(oldNode);
		}

		// Token: 0x04000C92 RID: 3218
		private readonly CssRuleType _type;

		// Token: 0x04000C93 RID: 3219
		private readonly CssParser _parser;

		// Token: 0x04000C94 RID: 3220
		private ICssStyleSheet _ownerSheet;

		// Token: 0x04000C95 RID: 3221
		private ICssRule _parentRule;
	}
}
