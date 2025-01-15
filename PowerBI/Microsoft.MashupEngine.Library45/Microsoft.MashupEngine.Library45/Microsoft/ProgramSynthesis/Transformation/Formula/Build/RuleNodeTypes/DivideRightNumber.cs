using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001575 RID: 5493
	public struct DivideRightNumber : IProgramNodeBuilder, IEquatable<DivideRightNumber>
	{
		// Token: 0x17001F55 RID: 8021
		// (get) Token: 0x0600B3AE RID: 45998 RVA: 0x00273B86 File Offset: 0x00271D86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B3AF RID: 45999 RVA: 0x00273B8E File Offset: 0x00271D8E
		private DivideRightNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B3B0 RID: 46000 RVA: 0x00273B97 File Offset: 0x00271D97
		public static DivideRightNumber CreateUnsafe(ProgramNode node)
		{
			return new DivideRightNumber(node);
		}

		// Token: 0x0600B3B1 RID: 46001 RVA: 0x00273BA0 File Offset: 0x00271DA0
		public static DivideRightNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DivideRightNumber)
			{
				return null;
			}
			return new DivideRightNumber?(DivideRightNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B3B2 RID: 46002 RVA: 0x00273BD5 File Offset: 0x00271DD5
		public DivideRightNumber(GrammarBuilders g, constNum value0)
		{
			this._node = g.Rule.DivideRightNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B3B3 RID: 46003 RVA: 0x00273BF4 File Offset: 0x00271DF4
		public static implicit operator divideRight(DivideRightNumber arg)
		{
			return divideRight.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F56 RID: 8022
		// (get) Token: 0x0600B3B4 RID: 46004 RVA: 0x00273C02 File Offset: 0x00271E02
		public constNum constNum
		{
			get
			{
				return constNum.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B3B5 RID: 46005 RVA: 0x00273C16 File Offset: 0x00271E16
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B3B6 RID: 46006 RVA: 0x00273C2C File Offset: 0x00271E2C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B3B7 RID: 46007 RVA: 0x00273C56 File Offset: 0x00271E56
		public bool Equals(DivideRightNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004623 RID: 17955
		private ProgramNode _node;
	}
}
