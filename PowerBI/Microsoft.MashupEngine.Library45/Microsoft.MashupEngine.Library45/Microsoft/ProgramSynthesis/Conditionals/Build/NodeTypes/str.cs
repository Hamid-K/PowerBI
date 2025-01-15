using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes
{
	// Token: 0x02000A53 RID: 2643
	public struct str : IProgramNodeBuilder, IEquatable<str>
	{
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x000CD472 File Offset: 0x000CB672
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x000CD47A File Offset: 0x000CB67A
		private str(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x000CD483 File Offset: 0x000CB683
		public static str CreateUnsafe(ProgramNode node)
		{
			return new str(node);
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x000CD48C File Offset: 0x000CB68C
		public static str? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.str)
			{
				return null;
			}
			return new str?(str.CreateUnsafe(node));
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x000CD4C6 File Offset: 0x000CB6C6
		public static str CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new str(new Hole(g.Symbol.str, holeId));
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x000CD4DE File Offset: 0x000CB6DE
		public str(GrammarBuilders g, string value)
		{
			this = new str(new LiteralNode(g.Symbol.str, value));
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x000CD4F7 File Offset: 0x000CB6F7
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x000CD50E File Offset: 0x000CB70E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x000CD524 File Offset: 0x000CB724
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x000CD54E File Offset: 0x000CB74E
		public bool Equals(str other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D8E RID: 7566
		private ProgramNode _node;
	}
}
