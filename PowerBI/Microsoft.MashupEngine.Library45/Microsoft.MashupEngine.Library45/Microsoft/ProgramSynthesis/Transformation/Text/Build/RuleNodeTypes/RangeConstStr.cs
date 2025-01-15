using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C09 RID: 7177
	public struct RangeConstStr : IProgramNodeBuilder, IEquatable<RangeConstStr>
	{
		// Token: 0x17002846 RID: 10310
		// (get) Token: 0x0600F16D RID: 61805 RVA: 0x0033F92E File Offset: 0x0033DB2E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F16E RID: 61806 RVA: 0x0033F936 File Offset: 0x0033DB36
		private RangeConstStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F16F RID: 61807 RVA: 0x0033F93F File Offset: 0x0033DB3F
		public static RangeConstStr CreateUnsafe(ProgramNode node)
		{
			return new RangeConstStr(node);
		}

		// Token: 0x0600F170 RID: 61808 RVA: 0x0033F948 File Offset: 0x0033DB48
		public static RangeConstStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RangeConstStr)
			{
				return null;
			}
			return new RangeConstStr?(RangeConstStr.CreateUnsafe(node));
		}

		// Token: 0x0600F171 RID: 61809 RVA: 0x0033F97D File Offset: 0x0033DB7D
		public RangeConstStr(GrammarBuilders g, s value0)
		{
			this._node = g.Rule.RangeConstStr.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F172 RID: 61810 RVA: 0x0033F99C File Offset: 0x0033DB9C
		public static implicit operator rangeSubstring(RangeConstStr arg)
		{
			return rangeSubstring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002847 RID: 10311
		// (get) Token: 0x0600F173 RID: 61811 RVA: 0x0033F9AA File Offset: 0x0033DBAA
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F174 RID: 61812 RVA: 0x0033F9BE File Offset: 0x0033DBBE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F175 RID: 61813 RVA: 0x0033F9D4 File Offset: 0x0033DBD4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F176 RID: 61814 RVA: 0x0033F9FE File Offset: 0x0033DBFE
		public bool Equals(RangeConstStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AF8 RID: 23288
		private ProgramNode _node;
	}
}
