using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C08 RID: 3080
	public struct k : IProgramNodeBuilder, IEquatable<k>
	{
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06004F93 RID: 20371 RVA: 0x000FAEC2 File Offset: 0x000F90C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x000FAECA File Offset: 0x000F90CA
		private k(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x000FAED3 File Offset: 0x000F90D3
		public static k CreateUnsafe(ProgramNode node)
		{
			return new k(node);
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x000FAEDC File Offset: 0x000F90DC
		public static k? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.k)
			{
				return null;
			}
			return new k?(k.CreateUnsafe(node));
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x000FAF16 File Offset: 0x000F9116
		public static k CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new k(new Hole(g.Symbol.k, holeId));
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x000FAF2E File Offset: 0x000F912E
		public k(GrammarBuilders g, int value)
		{
			this = new k(new LiteralNode(g.Symbol.k, value));
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06004F99 RID: 20377 RVA: 0x000FAF4C File Offset: 0x000F914C
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x000FAF63 File Offset: 0x000F9163
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x000FAF78 File Offset: 0x000F9178
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x000FAFA2 File Offset: 0x000F91A2
		public bool Equals(k other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002330 RID: 9008
		private ProgramNode _node;
	}
}
