using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Normalization
{
	// Token: 0x02000909 RID: 2313
	internal sealed class NormalizationModule : Module
	{
		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x060041E2 RID: 16866 RVA: 0x000DE466 File Offset: 0x000DC666
		public override string Name
		{
			get
			{
				return "Normalization";
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x060041E3 RID: 16867 RVA: 0x000DE46D File Offset: 0x000DC66D
		public override Keys ExportKeys
		{
			get
			{
				return NormalizationModule.exportKeys;
			}
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000DE474 File Offset: 0x000DC674
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(NormalizationModule.exportKeys, new Value[] { NormalizationModule.TableFromValue });
		}

		// Token: 0x040022A0 RID: 8864
		private static readonly Keys exportKeys = Keys.New("Table.FromValue");

		// Token: 0x040022A1 RID: 8865
		public static readonly FunctionValue TableFromValue = new NormalizationModule.TableFromValueFunctionValue();

		// Token: 0x0200090A RID: 2314
		private sealed class TableFromValueFunctionValue : NativeFunctionValue2<TableValue, Value, Value>, IAccumulableChainingFunction
		{
			// Token: 0x060041E7 RID: 16871 RVA: 0x000DE4A9 File Offset: 0x000DC6A9
			public TableFromValueFunctionValue()
				: base(TypeValue.Table, 1, "value", TypeValue.Any, "options", NullableTypeValue.Record)
			{
			}

			// Token: 0x1700150E RID: 5390
			// (get) Token: 0x060041E8 RID: 16872 RVA: 0x000DE4CB File Offset: 0x000DC6CB
			public string EnumerableParameter
			{
				get
				{
					return "value";
				}
			}

			// Token: 0x060041E9 RID: 16873 RVA: 0x000DE4D4 File Offset: 0x000DC6D4
			public override TableValue TypedInvoke(Value value, Value options)
			{
				Keys defaultKeys = NormalizationModule.TableFromValueFunctionValue.GetDefaultKeys(options);
				switch (value.Kind)
				{
				case ValueKind.List:
					return new NormalizationModule.TableFromValueFunctionValue.NormalizedListValue(value.AsList, defaultKeys).ToTable(defaultKeys);
				case ValueKind.Record:
					return Library.Record.ToTable.Invoke(value).AsTable;
				case ValueKind.Table:
				{
					TableValue tableValue = value.AsTable;
					if (tableValue.Columns.Contains(NormalizationModule.TableFromValueFunctionValue.EmptyColumnName.AsString))
					{
						int num = 1;
						string text;
						for (;;)
						{
							text = string.Format(CultureInfo.InvariantCulture, "Column{0}", num);
							if (!tableValue.Columns.Contains(text))
							{
								break;
							}
							num++;
						}
						tableValue = tableValue.RenameColumns(ListValue.New(new Value[]
						{
							NormalizationModule.TableFromValueFunctionValue.EmptyColumnName,
							TextValue.New(text)
						}), Value.Null);
					}
					return tableValue;
				}
				default:
				{
					RecordValue recordValue = RecordValue.New(defaultKeys, new Value[] { value });
					RecordTypeValue recordTypeValue = RecordTypeValue.New(RecordValue.New(defaultKeys, new Value[] { RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						value.Type,
						LogicalValue.False
					}) }));
					return ListValue.New(new Value[] { recordValue }).ToTable(TableTypeValue.New(recordTypeValue));
				}
				}
			}

			// Token: 0x060041EA RID: 16874 RVA: 0x00049610 File Offset: 0x00047810
			public override bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
			{
				accumulableChainingFunction = this;
				return true;
			}

			// Token: 0x060041EB RID: 16875 RVA: 0x000DE60D File Offset: 0x000DC80D
			public IAccumulable CreateAccumulable(RecordValue arguments, IAccumulable accumulable)
			{
				ListValue asList = arguments[this.EnumerableParameter].AsList;
				return new NormalizationModule.TableFromValueFunctionValue.TableFromListAccumulable(accumulable, arguments["options"]);
			}

			// Token: 0x060041EC RID: 16876 RVA: 0x000DE634 File Offset: 0x000DC834
			private static Keys GetDefaultKeys(Value options)
			{
				Value value;
				if (!options.IsNull && Options.ValidateOptions(options.AsRecord, NormalizationModule.TableFromValueFunctionValue.validOptionKeys, "Table.FromValue", null).TryGetValue("DefaultColumnName", out value))
				{
					return Keys.New(value.AsString);
				}
				return NormalizationModule.TableFromValueFunctionValue.DefaultKeys;
			}

			// Token: 0x040022A2 RID: 8866
			private const string enumerableParameter = "value";

			// Token: 0x040022A3 RID: 8867
			private const string ColumnTemplate = "Column{0}";

			// Token: 0x040022A4 RID: 8868
			private const string DefaultColumnName = "Value";

			// Token: 0x040022A5 RID: 8869
			private const string DefaultColumnNameOptionsKey = "DefaultColumnName";

			// Token: 0x040022A6 RID: 8870
			private static readonly Keys DefaultKeys = Keys.New("Value");

			// Token: 0x040022A7 RID: 8871
			private static readonly TextValue EmptyColumnName = TextValue.New("");

			// Token: 0x040022A8 RID: 8872
			private static readonly HashSet<string> validOptionKeys = new HashSet<string>(new string[] { "DefaultColumnName" });

			// Token: 0x0200090B RID: 2315
			private sealed class NormalizedListValue : StreamedListValue
			{
				// Token: 0x060041EE RID: 16878 RVA: 0x000DE6B6 File Offset: 0x000DC8B6
				public NormalizedListValue(ListValue list, Keys keys)
				{
					this.list = list;
					this.keys = keys;
				}

				// Token: 0x1700150F RID: 5391
				// (get) Token: 0x060041EF RID: 16879 RVA: 0x000DE6CC File Offset: 0x000DC8CC
				public override long LargeCount
				{
					get
					{
						return this.list.LargeCount;
					}
				}

				// Token: 0x060041F0 RID: 16880 RVA: 0x000DE6D9 File Offset: 0x000DC8D9
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					return new NormalizationModule.TableFromValueFunctionValue.NormalizedListValue.NormalizedEnumerator(this.list.GetEnumerator(), this.keys);
				}

				// Token: 0x040022A9 RID: 8873
				private readonly ListValue list;

				// Token: 0x040022AA RID: 8874
				private readonly Keys keys;

				// Token: 0x0200090C RID: 2316
				private class NormalizedEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
				{
					// Token: 0x060041F1 RID: 16881 RVA: 0x000DE6F1 File Offset: 0x000DC8F1
					public NormalizedEnumerator(IEnumerator<IValueReference> enumerator, Keys keys)
					{
						this.enumerator = enumerator;
						this.keys = keys;
					}

					// Token: 0x17001510 RID: 5392
					// (get) Token: 0x060041F2 RID: 16882 RVA: 0x000DE707 File Offset: 0x000DC907
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x060041F3 RID: 16883 RVA: 0x000DE70F File Offset: 0x000DC90F
					public void Dispose()
					{
						this.enumerator.Dispose();
					}

					// Token: 0x060041F4 RID: 16884 RVA: 0x0000EE09 File Offset: 0x0000D009
					public void Reset()
					{
						throw new InvalidOperationException();
					}

					// Token: 0x17001511 RID: 5393
					// (get) Token: 0x060041F5 RID: 16885 RVA: 0x000DE71C File Offset: 0x000DC91C
					public IValueReference Current
					{
						get
						{
							if (this.current == null)
							{
								this.current = RecordValue.New(this.keys, new IValueReference[] { this.enumerator.Current });
							}
							return this.current;
						}
					}

					// Token: 0x060041F6 RID: 16886 RVA: 0x000DE751 File Offset: 0x000DC951
					public bool MoveNext()
					{
						this.current = null;
						return this.enumerator.MoveNext();
					}

					// Token: 0x040022AB RID: 8875
					private readonly IEnumerator<IValueReference> enumerator;

					// Token: 0x040022AC RID: 8876
					private readonly Keys keys;

					// Token: 0x040022AD RID: 8877
					private RecordValue current;
				}
			}

			// Token: 0x0200090D RID: 2317
			private sealed class TableFromListAccumulable : IAccumulable
			{
				// Token: 0x060041F7 RID: 16887 RVA: 0x000DE765 File Offset: 0x000DC965
				public TableFromListAccumulable(IAccumulable accumulable, Value options)
				{
					this.accumulable = accumulable;
					this.defaultKeys = NormalizationModule.TableFromValueFunctionValue.GetDefaultKeys(options);
				}

				// Token: 0x060041F8 RID: 16888 RVA: 0x000DE780 File Offset: 0x000DC980
				public IAccumulator CreateAccumulator()
				{
					return new NormalizationModule.TableFromValueFunctionValue.TableFromListAccumulable.TableFromListAccumulator(this);
				}

				// Token: 0x040022AE RID: 8878
				private readonly IAccumulable accumulable;

				// Token: 0x040022AF RID: 8879
				private readonly Keys defaultKeys;

				// Token: 0x0200090E RID: 2318
				private sealed class TableFromListAccumulator : TransformingAccumulator
				{
					// Token: 0x060041F9 RID: 16889 RVA: 0x000DE788 File Offset: 0x000DC988
					public TableFromListAccumulator(NormalizationModule.TableFromValueFunctionValue.TableFromListAccumulable accumulable)
						: base(accumulable.accumulable.CreateAccumulator())
					{
						this.defaultKeys = accumulable.defaultKeys;
					}

					// Token: 0x060041FA RID: 16890 RVA: 0x000DE7A7 File Offset: 0x000DC9A7
					protected override IValueReference Transform(IValueReference valueReference)
					{
						return RecordValue.New(this.defaultKeys, new IValueReference[] { valueReference });
					}

					// Token: 0x040022B0 RID: 8880
					private readonly Keys defaultKeys;
				}
			}
		}
	}
}
