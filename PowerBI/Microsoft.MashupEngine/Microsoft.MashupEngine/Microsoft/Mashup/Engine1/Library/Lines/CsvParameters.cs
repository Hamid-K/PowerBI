using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009BB RID: 2491
	internal class CsvParameters
	{
		// Token: 0x0600470F RID: 18191 RVA: 0x000EE12E File Offset: 0x000EC32E
		public CsvParameters(string delimiters, int[] maxColumns, int[] popularColumns, int[] rowsWithPopularColumns, int[] rows, int[] quoteStyles)
		{
			this.delimiters = delimiters;
			this.maxColumns = maxColumns;
			this.popularColumns = popularColumns;
			this.rowsWithPopularColumns = rowsWithPopularColumns;
			this.rows = rows;
			this.quoteStyles = quoteStyles;
			this.positions = null;
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x000EE16A File Offset: 0x000EC36A
		public CsvParameters(int[] positions)
		{
			this.delimiters = null;
			this.maxColumns = null;
			this.popularColumns = null;
			this.rowsWithPopularColumns = null;
			this.rows = null;
			this.quoteStyles = null;
			this.positions = positions;
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06004711 RID: 18193 RVA: 0x000EE1A3 File Offset: 0x000EC3A3
		public string Delimiters
		{
			get
			{
				return this.delimiters;
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06004712 RID: 18194 RVA: 0x000EE1AB File Offset: 0x000EC3AB
		public int[] MaxColumns
		{
			get
			{
				return this.maxColumns;
			}
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06004713 RID: 18195 RVA: 0x000EE1B3 File Offset: 0x000EC3B3
		public int[] PopularColumns
		{
			get
			{
				return this.popularColumns;
			}
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06004714 RID: 18196 RVA: 0x000EE1BB File Offset: 0x000EC3BB
		public int[] RowsWithPopularColumns
		{
			get
			{
				return this.rowsWithPopularColumns;
			}
		}

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06004715 RID: 18197 RVA: 0x000EE1C3 File Offset: 0x000EC3C3
		public int[] Rows
		{
			get
			{
				return this.rows;
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06004716 RID: 18198 RVA: 0x000EE1CB File Offset: 0x000EC3CB
		public int[] QuoteStyles
		{
			get
			{
				return this.quoteStyles;
			}
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06004717 RID: 18199 RVA: 0x000EE1D3 File Offset: 0x000EC3D3
		public int[] Positions
		{
			get
			{
				return this.positions;
			}
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x000EE1DC File Offset: 0x000EC3DC
		public static CsvParameters Infer(CsvParameters.CreateLineReaderFunction createLineReader, bool partial, char[] candidateDelimiters = null, LinesModule.QuoteStyle[] candidateQuoteStyles = null)
		{
			if (candidateDelimiters == null)
			{
				candidateDelimiters = CsvParameters.defaultDelimiters;
			}
			if (candidateQuoteStyles == null)
			{
				candidateQuoteStyles = CsvParameters.defaultQuoteStyles;
			}
			CsvParameters csvParameters = CsvParameters.GetDelimiters(createLineReader, partial, candidateDelimiters, candidateQuoteStyles);
			bool flag = false;
			if (csvParameters != null)
			{
				for (int i = 0; i < csvParameters.Rows.Count<int>(); i++)
				{
					if (csvParameters.MaxColumns[i] != 1 && csvParameters.Rows[i] == csvParameters.RowsWithPopularColumns[i])
					{
						flag = true;
						break;
					}
				}
			}
			if ((csvParameters == null || !flag) && candidateDelimiters.Contains('W'))
			{
				CsvParameters csvParameters2 = CsvParameters.GetPositions(createLineReader, partial);
				if (csvParameters2 != null)
				{
					return csvParameters2;
				}
			}
			return csvParameters;
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x000EE264 File Offset: 0x000EC464
		private static CsvParameters GetDelimiters(CsvParameters.CreateLineReaderFunction createLineReader, bool partial, char[] candidateDelimiters, LinesModule.QuoteStyle[] candidateQuoteStyles)
		{
			CsvParameters csvParameters = CsvParameters.GetDelimiters(createLineReader, partial, false, candidateDelimiters, candidateQuoteStyles);
			if (csvParameters != null)
			{
				return csvParameters;
			}
			csvParameters = CsvParameters.GetDelimiters(createLineReader, partial, true, candidateDelimiters, candidateQuoteStyles);
			if (csvParameters != null)
			{
				return csvParameters;
			}
			return null;
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x000EE294 File Offset: 0x000EC494
		private static CsvParameters GetDelimiters(CsvParameters.CreateLineReaderFunction createLineReader, bool partial, bool removeEmptyTrailingFields, char[] candidateDelimiters, LinesModule.QuoteStyle[] candidateQuoteStyles)
		{
			string text = string.Empty;
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			List<int> list3 = new List<int>();
			List<int> list4 = new List<int>();
			List<int> list5 = new List<int>();
			foreach (char c in candidateDelimiters)
			{
				foreach (LinesModule.QuoteStyle quoteStyle in candidateQuoteStyles)
				{
					int rowCount = CsvParameters.GetRowCount(createLineReader, partial, c, quoteStyle);
					if (rowCount >= 1)
					{
						ILineReader lineReader = createLineReader(c, quoteStyle);
						if (removeEmptyTrailingFields && c != 'W')
						{
							lineReader = new CsvParameters.EmptyTrailingFieldRemovingLineReader(lineReader, c);
						}
						lineReader = new CsvParameters.EmptyLineRemovingLineReader(lineReader);
						using (lineReader)
						{
							int num = -1;
							int num2 = -1;
							int num3 = -1;
							CsvParameters.GetCsvParameter(CsvParameters.GetFieldReader(lineReader, c), rowCount, ref num, ref num2, ref num3);
							text += c.ToString();
							list.Add(num);
							list2.Add(num2);
							list3.Add(num3);
							list4.Add(rowCount);
							list5.Add(quoteStyle.Value.AsInteger32);
						}
					}
				}
			}
			if (text.Length > 0)
			{
				return new CsvParameters(text, list.ToArray(), list2.ToArray(), list3.ToArray(), list4.ToArray(), list5.ToArray());
			}
			return null;
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x000EE400 File Offset: 0x000EC600
		private static CsvParameters GetPositions(CsvParameters.CreateLineReaderFunction createLineReader, bool partial)
		{
			List<int> list = new List<int>();
			int rowCount = CsvParameters.GetRowCount(createLineReader, partial, 'W', LinesModule.QuoteStyle.Csv);
			HashSet<int> hashSet = new HashSet<int>();
			HashSet<int> hashSet2 = new HashSet<int>();
			int num = ((rowCount == 0) ? 0 : int.MaxValue);
			using (ILineReader lineReader = new CsvParameters.EmptyLineRemovingLineReader(createLineReader('W', LinesModule.QuoteStyle.Csv)))
			{
				for (int i = 0; i < rowCount; i++)
				{
					if (!lineReader.MoveNext())
					{
						throw new InvalidOperationException();
					}
					char[] text = lineReader.Text;
					num = Math.Min(lineReader.LineEnd - lineReader.LineStart, num);
					for (int j = 0; j < num; j++)
					{
						bool flag = char.IsWhiteSpace(text[lineReader.LineStart + j]);
						if (i == 0)
						{
							if (flag)
							{
								hashSet.Add(j);
							}
							else
							{
								hashSet2.Add(j);
							}
						}
						else if (flag)
						{
							hashSet2.Remove(j);
						}
						else
						{
							hashSet.Remove(j);
						}
					}
				}
			}
			int k = 0;
			while (k < num)
			{
				list.Add(k);
				k++;
				while (k < num && (!hashSet2.Contains(k) || !hashSet.Contains(k - 1)))
				{
					k++;
				}
			}
			if (list.Count<int>() > 1)
			{
				return new CsvParameters(list.ToArray());
			}
			return null;
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x000EE564 File Offset: 0x000EC764
		private static int GetRowCount(CsvParameters.CreateLineReaderFunction createLineReader, bool partial, char delimiter, LinesModule.QuoteStyle quoteStyle)
		{
			int num2;
			using (ILineReader lineReader = new CsvParameters.EmptyLineRemovingLineReader(createLineReader(delimiter, quoteStyle)))
			{
				int num = 0;
				while (lineReader.MoveNext())
				{
					num++;
				}
				if (partial)
				{
					num--;
				}
				num2 = num;
			}
			return num2;
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x000EE5B8 File Offset: 0x000EC7B8
		private static IFieldReader<IValueReference> GetFieldReader(ILineReader lineReader, char delimiter)
		{
			if (delimiter == 'W')
			{
				return new WhitespaceDelimitedFieldReader(lineReader, true);
			}
			return new ExcelSingleCharacterDelimitedFieldReader(lineReader, delimiter);
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x000EE5D0 File Offset: 0x000EC7D0
		private static void GetCsvParameter(IFieldReader<IValueReference> reader, int rowCount, ref int maxColumnCount, ref int popularColumnCount, ref int popularRowCount)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int i = 0; i < rowCount; i++)
			{
				if (!reader.MoveNextRow())
				{
					throw new InvalidOperationException();
				}
				int num = 0;
				while (reader.MoveNextField())
				{
					num++;
				}
				int num2;
				if (dictionary.TryGetValue(num, out num2))
				{
					num2++;
					dictionary[num] = num2;
				}
				else
				{
					num2 = 1;
					dictionary.Add(num, num2);
				}
				if (num > maxColumnCount)
				{
					maxColumnCount = num;
				}
				if (num2 > popularRowCount)
				{
					popularColumnCount = num;
					popularRowCount = num2;
				}
			}
		}

		// Token: 0x040025CD RID: 9677
		public const char WhitespaceDelimiter = 'W';

		// Token: 0x040025CE RID: 9678
		private static readonly char[] defaultDelimiters = new char[] { ',', '\t', ';', ':', '|', '\u0001', 'W' };

		// Token: 0x040025CF RID: 9679
		private static readonly LinesModule.QuoteStyle[] defaultQuoteStyles = new LinesModule.QuoteStyle[]
		{
			LinesModule.QuoteStyle.None,
			LinesModule.QuoteStyle.Csv
		};

		// Token: 0x040025D0 RID: 9680
		private string delimiters;

		// Token: 0x040025D1 RID: 9681
		private int[] rows;

		// Token: 0x040025D2 RID: 9682
		private int[] popularColumns;

		// Token: 0x040025D3 RID: 9683
		private int[] maxColumns;

		// Token: 0x040025D4 RID: 9684
		private int[] rowsWithPopularColumns;

		// Token: 0x040025D5 RID: 9685
		private int[] quoteStyles;

		// Token: 0x040025D6 RID: 9686
		private int[] positions;

		// Token: 0x020009BC RID: 2492
		// (Invoke) Token: 0x06004721 RID: 18209
		public delegate ILineReader CreateLineReaderFunction(char delimiter, LinesModule.QuoteStyle quoteStyle);

		// Token: 0x020009BD RID: 2493
		private enum TextReaders
		{
			// Token: 0x040025D8 RID: 9688
			QuotedTextReader,
			// Token: 0x040025D9 RID: 9689
			ExcelTextReader
		}

		// Token: 0x020009BE RID: 2494
		private class EmptyLineRemovingLineReader : ILineReader, IDisposable
		{
			// Token: 0x06004724 RID: 18212 RVA: 0x000EE678 File Offset: 0x000EC878
			public EmptyLineRemovingLineReader(ILineReader reader)
			{
				this.reader = reader;
			}

			// Token: 0x06004725 RID: 18213 RVA: 0x000EE687 File Offset: 0x000EC887
			public bool MoveNext()
			{
				while (this.reader.MoveNext())
				{
					if (this.LineEnd > this.LineStart)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06004726 RID: 18214 RVA: 0x000EE6A7 File Offset: 0x000EC8A7
			public string GetLine()
			{
				return this.reader.GetLine();
			}

			// Token: 0x170016BE RID: 5822
			// (get) Token: 0x06004727 RID: 18215 RVA: 0x000EE6B4 File Offset: 0x000EC8B4
			public char[] Text
			{
				get
				{
					return this.reader.Text;
				}
			}

			// Token: 0x170016BF RID: 5823
			// (get) Token: 0x06004728 RID: 18216 RVA: 0x000EE6C1 File Offset: 0x000EC8C1
			public int LineStart
			{
				get
				{
					return this.reader.LineStart;
				}
			}

			// Token: 0x170016C0 RID: 5824
			// (get) Token: 0x06004729 RID: 18217 RVA: 0x000EE6CE File Offset: 0x000EC8CE
			public int LineEnd
			{
				get
				{
					return this.reader.LineEnd;
				}
			}

			// Token: 0x170016C1 RID: 5825
			// (get) Token: 0x0600472A RID: 18218 RVA: 0x000EE6DB File Offset: 0x000EC8DB
			public bool HasQuotes
			{
				get
				{
					return this.reader.HasQuotes;
				}
			}

			// Token: 0x0600472B RID: 18219 RVA: 0x000EE6E8 File Offset: 0x000EC8E8
			public void Dispose()
			{
				this.reader.Dispose();
			}

			// Token: 0x040025DA RID: 9690
			private ILineReader reader;
		}

		// Token: 0x020009BF RID: 2495
		private class EmptyTrailingFieldRemovingLineReader : ILineReader, IDisposable
		{
			// Token: 0x0600472C RID: 18220 RVA: 0x000EE6F5 File Offset: 0x000EC8F5
			public EmptyTrailingFieldRemovingLineReader(ILineReader reader, char delimiter)
			{
				this.reader = reader;
				this.delimiter = delimiter;
			}

			// Token: 0x0600472D RID: 18221 RVA: 0x000EE70C File Offset: 0x000EC90C
			public bool MoveNext()
			{
				if (!this.reader.MoveNext())
				{
					return false;
				}
				this.lineEnd = this.reader.LineEnd;
				if (this.lineEnd > this.reader.LineStart && this.reader.Text[this.lineEnd - 1] == this.delimiter)
				{
					this.lineEnd--;
				}
				return true;
			}

			// Token: 0x0600472E RID: 18222 RVA: 0x000EE778 File Offset: 0x000EC978
			public string GetLine()
			{
				if (this.lineEnd == this.reader.LineEnd)
				{
					return this.reader.GetLine();
				}
				return new string(this.reader.Text, this.reader.LineStart, this.lineEnd - this.reader.LineStart);
			}

			// Token: 0x170016C2 RID: 5826
			// (get) Token: 0x0600472F RID: 18223 RVA: 0x000EE7D1 File Offset: 0x000EC9D1
			public char[] Text
			{
				get
				{
					return this.reader.Text;
				}
			}

			// Token: 0x170016C3 RID: 5827
			// (get) Token: 0x06004730 RID: 18224 RVA: 0x000EE7DE File Offset: 0x000EC9DE
			public int LineStart
			{
				get
				{
					return this.reader.LineStart;
				}
			}

			// Token: 0x170016C4 RID: 5828
			// (get) Token: 0x06004731 RID: 18225 RVA: 0x000EE7EB File Offset: 0x000EC9EB
			public int LineEnd
			{
				get
				{
					return this.lineEnd;
				}
			}

			// Token: 0x170016C5 RID: 5829
			// (get) Token: 0x06004732 RID: 18226 RVA: 0x000EE7F3 File Offset: 0x000EC9F3
			public bool HasQuotes
			{
				get
				{
					return this.reader.HasQuotes;
				}
			}

			// Token: 0x06004733 RID: 18227 RVA: 0x000EE800 File Offset: 0x000ECA00
			public void Dispose()
			{
				this.reader.Dispose();
			}

			// Token: 0x040025DB RID: 9691
			private ILineReader reader;

			// Token: 0x040025DC RID: 9692
			private int lineEnd;

			// Token: 0x040025DD RID: 9693
			private char delimiter;
		}
	}
}
