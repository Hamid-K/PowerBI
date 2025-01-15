using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000943 RID: 2371
	public struct KthKeyValue : IProgramNodeBuilder, IEquatable<KthKeyValue>
	{
		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600371B RID: 14107 RVA: 0x000AD5AA File Offset: 0x000AB7AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000AD5B2 File Offset: 0x000AB7B2
		private KthKeyValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000AD5BB File Offset: 0x000AB7BB
		public static KthKeyValue CreateUnsafe(ProgramNode node)
		{
			return new KthKeyValue(node);
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000AD5C4 File Offset: 0x000AB7C4
		public static KthKeyValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthKeyValue)
			{
				return null;
			}
			return new KthKeyValue?(KthKeyValue.CreateUnsafe(node));
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000AD5FC File Offset: 0x000AB7FC
		public KthKeyValue(GrammarBuilders g, key value0, sep value1, k value2, rowRecord value3)
		{
			this._node = g.Rule.KthKeyValue.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node });
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000AD64D File Offset: 0x000AB84D
		public static implicit operator columnSelector(KthKeyValue arg)
		{
			return columnSelector.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000AD65B File Offset: 0x000AB85B
		public key key
		{
			get
			{
				return key.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x000AD66F File Offset: 0x000AB86F
		public sep sep
		{
			get
			{
				return sep.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000AD683 File Offset: 0x000AB883
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06003724 RID: 14116 RVA: 0x000AD697 File Offset: 0x000AB897
		public rowRecord rowRecord
		{
			get
			{
				return rowRecord.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000AD6AB File Offset: 0x000AB8AB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000AD6C0 File Offset: 0x000AB8C0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000AD6EA File Offset: 0x000AB8EA
		public bool Equals(KthKeyValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A63 RID: 6755
		private ProgramNode _node;
	}
}
