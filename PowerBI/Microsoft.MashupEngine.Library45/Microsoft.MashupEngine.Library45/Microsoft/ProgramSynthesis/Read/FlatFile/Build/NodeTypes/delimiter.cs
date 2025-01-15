using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x02001286 RID: 4742
	public struct delimiter : IProgramNodeBuilder, IEquatable<delimiter>
	{
		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x06008F72 RID: 36722 RVA: 0x001E2A52 File Offset: 0x001E0C52
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F73 RID: 36723 RVA: 0x001E2A5A File Offset: 0x001E0C5A
		private delimiter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F74 RID: 36724 RVA: 0x001E2A63 File Offset: 0x001E0C63
		public static delimiter CreateUnsafe(ProgramNode node)
		{
			return new delimiter(node);
		}

		// Token: 0x06008F75 RID: 36725 RVA: 0x001E2A6C File Offset: 0x001E0C6C
		public static delimiter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.delimiter)
			{
				return null;
			}
			return new delimiter?(delimiter.CreateUnsafe(node));
		}

		// Token: 0x06008F76 RID: 36726 RVA: 0x001E2AA6 File Offset: 0x001E0CA6
		public static delimiter CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new delimiter(new Hole(g.Symbol.delimiter, holeId));
		}

		// Token: 0x06008F77 RID: 36727 RVA: 0x001E2ABE File Offset: 0x001E0CBE
		public delimiter(GrammarBuilders g, string value)
		{
			this = new delimiter(new LiteralNode(g.Symbol.delimiter, value));
		}

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x06008F78 RID: 36728 RVA: 0x001E2AD7 File Offset: 0x001E0CD7
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008F79 RID: 36729 RVA: 0x001E2AEE File Offset: 0x001E0CEE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F7A RID: 36730 RVA: 0x001E2B04 File Offset: 0x001E0D04
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F7B RID: 36731 RVA: 0x001E2B2E File Offset: 0x001E0D2E
		public bool Equals(delimiter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A77 RID: 14967
		private ProgramNode _node;
	}
}
