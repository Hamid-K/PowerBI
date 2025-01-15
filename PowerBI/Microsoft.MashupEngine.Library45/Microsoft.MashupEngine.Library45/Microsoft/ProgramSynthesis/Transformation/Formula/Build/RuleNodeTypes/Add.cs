using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200156B RID: 5483
	public struct Add : IProgramNodeBuilder, IEquatable<Add>
	{
		// Token: 0x17001F3D RID: 7997
		// (get) Token: 0x0600B346 RID: 45894 RVA: 0x0027323E File Offset: 0x0027143E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B347 RID: 45895 RVA: 0x00273246 File Offset: 0x00271446
		private Add(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B348 RID: 45896 RVA: 0x0027324F File Offset: 0x0027144F
		public static Add CreateUnsafe(ProgramNode node)
		{
			return new Add(node);
		}

		// Token: 0x0600B349 RID: 45897 RVA: 0x00273258 File Offset: 0x00271458
		public static Add? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Add)
			{
				return null;
			}
			return new Add?(Add.CreateUnsafe(node));
		}

		// Token: 0x0600B34A RID: 45898 RVA: 0x0027328D File Offset: 0x0027148D
		public Add(GrammarBuilders g, arithmeticLeft value0, addRight value1)
		{
			this._node = g.Rule.Add.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B34B RID: 45899 RVA: 0x002732B3 File Offset: 0x002714B3
		public static implicit operator arithmetic(Add arg)
		{
			return arithmetic.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F3E RID: 7998
		// (get) Token: 0x0600B34C RID: 45900 RVA: 0x002732C1 File Offset: 0x002714C1
		public arithmeticLeft arithmeticLeft
		{
			get
			{
				return arithmeticLeft.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F3F RID: 7999
		// (get) Token: 0x0600B34D RID: 45901 RVA: 0x002732D5 File Offset: 0x002714D5
		public addRight addRight
		{
			get
			{
				return addRight.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B34E RID: 45902 RVA: 0x002732E9 File Offset: 0x002714E9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B34F RID: 45903 RVA: 0x002732FC File Offset: 0x002714FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B350 RID: 45904 RVA: 0x00273326 File Offset: 0x00271526
		public bool Equals(Add other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004619 RID: 17945
		private ProgramNode _node;
	}
}
