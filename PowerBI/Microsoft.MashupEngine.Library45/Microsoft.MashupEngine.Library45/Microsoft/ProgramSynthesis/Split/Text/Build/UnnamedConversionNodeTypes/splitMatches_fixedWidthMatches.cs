using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200133A RID: 4922
	public struct splitMatches_fixedWidthMatches : IProgramNodeBuilder, IEquatable<splitMatches_fixedWidthMatches>
	{
		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x0600978C RID: 38796 RVA: 0x0020579A File Offset: 0x0020399A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600978D RID: 38797 RVA: 0x002057A2 File Offset: 0x002039A2
		private splitMatches_fixedWidthMatches(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600978E RID: 38798 RVA: 0x002057AB File Offset: 0x002039AB
		public static splitMatches_fixedWidthMatches CreateUnsafe(ProgramNode node)
		{
			return new splitMatches_fixedWidthMatches(node);
		}

		// Token: 0x0600978F RID: 38799 RVA: 0x002057B4 File Offset: 0x002039B4
		public static splitMatches_fixedWidthMatches? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.splitMatches_fixedWidthMatches)
			{
				return null;
			}
			return new splitMatches_fixedWidthMatches?(splitMatches_fixedWidthMatches.CreateUnsafe(node));
		}

		// Token: 0x06009790 RID: 38800 RVA: 0x002057E9 File Offset: 0x002039E9
		public splitMatches_fixedWidthMatches(GrammarBuilders g, fixedWidthMatches value0)
		{
			this._node = g.UnnamedConversion.splitMatches_fixedWidthMatches.BuildASTNode(value0.Node);
		}

		// Token: 0x06009791 RID: 38801 RVA: 0x00205808 File Offset: 0x00203A08
		public static implicit operator splitMatches(splitMatches_fixedWidthMatches arg)
		{
			return splitMatches.CreateUnsafe(arg.Node);
		}

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x06009792 RID: 38802 RVA: 0x00205816 File Offset: 0x00203A16
		public fixedWidthMatches fixedWidthMatches
		{
			get
			{
				return fixedWidthMatches.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06009793 RID: 38803 RVA: 0x0020582A File Offset: 0x00203A2A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009794 RID: 38804 RVA: 0x00205840 File Offset: 0x00203A40
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009795 RID: 38805 RVA: 0x0020586A File Offset: 0x00203A6A
		public bool Equals(splitMatches_fixedWidthMatches other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DB1 RID: 15793
		private ProgramNode _node;
	}
}
