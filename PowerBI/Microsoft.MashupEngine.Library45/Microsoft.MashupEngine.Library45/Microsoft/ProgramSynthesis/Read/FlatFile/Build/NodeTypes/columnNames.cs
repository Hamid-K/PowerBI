using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x02001283 RID: 4739
	public struct columnNames : IProgramNodeBuilder, IEquatable<columnNames>
	{
		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x06008F54 RID: 36692 RVA: 0x001E277A File Offset: 0x001E097A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F55 RID: 36693 RVA: 0x001E2782 File Offset: 0x001E0982
		private columnNames(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F56 RID: 36694 RVA: 0x001E278B File Offset: 0x001E098B
		public static columnNames CreateUnsafe(ProgramNode node)
		{
			return new columnNames(node);
		}

		// Token: 0x06008F57 RID: 36695 RVA: 0x001E2794 File Offset: 0x001E0994
		public static columnNames? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnNames)
			{
				return null;
			}
			return new columnNames?(columnNames.CreateUnsafe(node));
		}

		// Token: 0x06008F58 RID: 36696 RVA: 0x001E27CE File Offset: 0x001E09CE
		public static columnNames CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnNames(new Hole(g.Symbol.columnNames, holeId));
		}

		// Token: 0x06008F59 RID: 36697 RVA: 0x001E27E6 File Offset: 0x001E09E6
		public columnNames(GrammarBuilders g, IReadOnlyList<string> value)
		{
			this = new columnNames(new LiteralNode(g.Symbol.columnNames, value));
		}

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x06008F5A RID: 36698 RVA: 0x001E27FF File Offset: 0x001E09FF
		public IReadOnlyList<string> Value
		{
			get
			{
				return (IReadOnlyList<string>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008F5B RID: 36699 RVA: 0x001E2816 File Offset: 0x001E0A16
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F5C RID: 36700 RVA: 0x001E282C File Offset: 0x001E0A2C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F5D RID: 36701 RVA: 0x001E2856 File Offset: 0x001E0A56
		public bool Equals(columnNames other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A74 RID: 14964
		private ProgramNode _node;
	}
}
