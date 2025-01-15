using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes
{
	// Token: 0x02001279 RID: 4729
	public struct Csv : IProgramNodeBuilder, IEquatable<Csv>
	{
		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x06008ECE RID: 36558 RVA: 0x001E1903 File Offset: 0x001DFB03
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008ECF RID: 36559 RVA: 0x001E190B File Offset: 0x001DFB0B
		private Csv(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008ED0 RID: 36560 RVA: 0x001E1914 File Offset: 0x001DFB14
		public static Csv CreateUnsafe(ProgramNode node)
		{
			return new Csv(node);
		}

		// Token: 0x06008ED1 RID: 36561 RVA: 0x001E191C File Offset: 0x001DFB1C
		public static Csv? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Csv)
			{
				return null;
			}
			return new Csv?(Csv.CreateUnsafe(node));
		}

		// Token: 0x06008ED2 RID: 36562 RVA: 0x001E1954 File Offset: 0x001DFB54
		public Csv(GrammarBuilders g, file value0, columnNames value1, skip value2, skipFooter value3, delimiter value4, filterEmptyLines value5, commentStr value6, quoteChar value7, escapeChar value8, doubleQuote value9)
		{
			this._node = g.Rule.Csv.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node, value4.Node, value5.Node, value6.Node, value7.Node, value8.Node, value9.Node });
		}

		// Token: 0x06008ED3 RID: 36563 RVA: 0x001E19E3 File Offset: 0x001DFBE3
		public static implicit operator readFlatFile(Csv arg)
		{
			return readFlatFile.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x06008ED4 RID: 36564 RVA: 0x001E19F1 File Offset: 0x001DFBF1
		public file file
		{
			get
			{
				return file.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x06008ED5 RID: 36565 RVA: 0x001E1A05 File Offset: 0x001DFC05
		public columnNames columnNames
		{
			get
			{
				return columnNames.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x06008ED6 RID: 36566 RVA: 0x001E1A19 File Offset: 0x001DFC19
		public skip skip
		{
			get
			{
				return skip.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x06008ED7 RID: 36567 RVA: 0x001E1A2D File Offset: 0x001DFC2D
		public skipFooter skipFooter
		{
			get
			{
				return skipFooter.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x06008ED8 RID: 36568 RVA: 0x001E1A41 File Offset: 0x001DFC41
		public delimiter delimiter
		{
			get
			{
				return delimiter.CreateUnsafe(this.Node.Children[4]);
			}
		}

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x06008ED9 RID: 36569 RVA: 0x001E1A55 File Offset: 0x001DFC55
		public filterEmptyLines filterEmptyLines
		{
			get
			{
				return filterEmptyLines.CreateUnsafe(this.Node.Children[5]);
			}
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x06008EDA RID: 36570 RVA: 0x001E1A69 File Offset: 0x001DFC69
		public commentStr commentStr
		{
			get
			{
				return commentStr.CreateUnsafe(this.Node.Children[6]);
			}
		}

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x06008EDB RID: 36571 RVA: 0x001E1A7D File Offset: 0x001DFC7D
		public quoteChar quoteChar
		{
			get
			{
				return quoteChar.CreateUnsafe(this.Node.Children[7]);
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06008EDC RID: 36572 RVA: 0x001E1A91 File Offset: 0x001DFC91
		public escapeChar escapeChar
		{
			get
			{
				return escapeChar.CreateUnsafe(this.Node.Children[8]);
			}
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x06008EDD RID: 36573 RVA: 0x001E1AA5 File Offset: 0x001DFCA5
		public doubleQuote doubleQuote
		{
			get
			{
				return doubleQuote.CreateUnsafe(this.Node.Children[9]);
			}
		}

		// Token: 0x06008EDE RID: 36574 RVA: 0x001E1ABA File Offset: 0x001DFCBA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008EDF RID: 36575 RVA: 0x001E1AD0 File Offset: 0x001DFCD0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008EE0 RID: 36576 RVA: 0x001E1AFA File Offset: 0x001DFCFA
		public bool Equals(Csv other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A6A RID: 14954
		private ProgramNode _node;
	}
}
