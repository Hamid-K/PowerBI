using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C0B RID: 3083
	public struct betweenAxis : IProgramNodeBuilder, IEquatable<betweenAxis>
	{
		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06004FB1 RID: 20401 RVA: 0x000FB18E File Offset: 0x000F938E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x000FB196 File Offset: 0x000F9396
		private betweenAxis(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x000FB19F File Offset: 0x000F939F
		public static betweenAxis CreateUnsafe(ProgramNode node)
		{
			return new betweenAxis(node);
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x000FB1A8 File Offset: 0x000F93A8
		public static betweenAxis? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.betweenAxis)
			{
				return null;
			}
			return new betweenAxis?(betweenAxis.CreateUnsafe(node));
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x000FB1E2 File Offset: 0x000F93E2
		public static betweenAxis CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new betweenAxis(new Hole(g.Symbol.betweenAxis, holeId));
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x000FB1FA File Offset: 0x000F93FA
		public betweenAxis(GrammarBuilders g)
		{
			this = new betweenAxis(new VariableNode(g.Symbol.betweenAxis));
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06004FB7 RID: 20407 RVA: 0x000FB212 File Offset: 0x000F9412
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x000FB21F File Offset: 0x000F941F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x000FB234 File Offset: 0x000F9434
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x000FB25E File Offset: 0x000F945E
		public bool Equals(betweenAxis other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002333 RID: 9011
		private ProgramNode _node;
	}
}
