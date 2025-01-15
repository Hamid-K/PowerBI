using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C70 RID: 7280
	public struct idx : IProgramNodeBuilder, IEquatable<idx>
	{
		// Token: 0x17002918 RID: 10520
		// (get) Token: 0x0600F698 RID: 63128 RVA: 0x003497BE File Offset: 0x003479BE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F699 RID: 63129 RVA: 0x003497C6 File Offset: 0x003479C6
		private idx(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F69A RID: 63130 RVA: 0x003497CF File Offset: 0x003479CF
		public static idx CreateUnsafe(ProgramNode node)
		{
			return new idx(node);
		}

		// Token: 0x0600F69B RID: 63131 RVA: 0x003497D8 File Offset: 0x003479D8
		public static idx? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.idx)
			{
				return null;
			}
			return new idx?(idx.CreateUnsafe(node));
		}

		// Token: 0x0600F69C RID: 63132 RVA: 0x00349812 File Offset: 0x00347A12
		public static idx CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new idx(new Hole(g.Symbol.idx, holeId));
		}

		// Token: 0x0600F69D RID: 63133 RVA: 0x0034982A File Offset: 0x00347A2A
		public idx(GrammarBuilders g, string value)
		{
			this = new idx(new LiteralNode(g.Symbol.idx, value));
		}

		// Token: 0x17002919 RID: 10521
		// (get) Token: 0x0600F69E RID: 63134 RVA: 0x00349843 File Offset: 0x00347A43
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F69F RID: 63135 RVA: 0x0034985A File Offset: 0x00347A5A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F6A0 RID: 63136 RVA: 0x00349870 File Offset: 0x00347A70
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F6A1 RID: 63137 RVA: 0x0034989A File Offset: 0x00347A9A
		public bool Equals(idx other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B5F RID: 23391
		private ProgramNode _node;
	}
}
