using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Html
{
	// Token: 0x02000AC1 RID: 2753
	internal class HtmlDomValueCreator
	{
		// Token: 0x06004CF4 RID: 19700 RVA: 0x000FDB86 File Offset: 0x000FBD86
		public static ListValue CreateListValue(DomNode node)
		{
			if (node != null)
			{
				return ListValue.New(new Value[] { HtmlDomValueCreator.CreateRecordValue(node, new Dictionary<DomNode, RecordValue>()) });
			}
			return ListValue.Empty;
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x000FDBAC File Offset: 0x000FBDAC
		private static RecordValue CreateRecordValue(DomNode node, Dictionary<DomNode, RecordValue> visitedNodes)
		{
			RecordValue recordValue;
			if (visitedNodes.TryGetValue(node, out recordValue))
			{
				return recordValue;
			}
			visitedNodes.Add(node, null);
			DomNodeKind kind = node.Kind;
			if (kind != DomNodeKind.Element)
			{
				if (kind != DomNodeKind.Text)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				recordValue = RecordValue.New(HtmlDomValueCreator.elementKeys, new Value[]
				{
					HtmlDomValueCreator.textKind,
					Value.Null,
					Value.Null,
					TextValue.New(node.Text)
				});
			}
			else
			{
				recordValue = RecordValue.New(HtmlDomValueCreator.elementKeys, new Value[]
				{
					HtmlDomValueCreator.elementKind,
					TextValue.New(node.Name),
					HtmlDomValueCreator.CreateValue(node.Children, visitedNodes),
					Value.Null
				});
			}
			visitedNodes[node] = recordValue;
			return recordValue;
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x000FDC74 File Offset: 0x000FBE74
		private static Value CreateValue(List<DomNode> children, Dictionary<DomNode, RecordValue> visitedNodes)
		{
			if (children.Count == 0)
			{
				return Value.Null;
			}
			List<Value> list = null;
			foreach (DomNode domNode in children)
			{
				RecordValue recordValue = HtmlDomValueCreator.CreateRecordValue(domNode, visitedNodes);
				if (list == null)
				{
					list = new List<Value>();
				}
				list.Add(recordValue);
			}
			if (list == null)
			{
				return Value.Null;
			}
			return ListValue.New(list.ToArray()).ToTable(HtmlDocumentResult.Type);
		}

		// Token: 0x040028EE RID: 10478
		public const string Children = "Children";

		// Token: 0x040028EF RID: 10479
		public const string Element = "Element";

		// Token: 0x040028F0 RID: 10480
		public const string Kind = "Kind";

		// Token: 0x040028F1 RID: 10481
		public const string Name = "Name";

		// Token: 0x040028F2 RID: 10482
		public const string Text = "Text";

		// Token: 0x040028F3 RID: 10483
		private static readonly Keys elementKeys = Keys.New("Kind", "Name", "Children", "Text");

		// Token: 0x040028F4 RID: 10484
		private static readonly Value elementKind = TextValue.New("Element");

		// Token: 0x040028F5 RID: 10485
		private static readonly Value textKind = TextValue.New("Text");
	}
}
