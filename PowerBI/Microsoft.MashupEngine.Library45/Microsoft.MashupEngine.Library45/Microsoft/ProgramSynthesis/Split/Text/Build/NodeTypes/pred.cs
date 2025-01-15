using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001365 RID: 4965
	public struct pred : IProgramNodeBuilder, IEquatable<pred>
	{
		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x06009993 RID: 39315 RVA: 0x002090D6 File Offset: 0x002072D6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009994 RID: 39316 RVA: 0x002090DE File Offset: 0x002072DE
		private pred(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009995 RID: 39317 RVA: 0x002090E7 File Offset: 0x002072E7
		public static pred CreateUnsafe(ProgramNode node)
		{
			return new pred(node);
		}

		// Token: 0x06009996 RID: 39318 RVA: 0x002090F0 File Offset: 0x002072F0
		public static pred? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pred)
			{
				return null;
			}
			return new pred?(pred.CreateUnsafe(node));
		}

		// Token: 0x06009997 RID: 39319 RVA: 0x0020912A File Offset: 0x0020732A
		public static pred CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pred(new Hole(g.Symbol.pred, holeId));
		}

		// Token: 0x06009998 RID: 39320 RVA: 0x00209142 File Offset: 0x00207342
		public SpecialCharPattern Cast_SpecialCharPattern()
		{
			return SpecialCharPattern.CreateUnsafe(this.Node);
		}

		// Token: 0x06009999 RID: 39321 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SpecialCharPattern(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600999A RID: 39322 RVA: 0x0020914F File Offset: 0x0020734F
		public bool Is_SpecialCharPattern(GrammarBuilders g, out SpecialCharPattern value)
		{
			value = SpecialCharPattern.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600999B RID: 39323 RVA: 0x00209163 File Offset: 0x00207363
		public SpecialCharPattern? As_SpecialCharPattern(GrammarBuilders g)
		{
			return new SpecialCharPattern?(SpecialCharPattern.CreateUnsafe(this.Node));
		}

		// Token: 0x0600999C RID: 39324 RVA: 0x00209175 File Offset: 0x00207375
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600999D RID: 39325 RVA: 0x00209188 File Offset: 0x00207388
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600999E RID: 39326 RVA: 0x002091B2 File Offset: 0x002073B2
		public bool Equals(pred other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DDC RID: 15836
		private ProgramNode _node;
	}
}
