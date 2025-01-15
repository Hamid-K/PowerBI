using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C22 RID: 7202
	public struct PosPair : IProgramNodeBuilder, IEquatable<PosPair>
	{
		// Token: 0x17002895 RID: 10389
		// (get) Token: 0x0600F284 RID: 62084 RVA: 0x0034127A File Offset: 0x0033F47A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F285 RID: 62085 RVA: 0x00341282 File Offset: 0x0033F482
		private PosPair(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F286 RID: 62086 RVA: 0x0034128B File Offset: 0x0033F48B
		public static PosPair CreateUnsafe(ProgramNode node)
		{
			return new PosPair(node);
		}

		// Token: 0x0600F287 RID: 62087 RVA: 0x00341294 File Offset: 0x0033F494
		public static PosPair? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.PosPair)
			{
				return null;
			}
			return new PosPair?(PosPair.CreateUnsafe(node));
		}

		// Token: 0x0600F288 RID: 62088 RVA: 0x003412C9 File Offset: 0x0033F4C9
		public PosPair(GrammarBuilders g, pos value0, pos value1)
		{
			this._node = g.Rule.PosPair.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x0600F289 RID: 62089 RVA: 0x003412FB File Offset: 0x0033F4FB
		public static implicit operator PP(PosPair arg)
		{
			return PP.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002896 RID: 10390
		// (get) Token: 0x0600F28A RID: 62090 RVA: 0x00341309 File Offset: 0x0033F509
		public pos pos1
		{
			get
			{
				return pos.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002897 RID: 10391
		// (get) Token: 0x0600F28B RID: 62091 RVA: 0x0034131D File Offset: 0x0033F51D
		public pos pos2
		{
			get
			{
				return pos.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F28C RID: 62092 RVA: 0x00341331 File Offset: 0x0033F531
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F28D RID: 62093 RVA: 0x00341344 File Offset: 0x0033F544
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F28E RID: 62094 RVA: 0x0034136E File Offset: 0x0033F56E
		public bool Equals(PosPair other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B11 RID: 23313
		private ProgramNode _node;
	}
}
