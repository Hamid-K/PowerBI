using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200097A RID: 2426
	public struct delimiter : IProgramNodeBuilder, IEquatable<delimiter>
	{
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060039EA RID: 14826 RVA: 0x000B2C12 File Offset: 0x000B0E12
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000B2C1A File Offset: 0x000B0E1A
		private delimiter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x000B2C23 File Offset: 0x000B0E23
		public static delimiter CreateUnsafe(ProgramNode node)
		{
			return new delimiter(node);
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000B2C2C File Offset: 0x000B0E2C
		public static delimiter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.delimiter)
			{
				return null;
			}
			return new delimiter?(delimiter.CreateUnsafe(node));
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000B2C66 File Offset: 0x000B0E66
		public static delimiter CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new delimiter(new Hole(g.Symbol.delimiter, holeId));
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000B2C7E File Offset: 0x000B0E7E
		public delimiter(GrammarBuilders g, Optional<string> value)
		{
			this = new delimiter(new LiteralNode(g.Symbol.delimiter, value));
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000B2C9C File Offset: 0x000B0E9C
		public Optional<string> Value
		{
			get
			{
				return (Optional<string>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x000B2CB3 File Offset: 0x000B0EB3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000B2CC8 File Offset: 0x000B0EC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000B2CF2 File Offset: 0x000B0EF2
		public bool Equals(delimiter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A9A RID: 6810
		private ProgramNode _node;
	}
}
