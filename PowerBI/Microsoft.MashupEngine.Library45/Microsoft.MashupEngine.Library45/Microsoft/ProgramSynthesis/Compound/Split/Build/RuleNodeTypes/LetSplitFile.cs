using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200095B RID: 2395
	public struct LetSplitFile : IProgramNodeBuilder, IEquatable<LetSplitFile>
	{
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x000AEF82 File Offset: 0x000AD182
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000AEF8A File Offset: 0x000AD18A
		private LetSplitFile(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000AEF93 File Offset: 0x000AD193
		public static LetSplitFile CreateUnsafe(ProgramNode node)
		{
			return new LetSplitFile(node);
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000AEF9C File Offset: 0x000AD19C
		public static LetSplitFile? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetSplitFile)
			{
				return null;
			}
			return new LetSplitFile?(LetSplitFile.CreateUnsafe(node));
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000AEFD1 File Offset: 0x000AD1D1
		public LetSplitFile(GrammarBuilders g, _LetB0 value0, _LetB1 value1)
		{
			this._node = new LetNode(g.Rule.LetSplitFile, value0.Node, value1.Node);
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000AEFF7 File Offset: 0x000AD1F7
		public static implicit operator splitFile(LetSplitFile arg)
		{
			return splitFile.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000AF005 File Offset: 0x000AD205
		public _LetB0 _LetB0
		{
			get
			{
				return _LetB0.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x000AF019 File Offset: 0x000AD219
		public _LetB1 _LetB1
		{
			get
			{
				return _LetB1.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000AF02D File Offset: 0x000AD22D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000AF040 File Offset: 0x000AD240
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000AF06A File Offset: 0x000AD26A
		public bool Equals(LetSplitFile other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A7B RID: 6779
		private ProgramNode _node;
	}
}
