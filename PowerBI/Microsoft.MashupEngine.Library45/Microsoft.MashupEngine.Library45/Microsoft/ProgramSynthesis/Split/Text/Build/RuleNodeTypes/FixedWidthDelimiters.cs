using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001354 RID: 4948
	public struct FixedWidthDelimiters : IProgramNodeBuilder, IEquatable<FixedWidthDelimiters>
	{
		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x060098AB RID: 39083 RVA: 0x0020719E File Offset: 0x0020539E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060098AC RID: 39084 RVA: 0x002071A6 File Offset: 0x002053A6
		private FixedWidthDelimiters(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060098AD RID: 39085 RVA: 0x002071AF File Offset: 0x002053AF
		public static FixedWidthDelimiters CreateUnsafe(ProgramNode node)
		{
			return new FixedWidthDelimiters(node);
		}

		// Token: 0x060098AE RID: 39086 RVA: 0x002071B8 File Offset: 0x002053B8
		public static FixedWidthDelimiters? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FixedWidthDelimiters)
			{
				return null;
			}
			return new FixedWidthDelimiters?(FixedWidthDelimiters.CreateUnsafe(node));
		}

		// Token: 0x060098AF RID: 39087 RVA: 0x002071ED File Offset: 0x002053ED
		public FixedWidthDelimiters(GrammarBuilders g, v value0, delimiterPositions value1)
		{
			this._node = g.Rule.FixedWidthDelimiters.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060098B0 RID: 39088 RVA: 0x00207213 File Offset: 0x00205413
		public static implicit operator fixedWidthMatches(FixedWidthDelimiters arg)
		{
			return fixedWidthMatches.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x060098B1 RID: 39089 RVA: 0x00207221 File Offset: 0x00205421
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x060098B2 RID: 39090 RVA: 0x00207235 File Offset: 0x00205435
		public delimiterPositions delimiterPositions
		{
			get
			{
				return delimiterPositions.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060098B3 RID: 39091 RVA: 0x00207249 File Offset: 0x00205449
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060098B4 RID: 39092 RVA: 0x0020725C File Offset: 0x0020545C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060098B5 RID: 39093 RVA: 0x00207286 File Offset: 0x00205486
		public bool Equals(FixedWidthDelimiters other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DCB RID: 15819
		private ProgramNode _node;
	}
}
