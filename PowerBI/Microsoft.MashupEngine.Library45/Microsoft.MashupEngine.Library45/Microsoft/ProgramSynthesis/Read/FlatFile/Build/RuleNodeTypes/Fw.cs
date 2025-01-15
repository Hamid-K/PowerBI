using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes
{
	// Token: 0x0200127A RID: 4730
	public struct Fw : IProgramNodeBuilder, IEquatable<Fw>
	{
		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06008EE1 RID: 36577 RVA: 0x001E1B0E File Offset: 0x001DFD0E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008EE2 RID: 36578 RVA: 0x001E1B16 File Offset: 0x001DFD16
		private Fw(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008EE3 RID: 36579 RVA: 0x001E1B1F File Offset: 0x001DFD1F
		public static Fw CreateUnsafe(ProgramNode node)
		{
			return new Fw(node);
		}

		// Token: 0x06008EE4 RID: 36580 RVA: 0x001E1B28 File Offset: 0x001DFD28
		public static Fw? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Fw)
			{
				return null;
			}
			return new Fw?(Fw.CreateUnsafe(node));
		}

		// Token: 0x06008EE5 RID: 36581 RVA: 0x001E1B60 File Offset: 0x001DFD60
		public Fw(GrammarBuilders g, file value0, columnNames value1, skip value2, skipFooter value3, fieldPositions value4, filterEmptyLines value5, commentStr value6)
		{
			this._node = g.Rule.Fw.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node, value4.Node, value5.Node, value6.Node });
		}

		// Token: 0x06008EE6 RID: 36582 RVA: 0x001E1BCF File Offset: 0x001DFDCF
		public static implicit operator readFlatFile(Fw arg)
		{
			return readFlatFile.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06008EE7 RID: 36583 RVA: 0x001E1BDD File Offset: 0x001DFDDD
		public file file
		{
			get
			{
				return file.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06008EE8 RID: 36584 RVA: 0x001E1BF1 File Offset: 0x001DFDF1
		public columnNames columnNames
		{
			get
			{
				return columnNames.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06008EE9 RID: 36585 RVA: 0x001E1C05 File Offset: 0x001DFE05
		public skip skip
		{
			get
			{
				return skip.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06008EEA RID: 36586 RVA: 0x001E1C19 File Offset: 0x001DFE19
		public skipFooter skipFooter
		{
			get
			{
				return skipFooter.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06008EEB RID: 36587 RVA: 0x001E1C2D File Offset: 0x001DFE2D
		public fieldPositions fieldPositions
		{
			get
			{
				return fieldPositions.CreateUnsafe(this.Node.Children[4]);
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x06008EEC RID: 36588 RVA: 0x001E1C41 File Offset: 0x001DFE41
		public filterEmptyLines filterEmptyLines
		{
			get
			{
				return filterEmptyLines.CreateUnsafe(this.Node.Children[5]);
			}
		}

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x06008EED RID: 36589 RVA: 0x001E1C55 File Offset: 0x001DFE55
		public commentStr commentStr
		{
			get
			{
				return commentStr.CreateUnsafe(this.Node.Children[6]);
			}
		}

		// Token: 0x06008EEE RID: 36590 RVA: 0x001E1C69 File Offset: 0x001DFE69
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008EEF RID: 36591 RVA: 0x001E1C7C File Offset: 0x001DFE7C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008EF0 RID: 36592 RVA: 0x001E1CA6 File Offset: 0x001DFEA6
		public bool Equals(Fw other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A6B RID: 14955
		private ProgramNode _node;
	}
}
