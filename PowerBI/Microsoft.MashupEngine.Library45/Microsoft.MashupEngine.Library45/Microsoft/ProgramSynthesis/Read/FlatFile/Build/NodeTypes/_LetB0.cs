using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x02001281 RID: 4737
	public struct _LetB0 : IProgramNodeBuilder, IEquatable<_LetB0>
	{
		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x06008F3C RID: 36668 RVA: 0x001E259A File Offset: 0x001E079A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F3D RID: 36669 RVA: 0x001E25A2 File Offset: 0x001E07A2
		private _LetB0(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F3E RID: 36670 RVA: 0x001E25AB File Offset: 0x001E07AB
		public static _LetB0 CreateUnsafe(ProgramNode node)
		{
			return new _LetB0(node);
		}

		// Token: 0x06008F3F RID: 36671 RVA: 0x001E25B4 File Offset: 0x001E07B4
		public static _LetB0? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB0)
			{
				return null;
			}
			return new _LetB0?(_LetB0.CreateUnsafe(node));
		}

		// Token: 0x06008F40 RID: 36672 RVA: 0x001E25EE File Offset: 0x001E07EE
		public static _LetB0 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB0(new Hole(g.Symbol._LetB0, holeId));
		}

		// Token: 0x06008F41 RID: 36673 RVA: 0x001E2606 File Offset: 0x001E0806
		public CreateStringRegion Cast_CreateStringRegion()
		{
			return CreateStringRegion.CreateUnsafe(this.Node);
		}

		// Token: 0x06008F42 RID: 36674 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_CreateStringRegion(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06008F43 RID: 36675 RVA: 0x001E2613 File Offset: 0x001E0813
		public bool Is_CreateStringRegion(GrammarBuilders g, out CreateStringRegion value)
		{
			value = CreateStringRegion.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06008F44 RID: 36676 RVA: 0x001E2627 File Offset: 0x001E0827
		public CreateStringRegion? As_CreateStringRegion(GrammarBuilders g)
		{
			return new CreateStringRegion?(CreateStringRegion.CreateUnsafe(this.Node));
		}

		// Token: 0x06008F45 RID: 36677 RVA: 0x001E2639 File Offset: 0x001E0839
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F46 RID: 36678 RVA: 0x001E264C File Offset: 0x001E084C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F47 RID: 36679 RVA: 0x001E2676 File Offset: 0x001E0876
		public bool Equals(_LetB0 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A72 RID: 14962
		private ProgramNode _node;
	}
}
