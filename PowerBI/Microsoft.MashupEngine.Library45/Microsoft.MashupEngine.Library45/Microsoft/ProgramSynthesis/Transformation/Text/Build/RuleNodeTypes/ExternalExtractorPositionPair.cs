using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C17 RID: 7191
	public struct ExternalExtractorPositionPair : IProgramNodeBuilder, IEquatable<ExternalExtractorPositionPair>
	{
		// Token: 0x1700286E RID: 10350
		// (get) Token: 0x0600F205 RID: 61957 RVA: 0x003406CA File Offset: 0x0033E8CA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F206 RID: 61958 RVA: 0x003406D2 File Offset: 0x0033E8D2
		private ExternalExtractorPositionPair(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F207 RID: 61959 RVA: 0x003406DB File Offset: 0x0033E8DB
		public static ExternalExtractorPositionPair CreateUnsafe(ProgramNode node)
		{
			return new ExternalExtractorPositionPair(node);
		}

		// Token: 0x0600F208 RID: 61960 RVA: 0x003406E4 File Offset: 0x0033E8E4
		public static ExternalExtractorPositionPair? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ExternalExtractorPositionPair)
			{
				return null;
			}
			return new ExternalExtractorPositionPair?(ExternalExtractorPositionPair.CreateUnsafe(node));
		}

		// Token: 0x0600F209 RID: 61961 RVA: 0x00340719 File Offset: 0x0033E919
		public ExternalExtractorPositionPair(GrammarBuilders g, x value0, externalExtractor value1, k value2)
		{
			this._node = g.Rule.ExternalExtractorPositionPair.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600F20A RID: 61962 RVA: 0x00340746 File Offset: 0x0033E946
		public static implicit operator PP(ExternalExtractorPositionPair arg)
		{
			return PP.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700286F RID: 10351
		// (get) Token: 0x0600F20B RID: 61963 RVA: 0x00340754 File Offset: 0x0033E954
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002870 RID: 10352
		// (get) Token: 0x0600F20C RID: 61964 RVA: 0x00340768 File Offset: 0x0033E968
		public externalExtractor externalExtractor
		{
			get
			{
				return externalExtractor.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002871 RID: 10353
		// (get) Token: 0x0600F20D RID: 61965 RVA: 0x0034077C File Offset: 0x0033E97C
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600F20E RID: 61966 RVA: 0x00340790 File Offset: 0x0033E990
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F20F RID: 61967 RVA: 0x003407A4 File Offset: 0x0033E9A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F210 RID: 61968 RVA: 0x003407CE File Offset: 0x0033E9CE
		public bool Equals(ExternalExtractorPositionPair other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B06 RID: 23302
		private ProgramNode _node;
	}
}
