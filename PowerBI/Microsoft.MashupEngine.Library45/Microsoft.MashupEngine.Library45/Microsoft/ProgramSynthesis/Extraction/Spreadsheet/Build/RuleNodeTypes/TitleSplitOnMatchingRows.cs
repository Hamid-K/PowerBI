using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E46 RID: 3654
	public struct TitleSplitOnMatchingRows : IProgramNodeBuilder, IEquatable<TitleSplitOnMatchingRows>
	{
		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06006204 RID: 25092 RVA: 0x0013FF3A File Offset: 0x0013E13A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006205 RID: 25093 RVA: 0x0013FF42 File Offset: 0x0013E142
		private TitleSplitOnMatchingRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006206 RID: 25094 RVA: 0x0013FF4B File Offset: 0x0013E14B
		public static TitleSplitOnMatchingRows CreateUnsafe(ProgramNode node)
		{
			return new TitleSplitOnMatchingRows(node);
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x0013FF54 File Offset: 0x0013E154
		public static TitleSplitOnMatchingRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TitleSplitOnMatchingRows)
			{
				return null;
			}
			return new TitleSplitOnMatchingRows?(TitleSplitOnMatchingRows.CreateUnsafe(node));
		}

		// Token: 0x06006208 RID: 25096 RVA: 0x0013FF89 File Offset: 0x0013E189
		public TitleSplitOnMatchingRows(GrammarBuilders g, titleOf value0, styleFilter value1, splitMode value2)
		{
			this._node = g.Rule.TitleSplitOnMatchingRows.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x0013FFB6 File Offset: 0x0013E1B6
		public static implicit operator splitForTitle(TitleSplitOnMatchingRows arg)
		{
			return splitForTitle.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x0600620A RID: 25098 RVA: 0x0013FFC4 File Offset: 0x0013E1C4
		public titleOf titleOf
		{
			get
			{
				return titleOf.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x0600620B RID: 25099 RVA: 0x0013FFD8 File Offset: 0x0013E1D8
		public styleFilter styleFilter
		{
			get
			{
				return styleFilter.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x0600620C RID: 25100 RVA: 0x0013FFEC File Offset: 0x0013E1EC
		public splitMode splitMode
		{
			get
			{
				return splitMode.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x00140000 File Offset: 0x0013E200
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x00140014 File Offset: 0x0013E214
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600620F RID: 25103 RVA: 0x0014003E File Offset: 0x0013E23E
		public bool Equals(TitleSplitOnMatchingRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BF0 RID: 11248
		private ProgramNode _node;
	}
}
