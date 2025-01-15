using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001074 RID: 4212
	public struct filterSelection5 : IProgramNodeBuilder, IEquatable<filterSelection5>
	{
		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06007DDD RID: 32221 RVA: 0x001A81EE File Offset: 0x001A63EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007DDE RID: 32222 RVA: 0x001A81F6 File Offset: 0x001A63F6
		private filterSelection5(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007DDF RID: 32223 RVA: 0x001A81FF File Offset: 0x001A63FF
		public static filterSelection5 CreateUnsafe(ProgramNode node)
		{
			return new filterSelection5(node);
		}

		// Token: 0x06007DE0 RID: 32224 RVA: 0x001A8208 File Offset: 0x001A6408
		public static filterSelection5? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.filterSelection5)
			{
				return null;
			}
			return new filterSelection5?(filterSelection5.CreateUnsafe(node));
		}

		// Token: 0x06007DE1 RID: 32225 RVA: 0x001A8242 File Offset: 0x001A6442
		public static filterSelection5 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new filterSelection5(new Hole(g.Symbol.filterSelection5, holeId));
		}

		// Token: 0x06007DE2 RID: 32226 RVA: 0x001A825A File Offset: 0x001A645A
		public LeafFilter5 Cast_LeafFilter5()
		{
			return LeafFilter5.CreateUnsafe(this.Node);
		}

		// Token: 0x06007DE3 RID: 32227 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LeafFilter5(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007DE4 RID: 32228 RVA: 0x001A8267 File Offset: 0x001A6467
		public bool Is_LeafFilter5(GrammarBuilders g, out LeafFilter5 value)
		{
			value = LeafFilter5.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007DE5 RID: 32229 RVA: 0x001A827B File Offset: 0x001A647B
		public LeafFilter5? As_LeafFilter5(GrammarBuilders g)
		{
			return new LeafFilter5?(LeafFilter5.CreateUnsafe(this.Node));
		}

		// Token: 0x06007DE6 RID: 32230 RVA: 0x001A828D File Offset: 0x001A648D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007DE7 RID: 32231 RVA: 0x001A82A0 File Offset: 0x001A64A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007DE8 RID: 32232 RVA: 0x001A82CA File Offset: 0x001A64CA
		public bool Equals(filterSelection5 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400338D RID: 13197
		private ProgramNode _node;
	}
}
