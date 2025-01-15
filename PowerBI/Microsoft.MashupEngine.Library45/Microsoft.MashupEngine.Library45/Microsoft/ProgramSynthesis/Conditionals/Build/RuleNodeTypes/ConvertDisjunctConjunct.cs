using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A49 RID: 2633
	public struct ConvertDisjunctConjunct : IProgramNodeBuilder, IEquatable<ConvertDisjunctConjunct>
	{
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060040CD RID: 16589 RVA: 0x000CB442 File Offset: 0x000C9642
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x000CB44A File Offset: 0x000C964A
		private ConvertDisjunctConjunct(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x000CB453 File Offset: 0x000C9653
		public static ConvertDisjunctConjunct CreateUnsafe(ProgramNode node)
		{
			return new ConvertDisjunctConjunct(node);
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x000CB45C File Offset: 0x000C965C
		public static ConvertDisjunctConjunct? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConvertDisjunctConjunct)
			{
				return null;
			}
			return new ConvertDisjunctConjunct?(ConvertDisjunctConjunct.CreateUnsafe(node));
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x000CB491 File Offset: 0x000C9691
		public ConvertDisjunctConjunct(GrammarBuilders g, conjunct value0)
		{
			this._node = g.Rule.ConvertDisjunctConjunct.BuildASTNode(value0.Node);
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x000CB4B0 File Offset: 0x000C96B0
		public static implicit operator disjunct(ConvertDisjunctConjunct arg)
		{
			return disjunct.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060040D3 RID: 16595 RVA: 0x000CB4BE File Offset: 0x000C96BE
		public conjunct conjunct
		{
			get
			{
				return conjunct.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x000CB4D2 File Offset: 0x000C96D2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x000CB4E8 File Offset: 0x000C96E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x000CB512 File Offset: 0x000C9712
		public bool Equals(ConvertDisjunctConjunct other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D84 RID: 7556
		private ProgramNode _node;
	}
}
