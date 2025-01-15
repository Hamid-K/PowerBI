using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200094C RID: 2380
	public struct MergeRecordLines : IProgramNodeBuilder, IEquatable<MergeRecordLines>
	{
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06003787 RID: 14215 RVA: 0x000AE00A File Offset: 0x000AC20A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000AE012 File Offset: 0x000AC212
		private MergeRecordLines(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000AE01B File Offset: 0x000AC21B
		public static MergeRecordLines CreateUnsafe(ProgramNode node)
		{
			return new MergeRecordLines(node);
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000AE024 File Offset: 0x000AC224
		public static MergeRecordLines? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MergeRecordLines)
			{
				return null;
			}
			return new MergeRecordLines?(MergeRecordLines.CreateUnsafe(node));
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000AE059 File Offset: 0x000AC259
		public MergeRecordLines(GrammarBuilders g, splitLines value0)
		{
			this._node = g.Rule.MergeRecordLines.BuildASTNode(value0.Node);
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000AE078 File Offset: 0x000AC278
		public static implicit operator _LetB1(MergeRecordLines arg)
		{
			return _LetB1.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x0600378D RID: 14221 RVA: 0x000AE086 File Offset: 0x000AC286
		public splitLines splitLines
		{
			get
			{
				return splitLines.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000AE09A File Offset: 0x000AC29A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000AE0B0 File Offset: 0x000AC2B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000AE0DA File Offset: 0x000AC2DA
		public bool Equals(MergeRecordLines other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A6C RID: 6764
		private ProgramNode _node;
	}
}
