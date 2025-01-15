using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C20 RID: 7200
	public struct BuildNumberFormat : IProgramNodeBuilder, IEquatable<BuildNumberFormat>
	{
		// Token: 0x1700288B RID: 10379
		// (get) Token: 0x0600F26A RID: 62058 RVA: 0x00340FE2 File Offset: 0x0033F1E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F26B RID: 62059 RVA: 0x00340FEA File Offset: 0x0033F1EA
		private BuildNumberFormat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F26C RID: 62060 RVA: 0x00340FF3 File Offset: 0x0033F1F3
		public static BuildNumberFormat CreateUnsafe(ProgramNode node)
		{
			return new BuildNumberFormat(node);
		}

		// Token: 0x0600F26D RID: 62061 RVA: 0x00340FFC File Offset: 0x0033F1FC
		public static BuildNumberFormat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.BuildNumberFormat)
			{
				return null;
			}
			return new BuildNumberFormat?(BuildNumberFormat.CreateUnsafe(node));
		}

		// Token: 0x0600F26E RID: 62062 RVA: 0x00341034 File Offset: 0x0033F234
		public BuildNumberFormat(GrammarBuilders g, minTrailingZeros value0, maxTrailingZeros value1, minTrailingZerosAndWhitespace value2, minLeadingZeros value3, minLeadingZerosAndWhitespace value4, numberFormatDetails value5)
		{
			this._node = g.Rule.BuildNumberFormat.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node, value4.Node, value5.Node });
		}

		// Token: 0x0600F26F RID: 62063 RVA: 0x00341099 File Offset: 0x0033F299
		public static implicit operator numberFormat(BuildNumberFormat arg)
		{
			return numberFormat.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700288C RID: 10380
		// (get) Token: 0x0600F270 RID: 62064 RVA: 0x003410A7 File Offset: 0x0033F2A7
		public minTrailingZeros minTrailingZeros
		{
			get
			{
				return minTrailingZeros.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700288D RID: 10381
		// (get) Token: 0x0600F271 RID: 62065 RVA: 0x003410BB File Offset: 0x0033F2BB
		public maxTrailingZeros maxTrailingZeros
		{
			get
			{
				return maxTrailingZeros.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x1700288E RID: 10382
		// (get) Token: 0x0600F272 RID: 62066 RVA: 0x003410CF File Offset: 0x0033F2CF
		public minTrailingZerosAndWhitespace minTrailingZerosAndWhitespace
		{
			get
			{
				return minTrailingZerosAndWhitespace.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x1700288F RID: 10383
		// (get) Token: 0x0600F273 RID: 62067 RVA: 0x003410E3 File Offset: 0x0033F2E3
		public minLeadingZeros minLeadingZeros
		{
			get
			{
				return minLeadingZeros.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x17002890 RID: 10384
		// (get) Token: 0x0600F274 RID: 62068 RVA: 0x003410F7 File Offset: 0x0033F2F7
		public minLeadingZerosAndWhitespace minLeadingZerosAndWhitespace
		{
			get
			{
				return minLeadingZerosAndWhitespace.CreateUnsafe(this.Node.Children[4]);
			}
		}

		// Token: 0x17002891 RID: 10385
		// (get) Token: 0x0600F275 RID: 62069 RVA: 0x0034110B File Offset: 0x0033F30B
		public numberFormatDetails numberFormatDetails
		{
			get
			{
				return numberFormatDetails.CreateUnsafe(this.Node.Children[5]);
			}
		}

		// Token: 0x0600F276 RID: 62070 RVA: 0x0034111F File Offset: 0x0033F31F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F277 RID: 62071 RVA: 0x00341134 File Offset: 0x0033F334
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F278 RID: 62072 RVA: 0x0034115E File Offset: 0x0033F35E
		public bool Equals(BuildNumberFormat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B0F RID: 23311
		private ProgramNode _node;
	}
}
