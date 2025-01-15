using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E18 RID: 3608
	public struct Trim : IProgramNodeBuilder, IEquatable<Trim>
	{
		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06006030 RID: 24624 RVA: 0x0013D552 File Offset: 0x0013B752
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006031 RID: 24625 RVA: 0x0013D55A File Offset: 0x0013B75A
		private Trim(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006032 RID: 24626 RVA: 0x0013D563 File Offset: 0x0013B763
		public static Trim CreateUnsafe(ProgramNode node)
		{
			return new Trim(node);
		}

		// Token: 0x06006033 RID: 24627 RVA: 0x0013D56C File Offset: 0x0013B76C
		public static Trim? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Trim)
			{
				return null;
			}
			return new Trim?(Trim.CreateUnsafe(node));
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x0013D5A1 File Offset: 0x0013B7A1
		public Trim(GrammarBuilders g, area value0)
		{
			this._node = g.Rule.Trim.BuildASTNode(value0.Node);
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x0013D5C0 File Offset: 0x0013B7C0
		public static implicit operator trim(Trim arg)
		{
			return trim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06006036 RID: 24630 RVA: 0x0013D5CE File Offset: 0x0013B7CE
		public area area
		{
			get
			{
				return area.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x0013D5E2 File Offset: 0x0013B7E2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x0013D5F8 File Offset: 0x0013B7F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x0013D622 File Offset: 0x0013B822
		public bool Equals(Trim other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BC2 RID: 11202
		private ProgramNode _node;
	}
}
