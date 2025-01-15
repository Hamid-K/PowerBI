using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A21 RID: 6689
	public struct _Value : IProgramNodeBuilder, IEquatable<_Value>
	{
		// Token: 0x170024C7 RID: 9415
		// (get) Token: 0x0600DBB4 RID: 56244 RVA: 0x002EDDF6 File Offset: 0x002EBFF6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DBB5 RID: 56245 RVA: 0x002EDDFE File Offset: 0x002EBFFE
		private _Value(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DBB6 RID: 56246 RVA: 0x002EDE07 File Offset: 0x002EC007
		public static _Value CreateUnsafe(ProgramNode node)
		{
			return new _Value(node);
		}

		// Token: 0x0600DBB7 RID: 56247 RVA: 0x002EDE10 File Offset: 0x002EC010
		public static _Value? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule._Value)
			{
				return null;
			}
			return new _Value?(_Value.CreateUnsafe(node));
		}

		// Token: 0x0600DBB8 RID: 56248 RVA: 0x002EDE45 File Offset: 0x002EC045
		public _Value(GrammarBuilders g, selectKey value0)
		{
			this._node = g.Rule._Value.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DBB9 RID: 56249 RVA: 0x002EDE64 File Offset: 0x002EC064
		public static implicit operator value(_Value arg)
		{
			return value.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024C8 RID: 9416
		// (get) Token: 0x0600DBBA RID: 56250 RVA: 0x002EDE72 File Offset: 0x002EC072
		public selectKey selectKey
		{
			get
			{
				return selectKey.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DBBB RID: 56251 RVA: 0x002EDE86 File Offset: 0x002EC086
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DBBC RID: 56252 RVA: 0x002EDE9C File Offset: 0x002EC09C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DBBD RID: 56253 RVA: 0x002EDEC6 File Offset: 0x002EC0C6
		public bool Equals(_Value other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005412 RID: 21522
		private ProgramNode _node;
	}
}
