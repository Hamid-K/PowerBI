using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E62 RID: 7778
	public struct IsAttributePresent : IProgramNodeBuilder, IEquatable<IsAttributePresent>
	{
		// Token: 0x17002B88 RID: 11144
		// (get) Token: 0x06010625 RID: 67109 RVA: 0x00389442 File Offset: 0x00387642
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010626 RID: 67110 RVA: 0x0038944A File Offset: 0x0038764A
		private IsAttributePresent(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010627 RID: 67111 RVA: 0x00389453 File Offset: 0x00387653
		public static IsAttributePresent CreateUnsafe(ProgramNode node)
		{
			return new IsAttributePresent(node);
		}

		// Token: 0x06010628 RID: 67112 RVA: 0x0038945C File Offset: 0x0038765C
		public static IsAttributePresent? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsAttributePresent)
			{
				return null;
			}
			return new IsAttributePresent?(IsAttributePresent.CreateUnsafe(node));
		}

		// Token: 0x06010629 RID: 67113 RVA: 0x00389494 File Offset: 0x00387694
		public IsAttributePresent(GrammarBuilders g, x value0, path value1, name value2, value value3)
		{
			this._node = g.Rule.IsAttributePresent.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node });
		}

		// Token: 0x0601062A RID: 67114 RVA: 0x003894E5 File Offset: 0x003876E5
		public static implicit operator pred(IsAttributePresent arg)
		{
			return pred.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B89 RID: 11145
		// (get) Token: 0x0601062B RID: 67115 RVA: 0x003894F3 File Offset: 0x003876F3
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002B8A RID: 11146
		// (get) Token: 0x0601062C RID: 67116 RVA: 0x00389507 File Offset: 0x00387707
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002B8B RID: 11147
		// (get) Token: 0x0601062D RID: 67117 RVA: 0x0038951B File Offset: 0x0038771B
		public name name
		{
			get
			{
				return name.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x17002B8C RID: 11148
		// (get) Token: 0x0601062E RID: 67118 RVA: 0x0038952F File Offset: 0x0038772F
		public value value
		{
			get
			{
				return value.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x0601062F RID: 67119 RVA: 0x00389543 File Offset: 0x00387743
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010630 RID: 67120 RVA: 0x00389558 File Offset: 0x00387758
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010631 RID: 67121 RVA: 0x00389582 File Offset: 0x00387782
		public bool Equals(IsAttributePresent other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062A1 RID: 25249
		private ProgramNode _node;
	}
}
