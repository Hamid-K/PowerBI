using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A4A RID: 2634
	public struct Conjunct : IProgramNodeBuilder, IEquatable<Conjunct>
	{
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x000CB526 File Offset: 0x000C9726
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x000CB52E File Offset: 0x000C972E
		private Conjunct(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x000CB537 File Offset: 0x000C9737
		public static Conjunct CreateUnsafe(ProgramNode node)
		{
			return new Conjunct(node);
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x000CB540 File Offset: 0x000C9740
		public static Conjunct? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Conjunct)
			{
				return null;
			}
			return new Conjunct?(Conjunct.CreateUnsafe(node));
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x000CB575 File Offset: 0x000C9775
		public Conjunct(GrammarBuilders g, baseConjunct value0)
		{
			this._node = g.Rule.Conjunct.BuildASTNode(value0.Node);
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x000CB594 File Offset: 0x000C9794
		public static implicit operator conjunct(Conjunct arg)
		{
			return conjunct.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x000CB5A2 File Offset: 0x000C97A2
		public baseConjunct baseConjunct
		{
			get
			{
				return baseConjunct.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x000CB5B6 File Offset: 0x000C97B6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x000CB5CC File Offset: 0x000C97CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x000CB5F6 File Offset: 0x000C97F6
		public bool Equals(Conjunct other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D85 RID: 7557
		private ProgramNode _node;
	}
}
