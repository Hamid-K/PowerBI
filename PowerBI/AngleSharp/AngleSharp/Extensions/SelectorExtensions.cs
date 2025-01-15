using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Extensions
{
	// Token: 0x020000F5 RID: 245
	public static class SelectorExtensions
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x00035CF6 File Offset: 0x00033EF6
		public static IEnumerable<T> Is<T>(this IEnumerable<T> elements, string selectorText) where T : IElement
		{
			return elements.Filter(selectorText, true);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00035D00 File Offset: 0x00033F00
		public static IEnumerable<T> Not<T>(this IEnumerable<T> elements, string selectorText) where T : IElement
		{
			return elements.Filter(selectorText, false);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00035D0A File Offset: 0x00033F0A
		public static IEnumerable<IElement> Children(this IEnumerable<IElement> elements, string selectorText = null)
		{
			return elements.GetMany((IElement m) => m.Children, selectorText);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00035D32 File Offset: 0x00033F32
		public static IEnumerable<IElement> Siblings(this IEnumerable<IElement> elements, string selectorText = null)
		{
			return elements.GetMany((IElement m) => m.Parent.ChildNodes.OfType<IElement>().Except(m), selectorText);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00035D5A File Offset: 0x00033F5A
		public static IEnumerable<IElement> Parent(this IEnumerable<IElement> elements, string selectorText = null)
		{
			return elements.Get((IElement m) => m.ParentElement, selectorText);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00035D82 File Offset: 0x00033F82
		public static IEnumerable<IElement> Next(this IEnumerable<IElement> elements, string selectorText = null)
		{
			return elements.Get((IElement m) => m.NextElementSibling, selectorText);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00035DAA File Offset: 0x00033FAA
		public static IEnumerable<IElement> Previous(this IEnumerable<IElement> elements, string selectorText = null)
		{
			return elements.Get((IElement m) => m.PreviousElementSibling, selectorText);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00035DD2 File Offset: 0x00033FD2
		public static IEnumerable<T> Is<T>(this IEnumerable<T> elements, ISelector selector) where T : IElement
		{
			return elements.Filter(selector, true);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00035DDC File Offset: 0x00033FDC
		public static IEnumerable<T> Not<T>(this IEnumerable<T> elements, ISelector selector) where T : IElement
		{
			return elements.Filter(selector, false);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00035DE6 File Offset: 0x00033FE6
		public static IEnumerable<IElement> Children(this IEnumerable<IElement> elements, ISelector selector = null)
		{
			return elements.GetMany((IElement m) => m.Children, selector);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00035E0E File Offset: 0x0003400E
		public static IEnumerable<IElement> Siblings(this IEnumerable<IElement> elements, ISelector selector = null)
		{
			return elements.GetMany((IElement m) => m.Parent.ChildNodes.OfType<IElement>().Except(m), selector);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00035E36 File Offset: 0x00034036
		public static IEnumerable<IElement> Parent(this IEnumerable<IElement> elements, ISelector selector = null)
		{
			return elements.Get((IElement m) => m.ParentElement, selector);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00035E5E File Offset: 0x0003405E
		public static IEnumerable<IElement> Next(this IEnumerable<IElement> elements, ISelector selector = null)
		{
			return elements.Get((IElement m) => m.NextElementSibling, selector);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00035E86 File Offset: 0x00034086
		public static IEnumerable<IElement> Previous(this IEnumerable<IElement> elements, ISelector selector = null)
		{
			return elements.Get((IElement m) => m.PreviousElementSibling, selector);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00035EAE File Offset: 0x000340AE
		private static IEnumerable<IElement> GetMany(this IEnumerable<IElement> elements, Func<IElement, IEnumerable<IElement>> getter, ISelector selector)
		{
			if (selector == null)
			{
				selector = SimpleSelector.All;
			}
			foreach (IElement element in elements)
			{
				IEnumerable<IElement> enumerable = getter(element);
				foreach (IElement element2 in enumerable)
				{
					if (selector.Match(element2))
					{
						yield return element2;
					}
				}
				IEnumerator<IElement> enumerator2 = null;
			}
			IEnumerator<IElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00035ECC File Offset: 0x000340CC
		private static IEnumerable<IElement> GetMany(this IEnumerable<IElement> elements, Func<IElement, IEnumerable<IElement>> getter, string selectorText)
		{
			if (selectorText != null)
			{
				ISelector selector = SelectorExtensions.CreateSelector(selectorText);
				return elements.GetMany(getter, selector);
			}
			return elements.GetMany(getter, SimpleSelector.All);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00035EF8 File Offset: 0x000340F8
		private static IEnumerable<IElement> Get(this IEnumerable<IElement> elements, Func<IElement, IElement> getter, ISelector selector)
		{
			if (selector == null)
			{
				selector = SimpleSelector.All;
			}
			foreach (IElement element in elements)
			{
				IElement child;
				for (child = getter(element); child != null; child = getter(child))
				{
					if (selector.Match(child))
					{
						yield return child;
						break;
					}
				}
				child = null;
			}
			IEnumerator<IElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00035F18 File Offset: 0x00034118
		private static IEnumerable<IElement> Get(this IEnumerable<IElement> elements, Func<IElement, IElement> getter, string selectorText)
		{
			if (selectorText != null)
			{
				ISelector selector = SelectorExtensions.CreateSelector(selectorText);
				return elements.Get(getter, selector);
			}
			return elements.Get(getter, SimpleSelector.All);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00035F44 File Offset: 0x00034144
		private static IEnumerable<IElement> Except(this IEnumerable<IElement> elements, IElement excluded)
		{
			foreach (IElement element in elements)
			{
				if (element != excluded)
				{
					yield return element;
				}
			}
			IEnumerator<IElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00035F5B File Offset: 0x0003415B
		private static IEnumerable<T> Filter<T>(this IEnumerable<T> elements, ISelector selector, bool result) where T : IElement
		{
			if (selector == null)
			{
				selector = SimpleSelector.All;
			}
			foreach (T t in elements)
			{
				if (selector.Match(t) == result)
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00035F7C File Offset: 0x0003417C
		private static IEnumerable<T> Filter<T>(this IEnumerable<T> elements, string selectorText, bool result) where T : IElement
		{
			if (selectorText != null)
			{
				ISelector selector = SelectorExtensions.CreateSelector(selectorText);
				return elements.Filter(selector, result);
			}
			return elements.Filter(SimpleSelector.All, result);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00035FA8 File Offset: 0x000341A8
		private static ISelector CreateSelector(string selector)
		{
			return CssParser.Default.ParseSelector(selector);
		}
	}
}
