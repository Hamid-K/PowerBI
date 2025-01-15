using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001565 RID: 5477
	public struct Null : IProgramNodeBuilder, IEquatable<Null>
	{
		// Token: 0x17001F2E RID: 7982
		// (get) Token: 0x0600B307 RID: 45831 RVA: 0x00272C9E File Offset: 0x00270E9E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B308 RID: 45832 RVA: 0x00272CA6 File Offset: 0x00270EA6
		private Null(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B309 RID: 45833 RVA: 0x00272CAF File Offset: 0x00270EAF
		public static Null CreateUnsafe(ProgramNode node)
		{
			return new Null(node);
		}

		// Token: 0x0600B30A RID: 45834 RVA: 0x00272CB8 File Offset: 0x00270EB8
		public static Null? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Null)
			{
				return null;
			}
			return new Null?(Null.CreateUnsafe(node));
		}

		// Token: 0x0600B30B RID: 45835 RVA: 0x00272CED File Offset: 0x00270EED
		public Null(GrammarBuilders g)
		{
			this._node = g.Rule.Null.BuildASTNode(Array.Empty<ProgramNode>());
		}

		// Token: 0x0600B30C RID: 45836 RVA: 0x00272D0A File Offset: 0x00270F0A
		public static implicit operator inull(Null arg)
		{
			return inull.CreateUnsafe(arg.Node);
		}

		// Token: 0x0600B30D RID: 45837 RVA: 0x00272D18 File Offset: 0x00270F18
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B30E RID: 45838 RVA: 0x00272D2C File Offset: 0x00270F2C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B30F RID: 45839 RVA: 0x00272D56 File Offset: 0x00270F56
		public bool Equals(Null other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004613 RID: 17939
		private ProgramNode _node;
	}
}
