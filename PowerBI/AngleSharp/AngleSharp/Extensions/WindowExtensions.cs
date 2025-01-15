using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Css;
using AngleSharp.Dom;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Html;

namespace AngleSharp.Extensions
{
	// Token: 0x020000FC RID: 252
	internal static class WindowExtensions
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00037CF8 File Offset: 0x00035EF8
		public static CssStyleDeclaration ComputeDefaultStyle(this IWindow window, IElement element)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00037CF8 File Offset: 0x00035EF8
		public static CssStyleDeclaration ComputeRawStyle(this IWindow window, IElement element)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00037CF8 File Offset: 0x00035EF8
		public static CssStyleDeclaration ComputeUsedStyle(this IWindow window, IElement element)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00037D00 File Offset: 0x00035F00
		public static CssStyleDeclaration ComputeCascadedStyle(this StyleCollection styleCollection, IElement element)
		{
			CssStyleDeclaration cssStyleDeclaration = new CssStyleDeclaration();
			foreach (CssStyleRule cssStyleRule in styleCollection.SortBySpecifity(element))
			{
				CssStyleDeclaration style = cssStyleRule.Style;
				cssStyleDeclaration.SetDeclarations(style.Declarations);
			}
			if (element is IHtmlElement)
			{
				IHtmlElement htmlElement = (IHtmlElement)element;
				if (htmlElement.Style != null)
				{
					IEnumerable<CssProperty> enumerable = htmlElement.Style.OfType<CssProperty>();
					cssStyleDeclaration.SetDeclarations(enumerable);
				}
			}
			return cssStyleDeclaration;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00037D8C File Offset: 0x00035F8C
		public static StyleCollection GetStyleCollection(this IWindow window)
		{
			RenderDevice renderDevice = new RenderDevice(window.OuterWidth, window.OuterHeight);
			return new StyleCollection(window.Document.GetStyleSheets().OfType<CssStyleSheet>(), renderDevice);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00037DC4 File Offset: 0x00035FC4
		private static IEnumerable<CssStyleRule> SortBySpecifity(this IEnumerable<CssStyleRule> rules, IElement element)
		{
			return from m in rules
				where m.Selector.Match(element)
				orderby m.Selector.Specifity
				select m;
		}
	}
}
