using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C73 RID: 7283
	public struct cell : IProgramNodeBuilder, IEquatable<cell>
	{
		// Token: 0x1700291E RID: 10526
		// (get) Token: 0x0600F6B6 RID: 63158 RVA: 0x00349A86 File Offset: 0x00347C86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F6B7 RID: 63159 RVA: 0x00349A8E File Offset: 0x00347C8E
		private cell(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F6B8 RID: 63160 RVA: 0x00349A97 File Offset: 0x00347C97
		public static cell CreateUnsafe(ProgramNode node)
		{
			return new cell(node);
		}

		// Token: 0x0600F6B9 RID: 63161 RVA: 0x00349AA0 File Offset: 0x00347CA0
		public static cell? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.cell)
			{
				return null;
			}
			return new cell?(cell.CreateUnsafe(node));
		}

		// Token: 0x0600F6BA RID: 63162 RVA: 0x00349ADA File Offset: 0x00347CDA
		public static cell CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new cell(new Hole(g.Symbol.cell, holeId));
		}

		// Token: 0x0600F6BB RID: 63163 RVA: 0x00349AF2 File Offset: 0x00347CF2
		public cell(GrammarBuilders g)
		{
			this = new cell(new VariableNode(g.Symbol.cell));
		}

		// Token: 0x1700291F RID: 10527
		// (get) Token: 0x0600F6BC RID: 63164 RVA: 0x00349B0A File Offset: 0x00347D0A
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F6BD RID: 63165 RVA: 0x00349B17 File Offset: 0x00347D17
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F6BE RID: 63166 RVA: 0x00349B2C File Offset: 0x00347D2C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F6BF RID: 63167 RVA: 0x00349B56 File Offset: 0x00347D56
		public bool Equals(cell other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B62 RID: 23394
		private ProgramNode _node;
	}
}
