using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C71 RID: 7281
	public struct columnIdx : IProgramNodeBuilder, IEquatable<columnIdx>
	{
		// Token: 0x1700291A RID: 10522
		// (get) Token: 0x0600F6A2 RID: 63138 RVA: 0x003498AE File Offset: 0x00347AAE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F6A3 RID: 63139 RVA: 0x003498B6 File Offset: 0x00347AB6
		private columnIdx(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F6A4 RID: 63140 RVA: 0x003498BF File Offset: 0x00347ABF
		public static columnIdx CreateUnsafe(ProgramNode node)
		{
			return new columnIdx(node);
		}

		// Token: 0x0600F6A5 RID: 63141 RVA: 0x003498C8 File Offset: 0x00347AC8
		public static columnIdx? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnIdx)
			{
				return null;
			}
			return new columnIdx?(columnIdx.CreateUnsafe(node));
		}

		// Token: 0x0600F6A6 RID: 63142 RVA: 0x00349902 File Offset: 0x00347B02
		public static columnIdx CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnIdx(new Hole(g.Symbol.columnIdx, holeId));
		}

		// Token: 0x0600F6A7 RID: 63143 RVA: 0x0034991A File Offset: 0x00347B1A
		public columnIdx(GrammarBuilders g, int value)
		{
			this = new columnIdx(new LiteralNode(g.Symbol.columnIdx, value));
		}

		// Token: 0x1700291B RID: 10523
		// (get) Token: 0x0600F6A8 RID: 63144 RVA: 0x00349938 File Offset: 0x00347B38
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F6A9 RID: 63145 RVA: 0x0034994F File Offset: 0x00347B4F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F6AA RID: 63146 RVA: 0x00349964 File Offset: 0x00347B64
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F6AB RID: 63147 RVA: 0x0034998E File Offset: 0x00347B8E
		public bool Equals(columnIdx other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B60 RID: 23392
		private ProgramNode _node;
	}
}
