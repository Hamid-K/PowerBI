using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A36 RID: 2614
	public struct Disjunction : IProgramNodeBuilder, IEquatable<Disjunction>
	{
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06004005 RID: 16389 RVA: 0x000CA25E File Offset: 0x000C845E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x000CA266 File Offset: 0x000C8466
		private Disjunction(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x000CA26F File Offset: 0x000C846F
		public static Disjunction CreateUnsafe(ProgramNode node)
		{
			return new Disjunction(node);
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x000CA278 File Offset: 0x000C8478
		public static Disjunction? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Disjunction)
			{
				return null;
			}
			return new Disjunction?(Disjunction.CreateUnsafe(node));
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x000CA2AD File Offset: 0x000C84AD
		public Disjunction(GrammarBuilders g, conjunct value0, disjunct value1)
		{
			this._node = g.Rule.Disjunction.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x000CA2D3 File Offset: 0x000C84D3
		public static implicit operator disjunct(Disjunction arg)
		{
			return disjunct.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x000CA2E1 File Offset: 0x000C84E1
		public conjunct conjunct
		{
			get
			{
				return conjunct.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x000CA2F5 File Offset: 0x000C84F5
		public disjunct disjunct
		{
			get
			{
				return disjunct.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x000CA309 File Offset: 0x000C8509
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x000CA31C File Offset: 0x000C851C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x000CA346 File Offset: 0x000C8546
		public bool Equals(Disjunction other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D71 RID: 7537
		private ProgramNode _node;
	}
}
