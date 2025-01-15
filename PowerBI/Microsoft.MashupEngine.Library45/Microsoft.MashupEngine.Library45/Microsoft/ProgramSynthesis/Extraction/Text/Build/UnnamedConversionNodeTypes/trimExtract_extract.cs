using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000F1E RID: 3870
	public struct trimExtract_extract : IProgramNodeBuilder, IEquatable<trimExtract_extract>
	{
		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06006AF2 RID: 27378 RVA: 0x001605C9 File Offset: 0x0015E7C9
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006AF3 RID: 27379 RVA: 0x001605D1 File Offset: 0x0015E7D1
		private trimExtract_extract(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006AF4 RID: 27380 RVA: 0x001605DA File Offset: 0x0015E7DA
		public static trimExtract_extract CreateUnsafe(ProgramNode node)
		{
			return new trimExtract_extract(node);
		}

		// Token: 0x06006AF5 RID: 27381 RVA: 0x001605E4 File Offset: 0x0015E7E4
		public static trimExtract_extract? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.trimExtract_extract)
			{
				return null;
			}
			return new trimExtract_extract?(trimExtract_extract.CreateUnsafe(node));
		}

		// Token: 0x06006AF6 RID: 27382 RVA: 0x00160619 File Offset: 0x0015E819
		public trimExtract_extract(GrammarBuilders g, extract value0)
		{
			this._node = g.UnnamedConversion.trimExtract_extract.BuildASTNode(value0.Node);
		}

		// Token: 0x06006AF7 RID: 27383 RVA: 0x00160638 File Offset: 0x0015E838
		public static implicit operator trimExtract(trimExtract_extract arg)
		{
			return trimExtract.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06006AF8 RID: 27384 RVA: 0x00160646 File Offset: 0x0015E846
		public extract extract
		{
			get
			{
				return extract.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006AF9 RID: 27385 RVA: 0x0016065A File Offset: 0x0015E85A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006AFA RID: 27386 RVA: 0x00160670 File Offset: 0x0015E870
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006AFB RID: 27387 RVA: 0x0016069A File Offset: 0x0015E89A
		public bool Equals(trimExtract_extract other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F09 RID: 12041
		private ProgramNode _node;
	}
}
