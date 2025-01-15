using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AB3 RID: 6835
	public struct TTableProgram : IProgramNodeBuilder, IEquatable<TTableProgram>
	{
		// Token: 0x170025D0 RID: 9680
		// (get) Token: 0x0600E1D5 RID: 57813 RVA: 0x0030061A File Offset: 0x002FE81A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E1D6 RID: 57814 RVA: 0x00300622 File Offset: 0x002FE822
		private TTableProgram(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E1D7 RID: 57815 RVA: 0x0030062B File Offset: 0x002FE82B
		public static TTableProgram CreateUnsafe(ProgramNode node)
		{
			return new TTableProgram(node);
		}

		// Token: 0x0600E1D8 RID: 57816 RVA: 0x00300634 File Offset: 0x002FE834
		public static TTableProgram? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TTableProgram)
			{
				return null;
			}
			return new TTableProgram?(TTableProgram.CreateUnsafe(node));
		}

		// Token: 0x0600E1D9 RID: 57817 RVA: 0x00300669 File Offset: 0x002FE869
		public TTableProgram(GrammarBuilders g, table value0)
		{
			this._node = g.Rule.TTableProgram.BuildASTNode(value0.Node);
		}

		// Token: 0x0600E1DA RID: 57818 RVA: 0x00300688 File Offset: 0x002FE888
		public static implicit operator @out(TTableProgram arg)
		{
			return @out.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025D1 RID: 9681
		// (get) Token: 0x0600E1DB RID: 57819 RVA: 0x00300696 File Offset: 0x002FE896
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600E1DC RID: 57820 RVA: 0x003006AA File Offset: 0x002FE8AA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E1DD RID: 57821 RVA: 0x003006C0 File Offset: 0x002FE8C0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E1DE RID: 57822 RVA: 0x003006EA File Offset: 0x002FE8EA
		public bool Equals(TTableProgram other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005572 RID: 21874
		private ProgramNode _node;
	}
}
