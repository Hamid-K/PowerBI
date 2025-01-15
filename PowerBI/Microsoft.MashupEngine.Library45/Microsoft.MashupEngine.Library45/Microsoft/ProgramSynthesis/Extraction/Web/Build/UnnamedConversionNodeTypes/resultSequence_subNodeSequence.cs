using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000FFD RID: 4093
	public struct resultSequence_subNodeSequence : IProgramNodeBuilder, IEquatable<resultSequence_subNodeSequence>
	{
		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06007871 RID: 30833 RVA: 0x0019EFC7 File Offset: 0x0019D1C7
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007872 RID: 30834 RVA: 0x0019EFCF File Offset: 0x0019D1CF
		private resultSequence_subNodeSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007873 RID: 30835 RVA: 0x0019EFD8 File Offset: 0x0019D1D8
		public static resultSequence_subNodeSequence CreateUnsafe(ProgramNode node)
		{
			return new resultSequence_subNodeSequence(node);
		}

		// Token: 0x06007874 RID: 30836 RVA: 0x0019EFE0 File Offset: 0x0019D1E0
		public static resultSequence_subNodeSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.resultSequence_subNodeSequence)
			{
				return null;
			}
			return new resultSequence_subNodeSequence?(resultSequence_subNodeSequence.CreateUnsafe(node));
		}

		// Token: 0x06007875 RID: 30837 RVA: 0x0019F015 File Offset: 0x0019D215
		public resultSequence_subNodeSequence(GrammarBuilders g, subNodeSequence value0)
		{
			this._node = g.UnnamedConversion.resultSequence_subNodeSequence.BuildASTNode(value0.Node);
		}

		// Token: 0x06007876 RID: 30838 RVA: 0x0019F034 File Offset: 0x0019D234
		public static implicit operator resultSequence(resultSequence_subNodeSequence arg)
		{
			return resultSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06007877 RID: 30839 RVA: 0x0019F042 File Offset: 0x0019D242
		public subNodeSequence subNodeSequence
		{
			get
			{
				return subNodeSequence.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007878 RID: 30840 RVA: 0x0019F056 File Offset: 0x0019D256
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007879 RID: 30841 RVA: 0x0019F06C File Offset: 0x0019D26C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600787A RID: 30842 RVA: 0x0019F096 File Offset: 0x0019D296
		public bool Equals(resultSequence_subNodeSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003316 RID: 13078
		private ProgramNode _node;
	}
}
