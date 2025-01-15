using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A36 RID: 6710
	public struct SelectOrTransformValue : IProgramNodeBuilder, IEquatable<SelectOrTransformValue>
	{
		// Token: 0x170024FF RID: 9471
		// (get) Token: 0x0600DC94 RID: 56468 RVA: 0x002EF226 File Offset: 0x002ED426
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC95 RID: 56469 RVA: 0x002EF22E File Offset: 0x002ED42E
		private SelectOrTransformValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC96 RID: 56470 RVA: 0x002EF237 File Offset: 0x002ED437
		public static SelectOrTransformValue CreateUnsafe(ProgramNode node)
		{
			return new SelectOrTransformValue(node);
		}

		// Token: 0x0600DC97 RID: 56471 RVA: 0x002EF240 File Offset: 0x002ED440
		public static SelectOrTransformValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectOrTransformValue)
			{
				return null;
			}
			return new SelectOrTransformValue?(SelectOrTransformValue.CreateUnsafe(node));
		}

		// Token: 0x0600DC98 RID: 56472 RVA: 0x002EF275 File Offset: 0x002ED475
		public SelectOrTransformValue(GrammarBuilders g, selectOrTransformValue value0)
		{
			this._node = g.Rule.SelectOrTransformValue.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DC99 RID: 56473 RVA: 0x002EF294 File Offset: 0x002ED494
		public static implicit operator value(SelectOrTransformValue arg)
		{
			return value.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002500 RID: 9472
		// (get) Token: 0x0600DC9A RID: 56474 RVA: 0x002EF2A2 File Offset: 0x002ED4A2
		public selectOrTransformValue selectOrTransformValue
		{
			get
			{
				return selectOrTransformValue.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DC9B RID: 56475 RVA: 0x002EF2B6 File Offset: 0x002ED4B6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC9C RID: 56476 RVA: 0x002EF2CC File Offset: 0x002ED4CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC9D RID: 56477 RVA: 0x002EF2F6 File Offset: 0x002ED4F6
		public bool Equals(SelectOrTransformValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005427 RID: 21543
		private ProgramNode _node;
	}
}
