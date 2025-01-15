using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AngleSharp.Dom;
using AngleSharp.Dom.Collections;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Parser.Html
{
	// Token: 0x0200006F RID: 111
	internal static class HtmlParserExtensions
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00013E98 File Offset: 0x00012098
		public static void SetAttributes(this Element element, List<KeyValuePair<string, string>> attributes)
		{
			NamedNodeMap attributes2 = element.Attributes;
			for (int i = 0; i < attributes.Count; i++)
			{
				KeyValuePair<string, string> keyValuePair = attributes[i];
				Attr attr = new Attr(keyValuePair.Key, keyValuePair.Value);
				attributes2.FastAddItem(attr);
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00013EE0 File Offset: 0x000120E0
		public static HtmlTreeMode? SelectMode(this Element element, bool isLast, Stack<HtmlTreeMode> templateModes)
		{
			string localName = element.LocalName;
			if (localName.Is(TagNames.Select))
			{
				return new HtmlTreeMode?(HtmlTreeMode.InSelect);
			}
			if (TagNames.AllTableCells.Contains(localName))
			{
				return new HtmlTreeMode?(isLast ? HtmlTreeMode.InBody : HtmlTreeMode.InCell);
			}
			if (localName.Is(TagNames.Tr))
			{
				return new HtmlTreeMode?(HtmlTreeMode.InRow);
			}
			if (TagNames.AllTableSections.Contains(localName))
			{
				return new HtmlTreeMode?(HtmlTreeMode.InTableBody);
			}
			if (localName.Is(TagNames.Body))
			{
				return new HtmlTreeMode?(HtmlTreeMode.InBody);
			}
			if (localName.Is(TagNames.Table))
			{
				return new HtmlTreeMode?(HtmlTreeMode.InTable);
			}
			if (localName.Is(TagNames.Caption))
			{
				return new HtmlTreeMode?(HtmlTreeMode.InCaption);
			}
			if (localName.Is(TagNames.Colgroup))
			{
				return new HtmlTreeMode?(HtmlTreeMode.InColumnGroup);
			}
			if (localName.Is(TagNames.Template))
			{
				return new HtmlTreeMode?(templateModes.Peek());
			}
			if (localName.Is(TagNames.Html))
			{
				return new HtmlTreeMode?(HtmlTreeMode.BeforeHead);
			}
			if (localName.Is(TagNames.Head))
			{
				return new HtmlTreeMode?(isLast ? HtmlTreeMode.InBody : HtmlTreeMode.InHead);
			}
			if (localName.Is(TagNames.Frameset))
			{
				return new HtmlTreeMode?(HtmlTreeMode.InFrameset);
			}
			if (isLast)
			{
				return new HtmlTreeMode?(HtmlTreeMode.InBody);
			}
			return null;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		public static int GetCode(this HtmlParseError code)
		{
			return (int)code;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00014010 File Offset: 0x00012210
		public static void SetUniqueAttributes(this Element element, List<KeyValuePair<string, string>> attributes)
		{
			for (int i = attributes.Count - 1; i >= 0; i--)
			{
				if (element.HasAttribute(attributes[i].Key))
				{
					attributes.RemoveAt(i);
				}
			}
			element.SetAttributes(attributes);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00014058 File Offset: 0x00012258
		public static void AddFormatting(this List<Element> formatting, Element element)
		{
			int num = 0;
			for (int i = formatting.Count - 1; i >= 0; i--)
			{
				Element element2 = formatting[i];
				if (element2 == null)
				{
					break;
				}
				if (element2.NodeName.Is(element.NodeName) && element2.NamespaceUri.Is(element.NamespaceUri) && element2.Attributes.AreEqual(element.Attributes) && ++num == 3)
				{
					formatting.RemoveAt(i);
					break;
				}
			}
			formatting.Add(element);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000140D8 File Offset: 0x000122D8
		public static void ClearFormatting(this List<Element> formatting)
		{
			while (formatting.Count != 0)
			{
				int num = formatting.Count - 1;
				bool flag = formatting[num] != null;
				formatting.RemoveAt(num);
				if (!flag)
				{
					break;
				}
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00014108 File Offset: 0x00012308
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddScopeMarker(this List<Element> formatting)
		{
			formatting.Add(null);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00014111 File Offset: 0x00012311
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddComment(this Node parent, HtmlToken token)
		{
			parent.AddNode(new Comment(parent.Owner, token.Data));
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001412A File Offset: 0x0001232A
		public static QuirksMode GetQuirksMode(this HtmlDoctypeToken doctype)
		{
			if (doctype.IsFullQuirks)
			{
				return QuirksMode.On;
			}
			if (doctype.IsLimitedQuirks)
			{
				return QuirksMode.Limited;
			}
			return QuirksMode.Off;
		}
	}
}
