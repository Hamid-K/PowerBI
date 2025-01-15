using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Semantics
{
	// Token: 0x02001172 RID: 4466
	internal static class DomNodeExt
	{
		// Token: 0x060084CA RID: 33994 RVA: 0x001BF8F3 File Offset: 0x001BDAF3
		internal static List<DomNode> ToDomNodes(this IHtmlDocument document, HtmlDoc doc)
		{
			return document.All.ToDomNodes(doc);
		}

		// Token: 0x060084CB RID: 33995 RVA: 0x001BF904 File Offset: 0x001BDB04
		internal static List<DomNode> ToDomNodes(this IEnumerable<IElement> objs, HtmlDoc doc)
		{
			List<DomNode> list = new List<DomNode>();
			foreach (IElement element in objs)
			{
				DomNode domNode = doc.GetDomNode(element);
				list.Add(domNode);
			}
			return list;
		}

		// Token: 0x060084CC RID: 33996 RVA: 0x001BF95C File Offset: 0x001BDB5C
		internal static List<WebRegion> ToWebRegions(this List<DomNode> nodes)
		{
			List<WebRegion> list = new List<WebRegion>();
			for (int i = 0; i < nodes.Count; i++)
			{
				WebRegion webRegion = nodes[i].ToWebRegion();
				list.Add(webRegion);
			}
			return list;
		}

		// Token: 0x060084CD RID: 33997 RVA: 0x001BF995 File Offset: 0x001BDB95
		internal static WebRegion ToWebRegion(this DomNode node)
		{
			return node.Doc.GetWebRegion(node);
		}

		// Token: 0x060084CE RID: 33998 RVA: 0x001BF9A4 File Offset: 0x001BDBA4
		public static IDomNode LowestCommonAncestor(IReadOnlyList<IDomNode> nodes)
		{
			if (nodes == null || !nodes.Any<IDomNode>())
			{
				return null;
			}
			IDomNode domNode = nodes.ArgMin((IDomNode n) => n.Start);
			IDomNode domNode2 = nodes.ArgMax((IDomNode n) => n.Start);
			if (domNode == domNode2)
			{
				return domNode;
			}
			for (IDomNode domNode3 = domNode; domNode3 != null; domNode3 = domNode3.Parent)
			{
				if (domNode2.IsAncestor(domNode3))
				{
					return domNode3;
				}
			}
			return null;
		}

		// Token: 0x060084CF RID: 33999 RVA: 0x001BFA2C File Offset: 0x001BDC2C
		public static IDomNode LastUnderLCA(IReadOnlyList<IDomNode> nodes)
		{
			IDomNode domNode = DomNodeExt.LowestCommonAncestor(nodes);
			if (domNode == null)
			{
				return null;
			}
			while (domNode.ChildrenCount > 0)
			{
				domNode = domNode.GetChildren().Last<IDomNode>();
			}
			return domNode;
		}
	}
}
