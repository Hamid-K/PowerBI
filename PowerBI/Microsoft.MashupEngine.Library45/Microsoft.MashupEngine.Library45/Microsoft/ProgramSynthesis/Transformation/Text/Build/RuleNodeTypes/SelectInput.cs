using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C1F RID: 7199
	public struct SelectInput : IProgramNodeBuilder, IEquatable<SelectInput>
	{
		// Token: 0x17002888 RID: 10376
		// (get) Token: 0x0600F25F RID: 62047 RVA: 0x00340EE6 File Offset: 0x0033F0E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F260 RID: 62048 RVA: 0x00340EEE File Offset: 0x0033F0EE
		private SelectInput(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F261 RID: 62049 RVA: 0x00340EF7 File Offset: 0x0033F0F7
		public static SelectInput CreateUnsafe(ProgramNode node)
		{
			return new SelectInput(node);
		}

		// Token: 0x0600F262 RID: 62050 RVA: 0x00340F00 File Offset: 0x0033F100
		public static SelectInput? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectInput)
			{
				return null;
			}
			return new SelectInput?(SelectInput.CreateUnsafe(node));
		}

		// Token: 0x0600F263 RID: 62051 RVA: 0x00340F35 File Offset: 0x0033F135
		public SelectInput(GrammarBuilders g, vs value0, name value1)
		{
			this._node = g.Rule.SelectInput.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F264 RID: 62052 RVA: 0x00340F5B File Offset: 0x0033F15B
		public static implicit operator y(SelectInput arg)
		{
			return y.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002889 RID: 10377
		// (get) Token: 0x0600F265 RID: 62053 RVA: 0x00340F69 File Offset: 0x0033F169
		public vs vs
		{
			get
			{
				return vs.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700288A RID: 10378
		// (get) Token: 0x0600F266 RID: 62054 RVA: 0x00340F7D File Offset: 0x0033F17D
		public name name
		{
			get
			{
				return name.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F267 RID: 62055 RVA: 0x00340F91 File Offset: 0x0033F191
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F268 RID: 62056 RVA: 0x00340FA4 File Offset: 0x0033F1A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F269 RID: 62057 RVA: 0x00340FCE File Offset: 0x0033F1CE
		public bool Equals(SelectInput other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B0E RID: 23310
		private ProgramNode _node;
	}
}
