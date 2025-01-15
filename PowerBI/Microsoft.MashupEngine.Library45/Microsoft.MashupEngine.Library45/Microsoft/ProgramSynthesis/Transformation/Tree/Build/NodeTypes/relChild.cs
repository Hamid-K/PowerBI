using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E87 RID: 7815
	public struct relChild : IProgramNodeBuilder, IEquatable<relChild>
	{
		// Token: 0x17002BE0 RID: 11232
		// (get) Token: 0x06010814 RID: 67604 RVA: 0x0038D02E File Offset: 0x0038B22E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010815 RID: 67605 RVA: 0x0038D036 File Offset: 0x0038B236
		private relChild(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010816 RID: 67606 RVA: 0x0038D03F File Offset: 0x0038B23F
		public static relChild CreateUnsafe(ProgramNode node)
		{
			return new relChild(node);
		}

		// Token: 0x06010817 RID: 67607 RVA: 0x0038D048 File Offset: 0x0038B248
		public static relChild? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.relChild)
			{
				return null;
			}
			return new relChild?(relChild.CreateUnsafe(node));
		}

		// Token: 0x06010818 RID: 67608 RVA: 0x0038D082 File Offset: 0x0038B282
		public static relChild CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new relChild(new Hole(g.Symbol.relChild, holeId));
		}

		// Token: 0x06010819 RID: 67609 RVA: 0x0038D09A File Offset: 0x0038B29A
		public RelChild Cast_RelChild()
		{
			return RelChild.CreateUnsafe(this.Node);
		}

		// Token: 0x0601081A RID: 67610 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_RelChild(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0601081B RID: 67611 RVA: 0x0038D0A7 File Offset: 0x0038B2A7
		public bool Is_RelChild(GrammarBuilders g, out RelChild value)
		{
			value = RelChild.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0601081C RID: 67612 RVA: 0x0038D0BB File Offset: 0x0038B2BB
		public RelChild? As_RelChild(GrammarBuilders g)
		{
			return new RelChild?(RelChild.CreateUnsafe(this.Node));
		}

		// Token: 0x0601081D RID: 67613 RVA: 0x0038D0CD File Offset: 0x0038B2CD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601081E RID: 67614 RVA: 0x0038D0E0 File Offset: 0x0038B2E0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601081F RID: 67615 RVA: 0x0038D10A File Offset: 0x0038B30A
		public bool Equals(relChild other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062C6 RID: 25286
		private ProgramNode _node;
	}
}
