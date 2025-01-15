using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F4B RID: 3915
	public struct tup : IProgramNodeBuilder, IEquatable<tup>
	{
		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06006D17 RID: 27927 RVA: 0x001642AE File Offset: 0x001624AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x001642B6 File Offset: 0x001624B6
		private tup(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006D19 RID: 27929 RVA: 0x001642BF File Offset: 0x001624BF
		public static tup CreateUnsafe(ProgramNode node)
		{
			return new tup(node);
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x001642C8 File Offset: 0x001624C8
		public static tup? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.tup)
			{
				return null;
			}
			return new tup?(tup.CreateUnsafe(node));
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x00164302 File Offset: 0x00162502
		public static tup CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new tup(new Hole(g.Symbol.tup, holeId));
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x0016431A File Offset: 0x0016251A
		public tup(GrammarBuilders g)
		{
			this = new tup(new VariableNode(g.Symbol.tup));
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06006D1D RID: 27933 RVA: 0x00164332 File Offset: 0x00162532
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06006D1E RID: 27934 RVA: 0x0016433F File Offset: 0x0016253F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x00164354 File Offset: 0x00162554
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x0016437E File Offset: 0x0016257E
		public bool Equals(tup other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F36 RID: 12086
		private ProgramNode _node;
	}
}
