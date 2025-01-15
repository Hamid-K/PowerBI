using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009D1 RID: 2513
	internal sealed class LinesModule : Module
	{
		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x06004772 RID: 18290 RVA: 0x000EF2F2 File Offset: 0x000ED4F2
		public override string Name
		{
			get
			{
				return "Lines";
			}
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x06004773 RID: 18291 RVA: 0x000EF2F9 File Offset: 0x000ED4F9
		public override Keys ExportKeys
		{
			get
			{
				if (LinesModule.exportKeys == null)
				{
					LinesModule.exportKeys = Keys.New(32, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Csv.Document";
						case 1:
							return "Lines.FromText";
						case 2:
							return "Lines.FromBinary";
						case 3:
							return "Lines.ToText";
						case 4:
							return "Lines.ToBinary";
						case 5:
							return "Table.FromList";
						case 6:
							return "Table.ToList";
						case 7:
							return "Splitter.SplitByNothing";
						case 8:
							return "Splitter.SplitTextByCharacterTransition";
						case 9:
							return "Splitter.SplitTextByDelimiter";
						case 10:
							return "Splitter.SplitTextByRanges";
						case 11:
							return "Splitter.SplitTextByWhitespace";
						case 12:
							return "Splitter.SplitTextByEachDelimiter";
						case 13:
							return "Splitter.SplitTextByAnyDelimiter";
						case 14:
							return "Splitter.SplitTextByPositions";
						case 15:
							return "Splitter.SplitTextByRepeatedLengths";
						case 16:
							return "Splitter.SplitTextByLengths";
						case 17:
							return "Combiner.CombineTextByDelimiter";
						case 18:
							return "Combiner.CombineTextByEachDelimiter";
						case 19:
							return "Combiner.CombineTextByRanges";
						case 20:
							return "Combiner.CombineTextByPositions";
						case 21:
							return "Combiner.CombineTextByLengths";
						case 22:
							return LinesModule.ExtraValues.Type.GetName();
						case 23:
							return LinesModule.ExtraValues.List.GetName();
						case 24:
							return LinesModule.ExtraValues.Ignore.GetName();
						case 25:
							return LinesModule.ExtraValues.Error.GetName();
						case 26:
							return LinesModule.QuoteStyle.Type.GetName();
						case 27:
							return LinesModule.QuoteStyle.NoneEnum.GetName();
						case 28:
							return LinesModule.QuoteStyle.CsvEnum.GetName();
						case 29:
							return LinesModule.CsvStyle.Type.GetName();
						case 30:
							return LinesModule.CsvStyle.QuoteAlways.GetName();
						case 31:
							return LinesModule.CsvStyle.QuoteAfterDelimiter.GetName();
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return LinesModule.exportKeys;
			}
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x000EF334 File Offset: 0x000ED534
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new LinesModule.Csv.DocumentFunctionValue(host);
				case 1:
					return new LinesModule.Lines.FromTextFunctionValue(host);
				case 2:
					return new LinesModule.Lines.FromBinaryFunctionValue(host);
				case 3:
					return LinesModule.Lines.ToText;
				case 4:
					return LinesModule.Lines.ToBinary;
				case 5:
					return LinesModule.Table.FromList;
				case 6:
					return LinesModule.Table.ToList;
				case 7:
					return LinesModule.Splitter.SplitByNothing;
				case 8:
					return LinesModule.Splitter.SplitTextByCharacterTransition;
				case 9:
					return LinesModule.Splitter.SplitTextByDelimiter;
				case 10:
					return LinesModule.Splitter.SplitTextByRanges;
				case 11:
					return LinesModule.Splitter.SplitTextByWhitespace;
				case 12:
					return LinesModule.Splitter.SplitTextByEachDelimiter;
				case 13:
					return LinesModule.Splitter.SplitTextByAnyDelimiter;
				case 14:
					return LinesModule.Splitter.SplitTextByPositions;
				case 15:
					return LinesModule.Splitter.SplitTextByRepeatedLengths;
				case 16:
					return LinesModule.Splitter.SplitTextByLengths;
				case 17:
					return LinesModule.Combiner.CombineTextByDelimiter;
				case 18:
					return LinesModule.Combiner.CombineTextByEachDelimiter;
				case 19:
					return LinesModule.Combiner.CombineTextByRanges;
				case 20:
					return LinesModule.Combiner.CombineTextByPositions;
				case 21:
					return LinesModule.Combiner.CombineTextByLengths;
				case 22:
					return LinesModule.ExtraValues.Type;
				case 23:
					return LinesModule.ExtraValues.List;
				case 24:
					return LinesModule.ExtraValues.Ignore;
				case 25:
					return LinesModule.ExtraValues.Error;
				case 26:
					return LinesModule.QuoteStyle.Type;
				case 27:
					return LinesModule.QuoteStyle.NoneEnum;
				case 28:
					return LinesModule.QuoteStyle.CsvEnum;
				case 29:
					return LinesModule.CsvStyle.Type;
				case 30:
					return LinesModule.CsvStyle.QuoteAlways;
				case 31:
					return LinesModule.CsvStyle.QuoteAfterDelimiter;
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x000EF368 File Offset: 0x000ED568
		private static LinesModule.QuoteStyle GetQuoteStyle(Value quoteStyle)
		{
			if (quoteStyle.IsNull)
			{
				return LinesModule.QuoteStyle.Csv;
			}
			if (quoteStyle.Equals(LinesModule.QuoteStyle.None.Value))
			{
				return LinesModule.QuoteStyle.None;
			}
			if (quoteStyle.Equals(LinesModule.QuoteStyle.Csv.Value))
			{
				return LinesModule.QuoteStyle.Csv;
			}
			throw ValueException.NewExpressionError<Message0>(Strings.InvalidQuoteStyle, quoteStyle, null);
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x000EF3C0 File Offset: 0x000ED5C0
		private static bool AllCharDelimiters(string[] delimiters)
		{
			for (int i = 0; i < delimiters.Length; i++)
			{
				if (delimiters[i].Length != 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x000EF3EC File Offset: 0x000ED5EC
		private static HashSet<char> GetCharDelimiters(string[] delimiters)
		{
			HashSet<char> hashSet = new HashSet<char>();
			for (int i = 0; i < delimiters.Length; i++)
			{
				hashSet.Add(delimiters[i][0]);
			}
			return hashSet;
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x000EF420 File Offset: 0x000ED620
		private static string[] GetStringDelimiters(ListValue delimiters, bool reverse)
		{
			Value[] array = delimiters.ToArray();
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsText)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Splitter_Delimiter_Invalid, delimiters, null);
				}
				string text = array[i].AsString;
				if (text.Length == 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Splitter_Delimiter_Invalid, delimiters, null);
				}
				if (reverse)
				{
					text = LinesModule.ReverseString(text);
				}
				array2[i] = text;
			}
			return array2;
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x000EF490 File Offset: 0x000ED690
		private static string ReverseString(string s)
		{
			if (s.Length <= 1)
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder(s.Length);
			for (int i = 0; i < s.Length; i++)
			{
				stringBuilder.Append(s[s.Length - i - 1]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x000EF4E4 File Offset: 0x000ED6E4
		private static LinesModule.Range[] GetRangesFromLengths(ListValue list)
		{
			List<LinesModule.Range> list2 = new List<LinesModule.Range>();
			int num = 0;
			foreach (IValueReference valueReference in list)
			{
				Value value = valueReference.Value;
				if (!value.IsNumber)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Lines_Length_Invalid, value, null);
				}
				int asInteger = value.AsInteger32;
				if (asInteger < 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Lines_Length_Invalid, value, null);
				}
				list2.Add(new LinesModule.Range(num, asInteger));
				num += asInteger;
			}
			return list2.ToArray();
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x000EF57C File Offset: 0x000ED77C
		private static LinesModule.Range[] GetRangesFromPositions(ListValue list)
		{
			List<LinesModule.Range> list2 = new List<LinesModule.Range>();
			using (IEnumerator<IValueReference> enumerator = list.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					int num = LinesModule.GetPosition(enumerator.Current.Value, 0);
					while (enumerator.MoveNext())
					{
						IValueReference valueReference = enumerator.Current;
						int position = LinesModule.GetPosition(valueReference.Value, num);
						list2.Add(new LinesModule.Range(num, position - num));
						num = position;
					}
					list2.Add(new LinesModule.Range(num, int.MaxValue));
				}
			}
			return list2.ToArray();
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x000EF610 File Offset: 0x000ED810
		private static int GetPosition(Value value, int minValue)
		{
			if (!value.IsNumber)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Lines_Position_Invalid, value, null);
			}
			int asInteger = value.AsInteger32;
			if (asInteger < minValue)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Lines_Position_Invalid, value, null);
			}
			return asInteger;
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x000EF640 File Offset: 0x000ED840
		private static LinesModule.Range[] GetRanges(ListValue list)
		{
			List<LinesModule.Range> list2 = new List<LinesModule.Range>();
			foreach (IValueReference valueReference in list)
			{
				Value value = valueReference.Value;
				if (!value.IsList)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Lines_RangeList_Invalid, value, null);
				}
				list2.Add(LinesModule.GetRange(value.AsList));
			}
			return list2.ToArray();
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x000EF6B8 File Offset: 0x000ED8B8
		private static LinesModule.Range GetRange(ListValue range)
		{
			if (range.Count != 2)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Lines_RangeList_Invalid, range, null);
			}
			int asInteger = range[0].AsInteger32;
			if (asInteger < 0)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Lines_RangeOffset_OutOfRange, range, null);
			}
			int num = (range[1].IsNull ? int.MaxValue : range[1].AsInteger32);
			if (num < 0)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Lines_RangeLength_OutOfRange, range, null);
			}
			return new LinesModule.Range(asInteger, num);
		}

		// Token: 0x040025FB RID: 9723
		private static Keys exportKeys;

		// Token: 0x040025FC RID: 9724
		private const string TextToText = "Text.ToText";

		// Token: 0x040025FD RID: 9725
		private static readonly Keys TextToTextKeys = Keys.New("Text.ToText");

		// Token: 0x040025FE RID: 9726
		private static readonly RecordValue TextToTextMeta = RecordValue.New(LinesModule.TextToTextKeys, new Value[] { LogicalValue.True });

		// Token: 0x020009D2 RID: 2514
		private enum Exports
		{
			// Token: 0x04002600 RID: 9728
			CsvDocument,
			// Token: 0x04002601 RID: 9729
			LinesFromText,
			// Token: 0x04002602 RID: 9730
			LinesFromBinary,
			// Token: 0x04002603 RID: 9731
			LinesToText,
			// Token: 0x04002604 RID: 9732
			LinesToBinary,
			// Token: 0x04002605 RID: 9733
			TableFromList,
			// Token: 0x04002606 RID: 9734
			TableToList,
			// Token: 0x04002607 RID: 9735
			SplitterSplitByNothing,
			// Token: 0x04002608 RID: 9736
			SplitterSplitTextByCharacterTransition,
			// Token: 0x04002609 RID: 9737
			SplitterSplitTextByDelimiter,
			// Token: 0x0400260A RID: 9738
			SplitterSplitTextByRanges,
			// Token: 0x0400260B RID: 9739
			SplitterSplitTextByWhitespace,
			// Token: 0x0400260C RID: 9740
			SplitterSplitTextByEachDelimiter,
			// Token: 0x0400260D RID: 9741
			SplitterSplitTextByAnyDelimiter,
			// Token: 0x0400260E RID: 9742
			SplitterSplitTextByPositions,
			// Token: 0x0400260F RID: 9743
			SplitterSplitTextByRepeatedLengths,
			// Token: 0x04002610 RID: 9744
			SplitterSplitTextByLengths,
			// Token: 0x04002611 RID: 9745
			CombinerCombineTextByDelimiter,
			// Token: 0x04002612 RID: 9746
			CombinerCombineTextByEachDelimiter,
			// Token: 0x04002613 RID: 9747
			CombinerCombineTextByRanges,
			// Token: 0x04002614 RID: 9748
			CombinerCombineTextByPositions,
			// Token: 0x04002615 RID: 9749
			CombinerCombineTextByLengths,
			// Token: 0x04002616 RID: 9750
			ExtraValues_Type,
			// Token: 0x04002617 RID: 9751
			ExtraValues_List,
			// Token: 0x04002618 RID: 9752
			ExtraValues_Ignore,
			// Token: 0x04002619 RID: 9753
			ExtraValues_Error,
			// Token: 0x0400261A RID: 9754
			QuoteStyle_Type,
			// Token: 0x0400261B RID: 9755
			QuoteStyle_None,
			// Token: 0x0400261C RID: 9756
			QuoteStyle_Csv,
			// Token: 0x0400261D RID: 9757
			CsvStyle_Type,
			// Token: 0x0400261E RID: 9758
			CsvStyleQuoteAlways,
			// Token: 0x0400261F RID: 9759
			CsvStyleQuoteAfterDelimiter,
			// Token: 0x04002620 RID: 9760
			Count
		}

		// Token: 0x020009D3 RID: 2515
		public static class Csv
		{
			// Token: 0x020009D4 RID: 2516
			public sealed class DocumentFunctionValue : NativeFunctionValue5<TableValue, Value, Value, Value, Value, Value>
			{
				// Token: 0x06004781 RID: 18305 RVA: 0x000EF760 File Offset: 0x000ED960
				public DocumentFunctionValue(IEngineHost host)
					: base(TypeValue.Table, 1, "source", TypeValue.Any, "columns", TypeValue.Any, "delimiter", TypeValue.Any, "extraValues", NullableTypeValue.Number, "encoding", TextEncoding.Type.Nullable)
				{
					this.host = host;
				}

				// Token: 0x170016D1 RID: 5841
				// (get) Token: 0x06004782 RID: 18306 RVA: 0x000EF7B7 File Offset: 0x000ED9B7
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new FunctionTypeIdentity(typeof(LinesModule.Csv.DocumentFunctionValue));
					}
				}

				// Token: 0x06004783 RID: 18307 RVA: 0x000EF7C8 File Offset: 0x000ED9C8
				public override TableValue TypedInvoke(Value source, Value columns, Value delimiter, Value extraValues, Value encoding)
				{
					return new LinesModule.Csv.DocumentFunctionValue.InvokedFunctionTableValue(this.CreateTable(source, columns, delimiter, extraValues, encoding), this, new Value[] { source, columns, delimiter, extraValues, encoding });
				}

				// Token: 0x06004784 RID: 18308 RVA: 0x000EF7FC File Offset: 0x000ED9FC
				private TableValue CreateTable(Value source, Value columns, Value delimiter, Value extraValues, Value encoding)
				{
					Value quoteAlways = LinesModule.CsvStyle.QuoteAlways;
					Value csvEnum = LinesModule.QuoteStyle.CsvEnum;
					if (columns.IsRecord && delimiter.IsNull && extraValues.IsNull && encoding.IsNull)
					{
						LinesModule.Csv.DocumentFunctionValue.GetParameters(columns.AsRecord, out delimiter, out columns, out encoding, out quoteAlways, out csvEnum);
					}
					LinesModule.QuoteStyle quoteStyle = LinesModule.GetQuoteStyle(csvEnum);
					if (quoteAlways.Equals(LinesModule.CsvStyle.QuoteAfterDelimiter))
					{
						Value value = LinesModule.Splitter.ExcelSplitTextByDelimiter.Invoke(delimiter);
						if (quoteStyle == LinesModule.QuoteStyle.Csv)
						{
							quoteStyle = LinesModule.QuoteStyle.Excel(delimiter.AsText);
						}
						return this.CreateTable(source, columns, delimiter, encoding, value, quoteStyle, LinesModule.ExtraValues.Ignore);
					}
					if (quoteAlways.Equals(LinesModule.CsvStyle.QuoteAlways))
					{
						Value splitter = LinesModule.Csv.DocumentFunctionValue.GetSplitter(delimiter);
						return this.CreateTable(source, columns, delimiter, encoding, splitter, quoteStyle, extraValues);
					}
					throw ValueException.NewExpressionError<Message0>(Strings.InvalidCsvStyle, quoteAlways, null);
				}

				// Token: 0x06004785 RID: 18309 RVA: 0x000EF8C8 File Offset: 0x000EDAC8
				private TableValue CreateTable(Value source, Value columns, Value delimiter, Value encoding, Value splitter, LinesModule.QuoteStyle quoteStyle, Value extraValues)
				{
					Value value;
					if (source.IsText)
					{
						value = new LinesModule.Lines.FromTextFunctionValue(this.host).TypedInvoke(source.AsText, quoteStyle, LogicalValue.False);
					}
					else
					{
						if (!source.IsBinary)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.ContentTypeParameterValidationError, source, null);
						}
						value = new LinesModule.Lines.FromBinaryFunctionValue(this.host).TypedInvoke(source.AsBinary, quoteStyle, LogicalValue.False, encoding);
					}
					return ValueServices.AddFirstRowMayContainHeadersMeta(LinesModule.Table.FromList.Invoke(value, splitter, columns, TextValue.Empty, extraValues).AsTable).AsTable;
				}

				// Token: 0x06004786 RID: 18310 RVA: 0x000EF958 File Offset: 0x000EDB58
				private static Value GetSplitter(Value delimiter)
				{
					if (delimiter.IsNull)
					{
						return LinesModule.Splitter.SplitByComma;
					}
					if (delimiter.IsList)
					{
						return LinesModule.Splitter.SplitTextByPositions.Invoke(delimiter);
					}
					if (delimiter.AsString.Length == 0)
					{
						return LinesModule.Splitter.SplitTextByWhitespace.Invoke();
					}
					return LinesModule.Splitter.SplitTextByDelimiter.Invoke(delimiter);
				}

				// Token: 0x06004787 RID: 18311 RVA: 0x000EF9AC File Offset: 0x000EDBAC
				private static void GetParameters(RecordValue record, out Value delimiter, out Value columns, out Value encoding, out Value csvStyle, out Value quoteStyle)
				{
					HashSet<string> hashSet = new HashSet<string>(record.Keys);
					LinesModule.Csv.DocumentFunctionValue.GetParameter(record, hashSet, "Delimiter", TextValue.New(","), out delimiter);
					LinesModule.Csv.DocumentFunctionValue.GetParameter(record, hashSet, "Columns", Value.Null, out columns);
					if (columns.IsNull)
					{
						LinesModule.Csv.DocumentFunctionValue.GetParameter(record, hashSet, "Column", Value.Null, out columns);
					}
					LinesModule.Csv.DocumentFunctionValue.GetParameter(record, hashSet, "Encoding", Value.Null, out encoding);
					LinesModule.Csv.DocumentFunctionValue.GetParameter(record, hashSet, "CsvStyle", LinesModule.CsvStyle.QuoteAfterDelimiter, out csvStyle);
					LinesModule.Csv.DocumentFunctionValue.GetParameter(record, hashSet, "QuoteStyle", LinesModule.QuoteStyle.CsvEnum, out quoteStyle);
					if (hashSet.Count != 0)
					{
						string text = hashSet.First<string>();
						throw ValueException.NewExpressionError<Message1>(Strings.InvalidCsvParameter(text), record[text], null);
					}
				}

				// Token: 0x06004788 RID: 18312 RVA: 0x000EFA64 File Offset: 0x000EDC64
				private static void GetParameter(RecordValue record, HashSet<string> keys, string key, Value defaultValue, out Value value)
				{
					if (record.TryGetValue(key, out value))
					{
						keys.Remove(key);
						return;
					}
					value = defaultValue;
				}

				// Token: 0x04002621 RID: 9761
				private IEngineHost host;

				// Token: 0x020009D5 RID: 2517
				private class InvokedFunctionTableValue : DelegatingTableValue
				{
					// Token: 0x06004789 RID: 18313 RVA: 0x000EFA7E File Offset: 0x000EDC7E
					public InvokedFunctionTableValue(TableValue table, FunctionValue function, params Value[] arguments)
						: base(table)
					{
						this.function = function;
						this.arguments = arguments;
					}

					// Token: 0x0600478A RID: 18314 RVA: 0x000EFA95 File Offset: 0x000EDC95
					private TableValue New(TableValue table)
					{
						return new LinesModule.Csv.DocumentFunctionValue.InvokedFunctionTableValue(table, this.function, this.arguments);
					}

					// Token: 0x170016D2 RID: 5842
					// (get) Token: 0x0600478B RID: 18315 RVA: 0x000EFAAC File Offset: 0x000EDCAC
					public override IExpression Expression
					{
						get
						{
							if (this.expression == null)
							{
								ArrayBuilder<IExpression> arrayBuilder = default(ArrayBuilder<IExpression>);
								for (int i = this.arguments.Length - 1; i >= 0; i--)
								{
									if (arrayBuilder.Count > 0 || !this.arguments[i].IsNull || i < this.function.Type.AsFunctionType.Min)
									{
										arrayBuilder.Add(new ConstantExpressionSyntaxNode(this.arguments[i]));
									}
								}
								IExpression[] array = arrayBuilder.ToArray();
								Array.Reverse(array);
								this.expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(this.function), array);
							}
							return this.expression;
						}
					}

					// Token: 0x0600478C RID: 18316 RVA: 0x000EFB51 File Offset: 0x000EDD51
					public override TableValue Optimize()
					{
						return new OptimizedTableValue(this);
					}

					// Token: 0x0600478D RID: 18317 RVA: 0x000EFB59 File Offset: 0x000EDD59
					public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables)
					{
						return this.New(base.Table.ReplaceRelatedTables(relatedTables));
					}

					// Token: 0x0600478E RID: 18318 RVA: 0x000EFB6D File Offset: 0x000EDD6D
					public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables, ColumnIdentity[] columnIdentities, IList<Relationship> relationships)
					{
						return this.New(base.Table.ReplaceRelatedTables(relatedTables, columnIdentities, relationships));
					}

					// Token: 0x0600478F RID: 18319 RVA: 0x000EFB83 File Offset: 0x000EDD83
					public override TableValue ReplaceRelationshipIdentity(string identity)
					{
						return this.New(base.Table.ReplaceRelationshipIdentity(identity));
					}

					// Token: 0x06004790 RID: 18320 RVA: 0x000EFB97 File Offset: 0x000EDD97
					public override TableValue ReplaceColumnIdentities(ColumnIdentity[] columnIdentities)
					{
						return this.New(base.Table.ReplaceColumnIdentities(columnIdentities));
					}

					// Token: 0x06004791 RID: 18321 RVA: 0x000EFBAB File Offset: 0x000EDDAB
					public override TableValue ReplaceRelationships(IList<Relationship> relationships)
					{
						return this.New(base.Table.ReplaceRelationships(relationships));
					}

					// Token: 0x04002622 RID: 9762
					private readonly FunctionValue function;

					// Token: 0x04002623 RID: 9763
					private readonly Value[] arguments;

					// Token: 0x04002624 RID: 9764
					private IExpression expression;
				}
			}
		}

		// Token: 0x020009D6 RID: 2518
		private static class Lines
		{
			// Token: 0x04002625 RID: 9765
			public static readonly FunctionValue ToText = new LinesModule.Lines.ToTextFunctionValue();

			// Token: 0x04002626 RID: 9766
			public static readonly FunctionValue ToBinary = new LinesModule.Lines.ToBinaryFunctionValue();

			// Token: 0x020009D7 RID: 2519
			private class ToTextFunctionValue : NativeFunctionValue2<TextValue, ListValue, Value>
			{
				// Token: 0x06004793 RID: 18323 RVA: 0x000EFBD5 File Offset: 0x000EDDD5
				public ToTextFunctionValue()
					: base(TypeValue.Text, 1, "lines", TypeValue.List, "lineSeparator", NullableTypeValue.Text)
				{
				}

				// Token: 0x06004794 RID: 18324 RVA: 0x000EFBF8 File Offset: 0x000EDDF8
				public override TextValue TypedInvoke(ListValue list, Value lineSeparatorValue)
				{
					string text = (lineSeparatorValue.IsNull ? "\r\n" : lineSeparatorValue.AsString);
					StringBuilder stringBuilder = new StringBuilder();
					foreach (IValueReference valueReference in list)
					{
						stringBuilder.Append(valueReference.Value.AsString);
						stringBuilder.Append(text);
					}
					return TextValue.New(stringBuilder.ToString());
				}
			}

			// Token: 0x020009D8 RID: 2520
			private class ToBinaryFunctionValue : NativeFunctionValue4<BinaryValue, ListValue, Value, Value, Value>
			{
				// Token: 0x06004795 RID: 18325 RVA: 0x000EFC7C File Offset: 0x000EDE7C
				public ToBinaryFunctionValue()
					: base(TypeValue.Binary, 1, "lines", TypeValue.List, "lineSeparator", NullableTypeValue.Text, "encoding", TextEncoding.Type.Nullable, "includeByteOrderMark", NullableTypeValue.Logical)
				{
				}

				// Token: 0x06004796 RID: 18326 RVA: 0x000EFCC4 File Offset: 0x000EDEC4
				public override BinaryValue TypedInvoke(ListValue list, Value lineSeparatorValue, Value encodingValue, Value includeByteOrderMark)
				{
					string text = (lineSeparatorValue.IsNull ? "\r\n" : lineSeparatorValue.AsString);
					Encoding encoding = TextEncoding.GetEncoding(encodingValue, includeByteOrderMark, TextEncoding.CodePage.Utf8);
					return new LinesModule.Lines.ToBinaryFunctionValue.LinesToBinaryStreamProvider(list, encoding, text);
				}

				// Token: 0x020009D9 RID: 2521
				private class LinesToBinaryStreamProvider : StreamedBinaryValue
				{
					// Token: 0x06004797 RID: 18327 RVA: 0x000EFCFD File Offset: 0x000EDEFD
					public LinesToBinaryStreamProvider(ListValue list, Encoding encoding, string lineSeparator)
					{
						this.list = list;
						this.encoding = encoding;
						this.lineSeparator = lineSeparator;
					}

					// Token: 0x06004798 RID: 18328 RVA: 0x000EFD1A File Offset: 0x000EDF1A
					public override Stream Open()
					{
						return new LinesModule.Lines.ToBinaryFunctionValue.LinesToBinaryStreamProvider.LinesToBinaryStream(this.list.GetEnumerator(), this.encoding, this.lineSeparator);
					}

					// Token: 0x04002627 RID: 9767
					private ListValue list;

					// Token: 0x04002628 RID: 9768
					private Encoding encoding;

					// Token: 0x04002629 RID: 9769
					private string lineSeparator;

					// Token: 0x020009DA RID: 2522
					private class LinesToBinaryStream : Stream
					{
						// Token: 0x06004799 RID: 18329 RVA: 0x000EFD38 File Offset: 0x000EDF38
						public LinesToBinaryStream(IEnumerator<IValueReference> enumerator, Encoding encoding, string lineSeparator)
						{
							this.stream = new MemoryStream();
							this.writer = new StreamWriter(this.stream, encoding);
							this.enumerator = enumerator;
							this.lineSeparator = lineSeparator;
							this.buffer = new byte[1];
						}

						// Token: 0x170016D3 RID: 5843
						// (get) Token: 0x0600479A RID: 18330 RVA: 0x00002139 File Offset: 0x00000339
						public override bool CanRead
						{
							get
							{
								return true;
							}
						}

						// Token: 0x170016D4 RID: 5844
						// (get) Token: 0x0600479B RID: 18331 RVA: 0x00002105 File Offset: 0x00000305
						public override bool CanSeek
						{
							get
							{
								return false;
							}
						}

						// Token: 0x170016D5 RID: 5845
						// (get) Token: 0x0600479C RID: 18332 RVA: 0x00002105 File Offset: 0x00000305
						public override bool CanWrite
						{
							get
							{
								return false;
							}
						}

						// Token: 0x0600479D RID: 18333 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override void Flush()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x170016D6 RID: 5846
						// (get) Token: 0x0600479E RID: 18334 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override long Length
						{
							get
							{
								throw new InvalidOperationException();
							}
						}

						// Token: 0x170016D7 RID: 5847
						// (get) Token: 0x0600479F RID: 18335 RVA: 0x0000EE09 File Offset: 0x0000D009
						// (set) Token: 0x060047A0 RID: 18336 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override long Position
						{
							get
							{
								throw new InvalidOperationException();
							}
							set
							{
								throw new InvalidOperationException();
							}
						}

						// Token: 0x060047A1 RID: 18337 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override long Seek(long offset, SeekOrigin origin)
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060047A2 RID: 18338 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override void SetLength(long value)
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060047A3 RID: 18339 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override void Write(byte[] buffer, int offset, int count)
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060047A4 RID: 18340 RVA: 0x000EFD77 File Offset: 0x000EDF77
						public override int ReadByte()
						{
							if (this.Read(this.buffer, 0, 1) == 0)
							{
								return -1;
							}
							return (int)this.buffer[0];
						}

						// Token: 0x060047A5 RID: 18341 RVA: 0x000EFD94 File Offset: 0x000EDF94
						public override int Read(byte[] buffer, int offset, int count)
						{
							while (this.stream.Position == this.stream.Length)
							{
								this.stream.SetLength(0L);
								if (!this.enumerator.MoveNext())
								{
									return 0;
								}
								string asString = this.enumerator.Current.Value.AsString;
								this.writer.Write(asString);
								this.writer.Write(this.lineSeparator);
								this.writer.Flush();
								this.stream.Position = 0L;
							}
							return this.stream.Read(buffer, offset, count);
						}

						// Token: 0x060047A6 RID: 18342 RVA: 0x000EFE30 File Offset: 0x000EE030
						protected override void Dispose(bool disposing)
						{
							if (disposing)
							{
								this.enumerator.Dispose();
							}
							base.Dispose(disposing);
						}

						// Token: 0x0400262A RID: 9770
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x0400262B RID: 9771
						private MemoryStream stream;

						// Token: 0x0400262C RID: 9772
						private StreamWriter writer;

						// Token: 0x0400262D RID: 9773
						private string lineSeparator;

						// Token: 0x0400262E RID: 9774
						private byte[] buffer;
					}
				}
			}

			// Token: 0x020009DB RID: 2523
			public class FromTextFunctionValue : NativeFunctionValue3<ListValue, TextValue, Value, Value>
			{
				// Token: 0x060047A7 RID: 18343 RVA: 0x000EFE48 File Offset: 0x000EE048
				public FromTextFunctionValue(IEngineHost host)
					: base(TypeValue.List, 1, "text", TypeValue.Text, "quoteStyle", LinesModule.QuoteStyle.Type.Nullable, "includeLineSeparators", NullableTypeValue.Logical)
				{
					this.host = host;
				}

				// Token: 0x060047A8 RID: 18344 RVA: 0x000EFE8B File Offset: 0x000EE08B
				public override ListValue TypedInvoke(TextValue source, Value quoteStyle, Value includeLineSeparators)
				{
					return this.TypedInvoke(source, quoteStyle.IsNull ? LinesModule.QuoteStyle.None : LinesModule.GetQuoteStyle(quoteStyle), includeLineSeparators);
				}

				// Token: 0x060047A9 RID: 18345 RVA: 0x000EFEAC File Offset: 0x000EE0AC
				public ListValue TypedInvoke(TextValue source, LinesModule.QuoteStyle quoteStyle, Value includeLineSeparators)
				{
					LinesModule.Lines.TextListValue textListValue = new LinesModule.Lines.TextListValue(source, quoteStyle, !includeLineSeparators.IsNull && includeLineSeparators.AsBoolean, null);
					ISamplingService samplingService = SamplingService.GetSamplingService(this.host);
					if (samplingService.SamplingEnabled)
					{
						return new LinesModule.Lines.SamplingListValue(samplingService, textListValue);
					}
					return textListValue;
				}

				// Token: 0x0400262F RID: 9775
				private readonly IEngineHost host;
			}

			// Token: 0x020009DC RID: 2524
			public class FromBinaryFunctionValue : NativeFunctionValue4<ListValue, BinaryValue, Value, Value, Value>
			{
				// Token: 0x060047AA RID: 18346 RVA: 0x000EFEF0 File Offset: 0x000EE0F0
				public FromBinaryFunctionValue(IEngineHost host)
					: base(TypeValue.List, 1, "binary", TypeValue.Binary, "quoteStyle", LinesModule.QuoteStyle.Type.Nullable, "includeLineSeparators", NullableTypeValue.Logical, "encoding", TextEncoding.Type.Nullable)
				{
					this.host = host;
				}

				// Token: 0x060047AB RID: 18347 RVA: 0x000EFF42 File Offset: 0x000EE142
				public override ListValue TypedInvoke(BinaryValue source, Value quoteStyle, Value includeLineSeparators, Value encoding)
				{
					return this.TypedInvoke(source, quoteStyle.IsNull ? LinesModule.QuoteStyle.None : LinesModule.GetQuoteStyle(quoteStyle), includeLineSeparators, encoding);
				}

				// Token: 0x060047AC RID: 18348 RVA: 0x000EFF64 File Offset: 0x000EE164
				public ListValue TypedInvoke(BinaryValue source, LinesModule.QuoteStyle quoteStyle, Value includeLineSeparators, Value encoding)
				{
					LinesModule.Lines.TextListValue textListValue = new LinesModule.Lines.TextListValue(source, quoteStyle, !includeLineSeparators.IsNull && includeLineSeparators.AsBoolean, encoding.IsNull ? null : TextEncoding.GetEncoding(encoding, LogicalValue.False, TextEncoding.CodePage.Utf8));
					ISamplingService samplingService = SamplingService.GetSamplingService(this.host);
					if (samplingService.SamplingEnabled)
					{
						return new LinesModule.Lines.SamplingListValue(samplingService, textListValue);
					}
					return textListValue;
				}

				// Token: 0x04002630 RID: 9776
				private readonly IEngineHost host;
			}

			// Token: 0x020009DD RID: 2525
			private class SamplingListValue : StreamedListValue, ILines
			{
				// Token: 0x060047AD RID: 18349 RVA: 0x000EFFC4 File Offset: 0x000EE1C4
				public SamplingListValue(ISamplingService service, LinesModule.Lines.TextListValue list)
				{
					this.service = service;
					this.list = list;
				}

				// Token: 0x060047AE RID: 18350 RVA: 0x000EFFDA File Offset: 0x000EE1DA
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					return new LinesModule.Lines.SamplingListValue.SamplingEnumerator(this.service, this.list.GetEnumerator());
				}

				// Token: 0x060047AF RID: 18351 RVA: 0x000EFFF2 File Offset: 0x000EE1F2
				public ILineReader GetLineReader()
				{
					return new LinesModule.Lines.SamplingListValue.SamplingLineReader(this.service, this.list.GetLineReader());
				}

				// Token: 0x04002631 RID: 9777
				private readonly ISamplingService service;

				// Token: 0x04002632 RID: 9778
				private readonly LinesModule.Lines.TextListValue list;

				// Token: 0x020009DE RID: 2526
				private class SamplingEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
				{
					// Token: 0x060047B0 RID: 18352 RVA: 0x000F000A File Offset: 0x000EE20A
					public SamplingEnumerator(ISamplingService service, IEnumerator<IValueReference> enumerator)
					{
						this.service = service;
						this.enumerator = enumerator;
						this.count = service.SampleRowCount;
					}

					// Token: 0x170016D8 RID: 5848
					// (get) Token: 0x060047B1 RID: 18353 RVA: 0x000F002C File Offset: 0x000EE22C
					public IValueReference Current
					{
						get
						{
							return this.enumerator.Current;
						}
					}

					// Token: 0x170016D9 RID: 5849
					// (get) Token: 0x060047B2 RID: 18354 RVA: 0x000F0039 File Offset: 0x000EE239
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x060047B3 RID: 18355 RVA: 0x000F0041 File Offset: 0x000EE241
					public void Dispose()
					{
						this.enumerator.Dispose();
					}

					// Token: 0x060047B4 RID: 18356 RVA: 0x0000EE09 File Offset: 0x0000D009
					public void Reset()
					{
						throw new InvalidOperationException();
					}

					// Token: 0x060047B5 RID: 18357 RVA: 0x000F004E File Offset: 0x000EE24E
					public bool MoveNext()
					{
						if (this.count == 0)
						{
							this.service.RecordSampling();
							return false;
						}
						if (!this.enumerator.MoveNext())
						{
							return false;
						}
						this.count--;
						return true;
					}

					// Token: 0x04002633 RID: 9779
					private ISamplingService service;

					// Token: 0x04002634 RID: 9780
					private IEnumerator<IValueReference> enumerator;

					// Token: 0x04002635 RID: 9781
					private int count;
				}

				// Token: 0x020009DF RID: 2527
				private class SamplingLineReader : ILineReader, IDisposable
				{
					// Token: 0x060047B6 RID: 18358 RVA: 0x000F0083 File Offset: 0x000EE283
					public SamplingLineReader(ISamplingService service, ILineReader reader)
					{
						this.service = service;
						this.reader = reader;
						this.count = service.SampleRowCount;
					}

					// Token: 0x060047B7 RID: 18359 RVA: 0x000F00A5 File Offset: 0x000EE2A5
					public string GetLine()
					{
						return this.reader.GetLine();
					}

					// Token: 0x170016DA RID: 5850
					// (get) Token: 0x060047B8 RID: 18360 RVA: 0x000F00B2 File Offset: 0x000EE2B2
					public char[] Text
					{
						get
						{
							return this.reader.Text;
						}
					}

					// Token: 0x170016DB RID: 5851
					// (get) Token: 0x060047B9 RID: 18361 RVA: 0x000F00BF File Offset: 0x000EE2BF
					public int LineStart
					{
						get
						{
							return this.reader.LineStart;
						}
					}

					// Token: 0x170016DC RID: 5852
					// (get) Token: 0x060047BA RID: 18362 RVA: 0x000F00CC File Offset: 0x000EE2CC
					public int LineEnd
					{
						get
						{
							return this.reader.LineEnd;
						}
					}

					// Token: 0x170016DD RID: 5853
					// (get) Token: 0x060047BB RID: 18363 RVA: 0x000F00D9 File Offset: 0x000EE2D9
					public bool HasQuotes
					{
						get
						{
							return this.reader.HasQuotes;
						}
					}

					// Token: 0x060047BC RID: 18364 RVA: 0x000F00E6 File Offset: 0x000EE2E6
					public void Dispose()
					{
						this.reader.Dispose();
					}

					// Token: 0x060047BD RID: 18365 RVA: 0x000F00F3 File Offset: 0x000EE2F3
					public bool MoveNext()
					{
						if (this.count == 0)
						{
							this.service.RecordSampling();
							return false;
						}
						if (!this.reader.MoveNext())
						{
							return false;
						}
						this.count--;
						return true;
					}

					// Token: 0x04002636 RID: 9782
					private ISamplingService service;

					// Token: 0x04002637 RID: 9783
					private ILineReader reader;

					// Token: 0x04002638 RID: 9784
					private int count;
				}
			}

			// Token: 0x020009E0 RID: 2528
			private class TextListValue : StreamedListValue, ILines
			{
				// Token: 0x060047BE RID: 18366 RVA: 0x000F0128 File Offset: 0x000EE328
				public TextListValue(Value source, LinesModule.QuoteStyle quoteStyle, bool includeLineSeparators, Encoding encoding)
				{
					this.source = source;
					this.quoteStyle = quoteStyle;
					this.includeLineSeparators = includeLineSeparators;
					this.encoding = encoding;
				}

				// Token: 0x170016DE RID: 5854
				// (get) Token: 0x060047BF RID: 18367 RVA: 0x000F014D File Offset: 0x000EE34D
				public override TypeValue Type
				{
					get
					{
						return LinesModule.Lines.TextListValue.ListOfText;
					}
				}

				// Token: 0x060047C0 RID: 18368 RVA: 0x000F0154 File Offset: 0x000EE354
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					return new LinesModule.Lines.TextListEnumerator(this.GetLineReader());
				}

				// Token: 0x060047C1 RID: 18369 RVA: 0x000F0164 File Offset: 0x000EE364
				public ILineReader GetLineReader()
				{
					TextReader textReader;
					if (this.source.IsBinary)
					{
						textReader = this.source.AsBinary.OpenText(this.encoding);
					}
					else
					{
						textReader = new StringReader(this.source.AsString);
					}
					return LinesModule.Lines.TextListValue.GetLineReader(textReader, this.quoteStyle, this.includeLineSeparators);
				}

				// Token: 0x060047C2 RID: 18370 RVA: 0x000F01BA File Offset: 0x000EE3BA
				private static ILineReader GetLineReader(TextReader reader, LinesModule.QuoteStyle quoteStyle, bool includeLineSeparators)
				{
					return quoteStyle.CreateLineReader(reader, includeLineSeparators);
				}

				// Token: 0x04002639 RID: 9785
				private static readonly ListTypeValue ListOfText = ListTypeValue.New(TypeValue.Text);

				// Token: 0x0400263A RID: 9786
				private Value source;

				// Token: 0x0400263B RID: 9787
				private LinesModule.QuoteStyle quoteStyle;

				// Token: 0x0400263C RID: 9788
				private bool includeLineSeparators;

				// Token: 0x0400263D RID: 9789
				private Encoding encoding;
			}

			// Token: 0x020009E1 RID: 2529
			private class TextListEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x060047C4 RID: 18372 RVA: 0x000F01D5 File Offset: 0x000EE3D5
				public TextListEnumerator(ILineReader reader)
				{
					this.reader = reader;
				}

				// Token: 0x170016DF RID: 5855
				// (get) Token: 0x060047C5 RID: 18373 RVA: 0x000F01E4 File Offset: 0x000EE3E4
				public IValueReference Current
				{
					get
					{
						if (this.current == null)
						{
							this.current = TextValue.New(this.reader.GetLine());
						}
						return this.current;
					}
				}

				// Token: 0x060047C6 RID: 18374 RVA: 0x000F020A File Offset: 0x000EE40A
				public void Dispose()
				{
					this.reader.Dispose();
				}

				// Token: 0x170016E0 RID: 5856
				// (get) Token: 0x060047C7 RID: 18375 RVA: 0x000F0217 File Offset: 0x000EE417
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060047C8 RID: 18376 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x060047C9 RID: 18377 RVA: 0x000F021F File Offset: 0x000EE41F
				public bool MoveNext()
				{
					this.current = null;
					return this.reader.MoveNext();
				}

				// Token: 0x0400263E RID: 9790
				private ILineReader reader;

				// Token: 0x0400263F RID: 9791
				private IValueReference current;
			}
		}

		// Token: 0x020009E2 RID: 2530
		public static class List
		{
			// Token: 0x04002640 RID: 9792
			public static readonly FunctionValue Normalize = new LinesModule.List.NormalizeFunctionValue();

			// Token: 0x020009E3 RID: 2531
			private class NormalizeFunctionValue : NativeFunctionValue4<ListValue, ListValue, NumberValue, Value, Value>
			{
				// Token: 0x060047CB RID: 18379 RVA: 0x000F0240 File Offset: 0x000EE440
				public NormalizeFunctionValue()
					: base(TypeValue.List, 2, "list", TypeValue.List, "count", TypeValue.Number, "default", TypeValue.Any, "extraValues", LinesModule.ExtraValues.Type.Nullable)
				{
				}

				// Token: 0x060047CC RID: 18380 RVA: 0x000F0288 File Offset: 0x000EE488
				public override ListValue TypedInvoke(ListValue list, NumberValue countValue, Value defaultValue, Value extraValues)
				{
					int asInteger = countValue.AsInteger32;
					LinesModule.ExtraValuesEnum extraValuesEnum = (extraValues.IsNull ? LinesModule.ExtraValuesEnum.Error : LinesModule.ExtraValues.Type.GetValue(extraValues.AsNumber));
					if (asInteger < 0 || (asInteger == 0 && extraValuesEnum == LinesModule.ExtraValuesEnum.List))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.List_Normalize_Count_Invalid, countValue, null);
					}
					return LinesModule.Normalizer.New(asInteger, defaultValue, extraValuesEnum).Normalize(list);
				}
			}
		}

		// Token: 0x020009E4 RID: 2532
		public static class Table
		{
			// Token: 0x04002641 RID: 9793
			public static readonly FunctionValue FromList = new LinesModule.Table.FromListFunctionValue();

			// Token: 0x04002642 RID: 9794
			public static readonly FunctionValue ToList = new LinesModule.Table.ToListFunctionValue();

			// Token: 0x020009E5 RID: 2533
			private class FromListFunctionValue : NativeFunctionValue5<TableValue, ListValue, Value, Value, Value, Value>
			{
				// Token: 0x060047CE RID: 18382 RVA: 0x000F02F8 File Offset: 0x000EE4F8
				public FromListFunctionValue()
					: base(TypeValue.Table, 1, "list", TypeValue.List, "splitter", NullableTypeValue.Function, "columns", TypeValue.Any, "default", TypeValue.Any, "extraValues", LinesModule.ExtraValues.Type.Nullable)
				{
				}

				// Token: 0x060047CF RID: 18383 RVA: 0x000F0348 File Offset: 0x000EE548
				public override TableValue TypedInvoke(ListValue list, Value splitter, Value columns, Value defaultValue, Value extraValuesValue)
				{
					FunctionValue functionValue = (splitter.IsNull ? LinesModule.Splitter.SplitByComma : splitter.AsFunction);
					LinesModule.ExtraValuesEnum extraValuesEnum = (extraValuesValue.IsNull ? LinesModule.ExtraValuesEnum.Error : LinesModule.ExtraValues.Type.GetValue(extraValuesValue.AsNumber));
					bool flag = LinesModule.Table.FromListFunctionValue.IsTextToTextSplitter(splitter);
					TypeValue typeValue = (flag ? DataSource.NullableSerializedTextType : null);
					TableTypeValue tableTypeValue = TableTypeValue.FromValue(columns.IsNull ? LinesModule.Table.FromListFunctionValue.GetColumns(list, functionValue, extraValuesEnum) : columns, typeValue);
					RecordValue tableTypeFields = tableTypeValue.ItemType.Fields;
					Keys keys = tableTypeFields.Keys;
					if (extraValuesEnum == LinesModule.ExtraValuesEnum.List)
					{
						if (keys.Length == 0)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.Table_FromList_ExtraValueListRequiresAtLeastOneColumn, tableTypeValue, null);
						}
						int lastColumn = keys.Length - 1;
						RecordValue lastColumnTypeField = RecordValue.New(RecordTypeValue.New(Keys.New("Type", "Optional")), new Value[]
						{
							flag ? ListTypeValue.Text : ListTypeValue.Any,
							LogicalValue.True
						});
						tableTypeValue = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(keys, delegate(int i)
						{
							if (i != lastColumn)
							{
								return tableTypeFields[i];
							}
							return lastColumnTypeField;
						})));
					}
					return new LinesModule.Table.FromListFunctionValue.FromListTableValue(tableTypeValue, list, functionValue, splitter.IsNull ? TextValue.Empty : defaultValue, extraValuesEnum, new ColumnSelection(keys), tableTypeValue, RowRange.All, ColumnTransforms.None);
				}

				// Token: 0x060047D0 RID: 18384 RVA: 0x000F0498 File Offset: 0x000EE698
				private static bool IsTextToTextSplitter(Value splitter)
				{
					Value value;
					return splitter.TryGetMetaField("Text.ToText", out value) && value.IsLogical && value.AsBoolean;
				}

				// Token: 0x060047D1 RID: 18385 RVA: 0x000F04C4 File Offset: 0x000EE6C4
				private static Value GetColumns(ListValue lines, FunctionValue splitter, LinesModule.ExtraValuesEnum extraValues)
				{
					int num;
					using (IEnumerator<IValueReference> enumerator = lines.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							num = splitter.Invoke(enumerator.Current.Value).AsList.Count;
						}
						else
						{
							num = 0;
						}
					}
					if (extraValues == LinesModule.ExtraValuesEnum.List)
					{
						num++;
					}
					return NumberValue.New(num);
				}

				// Token: 0x020009E6 RID: 2534
				private class FromListTableValue : TableValue
				{
					// Token: 0x060047D2 RID: 18386 RVA: 0x000F052C File Offset: 0x000EE72C
					public FromListTableValue(TableTypeValue tableType, ListValue list, FunctionValue splitter, Value defaultValue, LinesModule.ExtraValuesEnum extraValues, ColumnSelection columnSelection, TableTypeValue selectedType, RowRange rowRange, ColumnTransforms transforms)
					{
						this.tableType = tableType;
						this.list = list;
						this.splitter = splitter;
						this.defaultValue = defaultValue;
						this.extraValues = extraValues;
						this.columnSelection = columnSelection;
						this.selectedType = selectedType;
						this.rowRange = rowRange;
						this.transforms = transforms;
					}

					// Token: 0x170016E1 RID: 5857
					// (get) Token: 0x060047D3 RID: 18387 RVA: 0x000F0584 File Offset: 0x000EE784
					public override Keys Columns
					{
						get
						{
							return this.columnSelection.Keys;
						}
					}

					// Token: 0x170016E2 RID: 5858
					// (get) Token: 0x060047D4 RID: 18388 RVA: 0x000F0594 File Offset: 0x000EE794
					public override TypeValue Type
					{
						get
						{
							if (this.selectedType == null)
							{
								Value[] array = new Value[this.columnSelection.Keys.Length];
								for (int i = 0; i < array.Length; i++)
								{
									ColumnTransform columnTransform;
									if (this.transforms.Dictionary.TryGetValue(i, out columnTransform))
									{
										array[i] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new IValueReference[]
										{
											columnTransform.Type,
											LogicalValue.False
										});
									}
									else
									{
										array[i] = this.tableType.ItemType.Fields[this.columnSelection.GetColumn(i)];
									}
								}
								RecordTypeValue recordTypeValue = RecordTypeValue.New(RecordValue.New(this.columnSelection.Keys, array));
								this.selectedType = TableTypeValue.New(recordTypeValue);
							}
							return this.selectedType;
						}
					}

					// Token: 0x060047D5 RID: 18389 RVA: 0x000F065C File Offset: 0x000EE85C
					public override TableValue Skip(RowCount count)
					{
						return new LinesModule.Table.FromListFunctionValue.FromListTableValue(this.tableType, this.list, this.splitter, this.defaultValue, this.extraValues, this.columnSelection, this.selectedType, this.rowRange.Skip(count), this.transforms);
					}

					// Token: 0x060047D6 RID: 18390 RVA: 0x000F06AC File Offset: 0x000EE8AC
					public override TableValue Take(RowCount count)
					{
						return new LinesModule.Table.FromListFunctionValue.FromListTableValue(this.tableType, this.list, this.splitter, this.defaultValue, this.extraValues, this.columnSelection, this.selectedType, this.rowRange.Take(count), this.transforms);
					}

					// Token: 0x060047D7 RID: 18391 RVA: 0x000F06FC File Offset: 0x000EE8FC
					public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
					{
						table = new LinesModule.Table.FromListFunctionValue.FromListTableValue(this.tableType, this.list, this.splitter, this.defaultValue, this.extraValues, this.columnSelection.SelectColumns(columnSelection), null, this.rowRange, this.transforms.SelectColumns(columnSelection));
						return true;
					}

					// Token: 0x060047D8 RID: 18392 RVA: 0x000F0750 File Offset: 0x000EE950
					public override TableValue TransformColumns(ColumnTransforms transforms)
					{
						return new LinesModule.Table.FromListFunctionValue.FromListTableValue(this.tableType, this.list, this.splitter, this.defaultValue, this.extraValues, this.columnSelection, null, this.rowRange, this.transforms.TransformColumns(transforms));
					}

					// Token: 0x170016E3 RID: 5859
					// (get) Token: 0x060047D9 RID: 18393 RVA: 0x000F079C File Offset: 0x000EE99C
					private LinesModule.SelectIndex[] SelectIndices
					{
						get
						{
							if (this.selectIndices == null)
							{
								Keys keys = this.columnSelection.Keys;
								LinesModule.SelectIndex[] array = new LinesModule.SelectIndex[keys.Length];
								for (int i = 0; i < keys.Length; i++)
								{
									array[i] = new LinesModule.SelectIndex(this.columnSelection.GetColumn(i), i);
								}
								Array.Sort<LinesModule.SelectIndex>(array);
								this.selectIndices = array;
							}
							return this.selectIndices;
						}
					}

					// Token: 0x060047DA RID: 18394 RVA: 0x000F0808 File Offset: 0x000EEA08
					public override IPageReader GetReader()
					{
						RecordTypeValue itemType = this.Type.AsTableType.ItemType;
						return new DataReaderPageReader(new TableDataReader(this.Type.AsTableType, new FieldReaderDataReader(this.GetFieldReader(), itemType.Fields.Keys.Length), null), new DataReaderPageReader.ExceptionPropertyGetter(PageExceptionSerializer.TryGetPropertiesFromException));
					}

					// Token: 0x060047DB RID: 18395 RVA: 0x000F0863 File Offset: 0x000EEA63
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new LinesModule.Table.FromListFunctionValue.FromListTableValue.FromListEnumerator(this.GetFieldReader(), this.Type.AsTableType.ItemType.Fields.Keys);
					}

					// Token: 0x060047DC RID: 18396 RVA: 0x000F088C File Offset: 0x000EEA8C
					private IFieldReader<IValueReference> GetFieldReader()
					{
						ILines lines = this.list as ILines;
						ILineSplitter lineSplitter;
						IFieldReader<IValueReference> fieldReader;
						if (lines != null && this.splitter.TryGetAs<ILineSplitter>(out lineSplitter))
						{
							fieldReader = lineSplitter.GetFieldReader(lines.GetLineReader());
						}
						else
						{
							fieldReader = new LinesModule.Table.FromListFunctionValue.FromListTableValue.ListSplitterFieldReader(this.list, this.splitter);
						}
						int length = this.tableType.ItemType.Fields.Keys.Length;
						if (this.extraValues == LinesModule.ExtraValuesEnum.List)
						{
							fieldReader = new ExtraFieldsFieldReader<IValueReference>(fieldReader, length, this.defaultValue, (IValueReference[] values) => ListValue.New(values));
						}
						else
						{
							bool flag = this.extraValues == LinesModule.ExtraValuesEnum.Ignore;
							fieldReader = new PaddedFieldReader<IValueReference>(fieldReader, length, this.defaultValue, flag);
						}
						if (!this.rowRange.IsAll)
						{
							fieldReader = new SkipTakeFieldReader<IValueReference>(fieldReader, this.rowRange);
						}
						LinesModule.SelectIndex[] array = this.SelectIndices;
						if (array.Length != length)
						{
							int[] array2 = array.Select((LinesModule.SelectIndex x) => x.Source).ToArray<int>();
							fieldReader = new SelectColumnsFieldReader<IValueReference>(fieldReader, array2);
						}
						if (!LinesModule.Table.FromListFunctionValue.FromListTableValue.InOrder(array))
						{
							int[] array3 = array.Select((LinesModule.SelectIndex x) => x.Target).ToArray<int>();
							fieldReader = new PermuteColumnsFieldReader<IValueReference>(fieldReader, array3);
						}
						IDictionary<int, ColumnTransform> dictionary = this.transforms.Dictionary;
						if (dictionary.Count > 0)
						{
							FunctionValue[] array4 = new FunctionValue[array.Length];
							for (int i = 0; i < array4.Length; i++)
							{
								ColumnTransform columnTransform;
								if (dictionary.TryGetValue(i, out columnTransform))
								{
									array4[i] = columnTransform.Function;
								}
							}
							fieldReader = new LinesModule.Table.FromListFunctionValue.FromListTableValue.TransformFieldsReader(fieldReader, array4);
						}
						return fieldReader;
					}

					// Token: 0x060047DD RID: 18397 RVA: 0x000F0A3C File Offset: 0x000EEC3C
					private static bool InOrder(LinesModule.SelectIndex[] indices)
					{
						for (int i = 0; i < indices.Length - 1; i++)
						{
							if (indices[i].Target > indices[i + 1].Target)
							{
								return false;
							}
						}
						return true;
					}

					// Token: 0x04002643 RID: 9795
					private TableTypeValue tableType;

					// Token: 0x04002644 RID: 9796
					private ListValue list;

					// Token: 0x04002645 RID: 9797
					private FunctionValue splitter;

					// Token: 0x04002646 RID: 9798
					private Value defaultValue;

					// Token: 0x04002647 RID: 9799
					private LinesModule.ExtraValuesEnum extraValues;

					// Token: 0x04002648 RID: 9800
					private ColumnSelection columnSelection;

					// Token: 0x04002649 RID: 9801
					private LinesModule.SelectIndex[] selectIndices;

					// Token: 0x0400264A RID: 9802
					private TableTypeValue selectedType;

					// Token: 0x0400264B RID: 9803
					private RowRange rowRange;

					// Token: 0x0400264C RID: 9804
					private ColumnTransforms transforms;

					// Token: 0x020009E7 RID: 2535
					private class FromListEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x060047DE RID: 18398 RVA: 0x000F0A78 File Offset: 0x000EEC78
						public FromListEnumerator(IFieldReader<IValueReference> reader, Keys keys)
						{
							this.reader = reader;
							this.keys = keys;
						}

						// Token: 0x170016E4 RID: 5860
						// (get) Token: 0x060047DF RID: 18399 RVA: 0x000F0A90 File Offset: 0x000EEC90
						public IValueReference Current
						{
							get
							{
								if (this.row == null)
								{
									try
									{
										IValueReference[] array = new IValueReference[this.keys.Length];
										for (int i = 0; i < this.keys.Length; i++)
										{
											if (!this.reader.MoveNextField())
											{
												throw new InvalidOperationException();
											}
											array[i] = this.reader.Current;
										}
										if (this.reader.MoveNextField())
										{
											throw new InvalidOperationException();
										}
										this.row = RecordValue.New(this.keys, array);
									}
									catch (ValueException ex)
									{
										this.row = new ExceptionValueReference(ex);
									}
								}
								return this.row;
							}
						}

						// Token: 0x060047E0 RID: 18400 RVA: 0x000F0B3C File Offset: 0x000EED3C
						public void Dispose()
						{
							this.reader.Dispose();
						}

						// Token: 0x170016E5 RID: 5861
						// (get) Token: 0x060047E1 RID: 18401 RVA: 0x000F0B49 File Offset: 0x000EED49
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x060047E2 RID: 18402 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060047E3 RID: 18403 RVA: 0x000F0B51 File Offset: 0x000EED51
						public bool MoveNext()
						{
							this.row = null;
							return this.reader.MoveNextRow();
						}

						// Token: 0x0400264D RID: 9805
						private IFieldReader<IValueReference> reader;

						// Token: 0x0400264E RID: 9806
						private Keys keys;

						// Token: 0x0400264F RID: 9807
						private IValueReference row;
					}

					// Token: 0x020009E8 RID: 2536
					private class ListSplitterFieldReader : IFieldReader<IValueReference>, IDisposable
					{
						// Token: 0x060047E4 RID: 18404 RVA: 0x000F0B65 File Offset: 0x000EED65
						public ListSplitterFieldReader(ListValue list, FunctionValue splitter)
						{
							this.listEnumerator = list.GetEnumerator();
							this.splitter = splitter;
						}

						// Token: 0x170016E6 RID: 5862
						// (get) Token: 0x060047E5 RID: 18405 RVA: 0x000F0B80 File Offset: 0x000EED80
						public IValueReference Current
						{
							get
							{
								return this.fieldEnumerator.Current;
							}
						}

						// Token: 0x060047E6 RID: 18406 RVA: 0x000F0B8D File Offset: 0x000EED8D
						public bool MoveNextRow()
						{
							if (this.fieldEnumerator != null)
							{
								this.fieldEnumerator.Dispose();
								this.fieldEnumerator = null;
							}
							return this.listEnumerator.MoveNext();
						}

						// Token: 0x060047E7 RID: 18407 RVA: 0x000F0BB4 File Offset: 0x000EEDB4
						public bool MoveNextField()
						{
							if (this.fieldEnumerator == null)
							{
								this.fieldEnumerator = this.splitter.Invoke(this.listEnumerator.Current.Value).AsList.GetEnumerator();
							}
							return this.fieldEnumerator.MoveNext();
						}

						// Token: 0x060047E8 RID: 18408 RVA: 0x000F0BF4 File Offset: 0x000EEDF4
						public void Dispose()
						{
							this.listEnumerator.Dispose();
							if (this.fieldEnumerator != null)
							{
								this.fieldEnumerator.Dispose();
								this.fieldEnumerator = null;
							}
						}

						// Token: 0x04002650 RID: 9808
						private IEnumerator<IValueReference> listEnumerator;

						// Token: 0x04002651 RID: 9809
						private IEnumerator<IValueReference> fieldEnumerator;

						// Token: 0x04002652 RID: 9810
						private FunctionValue splitter;
					}

					// Token: 0x020009E9 RID: 2537
					private class TransformFieldsReader : IFieldReader<IValueReference>, IDisposable
					{
						// Token: 0x060047E9 RID: 18409 RVA: 0x000F0C1B File Offset: 0x000EEE1B
						public TransformFieldsReader(IFieldReader<IValueReference> reader, FunctionValue[] functions)
						{
							this.reader = reader;
							this.functions = functions;
						}

						// Token: 0x170016E7 RID: 5863
						// (get) Token: 0x060047EA RID: 18410 RVA: 0x000F0C34 File Offset: 0x000EEE34
						public IValueReference Current
						{
							get
							{
								if (this.current == null)
								{
									IValueReference valueReference = this.reader.Current;
									FunctionValue functionValue = this.functions[this.fieldIndex];
									if (functionValue != null)
									{
										this.current = new TransformValueReference(valueReference, functionValue);
									}
									else
									{
										this.current = valueReference;
									}
								}
								return this.current;
							}
						}

						// Token: 0x060047EB RID: 18411 RVA: 0x000F0C82 File Offset: 0x000EEE82
						public bool MoveNextRow()
						{
							this.fieldIndex = -1;
							return this.reader.MoveNextRow();
						}

						// Token: 0x060047EC RID: 18412 RVA: 0x000F0C96 File Offset: 0x000EEE96
						public bool MoveNextField()
						{
							this.current = null;
							if (!this.reader.MoveNextField())
							{
								return false;
							}
							this.fieldIndex++;
							return true;
						}

						// Token: 0x060047ED RID: 18413 RVA: 0x000F0CBD File Offset: 0x000EEEBD
						public void Dispose()
						{
							this.reader.Dispose();
						}

						// Token: 0x04002653 RID: 9811
						private IFieldReader<IValueReference> reader;

						// Token: 0x04002654 RID: 9812
						private FunctionValue[] functions;

						// Token: 0x04002655 RID: 9813
						private IValueReference current;

						// Token: 0x04002656 RID: 9814
						private int fieldIndex;
					}
				}
			}

			// Token: 0x020009EC RID: 2540
			private class ToListFunctionValue : NativeFunctionValue2<ListValue, TableValue, Value>
			{
				// Token: 0x060047F5 RID: 18421 RVA: 0x000F0D0E File Offset: 0x000EEF0E
				public ToListFunctionValue()
					: base(TypeValue.List, 1, "table", TypeValue.Table, "combiner", NullableTypeValue.Function)
				{
				}

				// Token: 0x060047F6 RID: 18422 RVA: 0x000F0D30 File Offset: 0x000EEF30
				public override ListValue TypedInvoke(TableValue table, Value combiner)
				{
					if (combiner.IsNull)
					{
						combiner = LinesModule.Combiner.CombineByComma;
					}
					return new LinesModule.Table.ToListFunctionValue.ToListListValue(table, combiner.AsFunction, table.Type.AsTableType.ItemType.Fields.Keys.Length);
				}

				// Token: 0x020009ED RID: 2541
				private class ToListListValue : StreamedListValue
				{
					// Token: 0x060047F7 RID: 18423 RVA: 0x000F0D6C File Offset: 0x000EEF6C
					public ToListListValue(TableValue table, FunctionValue combiner, int count)
					{
						this.table = table;
						this.combiner = combiner;
						this.count = count;
					}

					// Token: 0x060047F8 RID: 18424 RVA: 0x000F0D89 File Offset: 0x000EEF89
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new LinesModule.Table.ToListFunctionValue.ToListListValue.TableToLinesEnumerator(this);
					}

					// Token: 0x0400265E RID: 9822
					private TableValue table;

					// Token: 0x0400265F RID: 9823
					private FunctionValue combiner;

					// Token: 0x04002660 RID: 9824
					private int count;

					// Token: 0x020009EE RID: 2542
					private class TableToLinesEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x060047F9 RID: 18425 RVA: 0x000F0D91 File Offset: 0x000EEF91
						public TableToLinesEnumerator(LinesModule.Table.ToListFunctionValue.ToListListValue linesList)
						{
							this.linesList = linesList;
							this.enumerator = linesList.table.GetEnumerator();
						}

						// Token: 0x170016E8 RID: 5864
						// (get) Token: 0x060047FA RID: 18426 RVA: 0x000F0DB4 File Offset: 0x000EEFB4
						public IValueReference Current
						{
							get
							{
								if (this.current == null)
								{
									RecordValue asRecord = this.enumerator.Current.Value.AsRecord;
									IValueReference[] array = new IValueReference[this.linesList.count];
									for (int i = 0; i < array.Length; i++)
									{
										array[i] = asRecord.GetReference(i);
									}
									ListValue listValue = ListValue.New(array);
									this.current = this.linesList.combiner.Invoke(listValue);
								}
								return this.current;
							}
						}

						// Token: 0x060047FB RID: 18427 RVA: 0x000F0E2C File Offset: 0x000EF02C
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x170016E9 RID: 5865
						// (get) Token: 0x060047FC RID: 18428 RVA: 0x000F0E39 File Offset: 0x000EF039
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x060047FD RID: 18429 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060047FE RID: 18430 RVA: 0x000F0E41 File Offset: 0x000EF041
						public bool MoveNext()
						{
							this.current = null;
							return this.enumerator.MoveNext();
						}

						// Token: 0x04002661 RID: 9825
						private LinesModule.Table.ToListFunctionValue.ToListListValue linesList;

						// Token: 0x04002662 RID: 9826
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x04002663 RID: 9827
						private IValueReference current;
					}
				}
			}
		}

		// Token: 0x020009EF RID: 2543
		private abstract class SplitFunctionValue : NativeFunctionValue1
		{
			// Token: 0x060047FF RID: 18431 RVA: 0x000F0E55 File Offset: 0x000EF055
			protected SplitFunctionValue()
				: base("line")
			{
			}
		}

		// Token: 0x020009F0 RID: 2544
		public static class Splitter
		{
			// Token: 0x06004800 RID: 18432 RVA: 0x000F0E64 File Offset: 0x000EF064
			private static FunctionValue GetRangeSplitter(LinesModule.Range[] ranges, Value endToStart)
			{
				FunctionValue functionValue = new LinesModule.Splitter.RangeSplitFunctionValue(ranges);
				if (!endToStart.IsNull && endToStart.AsBoolean)
				{
					functionValue = new LinesModule.Splitter.EndToStartSplitFunctionValue(functionValue);
				}
				return functionValue.NewMeta(LinesModule.TextToTextMeta).AsFunction;
			}

			// Token: 0x06004801 RID: 18433 RVA: 0x000F0EA0 File Offset: 0x000EF0A0
			private static FunctionValue GetRepeatedLengthSplitter(int length, Value endToStart)
			{
				FunctionValue functionValue = new LinesModule.Splitter.RepeatedLengthSplitFunctionValue(length);
				if (!endToStart.IsNull && endToStart.AsBoolean)
				{
					functionValue = new LinesModule.Splitter.EndToStartSplitFunctionValue(functionValue);
				}
				return functionValue.NewMeta(LinesModule.TextToTextMeta).AsFunction;
			}

			// Token: 0x04002664 RID: 9828
			public static readonly FunctionValue SplitByNothing = new LinesModule.Splitter.SplitByNothingFunctionValue();

			// Token: 0x04002665 RID: 9829
			public static readonly FunctionValue SplitTextByCharacterTransition = new LinesModule.Splitter.SplitTextByCharacterTransitionFunctionValue();

			// Token: 0x04002666 RID: 9830
			public static readonly FunctionValue SplitTextByDelimiter = new LinesModule.Splitter.SplitTextByDelimiterFunctionValue();

			// Token: 0x04002667 RID: 9831
			public static readonly FunctionValue SplitTextByEachDelimiter = new LinesModule.Splitter.SplitTextByEachDelimiterFunctionValue();

			// Token: 0x04002668 RID: 9832
			public static readonly FunctionValue SplitTextByAnyDelimiter = new LinesModule.Splitter.SplitTextByAnyDelimiterFunctionValue();

			// Token: 0x04002669 RID: 9833
			public static readonly FunctionValue SplitTextByWhitespace = new LinesModule.Splitter.SplitTextByWhitespaceFunctionValue();

			// Token: 0x0400266A RID: 9834
			public static readonly FunctionValue SplitTextByRanges = new LinesModule.Splitter.SplitTextByRangesFunctionValue();

			// Token: 0x0400266B RID: 9835
			public static readonly FunctionValue SplitTextByPositions = new LinesModule.Splitter.SplitTextByPositionsFunctionValue();

			// Token: 0x0400266C RID: 9836
			public static readonly FunctionValue SplitTextByRepeatedLengths = new LinesModule.Splitter.SplitTextByRepeatedLengthsFunctionValue();

			// Token: 0x0400266D RID: 9837
			public static readonly FunctionValue SplitTextByLengths = new LinesModule.Splitter.SplitTextByLengthsFunctionValue();

			// Token: 0x0400266E RID: 9838
			public static readonly FunctionValue ExcelSplitTextByDelimiter = new LinesModule.Splitter.ExcelSplitTextByDelimiterFunctionValue();

			// Token: 0x0400266F RID: 9839
			public static readonly FunctionValue SplitByComma = new FunctionResultFunctionValue(new LinesModule.Splitter.DelimitedSplitFunctionValue((ILineReader reader) => new SingleCharacterDelimitedFieldReader(reader, true, ',')), LinesModule.Splitter.SplitTextByDelimiter, new Value[]
			{
				TextValue.New(","),
				LinesModule.QuoteStyle.Csv.Value
			}).NewMeta(LinesModule.TextToTextMeta).AsFunction;

			// Token: 0x020009F1 RID: 2545
			private class EndToStartSplitFunctionValue : LinesModule.SplitFunctionValue
			{
				// Token: 0x06004803 RID: 18435 RVA: 0x000F0FAA File Offset: 0x000EF1AA
				public EndToStartSplitFunctionValue(FunctionValue splitter)
				{
					this.splitter = splitter;
				}

				// Token: 0x06004804 RID: 18436 RVA: 0x000F0FBC File Offset: 0x000EF1BC
				private ListValue Reverse(ListValue list)
				{
					Value[] array = list.ToArray();
					Value[] array2 = new Value[array.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[array2.Length - i - 1] = TextValue.New(LinesModule.ReverseString(array[i].AsString));
					}
					return ListValue.New(array2);
				}

				// Token: 0x06004805 RID: 18437 RVA: 0x000F1008 File Offset: 0x000EF208
				public override Value Invoke(Value line)
				{
					if (line.IsNull)
					{
						return ListValue.Empty;
					}
					return this.Reverse(this.splitter.Invoke(TextValue.New(LinesModule.ReverseString(line.AsString))).AsList);
				}

				// Token: 0x04002670 RID: 9840
				private readonly FunctionValue splitter;
			}

			// Token: 0x020009F2 RID: 2546
			private class DelimitedSplitFunctionValue : LinesModule.SplitFunctionValue, ILineSplitter
			{
				// Token: 0x06004806 RID: 18438 RVA: 0x000F103E File Offset: 0x000EF23E
				public DelimitedSplitFunctionValue(Func<ILineReader, IFieldReader<IValueReference>> getFieldReader)
				{
					this.getFieldReader = getFieldReader;
				}

				// Token: 0x06004807 RID: 18439 RVA: 0x000F1050 File Offset: 0x000EF250
				public override Value Invoke(Value line)
				{
					if (line.IsNull)
					{
						return ListValue.Empty;
					}
					ILineReader lineReader = new StringLineReader(line.AsString);
					IFieldReader<IValueReference> fieldReader = this.getFieldReader(lineReader);
					if (!fieldReader.MoveNextRow())
					{
						throw new InvalidOperationException();
					}
					List<Value> list = new List<Value>();
					while (fieldReader.MoveNextField())
					{
						list.Add(fieldReader.Current.Value);
					}
					return ListValue.New(list.ToArray());
				}

				// Token: 0x06004808 RID: 18440 RVA: 0x000F10BE File Offset: 0x000EF2BE
				public IFieldReader<IValueReference> GetFieldReader(ILineReader lineReader)
				{
					return this.getFieldReader(lineReader);
				}

				// Token: 0x04002671 RID: 9841
				private readonly Func<ILineReader, IFieldReader<IValueReference>> getFieldReader;
			}

			// Token: 0x020009F3 RID: 2547
			private class SplitByNothingFunctionValue : NativeFunctionValue0<FunctionValue>
			{
				// Token: 0x06004809 RID: 18441 RVA: 0x000F10CC File Offset: 0x000EF2CC
				public SplitByNothingFunctionValue()
					: base(TypeValue.Function)
				{
				}

				// Token: 0x0600480A RID: 18442 RVA: 0x000F10D9 File Offset: 0x000EF2D9
				public override FunctionValue TypedInvoke()
				{
					return LinesModule.Splitter.SplitByNothingFunctionValue.nothing;
				}

				// Token: 0x04002672 RID: 9842
				private static readonly FunctionValue nothing = new LinesModule.Splitter.SplitByNothingFunctionValue.NothingSplitFunctionValue();

				// Token: 0x020009F4 RID: 2548
				private class NothingSplitFunctionValue : LinesModule.SplitFunctionValue
				{
					// Token: 0x170016EA RID: 5866
					// (get) Token: 0x0600480C RID: 18444 RVA: 0x000F10EC File Offset: 0x000EF2EC
					public override IExpression Expression
					{
						get
						{
							return ConstantExpressionSyntaxNode.New(LinesModule.Splitter.SplitByNothing);
						}
					}

					// Token: 0x0600480D RID: 18445 RVA: 0x000F10F8 File Offset: 0x000EF2F8
					public override Value Invoke(Value value)
					{
						return ListValue.New(new Value[] { value });
					}
				}
			}

			// Token: 0x020009F5 RID: 2549
			private class SplitTextByCharacterTransitionFunctionValue : NativeFunctionValue2<FunctionValue, Value, Value>
			{
				// Token: 0x0600480F RID: 18447 RVA: 0x000F1111 File Offset: 0x000EF311
				public SplitTextByCharacterTransitionFunctionValue()
					: base(TypeValue.Function, 2, "before", TypeValue.Any.NonNullable, "after", TypeValue.Any.NonNullable)
				{
				}

				// Token: 0x06004810 RID: 18448 RVA: 0x000F1140 File Offset: 0x000EF340
				public override FunctionValue TypedInvoke(Value before, Value after)
				{
					if (before.IsList)
					{
						before = LinesModule.Splitter.SplitTextByCharacterTransitionFunctionValue.ValidateAndWrapList(before.AsList);
					}
					if (after.IsList)
					{
						after = LinesModule.Splitter.SplitTextByCharacterTransitionFunctionValue.ValidateAndWrapList(after.AsList);
					}
					return new FunctionResultFunctionValue(new LinesModule.Splitter.DelimitedSplitFunctionValue((ILineReader reader) => new CharacterTransitionFieldReader(reader, before.AsFunction, after.AsFunction)), this, new Value[] { before, after }).NewMeta(LinesModule.TextToTextMeta).AsFunction;
				}

				// Token: 0x06004811 RID: 18449 RVA: 0x000F11E8 File Offset: 0x000EF3E8
				private static FunctionValue ValidateAndWrapList(ListValue list)
				{
					foreach (IValueReference valueReference in list)
					{
						char asCharacter = valueReference.Value.AsCharacter;
					}
					return new LinesModule.Splitter.SplitTextByCharacterTransitionFunctionValue.ListContainsCharacterFunctionValue(list);
				}

				// Token: 0x020009F6 RID: 2550
				private class ListContainsCharacterFunctionValue : NativeFunctionValue1<LogicalValue, TextValue>
				{
					// Token: 0x06004812 RID: 18450 RVA: 0x000F123C File Offset: 0x000EF43C
					public ListContainsCharacterFunctionValue(ListValue list)
						: base(TypeValue.Logical, 1, "character", TypeValue.Character)
					{
						this.list = list;
					}

					// Token: 0x06004813 RID: 18451 RVA: 0x000F125B File Offset: 0x000EF45B
					public override LogicalValue TypedInvoke(TextValue character)
					{
						return Library.List.Contains.Invoke(this.list, character).AsLogical;
					}

					// Token: 0x04002673 RID: 9843
					private readonly ListValue list;
				}
			}

			// Token: 0x020009F8 RID: 2552
			private class SplitTextByDelimiterFunctionValue : NativeFunctionValue2<FunctionValue, TextValue, Value>
			{
				// Token: 0x06004816 RID: 18454 RVA: 0x000F1291 File Offset: 0x000EF491
				public SplitTextByDelimiterFunctionValue()
					: base(TypeValue.Function, 1, "delimiter", TypeValue.Text, "quoteStyle", LinesModule.QuoteStyle.Type.Nullable)
				{
				}

				// Token: 0x06004817 RID: 18455 RVA: 0x000F12B8 File Offset: 0x000EF4B8
				public override FunctionValue TypedInvoke(TextValue delimiter, Value quoteStyle)
				{
					string stringDelimiter = delimiter.String;
					if (stringDelimiter.Length == 0)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Splitter_Delimiter_Invalid, delimiter, null);
					}
					if (stringDelimiter == "," && quoteStyle.Equals(LinesModule.QuoteStyle.Csv.Value))
					{
						return LinesModule.Splitter.SplitByComma;
					}
					LinesModule.QuoteStyle qs = LinesModule.GetQuoteStyle(quoteStyle);
					Func<ILineReader, IFieldReader<IValueReference>> func;
					if (stringDelimiter.Length == 1)
					{
						func = (ILineReader reader) => new SingleCharacterDelimitedFieldReader(reader, qs.Quoting, stringDelimiter[0]);
					}
					else
					{
						func = (ILineReader reader) => new SingleStringDelimitedFieldReader(reader, qs.Quoting, stringDelimiter);
					}
					return new FunctionResultFunctionValue(new LinesModule.Splitter.DelimitedSplitFunctionValue(func), this, new Value[] { delimiter, quoteStyle }).NewMeta(LinesModule.TextToTextMeta).AsFunction;
				}
			}

			// Token: 0x020009FA RID: 2554
			private class ExcelSplitTextByDelimiterFunctionValue : NativeFunctionValue1<FunctionValue, TextValue>
			{
				// Token: 0x0600481B RID: 18459 RVA: 0x000F13B2 File Offset: 0x000EF5B2
				public ExcelSplitTextByDelimiterFunctionValue()
					: base(TypeValue.Function, 1, "delimiter", TypeValue.Text)
				{
				}

				// Token: 0x0600481C RID: 18460 RVA: 0x000F13CC File Offset: 0x000EF5CC
				public override FunctionValue TypedInvoke(TextValue delimiter)
				{
					char delimiterAsChar = delimiter.AsCharacter;
					return new FunctionResultFunctionValue(new LinesModule.Splitter.DelimitedSplitFunctionValue((ILineReader reader) => new ExcelSingleCharacterDelimitedFieldReader(reader, delimiterAsChar)), this, new Value[] { delimiter }).NewMeta(LinesModule.TextToTextMeta).AsFunction;
				}
			}

			// Token: 0x020009FC RID: 2556
			private class SplitTextByEachDelimiterFunctionValue : NativeFunctionValue3<FunctionValue, ListValue, Value, Value>
			{
				// Token: 0x0600481F RID: 18463 RVA: 0x000F1428 File Offset: 0x000EF628
				public SplitTextByEachDelimiterFunctionValue()
					: base(TypeValue.Function, 1, "delimiters", TypeValue.List, "quoteStyle", LinesModule.QuoteStyle.Type.Nullable, "startAtEnd", NullableTypeValue.Logical)
				{
				}

				// Token: 0x06004820 RID: 18464 RVA: 0x000F1464 File Offset: 0x000EF664
				public override FunctionValue TypedInvoke(ListValue delimiters, Value quoteStyle, Value startAtEnd)
				{
					bool flag = !startAtEnd.IsNull && startAtEnd.AsBoolean;
					string[] stringDelimiters = LinesModule.GetStringDelimiters(delimiters, flag);
					LinesModule.QuoteStyle qs = LinesModule.GetQuoteStyle(quoteStyle);
					Func<ILineReader, IFieldReader<IValueReference>> func;
					if (LinesModule.AllCharDelimiters(stringDelimiters))
					{
						func = (ILineReader reader) => new EachCharacterDelimitedFieldReader(reader, qs.Quoting, stringDelimiters);
					}
					else
					{
						func = (ILineReader reader) => new EachStringDelimitedFieldReader(reader, qs.Quoting, stringDelimiters);
					}
					LinesModule.SplitFunctionValue splitFunctionValue = new LinesModule.Splitter.DelimitedSplitFunctionValue(func);
					if (flag)
					{
						splitFunctionValue = new LinesModule.Splitter.EndToStartSplitFunctionValue(splitFunctionValue);
					}
					return new FunctionResultFunctionValue(splitFunctionValue, this, new Value[] { quoteStyle, startAtEnd }).NewMeta(LinesModule.TextToTextMeta).AsFunction;
				}
			}

			// Token: 0x020009FE RID: 2558
			private class SplitTextByAnyDelimiterFunctionValue : NativeFunctionValue3<FunctionValue, ListValue, Value, Value>
			{
				// Token: 0x06004824 RID: 18468 RVA: 0x000F1534 File Offset: 0x000EF734
				public SplitTextByAnyDelimiterFunctionValue()
					: base(TypeValue.Function, 1, "delimiters", TypeValue.List, "quoteStyle", LinesModule.QuoteStyle.Type.Nullable, "startAtEnd", NullableTypeValue.Logical)
				{
				}

				// Token: 0x06004825 RID: 18469 RVA: 0x000F1570 File Offset: 0x000EF770
				public override FunctionValue TypedInvoke(ListValue delimiters, Value quoteStyle, Value startAtEnd)
				{
					bool flag = !startAtEnd.IsNull && startAtEnd.AsBoolean;
					string[] stringDelimiters = LinesModule.GetStringDelimiters(delimiters, flag);
					LinesModule.QuoteStyle qs = LinesModule.GetQuoteStyle(quoteStyle);
					Func<ILineReader, IFieldReader<IValueReference>> func;
					if (LinesModule.AllCharDelimiters(stringDelimiters))
					{
						HashSet<char> charDelimiters = LinesModule.GetCharDelimiters(stringDelimiters);
						Func<char, bool> <>9__2;
						func = delegate(ILineReader reader)
						{
							bool quoting = qs.Quoting;
							Func<char, bool> func2;
							if ((func2 = <>9__2) == null)
							{
								func2 = (<>9__2 = (char ch) => charDelimiters.Contains(ch));
							}
							return new AnyCharacterDelimitedFieldReader(reader, quoting, func2);
						};
					}
					else
					{
						func = (ILineReader reader) => new AnyStringDelimitedFieldReader(reader, qs.Quoting, stringDelimiters);
					}
					LinesModule.SplitFunctionValue splitFunctionValue = new LinesModule.Splitter.DelimitedSplitFunctionValue(func);
					if (flag)
					{
						splitFunctionValue = new LinesModule.Splitter.EndToStartSplitFunctionValue(splitFunctionValue);
					}
					return new FunctionResultFunctionValue(splitFunctionValue, this, new Value[] { quoteStyle, startAtEnd }).NewMeta(LinesModule.TextToTextMeta).AsFunction;
				}
			}

			// Token: 0x02000A01 RID: 2561
			private class SplitTextByWhitespaceFunctionValue : NativeFunctionValue1<FunctionValue, Value>
			{
				// Token: 0x0600482B RID: 18475 RVA: 0x000F1698 File Offset: 0x000EF898
				public SplitTextByWhitespaceFunctionValue()
					: base(TypeValue.Function, 0, "quoteStyle", LinesModule.QuoteStyle.Type.Nullable)
				{
				}

				// Token: 0x0600482C RID: 18476 RVA: 0x000F16B8 File Offset: 0x000EF8B8
				public override FunctionValue TypedInvoke(Value quoteStyle)
				{
					LinesModule.QuoteStyle qs = LinesModule.GetQuoteStyle(quoteStyle);
					return new FunctionResultFunctionValue(new LinesModule.Splitter.DelimitedSplitFunctionValue((ILineReader reader) => new WhitespaceDelimitedFieldReader(reader, qs.Quoting)), this, new Value[] { quoteStyle }).NewMeta(LinesModule.TextToTextMeta).AsFunction;
				}
			}

			// Token: 0x02000A03 RID: 2563
			private class SplitTextByRangesFunctionValue : NativeFunctionValue2<FunctionValue, ListValue, Value>
			{
				// Token: 0x0600482F RID: 18479 RVA: 0x000F1718 File Offset: 0x000EF918
				public SplitTextByRangesFunctionValue()
					: base(TypeValue.Function, 1, "ranges", TypeValue.List, "startAtEnd", NullableTypeValue.Logical)
				{
				}

				// Token: 0x06004830 RID: 18480 RVA: 0x000F173A File Offset: 0x000EF93A
				public override FunctionValue TypedInvoke(ListValue ranges, Value startAtEnd)
				{
					return new FunctionResultFunctionValue(LinesModule.Splitter.GetRangeSplitter(LinesModule.GetRanges(ranges), startAtEnd), this, new Value[] { ranges, startAtEnd });
				}
			}

			// Token: 0x02000A04 RID: 2564
			private class SplitTextByPositionsFunctionValue : NativeFunctionValue2<FunctionValue, ListValue, Value>
			{
				// Token: 0x06004831 RID: 18481 RVA: 0x000F175C File Offset: 0x000EF95C
				public SplitTextByPositionsFunctionValue()
					: base(TypeValue.Function, 1, "positions", TypeValue.List, "startAtEnd", NullableTypeValue.Logical)
				{
				}

				// Token: 0x06004832 RID: 18482 RVA: 0x000F177E File Offset: 0x000EF97E
				public override FunctionValue TypedInvoke(ListValue positions, Value startAtEnd)
				{
					return new FunctionResultFunctionValue(LinesModule.Splitter.GetRangeSplitter(LinesModule.GetRangesFromPositions(positions), startAtEnd), this, new Value[] { positions, startAtEnd });
				}
			}

			// Token: 0x02000A05 RID: 2565
			private class SplitTextByRepeatedLengthsFunctionValue : NativeFunctionValue2<FunctionValue, NumberValue, Value>
			{
				// Token: 0x06004833 RID: 18483 RVA: 0x000F17A0 File Offset: 0x000EF9A0
				public SplitTextByRepeatedLengthsFunctionValue()
					: base(TypeValue.Function, 1, "length", TypeValue.Number, "startAtEnd", NullableTypeValue.Logical)
				{
				}

				// Token: 0x06004834 RID: 18484 RVA: 0x000F17C2 File Offset: 0x000EF9C2
				public override FunctionValue TypedInvoke(NumberValue length, Value startAtEnd)
				{
					int asInteger = length.AsInteger32;
					if (asInteger < 0)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Lines_Length_Invalid, length, null);
					}
					return new FunctionResultFunctionValue(LinesModule.Splitter.GetRepeatedLengthSplitter(asInteger, startAtEnd), this, new Value[] { length, startAtEnd });
				}
			}

			// Token: 0x02000A06 RID: 2566
			private class SplitTextByLengthsFunctionValue : NativeFunctionValue2<FunctionValue, ListValue, Value>
			{
				// Token: 0x06004835 RID: 18485 RVA: 0x000F17F5 File Offset: 0x000EF9F5
				public SplitTextByLengthsFunctionValue()
					: base(TypeValue.Function, 1, "lengths", TypeValue.List, "startAtEnd", NullableTypeValue.Logical)
				{
				}

				// Token: 0x06004836 RID: 18486 RVA: 0x000F1817 File Offset: 0x000EFA17
				public override FunctionValue TypedInvoke(ListValue lengths, Value startAtEnd)
				{
					return new FunctionResultFunctionValue(LinesModule.Splitter.GetRangeSplitter(LinesModule.GetRangesFromLengths(lengths), startAtEnd), this, new Value[] { lengths, startAtEnd });
				}
			}

			// Token: 0x02000A07 RID: 2567
			private class RangeSplitFunctionValue : NativeFunctionValue1<ListValue, Value>
			{
				// Token: 0x06004837 RID: 18487 RVA: 0x000F1839 File Offset: 0x000EFA39
				public RangeSplitFunctionValue(LinesModule.Range[] ranges)
					: base(TypeValue.List, "list", NullableTypeValue.Text)
				{
					this.ranges = ranges;
				}

				// Token: 0x06004838 RID: 18488 RVA: 0x000F1858 File Offset: 0x000EFA58
				public override ListValue TypedInvoke(Value line)
				{
					if (line.IsNull)
					{
						return ListValue.Empty;
					}
					Value[] array = new Value[this.ranges.Length];
					string @string = line.AsText.String;
					for (int i = 0; i < array.Length; i++)
					{
						LinesModule.Range range = this.ranges[i];
						int offset = range.Offset;
						int length = range.Length;
						if (offset < @string.Length)
						{
							array[i] = TextValue.New(@string.Substring(offset, Math.Min(length, @string.Length - offset)));
						}
						else
						{
							array[i] = TextValue.Empty;
						}
					}
					return ListValue.New(array.ToArray<Value>());
				}

				// Token: 0x04002681 RID: 9857
				private readonly LinesModule.Range[] ranges;
			}

			// Token: 0x02000A08 RID: 2568
			private class RepeatedLengthSplitFunctionValue : NativeFunctionValue1<ListValue, Value>
			{
				// Token: 0x06004839 RID: 18489 RVA: 0x000F18F9 File Offset: 0x000EFAF9
				public RepeatedLengthSplitFunctionValue(int length)
					: base(TypeValue.List, "list", NullableTypeValue.Text)
				{
					this.length = length;
				}

				// Token: 0x0600483A RID: 18490 RVA: 0x000F1918 File Offset: 0x000EFB18
				public override ListValue TypedInvoke(Value line)
				{
					if (line.IsNull)
					{
						return ListValue.Empty;
					}
					string @string = line.AsText.String;
					int num = @string.Length;
					int num2 = 1;
					if (this.length != 0)
					{
						num2 = (num + this.length - 1) / this.length;
					}
					Value[] array = new Value[num2];
					for (int i = 0; i < num2; i++)
					{
						int num3 = this.length * i;
						array[i] = TextValue.New(@string.Substring(num3, Math.Min(this.length, num - num3)));
					}
					return ListValue.New(array);
				}

				// Token: 0x04002682 RID: 9858
				private readonly int length;
			}
		}

		// Token: 0x02000A0A RID: 2570
		public static class Combiner
		{
			// Token: 0x0600483E RID: 18494 RVA: 0x000F19C4 File Offset: 0x000EFBC4
			private static bool TryRewriteCombineByEachDelimiter(bool quoting, IEnumerable<string> delimiters, IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
			{
				IListExpression listExpression;
				if (!quoting && invocation.Arguments.Count == 1 && environment.TryAsListExpression(invocation.Arguments[0], 8, out listExpression))
				{
					expression = LinesModule.Combiner.literalBlank;
					using (IEnumerator<string> enumerator = delimiters.GetEnumerator())
					{
						foreach (IExpression expression2 in listExpression.Members)
						{
							Value value;
							IExpression expression3;
							if (expression2.TryGetConstant(out value))
							{
								expression3 = (value.IsNull ? LinesModule.Combiner.literalBlank : expression2);
							}
							else
							{
								expression3 = new IfExpressionSyntaxNode(BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, expression2, ConstantExpressionSyntaxNode.Null, TokenRange.Null), LinesModule.Combiner.literalBlank, expression2, TokenRange.Null);
							}
							if (expression == LinesModule.Combiner.literalBlank)
							{
								expression = expression3;
							}
							else if (enumerator.MoveNext())
							{
								IExpression expression4 = new ConstantExpressionSyntaxNode(TextValue.New(enumerator.Current));
								expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.Concatenate, BinaryExpressionSyntaxNode.New(BinaryOperator2.Concatenate, expression, expression4, TokenRange.Null), expression3, TokenRange.Null);
							}
							else
							{
								expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.Concatenate, expression, expression3, TokenRange.Null);
							}
						}
					}
					return true;
				}
				expression = null;
				return false;
			}

			// Token: 0x04002684 RID: 9860
			public static readonly FunctionValue CombineByComma = new LinesModule.Combiner.DelimitedCombineFunctionValue(",", true);

			// Token: 0x04002685 RID: 9861
			public static readonly FunctionValue CombineTextByDelimiter = new LinesModule.Combiner.CombineTextByDelimiterFunctionValue();

			// Token: 0x04002686 RID: 9862
			public static readonly FunctionValue CombineTextByEachDelimiter = new LinesModule.Combiner.CombineTextByEachDelimiterFunctionValue();

			// Token: 0x04002687 RID: 9863
			public static readonly FunctionValue CombineTextByRanges = new LinesModule.Combiner.CombineTextByRangesFunctionValue();

			// Token: 0x04002688 RID: 9864
			public static readonly FunctionValue CombineTextByPositions = new LinesModule.Combiner.CombineTextByPositionsFunctionValue();

			// Token: 0x04002689 RID: 9865
			public static readonly FunctionValue CombineTextByLengths = new LinesModule.Combiner.CombineTextByLengthsFunctionValue();

			// Token: 0x0400268A RID: 9866
			private const int MaxArgumentsToExpand = 8;

			// Token: 0x0400268B RID: 9867
			private static readonly IExpression literalBlank = new ConstantExpressionSyntaxNode(TextValue.Empty);

			// Token: 0x02000A0B RID: 2571
			private class CombineTextByDelimiterFunctionValue : NativeFunctionValue2<FunctionValue, TextValue, Value>
			{
				// Token: 0x06004840 RID: 18496 RVA: 0x000F1291 File Offset: 0x000EF491
				public CombineTextByDelimiterFunctionValue()
					: base(TypeValue.Function, 1, "delimiter", TypeValue.Text, "quoteStyle", LinesModule.QuoteStyle.Type.Nullable)
				{
				}

				// Token: 0x06004841 RID: 18497 RVA: 0x000F1B74 File Offset: 0x000EFD74
				public override FunctionValue TypedInvoke(TextValue delimiter, Value quoteStyle)
				{
					string @string = delimiter.String;
					if (@string == "," && quoteStyle.Equals(LinesModule.QuoteStyle.Csv.Value))
					{
						return LinesModule.Combiner.CombineByComma;
					}
					return new LinesModule.Combiner.DelimitedCombineFunctionValue(@string, LinesModule.GetQuoteStyle(quoteStyle).Quoting);
				}
			}

			// Token: 0x02000A0C RID: 2572
			private class CombineTextByEachDelimiterFunctionValue : NativeFunctionValue2<FunctionValue, ListValue, Value>
			{
				// Token: 0x06004842 RID: 18498 RVA: 0x000F1BBE File Offset: 0x000EFDBE
				public CombineTextByEachDelimiterFunctionValue()
					: base(TypeValue.Function, 1, "delimiters", TypeValue.List, "quoteStyle", LinesModule.QuoteStyle.Type.Nullable)
				{
				}

				// Token: 0x06004843 RID: 18499 RVA: 0x000F1BE5 File Offset: 0x000EFDE5
				public override FunctionValue TypedInvoke(ListValue delimiters, Value quoteStyle)
				{
					return new LinesModule.Combiner.DelimitedCombineEachFunctionValue(LinesModule.GetStringDelimiters(delimiters, false), LinesModule.GetQuoteStyle(quoteStyle).Quoting);
				}
			}

			// Token: 0x02000A0D RID: 2573
			private class CombineTextByRangesFunctionValue : NativeFunctionValue2<FunctionValue, ListValue, Value>
			{
				// Token: 0x06004844 RID: 18500 RVA: 0x000F1BFE File Offset: 0x000EFDFE
				public CombineTextByRangesFunctionValue()
					: base(TypeValue.Function, 1, "ranges", TypeValue.List, "template", NullableTypeValue.Text)
				{
				}

				// Token: 0x06004845 RID: 18501 RVA: 0x000F1C20 File Offset: 0x000EFE20
				public override FunctionValue TypedInvoke(ListValue ranges, Value template)
				{
					return new LinesModule.Combiner.RangeCombineFunctionValue(LinesModule.GetRanges(ranges), template.IsNull ? string.Empty : template.AsString);
				}
			}

			// Token: 0x02000A0E RID: 2574
			private class CombineTextByPositionsFunctionValue : NativeFunctionValue2<FunctionValue, ListValue, Value>
			{
				// Token: 0x06004846 RID: 18502 RVA: 0x000F1C42 File Offset: 0x000EFE42
				public CombineTextByPositionsFunctionValue()
					: base(TypeValue.Function, 1, "positions", TypeValue.List, "template", NullableTypeValue.Text)
				{
				}

				// Token: 0x06004847 RID: 18503 RVA: 0x000F1C64 File Offset: 0x000EFE64
				public override FunctionValue TypedInvoke(ListValue positions, Value template)
				{
					return new LinesModule.Combiner.RangeCombineFunctionValue(LinesModule.GetRangesFromPositions(positions), template.IsNull ? string.Empty : template.AsString);
				}
			}

			// Token: 0x02000A0F RID: 2575
			private class CombineTextByLengthsFunctionValue : NativeFunctionValue2<FunctionValue, ListValue, Value>
			{
				// Token: 0x06004848 RID: 18504 RVA: 0x000F1C86 File Offset: 0x000EFE86
				public CombineTextByLengthsFunctionValue()
					: base(TypeValue.Function, 1, "lengths", TypeValue.List, "template", NullableTypeValue.Text)
				{
				}

				// Token: 0x06004849 RID: 18505 RVA: 0x000F1CA8 File Offset: 0x000EFEA8
				public override FunctionValue TypedInvoke(ListValue lengths, Value template)
				{
					return new LinesModule.Combiner.RangeCombineFunctionValue(LinesModule.GetRangesFromLengths(lengths), template.IsNull ? string.Empty : template.AsString);
				}
			}

			// Token: 0x02000A10 RID: 2576
			private class DelimitedCombineFunctionValue : NativeFunctionValue1<TextValue, ListValue>, IInvocationRewriter
			{
				// Token: 0x0600484A RID: 18506 RVA: 0x000F1CCA File Offset: 0x000EFECA
				public DelimitedCombineFunctionValue(string delimiter, bool quoting)
					: base(TypeValue.Text, "list", TypeValue.List)
				{
					this.delimiter = delimiter;
					this.quoting = quoting;
				}

				// Token: 0x0600484B RID: 18507 RVA: 0x000F1CF0 File Offset: 0x000EFEF0
				public override TextValue TypedInvoke(ListValue list)
				{
					DelimitedFieldWriter delimitedFieldWriter = new DelimitedFieldWriter(this.delimiter, this.quoting);
					foreach (IValueReference valueReference in list)
					{
						delimitedFieldWriter.WriteField(valueReference.Value.IsNull ? string.Empty : valueReference.Value.AsString);
					}
					return TextValue.New(delimitedFieldWriter.ToString());
				}

				// Token: 0x0600484C RID: 18508 RVA: 0x000F1D74 File Offset: 0x000EFF74
				public bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
				{
					return LinesModule.Combiner.TryRewriteCombineByEachDelimiter(this.quoting, this.RepeatDelimiter(), invocation, environment, out expression);
				}

				// Token: 0x0600484D RID: 18509 RVA: 0x000F1D8A File Offset: 0x000EFF8A
				private IEnumerable<string> RepeatDelimiter()
				{
					for (;;)
					{
						yield return this.delimiter;
					}
					yield break;
				}

				// Token: 0x0400268C RID: 9868
				private readonly string delimiter;

				// Token: 0x0400268D RID: 9869
				private readonly bool quoting;
			}

			// Token: 0x02000A12 RID: 2578
			private class DelimitedCombineEachFunctionValue : NativeFunctionValue1<TextValue, ListValue>, IInvocationRewriter
			{
				// Token: 0x06004856 RID: 18518 RVA: 0x000F1E4F File Offset: 0x000F004F
				public DelimitedCombineEachFunctionValue(string[] delimiters, bool quoting)
					: base(TypeValue.Text, "list", TypeValue.List)
				{
					this.delimiters = delimiters;
					this.quoting = quoting;
				}

				// Token: 0x06004857 RID: 18519 RVA: 0x000F1E74 File Offset: 0x000F0074
				public override TextValue TypedInvoke(ListValue list)
				{
					DelimitedFieldWriter delimitedFieldWriter = new DelimitedFieldWriter(this.delimiters, this.quoting);
					foreach (IValueReference valueReference in list)
					{
						delimitedFieldWriter.WriteField(valueReference.Value.IsNull ? string.Empty : valueReference.Value.AsString);
					}
					return TextValue.New(delimitedFieldWriter.ToString());
				}

				// Token: 0x06004858 RID: 18520 RVA: 0x000F1EF8 File Offset: 0x000F00F8
				public bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
				{
					return LinesModule.Combiner.TryRewriteCombineByEachDelimiter(this.quoting, this.delimiters, invocation, environment, out expression);
				}

				// Token: 0x04002692 RID: 9874
				private string[] delimiters;

				// Token: 0x04002693 RID: 9875
				private bool quoting;
			}

			// Token: 0x02000A13 RID: 2579
			private class RangeCombineFunctionValue : NativeFunctionValue1<TextValue, ListValue>
			{
				// Token: 0x06004859 RID: 18521 RVA: 0x000F1F0E File Offset: 0x000F010E
				public RangeCombineFunctionValue(LinesModule.Range[] ranges, string template)
					: base(TypeValue.Text, "fields", TypeValue.List)
				{
					this.ranges = ranges;
					this.template = template;
				}

				// Token: 0x0600485A RID: 18522 RVA: 0x000F1F34 File Offset: 0x000F0134
				public override TextValue TypedInvoke(ListValue fields)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(this.template);
					using (IEnumerator<IValueReference> enumerator = fields.GetEnumerator())
					{
						for (int i = 0; i < this.ranges.Length; i++)
						{
							string text = string.Empty;
							if (enumerator.MoveNext() && !enumerator.Current.Value.IsNull)
							{
								text = enumerator.Current.Value.AsString;
							}
							LinesModule.Range range = this.ranges[i];
							int offset = range.Offset;
							int num = Math.Min(range.Length, text.Length);
							while (stringBuilder.Length < offset + num)
							{
								stringBuilder.Append(' ');
							}
							for (int j = 0; j < num; j++)
							{
								stringBuilder[offset + j] = text[j];
							}
						}
					}
					return TextValue.New(stringBuilder.ToString());
				}

				// Token: 0x04002694 RID: 9876
				private LinesModule.Range[] ranges;

				// Token: 0x04002695 RID: 9877
				private string template;
			}
		}

		// Token: 0x02000A14 RID: 2580
		public enum ExtraValuesEnum
		{
			// Token: 0x04002697 RID: 9879
			List,
			// Token: 0x04002698 RID: 9880
			Error,
			// Token: 0x04002699 RID: 9881
			Ignore
		}

		// Token: 0x02000A15 RID: 2581
		private static class ExtraValues
		{
			// Token: 0x0400269A RID: 9882
			public static readonly IntEnumTypeValue<LinesModule.ExtraValuesEnum> Type = new IntEnumTypeValue<LinesModule.ExtraValuesEnum>("ExtraValues.Type");

			// Token: 0x0400269B RID: 9883
			public static readonly NumberValue List = LinesModule.ExtraValues.Type.NewEnumValue("ExtraValues.List", 0, LinesModule.ExtraValuesEnum.List, null);

			// Token: 0x0400269C RID: 9884
			public static readonly NumberValue Error = LinesModule.ExtraValues.Type.NewEnumValue("ExtraValues.Error", 1, LinesModule.ExtraValuesEnum.Error, null);

			// Token: 0x0400269D RID: 9885
			public static readonly NumberValue Ignore = LinesModule.ExtraValues.Type.NewEnumValue("ExtraValues.Ignore", 2, LinesModule.ExtraValuesEnum.Ignore, null);
		}

		// Token: 0x02000A16 RID: 2582
		private struct CsvStyle
		{
			// Token: 0x0400269E RID: 9886
			public static readonly IntEnumTypeValue<NumberValue> Type = new IntEnumTypeValue<NumberValue>("CsvStyle.Type");

			// Token: 0x0400269F RID: 9887
			public static readonly NumberValue QuoteAfterDelimiter = LinesModule.CsvStyle.Type.NewEnumValue("CsvStyle.QuoteAfterDelimiter", 0, NumberValue.Zero, null);

			// Token: 0x040026A0 RID: 9888
			public static readonly NumberValue QuoteAlways = LinesModule.CsvStyle.Type.NewEnumValue("CsvStyle.QuoteAlways", 1, NumberValue.One, null);
		}

		// Token: 0x02000A17 RID: 2583
		public abstract class QuoteStyle
		{
			// Token: 0x0600485D RID: 18525 RVA: 0x000F20EA File Offset: 0x000F02EA
			public static LinesModule.QuoteStyle Excel(TextValue delimiter)
			{
				return new LinesModule.QuoteStyle.ExcelQuoteStyle(delimiter.AsCharacter);
			}

			// Token: 0x170016ED RID: 5869
			// (get) Token: 0x0600485E RID: 18526
			public abstract Value Value { get; }

			// Token: 0x170016EE RID: 5870
			// (get) Token: 0x0600485F RID: 18527
			public abstract bool Quoting { get; }

			// Token: 0x06004860 RID: 18528
			public abstract ILineReader CreateLineReader(TextReader reader, bool includeLineSeparators);

			// Token: 0x040026A1 RID: 9889
			public static readonly LinesModule.QuoteStyle None = new LinesModule.QuoteStyle.NoneQuoteStyle();

			// Token: 0x040026A2 RID: 9890
			public static readonly LinesModule.QuoteStyle Csv = new LinesModule.QuoteStyle.CsvQuoteStyle();

			// Token: 0x040026A3 RID: 9891
			public static readonly IntEnumTypeValue<LinesModule.QuoteStyle> Type = new IntEnumTypeValue<LinesModule.QuoteStyle>("QuoteStyle.Type");

			// Token: 0x040026A4 RID: 9892
			public static readonly NumberValue NoneEnum = LinesModule.QuoteStyle.Type.NewEnumValue("QuoteStyle.None", LinesModule.QuoteStyle.None.Value.AsInteger32, LinesModule.QuoteStyle.None, null);

			// Token: 0x040026A5 RID: 9893
			public static readonly NumberValue CsvEnum = LinesModule.QuoteStyle.Type.NewEnumValue("QuoteStyle.Csv", LinesModule.QuoteStyle.Csv.Value.AsInteger32, LinesModule.QuoteStyle.Csv, null);

			// Token: 0x02000A18 RID: 2584
			private class NoneQuoteStyle : LinesModule.QuoteStyle
			{
				// Token: 0x06004863 RID: 18531 RVA: 0x000F217A File Offset: 0x000F037A
				public NoneQuoteStyle()
				{
					this.value = ValueHelper.CreateEnumValue(0);
				}

				// Token: 0x170016EF RID: 5871
				// (get) Token: 0x06004864 RID: 18532 RVA: 0x000F218E File Offset: 0x000F038E
				public override Value Value
				{
					get
					{
						return this.value;
					}
				}

				// Token: 0x170016F0 RID: 5872
				// (get) Token: 0x06004865 RID: 18533 RVA: 0x00002105 File Offset: 0x00000305
				public override bool Quoting
				{
					get
					{
						return false;
					}
				}

				// Token: 0x06004866 RID: 18534 RVA: 0x000F2196 File Offset: 0x000F0396
				public override ILineReader CreateLineReader(TextReader reader, bool includeLineSeparators)
				{
					return new UnquotedTextReaderLineReader(reader, includeLineSeparators);
				}

				// Token: 0x040026A6 RID: 9894
				private Value value;
			}

			// Token: 0x02000A19 RID: 2585
			private class CsvQuoteStyle : LinesModule.QuoteStyle
			{
				// Token: 0x06004867 RID: 18535 RVA: 0x000F219F File Offset: 0x000F039F
				public CsvQuoteStyle()
				{
					this.value = ValueHelper.CreateEnumValue(1);
				}

				// Token: 0x170016F1 RID: 5873
				// (get) Token: 0x06004868 RID: 18536 RVA: 0x000F21B3 File Offset: 0x000F03B3
				public override Value Value
				{
					get
					{
						return this.value;
					}
				}

				// Token: 0x170016F2 RID: 5874
				// (get) Token: 0x06004869 RID: 18537 RVA: 0x00002139 File Offset: 0x00000339
				public override bool Quoting
				{
					get
					{
						return true;
					}
				}

				// Token: 0x0600486A RID: 18538 RVA: 0x000F21BB File Offset: 0x000F03BB
				public override ILineReader CreateLineReader(TextReader reader, bool includeLineSeparators)
				{
					return new QuotedTextReaderLineReader(reader, includeLineSeparators);
				}

				// Token: 0x040026A7 RID: 9895
				private Value value;
			}

			// Token: 0x02000A1A RID: 2586
			private class ExcelQuoteStyle : LinesModule.QuoteStyle
			{
				// Token: 0x0600486B RID: 18539 RVA: 0x000F21C4 File Offset: 0x000F03C4
				public ExcelQuoteStyle(char delimiter)
				{
					this.delimiter = delimiter;
				}

				// Token: 0x170016F3 RID: 5875
				// (get) Token: 0x0600486C RID: 18540 RVA: 0x0000EE09 File Offset: 0x0000D009
				public override Value Value
				{
					get
					{
						throw new InvalidOperationException();
					}
				}

				// Token: 0x170016F4 RID: 5876
				// (get) Token: 0x0600486D RID: 18541 RVA: 0x00002139 File Offset: 0x00000339
				public override bool Quoting
				{
					get
					{
						return true;
					}
				}

				// Token: 0x0600486E RID: 18542 RVA: 0x000F21D3 File Offset: 0x000F03D3
				public override ILineReader CreateLineReader(TextReader reader, bool includeLineSeparators)
				{
					return new ExcelTextReaderLineReader(reader, includeLineSeparators, this.delimiter);
				}

				// Token: 0x040026A8 RID: 9896
				private char delimiter;
			}
		}

		// Token: 0x02000A1B RID: 2587
		private abstract class Normalizer
		{
			// Token: 0x0600486F RID: 18543 RVA: 0x000F21E2 File Offset: 0x000F03E2
			public static LinesModule.Normalizer New(int count, Value defaultValue, LinesModule.ExtraValuesEnum extraValues)
			{
				switch (extraValues)
				{
				case LinesModule.ExtraValuesEnum.List:
					return new LinesModule.VariableNormalizer(count, defaultValue);
				case LinesModule.ExtraValuesEnum.Error:
					return new LinesModule.FixedNormalizer(count, defaultValue, false);
				case LinesModule.ExtraValuesEnum.Ignore:
					return new LinesModule.FixedNormalizer(count, defaultValue, true);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06004870 RID: 18544
			public abstract ListValue Normalize(ListValue list);
		}

		// Token: 0x02000A1C RID: 2588
		private class VariableNormalizer : LinesModule.Normalizer
		{
			// Token: 0x06004872 RID: 18546 RVA: 0x000F2217 File Offset: 0x000F0417
			public VariableNormalizer(int count, Value defaultValue)
			{
				this.count = count;
				this.defaultValue = defaultValue;
			}

			// Token: 0x06004873 RID: 18547 RVA: 0x000F2230 File Offset: 0x000F0430
			public override ListValue Normalize(ListValue list)
			{
				IValueReference[] array = list.ToArray<IValueReference>();
				IValueReference[] array2 = new IValueReference[this.count];
				if (array.Length < array2.Length)
				{
					Array.Copy(array, array2, array.Length);
					for (int i = array.Length; i < array2.Length - 1; i++)
					{
						array2[i] = this.defaultValue;
					}
					array2[array2.Length - 1] = ListValue.Empty;
				}
				else
				{
					Array.Copy(array, array2, array2.Length - 1);
					IValueReference[] array3 = new IValueReference[array.Length - (array2.Length - 1)];
					Array.Copy(array, array2.Length - 1, array3, 0, array3.Length);
					array2[array2.Length - 1] = ListValue.New(array3);
				}
				return ListValue.New(array2);
			}

			// Token: 0x040026A9 RID: 9897
			private int count;

			// Token: 0x040026AA RID: 9898
			private Value defaultValue;
		}

		// Token: 0x02000A1D RID: 2589
		private class FixedNormalizer : LinesModule.Normalizer
		{
			// Token: 0x06004874 RID: 18548 RVA: 0x000F22C9 File Offset: 0x000F04C9
			public FixedNormalizer(int count, Value defaultValue, bool ignoreExtraValues)
			{
				this.count = count;
				this.defaultValue = defaultValue;
				this.ignoreExtraValues = ignoreExtraValues;
			}

			// Token: 0x06004875 RID: 18549 RVA: 0x000F22E8 File Offset: 0x000F04E8
			public override ListValue Normalize(ListValue list)
			{
				if (list.Count == this.count)
				{
					return list;
				}
				IValueReference[] array = list.ToArray<IValueReference>();
				IValueReference[] array2 = new IValueReference[this.count];
				if (array.Length > array2.Length)
				{
					if (!this.ignoreExtraValues)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.List_Normalizer_TooManyColumns, RecordValue.New(Keys.New("Count", "Values"), new Value[]
						{
							ListValue.New(new Value[] { NumberValue.New(this.count) }),
							ListValue.New(array)
						}), null);
					}
					Array.Copy(array, array2, array2.Length);
				}
				else
				{
					Array.Copy(array, array2, array.Length);
					for (int i = array.Length; i < array2.Length; i++)
					{
						array2[i] = this.defaultValue;
					}
				}
				return ListValue.New(array2);
			}

			// Token: 0x040026AB RID: 9899
			private int count;

			// Token: 0x040026AC RID: 9900
			private Value defaultValue;

			// Token: 0x040026AD RID: 9901
			private bool ignoreExtraValues;
		}

		// Token: 0x02000A1E RID: 2590
		private struct Range
		{
			// Token: 0x06004876 RID: 18550 RVA: 0x000F23A8 File Offset: 0x000F05A8
			public Range(int offset, int length)
			{
				this.offset = offset;
				this.length = length;
			}

			// Token: 0x170016F5 RID: 5877
			// (get) Token: 0x06004877 RID: 18551 RVA: 0x000F23B8 File Offset: 0x000F05B8
			public int Offset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x170016F6 RID: 5878
			// (get) Token: 0x06004878 RID: 18552 RVA: 0x000F23C0 File Offset: 0x000F05C0
			public int Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x040026AE RID: 9902
			private int offset;

			// Token: 0x040026AF RID: 9903
			private int length;
		}

		// Token: 0x02000A1F RID: 2591
		public struct SelectIndex : IComparable<LinesModule.SelectIndex>
		{
			// Token: 0x06004879 RID: 18553 RVA: 0x000F23C8 File Offset: 0x000F05C8
			public SelectIndex(int source, int target)
			{
				this.source = source;
				this.target = target;
			}

			// Token: 0x170016F7 RID: 5879
			// (get) Token: 0x0600487A RID: 18554 RVA: 0x000F23D8 File Offset: 0x000F05D8
			public int Source
			{
				get
				{
					return this.source;
				}
			}

			// Token: 0x170016F8 RID: 5880
			// (get) Token: 0x0600487B RID: 18555 RVA: 0x000F23E0 File Offset: 0x000F05E0
			public int Target
			{
				get
				{
					return this.target;
				}
			}

			// Token: 0x0600487C RID: 18556 RVA: 0x000F23E8 File Offset: 0x000F05E8
			public int CompareTo(LinesModule.SelectIndex other)
			{
				return this.source.CompareTo(other.source);
			}

			// Token: 0x040026B0 RID: 9904
			private int source;

			// Token: 0x040026B1 RID: 9905
			private int target;
		}
	}
}
