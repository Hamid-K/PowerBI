using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001548 RID: 5448
	public struct If : IProgramNodeBuilder, IEquatable<If>
	{
		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x0600B1C7 RID: 45511 RVA: 0x00270F8A File Offset: 0x0026F18A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B1C8 RID: 45512 RVA: 0x00270F92 File Offset: 0x0026F192
		private If(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B1C9 RID: 45513 RVA: 0x00270F9B File Offset: 0x0026F19B
		public static If CreateUnsafe(ProgramNode node)
		{
			return new If(node);
		}

		// Token: 0x0600B1CA RID: 45514 RVA: 0x00270FA4 File Offset: 0x0026F1A4
		public static If? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.If)
			{
				return null;
			}
			return new If?(If.CreateUnsafe(node));
		}

		// Token: 0x0600B1CB RID: 45515 RVA: 0x00270FD9 File Offset: 0x0026F1D9
		public If(GrammarBuilders g, condition value0, result value1, result value2)
		{
			this._node = g.Rule.If.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B1CC RID: 45516 RVA: 0x00271006 File Offset: 0x0026F206
		public static implicit operator result(If arg)
		{
			return result.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001ED7 RID: 7895
		// (get) Token: 0x0600B1CD RID: 45517 RVA: 0x00271014 File Offset: 0x0026F214
		public condition condition
		{
			get
			{
				return condition.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001ED8 RID: 7896
		// (get) Token: 0x0600B1CE RID: 45518 RVA: 0x00271028 File Offset: 0x0026F228
		public result result1
		{
			get
			{
				return result.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001ED9 RID: 7897
		// (get) Token: 0x0600B1CF RID: 45519 RVA: 0x0027103C File Offset: 0x0026F23C
		public result result2
		{
			get
			{
				return result.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B1D0 RID: 45520 RVA: 0x00271050 File Offset: 0x0026F250
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B1D1 RID: 45521 RVA: 0x00271064 File Offset: 0x0026F264
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B1D2 RID: 45522 RVA: 0x0027108E File Offset: 0x0026F28E
		public bool Equals(If other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045F6 RID: 17910
		private ProgramNode _node;
	}
}
