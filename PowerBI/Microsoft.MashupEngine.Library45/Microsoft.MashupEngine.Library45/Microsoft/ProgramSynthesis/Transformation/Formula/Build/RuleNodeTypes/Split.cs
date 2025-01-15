using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001582 RID: 5506
	public struct Split : IProgramNodeBuilder, IEquatable<Split>
	{
		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x0600B43C RID: 46140 RVA: 0x00274842 File Offset: 0x00272A42
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B43D RID: 46141 RVA: 0x0027484A File Offset: 0x00272A4A
		private Split(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B43E RID: 46142 RVA: 0x00274853 File Offset: 0x00272A53
		public static Split CreateUnsafe(ProgramNode node)
		{
			return new Split(node);
		}

		// Token: 0x0600B43F RID: 46143 RVA: 0x0027485C File Offset: 0x00272A5C
		public static Split? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Split)
			{
				return null;
			}
			return new Split?(Split.CreateUnsafe(node));
		}

		// Token: 0x0600B440 RID: 46144 RVA: 0x00274891 File Offset: 0x00272A91
		public Split(GrammarBuilders g, x value0, splitDelimiter value1, splitInstance value2)
		{
			this._node = g.Rule.Split.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B441 RID: 46145 RVA: 0x002748BE File Offset: 0x00272ABE
		public static implicit operator split(Split arg)
		{
			return split.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x0600B442 RID: 46146 RVA: 0x002748CC File Offset: 0x00272ACC
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x0600B443 RID: 46147 RVA: 0x002748E0 File Offset: 0x00272AE0
		public splitDelimiter splitDelimiter
		{
			get
			{
				return splitDelimiter.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x0600B444 RID: 46148 RVA: 0x002748F4 File Offset: 0x00272AF4
		public splitInstance splitInstance
		{
			get
			{
				return splitInstance.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B445 RID: 46149 RVA: 0x00274908 File Offset: 0x00272B08
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B446 RID: 46150 RVA: 0x0027491C File Offset: 0x00272B1C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B447 RID: 46151 RVA: 0x00274946 File Offset: 0x00272B46
		public bool Equals(Split other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004630 RID: 17968
		private ProgramNode _node;
	}
}
