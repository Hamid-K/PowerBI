using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F48 RID: 3912
	public struct columnNames : IProgramNodeBuilder, IEquatable<columnNames>
	{
		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06006CF9 RID: 27897 RVA: 0x00163FF6 File Offset: 0x001621F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006CFA RID: 27898 RVA: 0x00163FFE File Offset: 0x001621FE
		private columnNames(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006CFB RID: 27899 RVA: 0x00164007 File Offset: 0x00162207
		public static columnNames CreateUnsafe(ProgramNode node)
		{
			return new columnNames(node);
		}

		// Token: 0x06006CFC RID: 27900 RVA: 0x00164010 File Offset: 0x00162210
		public static columnNames? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnNames)
			{
				return null;
			}
			return new columnNames?(columnNames.CreateUnsafe(node));
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x0016404A File Offset: 0x0016224A
		public static columnNames CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnNames(new Hole(g.Symbol.columnNames, holeId));
		}

		// Token: 0x06006CFE RID: 27902 RVA: 0x00164062 File Offset: 0x00162262
		public columnNames(GrammarBuilders g, IReadOnlyList<string> value)
		{
			this = new columnNames(new LiteralNode(g.Symbol.columnNames, value));
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06006CFF RID: 27903 RVA: 0x0016407B File Offset: 0x0016227B
		public IReadOnlyList<string> Value
		{
			get
			{
				return (IReadOnlyList<string>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06006D00 RID: 27904 RVA: 0x00164092 File Offset: 0x00162292
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006D01 RID: 27905 RVA: 0x001640A8 File Offset: 0x001622A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006D02 RID: 27906 RVA: 0x001640D2 File Offset: 0x001622D2
		public bool Equals(columnNames other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F33 RID: 12083
		private ProgramNode _node;
	}
}
