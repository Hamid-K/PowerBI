using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B6F RID: 2927
	public struct v : IProgramNodeBuilder, IEquatable<v>
	{
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06004A4B RID: 19019 RVA: 0x000E985E File Offset: 0x000E7A5E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x000E9866 File Offset: 0x000E7A66
		private v(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x000E986F File Offset: 0x000E7A6F
		public static v CreateUnsafe(ProgramNode node)
		{
			return new v(node);
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x000E9878 File Offset: 0x000E7A78
		public static v? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.v)
			{
				return null;
			}
			return new v?(v.CreateUnsafe(node));
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x000E98B2 File Offset: 0x000E7AB2
		public static v CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new v(new Hole(g.Symbol.v, holeId));
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x000E98CA File Offset: 0x000E7ACA
		public v(GrammarBuilders g)
		{
			this = new v(new VariableNode(g.Symbol.v));
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x000E98E2 File Offset: 0x000E7AE2
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x000E98EF File Offset: 0x000E7AEF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x000E9904 File Offset: 0x000E7B04
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x000E992E File Offset: 0x000E7B2E
		public bool Equals(v other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400216A RID: 8554
		private ProgramNode _node;
	}
}
