using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001588 RID: 5512
	public struct Match : IProgramNodeBuilder, IEquatable<Match>
	{
		// Token: 0x17001F8F RID: 8079
		// (get) Token: 0x0600B480 RID: 46208 RVA: 0x00274E8A File Offset: 0x0027308A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B481 RID: 46209 RVA: 0x00274E92 File Offset: 0x00273092
		private Match(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B482 RID: 46210 RVA: 0x00274E9B File Offset: 0x0027309B
		public static Match CreateUnsafe(ProgramNode node)
		{
			return new Match(node);
		}

		// Token: 0x0600B483 RID: 46211 RVA: 0x00274EA4 File Offset: 0x002730A4
		public static Match? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Match)
			{
				return null;
			}
			return new Match?(Match.CreateUnsafe(node));
		}

		// Token: 0x0600B484 RID: 46212 RVA: 0x00274ED9 File Offset: 0x002730D9
		public Match(GrammarBuilders g, x value0, matchDesc value1, matchInstance value2)
		{
			this._node = g.Rule.Match.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B485 RID: 46213 RVA: 0x00274F06 File Offset: 0x00273106
		public static implicit operator pos(Match arg)
		{
			return pos.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x0600B486 RID: 46214 RVA: 0x00274F14 File Offset: 0x00273114
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x0600B487 RID: 46215 RVA: 0x00274F28 File Offset: 0x00273128
		public matchDesc matchDesc
		{
			get
			{
				return matchDesc.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F92 RID: 8082
		// (get) Token: 0x0600B488 RID: 46216 RVA: 0x00274F3C File Offset: 0x0027313C
		public matchInstance matchInstance
		{
			get
			{
				return matchInstance.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B489 RID: 46217 RVA: 0x00274F50 File Offset: 0x00273150
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B48A RID: 46218 RVA: 0x00274F64 File Offset: 0x00273164
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B48B RID: 46219 RVA: 0x00274F8E File Offset: 0x0027318E
		public bool Equals(Match other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004636 RID: 17974
		private ProgramNode _node;
	}
}
