using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A24 RID: 6692
	public struct Append : IProgramNodeBuilder, IEquatable<Append>
	{
		// Token: 0x170024CD RID: 9421
		// (get) Token: 0x0600DBD2 RID: 56274 RVA: 0x002EE0A2 File Offset: 0x002EC2A2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DBD3 RID: 56275 RVA: 0x002EE0AA File Offset: 0x002EC2AA
		private Append(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DBD4 RID: 56276 RVA: 0x002EE0B3 File Offset: 0x002EC2B3
		public static Append CreateUnsafe(ProgramNode node)
		{
			return new Append(node);
		}

		// Token: 0x0600DBD5 RID: 56277 RVA: 0x002EE0BC File Offset: 0x002EC2BC
		public static Append? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Append)
			{
				return null;
			}
			return new Append?(Append.CreateUnsafe(node));
		}

		// Token: 0x0600DBD6 RID: 56278 RVA: 0x002EE0F1 File Offset: 0x002EC2F1
		public Append(GrammarBuilders g, property value0, @object value1)
		{
			this._node = g.Rule.Append.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600DBD7 RID: 56279 RVA: 0x002EE117 File Offset: 0x002EC317
		public static implicit operator @object(Append arg)
		{
			return @object.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024CE RID: 9422
		// (get) Token: 0x0600DBD8 RID: 56280 RVA: 0x002EE125 File Offset: 0x002EC325
		public property property
		{
			get
			{
				return property.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024CF RID: 9423
		// (get) Token: 0x0600DBD9 RID: 56281 RVA: 0x002EE139 File Offset: 0x002EC339
		public @object @object
		{
			get
			{
				return @object.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DBDA RID: 56282 RVA: 0x002EE14D File Offset: 0x002EC34D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DBDB RID: 56283 RVA: 0x002EE160 File Offset: 0x002EC360
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DBDC RID: 56284 RVA: 0x002EE18A File Offset: 0x002EC38A
		public bool Equals(Append other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005415 RID: 21525
		private ProgramNode _node;
	}
}
