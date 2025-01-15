using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001553 RID: 5459
	public struct UpperCaseConcat : IProgramNodeBuilder, IEquatable<UpperCaseConcat>
	{
		// Token: 0x17001EF0 RID: 7920
		// (get) Token: 0x0600B239 RID: 45625 RVA: 0x002719BE File Offset: 0x0026FBBE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B23A RID: 45626 RVA: 0x002719C6 File Offset: 0x0026FBC6
		private UpperCaseConcat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B23B RID: 45627 RVA: 0x002719CF File Offset: 0x0026FBCF
		public static UpperCaseConcat CreateUnsafe(ProgramNode node)
		{
			return new UpperCaseConcat(node);
		}

		// Token: 0x0600B23C RID: 45628 RVA: 0x002719D8 File Offset: 0x0026FBD8
		public static UpperCaseConcat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.UpperCaseConcat)
			{
				return null;
			}
			return new UpperCaseConcat?(UpperCaseConcat.CreateUnsafe(node));
		}

		// Token: 0x0600B23D RID: 45629 RVA: 0x00271A0D File Offset: 0x0026FC0D
		public UpperCaseConcat(GrammarBuilders g, concat value0)
		{
			this._node = g.Rule.UpperCaseConcat.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B23E RID: 45630 RVA: 0x00271A2C File Offset: 0x0026FC2C
		public static implicit operator concatCase(UpperCaseConcat arg)
		{
			return concatCase.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EF1 RID: 7921
		// (get) Token: 0x0600B23F RID: 45631 RVA: 0x00271A3A File Offset: 0x0026FC3A
		public concat concat
		{
			get
			{
				return concat.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B240 RID: 45632 RVA: 0x00271A4E File Offset: 0x0026FC4E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B241 RID: 45633 RVA: 0x00271A64 File Offset: 0x0026FC64
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B242 RID: 45634 RVA: 0x00271A8E File Offset: 0x0026FC8E
		public bool Equals(UpperCaseConcat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004601 RID: 17921
		private ProgramNode _node;
	}
}
