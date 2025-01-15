using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001351 RID: 4945
	public struct RegexMatch : IProgramNodeBuilder, IEquatable<RegexMatch>
	{
		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x0600988A RID: 39050 RVA: 0x00206EAA File Offset: 0x002050AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600988B RID: 39051 RVA: 0x00206EB2 File Offset: 0x002050B2
		private RegexMatch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600988C RID: 39052 RVA: 0x00206EBB File Offset: 0x002050BB
		public static RegexMatch CreateUnsafe(ProgramNode node)
		{
			return new RegexMatch(node);
		}

		// Token: 0x0600988D RID: 39053 RVA: 0x00206EC4 File Offset: 0x002050C4
		public static RegexMatch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RegexMatch)
			{
				return null;
			}
			return new RegexMatch?(RegexMatch.CreateUnsafe(node));
		}

		// Token: 0x0600988E RID: 39054 RVA: 0x00206EF9 File Offset: 0x002050F9
		public RegexMatch(GrammarBuilders g, v value0, regex value1)
		{
			this._node = g.Rule.RegexMatch.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600988F RID: 39055 RVA: 0x00206F1F File Offset: 0x0020511F
		public static implicit operator regexMatch(RegexMatch arg)
		{
			return regexMatch.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x06009890 RID: 39056 RVA: 0x00206F2D File Offset: 0x0020512D
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x06009891 RID: 39057 RVA: 0x00206F41 File Offset: 0x00205141
		public regex regex
		{
			get
			{
				return regex.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06009892 RID: 39058 RVA: 0x00206F55 File Offset: 0x00205155
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009893 RID: 39059 RVA: 0x00206F68 File Offset: 0x00205168
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009894 RID: 39060 RVA: 0x00206F92 File Offset: 0x00205192
		public bool Equals(RegexMatch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DC8 RID: 15816
		private ProgramNode _node;
	}
}
