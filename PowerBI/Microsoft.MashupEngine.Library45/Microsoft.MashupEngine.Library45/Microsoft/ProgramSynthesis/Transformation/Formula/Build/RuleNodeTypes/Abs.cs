using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001587 RID: 5511
	public struct Abs : IProgramNodeBuilder, IEquatable<Abs>
	{
		// Token: 0x17001F8C RID: 8076
		// (get) Token: 0x0600B475 RID: 46197 RVA: 0x00274D8E File Offset: 0x00272F8E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B476 RID: 46198 RVA: 0x00274D96 File Offset: 0x00272F96
		private Abs(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B477 RID: 46199 RVA: 0x00274D9F File Offset: 0x00272F9F
		public static Abs CreateUnsafe(ProgramNode node)
		{
			return new Abs(node);
		}

		// Token: 0x0600B478 RID: 46200 RVA: 0x00274DA8 File Offset: 0x00272FA8
		public static Abs? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Abs)
			{
				return null;
			}
			return new Abs?(Abs.CreateUnsafe(node));
		}

		// Token: 0x0600B479 RID: 46201 RVA: 0x00274DDD File Offset: 0x00272FDD
		public Abs(GrammarBuilders g, x value0, absPos value1)
		{
			this._node = g.Rule.Abs.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B47A RID: 46202 RVA: 0x00274E03 File Offset: 0x00273003
		public static implicit operator pos(Abs arg)
		{
			return pos.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x0600B47B RID: 46203 RVA: 0x00274E11 File Offset: 0x00273011
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F8E RID: 8078
		// (get) Token: 0x0600B47C RID: 46204 RVA: 0x00274E25 File Offset: 0x00273025
		public absPos absPos
		{
			get
			{
				return absPos.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B47D RID: 46205 RVA: 0x00274E39 File Offset: 0x00273039
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B47E RID: 46206 RVA: 0x00274E4C File Offset: 0x0027304C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B47F RID: 46207 RVA: 0x00274E76 File Offset: 0x00273076
		public bool Equals(Abs other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004635 RID: 17973
		private ProgramNode _node;
	}
}
