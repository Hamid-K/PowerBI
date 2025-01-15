using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001352 RID: 4946
	public struct FieldMatch : IProgramNodeBuilder, IEquatable<FieldMatch>
	{
		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x06009895 RID: 39061 RVA: 0x00206FA6 File Offset: 0x002051A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009896 RID: 39062 RVA: 0x00206FAE File Offset: 0x002051AE
		private FieldMatch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009897 RID: 39063 RVA: 0x00206FB7 File Offset: 0x002051B7
		public static FieldMatch CreateUnsafe(ProgramNode node)
		{
			return new FieldMatch(node);
		}

		// Token: 0x06009898 RID: 39064 RVA: 0x00206FC0 File Offset: 0x002051C0
		public static FieldMatch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FieldMatch)
			{
				return null;
			}
			return new FieldMatch?(FieldMatch.CreateUnsafe(node));
		}

		// Token: 0x06009899 RID: 39065 RVA: 0x00206FF5 File Offset: 0x002051F5
		public FieldMatch(GrammarBuilders g, v value0, fregex value1)
		{
			this._node = g.Rule.FieldMatch.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600989A RID: 39066 RVA: 0x0020701B File Offset: 0x0020521B
		public static implicit operator fieldMatch(FieldMatch arg)
		{
			return fieldMatch.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x0600989B RID: 39067 RVA: 0x00207029 File Offset: 0x00205229
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x0600989C RID: 39068 RVA: 0x0020703D File Offset: 0x0020523D
		public fregex fregex
		{
			get
			{
				return fregex.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600989D RID: 39069 RVA: 0x00207051 File Offset: 0x00205251
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600989E RID: 39070 RVA: 0x00207064 File Offset: 0x00205264
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600989F RID: 39071 RVA: 0x0020708E File Offset: 0x0020528E
		public bool Equals(FieldMatch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DC9 RID: 15817
		private ProgramNode _node;
	}
}
