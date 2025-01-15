using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;

namespace AngleSharp.Extensions
{
	// Token: 0x020000F0 RID: 240
	internal static class ParentNodeExtensions
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x00035310 File Offset: 0x00033510
		public static INode MutationMacro(this INode parent, INode[] nodes)
		{
			if (nodes.Length > 1)
			{
				IDocumentFragment documentFragment = parent.Owner.CreateDocumentFragment();
				for (int i = 0; i < nodes.Length; i++)
				{
					documentFragment.AppendChild(nodes[i]);
				}
				return documentFragment;
			}
			return nodes[0];
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0003534C File Offset: 0x0003354C
		public static void PrependNodes(this INode parent, params INode[] nodes)
		{
			if (nodes.Length != 0)
			{
				INode node = parent.MutationMacro(nodes);
				parent.PreInsert(node, parent.FirstChild);
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00035374 File Offset: 0x00033574
		public static void AppendNodes(this INode parent, params INode[] nodes)
		{
			if (nodes.Length != 0)
			{
				INode node = parent.MutationMacro(nodes);
				parent.PreInsert(node, null);
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00035398 File Offset: 0x00033598
		public static void InsertBefore(this INode child, params INode[] nodes)
		{
			INode parent = child.Parent;
			if (parent != null && nodes.Length != 0)
			{
				INode node = parent.MutationMacro(nodes);
				parent.PreInsert(node, child);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000353C4 File Offset: 0x000335C4
		public static void InsertAfter(this INode child, params INode[] nodes)
		{
			INode parent = child.Parent;
			if (parent != null && nodes.Length != 0)
			{
				INode node = parent.MutationMacro(nodes);
				parent.PreInsert(node, child.NextSibling);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000353F8 File Offset: 0x000335F8
		public static void ReplaceWith(this INode child, params INode[] nodes)
		{
			INode parent = child.Parent;
			if (parent != null)
			{
				if (nodes.Length != 0)
				{
					INode node = parent.MutationMacro(nodes);
					parent.ReplaceChild(node, child);
					return;
				}
				parent.RemoveChild(child);
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0003542D File Offset: 0x0003362D
		public static void RemoveFromParent(this INode child)
		{
			INode parent = child.Parent;
			if (parent == null)
			{
				return;
			}
			parent.PreRemove(child);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00035441 File Offset: 0x00033641
		public static IEnumerable<TNode> DescendentsAndSelf<TNode>(this INode parent)
		{
			return parent.DescendentsAndSelf().OfType<TNode>();
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0003544E File Offset: 0x0003364E
		public static IEnumerable<INode> DescendentsAndSelf(this INode parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			return parent.GetDescendantsAndSelf();
		}
	}
}
