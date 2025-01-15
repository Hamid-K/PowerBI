using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200134D RID: 4941
	public struct ConstantDelimiterWithQuoting : IProgramNodeBuilder, IEquatable<ConstantDelimiterWithQuoting>
	{
		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x0600985E RID: 39006 RVA: 0x00206AB6 File Offset: 0x00204CB6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600985F RID: 39007 RVA: 0x00206ABE File Offset: 0x00204CBE
		private ConstantDelimiterWithQuoting(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009860 RID: 39008 RVA: 0x00206AC7 File Offset: 0x00204CC7
		public static ConstantDelimiterWithQuoting CreateUnsafe(ProgramNode node)
		{
			return new ConstantDelimiterWithQuoting(node);
		}

		// Token: 0x06009861 RID: 39009 RVA: 0x00206AD0 File Offset: 0x00204CD0
		public static ConstantDelimiterWithQuoting? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConstantDelimiterWithQuoting)
			{
				return null;
			}
			return new ConstantDelimiterWithQuoting?(ConstantDelimiterWithQuoting.CreateUnsafe(node));
		}

		// Token: 0x06009862 RID: 39010 RVA: 0x00206B05 File Offset: 0x00204D05
		public ConstantDelimiterWithQuoting(GrammarBuilders g, v value0, s value1, quotingConf value2)
		{
			this._node = g.Rule.ConstantDelimiterWithQuoting.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06009863 RID: 39011 RVA: 0x00206B32 File Offset: 0x00204D32
		public static implicit operator constantDelimiterMatches(ConstantDelimiterWithQuoting arg)
		{
			return constantDelimiterMatches.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x06009864 RID: 39012 RVA: 0x00206B40 File Offset: 0x00204D40
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x06009865 RID: 39013 RVA: 0x00206B54 File Offset: 0x00204D54
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x06009866 RID: 39014 RVA: 0x00206B68 File Offset: 0x00204D68
		public quotingConf quotingConf
		{
			get
			{
				return quotingConf.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06009867 RID: 39015 RVA: 0x00206B7C File Offset: 0x00204D7C
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009868 RID: 39016 RVA: 0x00206B90 File Offset: 0x00204D90
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009869 RID: 39017 RVA: 0x00206BBA File Offset: 0x00204DBA
		public bool Equals(ConstantDelimiterWithQuoting other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DC4 RID: 15812
		private ProgramNode _node;
	}
}
