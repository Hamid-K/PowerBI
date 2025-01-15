using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001563 RID: 5475
	public struct ContainsMatch : IProgramNodeBuilder, IEquatable<ContainsMatch>
	{
		// Token: 0x17001F26 RID: 7974
		// (get) Token: 0x0600B2EF RID: 45807 RVA: 0x00272A4E File Offset: 0x00270C4E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B2F0 RID: 45808 RVA: 0x00272A56 File Offset: 0x00270C56
		private ContainsMatch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B2F1 RID: 45809 RVA: 0x00272A5F File Offset: 0x00270C5F
		public static ContainsMatch CreateUnsafe(ProgramNode node)
		{
			return new ContainsMatch(node);
		}

		// Token: 0x0600B2F2 RID: 45810 RVA: 0x00272A68 File Offset: 0x00270C68
		public static ContainsMatch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ContainsMatch)
			{
				return null;
			}
			return new ContainsMatch?(ContainsMatch.CreateUnsafe(node));
		}

		// Token: 0x0600B2F3 RID: 45811 RVA: 0x00272AA0 File Offset: 0x00270CA0
		public ContainsMatch(GrammarBuilders g, row value0, columnName value1, containsMatchRegex value2, matchCount value3)
		{
			this._node = g.Rule.ContainsMatch.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node });
		}

		// Token: 0x0600B2F4 RID: 45812 RVA: 0x00272AF1 File Offset: 0x00270CF1
		public static implicit operator condition(ContainsMatch arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F27 RID: 7975
		// (get) Token: 0x0600B2F5 RID: 45813 RVA: 0x00272AFF File Offset: 0x00270CFF
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F28 RID: 7976
		// (get) Token: 0x0600B2F6 RID: 45814 RVA: 0x00272B13 File Offset: 0x00270D13
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F29 RID: 7977
		// (get) Token: 0x0600B2F7 RID: 45815 RVA: 0x00272B27 File Offset: 0x00270D27
		public containsMatchRegex containsMatchRegex
		{
			get
			{
				return containsMatchRegex.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x17001F2A RID: 7978
		// (get) Token: 0x0600B2F8 RID: 45816 RVA: 0x00272B3B File Offset: 0x00270D3B
		public matchCount matchCount
		{
			get
			{
				return matchCount.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x0600B2F9 RID: 45817 RVA: 0x00272B4F File Offset: 0x00270D4F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B2FA RID: 45818 RVA: 0x00272B64 File Offset: 0x00270D64
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B2FB RID: 45819 RVA: 0x00272B8E File Offset: 0x00270D8E
		public bool Equals(ContainsMatch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004611 RID: 17937
		private ProgramNode _node;
	}
}
