using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200137B RID: 4987
	public struct includeDelimiters : IProgramNodeBuilder, IEquatable<includeDelimiters>
	{
		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x06009AC1 RID: 39617 RVA: 0x0020B672 File Offset: 0x00209872
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009AC2 RID: 39618 RVA: 0x0020B67A File Offset: 0x0020987A
		private includeDelimiters(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009AC3 RID: 39619 RVA: 0x0020B683 File Offset: 0x00209883
		public static includeDelimiters CreateUnsafe(ProgramNode node)
		{
			return new includeDelimiters(node);
		}

		// Token: 0x06009AC4 RID: 39620 RVA: 0x0020B68C File Offset: 0x0020988C
		public static includeDelimiters? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.includeDelimiters)
			{
				return null;
			}
			return new includeDelimiters?(includeDelimiters.CreateUnsafe(node));
		}

		// Token: 0x06009AC5 RID: 39621 RVA: 0x0020B6C6 File Offset: 0x002098C6
		public static includeDelimiters CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new includeDelimiters(new Hole(g.Symbol.includeDelimiters, holeId));
		}

		// Token: 0x06009AC6 RID: 39622 RVA: 0x0020B6DE File Offset: 0x002098DE
		public includeDelimiters(GrammarBuilders g, bool value)
		{
			this = new includeDelimiters(new LiteralNode(g.Symbol.includeDelimiters, value));
		}

		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x06009AC7 RID: 39623 RVA: 0x0020B6FC File Offset: 0x002098FC
		public bool Value
		{
			get
			{
				return (bool)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009AC8 RID: 39624 RVA: 0x0020B713 File Offset: 0x00209913
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009AC9 RID: 39625 RVA: 0x0020B728 File Offset: 0x00209928
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009ACA RID: 39626 RVA: 0x0020B752 File Offset: 0x00209952
		public bool Equals(includeDelimiters other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DF2 RID: 15858
		private ProgramNode _node;
	}
}
