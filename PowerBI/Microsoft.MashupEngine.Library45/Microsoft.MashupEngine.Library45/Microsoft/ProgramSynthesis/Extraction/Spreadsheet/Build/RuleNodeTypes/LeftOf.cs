using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E43 RID: 3651
	public struct LeftOf : IProgramNodeBuilder, IEquatable<LeftOf>
	{
		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x060061E6 RID: 25062 RVA: 0x0013FC8E File Offset: 0x0013DE8E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060061E7 RID: 25063 RVA: 0x0013FC96 File Offset: 0x0013DE96
		private LeftOf(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x0013FC9F File Offset: 0x0013DE9F
		public static LeftOf CreateUnsafe(ProgramNode node)
		{
			return new LeftOf(node);
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x0013FCA8 File Offset: 0x0013DEA8
		public static LeftOf? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeftOf)
			{
				return null;
			}
			return new LeftOf?(LeftOf.CreateUnsafe(node));
		}

		// Token: 0x060061EA RID: 25066 RVA: 0x0013FCDD File Offset: 0x0013DEDD
		public LeftOf(GrammarBuilders g, titleOf value0)
		{
			this._node = g.Rule.LeftOf.BuildASTNode(value0.Node);
		}

		// Token: 0x060061EB RID: 25067 RVA: 0x0013FCFC File Offset: 0x0013DEFC
		public static implicit operator aboveOrLeftmost(LeftOf arg)
		{
			return aboveOrLeftmost.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x060061EC RID: 25068 RVA: 0x0013FD0A File Offset: 0x0013DF0A
		public titleOf titleOf
		{
			get
			{
				return titleOf.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060061ED RID: 25069 RVA: 0x0013FD1E File Offset: 0x0013DF1E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x0013FD34 File Offset: 0x0013DF34
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x0013FD5E File Offset: 0x0013DF5E
		public bool Equals(LeftOf other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BED RID: 11245
		private ProgramNode _node;
	}
}
