using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C21 RID: 7201
	public struct PosPairRelative : IProgramNodeBuilder, IEquatable<PosPairRelative>
	{
		// Token: 0x17002892 RID: 10386
		// (get) Token: 0x0600F279 RID: 62073 RVA: 0x00341172 File Offset: 0x0033F372
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F27A RID: 62074 RVA: 0x0034117A File Offset: 0x0033F37A
		private PosPairRelative(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F27B RID: 62075 RVA: 0x00341183 File Offset: 0x0033F383
		public static PosPairRelative CreateUnsafe(ProgramNode node)
		{
			return new PosPairRelative(node);
		}

		// Token: 0x0600F27C RID: 62076 RVA: 0x0034118C File Offset: 0x0033F38C
		public static PosPairRelative? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.PosPairRelative)
			{
				return null;
			}
			return new PosPairRelative?(PosPairRelative.CreateUnsafe(node));
		}

		// Token: 0x0600F27D RID: 62077 RVA: 0x003411C1 File Offset: 0x0033F3C1
		public PosPairRelative(GrammarBuilders g, pl1 value0, pl2p value1)
		{
			this._node = g.Rule.PosPairRelative.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x0600F27E RID: 62078 RVA: 0x003411F3 File Offset: 0x0033F3F3
		public static implicit operator _LetB3(PosPairRelative arg)
		{
			return _LetB3.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002893 RID: 10387
		// (get) Token: 0x0600F27F RID: 62079 RVA: 0x00341201 File Offset: 0x0033F401
		public pl1 pl1
		{
			get
			{
				return pl1.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002894 RID: 10388
		// (get) Token: 0x0600F280 RID: 62080 RVA: 0x00341215 File Offset: 0x0033F415
		public pl2p pl2p
		{
			get
			{
				return pl2p.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F281 RID: 62081 RVA: 0x00341229 File Offset: 0x0033F429
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F282 RID: 62082 RVA: 0x0034123C File Offset: 0x0033F43C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F283 RID: 62083 RVA: 0x00341266 File Offset: 0x0033F466
		public bool Equals(PosPairRelative other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B10 RID: 23312
		private ProgramNode _node;
	}
}
