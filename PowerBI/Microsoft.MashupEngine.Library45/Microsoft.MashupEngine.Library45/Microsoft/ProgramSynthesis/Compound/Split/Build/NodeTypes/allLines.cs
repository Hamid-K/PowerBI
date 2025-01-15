using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000984 RID: 2436
	public struct allLines : IProgramNodeBuilder, IEquatable<allLines>
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x000B354A File Offset: 0x000B174A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000B3552 File Offset: 0x000B1752
		private allLines(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000B355B File Offset: 0x000B175B
		public static allLines CreateUnsafe(ProgramNode node)
		{
			return new allLines(node);
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000B3564 File Offset: 0x000B1764
		public static allLines? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.allLines)
			{
				return null;
			}
			return new allLines?(allLines.CreateUnsafe(node));
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000B359E File Offset: 0x000B179E
		public static allLines CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new allLines(new Hole(g.Symbol.allLines, holeId));
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000B35B6 File Offset: 0x000B17B6
		public allLines(GrammarBuilders g)
		{
			this = new allLines(new VariableNode(g.Symbol.allLines));
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003A54 RID: 14932 RVA: 0x000B35CE File Offset: 0x000B17CE
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000B35DB File Offset: 0x000B17DB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000B35F0 File Offset: 0x000B17F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x000B361A File Offset: 0x000B181A
		public bool Equals(allLines other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001AA4 RID: 6820
		private ProgramNode _node;
	}
}
