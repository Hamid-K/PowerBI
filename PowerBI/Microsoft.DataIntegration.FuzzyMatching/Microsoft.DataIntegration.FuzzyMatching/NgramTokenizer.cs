using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000E6 RID: 230
	[Serializable]
	public class NgramTokenizer : StringRecordTokenizer
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0002AA8B File Offset: 0x00028C8B
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0002AA93 File Offset: 0x00028C93
		public int NgramLength { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002AA9C File Offset: 0x00028C9C
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0002AAA4 File Offset: 0x00028CA4
		public bool GenerateLeadingAndTrailingNgrams { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002AAAD File Offset: 0x00028CAD
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0002AAB5 File Offset: 0x00028CB5
		public char LeadingCharacter { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0002AABE File Offset: 0x00028CBE
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0002AAC6 File Offset: 0x00028CC6
		public char TrailingCharacter { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0002AACF File Offset: 0x00028CCF
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0002AAD7 File Offset: 0x00028CD7
		public bool RemoveDelimiters { get; set; }

		// Token: 0x0600091F RID: 2335 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		public NgramTokenizer()
		{
			this.NgramLength = 3;
			this.LeadingCharacter = ' ';
			this.TrailingCharacter = ' ';
			this.GenerateLeadingAndTrailingNgrams = true;
			this.RemoveDelimiters = true;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002AB10 File Offset: 0x00028D10
		public override void Prepare(DataTable schemaTable, DomainBinding domainBinding, out TokenizerContext _context)
		{
			NgramTokenizer.NgramTokenizerContext ngramTokenizerContext = new NgramTokenizer.NgramTokenizerContext
			{
				Tokenizer = this
			};
			TokenizerContext tokenizerContext;
			base.Prepare(schemaTable, domainBinding, out tokenizerContext);
			ngramTokenizerContext.BaseContext = (StringRecordTokenizer.Context)tokenizerContext;
			_context = ngramTokenizerContext;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0002AB43 File Offset: 0x00028D43
		public override IEnumerable<StringExtent> Tokenize(TokenizerContext tokenizerContext, IDataRecord record)
		{
			NgramTokenizer.NgramTokenizerContext ngramTokenizerContext = (NgramTokenizer.NgramTokenizerContext)tokenizerContext;
			ngramTokenizerContext.Reset();
			ngramTokenizerContext.Record = record;
			return ngramTokenizerContext;
		}

		// Token: 0x02000184 RID: 388
		[Serializable]
		private class NgramTokenizerContext : TokenizerContext, IEnumerable<StringExtent>, IEnumerable, IEnumerator<StringExtent>, IDisposable, IEnumerator
		{
			// Token: 0x06000D14 RID: 3348 RVA: 0x00037CE4 File Offset: 0x00035EE4
			public override void Reset()
			{
				this.BaseContext.Reset();
				this.Record = null;
			}

			// Token: 0x06000D15 RID: 3349 RVA: 0x00037CF8 File Offset: 0x00035EF8
			public IEnumerator<StringExtent> GetEnumerator()
			{
				this.Reset();
				return this;
			}

			// Token: 0x06000D16 RID: 3350 RVA: 0x00037D01 File Offset: 0x00035F01
			IEnumerator IEnumerable.GetEnumerator()
			{
				this.Reset();
				return this;
			}

			// Token: 0x1700026C RID: 620
			// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00037D0A File Offset: 0x00035F0A
			public StringExtent Current
			{
				get
				{
					return new StringExtent(this.outputExtent.Array, this.outputExtent.Offset + this.outputStart, this.outputLength);
				}
			}

			// Token: 0x1700026D RID: 621
			// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00037D34 File Offset: 0x00035F34
			object IEnumerator.Current
			{
				get
				{
					return new StringExtent(this.outputExtent.Array, this.outputExtent.Offset + this.outputStart, this.outputLength);
				}
			}

			// Token: 0x06000D19 RID: 3353 RVA: 0x00037D63 File Offset: 0x00035F63
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000D1A RID: 3354 RVA: 0x00037D65 File Offset: 0x00035F65
			void IEnumerator.Reset()
			{
				this.m_currentColumnIndex = -1;
				this.outputStart = 0;
				this.outputEnd = 0;
				this.outputLength = 0;
				this.outputExtent = default(StringExtent);
			}

			// Token: 0x06000D1B RID: 3355 RVA: 0x00037D90 File Offset: 0x00035F90
			public bool MoveNext()
			{
				StringRecordTokenizer.Context baseContext = this.BaseContext;
				int ngramLength = this.Tokenizer.NgramLength;
				BitArray isDelimiterBitArray = this.Tokenizer.m_isDelimiterBitArray;
				if (this.outputStart + 1 < this.outputEnd)
				{
					this.outputStart++;
					return true;
				}
				this.outputStart = (this.outputEnd = 0);
				if (this.m_currentColumnIndex + 1 >= baseContext.ColumnIndexes.Length)
				{
					this.outputStart = (this.outputEnd = 0);
					return false;
				}
				this.m_currentColumnIndex++;
				ArraySegment<char> arraySegment;
				TokenizerCommon.GetString(this.Record, baseContext.ColumnIndexes[this.m_currentColumnIndex], baseContext.IsString[this.m_currentColumnIndex], baseContext.m_charAllocator, out arraySegment, ref this.Tokenizer.m_providerConvertsStringToChar);
				StringNormalization.NormalizeString(this.Tokenizer.NormalizationOptions.FoldStringFlags, this.Tokenizer.NormalizationOptions.MapStringFlags, arraySegment, baseContext.m_charAllocator, out arraySegment);
				if (this.Tokenizer.RemoveDelimiters)
				{
					int num = 0;
					for (int i = 0; i < arraySegment.Count; i++)
					{
						if (!isDelimiterBitArray[(int)arraySegment.Array[arraySegment.Offset + i]])
						{
							arraySegment.Array[arraySegment.Offset + num++] = arraySegment.Array[arraySegment.Offset + i];
						}
					}
					if (num < arraySegment.Count)
					{
						arraySegment = new ArraySegment<char>(arraySegment.Array, arraySegment.Offset, num);
					}
				}
				if (arraySegment.Count == 0)
				{
					this.outputStart = (this.outputEnd = 0);
					return this.MoveNext();
				}
				this.outputExtent = new StringExtent(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				if (this.Tokenizer.GenerateLeadingAndTrailingNgrams)
				{
					int num2 = arraySegment.Count + 2 * ngramLength - 2;
					if (this.PaddedNgramBuffer == null || this.PaddedNgramBuffer.Length < num2)
					{
						this.PaddedNgramBuffer = new char[num2];
					}
					for (int j = 0; j < ngramLength - 1; j++)
					{
						this.PaddedNgramBuffer[j] = this.Tokenizer.LeadingCharacter;
						this.PaddedNgramBuffer[num2 - 1 - j] = this.Tokenizer.TrailingCharacter;
					}
					for (int k = 0; k < arraySegment.Count; k++)
					{
						this.PaddedNgramBuffer[ngramLength - 1 + k] = arraySegment.Array[arraySegment.Offset + k];
					}
					this.outputExtent = new StringExtent(this.PaddedNgramBuffer, 0, num2);
				}
				if (this.outputExtent.Length < ngramLength)
				{
					this.outputStart = 0;
					this.outputEnd = 1;
					this.outputLength = this.outputExtent.Length;
					return true;
				}
				this.outputStart = 0;
				this.outputEnd = this.outputExtent.Length - ngramLength + 1;
				this.outputLength = ngramLength;
				return true;
			}

			// Token: 0x0400062D RID: 1581
			public NgramTokenizer Tokenizer;

			// Token: 0x0400062E RID: 1582
			public StringRecordTokenizer.Context BaseContext;

			// Token: 0x0400062F RID: 1583
			public IDataRecord Record;

			// Token: 0x04000630 RID: 1584
			private char[] PaddedNgramBuffer;

			// Token: 0x04000631 RID: 1585
			private int m_currentColumnIndex;

			// Token: 0x04000632 RID: 1586
			private StringExtent outputExtent;

			// Token: 0x04000633 RID: 1587
			private int outputStart;

			// Token: 0x04000634 RID: 1588
			private int outputEnd;

			// Token: 0x04000635 RID: 1589
			private int outputLength;
		}
	}
}
