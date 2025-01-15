using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001AB9 RID: 6841
	public struct splitCell : IProgramNodeBuilder, IEquatable<splitCell>
	{
		// Token: 0x170025D8 RID: 9688
		// (get) Token: 0x0600E23F RID: 57919 RVA: 0x0030165A File Offset: 0x002FF85A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E240 RID: 57920 RVA: 0x00301662 File Offset: 0x002FF862
		private splitCell(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E241 RID: 57921 RVA: 0x0030166B File Offset: 0x002FF86B
		public static splitCell CreateUnsafe(ProgramNode node)
		{
			return new splitCell(node);
		}

		// Token: 0x0600E242 RID: 57922 RVA: 0x00301674 File Offset: 0x002FF874
		public static splitCell? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitCell)
			{
				return null;
			}
			return new splitCell?(splitCell.CreateUnsafe(node));
		}

		// Token: 0x0600E243 RID: 57923 RVA: 0x003016AE File Offset: 0x002FF8AE
		public static splitCell CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitCell(new Hole(g.Symbol.splitCell, holeId));
		}

		// Token: 0x0600E244 RID: 57924 RVA: 0x003016C6 File Offset: 0x002FF8C6
		public Split Cast_Split()
		{
			return Split.CreateUnsafe(this.Node);
		}

		// Token: 0x0600E245 RID: 57925 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Split(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600E246 RID: 57926 RVA: 0x003016D3 File Offset: 0x002FF8D3
		public bool Is_Split(GrammarBuilders g, out Split value)
		{
			value = Split.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600E247 RID: 57927 RVA: 0x003016E7 File Offset: 0x002FF8E7
		public Split? As_Split(GrammarBuilders g)
		{
			return new Split?(Split.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E248 RID: 57928 RVA: 0x003016F9 File Offset: 0x002FF8F9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E249 RID: 57929 RVA: 0x0030170C File Offset: 0x002FF90C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E24A RID: 57930 RVA: 0x00301736 File Offset: 0x002FF936
		public bool Equals(splitCell other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005578 RID: 21880
		private ProgramNode _node;
	}
}
