using System;
using System.Collections.Generic;
using System.IO;
using AngleSharp.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000309 RID: 777
	internal sealed class ComplexSelector : CssNode, ISelector, ICssNode, IStyleFormattable
	{
		// Token: 0x06001683 RID: 5763 RVA: 0x0004ED00 File Offset: 0x0004CF00
		public ComplexSelector()
		{
			this._selectors = new List<ComplexSelector.CombinatorSelector>();
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0004ED14 File Offset: 0x0004CF14
		public Priority Specifity
		{
			get
			{
				Priority priority = default(Priority);
				int count = this._selectors.Count;
				for (int i = 0; i < count; i++)
				{
					priority += this._selectors[i].Selector.Specifity;
				}
				return priority;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0004810B File Offset: 0x0004630B
		public string Text
		{
			get
			{
				return this.ToCss();
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0004ED5F File Offset: 0x0004CF5F
		public int Length
		{
			get
			{
				return this._selectors.Count;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0004ED6C File Offset: 0x0004CF6C
		// (set) Token: 0x06001688 RID: 5768 RVA: 0x0004ED74 File Offset: 0x0004CF74
		public bool IsReady { get; private set; }

		// Token: 0x06001689 RID: 5769 RVA: 0x0004ED80 File Offset: 0x0004CF80
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			if (this._selectors.Count > 0)
			{
				int num = this._selectors.Count - 1;
				for (int i = 0; i < num; i++)
				{
					writer.Write(this._selectors[i].Selector.Text);
					writer.Write(this._selectors[i].Delimiter);
				}
				writer.Write(this._selectors[num].Selector.Text);
			}
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0004EE04 File Offset: 0x0004D004
		public bool Match(IElement element)
		{
			int num = this._selectors.Count - 1;
			return this._selectors[num].Selector.Match(element) && (num <= 0 || this.MatchCascade(num - 1, element));
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0004EE4C File Offset: 0x0004D04C
		public void ConcludeSelector(ISelector selector)
		{
			if (!this.IsReady)
			{
				this._selectors.Add(new ComplexSelector.CombinatorSelector
				{
					Selector = selector,
					Transform = null,
					Delimiter = null
				});
				this.IsReady = true;
			}
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0004EE94 File Offset: 0x0004D094
		public void AppendSelector(ISelector selector, CssCombinator combinator)
		{
			if (!this.IsReady)
			{
				this._selectors.Add(new ComplexSelector.CombinatorSelector
				{
					Selector = combinator.Change(selector),
					Transform = combinator.Transform,
					Delimiter = combinator.Delimiter
				});
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0004EEE8 File Offset: 0x0004D0E8
		private bool MatchCascade(int pos, IElement element)
		{
			foreach (IElement element2 in this._selectors[pos].Transform(element))
			{
				if (this._selectors[pos].Selector.Match(element2) && (pos == 0 || this.MatchCascade(pos - 1, element2)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000C9C RID: 3228
		private readonly List<ComplexSelector.CombinatorSelector> _selectors;

		// Token: 0x02000507 RID: 1287
		private struct CombinatorSelector
		{
			// Token: 0x04001234 RID: 4660
			public string Delimiter;

			// Token: 0x04001235 RID: 4661
			public Func<IElement, IEnumerable<IElement>> Transform;

			// Token: 0x04001236 RID: 4662
			public ISelector Selector;
		}
	}
}
