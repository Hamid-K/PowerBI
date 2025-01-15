using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E64 RID: 3684
	public struct withoutFormatting : IProgramNodeBuilder, IEquatable<withoutFormatting>
	{
		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06006403 RID: 25603 RVA: 0x00145112 File Offset: 0x00143312
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x0014511A File Offset: 0x0014331A
		private withoutFormatting(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x00145123 File Offset: 0x00143323
		public static withoutFormatting CreateUnsafe(ProgramNode node)
		{
			return new withoutFormatting(node);
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x0014512C File Offset: 0x0014332C
		public static withoutFormatting? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.withoutFormatting)
			{
				return null;
			}
			return new withoutFormatting?(withoutFormatting.CreateUnsafe(node));
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x00145166 File Offset: 0x00143366
		public static withoutFormatting CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new withoutFormatting(new Hole(g.Symbol.withoutFormatting, holeId));
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x0014517E File Offset: 0x0014337E
		public WithoutFormatting Cast_WithoutFormatting()
		{
			return WithoutFormatting.CreateUnsafe(this.Node);
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_WithoutFormatting(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x0014518B File Offset: 0x0014338B
		public bool Is_WithoutFormatting(GrammarBuilders g, out WithoutFormatting value)
		{
			value = WithoutFormatting.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600640B RID: 25611 RVA: 0x0014519F File Offset: 0x0014339F
		public WithoutFormatting? As_WithoutFormatting(GrammarBuilders g)
		{
			return new WithoutFormatting?(WithoutFormatting.CreateUnsafe(this.Node));
		}

		// Token: 0x0600640C RID: 25612 RVA: 0x001451B1 File Offset: 0x001433B1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x001451C4 File Offset: 0x001433C4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x001451EE File Offset: 0x001433EE
		public bool Equals(withoutFormatting other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C0E RID: 11278
		private ProgramNode _node;
	}
}
