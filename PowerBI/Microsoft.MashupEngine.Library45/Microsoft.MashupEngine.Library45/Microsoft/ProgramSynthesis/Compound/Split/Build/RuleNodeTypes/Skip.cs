using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200094E RID: 2382
	public struct Skip : IProgramNodeBuilder, IEquatable<Skip>
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000AE25E File Offset: 0x000AC45E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000AE266 File Offset: 0x000AC466
		private Skip(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000AE26F File Offset: 0x000AC46F
		public static Skip CreateUnsafe(ProgramNode node)
		{
			return new Skip(node);
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000AE278 File Offset: 0x000AC478
		public static Skip? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Skip)
			{
				return null;
			}
			return new Skip?(Skip.CreateUnsafe(node));
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000AE2AD File Offset: 0x000AC4AD
		public Skip(GrammarBuilders g, k value0, headerIndex value1, skippedFooter value2)
		{
			this._node = g.Rule.Skip.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000AE2DA File Offset: 0x000AC4DA
		public static implicit operator skippedRecords(Skip arg)
		{
			return skippedRecords.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x000AE2E8 File Offset: 0x000AC4E8
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x000AE2FC File Offset: 0x000AC4FC
		public headerIndex headerIndex
		{
			get
			{
				return headerIndex.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x000AE310 File Offset: 0x000AC510
		public skippedFooter skippedFooter
		{
			get
			{
				return skippedFooter.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000AE324 File Offset: 0x000AC524
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000AE338 File Offset: 0x000AC538
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000AE362 File Offset: 0x000AC562
		public bool Equals(Skip other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A6E RID: 6766
		private ProgramNode _node;
	}
}
