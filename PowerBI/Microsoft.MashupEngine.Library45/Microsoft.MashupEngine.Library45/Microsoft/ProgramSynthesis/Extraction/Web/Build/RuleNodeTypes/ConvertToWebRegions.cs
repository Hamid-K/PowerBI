using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200100C RID: 4108
	public struct ConvertToWebRegions : IProgramNodeBuilder, IEquatable<ConvertToWebRegions>
	{
		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06007907 RID: 30983 RVA: 0x0019FD22 File Offset: 0x0019DF22
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007908 RID: 30984 RVA: 0x0019FD2A File Offset: 0x0019DF2A
		private ConvertToWebRegions(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007909 RID: 30985 RVA: 0x0019FD33 File Offset: 0x0019DF33
		public static ConvertToWebRegions CreateUnsafe(ProgramNode node)
		{
			return new ConvertToWebRegions(node);
		}

		// Token: 0x0600790A RID: 30986 RVA: 0x0019FD3C File Offset: 0x0019DF3C
		public static ConvertToWebRegions? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConvertToWebRegions)
			{
				return null;
			}
			return new ConvertToWebRegions?(ConvertToWebRegions.CreateUnsafe(node));
		}

		// Token: 0x0600790B RID: 30987 RVA: 0x0019FD71 File Offset: 0x0019DF71
		public ConvertToWebRegions(GrammarBuilders g, nodeCollection value0)
		{
			this._node = g.Rule.ConvertToWebRegions.BuildASTNode(value0.Node);
		}

		// Token: 0x0600790C RID: 30988 RVA: 0x0019FD90 File Offset: 0x0019DF90
		public static implicit operator resultSequence(ConvertToWebRegions arg)
		{
			return resultSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x0600790D RID: 30989 RVA: 0x0019FD9E File Offset: 0x0019DF9E
		public nodeCollection nodeCollection
		{
			get
			{
				return nodeCollection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600790E RID: 30990 RVA: 0x0019FDB2 File Offset: 0x0019DFB2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600790F RID: 30991 RVA: 0x0019FDC8 File Offset: 0x0019DFC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007910 RID: 30992 RVA: 0x0019FDF2 File Offset: 0x0019DFF2
		public bool Equals(ConvertToWebRegions other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003325 RID: 13093
		private ProgramNode _node;
	}
}
