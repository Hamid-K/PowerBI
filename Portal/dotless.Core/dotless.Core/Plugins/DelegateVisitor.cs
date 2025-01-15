using System;
using System.Collections.Generic;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Plugins
{
	// Token: 0x02000016 RID: 22
	public class DelegateVisitor : IVisitor
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000039AF File Offset: 0x00001BAF
		public DelegateVisitor(Func<Node, Node> visitor)
		{
			this.visitor = visitor;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000039C0 File Offset: 0x00001BC0
		public Node Visit(Node node)
		{
			IList<Node> list = node as IList<Node>;
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					list[i] = this.Visit(list[i]);
				}
			}
			return this.visitor(node);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003A08 File Offset: 0x00001C08
		public static IVisitor For<TNode>(Func<TNode, Node> projection) where TNode : Node
		{
			return new DelegateVisitor(delegate(Node node)
			{
				TNode tnode = node as TNode;
				if (tnode == null)
				{
					return node;
				}
				return projection(tnode);
			});
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003A26 File Offset: 0x00001C26
		public static IVisitor For<TNode>(Action<TNode> action) where TNode : Node
		{
			return new DelegateVisitor(delegate(Node node)
			{
				TNode tnode = node as TNode;
				if (tnode != null)
				{
					action(tnode);
				}
				return node;
			});
		}

		// Token: 0x0400001A RID: 26
		private readonly Func<Node, Node> visitor;
	}
}
