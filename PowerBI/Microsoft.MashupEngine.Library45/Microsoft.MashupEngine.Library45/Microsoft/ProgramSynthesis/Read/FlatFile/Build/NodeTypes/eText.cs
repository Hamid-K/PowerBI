using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x02001280 RID: 4736
	public struct eText : IProgramNodeBuilder, IEquatable<eText>
	{
		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x06008F30 RID: 36656 RVA: 0x001E24AA File Offset: 0x001E06AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F31 RID: 36657 RVA: 0x001E24B2 File Offset: 0x001E06B2
		private eText(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F32 RID: 36658 RVA: 0x001E24BB File Offset: 0x001E06BB
		public static eText CreateUnsafe(ProgramNode node)
		{
			return new eText(node);
		}

		// Token: 0x06008F33 RID: 36659 RVA: 0x001E24C4 File Offset: 0x001E06C4
		public static eText? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.eText)
			{
				return null;
			}
			return new eText?(eText.CreateUnsafe(node));
		}

		// Token: 0x06008F34 RID: 36660 RVA: 0x001E24FE File Offset: 0x001E06FE
		public static eText CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new eText(new Hole(g.Symbol.eText, holeId));
		}

		// Token: 0x06008F35 RID: 36661 RVA: 0x001E2516 File Offset: 0x001E0716
		public LetEText Cast_LetEText()
		{
			return LetEText.CreateUnsafe(this.Node);
		}

		// Token: 0x06008F36 RID: 36662 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetEText(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06008F37 RID: 36663 RVA: 0x001E2523 File Offset: 0x001E0723
		public bool Is_LetEText(GrammarBuilders g, out LetEText value)
		{
			value = LetEText.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06008F38 RID: 36664 RVA: 0x001E2537 File Offset: 0x001E0737
		public LetEText? As_LetEText(GrammarBuilders g)
		{
			return new LetEText?(LetEText.CreateUnsafe(this.Node));
		}

		// Token: 0x06008F39 RID: 36665 RVA: 0x001E2549 File Offset: 0x001E0749
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F3A RID: 36666 RVA: 0x001E255C File Offset: 0x001E075C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F3B RID: 36667 RVA: 0x001E2586 File Offset: 0x001E0786
		public bool Equals(eText other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A71 RID: 14961
		private ProgramNode _node;
	}
}
