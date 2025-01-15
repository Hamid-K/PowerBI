using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001A1E RID: 6686
	public struct array_selectArray : IProgramNodeBuilder, IEquatable<array_selectArray>
	{
		// Token: 0x170024C1 RID: 9409
		// (get) Token: 0x0600DB96 RID: 56214 RVA: 0x002EDB4A File Offset: 0x002EBD4A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DB97 RID: 56215 RVA: 0x002EDB52 File Offset: 0x002EBD52
		private array_selectArray(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DB98 RID: 56216 RVA: 0x002EDB5B File Offset: 0x002EBD5B
		public static array_selectArray CreateUnsafe(ProgramNode node)
		{
			return new array_selectArray(node);
		}

		// Token: 0x0600DB99 RID: 56217 RVA: 0x002EDB64 File Offset: 0x002EBD64
		public static array_selectArray? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.array_selectArray)
			{
				return null;
			}
			return new array_selectArray?(array_selectArray.CreateUnsafe(node));
		}

		// Token: 0x0600DB9A RID: 56218 RVA: 0x002EDB99 File Offset: 0x002EBD99
		public array_selectArray(GrammarBuilders g, selectArray value0)
		{
			this._node = g.UnnamedConversion.array_selectArray.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DB9B RID: 56219 RVA: 0x002EDBB8 File Offset: 0x002EBDB8
		public static implicit operator array(array_selectArray arg)
		{
			return array.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024C2 RID: 9410
		// (get) Token: 0x0600DB9C RID: 56220 RVA: 0x002EDBC6 File Offset: 0x002EBDC6
		public selectArray selectArray
		{
			get
			{
				return selectArray.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DB9D RID: 56221 RVA: 0x002EDBDA File Offset: 0x002EBDDA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DB9E RID: 56222 RVA: 0x002EDBF0 File Offset: 0x002EBDF0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DB9F RID: 56223 RVA: 0x002EDC1A File Offset: 0x002EBE1A
		public bool Equals(array_selectArray other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400540F RID: 21519
		private ProgramNode _node;
	}
}
