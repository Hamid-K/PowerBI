using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A39 RID: 6713
	public struct TransformLet : IProgramNodeBuilder, IEquatable<TransformLet>
	{
		// Token: 0x17002506 RID: 9478
		// (get) Token: 0x0600DCB3 RID: 56499 RVA: 0x002EF4EA File Offset: 0x002ED6EA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DCB4 RID: 56500 RVA: 0x002EF4F2 File Offset: 0x002ED6F2
		private TransformLet(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DCB5 RID: 56501 RVA: 0x002EF4FB File Offset: 0x002ED6FB
		public static TransformLet CreateUnsafe(ProgramNode node)
		{
			return new TransformLet(node);
		}

		// Token: 0x0600DCB6 RID: 56502 RVA: 0x002EF504 File Offset: 0x002ED704
		public static TransformLet? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TransformLet)
			{
				return null;
			}
			return new TransformLet?(TransformLet.CreateUnsafe(node));
		}

		// Token: 0x0600DCB7 RID: 56503 RVA: 0x002EF539 File Offset: 0x002ED739
		public TransformLet(GrammarBuilders g, _LetB0 value0, transformString value1)
		{
			this._node = new LetNode(g.Rule.TransformLet, value0.Node, value1.Node);
		}

		// Token: 0x0600DCB8 RID: 56504 RVA: 0x002EF55F File Offset: 0x002ED75F
		public static implicit operator transformLet(TransformLet arg)
		{
			return transformLet.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002507 RID: 9479
		// (get) Token: 0x0600DCB9 RID: 56505 RVA: 0x002EF56D File Offset: 0x002ED76D
		public _LetB0 _LetB0
		{
			get
			{
				return _LetB0.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002508 RID: 9480
		// (get) Token: 0x0600DCBA RID: 56506 RVA: 0x002EF581 File Offset: 0x002ED781
		public transformString transformString
		{
			get
			{
				return transformString.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DCBB RID: 56507 RVA: 0x002EF595 File Offset: 0x002ED795
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DCBC RID: 56508 RVA: 0x002EF5A8 File Offset: 0x002ED7A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DCBD RID: 56509 RVA: 0x002EF5D2 File Offset: 0x002ED7D2
		public bool Equals(TransformLet other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400542A RID: 21546
		private ProgramNode _node;
	}
}
