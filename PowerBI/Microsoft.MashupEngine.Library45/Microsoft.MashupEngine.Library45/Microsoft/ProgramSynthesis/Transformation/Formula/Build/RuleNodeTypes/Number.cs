using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001596 RID: 5526
	public struct Number : IProgramNodeBuilder, IEquatable<Number>
	{
		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x0600B519 RID: 46361 RVA: 0x00275C46 File Offset: 0x00273E46
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B51A RID: 46362 RVA: 0x00275C4E File Offset: 0x00273E4E
		private Number(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B51B RID: 46363 RVA: 0x00275C57 File Offset: 0x00273E57
		public static Number CreateUnsafe(ProgramNode node)
		{
			return new Number(node);
		}

		// Token: 0x0600B51C RID: 46364 RVA: 0x00275C60 File Offset: 0x00273E60
		public static Number? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Number)
			{
				return null;
			}
			return new Number?(Number.CreateUnsafe(node));
		}

		// Token: 0x0600B51D RID: 46365 RVA: 0x00275C95 File Offset: 0x00273E95
		public Number(GrammarBuilders g, constNum value0)
		{
			this._node = g.Rule.Number.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B51E RID: 46366 RVA: 0x00275CB4 File Offset: 0x00273EB4
		public static implicit operator constNumber(Number arg)
		{
			return constNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x0600B51F RID: 46367 RVA: 0x00275CC2 File Offset: 0x00273EC2
		public constNum constNum
		{
			get
			{
				return constNum.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B520 RID: 46368 RVA: 0x00275CD6 File Offset: 0x00273ED6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B521 RID: 46369 RVA: 0x00275CEC File Offset: 0x00273EEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B522 RID: 46370 RVA: 0x00275D16 File Offset: 0x00273F16
		public bool Equals(Number other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004644 RID: 17988
		private ProgramNode _node;
	}
}
