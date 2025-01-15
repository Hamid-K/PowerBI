using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001071 RID: 4209
	public struct filterSelection4 : IProgramNodeBuilder, IEquatable<filterSelection4>
	{
		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06007DAD RID: 32173 RVA: 0x001A7A86 File Offset: 0x001A5C86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007DAE RID: 32174 RVA: 0x001A7A8E File Offset: 0x001A5C8E
		private filterSelection4(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007DAF RID: 32175 RVA: 0x001A7A97 File Offset: 0x001A5C97
		public static filterSelection4 CreateUnsafe(ProgramNode node)
		{
			return new filterSelection4(node);
		}

		// Token: 0x06007DB0 RID: 32176 RVA: 0x001A7AA0 File Offset: 0x001A5CA0
		public static filterSelection4? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.filterSelection4)
			{
				return null;
			}
			return new filterSelection4?(filterSelection4.CreateUnsafe(node));
		}

		// Token: 0x06007DB1 RID: 32177 RVA: 0x001A7ADA File Offset: 0x001A5CDA
		public static filterSelection4 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new filterSelection4(new Hole(g.Symbol.filterSelection4, holeId));
		}

		// Token: 0x06007DB2 RID: 32178 RVA: 0x001A7AF2 File Offset: 0x001A5CF2
		public LeafFilter4 Cast_LeafFilter4()
		{
			return LeafFilter4.CreateUnsafe(this.Node);
		}

		// Token: 0x06007DB3 RID: 32179 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LeafFilter4(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007DB4 RID: 32180 RVA: 0x001A7AFF File Offset: 0x001A5CFF
		public bool Is_LeafFilter4(GrammarBuilders g, out LeafFilter4 value)
		{
			value = LeafFilter4.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007DB5 RID: 32181 RVA: 0x001A7B13 File Offset: 0x001A5D13
		public LeafFilter4? As_LeafFilter4(GrammarBuilders g)
		{
			return new LeafFilter4?(LeafFilter4.CreateUnsafe(this.Node));
		}

		// Token: 0x06007DB6 RID: 32182 RVA: 0x001A7B25 File Offset: 0x001A5D25
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007DB7 RID: 32183 RVA: 0x001A7B38 File Offset: 0x001A5D38
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007DB8 RID: 32184 RVA: 0x001A7B62 File Offset: 0x001A5D62
		public bool Equals(filterSelection4 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400338A RID: 13194
		private ProgramNode _node;
	}
}
