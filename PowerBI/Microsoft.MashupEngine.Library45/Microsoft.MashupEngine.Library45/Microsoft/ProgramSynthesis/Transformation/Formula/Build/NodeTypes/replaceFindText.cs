using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015DF RID: 5599
	public struct replaceFindText : IProgramNodeBuilder, IEquatable<replaceFindText>
	{
		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x0600B9DA RID: 47578 RVA: 0x00281BD6 File Offset: 0x0027FDD6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B9DB RID: 47579 RVA: 0x00281BDE File Offset: 0x0027FDDE
		private replaceFindText(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B9DC RID: 47580 RVA: 0x00281BE7 File Offset: 0x0027FDE7
		public static replaceFindText CreateUnsafe(ProgramNode node)
		{
			return new replaceFindText(node);
		}

		// Token: 0x0600B9DD RID: 47581 RVA: 0x00281BF0 File Offset: 0x0027FDF0
		public static replaceFindText? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.replaceFindText)
			{
				return null;
			}
			return new replaceFindText?(replaceFindText.CreateUnsafe(node));
		}

		// Token: 0x0600B9DE RID: 47582 RVA: 0x00281C2A File Offset: 0x0027FE2A
		public static replaceFindText CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new replaceFindText(new Hole(g.Symbol.replaceFindText, holeId));
		}

		// Token: 0x0600B9DF RID: 47583 RVA: 0x00281C42 File Offset: 0x0027FE42
		public replaceFindText(GrammarBuilders g, string value)
		{
			this = new replaceFindText(new LiteralNode(g.Symbol.replaceFindText, value));
		}

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x0600B9E0 RID: 47584 RVA: 0x00281C5B File Offset: 0x0027FE5B
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B9E1 RID: 47585 RVA: 0x00281C72 File Offset: 0x0027FE72
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B9E2 RID: 47586 RVA: 0x00281C88 File Offset: 0x0027FE88
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B9E3 RID: 47587 RVA: 0x00281CB2 File Offset: 0x0027FEB2
		public bool Equals(replaceFindText other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400468D RID: 18061
		private ProgramNode _node;
	}
}
