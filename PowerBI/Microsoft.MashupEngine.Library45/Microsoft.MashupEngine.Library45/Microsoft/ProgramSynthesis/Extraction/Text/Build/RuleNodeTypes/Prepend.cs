using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F24 RID: 3876
	public struct Prepend : IProgramNodeBuilder, IEquatable<Prepend>
	{
		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06006B2F RID: 27439 RVA: 0x00160B3A File Offset: 0x0015ED3A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B30 RID: 27440 RVA: 0x00160B42 File Offset: 0x0015ED42
		private Prepend(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B31 RID: 27441 RVA: 0x00160B4B File Offset: 0x0015ED4B
		public static Prepend CreateUnsafe(ProgramNode node)
		{
			return new Prepend(node);
		}

		// Token: 0x06006B32 RID: 27442 RVA: 0x00160B54 File Offset: 0x0015ED54
		public static Prepend? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Prepend)
			{
				return null;
			}
			return new Prepend?(Prepend.CreateUnsafe(node));
		}

		// Token: 0x06006B33 RID: 27443 RVA: 0x00160B89 File Offset: 0x0015ED89
		public Prepend(GrammarBuilders g, extractTup value0, colSplit value1)
		{
			this._node = g.Rule.Prepend.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06006B34 RID: 27444 RVA: 0x00160BAF File Offset: 0x0015EDAF
		public static implicit operator _LetB1(Prepend arg)
		{
			return _LetB1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06006B35 RID: 27445 RVA: 0x00160BBD File Offset: 0x0015EDBD
		public extractTup extractTup
		{
			get
			{
				return extractTup.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06006B36 RID: 27446 RVA: 0x00160BD1 File Offset: 0x0015EDD1
		public colSplit colSplit
		{
			get
			{
				return colSplit.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006B37 RID: 27447 RVA: 0x00160BE5 File Offset: 0x0015EDE5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B38 RID: 27448 RVA: 0x00160BF8 File Offset: 0x0015EDF8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B39 RID: 27449 RVA: 0x00160C22 File Offset: 0x0015EE22
		public bool Equals(Prepend other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F0F RID: 12047
		private ProgramNode _node;
	}
}
