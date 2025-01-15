using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001380 RID: 4992
	public struct fregex : IProgramNodeBuilder, IEquatable<fregex>
	{
		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x06009AF3 RID: 39667 RVA: 0x0020BB2A File Offset: 0x00209D2A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009AF4 RID: 39668 RVA: 0x0020BB32 File Offset: 0x00209D32
		private fregex(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009AF5 RID: 39669 RVA: 0x0020BB3B File Offset: 0x00209D3B
		public static fregex CreateUnsafe(ProgramNode node)
		{
			return new fregex(node);
		}

		// Token: 0x06009AF6 RID: 39670 RVA: 0x0020BB44 File Offset: 0x00209D44
		public static fregex? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fregex)
			{
				return null;
			}
			return new fregex?(fregex.CreateUnsafe(node));
		}

		// Token: 0x06009AF7 RID: 39671 RVA: 0x0020BB7E File Offset: 0x00209D7E
		public static fregex CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fregex(new Hole(g.Symbol.fregex, holeId));
		}

		// Token: 0x06009AF8 RID: 39672 RVA: 0x0020BB96 File Offset: 0x00209D96
		public fregex(GrammarBuilders g, RegularExpression value)
		{
			this = new fregex(new LiteralNode(g.Symbol.fregex, value));
		}

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x06009AF9 RID: 39673 RVA: 0x0020BBAF File Offset: 0x00209DAF
		public RegularExpression Value
		{
			get
			{
				return (RegularExpression)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009AFA RID: 39674 RVA: 0x0020BBC6 File Offset: 0x00209DC6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009AFB RID: 39675 RVA: 0x0020BBDC File Offset: 0x00209DDC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009AFC RID: 39676 RVA: 0x0020BC06 File Offset: 0x00209E06
		public bool Equals(fregex other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DF7 RID: 15863
		private ProgramNode _node;
	}
}
