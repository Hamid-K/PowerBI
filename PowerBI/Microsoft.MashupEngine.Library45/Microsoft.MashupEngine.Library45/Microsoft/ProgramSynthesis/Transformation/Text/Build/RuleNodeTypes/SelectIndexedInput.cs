using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C2A RID: 7210
	public struct SelectIndexedInput : IProgramNodeBuilder, IEquatable<SelectIndexedInput>
	{
		// Token: 0x170028A7 RID: 10407
		// (get) Token: 0x0600F2D6 RID: 62166 RVA: 0x003419E2 File Offset: 0x0033FBE2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F2D7 RID: 62167 RVA: 0x003419EA File Offset: 0x0033FBEA
		private SelectIndexedInput(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F2D8 RID: 62168 RVA: 0x003419F3 File Offset: 0x0033FBF3
		public static SelectIndexedInput CreateUnsafe(ProgramNode node)
		{
			return new SelectIndexedInput(node);
		}

		// Token: 0x0600F2D9 RID: 62169 RVA: 0x003419FC File Offset: 0x0033FBFC
		public static SelectIndexedInput? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectIndexedInput)
			{
				return null;
			}
			return new SelectIndexedInput?(SelectIndexedInput.CreateUnsafe(node));
		}

		// Token: 0x0600F2DA RID: 62170 RVA: 0x00341A31 File Offset: 0x0033FC31
		public SelectIndexedInput(GrammarBuilders g, v value0)
		{
			this._node = g.Rule.SelectIndexedInput.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F2DB RID: 62171 RVA: 0x00341A50 File Offset: 0x0033FC50
		public static implicit operator y(SelectIndexedInput arg)
		{
			return y.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028A8 RID: 10408
		// (get) Token: 0x0600F2DC RID: 62172 RVA: 0x00341A5E File Offset: 0x0033FC5E
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F2DD RID: 62173 RVA: 0x00341A72 File Offset: 0x0033FC72
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F2DE RID: 62174 RVA: 0x00341A88 File Offset: 0x0033FC88
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F2DF RID: 62175 RVA: 0x00341AB2 File Offset: 0x0033FCB2
		public bool Equals(SelectIndexedInput other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B19 RID: 23321
		private ProgramNode _node;
	}
}
