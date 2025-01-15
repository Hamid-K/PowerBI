using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200102F RID: 4143
	public struct HasStyle : IProgramNodeBuilder, IEquatable<HasStyle>
	{
		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06007A78 RID: 31352 RVA: 0x001A1E1A File Offset: 0x001A001A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A79 RID: 31353 RVA: 0x001A1E22 File Offset: 0x001A0022
		private HasStyle(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A7A RID: 31354 RVA: 0x001A1E2B File Offset: 0x001A002B
		public static HasStyle CreateUnsafe(ProgramNode node)
		{
			return new HasStyle(node);
		}

		// Token: 0x06007A7B RID: 31355 RVA: 0x001A1E34 File Offset: 0x001A0034
		public static HasStyle? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.HasStyle)
			{
				return null;
			}
			return new HasStyle?(HasStyle.CreateUnsafe(node));
		}

		// Token: 0x06007A7C RID: 31356 RVA: 0x001A1E69 File Offset: 0x001A0069
		public HasStyle(GrammarBuilders g, name value0, value value1, node value2)
		{
			this._node = g.Rule.HasStyle.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06007A7D RID: 31357 RVA: 0x001A1E96 File Offset: 0x001A0096
		public static implicit operator atomExpr(HasStyle arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06007A7E RID: 31358 RVA: 0x001A1EA4 File Offset: 0x001A00A4
		public name name
		{
			get
			{
				return name.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06007A7F RID: 31359 RVA: 0x001A1EB8 File Offset: 0x001A00B8
		public value value
		{
			get
			{
				return value.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06007A80 RID: 31360 RVA: 0x001A1ECC File Offset: 0x001A00CC
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06007A81 RID: 31361 RVA: 0x001A1EE0 File Offset: 0x001A00E0
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A82 RID: 31362 RVA: 0x001A1EF4 File Offset: 0x001A00F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A83 RID: 31363 RVA: 0x001A1F1E File Offset: 0x001A011E
		public bool Equals(HasStyle other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003348 RID: 13128
		private ProgramNode _node;
	}
}
