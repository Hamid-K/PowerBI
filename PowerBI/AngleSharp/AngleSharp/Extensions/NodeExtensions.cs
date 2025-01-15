using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Html;

namespace AngleSharp.Extensions
{
	// Token: 0x020000EE RID: 238
	internal static class NodeExtensions
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x00034AD5 File Offset: 0x00032CD5
		public static INode GetRoot(this INode node)
		{
			if (node.Parent == null)
			{
				return node;
			}
			return node.Parent.GetRoot();
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00034AEC File Offset: 0x00032CEC
		public static NodeList CreateChildren(this INode node)
		{
			if (!node.IsEndPoint())
			{
				return new NodeList();
			}
			return NodeList.Empty;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00034B04 File Offset: 0x00032D04
		public static bool IsEndPoint(this INode node)
		{
			NodeType nodeType = node.NodeType;
			return nodeType != NodeType.Document && nodeType != NodeType.DocumentFragment && nodeType != NodeType.Element;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00034B2C File Offset: 0x00032D2C
		public static bool IsInsertable(this INode node)
		{
			NodeType nodeType = node.NodeType;
			return nodeType == NodeType.Element || nodeType == NodeType.Comment || nodeType == NodeType.Text || nodeType == NodeType.ProcessingInstruction || nodeType == NodeType.DocumentFragment || nodeType == NodeType.DocumentType;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00034B5C File Offset: 0x00032D5C
		public static Url HyperReference(this INode node, string url)
		{
			if (url == null)
			{
				return null;
			}
			return new Url(node.BaseUrl, url);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00034B6F File Offset: 0x00032D6F
		public static bool IsDescendantOf(this INode node, INode parent)
		{
			return node.Parent != null && (node.Parent == parent || node.Parent.IsDescendantOf(parent));
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00034B92 File Offset: 0x00032D92
		public static IEnumerable<INode> GetDescendants(this INode parent)
		{
			return parent.GetDescendantsAndSelf().Skip(1);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00034BA0 File Offset: 0x00032DA0
		public static IEnumerable<INode> GetDescendantsAndSelf(this INode parent)
		{
			Stack<INode> stack = new Stack<INode>();
			stack.Push(parent);
			while (stack.Count > 0)
			{
				INode next = stack.Pop();
				yield return next;
				int i = next.ChildNodes.Length;
				while (i > 0)
				{
					stack.Push(next.ChildNodes[--i]);
				}
				next = null;
			}
			yield break;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00034BB0 File Offset: 0x00032DB0
		public static bool IsInclusiveDescendantOf(this INode node, INode parent)
		{
			return node == parent || node.IsDescendantOf(parent);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00034BBF File Offset: 0x00032DBF
		public static bool IsAncestorOf(this INode parent, INode node)
		{
			return node.IsDescendantOf(parent);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00034BC8 File Offset: 0x00032DC8
		public static IEnumerable<INode> GetAncestors(this INode node)
		{
			while ((node = node.Parent) != null)
			{
				yield return node;
			}
			yield break;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00034BD8 File Offset: 0x00032DD8
		public static IEnumerable<INode> GetInclusiveAncestors(this INode node)
		{
			do
			{
				yield return node;
			}
			while ((node = node.Parent) != null);
			yield break;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00034BE8 File Offset: 0x00032DE8
		public static bool IsInclusiveAncestorOf(this INode parent, INode node)
		{
			return node == parent || node.IsDescendantOf(parent);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00034BF8 File Offset: 0x00032DF8
		public static T GetAncestor<T>(this INode node) where T : INode
		{
			while ((node = node.Parent) != null)
			{
				if (node is T)
				{
					return (T)((object)node);
				}
			}
			return default(T);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00034C2A File Offset: 0x00032E2A
		public static bool HasDataListAncestor(this INode child)
		{
			return child.Ancestors<IHtmlDataListElement>().Any<IHtmlDataListElement>();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00034C37 File Offset: 0x00032E37
		public static bool IsSiblingOf(this INode node, INode element)
		{
			return ((node != null) ? node.Parent : null) == element.Parent;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00034C4D File Offset: 0x00032E4D
		public static int Index(this INode node)
		{
			return node.Parent.IndexOf(node);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00034C5C File Offset: 0x00032E5C
		public static int IndexOf(this INode parent, INode node)
		{
			int num = 0;
			if (parent != null)
			{
				using (IEnumerator<INode> enumerator = parent.ChildNodes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == node)
						{
							return num;
						}
						num++;
					}
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00034CB4 File Offset: 0x00032EB4
		public static bool IsPreceding(this INode before, INode after)
		{
			Queue<INode> queue = new Queue<INode>(before.GetInclusiveAncestors());
			Queue<INode> queue2 = new Queue<INode>(after.GetInclusiveAncestors());
			int num = queue2.Count - queue.Count;
			if (num != 0)
			{
				while (queue.Count > queue2.Count)
				{
					queue.Dequeue();
				}
				while (queue2.Count > queue.Count)
				{
					queue2.Dequeue();
				}
				if (NodeExtensions.IsCurrentlySame(queue2, queue))
				{
					return num > 0;
				}
			}
			while (queue.Count > 0)
			{
				before = queue.Dequeue();
				after = queue2.Dequeue();
				if (NodeExtensions.IsCurrentlySame(queue2, queue))
				{
					return before.Index() < after.Index();
				}
			}
			return false;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00034D58 File Offset: 0x00032F58
		public static bool IsFollowing(this INode after, INode before)
		{
			return before.IsPreceding(after);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00034D64 File Offset: 0x00032F64
		public static INode GetAssociatedHost(this INode node)
		{
			if (!(node is IDocumentFragment))
			{
				return null;
			}
			IDocument owner = node.Owner;
			if (owner == null)
			{
				return null;
			}
			return owner.All.OfType<IHtmlTemplateElement>().FirstOrDefault((IHtmlTemplateElement m) => m.Content == node);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00034DBC File Offset: 0x00032FBC
		public static bool IsHostIncludingInclusiveAncestor(this INode parent, INode node)
		{
			if (!parent.IsInclusiveAncestorOf(node))
			{
				INode associatedHost = node.GetRoot().GetAssociatedHost();
				return associatedHost != null && parent.IsInclusiveAncestorOf(associatedHost);
			}
			return true;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00034DEC File Offset: 0x00032FEC
		public static void EnsurePreInsertionValidity(this INode parent, INode node, INode child)
		{
			if (parent.IsEndPoint() || node.IsHostIncludingInclusiveAncestor(parent))
			{
				throw new DomException(DomError.HierarchyRequest);
			}
			if (child != null && child.Parent != parent)
			{
				throw new DomException(DomError.NotFound);
			}
			if (!(node is IElement) && !(node is ICharacterData) && !(node is IDocumentType) && !(node is IDocumentFragment))
			{
				throw new DomException(DomError.HierarchyRequest);
			}
			IDocument document = parent as IDocument;
			if (document != null)
			{
				bool flag = false;
				NodeType nodeType = node.NodeType;
				if (nodeType <= NodeType.Text)
				{
					if (nodeType != NodeType.Element)
					{
						if (nodeType == NodeType.Text)
						{
							flag = true;
						}
					}
					else
					{
						flag = document.DocumentElement != null || child is IDocumentType || child.IsFollowedByDoctype();
					}
				}
				else if (nodeType != NodeType.DocumentType)
				{
					if (nodeType == NodeType.DocumentFragment)
					{
						int elementCount = node.GetElementCount();
						flag = elementCount > 1 || node.HasTextNodes() || (elementCount == 1 && document.DocumentElement != null) || child is IDocumentType || child.IsFollowedByDoctype();
					}
				}
				else
				{
					flag = document.Doctype != null || (child != null && child.IsPrecededByElement()) || (child == null && document.DocumentElement != null);
				}
				if (flag)
				{
					throw new DomException(DomError.HierarchyRequest);
				}
			}
			else if (node is IDocumentType)
			{
				throw new DomException(DomError.HierarchyRequest);
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00034F14 File Offset: 0x00033114
		public static INode PreInsert(this INode parent, INode node, INode child)
		{
			Node node2 = parent as Node;
			Node node3 = node as Node;
			if (node2 == null)
			{
				throw new DomException(DomError.NotSupported);
			}
			parent.EnsurePreInsertionValidity(node, child);
			Node node4 = child as Node;
			if (node4 == node)
			{
				node4 = node3.NextSibling;
			}
			(parent.Owner ?? (parent as IDocument)).AdoptNode(node);
			node2.InsertBefore(node3, node4, false);
			return node;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00034F73 File Offset: 0x00033173
		public static INode PreRemove(this INode parent, INode child)
		{
			Node node = parent as Node;
			if (node == null)
			{
				throw new DomException(DomError.NotSupported);
			}
			if (child == null || child.Parent != parent)
			{
				throw new DomException(DomError.NotFound);
			}
			node.RemoveChild(child as Node, false);
			return child;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00034FA6 File Offset: 0x000331A6
		public static bool HasTextNodes(this INode node)
		{
			return node.ChildNodes.OfType<IText>().Any<IText>();
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00034FB8 File Offset: 0x000331B8
		public static bool IsFollowedByDoctype(this INode child)
		{
			if (child != null)
			{
				bool flag = true;
				foreach (INode node in child.Parent.ChildNodes)
				{
					if (flag)
					{
						flag = node != child;
					}
					else if (node.NodeType == NodeType.DocumentType)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00035028 File Offset: 0x00033228
		public static bool IsPrecededByElement(this INode child)
		{
			foreach (INode node in child.Parent.ChildNodes)
			{
				if (node == child)
				{
					break;
				}
				if (node.NodeType == NodeType.Element)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0003508C File Offset: 0x0003328C
		public static int GetElementCount(this INode parent)
		{
			int num = 0;
			using (IEnumerator<INode> enumerator = parent.ChildNodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.NodeType == NodeType.Element)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000350E0 File Offset: 0x000332E0
		public static TNode FindChild<TNode>(this INode parent) where TNode : class, INode
		{
			if (parent != null)
			{
				for (int i = 0; i < parent.ChildNodes.Length; i++)
				{
					TNode tnode = parent.ChildNodes[i] as TNode;
					if (tnode != null)
					{
						return tnode;
					}
				}
			}
			return default(TNode);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00035130 File Offset: 0x00033330
		public static TNode FindDescendant<TNode>(this INode parent) where TNode : class, INode
		{
			if (parent != null)
			{
				for (int i = 0; i < parent.ChildNodes.Length; i++)
				{
					INode node = parent.ChildNodes[i];
					TNode tnode;
					if ((tnode = node as TNode) == null)
					{
						tnode = node.FindDescendant<TNode>();
					}
					TNode tnode2 = tnode;
					if (tnode2 != null)
					{
						return tnode2;
					}
				}
			}
			return default(TNode);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00035194 File Offset: 0x00033394
		public static IElement GetAssignedSlot(this IShadowRoot root, string name)
		{
			return root.GetDescendants().OfType<IHtmlSlotElement>().FirstOrDefault((IHtmlSlotElement m) => m.Name.Is(name));
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000351CA File Offset: 0x000333CA
		private static bool IsCurrentlySame(Queue<INode> after, Queue<INode> before)
		{
			return after.Count > 0 && before.Count > 0 && after.Peek() == before.Peek();
		}
	}
}
