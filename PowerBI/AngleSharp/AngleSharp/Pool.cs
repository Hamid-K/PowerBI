using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using AngleSharp.Services;

namespace AngleSharp
{
	// Token: 0x0200001D RID: 29
	internal static class Pool
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000065BC File Offset: 0x000047BC
		public static StringBuilder NewStringBuilder()
		{
			object @lock = Pool._lock;
			StringBuilder stringBuilder;
			lock (@lock)
			{
				if (Pool._builder.Count == 0)
				{
					stringBuilder = new StringBuilder(1024);
				}
				else
				{
					stringBuilder = Pool._builder.Pop().Clear();
				}
			}
			return stringBuilder;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006620 File Offset: 0x00004820
		public static CssSelectorConstructor NewSelectorConstructor(IAttributeSelectorFactory attributeSelector, IPseudoClassSelectorFactory pseudoClassSelector, IPseudoElementSelectorFactory pseudoElementSelector)
		{
			object @lock = Pool._lock;
			CssSelectorConstructor cssSelectorConstructor;
			lock (@lock)
			{
				if (Pool._selector.Count == 0)
				{
					cssSelectorConstructor = new CssSelectorConstructor(attributeSelector, pseudoClassSelector, pseudoElementSelector);
				}
				else
				{
					cssSelectorConstructor = Pool._selector.Pop().Reset(attributeSelector, pseudoClassSelector, pseudoElementSelector);
				}
			}
			return cssSelectorConstructor;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006684 File Offset: 0x00004884
		public static CssValueBuilder NewValueBuilder()
		{
			object @lock = Pool._lock;
			CssValueBuilder cssValueBuilder;
			lock (@lock)
			{
				if (Pool._value.Count == 0)
				{
					cssValueBuilder = new CssValueBuilder();
				}
				else
				{
					cssValueBuilder = Pool._value.Pop().Reset();
				}
			}
			return cssValueBuilder;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000066E4 File Offset: 0x000048E4
		public static string ToPool(this StringBuilder sb)
		{
			string text = sb.ToString();
			object @lock = Pool._lock;
			lock (@lock)
			{
				Pool._builder.Push(sb);
			}
			return text;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006730 File Offset: 0x00004930
		public static ISelector ToPool(this CssSelectorConstructor ctor)
		{
			ISelector result = ctor.GetResult();
			object @lock = Pool._lock;
			lock (@lock)
			{
				Pool._selector.Push(ctor);
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000677C File Offset: 0x0000497C
		public static CssValue ToPool(this CssValueBuilder vb)
		{
			CssValue result = vb.GetResult();
			object @lock = Pool._lock;
			lock (@lock)
			{
				Pool._value.Push(vb);
			}
			return result;
		}

		// Token: 0x0400017D RID: 381
		private static readonly Stack<StringBuilder> _builder = new Stack<StringBuilder>();

		// Token: 0x0400017E RID: 382
		private static readonly Stack<CssSelectorConstructor> _selector = new Stack<CssSelectorConstructor>();

		// Token: 0x0400017F RID: 383
		private static readonly Stack<CssValueBuilder> _value = new Stack<CssValueBuilder>();

		// Token: 0x04000180 RID: 384
		private static readonly object _lock = new object();
	}
}
