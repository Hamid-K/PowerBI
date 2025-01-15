using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Html;
using AngleSharp.Html;

namespace AngleSharp.Extensions
{
	// Token: 0x020000F8 RID: 248
	public static class StyleSheetExtensions
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00036C64 File Offset: 0x00034E64
		public static TRule AddNewRule<TRule>(this ICssRuleCreator creator) where TRule : ICssRule
		{
			string fullName = typeof(TRule).FullName;
			CssRuleType cssRuleType = CssRuleType.Unknown;
			if (StyleSheetExtensions.RuleMapping.TryGetValue(fullName, out cssRuleType))
			{
				ICssRule cssRule = creator.AddNewRule(cssRuleType);
				if (cssRule is TRule)
				{
					return (TRule)((object)cssRule);
				}
			}
			return default(TRule);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00036CB4 File Offset: 0x00034EB4
		public static ICssStyleRule AddNewStyle(this ICssRuleCreator creator, string selector = null, IDictionary<string, string> declarations = null)
		{
			ICssStyleRule cssStyleRule = creator.AddNewRule<ICssStyleRule>();
			if (!string.IsNullOrEmpty(selector))
			{
				cssStyleRule.SelectorText = selector;
			}
			if (declarations != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in declarations)
				{
					cssStyleRule.Style.SetProperty(keyValuePair.Key, keyValuePair.Value, null);
				}
			}
			return cssStyleRule;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00036D2C File Offset: 0x00034F2C
		public static ICssStyleRule AddNewStyle(this ICssRuleCreator creator, string selector, object declarations)
		{
			return creator.AddNewStyle(selector, declarations.ToDictionary());
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00036D3C File Offset: 0x00034F3C
		public static IEnumerable<TRule> RulesOf<TRule>(this IEnumerable<IStyleSheet> sheets) where TRule : ICssRule
		{
			if (sheets == null)
			{
				throw new ArgumentNullException("sheets");
			}
			return sheets.Where((IStyleSheet m) => !m.IsDisabled).OfType<ICssStyleSheet>().SelectMany((ICssStyleSheet m) => m.Rules)
				.OfType<TRule>();
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00036DAC File Offset: 0x00034FAC
		public static IEnumerable<ICssStyleRule> StylesWith(this IEnumerable<IStyleSheet> sheets, ISelector selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			string selectorText = selector.Text;
			return from m in sheets.RulesOf<ICssStyleRule>()
				where m.SelectorText == selectorText
				select m;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00036DF0 File Offset: 0x00034FF0
		public static IDocument GetDocument(this IStyleSheet sheet)
		{
			if (sheet == null)
			{
				return null;
			}
			IElement ownerNode = sheet.OwnerNode;
			if (ownerNode == null)
			{
				return null;
			}
			return ownerNode.Owner;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00036E08 File Offset: 0x00035008
		public static IEnumerable<ICssComment> GetComments(this ICssNode node)
		{
			return node.GetAll<ICssComment>();
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00036E10 File Offset: 0x00035010
		public static IEnumerable<ICssNode> GetAllDescendents(this ICssNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			return node.Children.SelectMany((ICssNode m) => m.GetAllDescendents());
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00036E4A File Offset: 0x0003504A
		public static IEnumerable<T> GetAll<T>(this ICssNode node) where T : IStyleFormattable
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node is T)
			{
				yield return (T)((object)node);
			}
			foreach (T t in node.Children.SelectMany((ICssNode m) => m.GetAll<T>()))
			{
				yield return t;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00036E5A File Offset: 0x0003505A
		public static bool IsPersistent(this IHtmlLinkElement link)
		{
			return link.Relation.Isi(LinkRelNames.StyleSheet) && link.Title == null;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00036E79 File Offset: 0x00035079
		public static bool IsPreferred(this IHtmlLinkElement link)
		{
			return link.Relation.Isi(LinkRelNames.StyleSheet) && link.Title != null;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00036E98 File Offset: 0x00035098
		public static bool IsAlternate(this IHtmlLinkElement link)
		{
			ITokenList relationList = link.RelationList;
			return relationList.Contains(LinkRelNames.StyleSheet) && relationList.Contains(LinkRelNames.Alternate) && link.Title != null;
		}

		// Token: 0x04000608 RID: 1544
		private static readonly Dictionary<string, CssRuleType> RuleMapping = new Dictionary<string, CssRuleType>
		{
			{
				typeof(ICssCharsetRule).FullName,
				CssRuleType.Charset
			},
			{
				typeof(ICssCounterStyleRule).FullName,
				CssRuleType.CounterStyle
			},
			{
				typeof(ICssDocumentRule).FullName,
				CssRuleType.Document
			},
			{
				typeof(ICssFontFaceRule).FullName,
				CssRuleType.FontFace
			},
			{
				typeof(ICssFontFeatureValuesRule).FullName,
				CssRuleType.FontFeatureValues
			},
			{
				typeof(ICssImportRule).FullName,
				CssRuleType.Import
			},
			{
				typeof(ICssKeyframeRule).FullName,
				CssRuleType.Keyframe
			},
			{
				typeof(ICssKeyframesRule).FullName,
				CssRuleType.Keyframes
			},
			{
				typeof(ICssMediaRule).FullName,
				CssRuleType.Media
			},
			{
				typeof(ICssNamespaceRule).FullName,
				CssRuleType.Namespace
			},
			{
				typeof(ICssPageRule).FullName,
				CssRuleType.Page
			},
			{
				typeof(ICssStyleRule).FullName,
				CssRuleType.Style
			},
			{
				typeof(ICssSupportsRule).FullName,
				CssRuleType.Supports
			}
		};
	}
}
