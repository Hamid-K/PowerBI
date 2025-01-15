using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001342 RID: 4930
	public struct EmptyDelimitersList : IProgramNodeBuilder, IEquatable<EmptyDelimitersList>
	{
		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x060097E7 RID: 38887 RVA: 0x00206006 File Offset: 0x00204206
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060097E8 RID: 38888 RVA: 0x0020600E File Offset: 0x0020420E
		private EmptyDelimitersList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060097E9 RID: 38889 RVA: 0x00206017 File Offset: 0x00204217
		public static EmptyDelimitersList CreateUnsafe(ProgramNode node)
		{
			return new EmptyDelimitersList(node);
		}

		// Token: 0x060097EA RID: 38890 RVA: 0x00206020 File Offset: 0x00204220
		public static EmptyDelimitersList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.EmptyDelimitersList)
			{
				return null;
			}
			return new EmptyDelimitersList?(EmptyDelimitersList.CreateUnsafe(node));
		}

		// Token: 0x060097EB RID: 38891 RVA: 0x00206055 File Offset: 0x00204255
		public EmptyDelimitersList(GrammarBuilders g)
		{
			this._node = g.Rule.EmptyDelimitersList.BuildASTNode(Array.Empty<ProgramNode>());
		}

		// Token: 0x060097EC RID: 38892 RVA: 0x00206072 File Offset: 0x00204272
		public static implicit operator delimiterList(EmptyDelimitersList arg)
		{
			return delimiterList.CreateUnsafe(arg.Node);
		}

		// Token: 0x060097ED RID: 38893 RVA: 0x00206080 File Offset: 0x00204280
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060097EE RID: 38894 RVA: 0x00206094 File Offset: 0x00204294
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060097EF RID: 38895 RVA: 0x002060BE File Offset: 0x002042BE
		public bool Equals(EmptyDelimitersList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DB9 RID: 15801
		private ProgramNode _node;
	}
}
