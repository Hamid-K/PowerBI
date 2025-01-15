using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200019D RID: 413
	internal class SqlScriptGeneratorOptions
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x0015E96B File Offset: 0x0015CB6B
		// (set) Token: 0x060021A7 RID: 8615 RVA: 0x0015E973 File Offset: 0x0015CB73
		public KeywordCasing KeywordCasing
		{
			get
			{
				return this.keywordCasing;
			}
			set
			{
				this.keywordCasing = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x0015E97C File Offset: 0x0015CB7C
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x0015E984 File Offset: 0x0015CB84
		public SqlVersion SqlVersion
		{
			get
			{
				return this.sqlVersion;
			}
			set
			{
				this.sqlVersion = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x0015E98D File Offset: 0x0015CB8D
		// (set) Token: 0x060021AB RID: 8619 RVA: 0x0015E995 File Offset: 0x0015CB95
		public int IndentationSize
		{
			get
			{
				return this.indentationSize;
			}
			set
			{
				if (value < 0)
				{
					this.indentationSize = 0;
					return;
				}
				this.indentationSize = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x0015E9AA File Offset: 0x0015CBAA
		// (set) Token: 0x060021AD RID: 8621 RVA: 0x0015E9B2 File Offset: 0x0015CBB2
		public bool IncludeSemicolons
		{
			get
			{
				return this.includeSemicolons;
			}
			set
			{
				this.includeSemicolons = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x0015E9BB File Offset: 0x0015CBBB
		// (set) Token: 0x060021AF RID: 8623 RVA: 0x0015E9C3 File Offset: 0x0015CBC3
		public bool AlignColumnDefinitionFields
		{
			get
			{
				return this.alignColumnDefinitionFields;
			}
			set
			{
				this.alignColumnDefinitionFields = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x0015E9CC File Offset: 0x0015CBCC
		// (set) Token: 0x060021B1 RID: 8625 RVA: 0x0015E9D4 File Offset: 0x0015CBD4
		public bool NewLineBeforeFromClause
		{
			get
			{
				return this.newLineBeforeFromClause;
			}
			set
			{
				this.newLineBeforeFromClause = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x0015E9DD File Offset: 0x0015CBDD
		// (set) Token: 0x060021B3 RID: 8627 RVA: 0x0015E9E5 File Offset: 0x0015CBE5
		public bool NewLineBeforeWhereClause
		{
			get
			{
				return this.newLineBeforeWhereClause;
			}
			set
			{
				this.newLineBeforeWhereClause = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0015E9EE File Offset: 0x0015CBEE
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x0015E9F6 File Offset: 0x0015CBF6
		public bool NewLineBeforeGroupByClause
		{
			get
			{
				return this.newLineBeforeGroupByClause;
			}
			set
			{
				this.newLineBeforeGroupByClause = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x0015E9FF File Offset: 0x0015CBFF
		// (set) Token: 0x060021B7 RID: 8631 RVA: 0x0015EA07 File Offset: 0x0015CC07
		public bool NewLineBeforeOrderByClause
		{
			get
			{
				return this.newLineBeforeOrderByClause;
			}
			set
			{
				this.newLineBeforeOrderByClause = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x0015EA10 File Offset: 0x0015CC10
		// (set) Token: 0x060021B9 RID: 8633 RVA: 0x0015EA18 File Offset: 0x0015CC18
		public bool NewLineBeforeHavingClause
		{
			get
			{
				return this.newLineBeforeHavingClause;
			}
			set
			{
				this.newLineBeforeHavingClause = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x0015EA21 File Offset: 0x0015CC21
		// (set) Token: 0x060021BB RID: 8635 RVA: 0x0015EA29 File Offset: 0x0015CC29
		public bool NewLineBeforeJoinClause
		{
			get
			{
				return this.newLineBeforeJoinClause;
			}
			set
			{
				this.newLineBeforeJoinClause = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x0015EA32 File Offset: 0x0015CC32
		// (set) Token: 0x060021BD RID: 8637 RVA: 0x0015EA3A File Offset: 0x0015CC3A
		public bool NewLineBeforeOffsetClause
		{
			get
			{
				return this.newLineBeforeOffsetClause;
			}
			set
			{
				this.newLineBeforeOffsetClause = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x0015EA43 File Offset: 0x0015CC43
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x0015EA4B File Offset: 0x0015CC4B
		public bool NewLineBeforeOutputClause
		{
			get
			{
				return this.newLineBeforeOutputClause;
			}
			set
			{
				this.newLineBeforeOutputClause = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x0015EA54 File Offset: 0x0015CC54
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x0015EA5C File Offset: 0x0015CC5C
		public bool AlignClauseBodies
		{
			get
			{
				return this.alignClauseBodies;
			}
			set
			{
				this.alignClauseBodies = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x0015EA65 File Offset: 0x0015CC65
		// (set) Token: 0x060021C3 RID: 8643 RVA: 0x0015EA6D File Offset: 0x0015CC6D
		public bool MultilineSelectElementsList
		{
			get
			{
				return this.multilineSelectElementsList;
			}
			set
			{
				this.multilineSelectElementsList = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x0015EA76 File Offset: 0x0015CC76
		// (set) Token: 0x060021C5 RID: 8645 RVA: 0x0015EA7E File Offset: 0x0015CC7E
		public bool MultilineWherePredicatesList
		{
			get
			{
				return this.multilineWherePredicatesList;
			}
			set
			{
				this.multilineWherePredicatesList = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x0015EA87 File Offset: 0x0015CC87
		// (set) Token: 0x060021C7 RID: 8647 RVA: 0x0015EA8F File Offset: 0x0015CC8F
		public bool IndentViewBody
		{
			get
			{
				return this.indentViewBody;
			}
			set
			{
				this.indentViewBody = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x0015EA98 File Offset: 0x0015CC98
		// (set) Token: 0x060021C9 RID: 8649 RVA: 0x0015EAA0 File Offset: 0x0015CCA0
		public bool MultilineViewColumnsList
		{
			get
			{
				return this.multilineViewColumnsList;
			}
			set
			{
				this.multilineViewColumnsList = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x0015EAA9 File Offset: 0x0015CCA9
		// (set) Token: 0x060021CB RID: 8651 RVA: 0x0015EAB1 File Offset: 0x0015CCB1
		public bool AsKeywordOnOwnLine
		{
			get
			{
				return this.asKeywordOnOwnLine;
			}
			set
			{
				this.asKeywordOnOwnLine = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x0015EABA File Offset: 0x0015CCBA
		// (set) Token: 0x060021CD RID: 8653 RVA: 0x0015EAC2 File Offset: 0x0015CCC2
		public bool IndentSetClause
		{
			get
			{
				return this.indentSetClause;
			}
			set
			{
				this.indentSetClause = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x0015EACB File Offset: 0x0015CCCB
		// (set) Token: 0x060021CF RID: 8655 RVA: 0x0015EAD3 File Offset: 0x0015CCD3
		public bool AlignSetClauseItem
		{
			get
			{
				return this.alignSetClauseItem;
			}
			set
			{
				this.alignSetClauseItem = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x0015EADC File Offset: 0x0015CCDC
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x0015EAE4 File Offset: 0x0015CCE4
		public bool MultilineSetClauseItems
		{
			get
			{
				return this.multilineSetClauseItems;
			}
			set
			{
				this.multilineSetClauseItems = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0015EAED File Offset: 0x0015CCED
		// (set) Token: 0x060021D3 RID: 8659 RVA: 0x0015EAF5 File Offset: 0x0015CCF5
		public bool MultilineInsertTargetsList
		{
			get
			{
				return this.multilineInsertTargetsList;
			}
			set
			{
				this.multilineInsertTargetsList = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x0015EAFE File Offset: 0x0015CCFE
		// (set) Token: 0x060021D5 RID: 8661 RVA: 0x0015EB06 File Offset: 0x0015CD06
		public bool MultilineInsertSourcesList
		{
			get
			{
				return this.multilineInsertSourcesList;
			}
			set
			{
				this.multilineInsertSourcesList = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x0015EB0F File Offset: 0x0015CD0F
		// (set) Token: 0x060021D7 RID: 8663 RVA: 0x0015EB17 File Offset: 0x0015CD17
		public bool NewLineBeforeOpenParenthesisInMultilineList
		{
			get
			{
				return this.newLineBeforeOpenParenthesisInMultilineList;
			}
			set
			{
				this.newLineBeforeOpenParenthesisInMultilineList = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060021D8 RID: 8664 RVA: 0x0015EB20 File Offset: 0x0015CD20
		// (set) Token: 0x060021D9 RID: 8665 RVA: 0x0015EB28 File Offset: 0x0015CD28
		public bool NewLineBeforeCloseParenthesisInMultilineList
		{
			get
			{
				return this.newLineBeforeCloseParenthesisInMultilineList;
			}
			set
			{
				this.newLineBeforeCloseParenthesisInMultilineList = value;
			}
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0015EB34 File Offset: 0x0015CD34
		public void Reset()
		{
			this.KeywordCasing = KeywordCasing.Uppercase;
			this.SqlVersion = SqlVersion.Sql90;
			this.IndentationSize = 4;
			this.IncludeSemicolons = false;
			this.AlignColumnDefinitionFields = true;
			this.NewLineBeforeFromClause = true;
			this.NewLineBeforeWhereClause = true;
			this.NewLineBeforeGroupByClause = true;
			this.NewLineBeforeOrderByClause = true;
			this.NewLineBeforeHavingClause = true;
			this.NewLineBeforeJoinClause = true;
			this.NewLineBeforeOffsetClause = true;
			this.NewLineBeforeOutputClause = true;
			this.AlignClauseBodies = true;
			this.MultilineSelectElementsList = true;
			this.MultilineWherePredicatesList = true;
			this.IndentViewBody = false;
			this.MultilineViewColumnsList = true;
			this.AsKeywordOnOwnLine = true;
			this.IndentSetClause = false;
			this.AlignSetClauseItem = true;
			this.MultilineSetClauseItems = true;
			this.MultilineInsertTargetsList = true;
			this.MultilineInsertSourcesList = true;
			this.NewLineBeforeOpenParenthesisInMultilineList = false;
			this.NewLineBeforeCloseParenthesisInMultilineList = true;
		}

		// Token: 0x040019BC RID: 6588
		private const KeywordCasing DefaultKeywordCasing = KeywordCasing.Uppercase;

		// Token: 0x040019BD RID: 6589
		private const SqlVersion DefaultSqlVersion = SqlVersion.Sql90;

		// Token: 0x040019BE RID: 6590
		private const int DefaultIndentationSize = 4;

		// Token: 0x040019BF RID: 6591
		private const bool DefaultIncludeSemicolons = false;

		// Token: 0x040019C0 RID: 6592
		private const bool DefaultAlignColumnDefinitionFields = true;

		// Token: 0x040019C1 RID: 6593
		private const bool DefaultNewLineBeforeFromClause = true;

		// Token: 0x040019C2 RID: 6594
		private const bool DefaultNewLineBeforeWhereClause = true;

		// Token: 0x040019C3 RID: 6595
		private const bool DefaultNewLineBeforeGroupByClause = true;

		// Token: 0x040019C4 RID: 6596
		private const bool DefaultNewLineBeforeOrderByClause = true;

		// Token: 0x040019C5 RID: 6597
		private const bool DefaultNewLineBeforeHavingClause = true;

		// Token: 0x040019C6 RID: 6598
		private const bool DefaultNewLineBeforeJoinClause = true;

		// Token: 0x040019C7 RID: 6599
		private const bool DefaultNewLineBeforeOffsetClause = true;

		// Token: 0x040019C8 RID: 6600
		private const bool DefaultNewLineBeforeOutputClause = true;

		// Token: 0x040019C9 RID: 6601
		private const bool DefaultAlignClauseBodies = true;

		// Token: 0x040019CA RID: 6602
		private const bool DefaultMultilineSelectElementsList = true;

		// Token: 0x040019CB RID: 6603
		private const bool DefaultMultilineWherePredicatesList = true;

		// Token: 0x040019CC RID: 6604
		private const bool DefaultIndentViewBody = false;

		// Token: 0x040019CD RID: 6605
		private const bool DefaultMultilineViewColumnsList = true;

		// Token: 0x040019CE RID: 6606
		private const bool DefaultAsKeywordOnOwnLine = true;

		// Token: 0x040019CF RID: 6607
		private const bool DefaultIndentSetClause = false;

		// Token: 0x040019D0 RID: 6608
		private const bool DefaultAlignSetClauseItem = true;

		// Token: 0x040019D1 RID: 6609
		private const bool DefaultMultilineSetClauseItems = true;

		// Token: 0x040019D2 RID: 6610
		private const bool DefaultMultilineInsertTargetsList = true;

		// Token: 0x040019D3 RID: 6611
		private const bool DefaultMultilineInsertSourcesList = true;

		// Token: 0x040019D4 RID: 6612
		private const bool DefaultNewLineBeforeOpenParenthesisInMultilineList = false;

		// Token: 0x040019D5 RID: 6613
		private const bool DefaultNewLineBeforeCloseParenthesisInMultilineList = true;

		// Token: 0x040019D6 RID: 6614
		private const int MinIndentationSize = 0;

		// Token: 0x040019D7 RID: 6615
		private KeywordCasing keywordCasing = KeywordCasing.Uppercase;

		// Token: 0x040019D8 RID: 6616
		private SqlVersion sqlVersion;

		// Token: 0x040019D9 RID: 6617
		private int indentationSize = 4;

		// Token: 0x040019DA RID: 6618
		private bool includeSemicolons;

		// Token: 0x040019DB RID: 6619
		private bool alignColumnDefinitionFields = true;

		// Token: 0x040019DC RID: 6620
		private bool newLineBeforeFromClause = true;

		// Token: 0x040019DD RID: 6621
		private bool newLineBeforeWhereClause = true;

		// Token: 0x040019DE RID: 6622
		private bool newLineBeforeGroupByClause = true;

		// Token: 0x040019DF RID: 6623
		private bool newLineBeforeOrderByClause = true;

		// Token: 0x040019E0 RID: 6624
		private bool newLineBeforeHavingClause = true;

		// Token: 0x040019E1 RID: 6625
		private bool newLineBeforeJoinClause = true;

		// Token: 0x040019E2 RID: 6626
		private bool newLineBeforeOffsetClause = true;

		// Token: 0x040019E3 RID: 6627
		private bool newLineBeforeOutputClause = true;

		// Token: 0x040019E4 RID: 6628
		private bool alignClauseBodies = true;

		// Token: 0x040019E5 RID: 6629
		private bool multilineSelectElementsList = true;

		// Token: 0x040019E6 RID: 6630
		private bool multilineWherePredicatesList = true;

		// Token: 0x040019E7 RID: 6631
		private bool indentViewBody;

		// Token: 0x040019E8 RID: 6632
		private bool multilineViewColumnsList = true;

		// Token: 0x040019E9 RID: 6633
		private bool asKeywordOnOwnLine = true;

		// Token: 0x040019EA RID: 6634
		private bool indentSetClause;

		// Token: 0x040019EB RID: 6635
		private bool alignSetClauseItem = true;

		// Token: 0x040019EC RID: 6636
		private bool multilineSetClauseItems = true;

		// Token: 0x040019ED RID: 6637
		private bool multilineInsertTargetsList = true;

		// Token: 0x040019EE RID: 6638
		private bool multilineInsertSourcesList = true;

		// Token: 0x040019EF RID: 6639
		private bool newLineBeforeOpenParenthesisInMultilineList;

		// Token: 0x040019F0 RID: 6640
		private bool newLineBeforeCloseParenthesisInMultilineList = true;
	}
}
