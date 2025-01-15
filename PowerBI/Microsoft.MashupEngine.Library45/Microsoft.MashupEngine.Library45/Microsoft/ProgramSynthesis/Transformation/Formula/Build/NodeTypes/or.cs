using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015A9 RID: 5545
	public struct or : IProgramNodeBuilder, IEquatable<or>
	{
		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x0600B6B2 RID: 46770 RVA: 0x0027A9B6 File Offset: 0x00278BB6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B6B3 RID: 46771 RVA: 0x0027A9BE File Offset: 0x00278BBE
		private or(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B6B4 RID: 46772 RVA: 0x0027A9C7 File Offset: 0x00278BC7
		public static or CreateUnsafe(ProgramNode node)
		{
			return new or(node);
		}

		// Token: 0x0600B6B5 RID: 46773 RVA: 0x0027A9D0 File Offset: 0x00278BD0
		public static or? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.or)
			{
				return null;
			}
			return new or?(or.CreateUnsafe(node));
		}

		// Token: 0x0600B6B6 RID: 46774 RVA: 0x0027AA0A File Offset: 0x00278C0A
		public static or CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new or(new Hole(g.Symbol.or, holeId));
		}

		// Token: 0x0600B6B7 RID: 46775 RVA: 0x0027AA22 File Offset: 0x00278C22
		public Or Cast_Or()
		{
			return Or.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B6B8 RID: 46776 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Or(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B6B9 RID: 46777 RVA: 0x0027AA2F File Offset: 0x00278C2F
		public bool Is_Or(GrammarBuilders g, out Or value)
		{
			value = Or.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B6BA RID: 46778 RVA: 0x0027AA43 File Offset: 0x00278C43
		public Or? As_Or(GrammarBuilders g)
		{
			return new Or?(Or.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6BB RID: 46779 RVA: 0x0027AA55 File Offset: 0x00278C55
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B6BC RID: 46780 RVA: 0x0027AA68 File Offset: 0x00278C68
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B6BD RID: 46781 RVA: 0x0027AA92 File Offset: 0x00278C92
		public bool Equals(or other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004657 RID: 18007
		private ProgramNode _node;
	}
}
