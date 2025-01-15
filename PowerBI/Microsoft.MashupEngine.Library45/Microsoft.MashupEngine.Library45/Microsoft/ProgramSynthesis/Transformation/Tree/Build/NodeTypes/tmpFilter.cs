using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E80 RID: 7808
	public struct tmpFilter : IProgramNodeBuilder, IEquatable<tmpFilter>
	{
		// Token: 0x17002BD9 RID: 11225
		// (get) Token: 0x060107A4 RID: 67492 RVA: 0x0038C0D2 File Offset: 0x0038A2D2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060107A5 RID: 67493 RVA: 0x0038C0DA File Offset: 0x0038A2DA
		private tmpFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060107A6 RID: 67494 RVA: 0x0038C0E3 File Offset: 0x0038A2E3
		public static tmpFilter CreateUnsafe(ProgramNode node)
		{
			return new tmpFilter(node);
		}

		// Token: 0x060107A7 RID: 67495 RVA: 0x0038C0EC File Offset: 0x0038A2EC
		public static tmpFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.tmpFilter)
			{
				return null;
			}
			return new tmpFilter?(tmpFilter.CreateUnsafe(node));
		}

		// Token: 0x060107A8 RID: 67496 RVA: 0x0038C126 File Offset: 0x0038A326
		public static tmpFilter CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new tmpFilter(new Hole(g.Symbol.tmpFilter, holeId));
		}

		// Token: 0x060107A9 RID: 67497 RVA: 0x0038C13E File Offset: 0x0038A33E
		public TmpFilter Cast_TmpFilter()
		{
			return TmpFilter.CreateUnsafe(this.Node);
		}

		// Token: 0x060107AA RID: 67498 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_TmpFilter(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x060107AB RID: 67499 RVA: 0x0038C14B File Offset: 0x0038A34B
		public bool Is_TmpFilter(GrammarBuilders g, out TmpFilter value)
		{
			value = TmpFilter.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060107AC RID: 67500 RVA: 0x0038C15F File Offset: 0x0038A35F
		public TmpFilter? As_TmpFilter(GrammarBuilders g)
		{
			return new TmpFilter?(TmpFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x060107AD RID: 67501 RVA: 0x0038C171 File Offset: 0x0038A371
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060107AE RID: 67502 RVA: 0x0038C184 File Offset: 0x0038A384
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060107AF RID: 67503 RVA: 0x0038C1AE File Offset: 0x0038A3AE
		public bool Equals(tmpFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062BF RID: 25279
		private ProgramNode _node;
	}
}
