using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F25 RID: 3877
	public struct List : IProgramNodeBuilder, IEquatable<List>
	{
		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06006B3A RID: 27450 RVA: 0x00160C36 File Offset: 0x0015EE36
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B3B RID: 27451 RVA: 0x00160C3E File Offset: 0x0015EE3E
		private List(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B3C RID: 27452 RVA: 0x00160C47 File Offset: 0x0015EE47
		public static List CreateUnsafe(ProgramNode node)
		{
			return new List(node);
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x00160C50 File Offset: 0x0015EE50
		public static List? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.List)
			{
				return null;
			}
			return new List?(List.CreateUnsafe(node));
		}

		// Token: 0x06006B3E RID: 27454 RVA: 0x00160C85 File Offset: 0x0015EE85
		public List(GrammarBuilders g, trimExtract value0)
		{
			this._node = g.Rule.List.BuildASTNode(value0.Node);
		}

		// Token: 0x06006B3F RID: 27455 RVA: 0x00160CA4 File Offset: 0x0015EEA4
		public static implicit operator colSplit(List arg)
		{
			return colSplit.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06006B40 RID: 27456 RVA: 0x00160CB2 File Offset: 0x0015EEB2
		public trimExtract trimExtract
		{
			get
			{
				return trimExtract.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006B41 RID: 27457 RVA: 0x00160CC6 File Offset: 0x0015EEC6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B42 RID: 27458 RVA: 0x00160CDC File Offset: 0x0015EEDC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B43 RID: 27459 RVA: 0x00160D06 File Offset: 0x0015EF06
		public bool Equals(List other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F10 RID: 12048
		private ProgramNode _node;
	}
}
