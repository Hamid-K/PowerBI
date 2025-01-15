using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200154F RID: 5455
	public struct LowerCase : IProgramNodeBuilder, IEquatable<LowerCase>
	{
		// Token: 0x17001EE8 RID: 7912
		// (get) Token: 0x0600B211 RID: 45585 RVA: 0x0027162E File Offset: 0x0026F82E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B212 RID: 45586 RVA: 0x00271636 File Offset: 0x0026F836
		private LowerCase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B213 RID: 45587 RVA: 0x0027163F File Offset: 0x0026F83F
		public static LowerCase CreateUnsafe(ProgramNode node)
		{
			return new LowerCase(node);
		}

		// Token: 0x0600B214 RID: 45588 RVA: 0x00271648 File Offset: 0x0026F848
		public static LowerCase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LowerCase)
			{
				return null;
			}
			return new LowerCase?(LowerCase.CreateUnsafe(node));
		}

		// Token: 0x0600B215 RID: 45589 RVA: 0x0027167D File Offset: 0x0026F87D
		public LowerCase(GrammarBuilders g, segment value0)
		{
			this._node = g.Rule.LowerCase.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B216 RID: 45590 RVA: 0x0027169C File Offset: 0x0026F89C
		public static implicit operator segmentCase(LowerCase arg)
		{
			return segmentCase.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EE9 RID: 7913
		// (get) Token: 0x0600B217 RID: 45591 RVA: 0x002716AA File Offset: 0x0026F8AA
		public segment segment
		{
			get
			{
				return segment.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B218 RID: 45592 RVA: 0x002716BE File Offset: 0x0026F8BE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B219 RID: 45593 RVA: 0x002716D4 File Offset: 0x0026F8D4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B21A RID: 45594 RVA: 0x002716FE File Offset: 0x0026F8FE
		public bool Equals(LowerCase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045FD RID: 17917
		private ProgramNode _node;
	}
}
