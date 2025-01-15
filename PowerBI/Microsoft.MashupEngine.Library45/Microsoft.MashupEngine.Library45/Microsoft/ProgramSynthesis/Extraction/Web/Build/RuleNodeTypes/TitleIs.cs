using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001027 RID: 4135
	public struct TitleIs : IProgramNodeBuilder, IEquatable<TitleIs>
	{
		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06007A1F RID: 31263 RVA: 0x001A161E File Offset: 0x0019F81E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A20 RID: 31264 RVA: 0x001A1626 File Offset: 0x0019F826
		private TitleIs(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A21 RID: 31265 RVA: 0x001A162F File Offset: 0x0019F82F
		public static TitleIs CreateUnsafe(ProgramNode node)
		{
			return new TitleIs(node);
		}

		// Token: 0x06007A22 RID: 31266 RVA: 0x001A1638 File Offset: 0x0019F838
		public static TitleIs? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TitleIs)
			{
				return null;
			}
			return new TitleIs?(TitleIs.CreateUnsafe(node));
		}

		// Token: 0x06007A23 RID: 31267 RVA: 0x001A166D File Offset: 0x0019F86D
		public TitleIs(GrammarBuilders g, name value0, node value1)
		{
			this._node = g.Rule.TitleIs.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007A24 RID: 31268 RVA: 0x001A1693 File Offset: 0x0019F893
		public static implicit operator atomExpr(TitleIs arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06007A25 RID: 31269 RVA: 0x001A16A1 File Offset: 0x0019F8A1
		public name name
		{
			get
			{
				return name.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x06007A26 RID: 31270 RVA: 0x001A16B5 File Offset: 0x0019F8B5
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007A27 RID: 31271 RVA: 0x001A16C9 File Offset: 0x0019F8C9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x001A16DC File Offset: 0x0019F8DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x001A1706 File Offset: 0x0019F906
		public bool Equals(TitleIs other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003340 RID: 13120
		private ProgramNode _node;
	}
}
