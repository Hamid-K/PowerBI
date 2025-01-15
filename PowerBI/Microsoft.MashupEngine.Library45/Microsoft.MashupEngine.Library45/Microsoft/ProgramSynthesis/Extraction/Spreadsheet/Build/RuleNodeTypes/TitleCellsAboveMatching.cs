using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E48 RID: 3656
	public struct TitleCellsAboveMatching : IProgramNodeBuilder, IEquatable<TitleCellsAboveMatching>
	{
		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600621B RID: 25115 RVA: 0x0014014E File Offset: 0x0013E34E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x00140156 File Offset: 0x0013E356
		private TitleCellsAboveMatching(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x0014015F File Offset: 0x0013E35F
		public static TitleCellsAboveMatching CreateUnsafe(ProgramNode node)
		{
			return new TitleCellsAboveMatching(node);
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x00140168 File Offset: 0x0013E368
		public static TitleCellsAboveMatching? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TitleCellsAboveMatching)
			{
				return null;
			}
			return new TitleCellsAboveMatching?(TitleCellsAboveMatching.CreateUnsafe(node));
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x0014019D File Offset: 0x0013E39D
		public TitleCellsAboveMatching(GrammarBuilders g, titleOf value0, titleAboveMode value1, styleFilter value2)
		{
			this._node = g.Rule.TitleCellsAboveMatching.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x001401CA File Offset: 0x0013E3CA
		public static implicit operator above(TitleCellsAboveMatching arg)
		{
			return above.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06006221 RID: 25121 RVA: 0x001401D8 File Offset: 0x0013E3D8
		public titleOf titleOf
		{
			get
			{
				return titleOf.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06006222 RID: 25122 RVA: 0x001401EC File Offset: 0x0013E3EC
		public titleAboveMode titleAboveMode
		{
			get
			{
				return titleAboveMode.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06006223 RID: 25123 RVA: 0x00140200 File Offset: 0x0013E400
		public styleFilter styleFilter
		{
			get
			{
				return styleFilter.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x00140214 File Offset: 0x0013E414
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x00140228 File Offset: 0x0013E428
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006226 RID: 25126 RVA: 0x00140252 File Offset: 0x0013E452
		public bool Equals(TitleCellsAboveMatching other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BF2 RID: 11250
		private ProgramNode _node;
	}
}
