using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E60 RID: 7776
	public struct Conj : IProgramNodeBuilder, IEquatable<Conj>
	{
		// Token: 0x17002B81 RID: 11137
		// (get) Token: 0x0601060E RID: 67086 RVA: 0x0038922E File Offset: 0x0038742E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601060F RID: 67087 RVA: 0x00389236 File Offset: 0x00387436
		private Conj(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010610 RID: 67088 RVA: 0x0038923F File Offset: 0x0038743F
		public static Conj CreateUnsafe(ProgramNode node)
		{
			return new Conj(node);
		}

		// Token: 0x06010611 RID: 67089 RVA: 0x00389248 File Offset: 0x00387448
		public static Conj? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Conj)
			{
				return null;
			}
			return new Conj?(Conj.CreateUnsafe(node));
		}

		// Token: 0x06010612 RID: 67090 RVA: 0x0038927D File Offset: 0x0038747D
		public Conj(GrammarBuilders g, pred value0, match value1)
		{
			this._node = g.Rule.Conj.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06010613 RID: 67091 RVA: 0x003892A3 File Offset: 0x003874A3
		public static implicit operator match(Conj arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B82 RID: 11138
		// (get) Token: 0x06010614 RID: 67092 RVA: 0x003892B1 File Offset: 0x003874B1
		public pred pred
		{
			get
			{
				return pred.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002B83 RID: 11139
		// (get) Token: 0x06010615 RID: 67093 RVA: 0x003892C5 File Offset: 0x003874C5
		public match match
		{
			get
			{
				return match.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06010616 RID: 67094 RVA: 0x003892D9 File Offset: 0x003874D9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010617 RID: 67095 RVA: 0x003892EC File Offset: 0x003874EC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010618 RID: 67096 RVA: 0x00389316 File Offset: 0x00387516
		public bool Equals(Conj other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400629F RID: 25247
		private ProgramNode _node;
	}
}
