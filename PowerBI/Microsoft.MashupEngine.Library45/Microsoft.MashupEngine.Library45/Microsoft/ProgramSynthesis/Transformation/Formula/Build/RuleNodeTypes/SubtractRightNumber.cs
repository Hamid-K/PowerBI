using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001573 RID: 5491
	public struct SubtractRightNumber : IProgramNodeBuilder, IEquatable<SubtractRightNumber>
	{
		// Token: 0x17001F51 RID: 8017
		// (get) Token: 0x0600B39A RID: 45978 RVA: 0x002739BE File Offset: 0x00271BBE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B39B RID: 45979 RVA: 0x002739C6 File Offset: 0x00271BC6
		private SubtractRightNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B39C RID: 45980 RVA: 0x002739CF File Offset: 0x00271BCF
		public static SubtractRightNumber CreateUnsafe(ProgramNode node)
		{
			return new SubtractRightNumber(node);
		}

		// Token: 0x0600B39D RID: 45981 RVA: 0x002739D8 File Offset: 0x00271BD8
		public static SubtractRightNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SubtractRightNumber)
			{
				return null;
			}
			return new SubtractRightNumber?(SubtractRightNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B39E RID: 45982 RVA: 0x00273A0D File Offset: 0x00271C0D
		public SubtractRightNumber(GrammarBuilders g, constNum value0)
		{
			this._node = g.Rule.SubtractRightNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B39F RID: 45983 RVA: 0x00273A2C File Offset: 0x00271C2C
		public static implicit operator subtractRight(SubtractRightNumber arg)
		{
			return subtractRight.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F52 RID: 8018
		// (get) Token: 0x0600B3A0 RID: 45984 RVA: 0x00273A3A File Offset: 0x00271C3A
		public constNum constNum
		{
			get
			{
				return constNum.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B3A1 RID: 45985 RVA: 0x00273A4E File Offset: 0x00271C4E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B3A2 RID: 45986 RVA: 0x00273A64 File Offset: 0x00271C64
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B3A3 RID: 45987 RVA: 0x00273A8E File Offset: 0x00271C8E
		public bool Equals(SubtractRightNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004621 RID: 17953
		private ProgramNode _node;
	}
}
