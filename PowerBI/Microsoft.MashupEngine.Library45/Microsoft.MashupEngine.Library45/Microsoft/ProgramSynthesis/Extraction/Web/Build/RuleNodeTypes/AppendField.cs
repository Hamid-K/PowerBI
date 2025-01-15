using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001031 RID: 4145
	public struct AppendField : IProgramNodeBuilder, IEquatable<AppendField>
	{
		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06007A90 RID: 31376 RVA: 0x001A204A File Offset: 0x001A024A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A91 RID: 31377 RVA: 0x001A2052 File Offset: 0x001A0252
		private AppendField(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A92 RID: 31378 RVA: 0x001A205B File Offset: 0x001A025B
		public static AppendField CreateUnsafe(ProgramNode node)
		{
			return new AppendField(node);
		}

		// Token: 0x06007A93 RID: 31379 RVA: 0x001A2064 File Offset: 0x001A0264
		public static AppendField? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.AppendField)
			{
				return null;
			}
			return new AppendField?(AppendField.CreateUnsafe(node));
		}

		// Token: 0x06007A94 RID: 31380 RVA: 0x001A2099 File Offset: 0x001A0299
		public AppendField(GrammarBuilders g, resultFields value0, singletonField value1)
		{
			this._node = g.Rule.AppendField.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007A95 RID: 31381 RVA: 0x001A20BF File Offset: 0x001A02BF
		public static implicit operator resultFields(AppendField arg)
		{
			return resultFields.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06007A96 RID: 31382 RVA: 0x001A20CD File Offset: 0x001A02CD
		public resultFields resultFields
		{
			get
			{
				return resultFields.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06007A97 RID: 31383 RVA: 0x001A20E1 File Offset: 0x001A02E1
		public singletonField singletonField
		{
			get
			{
				return singletonField.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007A98 RID: 31384 RVA: 0x001A20F5 File Offset: 0x001A02F5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A99 RID: 31385 RVA: 0x001A2108 File Offset: 0x001A0308
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x001A2132 File Offset: 0x001A0332
		public bool Equals(AppendField other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400334A RID: 13130
		private ProgramNode _node;
	}
}
