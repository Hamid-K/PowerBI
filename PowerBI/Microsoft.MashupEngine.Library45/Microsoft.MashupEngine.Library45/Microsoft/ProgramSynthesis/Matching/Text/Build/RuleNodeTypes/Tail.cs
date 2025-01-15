using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011E5 RID: 4581
	public struct Tail : IProgramNodeBuilder, IEquatable<Tail>
	{
		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x0600899F RID: 35231 RVA: 0x001CF6A6 File Offset: 0x001CD8A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060089A0 RID: 35232 RVA: 0x001CF6AE File Offset: 0x001CD8AE
		private Tail(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060089A1 RID: 35233 RVA: 0x001CF6B7 File Offset: 0x001CD8B7
		public static Tail CreateUnsafe(ProgramNode node)
		{
			return new Tail(node);
		}

		// Token: 0x060089A2 RID: 35234 RVA: 0x001CF6C0 File Offset: 0x001CD8C0
		public static Tail? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Tail)
			{
				return null;
			}
			return new Tail?(Tail.CreateUnsafe(node));
		}

		// Token: 0x060089A3 RID: 35235 RVA: 0x001CF6F5 File Offset: 0x001CD8F5
		public Tail(GrammarBuilders g, sRegions value0)
		{
			this._node = g.Rule.Tail.BuildASTNode(value0.Node);
		}

		// Token: 0x060089A4 RID: 35236 RVA: 0x001CF714 File Offset: 0x001CD914
		public static implicit operator _LetB1(Tail arg)
		{
			return _LetB1.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x060089A5 RID: 35237 RVA: 0x001CF722 File Offset: 0x001CD922
		public sRegions sRegions
		{
			get
			{
				return sRegions.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060089A6 RID: 35238 RVA: 0x001CF736 File Offset: 0x001CD936
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060089A7 RID: 35239 RVA: 0x001CF74C File Offset: 0x001CD94C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060089A8 RID: 35240 RVA: 0x001CF776 File Offset: 0x001CD976
		public bool Equals(Tail other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003899 RID: 14489
		private ProgramNode _node;
	}
}
