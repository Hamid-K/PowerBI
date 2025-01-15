using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library.Lines;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1
{
	// Token: 0x02000229 RID: 553
	internal class PreviewInference
	{
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001B02E File Offset: 0x0001922E
		public static Value Apply(Value value, char[] candidateDelimiters = null, LinesModule.QuoteStyle[] candidateQuoteStyles = null)
		{
			if (value.IsBinary)
			{
				value = PreviewInference.Apply(value.AsBinary, candidateDelimiters, candidateQuoteStyles);
			}
			else if (value.IsList)
			{
				value = PreviewInference.Apply(value.AsList, candidateDelimiters, candidateQuoteStyles);
			}
			return value;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0001B064 File Offset: 0x00019264
		public static RecordValue InferFromBinary(BinaryValue value, char[] candidateDelimiters = null, LinesModule.QuoteStyle[] candidateQuoteStyles = null)
		{
			byte[] bytes = PreviewInference.GetBytes(value, 65536);
			RecordValue recordValue = RecordValue.Empty;
			Value value2;
			value.TryGetMetaField("Content.Type", out value2);
			if (bytes != null)
			{
				NumberValue numberValue;
				string text = PreviewInference.ReadContentAsText(bytes, out numberValue);
				if (!PreviewInference.IsBinary(text))
				{
					if (numberValue != NumberValue.NaN)
					{
						recordValue = RecordValue.New(Keys.New("Content.Encoding"), new Value[] { numberValue });
					}
					CsvParameters csvParameters = CsvParameters.Infer((char delimiter, LinesModule.QuoteStyle quoteStyle) => PreviewInference.CreateLineReader(text, delimiter, quoteStyle), bytes.Length == 65536, candidateDelimiters, candidateQuoteStyles);
					if (csvParameters != null)
					{
						string text2 = ((value2 != null && value2.IsText) ? value2.AsString : null);
						if (string.IsNullOrEmpty(text2) || text2 == "text/plain")
						{
							value2 = PreviewInference.csvContentType;
						}
						if (csvParameters.Delimiters != null)
						{
							recordValue = recordValue.Concatenate(RecordValue.New(PreviewInference.potentialDelimitersKeys, new Value[] { PreviewInference.GetPotentialDelimitersTable(csvParameters) })).AsRecord;
						}
						else if (csvParameters.Positions != null)
						{
							Value value3 = recordValue;
							Keys keys = PreviewInference.potentialPositionsKeys;
							Value[] array = new Value[1];
							int num = 0;
							Value[] array2 = csvParameters.Positions.Select((int x) => NumberValue.New(x)).ToArray<NumberValue>();
							array[num] = ListValue.New(array2);
							recordValue = value3.Concatenate(RecordValue.New(keys, array)).AsRecord;
						}
					}
				}
				if (value2 != null)
				{
					recordValue = RecordValue.New(Keys.New("Content.Type"), new Value[] { value2 }).Concatenate(recordValue).AsRecord;
				}
			}
			return recordValue;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0001B1F4 File Offset: 0x000193F4
		private static string ReadContentAsText(byte[] bytes, out NumberValue textEncoding)
		{
			Encoding encoding;
			if (TextEncoding.TryGetEncoding(bytes, 0, bytes.Length, out encoding))
			{
				byte[] preamble = encoding.GetPreamble();
				textEncoding = NumberValue.New(encoding.CodePage);
				return encoding.GetString(bytes, preamble.Length, bytes.Length - preamble.Length);
			}
			textEncoding = NumberValue.NaN;
			return Encoding.ASCII.GetString(bytes);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0001B248 File Offset: 0x00019448
		private static Value Apply(BinaryValue value, char[] candidateDelimiters = null, LinesModule.QuoteStyle[] candidateQuoteStyles = null)
		{
			byte[] bytes = PreviewInference.GetBytes(value, 65536);
			if (bytes != null)
			{
				NumberValue numberValue;
				string text = PreviewInference.ReadContentAsText(bytes, out numberValue);
				if (!PreviewInference.IsBinary(text))
				{
					if (numberValue != NumberValue.NaN)
					{
						RecordValue recordValue = RecordValue.New(Keys.New("Content.Encoding"), new Value[] { numberValue });
						value = value.NewMeta(value.MetaValue.Concatenate(recordValue).AsRecord).AsBinary;
					}
					CsvParameters csvParameters = CsvParameters.Infer((char delimiter, LinesModule.QuoteStyle quoteStyle) => PreviewInference.CreateLineReader(text, delimiter, quoteStyle), bytes.Length == 65536, candidateDelimiters, candidateQuoteStyles);
					if (csvParameters != null)
					{
						value = PreviewInference.GetValueWithCsvMetadata(value, csvParameters).AsBinary;
					}
				}
			}
			return value;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0001B2FC File Offset: 0x000194FC
		private static Value Apply(ListValue list, char[] candidateDelimiters = null, LinesModule.QuoteStyle[] candidateQuoteStyles = null)
		{
			ListValue inferenceList = LanguageLibrary.List.Take.Invoke(list, NumberValue.New(200)).AsList;
			if (inferenceList.Any((IValueReference v) => !v.Value.IsText && !v.Value.IsNull))
			{
				return list;
			}
			bool flag = false;
			CsvParameters csvParameters = CsvParameters.Infer((char delimiter, LinesModule.QuoteStyle quoteStyle) => new ListLineReader(inferenceList), flag, candidateDelimiters, candidateQuoteStyles);
			if (csvParameters != null)
			{
				list = PreviewInference.GetValueWithCsvMetadata(list, csvParameters).AsList;
			}
			return list;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0001B388 File Offset: 0x00019588
		private static ILineReader CreateLineReader(string text, char delimiter, LinesModule.QuoteStyle quoteStyle)
		{
			if (quoteStyle.Value.Equals(LinesModule.QuoteStyle.None.Value))
			{
				return new UnquotedTextReaderLineReader(new StringReader(text), false);
			}
			if (delimiter == 'W')
			{
				return new QuotedTextReaderLineReader(new StringReader(text), false);
			}
			return new ExcelTextReaderLineReader(new StringReader(text), false, delimiter);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0001B3D8 File Offset: 0x000195D8
		private static byte[] GetBytes(BinaryValue value, int count)
		{
			try
			{
				using (Stream stream = value.Open())
				{
					byte[] array = new byte[count];
					int i;
					int num;
					for (i = 0; i < count; i += num)
					{
						num = stream.Read(array, i, count - i);
						if (num == 0)
						{
							break;
						}
					}
					if (i != count)
					{
						byte[] array2 = new byte[i];
						Buffer.BlockCopy(array, 0, array2, 0, i);
						array = array2;
					}
					return array;
				}
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (InvalidDataException ex)
			{
				throw ValueException.NewDataFormatError(ex.Message, value, ex);
			}
			return null;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0001B488 File Offset: 0x00019688
		private static Value GetValueWithCsvMetadata(Value value, CsvParameters parameters)
		{
			Value value2;
			if (!value.TryGetMetaField("Content.Type", out value2) || (value2.IsText && value2.AsString == "text/plain"))
			{
				RecordValue recordValue = RecordValue.New(Keys.New("Content.Type"), new Value[] { PreviewInference.csvContentType });
				value = value.NewMeta(value.MetaValue.Concatenate(recordValue).AsRecord);
			}
			RecordValue recordValue2 = RecordValue.Empty;
			if (parameters.Delimiters != null)
			{
				Keys keys = Keys.New(new string[] { "Csv.PotentialDelimiters", "Csv.PotentialMaxColumns", "Csv.PotentialPopularColumns", "Csv.PotentialRowsWithPopularColumns", "Csv.PotentialRows", "Csv.PotentialQuoteStyles" });
				Value[] array = new Value[6];
				array[0] = TextValue.New(parameters.Delimiters);
				int num = 1;
				Value[] array2 = parameters.MaxColumns.Select((int x) => NumberValue.New(x)).ToArray<NumberValue>();
				array[num] = ListValue.New(array2);
				int num2 = 2;
				array2 = parameters.PopularColumns.Select((int x) => NumberValue.New(x)).ToArray<NumberValue>();
				array[num2] = ListValue.New(array2);
				int num3 = 3;
				array2 = parameters.RowsWithPopularColumns.Select((int x) => NumberValue.New(x)).ToArray<NumberValue>();
				array[num3] = ListValue.New(array2);
				int num4 = 4;
				array2 = parameters.Rows.Select((int x) => NumberValue.New(x)).ToArray<NumberValue>();
				array[num4] = ListValue.New(array2);
				int num5 = 5;
				array2 = parameters.QuoteStyles.Select((int x) => NumberValue.New(x)).ToArray<NumberValue>();
				array[num5] = ListValue.New(array2);
				recordValue2 = RecordValue.New(keys, array);
			}
			else if (parameters.Positions != null)
			{
				Keys keys2 = PreviewInference.potentialPositionsKeys;
				Value[] array3 = new Value[1];
				int num6 = 0;
				Value[] array2 = parameters.Positions.Select((int x) => NumberValue.New(x)).ToArray<NumberValue>();
				array3[num6] = ListValue.New(array2);
				recordValue2 = RecordValue.New(keys2, array3);
			}
			value = value.NewMeta(value.MetaValue.Concatenate(recordValue2).AsRecord);
			return value;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0001B6E8 File Offset: 0x000198E8
		private static TableValue GetPotentialDelimitersTable(CsvParameters parameters)
		{
			Value[] array = new Value[parameters.Delimiters.Length];
			for (int i = 0; i < parameters.Delimiters.Length; i++)
			{
				array[i] = RecordValue.New(PreviewInference.delimitersTableKeys, new Value[]
				{
					TextValue.New(parameters.Delimiters[i]),
					NumberValue.New(parameters.QuoteStyles[i]),
					NumberValue.New(parameters.MaxColumns[i]),
					NumberValue.New(parameters.PopularColumns[i]),
					NumberValue.New(parameters.Rows[i]),
					NumberValue.New(parameters.RowsWithPopularColumns[i])
				});
			}
			return ListValue.New(array).ToTable();
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0001B7A4 File Offset: 0x000199A4
		private static bool IsBinary(string text)
		{
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] < ' ')
				{
					char c = text[i];
					if (c != '\u0001')
					{
						switch (c)
						{
						case '\t':
						case '\n':
						case '\v':
						case '\r':
							break;
						default:
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04000680 RID: 1664
		public const int InferenceBufferSize = 65536;

		// Token: 0x04000681 RID: 1665
		private const string contentTypeName = "Content.Type";

		// Token: 0x04000682 RID: 1666
		private const string contentEncodingName = "Content.Encoding";

		// Token: 0x04000683 RID: 1667
		private static readonly Keys delimitersTableKeys = Keys.New(new string[] { "PotentialDelimiter", "QuoteStyle", "MaxColumns", "NonEmptyColumns", "MaxRows", "NonEmptyRows" });

		// Token: 0x04000684 RID: 1668
		private static readonly Keys potentialDelimitersKeys = Keys.New("Csv.PotentialDelimiters");

		// Token: 0x04000685 RID: 1669
		private static readonly Keys potentialPositionsKeys = Keys.New("Csv.PotentialPositions");

		// Token: 0x04000686 RID: 1670
		private static readonly TableTypeValue delimitersTableType = TableTypeValue.New(RecordTypeValue.New(PreviewInference.delimitersTableKeys));

		// Token: 0x04000687 RID: 1671
		private static readonly TextValue csvContentType = TextValue.New("text/csv");
	}
}
