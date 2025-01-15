using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A30 RID: 6704
	public struct TransformValue : IProgramNodeBuilder, IEquatable<TransformValue>
	{
		// Token: 0x170024EF RID: 9455
		// (get) Token: 0x0600DC54 RID: 56404 RVA: 0x002EEC46 File Offset: 0x002ECE46
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC55 RID: 56405 RVA: 0x002EEC4E File Offset: 0x002ECE4E
		private TransformValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC56 RID: 56406 RVA: 0x002EEC57 File Offset: 0x002ECE57
		public static TransformValue CreateUnsafe(ProgramNode node)
		{
			return new TransformValue(node);
		}

		// Token: 0x0600DC57 RID: 56407 RVA: 0x002EEC60 File Offset: 0x002ECE60
		public static TransformValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TransformValue)
			{
				return null;
			}
			return new TransformValue?(TransformValue.CreateUnsafe(node));
		}

		// Token: 0x0600DC58 RID: 56408 RVA: 0x002EEC95 File Offset: 0x002ECE95
		public TransformValue(GrammarBuilders g, transformLet value0)
		{
			this._node = g.Rule.TransformValue.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DC59 RID: 56409 RVA: 0x002EECB4 File Offset: 0x002ECEB4
		public static implicit operator transformValue(TransformValue arg)
		{
			return transformValue.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024F0 RID: 9456
		// (get) Token: 0x0600DC5A RID: 56410 RVA: 0x002EECC2 File Offset: 0x002ECEC2
		public transformLet transformLet
		{
			get
			{
				return transformLet.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DC5B RID: 56411 RVA: 0x002EECD6 File Offset: 0x002ECED6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC5C RID: 56412 RVA: 0x002EECEC File Offset: 0x002ECEEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC5D RID: 56413 RVA: 0x002EED16 File Offset: 0x002ECF16
		public bool Equals(TransformValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005421 RID: 21537
		private ProgramNode _node;
	}
}
