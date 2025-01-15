using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001086 RID: 4230
	public struct gen_NthLastChild : IProgramNodeBuilder, IEquatable<gen_NthLastChild>
	{
		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06007F37 RID: 32567 RVA: 0x001ABB7A File Offset: 0x001A9D7A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F38 RID: 32568 RVA: 0x001ABB82 File Offset: 0x001A9D82
		private gen_NthLastChild(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F39 RID: 32569 RVA: 0x001ABB8B File Offset: 0x001A9D8B
		public static gen_NthLastChild CreateUnsafe(ProgramNode node)
		{
			return new gen_NthLastChild(node);
		}

		// Token: 0x06007F3A RID: 32570 RVA: 0x001ABB94 File Offset: 0x001A9D94
		public static gen_NthLastChild? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.gen_NthLastChild)
			{
				return null;
			}
			return new gen_NthLastChild?(gen_NthLastChild.CreateUnsafe(node));
		}

		// Token: 0x06007F3B RID: 32571 RVA: 0x001ABBCE File Offset: 0x001A9DCE
		public static gen_NthLastChild CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new gen_NthLastChild(new Hole(g.Symbol.gen_NthLastChild, holeId));
		}

		// Token: 0x06007F3C RID: 32572 RVA: 0x001ABBE6 File Offset: 0x001A9DE6
		public GEN_NthLastChildFilter Cast_GEN_NthLastChildFilter()
		{
			return GEN_NthLastChildFilter.CreateUnsafe(this.Node);
		}

		// Token: 0x06007F3D RID: 32573 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GEN_NthLastChildFilter(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007F3E RID: 32574 RVA: 0x001ABBF3 File Offset: 0x001A9DF3
		public bool Is_GEN_NthLastChildFilter(GrammarBuilders g, out GEN_NthLastChildFilter value)
		{
			value = GEN_NthLastChildFilter.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007F3F RID: 32575 RVA: 0x001ABC07 File Offset: 0x001A9E07
		public GEN_NthLastChildFilter? As_GEN_NthLastChildFilter(GrammarBuilders g)
		{
			return new GEN_NthLastChildFilter?(GEN_NthLastChildFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F40 RID: 32576 RVA: 0x001ABC19 File Offset: 0x001A9E19
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F41 RID: 32577 RVA: 0x001ABC2C File Offset: 0x001A9E2C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F42 RID: 32578 RVA: 0x001ABC56 File Offset: 0x001A9E56
		public bool Equals(gen_NthLastChild other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400339F RID: 13215
		private ProgramNode _node;
	}
}
