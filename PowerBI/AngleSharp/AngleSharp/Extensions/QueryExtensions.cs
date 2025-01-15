using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Extensions
{
	// Token: 0x020000F2 RID: 242
	internal static class QueryExtensions
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x0003554C File Offset: 0x0003374C
		public static IElement QuerySelector(this INodeList elements, string selectors)
		{
			ISelector selector = CssParser.Default.ParseSelector(selectors);
			QueryExtensions.Validate(selector);
			return elements.QuerySelector(selector);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00035574 File Offset: 0x00033774
		public static HtmlCollection<IElement> QuerySelectorAll(this INodeList elements, string selectors)
		{
			ISelector selector = CssParser.Default.ParseSelector(selectors);
			QueryExtensions.Validate(selector);
			List<IElement> list = new List<IElement>();
			elements.QuerySelectorAll(selector, list);
			return new HtmlCollection<IElement>(list);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000355A8 File Offset: 0x000337A8
		public static HtmlCollection<IElement> GetElementsByClassName(this INodeList elements, string classNames)
		{
			List<IElement> list = new List<IElement>();
			string[] array = classNames.SplitSpaces();
			if (array.Length != 0)
			{
				elements.GetElementsByClassName(array, list);
			}
			return new HtmlCollection<IElement>(list);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000355D4 File Offset: 0x000337D4
		public static HtmlCollection<IElement> GetElementsByTagName(this INodeList elements, string tagName)
		{
			List<IElement> list = new List<IElement>();
			elements.GetElementsByTagName(tagName.Is(Keywords.Asterisk) ? null : tagName, list);
			return new HtmlCollection<IElement>(list);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00035608 File Offset: 0x00033808
		public static HtmlCollection<IElement> GetElementsByTagName(this INodeList elements, string namespaceUri, string localName)
		{
			List<IElement> list = new List<IElement>();
			elements.GetElementsByTagName(namespaceUri, localName.Is(Keywords.Asterisk) ? null : localName, list);
			return new HtmlCollection<IElement>(list);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0003563A File Offset: 0x0003383A
		public static T QuerySelector<T>(this INodeList elements, ISelector selectors) where T : class, IElement
		{
			return elements.QuerySelector(selectors) as T;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00035650 File Offset: 0x00033850
		public static IElement QuerySelector(this INodeList elements, ISelector selector)
		{
			for (int i = 0; i < elements.Length; i++)
			{
				IElement element = elements[i] as IElement;
				if (element != null)
				{
					if (selector.Match(element))
					{
						return element;
					}
					if (element.HasChildNodes)
					{
						element = element.ChildNodes.QuerySelector(selector);
						if (element != null)
						{
							return element;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000356A4 File Offset: 0x000338A4
		public static HtmlCollection<IElement> QuerySelectorAll(this INodeList elements, ISelector selector)
		{
			List<IElement> list = new List<IElement>();
			elements.QuerySelectorAll(selector, list);
			return new HtmlCollection<IElement>(list);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000356C8 File Offset: 0x000338C8
		public static void QuerySelectorAll(this INodeList elements, ISelector selector, List<IElement> result)
		{
			for (int i = 0; i < elements.Length; i++)
			{
				IElement element = elements[i] as IElement;
				if (element != null)
				{
					foreach (IElement element2 in element.DescendentsAndSelf<IElement>())
					{
						if (selector.Match(element2))
						{
							result.Add(element2);
						}
					}
				}
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00035740 File Offset: 0x00033940
		public static bool Contains(this ITokenList list, string[] tokens)
		{
			for (int i = 0; i < tokens.Length; i++)
			{
				if (!list.Contains(tokens[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0003576C File Offset: 0x0003396C
		public static void GetElementsByClassName(this INodeList elements, string[] classNames, List<IElement> result)
		{
			for (int i = 0; i < elements.Length; i++)
			{
				IElement element = elements[i] as IElement;
				if (element != null)
				{
					if (element.ClassList.Contains(classNames))
					{
						result.Add(element);
					}
					if (element.ChildElementCount != 0)
					{
						element.ChildNodes.GetElementsByClassName(classNames, result);
					}
				}
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000357C4 File Offset: 0x000339C4
		public static void GetElementsByTagName(this INodeList elements, string tagName, List<IElement> result)
		{
			for (int i = 0; i < elements.Length; i++)
			{
				IElement element = elements[i] as IElement;
				if (element != null)
				{
					if (tagName == null || tagName.Isi(element.LocalName))
					{
						result.Add(element);
					}
					if (element.ChildElementCount != 0)
					{
						element.ChildNodes.GetElementsByTagName(tagName, result);
					}
				}
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00035820 File Offset: 0x00033A20
		public static void GetElementsByTagName(this INodeList elements, string namespaceUri, string localName, List<IElement> result)
		{
			for (int i = 0; i < elements.Length; i++)
			{
				IElement element = elements[i] as IElement;
				if (element != null)
				{
					if (element.NamespaceUri.Is(namespaceUri) && (localName == null || localName.Isi(element.LocalName)))
					{
						result.Add(element);
					}
					if (element.ChildElementCount != 0)
					{
						element.ChildNodes.GetElementsByTagName(namespaceUri, localName, result);
					}
				}
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0003588A File Offset: 0x00033A8A
		private static void Validate(ISelector selector)
		{
			if (selector == null || selector is UnknownSelector)
			{
				throw new DomException(DomError.Syntax);
			}
		}
	}
}
