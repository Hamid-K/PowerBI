using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001A1C RID: 6684
	public struct value_object : IProgramNodeBuilder, IEquatable<value_object>
	{
		// Token: 0x170024BD RID: 9405
		// (get) Token: 0x0600DB82 RID: 56194 RVA: 0x002ED982 File Offset: 0x002EBB82
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DB83 RID: 56195 RVA: 0x002ED98A File Offset: 0x002EBB8A
		private value_object(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DB84 RID: 56196 RVA: 0x002ED993 File Offset: 0x002EBB93
		public static value_object CreateUnsafe(ProgramNode node)
		{
			return new value_object(node);
		}

		// Token: 0x0600DB85 RID: 56197 RVA: 0x002ED99C File Offset: 0x002EBB9C
		public static value_object? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.value_object)
			{
				return null;
			}
			return new value_object?(value_object.CreateUnsafe(node));
		}

		// Token: 0x0600DB86 RID: 56198 RVA: 0x002ED9D1 File Offset: 0x002EBBD1
		public value_object(GrammarBuilders g, @object value0)
		{
			this._node = g.UnnamedConversion.value_object.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DB87 RID: 56199 RVA: 0x002ED9F0 File Offset: 0x002EBBF0
		public static implicit operator value(value_object arg)
		{
			return value.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024BE RID: 9406
		// (get) Token: 0x0600DB88 RID: 56200 RVA: 0x002ED9FE File Offset: 0x002EBBFE
		public @object @object
		{
			get
			{
				return @object.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DB89 RID: 56201 RVA: 0x002EDA12 File Offset: 0x002EBC12
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DB8A RID: 56202 RVA: 0x002EDA28 File Offset: 0x002EBC28
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DB8B RID: 56203 RVA: 0x002EDA52 File Offset: 0x002EBC52
		public bool Equals(value_object other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400540D RID: 21517
		private ProgramNode _node;
	}
}
