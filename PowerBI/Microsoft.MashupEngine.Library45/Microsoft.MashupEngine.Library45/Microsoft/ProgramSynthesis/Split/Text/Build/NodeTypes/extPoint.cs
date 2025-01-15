using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001376 RID: 4982
	public struct extPoint : IProgramNodeBuilder, IEquatable<extPoint>
	{
		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x06009A8F RID: 39567 RVA: 0x0020B1B2 File Offset: 0x002093B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A90 RID: 39568 RVA: 0x0020B1BA File Offset: 0x002093BA
		private extPoint(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A91 RID: 39569 RVA: 0x0020B1C3 File Offset: 0x002093C3
		public static extPoint CreateUnsafe(ProgramNode node)
		{
			return new extPoint(node);
		}

		// Token: 0x06009A92 RID: 39570 RVA: 0x0020B1CC File Offset: 0x002093CC
		public static extPoint? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.extPoint)
			{
				return null;
			}
			return new extPoint?(extPoint.CreateUnsafe(node));
		}

		// Token: 0x06009A93 RID: 39571 RVA: 0x0020B206 File Offset: 0x00209406
		public static extPoint CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new extPoint(new Hole(g.Symbol.extPoint, holeId));
		}

		// Token: 0x06009A94 RID: 39572 RVA: 0x0020B21E File Offset: 0x0020941E
		public extPoint(GrammarBuilders g, Record<int, int, int, int>? value)
		{
			this = new extPoint(new LiteralNode(g.Symbol.extPoint, value));
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x06009A95 RID: 39573 RVA: 0x0020B23C File Offset: 0x0020943C
		public Record<int, int, int, int>? Value
		{
			get
			{
				return (Record<int, int, int, int>?)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009A96 RID: 39574 RVA: 0x0020B253 File Offset: 0x00209453
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A97 RID: 39575 RVA: 0x0020B268 File Offset: 0x00209468
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A98 RID: 39576 RVA: 0x0020B292 File Offset: 0x00209492
		public bool Equals(extPoint other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DED RID: 15853
		private ProgramNode _node;
	}
}
