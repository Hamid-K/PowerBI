using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000949 RID: 2377
	public struct KeyValue : IProgramNodeBuilder, IEquatable<KeyValue>
	{
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x000ADD12 File Offset: 0x000ABF12
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000ADD1A File Offset: 0x000ABF1A
		private KeyValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000ADD23 File Offset: 0x000ABF23
		public static KeyValue CreateUnsafe(ProgramNode node)
		{
			return new KeyValue(node);
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000ADD2C File Offset: 0x000ABF2C
		public static KeyValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KeyValue)
			{
				return null;
			}
			return new KeyValue?(KeyValue.CreateUnsafe(node));
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000ADD61 File Offset: 0x000ABF61
		public KeyValue(GrammarBuilders g, key value0, sep value1, records value2)
		{
			this._node = g.Rule.KeyValue.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000ADD8E File Offset: 0x000ABF8E
		public static implicit operator primarySelector(KeyValue arg)
		{
			return primarySelector.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000ADD9C File Offset: 0x000ABF9C
		public key key
		{
			get
			{
				return key.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x000ADDB0 File Offset: 0x000ABFB0
		public sep sep
		{
			get
			{
				return sep.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x000ADDC4 File Offset: 0x000ABFC4
		public records records
		{
			get
			{
				return records.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000ADDD8 File Offset: 0x000ABFD8
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000ADDEC File Offset: 0x000ABFEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000ADE16 File Offset: 0x000AC016
		public bool Equals(KeyValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A69 RID: 6761
		private ProgramNode _node;
	}
}
