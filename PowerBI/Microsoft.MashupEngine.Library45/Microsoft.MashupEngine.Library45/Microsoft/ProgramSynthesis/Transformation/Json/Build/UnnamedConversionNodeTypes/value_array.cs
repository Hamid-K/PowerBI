using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001A1D RID: 6685
	public struct value_array : IProgramNodeBuilder, IEquatable<value_array>
	{
		// Token: 0x170024BF RID: 9407
		// (get) Token: 0x0600DB8C RID: 56204 RVA: 0x002EDA66 File Offset: 0x002EBC66
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DB8D RID: 56205 RVA: 0x002EDA6E File Offset: 0x002EBC6E
		private value_array(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DB8E RID: 56206 RVA: 0x002EDA77 File Offset: 0x002EBC77
		public static value_array CreateUnsafe(ProgramNode node)
		{
			return new value_array(node);
		}

		// Token: 0x0600DB8F RID: 56207 RVA: 0x002EDA80 File Offset: 0x002EBC80
		public static value_array? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.value_array)
			{
				return null;
			}
			return new value_array?(value_array.CreateUnsafe(node));
		}

		// Token: 0x0600DB90 RID: 56208 RVA: 0x002EDAB5 File Offset: 0x002EBCB5
		public value_array(GrammarBuilders g, array value0)
		{
			this._node = g.UnnamedConversion.value_array.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DB91 RID: 56209 RVA: 0x002EDAD4 File Offset: 0x002EBCD4
		public static implicit operator value(value_array arg)
		{
			return value.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024C0 RID: 9408
		// (get) Token: 0x0600DB92 RID: 56210 RVA: 0x002EDAE2 File Offset: 0x002EBCE2
		public array array
		{
			get
			{
				return array.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DB93 RID: 56211 RVA: 0x002EDAF6 File Offset: 0x002EBCF6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DB94 RID: 56212 RVA: 0x002EDB0C File Offset: 0x002EBD0C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DB95 RID: 56213 RVA: 0x002EDB36 File Offset: 0x002EBD36
		public bool Equals(value_array other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400540E RID: 21518
		private ProgramNode _node;
	}
}
