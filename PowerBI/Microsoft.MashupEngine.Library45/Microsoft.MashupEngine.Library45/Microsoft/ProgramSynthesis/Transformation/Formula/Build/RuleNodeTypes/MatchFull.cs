using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200157E RID: 5502
	public struct MatchFull : IProgramNodeBuilder, IEquatable<MatchFull>
	{
		// Token: 0x17001F6F RID: 8047
		// (get) Token: 0x0600B410 RID: 46096 RVA: 0x0027444A File Offset: 0x0027264A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B411 RID: 46097 RVA: 0x00274452 File Offset: 0x00272652
		private MatchFull(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B412 RID: 46098 RVA: 0x0027445B File Offset: 0x0027265B
		public static MatchFull CreateUnsafe(ProgramNode node)
		{
			return new MatchFull(node);
		}

		// Token: 0x0600B413 RID: 46099 RVA: 0x00274464 File Offset: 0x00272664
		public static MatchFull? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MatchFull)
			{
				return null;
			}
			return new MatchFull?(MatchFull.CreateUnsafe(node));
		}

		// Token: 0x0600B414 RID: 46100 RVA: 0x00274499 File Offset: 0x00272699
		public MatchFull(GrammarBuilders g, x value0, matchDesc value1, matchInstance value2)
		{
			this._node = g.Rule.MatchFull.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B415 RID: 46101 RVA: 0x002744C6 File Offset: 0x002726C6
		public static implicit operator substring(MatchFull arg)
		{
			return substring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x0600B416 RID: 46102 RVA: 0x002744D4 File Offset: 0x002726D4
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x0600B417 RID: 46103 RVA: 0x002744E8 File Offset: 0x002726E8
		public matchDesc matchDesc
		{
			get
			{
				return matchDesc.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x0600B418 RID: 46104 RVA: 0x002744FC File Offset: 0x002726FC
		public matchInstance matchInstance
		{
			get
			{
				return matchInstance.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B419 RID: 46105 RVA: 0x00274510 File Offset: 0x00272710
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B41A RID: 46106 RVA: 0x00274524 File Offset: 0x00272724
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B41B RID: 46107 RVA: 0x0027454E File Offset: 0x0027274E
		public bool Equals(MatchFull other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400462C RID: 17964
		private ProgramNode _node;
	}
}
