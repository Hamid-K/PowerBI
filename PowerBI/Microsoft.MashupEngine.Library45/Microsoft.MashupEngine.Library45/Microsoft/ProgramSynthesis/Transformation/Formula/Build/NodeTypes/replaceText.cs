using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015E0 RID: 5600
	public struct replaceText : IProgramNodeBuilder, IEquatable<replaceText>
	{
		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x0600B9E4 RID: 47588 RVA: 0x00281CC6 File Offset: 0x0027FEC6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B9E5 RID: 47589 RVA: 0x00281CCE File Offset: 0x0027FECE
		private replaceText(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B9E6 RID: 47590 RVA: 0x00281CD7 File Offset: 0x0027FED7
		public static replaceText CreateUnsafe(ProgramNode node)
		{
			return new replaceText(node);
		}

		// Token: 0x0600B9E7 RID: 47591 RVA: 0x00281CE0 File Offset: 0x0027FEE0
		public static replaceText? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.replaceText)
			{
				return null;
			}
			return new replaceText?(replaceText.CreateUnsafe(node));
		}

		// Token: 0x0600B9E8 RID: 47592 RVA: 0x00281D1A File Offset: 0x0027FF1A
		public static replaceText CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new replaceText(new Hole(g.Symbol.replaceText, holeId));
		}

		// Token: 0x0600B9E9 RID: 47593 RVA: 0x00281D32 File Offset: 0x0027FF32
		public replaceText(GrammarBuilders g, string value)
		{
			this = new replaceText(new LiteralNode(g.Symbol.replaceText, value));
		}

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x0600B9EA RID: 47594 RVA: 0x00281D4B File Offset: 0x0027FF4B
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B9EB RID: 47595 RVA: 0x00281D62 File Offset: 0x0027FF62
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B9EC RID: 47596 RVA: 0x00281D78 File Offset: 0x0027FF78
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B9ED RID: 47597 RVA: 0x00281DA2 File Offset: 0x0027FFA2
		public bool Equals(replaceText other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400468E RID: 18062
		private ProgramNode _node;
	}
}
