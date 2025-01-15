using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001572 RID: 5490
	public struct AddRightNumber : IProgramNodeBuilder, IEquatable<AddRightNumber>
	{
		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x0600B390 RID: 45968 RVA: 0x002738DA File Offset: 0x00271ADA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B391 RID: 45969 RVA: 0x002738E2 File Offset: 0x00271AE2
		private AddRightNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B392 RID: 45970 RVA: 0x002738EB File Offset: 0x00271AEB
		public static AddRightNumber CreateUnsafe(ProgramNode node)
		{
			return new AddRightNumber(node);
		}

		// Token: 0x0600B393 RID: 45971 RVA: 0x002738F4 File Offset: 0x00271AF4
		public static AddRightNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.AddRightNumber)
			{
				return null;
			}
			return new AddRightNumber?(AddRightNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B394 RID: 45972 RVA: 0x00273929 File Offset: 0x00271B29
		public AddRightNumber(GrammarBuilders g, constNum value0)
		{
			this._node = g.Rule.AddRightNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B395 RID: 45973 RVA: 0x00273948 File Offset: 0x00271B48
		public static implicit operator addRight(AddRightNumber arg)
		{
			return addRight.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F50 RID: 8016
		// (get) Token: 0x0600B396 RID: 45974 RVA: 0x00273956 File Offset: 0x00271B56
		public constNum constNum
		{
			get
			{
				return constNum.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B397 RID: 45975 RVA: 0x0027396A File Offset: 0x00271B6A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B398 RID: 45976 RVA: 0x00273980 File Offset: 0x00271B80
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B399 RID: 45977 RVA: 0x002739AA File Offset: 0x00271BAA
		public bool Equals(AddRightNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004620 RID: 17952
		private ProgramNode _node;
	}
}
