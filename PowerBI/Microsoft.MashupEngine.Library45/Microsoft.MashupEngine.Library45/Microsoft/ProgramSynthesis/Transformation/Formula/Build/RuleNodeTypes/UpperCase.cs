using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001550 RID: 5456
	public struct UpperCase : IProgramNodeBuilder, IEquatable<UpperCase>
	{
		// Token: 0x17001EEA RID: 7914
		// (get) Token: 0x0600B21B RID: 45595 RVA: 0x00271712 File Offset: 0x0026F912
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B21C RID: 45596 RVA: 0x0027171A File Offset: 0x0026F91A
		private UpperCase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B21D RID: 45597 RVA: 0x00271723 File Offset: 0x0026F923
		public static UpperCase CreateUnsafe(ProgramNode node)
		{
			return new UpperCase(node);
		}

		// Token: 0x0600B21E RID: 45598 RVA: 0x0027172C File Offset: 0x0026F92C
		public static UpperCase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.UpperCase)
			{
				return null;
			}
			return new UpperCase?(UpperCase.CreateUnsafe(node));
		}

		// Token: 0x0600B21F RID: 45599 RVA: 0x00271761 File Offset: 0x0026F961
		public UpperCase(GrammarBuilders g, segment value0)
		{
			this._node = g.Rule.UpperCase.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B220 RID: 45600 RVA: 0x00271780 File Offset: 0x0026F980
		public static implicit operator segmentCase(UpperCase arg)
		{
			return segmentCase.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EEB RID: 7915
		// (get) Token: 0x0600B221 RID: 45601 RVA: 0x0027178E File Offset: 0x0026F98E
		public segment segment
		{
			get
			{
				return segment.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B222 RID: 45602 RVA: 0x002717A2 File Offset: 0x0026F9A2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B223 RID: 45603 RVA: 0x002717B8 File Offset: 0x0026F9B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B224 RID: 45604 RVA: 0x002717E2 File Offset: 0x0026F9E2
		public bool Equals(UpperCase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045FE RID: 17918
		private ProgramNode _node;
	}
}
