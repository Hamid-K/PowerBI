using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F39 RID: 3897
	public struct extractTup : IProgramNodeBuilder, IEquatable<extractTup>
	{
		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06006C1F RID: 27679 RVA: 0x00162232 File Offset: 0x00160432
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006C20 RID: 27680 RVA: 0x0016223A File Offset: 0x0016043A
		private extractTup(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006C21 RID: 27681 RVA: 0x00162243 File Offset: 0x00160443
		public static extractTup CreateUnsafe(ProgramNode node)
		{
			return new extractTup(node);
		}

		// Token: 0x06006C22 RID: 27682 RVA: 0x0016224C File Offset: 0x0016044C
		public static extractTup? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.extractTup)
			{
				return null;
			}
			return new extractTup?(extractTup.CreateUnsafe(node));
		}

		// Token: 0x06006C23 RID: 27683 RVA: 0x00162286 File Offset: 0x00160486
		public static extractTup CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new extractTup(new Hole(g.Symbol.extractTup, holeId));
		}

		// Token: 0x06006C24 RID: 27684 RVA: 0x0016229E File Offset: 0x0016049E
		public LetExtractTup Cast_LetExtractTup()
		{
			return LetExtractTup.CreateUnsafe(this.Node);
		}

		// Token: 0x06006C25 RID: 27685 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetExtractTup(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006C26 RID: 27686 RVA: 0x001622AB File Offset: 0x001604AB
		public bool Is_LetExtractTup(GrammarBuilders g, out LetExtractTup value)
		{
			value = LetExtractTup.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006C27 RID: 27687 RVA: 0x001622BF File Offset: 0x001604BF
		public LetExtractTup? As_LetExtractTup(GrammarBuilders g)
		{
			return new LetExtractTup?(LetExtractTup.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C28 RID: 27688 RVA: 0x001622D1 File Offset: 0x001604D1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006C29 RID: 27689 RVA: 0x001622E4 File Offset: 0x001604E4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006C2A RID: 27690 RVA: 0x0016230E File Offset: 0x0016050E
		public bool Equals(extractTup other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F24 RID: 12068
		private ProgramNode _node;
	}
}
