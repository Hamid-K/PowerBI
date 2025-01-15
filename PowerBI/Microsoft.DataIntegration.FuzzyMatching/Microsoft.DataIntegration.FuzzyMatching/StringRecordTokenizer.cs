using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	public class StringRecordTokenizer : RecordTokenizerBase, IRecordTokenizer
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0002AC78 File Offset: 0x00028E78
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0002AC80 File Offset: 0x00028E80
		public int ExtendibleClasses
		{
			get
			{
				return this.m_extendibleClasses;
			}
			set
			{
				this.m_extendibleClasses = value;
				this.InitExtendibleBitArray();
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0002AC8F File Offset: 0x00028E8F
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x0002AC97 File Offset: 0x00028E97
		public Predicate<char> IsExtendible
		{
			get
			{
				return this.m_isExtendibleDelegate;
			}
			set
			{
				if (value != null)
				{
					this.m_isExtendibleDelegate = value;
				}
				else
				{
					this.m_isExtendibleDelegate = new Predicate<char>(this.IsExtendibleInternal);
				}
				this.InitExtendibleBitArray();
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0002ACBD File Offset: 0x00028EBD
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0002ACC5 File Offset: 0x00028EC5
		public bool TokenizeEachCharacter { get; set; }

		// Token: 0x0600092D RID: 2349 RVA: 0x0002ACCE File Offset: 0x00028ECE
		public StringRecordTokenizer()
		{
			this.IsExtendible = new Predicate<char>(this.IsExtendibleInternal);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0002AD00 File Offset: 0x00028F00
		public bool IsExtendibleInternal(char c)
		{
			return this.m_customNonExtendibles.IndexOf(c) == -1 && ((this.IsExtendibleClass(RecordTokenizerBase.CharClass2.WhiteSpace) && char.IsWhiteSpace(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.Punctuation) && char.IsPunctuation(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.Symbol) && char.IsSymbol(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.Letter) && char.IsLetter(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.Control) && char.IsControl(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.Separator) && char.IsSeparator(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.Digit) && char.IsDigit(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.HighSurrogate) && char.IsHighSurrogate(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.LowSurrogate) && char.IsLowSurrogate(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.Surrogate) && char.IsSurrogate(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.Upper) && char.IsUpper(c)) || (this.IsExtendibleClass(RecordTokenizerBase.CharClass2.Lower) && char.IsLower(c)) || this.m_customExtendibles.IndexOf(c) >= 0);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002AE2A File Offset: 0x0002902A
		private void InitExtendibleClasses()
		{
			this.m_extendibleClasses = 12;
			this.InitExtendibleBitArray();
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0002AE3A File Offset: 0x0002903A
		public bool IsExtendibleClass(RecordTokenizerBase.CharClass2 charClass)
		{
			return (this.m_extendibleClasses & (int)charClass) != 0;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0002AE47 File Offset: 0x00029047
		public bool IsExtendibleClass(RecordTokenizerBase.CharClass2 charClass, bool isExtendible)
		{
			if (isExtendible)
			{
				this.m_extendibleClasses |= (int)charClass;
			}
			else
			{
				this.m_extendibleClasses &= (int)(~(int)charClass);
			}
			this.InitExtendibleBitArray();
			return isExtendible;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0002AE74 File Offset: 0x00029074
		private void InitExtendibleBitArray()
		{
			this.m_isExtendibleBitArray = new BitArray(65536);
			for (int i = 0; i < 65535; i++)
			{
				if (this.m_isExtendibleDelegate.Invoke((char)i))
				{
					this.m_isExtendibleBitArray.Set(i, true);
				}
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0002AEC0 File Offset: 0x000290C0
		public virtual void Prepare(DataTable schemaTable, DomainBinding domainBinding, out TokenizerContext _context)
		{
			StringRecordTokenizer.Context context = new StringRecordTokenizer.Context();
			List<Column> columns = domainBinding.Columns;
			context.ColumnIndexes = new int[columns.Count];
			context.IsString = new bool[columns.Count];
			for (int i = 0; i < columns.Count; i++)
			{
				if (columns[i].Ordinal >= 0)
				{
					context.ColumnIndexes[i] = columns[i].Ordinal;
					if (schemaTable != null)
					{
						if (SchemaUtils.FindColumnSchemaRow(schemaTable, columns[i].Ordinal) == null)
						{
							throw new Exception(string.Format("Column with ordinal '{0}' not present.", columns[i].Ordinal));
						}
						if (columns[i].Type != null)
						{
							context.IsString[i] = columns[i].Type == typeof(string);
						}
					}
				}
				else
				{
					if (string.IsNullOrEmpty(domainBinding.Columns[i].Name))
					{
						throw new Exception(string.Format("Neither the column Name or column Ordinal was specified for a column in the domain binding for domain {0}", domainBinding.DomainName));
					}
					DataRow dataRow = SchemaUtils.FindColumnSchemaRow(schemaTable, columns[i].Name, true);
					Type type = (Type)dataRow[SchemaTableColumn.DataType];
					context.IsString[i] = type == typeof(string);
					context.ColumnIndexes[i] = (int)dataRow[SchemaTableColumn.ColumnOrdinal];
				}
			}
			_context = context;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0002B033 File Offset: 0x00029233
		public virtual IEnumerable<StringExtent> Tokenize(TokenizerContext tokenizerContext, IDataRecord record)
		{
			StringRecordTokenizer.Context context = (StringRecordTokenizer.Context)tokenizerContext;
			context.Reset();
			context.tokenizer = this;
			context.record = record;
			return context;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0002B050 File Offset: 0x00029250
		public void Tokenize(TokenizerContext context, ITokenIdProvider tokenIdProvider, int domainId, IDataRecord record, ArraySegmentBuilder<int> tokenSequence)
		{
			foreach (StringExtent stringExtent in this.Tokenize(context, record))
			{
				tokenSequence.Add(tokenIdProvider.GetOrCreateTokenId(stringExtent, domainId));
			}
		}

		// Token: 0x0400039A RID: 922
		private int m_extendibleClasses;

		// Token: 0x0400039B RID: 923
		private Predicate<char> m_isExtendibleDelegate;

		// Token: 0x0400039C RID: 924
		internal BitArray m_isExtendibleBitArray;

		// Token: 0x0400039D RID: 925
		private string m_customNonExtendibles = string.Empty;

		// Token: 0x0400039E RID: 926
		private string m_customExtendibles = string.Empty;

		// Token: 0x040003A0 RID: 928
		protected bool m_providerConvertsStringToChar;

		// Token: 0x02000185 RID: 389
		[Serializable]
		protected class Context : TokenizerContext, IEnumerable<StringExtent>, IEnumerable, IEnumerator<StringExtent>, IDisposable, IEnumerator
		{
			// Token: 0x06000D1E RID: 3358 RVA: 0x00038089 File Offset: 0x00036289
			public override void Reset()
			{
				this.m_charAllocator.Reset();
			}

			// Token: 0x1700026E RID: 622
			// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00038096 File Offset: 0x00036296
			StringExtent IEnumerator<StringExtent>.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700026F RID: 623
			// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0003809E File Offset: 0x0003629E
			object IEnumerator.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06000D21 RID: 3361 RVA: 0x000380AB File Offset: 0x000362AB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000D22 RID: 3362 RVA: 0x000380AD File Offset: 0x000362AD
			void IEnumerator.Reset()
			{
				this.i = 0;
				this.j = 0;
				this.Reset();
			}

			// Token: 0x06000D23 RID: 3363 RVA: 0x000380C4 File Offset: 0x000362C4
			bool IEnumerator.MoveNext()
			{
				while (this.i < this.ColumnIndexes.Length)
				{
					if (this.j == 0)
					{
						TokenizerCommon.GetString(this.record, this.ColumnIndexes[this.i], this.IsString[this.i], this.m_charAllocator, out this.sourceString, ref this.tokenizer.m_providerConvertsStringToChar);
						StringNormalization.NormalizeString(this.tokenizer.NormalizationOptions.FoldStringFlags, this.tokenizer.NormalizationOptions.MapStringFlags, this.sourceString, this.m_charAllocator, out this.sourceString);
					}
					while (this.j < this.sourceString.Count)
					{
						if (!this.tokenizer.m_isDelimiterBitArray[(int)this.sourceString.Array[this.sourceString.Offset + this.j]])
						{
							int num = this.j;
							if (this.tokenizer.TokenizeEachCharacter)
							{
								while (this.j < this.sourceString.Count && this.tokenizer.m_isExtendibleBitArray[(int)this.sourceString.Array[this.sourceString.Offset + this.j]])
								{
									this.j++;
								}
								this.j++;
							}
							else
							{
								int num2;
								do
								{
									num2 = this.j + 1;
									this.j = num2;
								}
								while (num2 < this.sourceString.Count && !this.tokenizer.m_isDelimiterBitArray[(int)this.sourceString.Array[this.sourceString.Offset + this.j]]);
							}
							int num3 = this.j - num;
							this.current = new StringExtent(this.sourceString.Array, this.sourceString.Offset + num, num3);
							this.j++;
							return true;
						}
						this.j++;
					}
					this.i++;
					this.j = 0;
				}
				this.current = default(StringExtent);
				return false;
			}

			// Token: 0x06000D24 RID: 3364 RVA: 0x000382E9 File Offset: 0x000364E9
			IEnumerator<StringExtent> IEnumerable<StringExtent>.GetEnumerator()
			{
				this.Reset();
				this.Reset();
				return this;
			}

			// Token: 0x06000D25 RID: 3365 RVA: 0x000382F8 File Offset: 0x000364F8
			IEnumerator IEnumerable.GetEnumerator()
			{
				this.Reset();
				this.Reset();
				return this;
			}

			// Token: 0x04000636 RID: 1590
			public int[] ColumnIndexes;

			// Token: 0x04000637 RID: 1591
			public bool[] IsString;

			// Token: 0x04000638 RID: 1592
			public BlockedSegmentArray<char> m_charAllocator = new BlockedSegmentArray<char>();

			// Token: 0x04000639 RID: 1593
			public StringRecordTokenizer tokenizer;

			// Token: 0x0400063A RID: 1594
			public IDataRecord record;

			// Token: 0x0400063B RID: 1595
			private StringExtent current;

			// Token: 0x0400063C RID: 1596
			private int i;

			// Token: 0x0400063D RID: 1597
			private int j;

			// Token: 0x0400063E RID: 1598
			private ArraySegment<char> sourceString;
		}
	}
}
