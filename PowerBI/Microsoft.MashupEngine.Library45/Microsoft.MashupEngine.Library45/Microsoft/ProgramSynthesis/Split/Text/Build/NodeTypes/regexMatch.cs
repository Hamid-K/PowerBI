using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200136A RID: 4970
	public struct regexMatch : IProgramNodeBuilder, IEquatable<regexMatch>
	{
		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x060099F3 RID: 39411 RVA: 0x0020A1DA File Offset: 0x002083DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060099F4 RID: 39412 RVA: 0x0020A1E2 File Offset: 0x002083E2
		private regexMatch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060099F5 RID: 39413 RVA: 0x0020A1EB File Offset: 0x002083EB
		public static regexMatch CreateUnsafe(ProgramNode node)
		{
			return new regexMatch(node);
		}

		// Token: 0x060099F6 RID: 39414 RVA: 0x0020A1F4 File Offset: 0x002083F4
		public static regexMatch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.regexMatch)
			{
				return null;
			}
			return new regexMatch?(regexMatch.CreateUnsafe(node));
		}

		// Token: 0x060099F7 RID: 39415 RVA: 0x0020A22E File Offset: 0x0020842E
		public static regexMatch CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new regexMatch(new Hole(g.Symbol.regexMatch, holeId));
		}

		// Token: 0x060099F8 RID: 39416 RVA: 0x0020A246 File Offset: 0x00208446
		public RegexMatch Cast_RegexMatch()
		{
			return RegexMatch.CreateUnsafe(this.Node);
		}

		// Token: 0x060099F9 RID: 39417 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_RegexMatch(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x060099FA RID: 39418 RVA: 0x0020A253 File Offset: 0x00208453
		public bool Is_RegexMatch(GrammarBuilders g, out RegexMatch value)
		{
			value = RegexMatch.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060099FB RID: 39419 RVA: 0x0020A267 File Offset: 0x00208467
		public RegexMatch? As_RegexMatch(GrammarBuilders g)
		{
			return new RegexMatch?(RegexMatch.CreateUnsafe(this.Node));
		}

		// Token: 0x060099FC RID: 39420 RVA: 0x0020A279 File Offset: 0x00208479
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060099FD RID: 39421 RVA: 0x0020A28C File Offset: 0x0020848C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060099FE RID: 39422 RVA: 0x0020A2B6 File Offset: 0x002084B6
		public bool Equals(regexMatch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DE1 RID: 15841
		private ProgramNode _node;
	}
}
