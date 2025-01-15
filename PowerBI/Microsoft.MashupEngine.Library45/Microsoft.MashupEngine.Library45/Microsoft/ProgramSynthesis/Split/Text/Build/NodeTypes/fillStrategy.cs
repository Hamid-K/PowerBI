using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200137C RID: 4988
	public struct fillStrategy : IProgramNodeBuilder, IEquatable<fillStrategy>
	{
		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x06009ACB RID: 39627 RVA: 0x0020B766 File Offset: 0x00209966
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009ACC RID: 39628 RVA: 0x0020B76E File Offset: 0x0020996E
		private fillStrategy(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009ACD RID: 39629 RVA: 0x0020B777 File Offset: 0x00209977
		public static fillStrategy CreateUnsafe(ProgramNode node)
		{
			return new fillStrategy(node);
		}

		// Token: 0x06009ACE RID: 39630 RVA: 0x0020B780 File Offset: 0x00209980
		public static fillStrategy? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fillStrategy)
			{
				return null;
			}
			return new fillStrategy?(fillStrategy.CreateUnsafe(node));
		}

		// Token: 0x06009ACF RID: 39631 RVA: 0x0020B7BA File Offset: 0x002099BA
		public static fillStrategy CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fillStrategy(new Hole(g.Symbol.fillStrategy, holeId));
		}

		// Token: 0x06009AD0 RID: 39632 RVA: 0x0020B7D2 File Offset: 0x002099D2
		public fillStrategy(GrammarBuilders g, FillStrategy value)
		{
			this = new fillStrategy(new LiteralNode(g.Symbol.fillStrategy, value));
		}

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x06009AD1 RID: 39633 RVA: 0x0020B7F0 File Offset: 0x002099F0
		public FillStrategy Value
		{
			get
			{
				return (FillStrategy)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009AD2 RID: 39634 RVA: 0x0020B807 File Offset: 0x00209A07
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009AD3 RID: 39635 RVA: 0x0020B81C File Offset: 0x00209A1C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009AD4 RID: 39636 RVA: 0x0020B846 File Offset: 0x00209A46
		public bool Equals(fillStrategy other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DF3 RID: 15859
		private ProgramNode _node;
	}
}
