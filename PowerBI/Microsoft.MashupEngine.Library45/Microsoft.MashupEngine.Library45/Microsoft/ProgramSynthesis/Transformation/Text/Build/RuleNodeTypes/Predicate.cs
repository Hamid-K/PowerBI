using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C25 RID: 7205
	public struct Predicate : IProgramNodeBuilder, IEquatable<Predicate>
	{
		// Token: 0x1700289D RID: 10397
		// (get) Token: 0x0600F2A4 RID: 62116 RVA: 0x0034156E File Offset: 0x0033F76E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F2A5 RID: 62117 RVA: 0x00341576 File Offset: 0x0033F776
		private Predicate(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F2A6 RID: 62118 RVA: 0x0034157F File Offset: 0x0033F77F
		public static Predicate CreateUnsafe(ProgramNode node)
		{
			return new Predicate(node);
		}

		// Token: 0x0600F2A7 RID: 62119 RVA: 0x00341588 File Offset: 0x0033F788
		public static Predicate? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Predicate)
			{
				return null;
			}
			return new Predicate?(Predicate.CreateUnsafe(node));
		}

		// Token: 0x0600F2A8 RID: 62120 RVA: 0x003415BD File Offset: 0x0033F7BD
		public Predicate(GrammarBuilders g, conjunct value0)
		{
			this._node = g.Rule.Predicate.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F2A9 RID: 62121 RVA: 0x003415DC File Offset: 0x0033F7DC
		public static implicit operator Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred(Predicate arg)
		{
			return Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes.pred.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700289E RID: 10398
		// (get) Token: 0x0600F2AA RID: 62122 RVA: 0x003415EA File Offset: 0x0033F7EA
		public conjunct conjunct
		{
			get
			{
				return conjunct.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F2AB RID: 62123 RVA: 0x003415FE File Offset: 0x0033F7FE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F2AC RID: 62124 RVA: 0x00341614 File Offset: 0x0033F814
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F2AD RID: 62125 RVA: 0x0034163E File Offset: 0x0033F83E
		public bool Equals(Predicate other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B14 RID: 23316
		private ProgramNode _node;
	}
}
