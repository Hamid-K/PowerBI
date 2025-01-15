using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001021 RID: 4129
	public struct SingleSelection5 : IProgramNodeBuilder, IEquatable<SingleSelection5>
	{
		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x060079E0 RID: 31200 RVA: 0x001A107E File Offset: 0x0019F27E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060079E1 RID: 31201 RVA: 0x001A1086 File Offset: 0x0019F286
		private SingleSelection5(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060079E2 RID: 31202 RVA: 0x001A108F File Offset: 0x0019F28F
		public static SingleSelection5 CreateUnsafe(ProgramNode node)
		{
			return new SingleSelection5(node);
		}

		// Token: 0x060079E3 RID: 31203 RVA: 0x001A1098 File Offset: 0x0019F298
		public static SingleSelection5? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SingleSelection5)
			{
				return null;
			}
			return new SingleSelection5?(SingleSelection5.CreateUnsafe(node));
		}

		// Token: 0x060079E4 RID: 31204 RVA: 0x001A10CD File Offset: 0x0019F2CD
		public SingleSelection5(GrammarBuilders g, filterSelection5 value0)
		{
			this._node = g.Rule.SingleSelection5.BuildASTNode(value0.Node);
		}

		// Token: 0x060079E5 RID: 31205 RVA: 0x001A10EC File Offset: 0x0019F2EC
		public static implicit operator selection9(SingleSelection5 arg)
		{
			return selection9.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x060079E6 RID: 31206 RVA: 0x001A10FA File Offset: 0x0019F2FA
		public filterSelection5 filterSelection5
		{
			get
			{
				return filterSelection5.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060079E7 RID: 31207 RVA: 0x001A110E File Offset: 0x0019F30E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060079E8 RID: 31208 RVA: 0x001A1124 File Offset: 0x0019F324
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060079E9 RID: 31209 RVA: 0x001A114E File Offset: 0x0019F34E
		public bool Equals(SingleSelection5 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400333A RID: 13114
		private ProgramNode _node;
	}
}
