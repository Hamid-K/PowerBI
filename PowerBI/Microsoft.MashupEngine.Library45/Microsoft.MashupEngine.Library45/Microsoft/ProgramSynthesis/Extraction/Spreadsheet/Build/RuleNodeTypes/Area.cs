using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E29 RID: 3625
	public struct Area : IProgramNodeBuilder, IEquatable<Area>
	{
		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x060060DB RID: 24795 RVA: 0x0013E48E File Offset: 0x0013C68E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060060DC RID: 24796 RVA: 0x0013E496 File Offset: 0x0013C696
		private Area(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x0013E49F File Offset: 0x0013C69F
		public static Area CreateUnsafe(ProgramNode node)
		{
			return new Area(node);
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x0013E4A8 File Offset: 0x0013C6A8
		public static Area? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Area)
			{
				return null;
			}
			return new Area?(Area.CreateUnsafe(node));
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x0013E4E0 File Offset: 0x0013C6E0
		public Area(GrammarBuilders g, sheet value0, index value1, index value2, index value3, index value4)
		{
			this._node = g.Rule.Area.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node, value4.Node });
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x0013E53B File Offset: 0x0013C73B
		public static implicit operator uncleanedSheetSection(Area arg)
		{
			return uncleanedSheetSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x060060E1 RID: 24801 RVA: 0x0013E549 File Offset: 0x0013C749
		public sheet sheet
		{
			get
			{
				return sheet.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x060060E2 RID: 24802 RVA: 0x0013E55D File Offset: 0x0013C75D
		public index index1
		{
			get
			{
				return index.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x060060E3 RID: 24803 RVA: 0x0013E571 File Offset: 0x0013C771
		public index index2
		{
			get
			{
				return index.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x060060E4 RID: 24804 RVA: 0x0013E585 File Offset: 0x0013C785
		public index index3
		{
			get
			{
				return index.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x060060E5 RID: 24805 RVA: 0x0013E599 File Offset: 0x0013C799
		public index index4
		{
			get
			{
				return index.CreateUnsafe(this.Node.Children[4]);
			}
		}

		// Token: 0x060060E6 RID: 24806 RVA: 0x0013E5AD File Offset: 0x0013C7AD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x0013E5C0 File Offset: 0x0013C7C0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x0013E5EA File Offset: 0x0013C7EA
		public bool Equals(Area other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BD3 RID: 11219
		private ProgramNode _node;
	}
}
