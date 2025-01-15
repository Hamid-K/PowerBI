using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001574 RID: 5492
	public struct MultiplyRightNumber : IProgramNodeBuilder, IEquatable<MultiplyRightNumber>
	{
		// Token: 0x17001F53 RID: 8019
		// (get) Token: 0x0600B3A4 RID: 45988 RVA: 0x00273AA2 File Offset: 0x00271CA2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B3A5 RID: 45989 RVA: 0x00273AAA File Offset: 0x00271CAA
		private MultiplyRightNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B3A6 RID: 45990 RVA: 0x00273AB3 File Offset: 0x00271CB3
		public static MultiplyRightNumber CreateUnsafe(ProgramNode node)
		{
			return new MultiplyRightNumber(node);
		}

		// Token: 0x0600B3A7 RID: 45991 RVA: 0x00273ABC File Offset: 0x00271CBC
		public static MultiplyRightNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MultiplyRightNumber)
			{
				return null;
			}
			return new MultiplyRightNumber?(MultiplyRightNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B3A8 RID: 45992 RVA: 0x00273AF1 File Offset: 0x00271CF1
		public MultiplyRightNumber(GrammarBuilders g, constNum value0)
		{
			this._node = g.Rule.MultiplyRightNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B3A9 RID: 45993 RVA: 0x00273B10 File Offset: 0x00271D10
		public static implicit operator multiplyRight(MultiplyRightNumber arg)
		{
			return multiplyRight.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x0600B3AA RID: 45994 RVA: 0x00273B1E File Offset: 0x00271D1E
		public constNum constNum
		{
			get
			{
				return constNum.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B3AB RID: 45995 RVA: 0x00273B32 File Offset: 0x00271D32
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B3AC RID: 45996 RVA: 0x00273B48 File Offset: 0x00271D48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B3AD RID: 45997 RVA: 0x00273B72 File Offset: 0x00271D72
		public bool Equals(MultiplyRightNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004622 RID: 17954
		private ProgramNode _node;
	}
}
