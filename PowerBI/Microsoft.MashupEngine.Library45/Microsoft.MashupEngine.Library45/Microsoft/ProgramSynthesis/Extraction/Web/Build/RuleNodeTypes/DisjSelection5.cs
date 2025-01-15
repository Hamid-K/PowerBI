using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001022 RID: 4130
	public struct DisjSelection5 : IProgramNodeBuilder, IEquatable<DisjSelection5>
	{
		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x060079EA RID: 31210 RVA: 0x001A1162 File Offset: 0x0019F362
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060079EB RID: 31211 RVA: 0x001A116A File Offset: 0x0019F36A
		private DisjSelection5(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060079EC RID: 31212 RVA: 0x001A1173 File Offset: 0x0019F373
		public static DisjSelection5 CreateUnsafe(ProgramNode node)
		{
			return new DisjSelection5(node);
		}

		// Token: 0x060079ED RID: 31213 RVA: 0x001A117C File Offset: 0x0019F37C
		public static DisjSelection5? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DisjSelection5)
			{
				return null;
			}
			return new DisjSelection5?(DisjSelection5.CreateUnsafe(node));
		}

		// Token: 0x060079EE RID: 31214 RVA: 0x001A11B1 File Offset: 0x0019F3B1
		public DisjSelection5(GrammarBuilders g, selection9 value0, filterSelection5 value1)
		{
			this._node = g.Rule.DisjSelection5.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060079EF RID: 31215 RVA: 0x001A11D7 File Offset: 0x0019F3D7
		public static implicit operator selection9(DisjSelection5 arg)
		{
			return selection9.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x060079F0 RID: 31216 RVA: 0x001A11E5 File Offset: 0x0019F3E5
		public selection9 selection9
		{
			get
			{
				return selection9.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x060079F1 RID: 31217 RVA: 0x001A11F9 File Offset: 0x0019F3F9
		public filterSelection5 filterSelection5
		{
			get
			{
				return filterSelection5.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060079F2 RID: 31218 RVA: 0x001A120D File Offset: 0x0019F40D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060079F3 RID: 31219 RVA: 0x001A1220 File Offset: 0x0019F420
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060079F4 RID: 31220 RVA: 0x001A124A File Offset: 0x0019F44A
		public bool Equals(DisjSelection5 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400333B RID: 13115
		private ProgramNode _node;
	}
}
