using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Semantics
{
	// Token: 0x02001174 RID: 4468
	public class HtmlDoc
	{
		// Token: 0x060084D4 RID: 34004 RVA: 0x001BFA67 File Offset: 0x001BDC67
		static HtmlDoc()
		{
			Configuration.SetDefault(Configuration.Default.WithCss(null).WithLocaleBasedEncoding());
		}

		// Token: 0x060084D5 RID: 34005 RVA: 0x001BFA80 File Offset: 0x001BDC80
		private HtmlDoc(IHtmlDocument doc)
		{
			this._doc = doc;
			IElement firstElementChild = this._doc.FirstElementChild;
			if (firstElementChild == null)
			{
				this.IsValid = false;
				return;
			}
			IElement element = ((firstElementChild.NodeName == "HTML") ? firstElementChild : this._doc.QuerySelectorAll("html:first").FirstOrDefault<IElement>());
			if (element != null)
			{
				this.RootNode = this.PopulateCache(element, null, 0, 0);
				this.IsValid = true;
				return;
			}
			this.IsValid = false;
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x060084D6 RID: 34006 RVA: 0x001BFB09 File Offset: 0x001BDD09
		public DomNode RootNode { get; }

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x060084D7 RID: 34007 RVA: 0x001BFB14 File Offset: 0x001BDD14
		public IEnumerable<IDomNode> AllNodes
		{
			get
			{
				Dictionary<IElement, DomNode> domNodeCache = this._domNodeCache;
				IEnumerable<IDomNode> enumerable = ((domNodeCache != null) ? domNodeCache.Values : null);
				return enumerable ?? Enumerable.Empty<IDomNode>();
			}
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x060084D8 RID: 34008 RVA: 0x001BFB3E File Offset: 0x001BDD3E
		public bool IsValid { get; }

		// Token: 0x060084D9 RID: 34009 RVA: 0x001BFB48 File Offset: 0x001BDD48
		private DomNode PopulateCache(IElement element, DomNode parent, int index, int docIndex)
		{
			DomNode domNode = new DomNode(element, parent, this, index, docIndex);
			this._domNodeCache.Add(element, domNode);
			List<IDomNode> list = new List<IDomNode>();
			int num = 0;
			DomNode domNode2 = null;
			foreach (IElement element2 in element.Children)
			{
				DomNode domNode3 = this.PopulateCache(element2, domNode, num++, ++docIndex);
				list.Add(domNode3);
				if (domNode2 != null)
				{
					domNode2.NextSibling = domNode3;
				}
				else
				{
					domNode.FirstChild = domNode3;
				}
				domNode2 = domNode3;
				docIndex = domNode3.End;
			}
			domNode.End = docIndex;
			domNode.Children = list;
			return domNode;
		}

		// Token: 0x060084DA RID: 34010 RVA: 0x001BFC08 File Offset: 0x001BDE08
		public static HtmlDoc Create(string s)
		{
			return new HtmlDoc(new HtmlParser(Configuration.Default.WithCss(null).WithLocaleBasedEncoding()).Parse(s));
		}

		// Token: 0x060084DB RID: 34011 RVA: 0x001BFC08 File Offset: 0x001BDE08
		public static HtmlDoc CreateHtmlDocument(string s)
		{
			return new HtmlDoc(new HtmlParser(Configuration.Default.WithCss(null).WithLocaleBasedEncoding()).Parse(s));
		}

		// Token: 0x060084DC RID: 34012 RVA: 0x001BFC2C File Offset: 0x001BDE2C
		public static string NormalizeText(string textValue)
		{
			if (textValue == null)
			{
				return null;
			}
			IEnumerable<char> enumerable = textValue.Where((char c) => !char.IsWhiteSpace(c));
			Func<char, char> func;
			if ((func = HtmlDoc.<>O.<0>__ReplaceSpecialChar) == null)
			{
				func = (HtmlDoc.<>O.<0>__ReplaceSpecialChar = new Func<char, char>(HtmlDoc.ReplaceSpecialChar));
			}
			return new string(enumerable.Select(func).ToArray<char>()).Replace("…", "...");
		}

		// Token: 0x060084DD RID: 34013 RVA: 0x001BFC9C File Offset: 0x001BDE9C
		private static char ReplaceSpecialChar(char c)
		{
			switch (c)
			{
			case '–':
				return '-';
			case '—':
				return '-';
			case '―':
				return '-';
			case '‖':
			case '‚':
				break;
			case '‗':
				return '_';
			case '‘':
				return '\'';
			case '’':
				return '\'';
			case '‛':
				return '\'';
			case '“':
				return '"';
			case '”':
				return '"';
			case '„':
				return '"';
			default:
				if (c == '′')
				{
					return '\'';
				}
				if (c == '″')
				{
					return '"';
				}
				break;
			}
			return c;
		}

		// Token: 0x060084DE RID: 34014 RVA: 0x001BFD1C File Offset: 0x001BDF1C
		public List<DomNode> Select(string selector)
		{
			return this._doc.QuerySelectorAll(selector).ToDomNodes(this);
		}

		// Token: 0x060084DF RID: 34015 RVA: 0x001BFD30 File Offset: 0x001BDF30
		public DomNode GetDomNode(string selector)
		{
			return this.Select(selector)[0];
		}

		// Token: 0x060084E0 RID: 34016 RVA: 0x001BFD3F File Offset: 0x001BDF3F
		public WebRegion GetRegion(string selector)
		{
			return this.Select(selector)[0].ToWebRegion();
		}

		// Token: 0x060084E1 RID: 34017 RVA: 0x001BFD53 File Offset: 0x001BDF53
		public bool IsValidHtml()
		{
			return this.IsValid;
		}

		// Token: 0x060084E2 RID: 34018 RVA: 0x001BFD5B File Offset: 0x001BDF5B
		public DomNode GetDomNode(IElement domElement)
		{
			return this._domNodeCache[domElement];
		}

		// Token: 0x060084E3 RID: 34019 RVA: 0x001BC3C8 File Offset: 0x001BA5C8
		public WebRegion GetWebRegion(DomNode node)
		{
			return new WebRegion(node);
		}

		// Token: 0x040036CE RID: 14030
		public const string BrTag = "BR";

		// Token: 0x040036CF RID: 14031
		public const string DivTag = "DIV";

		// Token: 0x040036D0 RID: 14032
		public const string H1Tag = "H1";

		// Token: 0x040036D1 RID: 14033
		public const string H2Tag = "H2";

		// Token: 0x040036D2 RID: 14034
		public const string H3Tag = "H3";

		// Token: 0x040036D3 RID: 14035
		public const string H4Tag = "H4";

		// Token: 0x040036D4 RID: 14036
		public const string H5Tag = "H5";

		// Token: 0x040036D5 RID: 14037
		public const string H6Tag = "H6";

		// Token: 0x040036D6 RID: 14038
		public const string PTag = "P";

		// Token: 0x040036D7 RID: 14039
		public const string SelectTag = "SELECT";

		// Token: 0x040036D8 RID: 14040
		public const string HiddenAttribute = "hidden";

		// Token: 0x040036D9 RID: 14041
		private readonly IHtmlDocument _doc;

		// Token: 0x040036DA RID: 14042
		private readonly Dictionary<IElement, DomNode> _domNodeCache = new Dictionary<IElement, DomNode>();

		// Token: 0x02001175 RID: 4469
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040036DD RID: 14045
			public static Func<char, char> <0>__ReplaceSpecialChar;
		}
	}
}
