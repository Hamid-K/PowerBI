using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200094B RID: 2379
	public struct SplitFile : IProgramNodeBuilder, IEquatable<SplitFile>
	{
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x000ADF26 File Offset: 0x000AC126
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000ADF2E File Offset: 0x000AC12E
		private SplitFile(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000ADF37 File Offset: 0x000AC137
		public static SplitFile CreateUnsafe(ProgramNode node)
		{
			return new SplitFile(node);
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000ADF40 File Offset: 0x000AC140
		public static SplitFile? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitFile)
			{
				return null;
			}
			return new SplitFile?(SplitFile.CreateUnsafe(node));
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000ADF75 File Offset: 0x000AC175
		public SplitFile(GrammarBuilders g, file value0)
		{
			this._node = g.Rule.SplitFile.BuildASTNode(value0.Node);
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000ADF94 File Offset: 0x000AC194
		public static implicit operator _LetB0(SplitFile arg)
		{
			return _LetB0.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06003783 RID: 14211 RVA: 0x000ADFA2 File Offset: 0x000AC1A2
		public file file
		{
			get
			{
				return file.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000ADFB6 File Offset: 0x000AC1B6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000ADFCC File Offset: 0x000AC1CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000ADFF6 File Offset: 0x000AC1F6
		public bool Equals(SplitFile other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A6B RID: 6763
		private ProgramNode _node;
	}
}
