using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C23 RID: 7203
	public struct RegexPair : IProgramNodeBuilder, IEquatable<RegexPair>
	{
		// Token: 0x17002898 RID: 10392
		// (get) Token: 0x0600F28F RID: 62095 RVA: 0x00341382 File Offset: 0x0033F582
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F290 RID: 62096 RVA: 0x0034138A File Offset: 0x0033F58A
		private RegexPair(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F291 RID: 62097 RVA: 0x00341393 File Offset: 0x0033F593
		public static RegexPair CreateUnsafe(ProgramNode node)
		{
			return new RegexPair(node);
		}

		// Token: 0x0600F292 RID: 62098 RVA: 0x0034139C File Offset: 0x0033F59C
		public static RegexPair? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RegexPair)
			{
				return null;
			}
			return new RegexPair?(RegexPair.CreateUnsafe(node));
		}

		// Token: 0x0600F293 RID: 62099 RVA: 0x003413D1 File Offset: 0x0033F5D1
		public RegexPair(GrammarBuilders g, r value0, r value1)
		{
			this._node = g.Rule.RegexPair.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x0600F294 RID: 62100 RVA: 0x00341403 File Offset: 0x0033F603
		public static implicit operator regexPair(RegexPair arg)
		{
			return regexPair.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002899 RID: 10393
		// (get) Token: 0x0600F295 RID: 62101 RVA: 0x00341411 File Offset: 0x0033F611
		public r r1
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700289A RID: 10394
		// (get) Token: 0x0600F296 RID: 62102 RVA: 0x00341425 File Offset: 0x0033F625
		public r r2
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F297 RID: 62103 RVA: 0x00341439 File Offset: 0x0033F639
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F298 RID: 62104 RVA: 0x0034144C File Offset: 0x0033F64C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F299 RID: 62105 RVA: 0x00341476 File Offset: 0x0033F676
		public bool Equals(RegexPair other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B12 RID: 23314
		private ProgramNode _node;
	}
}
