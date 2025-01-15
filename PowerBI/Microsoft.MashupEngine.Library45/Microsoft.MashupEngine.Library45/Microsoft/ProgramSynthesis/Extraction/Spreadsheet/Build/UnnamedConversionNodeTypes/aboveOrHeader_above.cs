using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E16 RID: 3606
	public struct aboveOrHeader_above : IProgramNodeBuilder, IEquatable<aboveOrHeader_above>
	{
		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x0600601C RID: 24604 RVA: 0x0013D38A File Offset: 0x0013B58A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x0013D392 File Offset: 0x0013B592
		private aboveOrHeader_above(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x0013D39B File Offset: 0x0013B59B
		public static aboveOrHeader_above CreateUnsafe(ProgramNode node)
		{
			return new aboveOrHeader_above(node);
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x0013D3A4 File Offset: 0x0013B5A4
		public static aboveOrHeader_above? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.aboveOrHeader_above)
			{
				return null;
			}
			return new aboveOrHeader_above?(aboveOrHeader_above.CreateUnsafe(node));
		}

		// Token: 0x06006020 RID: 24608 RVA: 0x0013D3D9 File Offset: 0x0013B5D9
		public aboveOrHeader_above(GrammarBuilders g, above value0)
		{
			this._node = g.UnnamedConversion.aboveOrHeader_above.BuildASTNode(value0.Node);
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x0013D3F8 File Offset: 0x0013B5F8
		public static implicit operator aboveOrHeader(aboveOrHeader_above arg)
		{
			return aboveOrHeader.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06006022 RID: 24610 RVA: 0x0013D406 File Offset: 0x0013B606
		public above above
		{
			get
			{
				return above.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x0013D41A File Offset: 0x0013B61A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x0013D430 File Offset: 0x0013B630
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x0013D45A File Offset: 0x0013B65A
		public bool Equals(aboveOrHeader_above other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BC0 RID: 11200
		private ProgramNode _node;
	}
}
