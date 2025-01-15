using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E10 RID: 3600
	public struct wholeSheet_wholeSheetFull : IProgramNodeBuilder, IEquatable<wholeSheet_wholeSheetFull>
	{
		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06005FE0 RID: 24544 RVA: 0x0013CE32 File Offset: 0x0013B032
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x0013CE3A File Offset: 0x0013B03A
		private wholeSheet_wholeSheetFull(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005FE2 RID: 24546 RVA: 0x0013CE43 File Offset: 0x0013B043
		public static wholeSheet_wholeSheetFull CreateUnsafe(ProgramNode node)
		{
			return new wholeSheet_wholeSheetFull(node);
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x0013CE4C File Offset: 0x0013B04C
		public static wholeSheet_wholeSheetFull? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.wholeSheet_wholeSheetFull)
			{
				return null;
			}
			return new wholeSheet_wholeSheetFull?(wholeSheet_wholeSheetFull.CreateUnsafe(node));
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x0013CE81 File Offset: 0x0013B081
		public wholeSheet_wholeSheetFull(GrammarBuilders g, wholeSheetFull value0)
		{
			this._node = g.UnnamedConversion.wholeSheet_wholeSheetFull.BuildASTNode(value0.Node);
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x0013CEA0 File Offset: 0x0013B0A0
		public static implicit operator wholeSheet(wholeSheet_wholeSheetFull arg)
		{
			return wholeSheet.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06005FE6 RID: 24550 RVA: 0x0013CEAE File Offset: 0x0013B0AE
		public wholeSheetFull wholeSheetFull
		{
			get
			{
				return wholeSheetFull.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x0013CEC2 File Offset: 0x0013B0C2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x0013CED8 File Offset: 0x0013B0D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x0013CF02 File Offset: 0x0013B102
		public bool Equals(wholeSheet_wholeSheetFull other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BBA RID: 11194
		private ProgramNode _node;
	}
}
