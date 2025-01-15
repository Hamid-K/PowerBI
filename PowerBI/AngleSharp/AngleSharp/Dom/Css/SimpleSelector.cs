using System;
using System.IO;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000314 RID: 788
	internal sealed class SimpleSelector : CssNode, ISelector, ICssNode, IStyleFormattable
	{
		// Token: 0x060016B0 RID: 5808 RVA: 0x0004F601 File Offset: 0x0004D801
		public SimpleSelector()
			: this((IElement _) => true, Priority.Zero, Keywords.Asterisk)
		{
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0004F634 File Offset: 0x0004D834
		public SimpleSelector(string match)
			: this((IElement el) => match.Isi(el.LocalName), Priority.OneTag, match)
		{
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0004F66B File Offset: 0x0004D86B
		public SimpleSelector(Predicate<IElement> matches, Priority specifify, string code)
		{
			this._matches = matches;
			this._specifity = specifify;
			this._code = code;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0004F688 File Offset: 0x0004D888
		public Priority Specifity
		{
			get
			{
				return this._specifity;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x0004F690 File Offset: 0x0004D890
		public string Text
		{
			get
			{
				return this._code;
			}
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0004F698 File Offset: 0x0004D898
		public static SimpleSelector PseudoElement(Predicate<IElement> action, string pseudoElement)
		{
			return new SimpleSelector(action, Priority.OneTag, PseudoElementNames.Separator + pseudoElement);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0004F6B0 File Offset: 0x0004D8B0
		public static SimpleSelector PseudoClass(Predicate<IElement> action, string pseudoClass)
		{
			return new SimpleSelector(action, Priority.OneClass, PseudoClassNames.Separator + pseudoClass);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0004F6C8 File Offset: 0x0004D8C8
		public static SimpleSelector Class(string match)
		{
			return new SimpleSelector((IElement _) => _.ClassList.Contains(match), Priority.OneClass, "." + match);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0004F708 File Offset: 0x0004D908
		public static SimpleSelector Id(string match)
		{
			return new SimpleSelector((IElement _) => _.Id.Is(match), Priority.OneId, "#" + match);
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0004F748 File Offset: 0x0004D948
		public static SimpleSelector AttrAvailable(string match, string prefix = null, bool insensitive = false)
		{
			string text = match;
			if (!string.IsNullOrEmpty(prefix))
			{
				text = SimpleSelector.FormFront(prefix, match);
				match = SimpleSelector.FormMatch(prefix, match);
			}
			string text2 = SimpleSelector.FormCode(text);
			return new SimpleSelector((IElement _) => _.HasAttribute(match), Priority.OneClass, text2);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0004F7B0 File Offset: 0x0004D9B0
		public static SimpleSelector AttrMatch(string match, string value, string prefix = null, bool insensitive = false)
		{
			string text = match;
			if (!string.IsNullOrEmpty(prefix))
			{
				text = SimpleSelector.FormFront(prefix, match);
				match = SimpleSelector.FormMatch(prefix, match);
			}
			string text2 = SimpleSelector.FormCode(text, "=", value.CssString());
			StringComparison comparison = (insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return new SimpleSelector((IElement _) => string.Equals(_.GetAttribute(match), value, comparison), Priority.OneClass, text2);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0004F83C File Offset: 0x0004DA3C
		public static SimpleSelector AttrNotMatch(string match, string value, string prefix = null, bool insensitive = false)
		{
			string text = match;
			if (!string.IsNullOrEmpty(prefix))
			{
				text = SimpleSelector.FormFront(prefix, match);
				match = SimpleSelector.FormMatch(prefix, match);
			}
			string text2 = SimpleSelector.FormCode(text, "!=", value.CssString());
			StringComparison comparison = (insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return new SimpleSelector((IElement _) => !string.Equals(_.GetAttribute(match), value, comparison), Priority.OneClass, text2);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0004F8C8 File Offset: 0x0004DAC8
		public static SimpleSelector AttrList(string match, string value, string prefix = null, bool insensitive = false)
		{
			string text = match;
			if (!string.IsNullOrEmpty(prefix))
			{
				text = SimpleSelector.FormFront(prefix, match);
				match = SimpleSelector.FormMatch(prefix, match);
			}
			string text2 = SimpleSelector.FormCode(text, "~=", value.CssString());
			StringComparison comparison = (insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return new SimpleSelector(SimpleSelector.Select(value, (IElement _) => (_.GetAttribute(match) ?? string.Empty).SplitSpaces().Contains(value, comparison)), Priority.OneClass, text2);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0004F960 File Offset: 0x0004DB60
		public static SimpleSelector AttrBegins(string match, string value, string prefix = null, bool insensitive = false)
		{
			string text = match;
			if (!string.IsNullOrEmpty(prefix))
			{
				text = SimpleSelector.FormFront(prefix, match);
				match = SimpleSelector.FormMatch(prefix, match);
			}
			string text2 = SimpleSelector.FormCode(text, "^=", value.CssString());
			StringComparison comparison = (insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return new SimpleSelector(SimpleSelector.Select(value, (IElement _) => (_.GetAttribute(match) ?? string.Empty).StartsWith(value, comparison)), Priority.OneClass, text2);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0004F9F8 File Offset: 0x0004DBF8
		public static SimpleSelector AttrEnds(string match, string value, string prefix = null, bool insensitive = false)
		{
			string text = match;
			if (!string.IsNullOrEmpty(prefix))
			{
				text = SimpleSelector.FormFront(prefix, match);
				match = SimpleSelector.FormMatch(prefix, match);
			}
			string text2 = SimpleSelector.FormCode(text, "$=", value.CssString());
			StringComparison comparison = (insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return new SimpleSelector(SimpleSelector.Select(value, (IElement _) => (_.GetAttribute(match) ?? string.Empty).EndsWith(value, comparison)), Priority.OneClass, text2);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0004FA90 File Offset: 0x0004DC90
		public static SimpleSelector AttrContains(string match, string value, string prefix = null, bool insensitive = false)
		{
			string text = match;
			if (!string.IsNullOrEmpty(prefix))
			{
				text = SimpleSelector.FormFront(prefix, match);
				match = SimpleSelector.FormMatch(prefix, match);
			}
			string text2 = SimpleSelector.FormCode(text, "*=", value.CssString());
			StringComparison comparison = (insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return new SimpleSelector(SimpleSelector.Select(value, (IElement _) => (_.GetAttribute(match) ?? string.Empty).IndexOf(value, comparison) != -1), Priority.OneClass, text2);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0004FB28 File Offset: 0x0004DD28
		public static SimpleSelector AttrHyphen(string match, string value, string prefix = null, bool insensitive = false)
		{
			string text = match;
			if (!string.IsNullOrEmpty(prefix))
			{
				text = SimpleSelector.FormFront(prefix, match);
				match = SimpleSelector.FormMatch(prefix, match);
			}
			string text2 = SimpleSelector.FormCode(text, "|=", value.CssString());
			StringComparison comparison = (insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return new SimpleSelector(SimpleSelector.Select(value, (IElement _) => (_.GetAttribute(match) ?? string.Empty).HasHyphen(value, comparison)), Priority.OneClass, text2);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0004FBBD File Offset: 0x0004DDBD
		public static SimpleSelector Type(string match)
		{
			return new SimpleSelector(match);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0004FBC5 File Offset: 0x0004DDC5
		public bool Match(IElement element)
		{
			return this._matches(element);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0004FBD3 File Offset: 0x0004DDD3
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write(this.Text);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0004FBE1 File Offset: 0x0004DDE1
		private static Predicate<IElement> Select(string value, Predicate<IElement> predicate)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return predicate;
			}
			return (IElement _) => false;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0004FC0C File Offset: 0x0004DE0C
		private static string FormCode(string content)
		{
			return "[" + content + "]";
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0004FC1E File Offset: 0x0004DE1E
		private static string FormCode(string name, string op, string value)
		{
			return SimpleSelector.FormCode(name + op + value);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0004FC2D File Offset: 0x0004DE2D
		private static string FormFront(string prefix, string match)
		{
			return prefix + CombinatorSymbols.Pipe + match;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0004FC3B File Offset: 0x0004DE3B
		private static string FormMatch(string prefix, string match)
		{
			if (!prefix.Is(Keywords.Asterisk))
			{
				return prefix + PseudoClassNames.Separator + match;
			}
			return match;
		}

		// Token: 0x04000CA1 RID: 3233
		private readonly Predicate<IElement> _matches;

		// Token: 0x04000CA2 RID: 3234
		private readonly Priority _specifity;

		// Token: 0x04000CA3 RID: 3235
		private readonly string _code;

		// Token: 0x04000CA4 RID: 3236
		public static readonly SimpleSelector All = new SimpleSelector();
	}
}
