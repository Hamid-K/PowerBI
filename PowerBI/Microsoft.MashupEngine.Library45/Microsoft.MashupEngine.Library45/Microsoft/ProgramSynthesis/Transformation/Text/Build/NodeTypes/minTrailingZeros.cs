using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C65 RID: 7269
	public struct minTrailingZeros : IProgramNodeBuilder, IEquatable<minTrailingZeros>
	{
		// Token: 0x17002902 RID: 10498
		// (get) Token: 0x0600F62A RID: 63018 RVA: 0x00348D56 File Offset: 0x00346F56
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F62B RID: 63019 RVA: 0x00348D5E File Offset: 0x00346F5E
		private minTrailingZeros(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F62C RID: 63020 RVA: 0x00348D67 File Offset: 0x00346F67
		public static minTrailingZeros CreateUnsafe(ProgramNode node)
		{
			return new minTrailingZeros(node);
		}

		// Token: 0x0600F62D RID: 63021 RVA: 0x00348D70 File Offset: 0x00346F70
		public static minTrailingZeros? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.minTrailingZeros)
			{
				return null;
			}
			return new minTrailingZeros?(minTrailingZeros.CreateUnsafe(node));
		}

		// Token: 0x0600F62E RID: 63022 RVA: 0x00348DAA File Offset: 0x00346FAA
		public static minTrailingZeros CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new minTrailingZeros(new Hole(g.Symbol.minTrailingZeros, holeId));
		}

		// Token: 0x0600F62F RID: 63023 RVA: 0x00348DC2 File Offset: 0x00346FC2
		public minTrailingZeros(GrammarBuilders g, uint? value)
		{
			this = new minTrailingZeros(new LiteralNode(g.Symbol.minTrailingZeros, value));
		}

		// Token: 0x17002903 RID: 10499
		// (get) Token: 0x0600F630 RID: 63024 RVA: 0x00348DE0 File Offset: 0x00346FE0
		public uint? Value
		{
			get
			{
				return (uint?)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F631 RID: 63025 RVA: 0x00348DF7 File Offset: 0x00346FF7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F632 RID: 63026 RVA: 0x00348E0C File Offset: 0x0034700C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F633 RID: 63027 RVA: 0x00348E36 File Offset: 0x00347036
		public bool Equals(minTrailingZeros other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B54 RID: 23380
		private ProgramNode _node;
	}
}
