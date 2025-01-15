using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A48 RID: 2632
	public struct Start : IProgramNodeBuilder, IEquatable<Start>
	{
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x000CB35E File Offset: 0x000C955E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x000CB366 File Offset: 0x000C9566
		private Start(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x000CB36F File Offset: 0x000C956F
		public static Start CreateUnsafe(ProgramNode node)
		{
			return new Start(node);
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x000CB378 File Offset: 0x000C9578
		public static Start? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Start)
			{
				return null;
			}
			return new Start?(Start.CreateUnsafe(node));
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x000CB3AD File Offset: 0x000C95AD
		public Start(GrammarBuilders g, disjunct value0)
		{
			this._node = g.Rule.Start.BuildASTNode(value0.Node);
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x000CB3CC File Offset: 0x000C95CC
		public static implicit operator output(Start arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060040C9 RID: 16585 RVA: 0x000CB3DA File Offset: 0x000C95DA
		public disjunct disjunct
		{
			get
			{
				return disjunct.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x000CB3EE File Offset: 0x000C95EE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x000CB404 File Offset: 0x000C9604
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x000CB42E File Offset: 0x000C962E
		public bool Equals(Start other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D83 RID: 7555
		private ProgramNode _node;
	}
}
