using System;
using System.IO;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020003B8 RID: 952
	public sealed class TextSaver : IDataSaver
	{
		// Token: 0x0600145A RID: 5210 RVA: 0x00075904 File Offset: 0x00073B04
		public TextSaver(TextSaver.Arguments args, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("TextSaver");
			this._forceDense = args.dense;
			this._outputSchema = args.outputSchema;
			this._outputHeader = args.outputHeader;
			this._sepChar = TextSaver.SepStrToChar(args.separator);
			this._sepStr = this._sepChar.ToString();
			this._silent = args.silent;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00075988 File Offset: 0x00073B88
		private static char SepStrToChar(string sep)
		{
			Contracts.CheckUserArg(!string.IsNullOrEmpty(sep), "separator", "Must specify a separator");
			sep = sep.ToLowerInvariant();
			string text;
			switch (text = sep)
			{
			case "space":
			case " ":
				return ' ';
			case "tab":
			case "\t":
				return '\t';
			case "comma":
			case ",":
				return ',';
			case "semicolon":
			case ";":
				return ';';
			case "bar":
			case "|":
				return '|';
			}
			throw Contracts.ExceptUserArg("separator", "Invalid separator - must be: space, tab, comma, semicolon, or bar");
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00075AB0 File Offset: 0x00073CB0
		private static string SepCharToStr(char sep)
		{
			char c = sep;
			if (c <= ' ')
			{
				if (c == '\t')
				{
					return "tab";
				}
				if (c == ' ')
				{
					return "space";
				}
			}
			else
			{
				if (c == ',')
				{
					return "comma";
				}
				if (c == ';')
				{
					return "semicolon";
				}
				if (c == '|')
				{
					return "bar";
				}
			}
			return sep.ToString();
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00075B08 File Offset: 0x00073D08
		public bool IsColumnSavable(ColumnType type)
		{
			ColumnType itemType = type.ItemType;
			return itemType.IsStandardScalar || itemType.IsKey;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00075B2C File Offset: 0x00073D2C
		public void SaveData(Stream stream, IDataView data, params int[] cols)
		{
			string text;
			this.SaveData(out text, stream, data, cols);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00075B44 File Offset: 0x00073D44
		public void SaveData(out string argsLoader, Stream stream, IDataView data, params int[] cols)
		{
			Contracts.CheckValue<Stream>(this._host, stream, "stream");
			Contracts.CheckValue<IDataView>(this._host, data, "data");
			Contracts.CheckNonEmpty<int>(this._host, cols, "cols");
			using (IChannel channel = this._host.Start("Saving"))
			{
				long num;
				int num2;
				int num3;
				using (StreamWriter streamWriter = Utils.OpenWriter(stream, null, 1024, true))
				{
					this.WriteDataCore(channel, streamWriter, data, out argsLoader, out num, out num2, out num3, cols);
				}
				if (!this._silent)
				{
					this.ShowCount(channel, num, num2, num3);
				}
				channel.Done();
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00075C08 File Offset: 0x00073E08
		public void WriteData(IDataView data, bool showCount, params int[] cols)
		{
			Contracts.CheckValue<IDataView>(this._host, data, "data");
			Contracts.CheckNonEmpty<int>(this._host, cols, "cols");
			using (IChannel channel = this._host.Start("Writing"))
			{
				long num;
				int num2;
				int num3;
				using (StringWriter stringWriter = new StringWriter())
				{
					string text;
					this.WriteDataCore(channel, stringWriter, data, out text, out num, out num2, out num3, cols);
					channel.Info(stringWriter.ToString());
				}
				if (showCount)
				{
					this.ShowCount(channel, num, num2, num3);
				}
				channel.Done();
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00075CBC File Offset: 0x00073EBC
		private void ShowCount(IChannel ch, long count, int min, int max)
		{
			if (count == 0L)
			{
				ch.Warning("Wrote zero rows of data!");
				return;
			}
			if (min == max)
			{
				ch.Info("Wrote {0} rows of length {1}", new object[] { count, min });
				return;
			}
			ch.Info("Wrote {0} rows of lengths between {1} and {2}", new object[] { count, min, max });
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00075D74 File Offset: 0x00073F74
		private void WriteDataCore(IChannel ch, TextWriter writer, IDataView data, out string argsLoader, out long count, out int min, out int max, params int[] cols)
		{
			bool[] active = new bool[data.Schema.ColumnCount];
			for (int l = 0; l < cols.Length; l++)
			{
				Contracts.Check(ch, 0 <= cols[l] && cols[l] < active.Length);
				Contracts.Check(ch, data.Schema.GetColumnType(cols[l]).ItemType.RawKind != 0);
				active[cols[l]] = true;
			}
			bool flag = false;
			if (this._outputHeader)
			{
				for (int j = 0; j < cols.Length; j++)
				{
					if (!flag)
					{
						ColumnType columnType = data.Schema.GetColumnType(cols[j]);
						if (!columnType.IsVector)
						{
							flag = true;
						}
						else if (columnType.IsKnownSizeVector)
						{
							ColumnType metadataTypeOrNull = data.Schema.GetMetadataTypeOrNull("SlotNames", cols[j]);
							if (metadataTypeOrNull != null && metadataTypeOrNull.VectorSize == columnType.VectorSize && metadataTypeOrNull.ItemType.IsText)
							{
								flag = true;
							}
						}
					}
				}
			}
			using (IRowCursor rowCursor = data.GetRowCursor((int i) => active[i], null))
			{
				TextSaver.<>c__DisplayClass5 CS$<>8__locals2 = new TextSaver.<>c__DisplayClass5();
				TextSaver.ValueWriter[] array = new TextSaver.ValueWriter[cols.Length];
				for (int k = 0; k < cols.Length; k++)
				{
					array[k] = TextSaver.ValueWriter.Create(rowCursor, cols[k], this._sepChar);
				}
				string text = this.CreateLoaderArguments(data.Schema, array, flag, ch);
				argsLoader = text;
				if (this._outputSchema)
				{
					this.WriteSchemaAsComment(writer, text);
				}
				TextSaver.<>c__DisplayClass5 CS$<>8__locals3 = CS$<>8__locals2;
				long? rowCount = data.GetRowCount(true);
				CS$<>8__locals3.rowCount = ((rowCount != null) ? ((double)rowCount.GetValueOrDefault()) : double.NaN);
				using (IProgressChannel progressChannel = ((!this._silent) ? this._host.StartProgressChannel("TextSaver: saving data") : null))
				{
					long stateCount = 0L;
					TextSaver.State state = new TextSaver.State(this, writer, array, flag);
					if (progressChannel != null)
					{
						progressChannel.SetHeader(new ProgressHeader(new string[] { "rows" }), delegate(IProgressEntry e)
						{
							e.SetProgress(0, (double)stateCount, CS$<>8__locals2.rowCount);
						});
					}
					state.Run(rowCursor, ref stateCount, out min, out max);
					count = stateCount;
					if (progressChannel != null)
					{
						progressChannel.Checkpoint(new double?[]
						{
							new double?((double)stateCount)
						});
					}
				}
			}
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00076044 File Offset: 0x00074244
		private void WriteSchemaAsComment(TextWriter writer, string str)
		{
			writer.WriteLine("#@ TextLoader{");
			foreach (string text in CmdIndenter.GetIndentedCommandLine(str).Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
			{
				writer.WriteLine("#@   " + text);
			}
			writer.WriteLine("#@ }");
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x000760B0 File Offset: 0x000742B0
		private string CreateLoaderArguments(ISchema schema, TextSaver.ValueWriter[] pipes, bool hasHeader, IChannel ch)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (hasHeader)
			{
				stringBuilder.Append("header+ ");
			}
			stringBuilder.AppendFormat("sep={0}", TextSaver.SepCharToStr(this._sepChar));
			int? num = new int?(0);
			for (int i = 0; i < pipes.Length; i++)
			{
				int source = pipes[i].Source;
				string columnName = schema.GetColumnName(source);
				ColumnType columnType = schema.GetColumnType(source);
				TextLoader.Column column = this.GetColumn(columnName, columnType, num);
				stringBuilder.Append(" col=");
				if (!column.TryUnparse(stringBuilder))
				{
					string settings = CmdParser.GetSettings(column, new TextLoader.Column(), 3);
					CmdQuoter.QuoteValue(settings, stringBuilder, true);
				}
				if (columnType.IsVector && !columnType.IsKnownSizeVector && i != pipes.Length - 1)
				{
					ch.Warning("Column '{0}' is variable length, so it must be the last, or the file will be unreadable. Consider switching to binary format or use xf=Choose to make '{0}' the last column.", new object[] { columnName });
					num = null;
				}
				num += columnType.ValueCount;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x000761D4 File Offset: 0x000743D4
		private TextLoader.Column GetColumn(string name, ColumnType type, int? start)
		{
			KeyRange keyRange = null;
			DataKind? dataKind;
			if (type.ItemType.IsKey)
			{
				KeyType asKey = type.ItemType.AsKey;
				if (!asKey.Contiguous)
				{
					keyRange = new KeyRange
					{
						min = asKey.Min,
						contiguous = false
					};
				}
				else if (asKey.Count == 0)
				{
					keyRange = new KeyRange
					{
						min = asKey.Min
					};
				}
				else
				{
					keyRange = new KeyRange
					{
						min = asKey.Min,
						max = new ulong?(asKey.Min + (ulong)((long)(asKey.Count - 1)))
					};
				}
				dataKind = new DataKind?(asKey.RawKind);
			}
			else
			{
				dataKind = new DataKind?(type.ItemType.RawKind);
			}
			int num = start ?? (-1);
			TextLoader.Range range;
			if (type.IsKnownSizeVector)
			{
				range = new TextLoader.Range
				{
					min = num,
					max = new int?(num + type.ValueCount - 1),
					forceVector = true
				};
			}
			else if (type.IsVector)
			{
				range = new TextLoader.Range
				{
					min = num,
					variableEnd = true
				};
			}
			else
			{
				range = new TextLoader.Range
				{
					min = num
				};
			}
			TextLoader.Range[] array = new TextLoader.Range[] { range };
			return new TextLoader.Column
			{
				name = name,
				keyRange = keyRange,
				source = array,
				type = dataKind
			};
		}

		// Token: 0x04000C2B RID: 3115
		internal const string Summary = "Writes data into a text file.";

		// Token: 0x04000C2C RID: 3116
		private const double _sparseWeight = 2.5;

		// Token: 0x04000C2D RID: 3117
		private readonly bool _forceDense;

		// Token: 0x04000C2E RID: 3118
		private readonly bool _outputSchema;

		// Token: 0x04000C2F RID: 3119
		private readonly bool _outputHeader;

		// Token: 0x04000C30 RID: 3120
		private readonly char _sepChar;

		// Token: 0x04000C31 RID: 3121
		private readonly string _sepStr;

		// Token: 0x04000C32 RID: 3122
		private readonly bool _silent;

		// Token: 0x04000C33 RID: 3123
		private readonly IHost _host;

		// Token: 0x020003B9 RID: 953
		public sealed class Arguments
		{
			// Token: 0x04000C34 RID: 3124
			[Argument(0, HelpText = "Separator", ShortName = "sep")]
			public string separator = "tab";

			// Token: 0x04000C35 RID: 3125
			[Argument(0, HelpText = "Force dense format", ShortName = "dense")]
			public bool dense;

			// Token: 0x04000C36 RID: 3126
			[Argument(4, HelpText = "Suppress any info output (not warnings or errors)", Hide = true)]
			public bool silent;

			// Token: 0x04000C37 RID: 3127
			[Argument(0, HelpText = "Output the comment containing the loader settings", ShortName = "schema")]
			public bool outputSchema = true;

			// Token: 0x04000C38 RID: 3128
			[Argument(0, HelpText = "Output the header", ShortName = "header")]
			public bool outputHeader = true;
		}

		// Token: 0x020003BA RID: 954
		private abstract class ValueWriter
		{
			// Token: 0x06001467 RID: 5223 RVA: 0x0007638C File Offset: 0x0007458C
			public static TextSaver.ValueWriter Create(IRowCursor cursor, int col, char sep)
			{
				ColumnType columnType = cursor.Schema.GetColumnType(col);
				Type type;
				if (columnType.IsVector)
				{
					type = typeof(TextSaver.VecValueWriter<>).MakeGenericType(new Type[] { columnType.ItemType.RawType });
				}
				else
				{
					type = typeof(TextSaver.ValueWriter<>).MakeGenericType(new Type[] { columnType.RawType });
				}
				return (TextSaver.ValueWriter)Activator.CreateInstance(type, new object[] { cursor, columnType, col, sep });
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06001468 RID: 5224
			public abstract string Default { get; }

			// Token: 0x06001469 RID: 5225 RVA: 0x0007642A File Offset: 0x0007462A
			public ValueWriter(int source)
			{
				this.Source = source;
			}

			// Token: 0x0600146A RID: 5226
			public abstract void WriteData(Action<StringBuilder, int> appendItem, out int length);

			// Token: 0x0600146B RID: 5227
			public abstract void WriteHeader(Action<StringBuilder, int> appendItem, out int length);

			// Token: 0x04000C39 RID: 3129
			public readonly int Source;
		}

		// Token: 0x020003BB RID: 955
		private abstract class ValueWriterBase<T> : TextSaver.ValueWriter
		{
			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x0600146C RID: 5228 RVA: 0x00076439 File Offset: 0x00074639
			public override string Default
			{
				get
				{
					return this._default;
				}
			}

			// Token: 0x0600146D RID: 5229 RVA: 0x00076444 File Offset: 0x00074644
			protected ValueWriterBase(PrimitiveType type, int source, char sep)
				: base(source)
			{
				this._sep = sep;
				if (type.IsText)
				{
					ValueMapper<DvText, StringBuilder> valueMapper = new ValueMapper<DvText, StringBuilder>(this.MapText);
					this._conv = (ValueMapper<T, StringBuilder>)valueMapper;
				}
				else if (type.IsTimeSpan)
				{
					ValueMapper<DvTimeSpan, StringBuilder> valueMapper2 = new ValueMapper<DvTimeSpan, StringBuilder>(this.MapTimeSpan);
					this._conv = (ValueMapper<T, StringBuilder>)valueMapper2;
				}
				else if (type.IsDateTime)
				{
					ValueMapper<DvDateTime, StringBuilder> valueMapper3 = new ValueMapper<DvDateTime, StringBuilder>(this.MapDateTime);
					this._conv = (ValueMapper<T, StringBuilder>)valueMapper3;
				}
				else if (type.IsDateTimeZone)
				{
					ValueMapper<DvDateTimeZone, StringBuilder> valueMapper4 = new ValueMapper<DvDateTimeZone, StringBuilder>(this.MapDateTimeZone);
					this._conv = (ValueMapper<T, StringBuilder>)valueMapper4;
				}
				else
				{
					this._conv = Conversions.Instance.GetStringConversion<T>(type);
				}
				T t = default(T);
				this._conv.Invoke(ref t, ref this._sb);
				this._default = this._sb.ToString();
			}

			// Token: 0x0600146E RID: 5230 RVA: 0x00076528 File Offset: 0x00074728
			protected void MapText(ref DvText src, ref StringBuilder sb)
			{
				TextSaverUtils.MapText(ref src, ref sb, this._sep);
			}

			// Token: 0x0600146F RID: 5231 RVA: 0x00076537 File Offset: 0x00074737
			protected void MapTimeSpan(ref DvTimeSpan src, ref StringBuilder sb)
			{
				TextSaverUtils.MapTimeSpan(ref src, ref sb);
			}

			// Token: 0x06001470 RID: 5232 RVA: 0x00076540 File Offset: 0x00074740
			protected void MapDateTime(ref DvDateTime src, ref StringBuilder sb)
			{
				TextSaverUtils.MapDateTime(ref src, ref sb);
			}

			// Token: 0x06001471 RID: 5233 RVA: 0x00076549 File Offset: 0x00074749
			protected void MapDateTimeZone(ref DvDateTimeZone src, ref StringBuilder sb)
			{
				TextSaverUtils.MapDateTimeZone(ref src, ref sb);
			}

			// Token: 0x04000C3A RID: 3130
			protected readonly ValueMapper<T, StringBuilder> _conv;

			// Token: 0x04000C3B RID: 3131
			protected readonly string _default;

			// Token: 0x04000C3C RID: 3132
			protected readonly char _sep;

			// Token: 0x04000C3D RID: 3133
			protected StringBuilder _sb;
		}

		// Token: 0x020003BC RID: 956
		private sealed class VecValueWriter<T> : TextSaver.ValueWriterBase<T>
		{
			// Token: 0x06001472 RID: 5234 RVA: 0x00076554 File Offset: 0x00074754
			public VecValueWriter(IRowCursor cursor, VectorType type, int source, char sep)
				: base(type.ItemType, source, sep)
			{
				this._getSrc = cursor.GetGetter<VBuffer<T>>(source);
				ColumnType metadataTypeOrNull;
				if (type.IsKnownSizeVector && (metadataTypeOrNull = cursor.Schema.GetMetadataTypeOrNull("SlotNames", source)) != null && metadataTypeOrNull.VectorSize == type.VectorSize && metadataTypeOrNull.ItemType.IsText)
				{
					cursor.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", source, ref this._slotNames);
					Contracts.Check(this._slotNames.Length == metadataTypeOrNull.VectorSize, "Unexpected slot names length");
				}
				this._slotCount = type.VectorSize;
			}

			// Token: 0x06001473 RID: 5235 RVA: 0x000765F8 File Offset: 0x000747F8
			public override void WriteData(Action<StringBuilder, int> appendItem, out int length)
			{
				this._getSrc.Invoke(ref this._src);
				if (this._src.IsDense)
				{
					for (int i = 0; i < this._src.Length; i++)
					{
						this._conv.Invoke(ref this._src.Values[i], ref this._sb);
						appendItem(this._sb, i);
					}
				}
				else
				{
					for (int j = 0; j < this._src.Count; j++)
					{
						this._conv.Invoke(ref this._src.Values[j], ref this._sb);
						appendItem(this._sb, this._src.Indices[j]);
					}
				}
				length = this._src.Length;
			}

			// Token: 0x06001474 RID: 5236 RVA: 0x000766C8 File Offset: 0x000748C8
			public override void WriteHeader(Action<StringBuilder, int> appendItem, out int length)
			{
				length = this._slotCount;
				if (this._slotNames.Count == 0)
				{
					return;
				}
				for (int i = 0; i < this._slotNames.Count; i++)
				{
					DvText dvText = this._slotNames.Values[i];
					if (!dvText.IsEmpty)
					{
						base.MapText(ref dvText, ref this._sb);
						int num = (this._slotNames.IsDense ? i : this._slotNames.Indices[i]);
						appendItem(this._sb, num);
					}
				}
			}

			// Token: 0x04000C3E RID: 3134
			private readonly ValueGetter<VBuffer<T>> _getSrc;

			// Token: 0x04000C3F RID: 3135
			private VBuffer<T> _src;

			// Token: 0x04000C40 RID: 3136
			private readonly VBuffer<DvText> _slotNames;

			// Token: 0x04000C41 RID: 3137
			private readonly int _slotCount;
		}

		// Token: 0x020003BD RID: 957
		private sealed class ValueWriter<T> : TextSaver.ValueWriterBase<T>
		{
			// Token: 0x06001475 RID: 5237 RVA: 0x0007675D File Offset: 0x0007495D
			public ValueWriter(IRowCursor cursor, PrimitiveType type, int source, char sep)
				: base(type, source, sep)
			{
				this._getSrc = cursor.GetGetter<T>(source);
				this._columnName = cursor.Schema.GetColumnName(source);
			}

			// Token: 0x06001476 RID: 5238 RVA: 0x00076788 File Offset: 0x00074988
			public override void WriteData(Action<StringBuilder, int> appendItem, out int length)
			{
				this._getSrc.Invoke(ref this._src);
				this._conv.Invoke(ref this._src, ref this._sb);
				appendItem(this._sb, 0);
				length = 1;
			}

			// Token: 0x06001477 RID: 5239 RVA: 0x000767C4 File Offset: 0x000749C4
			public override void WriteHeader(Action<StringBuilder, int> appendItem, out int length)
			{
				DvText dvText = new DvText(this._columnName);
				base.MapText(ref dvText, ref this._sb);
				appendItem(this._sb, 0);
				length = 1;
			}

			// Token: 0x04000C42 RID: 3138
			private readonly ValueGetter<T> _getSrc;

			// Token: 0x04000C43 RID: 3139
			private T _src;

			// Token: 0x04000C44 RID: 3140
			private string _columnName;
		}

		// Token: 0x020003BE RID: 958
		private sealed class State
		{
			// Token: 0x06001478 RID: 5240 RVA: 0x00076804 File Offset: 0x00074A04
			public State(TextSaver parent, TextWriter writer, TextSaver.ValueWriter[] pipes, bool hasHeader)
			{
				this._host = parent._host;
				this._dense = parent._forceDense;
				this._sepChar = parent._sepChar;
				this._sepStr = parent._sepStr;
				this._writer = writer;
				this._pipes = pipes;
				this._hasHeader = hasHeader && parent._outputHeader;
				this._mpcoldst = new int[this._pipes.Length + 1];
				this._mpcolslot = new int[this._pipes.Length + 1];
				this._rgch = new char[1024];
				this._mpslotdst = new int[128];
				this._mpslotichLim = new int[128];
			}

			// Token: 0x06001479 RID: 5241 RVA: 0x000768F0 File Offset: 0x00074AF0
			public void Run(IRowCursor cursor, ref long count, out int minLen, out int maxLen)
			{
				minLen = int.MaxValue;
				maxLen = 0;
				Action<StringBuilder, int> action = delegate(StringBuilder sb, int index)
				{
					this.AppendItem(sb, index, this._pipes[this._col].Default);
				};
				Action<StringBuilder, int> action2 = delegate(StringBuilder sb, int index)
				{
					this.AppendItem(sb, index, "");
				};
				if (this._hasHeader)
				{
					this.StartLine();
					while (this._col < this._pipes.Length)
					{
						int num;
						this._pipes[this._col].WriteHeader(action2, out num);
						this.EndColumn(num);
					}
					this.EndLine("\"\"");
					this._writer.WriteLine();
				}
				while (cursor.MoveNext())
				{
					this.StartLine();
					while (this._col < this._pipes.Length)
					{
						int num2;
						this._pipes[this._col].WriteData(action, out num2);
						this.EndColumn(num2);
					}
					if (minLen > this._dstBase)
					{
						minLen = this._dstBase;
					}
					if (maxLen < this._dstBase)
					{
						maxLen = this._dstBase;
					}
					this.EndLine(null);
					this._writer.WriteLine();
					count += 1L;
				}
			}

			// Token: 0x0600147A RID: 5242 RVA: 0x000769F4 File Offset: 0x00074BF4
			private void StartLine()
			{
				this._cch = 0;
				this._slotLim = 0;
				this._col = 0;
				this._dstBase = 0;
				this._dstPrev = -1;
			}

			// Token: 0x0600147B RID: 5243 RVA: 0x00076A1C File Offset: 0x00074C1C
			private bool Matches(StringBuilder sb, string def)
			{
				if (sb.Length != def.Length)
				{
					return false;
				}
				for (int i = 0; i < def.Length; i++)
				{
					if (sb[i] != def[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600147C RID: 5244 RVA: 0x00076A60 File Offset: 0x00074C60
			private void AppendItem(StringBuilder sb, int index, string def)
			{
				Contracts.Check(index >= 0);
				checked
				{
					int num = this._dstBase + index;
					Contracts.Check(num > this._dstPrev);
					this._dstPrev = num;
					if (this.Matches(sb, def))
					{
						return;
					}
					int i = this._cch + sb.Length;
					while (i > this._rgch.Length)
					{
						Array.Resize<char>(ref this._rgch, this._rgch.Length * 2);
					}
					if (this._mpslotdst.Length <= this._slotLim)
					{
						Array.Resize<int>(ref this._mpslotdst, this._mpslotdst.Length * 2);
					}
					if (this._mpslotichLim.Length <= this._slotLim)
					{
						Array.Resize<int>(ref this._mpslotichLim, this._mpslotichLim.Length * 2);
					}
					sb.CopyTo(0, this._rgch, this._cch, sb.Length);
					this._cch = i;
					this._mpslotichLim[this._slotLim] = this._cch;
					this._mpslotdst[this._slotLim] = num;
				}
				this._slotLim++;
			}

			// Token: 0x0600147D RID: 5245 RVA: 0x00076B6C File Offset: 0x00074D6C
			private void EndColumn(int length)
			{
				int num = checked(this._dstBase + length);
				Contracts.Check(this._dstPrev < num);
				this._col++;
				this._mpcoldst[this._col] = num;
				this._mpcolslot[this._col] = this._slotLim;
				this._dstBase = num;
			}

			// Token: 0x0600147E RID: 5246 RVA: 0x00076BC8 File Offset: 0x00074DC8
			private void EndLine(string defaultStr = null)
			{
				if (this._dense)
				{
					this.WriteDenseTo(this._dstBase, defaultStr);
					return;
				}
				int num = 0;
				double num2 = 2.5 * (double)this._slotLim;
				for (int i = 1; i <= this._pipes.Length; i++)
				{
					int num3 = this._mpcoldst[i];
					int num4 = this._slotLim - this._mpcolslot[i];
					double num5 = (double)num3 + 2.5 * (double)num4;
					if (num2 > num5)
					{
						num2 = num5;
						num = i;
					}
				}
				int num6 = this._mpcoldst[num];
				int num7 = this._dstBase - num6;
				if (num7 < 5 || num7 < num6 / 5)
				{
					num = this._pipes.Length;
				}
				string text = "";
				if (num > 0)
				{
					this.WriteDenseTo(this._mpcoldst[num], defaultStr);
					text = this._sepStr;
				}
				if (num >= this._pipes.Length)
				{
					return;
				}
				this._writer.Write(text);
				text = this._sepStr;
				this._writer.Write(num7);
				int j = this._mpcolslot[num];
				if (j == this._slotLim)
				{
					this._writer.Write(text);
					this._writer.Write("0:");
					this._writer.Write(defaultStr ?? this._pipes[num].Default);
					return;
				}
				int num8 = ((j > 0) ? this._mpslotichLim[j - 1] : 0);
				while (j < this._slotLim)
				{
					this._writer.Write(text);
					this._writer.Write(this._mpslotdst[j] - num6);
					this._writer.Write(':');
					int num9 = this._mpslotichLim[j];
					this._writer.Write(this._rgch, num8, num9 - num8);
					num8 = num9;
					j++;
				}
			}

			// Token: 0x0600147F RID: 5247 RVA: 0x00076D94 File Offset: 0x00074F94
			private void WriteDenseTo(int dstLim, string defaultStr = null)
			{
				string text = "";
				int num = 0;
				string text2 = defaultStr ?? this._pipes[0].Default;
				int num2 = 0;
				int num3 = 0;
				for (int i = 0; i < dstLim; i++)
				{
					this._writer.Write(text);
					text = this._sepStr;
					while (i >= this._mpcoldst[num + 1])
					{
						num++;
						text2 = defaultStr ?? this._pipes[num].Default;
					}
					if (num2 == this._slotLim || i < this._mpslotdst[num2])
					{
						this._writer.Write(text2);
					}
					else
					{
						int num4 = this._mpslotichLim[num2];
						this._writer.Write(this._rgch, num3, num4 - num3);
						num3 = num4;
						num2++;
					}
				}
			}

			// Token: 0x04000C45 RID: 3141
			private readonly bool _dense;

			// Token: 0x04000C46 RID: 3142
			private readonly char _sepChar;

			// Token: 0x04000C47 RID: 3143
			private readonly string _sepStr;

			// Token: 0x04000C48 RID: 3144
			private readonly TextWriter _writer;

			// Token: 0x04000C49 RID: 3145
			private readonly TextSaver.ValueWriter[] _pipes;

			// Token: 0x04000C4A RID: 3146
			private readonly bool _hasHeader;

			// Token: 0x04000C4B RID: 3147
			private readonly IHost _host;

			// Token: 0x04000C4C RID: 3148
			private int _col;

			// Token: 0x04000C4D RID: 3149
			private int _dstBase;

			// Token: 0x04000C4E RID: 3150
			private int _dstPrev;

			// Token: 0x04000C4F RID: 3151
			private int[] _mpcoldst;

			// Token: 0x04000C50 RID: 3152
			private int[] _mpcolslot;

			// Token: 0x04000C51 RID: 3153
			private int _slotLim;

			// Token: 0x04000C52 RID: 3154
			private int[] _mpslotdst;

			// Token: 0x04000C53 RID: 3155
			private int[] _mpslotichLim;

			// Token: 0x04000C54 RID: 3156
			private int _cch;

			// Token: 0x04000C55 RID: 3157
			private char[] _rgch;
		}
	}
}
