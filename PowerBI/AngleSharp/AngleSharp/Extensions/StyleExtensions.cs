using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;

namespace AngleSharp.Extensions
{
	// Token: 0x020000F7 RID: 247
	internal static class StyleExtensions
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x000369C8 File Offset: 0x00034BC8
		public static CssStyleDeclaration ComputeDeclarations(this StyleCollection rules, IElement element, string pseudoSelector = null)
		{
			CssStyleDeclaration cssStyleDeclaration = new CssStyleDeclaration();
			PseudoElement pseudoElement = PseudoElement.Create(element, pseudoSelector);
			if (pseudoElement != null)
			{
				element = pseudoElement;
			}
			cssStyleDeclaration.SetDeclarations(rules.ComputeCascadedStyle(element).Declarations);
			foreach (IElement element2 in element.GetAncestors().OfType<IElement>())
			{
				CssStyleDeclaration cssStyleDeclaration2 = rules.ComputeCascadedStyle(element2);
				cssStyleDeclaration.UpdateDeclarations(cssStyleDeclaration2.Declarations);
			}
			return cssStyleDeclaration;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00036A50 File Offset: 0x00034C50
		public static IEnumerable<string> GetAllStyleSheetSets(this IStyleSheetList sheets)
		{
			List<string> existing = new List<string>();
			foreach (IStyleSheet styleSheet in sheets)
			{
				string title = styleSheet.Title;
				if (!string.IsNullOrEmpty(title) && !existing.Contains(title))
				{
					existing.Add(title);
					yield return title;
				}
			}
			IEnumerator<IStyleSheet> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00036A60 File Offset: 0x00034C60
		public static IEnumerable<string> GetEnabledStyleSheetSets(this IStyleSheetList sheets)
		{
			List<string> list = new List<string>();
			foreach (IStyleSheet styleSheet in sheets)
			{
				string title = styleSheet.Title;
				if (!string.IsNullOrEmpty(title) && !list.Contains(title) && styleSheet.IsDisabled)
				{
					list.Add(title);
				}
			}
			return sheets.GetAllStyleSheetSets().Except(list);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00036ADC File Offset: 0x00034CDC
		public static void EnableStyleSheetSet(this IStyleSheetList sheets, string name)
		{
			foreach (IStyleSheet styleSheet in sheets)
			{
				string title = styleSheet.Title;
				if (!string.IsNullOrEmpty(title))
				{
					styleSheet.IsDisabled = title != name;
				}
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00036B3C File Offset: 0x00034D3C
		public static IStyleSheetList CreateStyleSheets(this INode parent)
		{
			return new StyleSheetList(parent.GetStyleSheets());
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00036B4C File Offset: 0x00034D4C
		public static IStringList CreateStyleSheetSets(this INode parent)
		{
			return new StringList(from m in parent.GetStyleSheets()
				select m.Title into m
				where m != null
				select m);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00036BAC File Offset: 0x00034DAC
		public static IEnumerable<IStyleSheet> GetStyleSheets(this INode parent)
		{
			foreach (INode child in parent.ChildNodes)
			{
				if (child.NodeType == NodeType.Element)
				{
					ILinkStyle linkStyle = child as ILinkStyle;
					if (linkStyle != null)
					{
						IStyleSheet sheet = linkStyle.Sheet;
						if (sheet != null && !sheet.IsDisabled)
						{
							yield return sheet;
						}
					}
					else
					{
						foreach (IStyleSheet styleSheet in child.GetStyleSheets())
						{
							yield return styleSheet;
						}
						IEnumerator<IStyleSheet> enumerator2 = null;
					}
				}
				child = null;
			}
			IEnumerator<INode> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00036BBC File Offset: 0x00034DBC
		public static string LocateNamespace(this IStyleSheetList sheets, string prefix)
		{
			foreach (IStyleSheet styleSheet in sheets)
			{
				CssStyleSheet cssStyleSheet = styleSheet as CssStyleSheet;
				if (!styleSheet.IsDisabled && cssStyleSheet != null)
				{
					foreach (CssNamespaceRule cssNamespaceRule in cssStyleSheet.Rules.OfType<CssNamespaceRule>())
					{
						if (cssNamespaceRule.Prefix.Is(prefix))
						{
							return cssNamespaceRule.NamespaceUri;
						}
					}
				}
			}
			return null;
		}
	}
}
