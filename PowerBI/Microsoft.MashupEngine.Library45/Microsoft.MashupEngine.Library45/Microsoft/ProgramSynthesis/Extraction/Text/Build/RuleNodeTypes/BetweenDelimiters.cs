using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F28 RID: 3880
	public struct BetweenDelimiters : IProgramNodeBuilder, IEquatable<BetweenDelimiters>
	{
		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06006B58 RID: 27480 RVA: 0x00160EE2 File Offset: 0x0015F0E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B59 RID: 27481 RVA: 0x00160EEA File Offset: 0x0015F0EA
		private BetweenDelimiters(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B5A RID: 27482 RVA: 0x00160EF3 File Offset: 0x0015F0F3
		public static BetweenDelimiters CreateUnsafe(ProgramNode node)
		{
			return new BetweenDelimiters(node);
		}

		// Token: 0x06006B5B RID: 27483 RVA: 0x00160EFC File Offset: 0x0015F0FC
		public static BetweenDelimiters? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.BetweenDelimiters)
			{
				return null;
			}
			return new BetweenDelimiters?(BetweenDelimiters.CreateUnsafe(node));
		}

		// Token: 0x06006B5C RID: 27484 RVA: 0x00160F31 File Offset: 0x0015F131
		public BetweenDelimiters(GrammarBuilders g, row value0, del value1, del value2)
		{
			this._node = g.Rule.BetweenDelimiters.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06006B5D RID: 27485 RVA: 0x00160F5E File Offset: 0x0015F15E
		public static implicit operator extract(BetweenDelimiters arg)
		{
			return extract.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06006B5E RID: 27486 RVA: 0x00160F6C File Offset: 0x0015F16C
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06006B5F RID: 27487 RVA: 0x00160F80 File Offset: 0x0015F180
		public del del1
		{
			get
			{
				return del.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06006B60 RID: 27488 RVA: 0x00160F94 File Offset: 0x0015F194
		public del del2
		{
			get
			{
				return del.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06006B61 RID: 27489 RVA: 0x00160FA8 File Offset: 0x0015F1A8
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B62 RID: 27490 RVA: 0x00160FBC File Offset: 0x0015F1BC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B63 RID: 27491 RVA: 0x00160FE6 File Offset: 0x0015F1E6
		public bool Equals(BetweenDelimiters other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F13 RID: 12051
		private ProgramNode _node;
	}
}
