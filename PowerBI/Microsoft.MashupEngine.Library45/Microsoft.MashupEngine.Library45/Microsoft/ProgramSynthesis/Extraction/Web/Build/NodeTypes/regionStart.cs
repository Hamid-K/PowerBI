using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200109E RID: 4254
	public struct regionStart : IProgramNodeBuilder, IEquatable<regionStart>
	{
		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06008033 RID: 32819 RVA: 0x001AD1FA File Offset: 0x001AB3FA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008034 RID: 32820 RVA: 0x001AD202 File Offset: 0x001AB402
		private regionStart(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x001AD20B File Offset: 0x001AB40B
		public static regionStart CreateUnsafe(ProgramNode node)
		{
			return new regionStart(node);
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x001AD214 File Offset: 0x001AB414
		public static regionStart? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.regionStart)
			{
				return null;
			}
			return new regionStart?(regionStart.CreateUnsafe(node));
		}

		// Token: 0x06008037 RID: 32823 RVA: 0x001AD24E File Offset: 0x001AB44E
		public static regionStart CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new regionStart(new Hole(g.Symbol.regionStart, holeId));
		}

		// Token: 0x06008038 RID: 32824 RVA: 0x001AD266 File Offset: 0x001AB466
		public regionStart(GrammarBuilders g)
		{
			this = new regionStart(new VariableNode(g.Symbol.regionStart));
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06008039 RID: 32825 RVA: 0x001AD27E File Offset: 0x001AB47E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600803A RID: 32826 RVA: 0x001AD28B File Offset: 0x001AB48B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600803B RID: 32827 RVA: 0x001AD2A0 File Offset: 0x001AB4A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600803C RID: 32828 RVA: 0x001AD2CA File Offset: 0x001AB4CA
		public bool Equals(regionStart other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033B7 RID: 13239
		private ProgramNode _node;
	}
}
