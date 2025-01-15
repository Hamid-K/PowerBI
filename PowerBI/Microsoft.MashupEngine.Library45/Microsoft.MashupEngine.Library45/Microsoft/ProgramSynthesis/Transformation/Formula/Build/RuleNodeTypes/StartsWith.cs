using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200155A RID: 5466
	public struct StartsWith : IProgramNodeBuilder, IEquatable<StartsWith>
	{
		// Token: 0x17001F06 RID: 7942
		// (get) Token: 0x0600B287 RID: 45703 RVA: 0x002720E6 File Offset: 0x002702E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B288 RID: 45704 RVA: 0x002720EE File Offset: 0x002702EE
		private StartsWith(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B289 RID: 45705 RVA: 0x002720F7 File Offset: 0x002702F7
		public static StartsWith CreateUnsafe(ProgramNode node)
		{
			return new StartsWith(node);
		}

		// Token: 0x0600B28A RID: 45706 RVA: 0x00272100 File Offset: 0x00270300
		public static StartsWith? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.StartsWith)
			{
				return null;
			}
			return new StartsWith?(StartsWith.CreateUnsafe(node));
		}

		// Token: 0x0600B28B RID: 45707 RVA: 0x00272135 File Offset: 0x00270335
		public StartsWith(GrammarBuilders g, row value0, columnName value1, startsWithFindText value2)
		{
			this._node = g.Rule.StartsWith.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B28C RID: 45708 RVA: 0x00272162 File Offset: 0x00270362
		public static implicit operator condition(StartsWith arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F07 RID: 7943
		// (get) Token: 0x0600B28D RID: 45709 RVA: 0x00272170 File Offset: 0x00270370
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F08 RID: 7944
		// (get) Token: 0x0600B28E RID: 45710 RVA: 0x00272184 File Offset: 0x00270384
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F09 RID: 7945
		// (get) Token: 0x0600B28F RID: 45711 RVA: 0x00272198 File Offset: 0x00270398
		public startsWithFindText startsWithFindText
		{
			get
			{
				return startsWithFindText.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B290 RID: 45712 RVA: 0x002721AC File Offset: 0x002703AC
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B291 RID: 45713 RVA: 0x002721C0 File Offset: 0x002703C0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B292 RID: 45714 RVA: 0x002721EA File Offset: 0x002703EA
		public bool Equals(StartsWith other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004608 RID: 17928
		private ProgramNode _node;
	}
}
