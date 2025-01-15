using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E1F RID: 3615
	public struct FreezePaneTight : IProgramNodeBuilder, IEquatable<FreezePaneTight>
	{
		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06006077 RID: 24695 RVA: 0x0013DBA6 File Offset: 0x0013BDA6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x0013DBAE File Offset: 0x0013BDAE
		private FreezePaneTight(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x0013DBB7 File Offset: 0x0013BDB7
		public static FreezePaneTight CreateUnsafe(ProgramNode node)
		{
			return new FreezePaneTight(node);
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x0013DBC0 File Offset: 0x0013BDC0
		public static FreezePaneTight? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FreezePaneTight)
			{
				return null;
			}
			return new FreezePaneTight?(FreezePaneTight.CreateUnsafe(node));
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x0013DBF5 File Offset: 0x0013BDF5
		public FreezePaneTight(GrammarBuilders g, sheet value0)
		{
			this._node = g.Rule.FreezePaneTight.BuildASTNode(value0.Node);
		}

		// Token: 0x0600607C RID: 24700 RVA: 0x0013DC14 File Offset: 0x0013BE14
		public static implicit operator trimTop(FreezePaneTight arg)
		{
			return trimTop.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x0600607D RID: 24701 RVA: 0x0013DC22 File Offset: 0x0013BE22
		public sheet sheet
		{
			get
			{
				return sheet.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x0013DC36 File Offset: 0x0013BE36
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x0013DC4C File Offset: 0x0013BE4C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x0013DC76 File Offset: 0x0013BE76
		public bool Equals(FreezePaneTight other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BC9 RID: 11209
		private ProgramNode _node;
	}
}
