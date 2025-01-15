using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001589 RID: 5513
	public struct MatchEnd : IProgramNodeBuilder, IEquatable<MatchEnd>
	{
		// Token: 0x17001F93 RID: 8083
		// (get) Token: 0x0600B48C RID: 46220 RVA: 0x00274FA2 File Offset: 0x002731A2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B48D RID: 46221 RVA: 0x00274FAA File Offset: 0x002731AA
		private MatchEnd(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B48E RID: 46222 RVA: 0x00274FB3 File Offset: 0x002731B3
		public static MatchEnd CreateUnsafe(ProgramNode node)
		{
			return new MatchEnd(node);
		}

		// Token: 0x0600B48F RID: 46223 RVA: 0x00274FBC File Offset: 0x002731BC
		public static MatchEnd? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MatchEnd)
			{
				return null;
			}
			return new MatchEnd?(MatchEnd.CreateUnsafe(node));
		}

		// Token: 0x0600B490 RID: 46224 RVA: 0x00274FF1 File Offset: 0x002731F1
		public MatchEnd(GrammarBuilders g, x value0, matchDesc value1, matchInstance value2)
		{
			this._node = g.Rule.MatchEnd.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B491 RID: 46225 RVA: 0x0027501E File Offset: 0x0027321E
		public static implicit operator pos(MatchEnd arg)
		{
			return pos.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F94 RID: 8084
		// (get) Token: 0x0600B492 RID: 46226 RVA: 0x0027502C File Offset: 0x0027322C
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x0600B493 RID: 46227 RVA: 0x00275040 File Offset: 0x00273240
		public matchDesc matchDesc
		{
			get
			{
				return matchDesc.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x0600B494 RID: 46228 RVA: 0x00275054 File Offset: 0x00273254
		public matchInstance matchInstance
		{
			get
			{
				return matchInstance.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B495 RID: 46229 RVA: 0x00275068 File Offset: 0x00273268
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B496 RID: 46230 RVA: 0x0027507C File Offset: 0x0027327C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B497 RID: 46231 RVA: 0x002750A6 File Offset: 0x002732A6
		public bool Equals(MatchEnd other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004637 RID: 17975
		private ProgramNode _node;
	}
}
