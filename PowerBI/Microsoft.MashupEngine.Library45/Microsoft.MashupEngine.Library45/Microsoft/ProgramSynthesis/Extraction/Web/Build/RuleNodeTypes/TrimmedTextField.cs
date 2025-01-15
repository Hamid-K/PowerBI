using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001032 RID: 4146
	public struct TrimmedTextField : IProgramNodeBuilder, IEquatable<TrimmedTextField>
	{
		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06007A9B RID: 31387 RVA: 0x001A2146 File Offset: 0x001A0346
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A9C RID: 31388 RVA: 0x001A214E File Offset: 0x001A034E
		private TrimmedTextField(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A9D RID: 31389 RVA: 0x001A2157 File Offset: 0x001A0357
		public static TrimmedTextField CreateUnsafe(ProgramNode node)
		{
			return new TrimmedTextField(node);
		}

		// Token: 0x06007A9E RID: 31390 RVA: 0x001A2160 File Offset: 0x001A0360
		public static TrimmedTextField? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimmedTextField)
			{
				return null;
			}
			return new TrimmedTextField?(TrimmedTextField.CreateUnsafe(node));
		}

		// Token: 0x06007A9F RID: 31391 RVA: 0x001A2195 File Offset: 0x001A0395
		public TrimmedTextField(GrammarBuilders g, resultRegion value0)
		{
			this._node = g.Rule.TrimmedTextField.BuildASTNode(value0.Node);
		}

		// Token: 0x06007AA0 RID: 31392 RVA: 0x001A21B4 File Offset: 0x001A03B4
		public static implicit operator singletonField(TrimmedTextField arg)
		{
			return singletonField.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06007AA1 RID: 31393 RVA: 0x001A21C2 File Offset: 0x001A03C2
		public resultRegion resultRegion
		{
			get
			{
				return resultRegion.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007AA2 RID: 31394 RVA: 0x001A21D6 File Offset: 0x001A03D6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007AA3 RID: 31395 RVA: 0x001A21EC File Offset: 0x001A03EC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007AA4 RID: 31396 RVA: 0x001A2216 File Offset: 0x001A0416
		public bool Equals(TrimmedTextField other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400334B RID: 13131
		private ProgramNode _node;
	}
}
