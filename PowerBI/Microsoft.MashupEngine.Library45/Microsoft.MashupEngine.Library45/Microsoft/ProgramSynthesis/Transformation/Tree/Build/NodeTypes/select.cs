using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E7F RID: 7807
	public struct select : IProgramNodeBuilder, IEquatable<select>
	{
		// Token: 0x17002BD8 RID: 11224
		// (get) Token: 0x06010798 RID: 67480 RVA: 0x0038BFE2 File Offset: 0x0038A1E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010799 RID: 67481 RVA: 0x0038BFEA File Offset: 0x0038A1EA
		private select(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601079A RID: 67482 RVA: 0x0038BFF3 File Offset: 0x0038A1F3
		public static select CreateUnsafe(ProgramNode node)
		{
			return new select(node);
		}

		// Token: 0x0601079B RID: 67483 RVA: 0x0038BFFC File Offset: 0x0038A1FC
		public static select? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.select)
			{
				return null;
			}
			return new select?(select.CreateUnsafe(node));
		}

		// Token: 0x0601079C RID: 67484 RVA: 0x0038C036 File Offset: 0x0038A236
		public static select CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new select(new Hole(g.Symbol.select, holeId));
		}

		// Token: 0x0601079D RID: 67485 RVA: 0x0038C04E File Offset: 0x0038A24E
		public Select Cast_Select()
		{
			return Select.CreateUnsafe(this.Node);
		}

		// Token: 0x0601079E RID: 67486 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Select(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0601079F RID: 67487 RVA: 0x0038C05B File Offset: 0x0038A25B
		public bool Is_Select(GrammarBuilders g, out Select value)
		{
			value = Select.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060107A0 RID: 67488 RVA: 0x0038C06F File Offset: 0x0038A26F
		public Select? As_Select(GrammarBuilders g)
		{
			return new Select?(Select.CreateUnsafe(this.Node));
		}

		// Token: 0x060107A1 RID: 67489 RVA: 0x0038C081 File Offset: 0x0038A281
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060107A2 RID: 67490 RVA: 0x0038C094 File Offset: 0x0038A294
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060107A3 RID: 67491 RVA: 0x0038C0BE File Offset: 0x0038A2BE
		public bool Equals(select other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062BE RID: 25278
		private ProgramNode _node;
	}
}
