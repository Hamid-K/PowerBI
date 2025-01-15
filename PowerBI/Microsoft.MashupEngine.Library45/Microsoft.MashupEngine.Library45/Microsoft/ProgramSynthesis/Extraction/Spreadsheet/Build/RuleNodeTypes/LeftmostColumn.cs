using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E42 RID: 3650
	public struct LeftmostColumn : IProgramNodeBuilder, IEquatable<LeftmostColumn>
	{
		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x060061DC RID: 25052 RVA: 0x0013FBAA File Offset: 0x0013DDAA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x0013FBB2 File Offset: 0x0013DDB2
		private LeftmostColumn(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060061DE RID: 25054 RVA: 0x0013FBBB File Offset: 0x0013DDBB
		public static LeftmostColumn CreateUnsafe(ProgramNode node)
		{
			return new LeftmostColumn(node);
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x0013FBC4 File Offset: 0x0013DDC4
		public static LeftmostColumn? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeftmostColumn)
			{
				return null;
			}
			return new LeftmostColumn?(LeftmostColumn.CreateUnsafe(node));
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x0013FBF9 File Offset: 0x0013DDF9
		public LeftmostColumn(GrammarBuilders g, aboveOrOutput value0)
		{
			this._node = g.Rule.LeftmostColumn.BuildASTNode(value0.Node);
		}

		// Token: 0x060061E1 RID: 25057 RVA: 0x0013FC18 File Offset: 0x0013DE18
		public static implicit operator aboveOrLeftmost(LeftmostColumn arg)
		{
			return aboveOrLeftmost.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x060061E2 RID: 25058 RVA: 0x0013FC26 File Offset: 0x0013DE26
		public aboveOrOutput aboveOrOutput
		{
			get
			{
				return aboveOrOutput.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060061E3 RID: 25059 RVA: 0x0013FC3A File Offset: 0x0013DE3A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x0013FC50 File Offset: 0x0013DE50
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060061E5 RID: 25061 RVA: 0x0013FC7A File Offset: 0x0013DE7A
		public bool Equals(LeftmostColumn other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BEC RID: 11244
		private ProgramNode _node;
	}
}
