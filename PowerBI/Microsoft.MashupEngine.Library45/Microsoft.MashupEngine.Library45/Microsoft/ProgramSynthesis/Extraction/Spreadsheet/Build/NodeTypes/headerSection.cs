using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E6A RID: 3690
	public struct headerSection : IProgramNodeBuilder, IEquatable<headerSection>
	{
		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x0600646F RID: 25711 RVA: 0x00146306 File Offset: 0x00144506
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006470 RID: 25712 RVA: 0x0014630E File Offset: 0x0014450E
		private headerSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x00146317 File Offset: 0x00144517
		public static headerSection CreateUnsafe(ProgramNode node)
		{
			return new headerSection(node);
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x00146320 File Offset: 0x00144520
		public static headerSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.headerSection)
			{
				return null;
			}
			return new headerSection?(headerSection.CreateUnsafe(node));
		}

		// Token: 0x06006473 RID: 25715 RVA: 0x0014635A File Offset: 0x0014455A
		public static headerSection CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new headerSection(new Hole(g.Symbol.headerSection, holeId));
		}

		// Token: 0x06006474 RID: 25716 RVA: 0x00146372 File Offset: 0x00144572
		public FirstSplit Cast_FirstSplit()
		{
			return FirstSplit.CreateUnsafe(this.Node);
		}

		// Token: 0x06006475 RID: 25717 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FirstSplit(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x0014637F File Offset: 0x0014457F
		public bool Is_FirstSplit(GrammarBuilders g, out FirstSplit value)
		{
			value = FirstSplit.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x00146393 File Offset: 0x00144593
		public FirstSplit? As_FirstSplit(GrammarBuilders g)
		{
			return new FirstSplit?(FirstSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x001463A5 File Offset: 0x001445A5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x001463B8 File Offset: 0x001445B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x001463E2 File Offset: 0x001445E2
		public bool Equals(headerSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C14 RID: 11284
		private ProgramNode _node;
	}
}
