using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E31 RID: 3633
	public struct RemoveEmptyRows : IProgramNodeBuilder, IEquatable<RemoveEmptyRows>
	{
		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06006131 RID: 24881 RVA: 0x0013EC6E File Offset: 0x0013CE6E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006132 RID: 24882 RVA: 0x0013EC76 File Offset: 0x0013CE76
		private RemoveEmptyRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006133 RID: 24883 RVA: 0x0013EC7F File Offset: 0x0013CE7F
		public static RemoveEmptyRows CreateUnsafe(ProgramNode node)
		{
			return new RemoveEmptyRows(node);
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x0013EC88 File Offset: 0x0013CE88
		public static RemoveEmptyRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RemoveEmptyRows)
			{
				return null;
			}
			return new RemoveEmptyRows?(RemoveEmptyRows.CreateUnsafe(node));
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x0013ECBD File Offset: 0x0013CEBD
		public RemoveEmptyRows(GrammarBuilders g, mProgram value0)
		{
			this._node = g.Rule.RemoveEmptyRows.BuildASTNode(value0.Node);
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x0013ECDC File Offset: 0x0013CEDC
		public static implicit operator mProgram(RemoveEmptyRows arg)
		{
			return mProgram.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06006137 RID: 24887 RVA: 0x0013ECEA File Offset: 0x0013CEEA
		public mProgram mProgram
		{
			get
			{
				return mProgram.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006138 RID: 24888 RVA: 0x0013ECFE File Offset: 0x0013CEFE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006139 RID: 24889 RVA: 0x0013ED14 File Offset: 0x0013CF14
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x0013ED3E File Offset: 0x0013CF3E
		public bool Equals(RemoveEmptyRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BDB RID: 11227
		private ProgramNode _node;
	}
}
