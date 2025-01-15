using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001386 RID: 4998
	public struct delimiter : IProgramNodeBuilder, IEquatable<delimiter>
	{
		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x06009B2F RID: 39727 RVA: 0x0020C0C6 File Offset: 0x0020A2C6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009B30 RID: 39728 RVA: 0x0020C0CE File Offset: 0x0020A2CE
		private delimiter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009B31 RID: 39729 RVA: 0x0020C0D7 File Offset: 0x0020A2D7
		public static delimiter CreateUnsafe(ProgramNode node)
		{
			return new delimiter(node);
		}

		// Token: 0x06009B32 RID: 39730 RVA: 0x0020C0E0 File Offset: 0x0020A2E0
		public static delimiter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.delimiter)
			{
				return null;
			}
			return new delimiter?(delimiter.CreateUnsafe(node));
		}

		// Token: 0x06009B33 RID: 39731 RVA: 0x0020C11A File Offset: 0x0020A31A
		public static delimiter CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new delimiter(new Hole(g.Symbol.delimiter, holeId));
		}

		// Token: 0x06009B34 RID: 39732 RVA: 0x0020C132 File Offset: 0x0020A332
		public delimiter(GrammarBuilders g, Record<RegularExpression, RegularExpression, RegularExpression> value)
		{
			this = new delimiter(new LiteralNode(g.Symbol.delimiter, value));
		}

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x06009B35 RID: 39733 RVA: 0x0020C150 File Offset: 0x0020A350
		public Record<RegularExpression, RegularExpression, RegularExpression> Value
		{
			get
			{
				return (Record<RegularExpression, RegularExpression, RegularExpression>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009B36 RID: 39734 RVA: 0x0020C167 File Offset: 0x0020A367
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009B37 RID: 39735 RVA: 0x0020C17C File Offset: 0x0020A37C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009B38 RID: 39736 RVA: 0x0020C1A6 File Offset: 0x0020A3A6
		public bool Equals(delimiter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DFD RID: 15869
		private ProgramNode _node;
	}
}
