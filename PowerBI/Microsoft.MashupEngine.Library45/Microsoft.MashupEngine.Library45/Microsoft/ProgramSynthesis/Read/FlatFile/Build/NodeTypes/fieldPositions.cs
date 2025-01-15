using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x02001287 RID: 4743
	public struct fieldPositions : IProgramNodeBuilder, IEquatable<fieldPositions>
	{
		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x06008F7C RID: 36732 RVA: 0x001E2B42 File Offset: 0x001E0D42
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F7D RID: 36733 RVA: 0x001E2B4A File Offset: 0x001E0D4A
		private fieldPositions(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F7E RID: 36734 RVA: 0x001E2B53 File Offset: 0x001E0D53
		public static fieldPositions CreateUnsafe(ProgramNode node)
		{
			return new fieldPositions(node);
		}

		// Token: 0x06008F7F RID: 36735 RVA: 0x001E2B5C File Offset: 0x001E0D5C
		public static fieldPositions? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fieldPositions)
			{
				return null;
			}
			return new fieldPositions?(fieldPositions.CreateUnsafe(node));
		}

		// Token: 0x06008F80 RID: 36736 RVA: 0x001E2B96 File Offset: 0x001E0D96
		public static fieldPositions CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fieldPositions(new Hole(g.Symbol.fieldPositions, holeId));
		}

		// Token: 0x06008F81 RID: 36737 RVA: 0x001E2BAE File Offset: 0x001E0DAE
		public fieldPositions(GrammarBuilders g, IReadOnlyList<Record<int, int?>> value)
		{
			this = new fieldPositions(new LiteralNode(g.Symbol.fieldPositions, value));
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06008F82 RID: 36738 RVA: 0x001E2BC7 File Offset: 0x001E0DC7
		public IReadOnlyList<Record<int, int?>> Value
		{
			get
			{
				return (IReadOnlyList<Record<int, int?>>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008F83 RID: 36739 RVA: 0x001E2BDE File Offset: 0x001E0DDE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F84 RID: 36740 RVA: 0x001E2BF4 File Offset: 0x001E0DF4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F85 RID: 36741 RVA: 0x001E2C1E File Offset: 0x001E0E1E
		public bool Equals(fieldPositions other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A78 RID: 14968
		private ProgramNode _node;
	}
}
