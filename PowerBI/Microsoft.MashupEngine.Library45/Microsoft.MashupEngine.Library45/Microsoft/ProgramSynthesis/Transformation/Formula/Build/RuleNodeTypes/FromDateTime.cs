using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001592 RID: 5522
	public struct FromDateTime : IProgramNodeBuilder, IEquatable<FromDateTime>
	{
		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x0600B4ED RID: 46317 RVA: 0x00275852 File Offset: 0x00273A52
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B4EE RID: 46318 RVA: 0x0027585A File Offset: 0x00273A5A
		private FromDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B4EF RID: 46319 RVA: 0x00275863 File Offset: 0x00273A63
		public static FromDateTime CreateUnsafe(ProgramNode node)
		{
			return new FromDateTime(node);
		}

		// Token: 0x0600B4F0 RID: 46320 RVA: 0x0027586C File Offset: 0x00273A6C
		public static FromDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FromDateTime)
			{
				return null;
			}
			return new FromDateTime?(FromDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600B4F1 RID: 46321 RVA: 0x002758A1 File Offset: 0x00273AA1
		public FromDateTime(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.FromDateTime.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B4F2 RID: 46322 RVA: 0x002758C7 File Offset: 0x00273AC7
		public static implicit operator fromDateTime(FromDateTime arg)
		{
			return fromDateTime.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FAD RID: 8109
		// (get) Token: 0x0600B4F3 RID: 46323 RVA: 0x002758D5 File Offset: 0x00273AD5
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x0600B4F4 RID: 46324 RVA: 0x002758E9 File Offset: 0x00273AE9
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B4F5 RID: 46325 RVA: 0x002758FD File Offset: 0x00273AFD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B4F6 RID: 46326 RVA: 0x00275910 File Offset: 0x00273B10
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B4F7 RID: 46327 RVA: 0x0027593A File Offset: 0x00273B3A
		public bool Equals(FromDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004640 RID: 17984
		private ProgramNode _node;
	}
}
