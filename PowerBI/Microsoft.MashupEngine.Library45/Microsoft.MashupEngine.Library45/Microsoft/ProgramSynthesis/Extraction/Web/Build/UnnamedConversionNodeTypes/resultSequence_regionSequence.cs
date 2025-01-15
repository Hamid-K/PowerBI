using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000FFE RID: 4094
	public struct resultSequence_regionSequence : IProgramNodeBuilder, IEquatable<resultSequence_regionSequence>
	{
		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x0600787B RID: 30843 RVA: 0x0019F0AA File Offset: 0x0019D2AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600787C RID: 30844 RVA: 0x0019F0B2 File Offset: 0x0019D2B2
		private resultSequence_regionSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600787D RID: 30845 RVA: 0x0019F0BB File Offset: 0x0019D2BB
		public static resultSequence_regionSequence CreateUnsafe(ProgramNode node)
		{
			return new resultSequence_regionSequence(node);
		}

		// Token: 0x0600787E RID: 30846 RVA: 0x0019F0C4 File Offset: 0x0019D2C4
		public static resultSequence_regionSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.resultSequence_regionSequence)
			{
				return null;
			}
			return new resultSequence_regionSequence?(resultSequence_regionSequence.CreateUnsafe(node));
		}

		// Token: 0x0600787F RID: 30847 RVA: 0x0019F0F9 File Offset: 0x0019D2F9
		public resultSequence_regionSequence(GrammarBuilders g, regionSequence value0)
		{
			this._node = g.UnnamedConversion.resultSequence_regionSequence.BuildASTNode(value0.Node);
		}

		// Token: 0x06007880 RID: 30848 RVA: 0x0019F118 File Offset: 0x0019D318
		public static implicit operator resultSequence(resultSequence_regionSequence arg)
		{
			return resultSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06007881 RID: 30849 RVA: 0x0019F126 File Offset: 0x0019D326
		public regionSequence regionSequence
		{
			get
			{
				return regionSequence.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007882 RID: 30850 RVA: 0x0019F13A File Offset: 0x0019D33A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007883 RID: 30851 RVA: 0x0019F150 File Offset: 0x0019D350
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007884 RID: 30852 RVA: 0x0019F17A File Offset: 0x0019D37A
		public bool Equals(resultSequence_regionSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003317 RID: 13079
		private ProgramNode _node;
	}
}
