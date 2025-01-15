using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001339 RID: 4921
	public struct splitMatches_constantDelimiterMatches : IProgramNodeBuilder, IEquatable<splitMatches_constantDelimiterMatches>
	{
		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x06009782 RID: 38786 RVA: 0x002056B6 File Offset: 0x002038B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009783 RID: 38787 RVA: 0x002056BE File Offset: 0x002038BE
		private splitMatches_constantDelimiterMatches(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009784 RID: 38788 RVA: 0x002056C7 File Offset: 0x002038C7
		public static splitMatches_constantDelimiterMatches CreateUnsafe(ProgramNode node)
		{
			return new splitMatches_constantDelimiterMatches(node);
		}

		// Token: 0x06009785 RID: 38789 RVA: 0x002056D0 File Offset: 0x002038D0
		public static splitMatches_constantDelimiterMatches? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.splitMatches_constantDelimiterMatches)
			{
				return null;
			}
			return new splitMatches_constantDelimiterMatches?(splitMatches_constantDelimiterMatches.CreateUnsafe(node));
		}

		// Token: 0x06009786 RID: 38790 RVA: 0x00205705 File Offset: 0x00203905
		public splitMatches_constantDelimiterMatches(GrammarBuilders g, constantDelimiterMatches value0)
		{
			this._node = g.UnnamedConversion.splitMatches_constantDelimiterMatches.BuildASTNode(value0.Node);
		}

		// Token: 0x06009787 RID: 38791 RVA: 0x00205724 File Offset: 0x00203924
		public static implicit operator splitMatches(splitMatches_constantDelimiterMatches arg)
		{
			return splitMatches.CreateUnsafe(arg.Node);
		}

		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x06009788 RID: 38792 RVA: 0x00205732 File Offset: 0x00203932
		public constantDelimiterMatches constantDelimiterMatches
		{
			get
			{
				return constantDelimiterMatches.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06009789 RID: 38793 RVA: 0x00205746 File Offset: 0x00203946
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600978A RID: 38794 RVA: 0x0020575C File Offset: 0x0020395C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600978B RID: 38795 RVA: 0x00205786 File Offset: 0x00203986
		public bool Equals(splitMatches_constantDelimiterMatches other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DB0 RID: 15792
		private ProgramNode _node;
	}
}
