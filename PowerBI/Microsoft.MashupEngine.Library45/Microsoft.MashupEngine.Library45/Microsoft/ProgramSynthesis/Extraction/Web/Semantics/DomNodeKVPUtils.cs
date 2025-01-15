using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Semantics
{
	// Token: 0x02001169 RID: 4457
	public static class DomNodeKVPUtils
	{
		// Token: 0x0600846B RID: 33899 RVA: 0x001BE7CC File Offset: 0x001BC9CC
		internal static IEnumerable<IDomNode> GetLeftKeys(IDomNode node, int maxDistance = 0)
		{
			if (node.Parent == null)
			{
				return Enumerable.Empty<IDomNode>();
			}
			IEnumerable<IDomNode> enumerable = from x in node.Parent.GetChildren()
				where x.Index < node.Index && x.TrimmedInnerText.Length > 0
				select x;
			if (maxDistance == 0)
			{
				return enumerable;
			}
			return enumerable.Concat(DomNodeKVPUtils.GetLeftKeys((DomNode)node.Parent, maxDistance - 1));
		}

		// Token: 0x0600846C RID: 33900 RVA: 0x001BE840 File Offset: 0x001BCA40
		internal static IEnumerable<IDomNode> GetTopKeys(IDomNode node, int maxDistance = 0)
		{
			IEnumerable<IDomNode> enumerable = Enumerable.Empty<IDomNode>();
			if (node.Parent == null)
			{
				return enumerable;
			}
			if (!node.NodeName.Equals("TD", StringComparison.OrdinalIgnoreCase))
			{
				if (maxDistance > 0)
				{
					return DomNodeKVPUtils.GetTopKeys((DomNode)node.Parent, maxDistance - 1);
				}
				return enumerable;
			}
			else
			{
				int colNo = node.Index;
				IDomNode curRow = node.Parent;
				if (curRow.Index == 0)
				{
					if (maxDistance > 0 && curRow.Parent != null)
					{
						return DomNodeKVPUtils.GetTopKeys((DomNode)node.Parent, maxDistance - 1);
					}
					return enumerable;
				}
				else
				{
					IDomNode domNode = curRow.Parent.GetChildren().FirstOrDefault((IDomNode x) => x.Index < curRow.Index && x.TrimmedInnerText.Length > 0);
					if (domNode == null)
					{
						if (maxDistance > 0)
						{
							return DomNodeKVPUtils.GetTopKeys((DomNode)node.Parent, maxDistance - 1);
						}
						return enumerable;
					}
					else
					{
						enumerable = from x in domNode.GetChildren()
							where x.Index == colNo
							select x;
						Func<IDomNode, bool> <>9__3;
						int idx2;
						int idx;
						for (idx = curRow.Index - 1; idx > domNode.Index; idx = idx2 - 1)
						{
							IDomNode domNode2 = curRow.Parent.GetChildren().FirstOrDefault((IDomNode x) => x.Index == idx);
							if (domNode2 != null && domNode2.TrimmedInnerText.Length > 0)
							{
								IEnumerable<IDomNode> enumerable2 = enumerable;
								IEnumerable<IDomNode> children = domNode2.GetChildren();
								Func<IDomNode, bool> func;
								if ((func = <>9__3) == null)
								{
									func = (<>9__3 = (IDomNode x) => x.Index == colNo);
								}
								enumerable = enumerable2.Concat(children.Where(func));
								break;
							}
							idx2 = idx;
						}
						if (node.Parent.Parent != null && maxDistance > 0)
						{
							return enumerable.Concat(DomNodeKVPUtils.GetTopKeys((DomNode)node.Parent, maxDistance - 1));
						}
						return enumerable;
					}
				}
			}
		}

		// Token: 0x0600846D RID: 33901 RVA: 0x001BEA0C File Offset: 0x001BCC0C
		internal static IEnumerable<IDomNode> GetKeys(IDomNode node, KeyDirections dir, int maxDistance = 0)
		{
			switch (dir)
			{
			case KeyDirections.Top:
				return DomNodeKVPUtils.GetTopKeys(node, maxDistance);
			case KeyDirections.Left:
				return DomNodeKVPUtils.GetLeftKeys(node, maxDistance);
			case KeyDirections.Any:
				return DomNodeKVPUtils.GetLeftKeys(node, maxDistance).Concat(DomNodeKVPUtils.GetTopKeys(node, maxDistance));
			case KeyDirections.Self:
				return new List<IDomNode> { node };
			default:
				return Enumerable.Empty<IDomNode>();
			}
		}
	}
}
