using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F2A RID: 3882
	public struct Slice : IProgramNodeBuilder, IEquatable<Slice>
	{
		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06006B70 RID: 27504 RVA: 0x00161112 File Offset: 0x0015F312
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B71 RID: 27505 RVA: 0x0016111A File Offset: 0x0015F31A
		private Slice(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B72 RID: 27506 RVA: 0x00161123 File Offset: 0x0015F323
		public static Slice CreateUnsafe(ProgramNode node)
		{
			return new Slice(node);
		}

		// Token: 0x06006B73 RID: 27507 RVA: 0x0016112C File Offset: 0x0015F32C
		public static Slice? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Slice)
			{
				return null;
			}
			return new Slice?(Slice.CreateUnsafe(node));
		}

		// Token: 0x06006B74 RID: 27508 RVA: 0x00161161 File Offset: 0x0015F361
		public Slice(GrammarBuilders g, row value0, k value1, k value2)
		{
			this._node = g.Rule.Slice.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06006B75 RID: 27509 RVA: 0x0016118E File Offset: 0x0015F38E
		public static implicit operator extract(Slice arg)
		{
			return extract.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06006B76 RID: 27510 RVA: 0x0016119C File Offset: 0x0015F39C
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06006B77 RID: 27511 RVA: 0x001611B0 File Offset: 0x0015F3B0
		public k k1
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06006B78 RID: 27512 RVA: 0x001611C4 File Offset: 0x0015F3C4
		public k k2
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06006B79 RID: 27513 RVA: 0x001611D8 File Offset: 0x0015F3D8
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B7A RID: 27514 RVA: 0x001611EC File Offset: 0x0015F3EC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B7B RID: 27515 RVA: 0x00161216 File Offset: 0x0015F416
		public bool Equals(Slice other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F15 RID: 12053
		private ProgramNode _node;
	}
}
