using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001066 RID: 4198
	public struct filterSelection : IProgramNodeBuilder, IEquatable<filterSelection>
	{
		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06007CFB RID: 31995 RVA: 0x001A5F16 File Offset: 0x001A4116
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007CFC RID: 31996 RVA: 0x001A5F1E File Offset: 0x001A411E
		private filterSelection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007CFD RID: 31997 RVA: 0x001A5F27 File Offset: 0x001A4127
		public static filterSelection CreateUnsafe(ProgramNode node)
		{
			return new filterSelection(node);
		}

		// Token: 0x06007CFE RID: 31998 RVA: 0x001A5F30 File Offset: 0x001A4130
		public static filterSelection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.filterSelection)
			{
				return null;
			}
			return new filterSelection?(filterSelection.CreateUnsafe(node));
		}

		// Token: 0x06007CFF RID: 31999 RVA: 0x001A5F6A File Offset: 0x001A416A
		public static filterSelection CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new filterSelection(new Hole(g.Symbol.filterSelection, holeId));
		}

		// Token: 0x06007D00 RID: 32000 RVA: 0x001A5F82 File Offset: 0x001A4182
		public LeafFilter1 Cast_LeafFilter1()
		{
			return LeafFilter1.CreateUnsafe(this.Node);
		}

		// Token: 0x06007D01 RID: 32001 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LeafFilter1(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007D02 RID: 32002 RVA: 0x001A5F8F File Offset: 0x001A418F
		public bool Is_LeafFilter1(GrammarBuilders g, out LeafFilter1 value)
		{
			value = LeafFilter1.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007D03 RID: 32003 RVA: 0x001A5FA3 File Offset: 0x001A41A3
		public LeafFilter1? As_LeafFilter1(GrammarBuilders g)
		{
			return new LeafFilter1?(LeafFilter1.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D04 RID: 32004 RVA: 0x001A5FB5 File Offset: 0x001A41B5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007D05 RID: 32005 RVA: 0x001A5FC8 File Offset: 0x001A41C8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007D06 RID: 32006 RVA: 0x001A5FF2 File Offset: 0x001A41F2
		public bool Equals(filterSelection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400337F RID: 13183
		private ProgramNode _node;
	}
}
