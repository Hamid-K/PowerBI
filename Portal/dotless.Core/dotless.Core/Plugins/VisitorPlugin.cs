using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Plugins
{
	// Token: 0x02000022 RID: 34
	public abstract class VisitorPlugin : IVisitorPlugin, IPlugin, IVisitor
	{
		// Token: 0x060000CC RID: 204 RVA: 0x000044AE File Offset: 0x000026AE
		public Root Apply(Root tree)
		{
			this.Visit(tree);
			return tree;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CD RID: 205
		public abstract VisitorPluginType AppliesTo { get; }

		// Token: 0x060000CE RID: 206 RVA: 0x000044BC File Offset: 0x000026BC
		public Node Visit(Node node)
		{
			bool flag;
			node = this.Execute(node, out flag);
			if (flag && node != null)
			{
				node.Accept(this);
			}
			return node;
		}

		// Token: 0x060000CF RID: 207
		public abstract Node Execute(Node node, out bool visitDeeper);

		// Token: 0x060000D0 RID: 208 RVA: 0x000044E2 File Offset: 0x000026E2
		public virtual void OnPreVisiting(Env env)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000044E4 File Offset: 0x000026E4
		public virtual void OnPostVisiting(Env env)
		{
		}
	}
}
