using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B6C RID: 2924
	public struct selectRegion : IProgramNodeBuilder, IEquatable<selectRegion>
	{
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x000E958E File Offset: 0x000E778E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x000E9596 File Offset: 0x000E7796
		private selectRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x000E959F File Offset: 0x000E779F
		public static selectRegion CreateUnsafe(ProgramNode node)
		{
			return new selectRegion(node);
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x000E95A8 File Offset: 0x000E77A8
		public static selectRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selectRegion)
			{
				return null;
			}
			return new selectRegion?(selectRegion.CreateUnsafe(node));
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x000E95E2 File Offset: 0x000E77E2
		public static selectRegion CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selectRegion(new Hole(g.Symbol.selectRegion, holeId));
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x000E95FA File Offset: 0x000E77FA
		public SelectRegion Cast_SelectRegion()
		{
			return SelectRegion.CreateUnsafe(this.Node);
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SelectRegion(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x000E9607 File Offset: 0x000E7807
		public bool Is_SelectRegion(GrammarBuilders g, out SelectRegion value)
		{
			value = SelectRegion.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x000E961B File Offset: 0x000E781B
		public SelectRegion? As_SelectRegion(GrammarBuilders g)
		{
			return new SelectRegion?(SelectRegion.CreateUnsafe(this.Node));
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x000E962D File Offset: 0x000E782D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x000E9640 File Offset: 0x000E7840
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x000E966A File Offset: 0x000E786A
		public bool Equals(selectRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002167 RID: 8551
		private ProgramNode _node;
	}
}
