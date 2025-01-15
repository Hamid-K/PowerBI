using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E86 RID: 7814
	public struct singleRelChildList : IProgramNodeBuilder, IEquatable<singleRelChildList>
	{
		// Token: 0x17002BDF RID: 11231
		// (get) Token: 0x06010808 RID: 67592 RVA: 0x0038CF3E File Offset: 0x0038B13E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010809 RID: 67593 RVA: 0x0038CF46 File Offset: 0x0038B146
		private singleRelChildList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601080A RID: 67594 RVA: 0x0038CF4F File Offset: 0x0038B14F
		public static singleRelChildList CreateUnsafe(ProgramNode node)
		{
			return new singleRelChildList(node);
		}

		// Token: 0x0601080B RID: 67595 RVA: 0x0038CF58 File Offset: 0x0038B158
		public static singleRelChildList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.singleRelChildList)
			{
				return null;
			}
			return new singleRelChildList?(singleRelChildList.CreateUnsafe(node));
		}

		// Token: 0x0601080C RID: 67596 RVA: 0x0038CF92 File Offset: 0x0038B192
		public static singleRelChildList CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new singleRelChildList(new Hole(g.Symbol.singleRelChildList, holeId));
		}

		// Token: 0x0601080D RID: 67597 RVA: 0x0038CFAA File Offset: 0x0038B1AA
		public SinglePosList Cast_SinglePosList()
		{
			return SinglePosList.CreateUnsafe(this.Node);
		}

		// Token: 0x0601080E RID: 67598 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SinglePosList(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0601080F RID: 67599 RVA: 0x0038CFB7 File Offset: 0x0038B1B7
		public bool Is_SinglePosList(GrammarBuilders g, out SinglePosList value)
		{
			value = SinglePosList.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06010810 RID: 67600 RVA: 0x0038CFCB File Offset: 0x0038B1CB
		public SinglePosList? As_SinglePosList(GrammarBuilders g)
		{
			return new SinglePosList?(SinglePosList.CreateUnsafe(this.Node));
		}

		// Token: 0x06010811 RID: 67601 RVA: 0x0038CFDD File Offset: 0x0038B1DD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010812 RID: 67602 RVA: 0x0038CFF0 File Offset: 0x0038B1F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010813 RID: 67603 RVA: 0x0038D01A File Offset: 0x0038B21A
		public bool Equals(singleRelChildList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062C5 RID: 25285
		private ProgramNode _node;
	}
}
