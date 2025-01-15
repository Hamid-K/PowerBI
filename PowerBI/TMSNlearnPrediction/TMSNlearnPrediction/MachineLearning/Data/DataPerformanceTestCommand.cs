using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200038C RID: 908
	public sealed class DataPerformanceTestCommand : ICommand
	{
		// Token: 0x0600139B RID: 5019 RVA: 0x0006ECE8 File Offset: 0x0006CEE8
		public DataPerformanceTestCommand(DataPerformanceTestCommand.Arguments args, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<DataPerformanceTestCommand.Arguments>(env, args, "args");
			Contracts.CheckUserArg(env, args.passes >= 0, "trials", "cannot be negative");
			Contracts.CheckUserArg(env, args.reportFrequency >= 0L, "reportFrequency", "cannot be negative");
			Contracts.CheckUserArg(env, !(args.parallel < 0), "parallel", "cannot be negative");
			this._impl = new DataPerformanceTestCommand.Impl(args, env);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0006ED86 File Offset: 0x0006CF86
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x04000B5A RID: 2906
		internal const string LoadName = "DataPerformanceTest";

		// Token: 0x04000B5B RID: 2907
		private readonly DataPerformanceTestCommand.Impl _impl;

		// Token: 0x0200038D RID: 909
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x04000B5C RID: 2908
			[Argument(4, HelpText = "The columns to extract on, * to extract all non-hidden columns, ** for all columns hidden or not", ShortName = "col")]
			public string[] columns;

			// Token: 0x04000B5D RID: 2909
			[Argument(4, HelpText = "Whether to attempt to interpret the data model as an example cursorable", ShortName = "ec")]
			public bool exampleCursorable;

			// Token: 0x04000B5E RID: 2910
			[Argument(4, HelpText = "The number of passes to perform over the data", ShortName = "p")]
			public int passes = 10;

			// Token: 0x04000B5F RID: 2911
			[Argument(4, HelpText = "Write intermediate reports every this many moves on the cursor, within a trial", ShortName = "rf")]
			public long reportFrequency;

			// Token: 0x04000B60 RID: 2912
			[Argument(4, HelpText = "Omit the first pass from the statistics calculation, so that the code and cache has a chance to warm up", ShortName = "warmup")]
			public bool firstIsWarmup = true;

			// Token: 0x04000B61 RID: 2913
			[Argument(4, HelpText = "Generated shuffled cursors starting with with this seed", ShortName = "ss")]
			public int? shuffleSeed;

			// Token: 0x04000B62 RID: 2914
			[Argument(4, HelpText = "Whether to cache the data in main memory")]
			public bool cache;

			// Token: 0x04000B63 RID: 2915
			[Argument(4, HelpText = "Whether we should iterate over a parallel cursor set")]
			public bool set;

			// Token: 0x04000B64 RID: 2916
			[Argument(4, HelpText = "The number of passes to perform over the data columns when they are transposed", ShortName = "tp")]
			public int transposePasses;

			// Token: 0x04000B65 RID: 2917
			[Argument(4, HelpText = "Whether the transposer will force save columns even the column is not 'sliced'", ShortName = "ts")]
			public bool transposeForceSave;

			// Token: 0x04000B66 RID: 2918
			[Argument(4, HelpText = "When true each transpose pass will iterate over all selected columns, else we iterate over each selected columns the given number of times before moving to the next one", ShortName = "tc")]
			public bool transposeByColumn;
		}

		// Token: 0x0200038E RID: 910
		private sealed class Impl : DataCommand.ImplBase<DataPerformanceTestCommand.Arguments>
		{
			// Token: 0x0600139E RID: 5022 RVA: 0x0006EDAC File Offset: 0x0006CFAC
			public Impl(DataPerformanceTestCommand.Arguments args, IHostEnvironment env)
				: base("DataPerformanceTestCommand", args, env, null)
			{
			}

			// Token: 0x0600139F RID: 5023 RVA: 0x0006EDD0 File Offset: 0x0006CFD0
			private Action ColumnOperator(IRowCursor cursor, int col)
			{
				ISchema schema = cursor.Schema;
				Func<IRowCursor, int, Action> func = new Func<IRowCursor, int, Action>(this.ColumnOperatorCore<int>);
				MethodInfo genericMethodDefinition = func.Method.GetGenericMethodDefinition();
				Type rawType = schema.GetColumnType(col).RawType;
				MethodInfo methodInfo = genericMethodDefinition.MakeGenericMethod(new Type[] { rawType });
				object[] array = new object[] { cursor, col };
				return methodInfo.Invoke(this, array) as Action;
			}

			// Token: 0x060013A0 RID: 5024 RVA: 0x0006EE6C File Offset: 0x0006D06C
			private Action ColumnOperatorCore<TValue>(IRowCursor cursor, int col)
			{
				TValue value = default(TValue);
				ValueGetter<TValue> del = cursor.GetGetter<TValue>(col);
				return delegate
				{
					del.Invoke(ref value);
				};
			}

			// Token: 0x060013A1 RID: 5025 RVA: 0x0006EEA4 File Offset: 0x0006D0A4
			private static string FormatTime(TimeSpan span)
			{
				if (span >= new TimeSpan(0, 1, 0, 0))
				{
					return span.ToString("hh\\:mm\\:ss\\.fff");
				}
				if (span >= new TimeSpan(0, 0, 1, 0))
				{
					return span.ToString("mm\\:ss\\.fff");
				}
				return span.ToString("ss\\.fff");
			}

			// Token: 0x060013A2 RID: 5026 RVA: 0x0006EEFC File Offset: 0x0006D0FC
			private static string FormatTicks(double ticks)
			{
				return new TimeSpan((long)ticks).ToString();
			}

			// Token: 0x060013A3 RID: 5027 RVA: 0x0006EF20 File Offset: 0x0006D120
			private Action TransposeOperator(Transposer transposer, int col)
			{
				VectorType slotType = transposer.TransposeSchema.GetSlotType(col);
				PrimitiveType itemType = slotType.ItemType;
				Func<Transposer, int, Action> func = new Func<Transposer, int, Action>(this.TransposeOperatorCore<int>);
				MethodInfo methodInfo = func.Method.GetGenericMethodDefinition().MakeGenericMethod(new Type[] { itemType.RawType });
				return (Action)methodInfo.Invoke(this, new object[] { transposer, col });
			}

			// Token: 0x060013A4 RID: 5028 RVA: 0x0006EFFC File Offset: 0x0006D1FC
			private Action TransposeOperatorCore<TValue>(Transposer transposer, int col)
			{
				return delegate
				{
					VBuffer<TValue> vbuffer = default(VBuffer<TValue>);
					using (ISlotCursor slotCursor = transposer.GetSlotCursor(col))
					{
						ValueGetter<VBuffer<TValue>> getter = slotCursor.GetGetter<TValue>();
						while (slotCursor.MoveNext())
						{
							getter.Invoke(ref vbuffer);
						}
					}
				};
			}

			// Token: 0x060013A5 RID: 5029 RVA: 0x0006F02C File Offset: 0x0006D22C
			public override void Run()
			{
				using (IChannel channel = this._host.Start("DataPerformanceTest"))
				{
					this.RunCore(channel);
					channel.Done();
				}
			}

			// Token: 0x060013A6 RID: 5030 RVA: 0x0006F348 File Offset: 0x0006D548
			private void RunCore(IChannel ch)
			{
				Stopwatch sw = Stopwatch.StartNew();
				IDataView view = null;
				HashSet<int> selectedColumns = new HashSet<int>();
				view = base.CreateAndSaveLoader("TextLoader");
				if (this._args.cache)
				{
					view = new CacheDataView(this._host, view, null);
				}
				ISchema schema = view.Schema;
				sw.Stop();
				ch.Info("Constructed data view in {0}", new object[] { sw.Elapsed });
				if (Utils.Size<string>(this._args.columns) > 0)
				{
					foreach (string text in this._args.columns.SelectMany((string c) => c.Split(new char[] { ',' })))
					{
						int num = 0;
						if (schema.TryGetColumnIndex(text, ref num))
						{
							selectedColumns.Add(num);
						}
						else if (text == "**")
						{
							selectedColumns.UnionWith(Enumerable.Range(0, schema.ColumnCount));
						}
						else if (text == "*")
						{
							selectedColumns.UnionWith(from i in Enumerable.Range(0, schema.ColumnCount)
								where !MetadataUtils.IsHidden(schema, i)
								select i);
						}
						else
						{
							if (!int.TryParse(text, out num))
							{
								throw Contracts.ExceptUserArg(this._host, "columns", string.Format("Could not resolve column '{0}'", text));
							}
							if (0 > num || num >= schema.ColumnCount)
							{
								throw Contracts.ExceptUserArg(this._host, "columns", string.Format("Column '{0}' interpreted as index, but out of range", text));
							}
							selectedColumns.Add(num);
						}
					}
				}
				ch.Info("Reading {0} of {1} column{2}", new object[]
				{
					selectedColumns.Count,
					schema.ColumnCount,
					(schema.ColumnCount == 1) ? "" : "s"
				});
				bool canShuffle = view.CanShuffle;
				Contracts.CheckUserArg(ch, this._args.shuffleSeed == null || canShuffle, "shuffleSeed", "user specified a shuffle seed, but we cannot shuffle the data");
				SummaryStatistics summaryStatistics = new SummaryStatistics();
				TauswortheHybrid tauswortheHybrid = ((this._args.shuffleSeed != null && view != null) ? RandomUtils.Create(this._args.shuffleSeed) : null);
				if (this._args.shuffleSeed != null && view == null)
				{
					new Random(this._args.shuffleSeed.Value);
				}
				for (int i2 = 0; i2 < this._args.passes; i2++)
				{
					sw.Restart();
					long count = 0L;
					Action<IRowCursor> cursorAction = delegate(IRowCursor cursor)
					{
						using (cursor)
						{
							long num4 = 0L;
							long num5 = this._args.reportFrequency;
							Action[] array3 = (from col in selectedColumns
								orderby col
								select this.ColumnOperator(cursor, col)).ToArray<Action>();
							while (cursor.MoveNext())
							{
								for (int num6 = 0; num6 < array3.Length; num6++)
								{
									array3[num6]();
								}
								num4 += 1L;
								if (this._args.reportFrequency > 0L && (num5 -= 1L) == 0L)
								{
									ch.Info("{0}: Finished {1}", new object[] { sw.Elapsed, num4 });
									num5 = this._args.reportFrequency;
								}
							}
							Interlocked.Add(ref count, num4);
						}
					};
					if (this._args.set)
					{
						int threadCount = DataViewUtils.GetThreadCount(this._host, 0, false);
						IRowCursorConsolidator rowCursorConsolidator;
						IRowCursor[] rowCursorSet = view.GetRowCursorSet(ref rowCursorConsolidator, new Func<int, bool>(selectedColumns.Contains), threadCount, tauswortheHybrid);
						ch.Info("Created cursor set, requested {0} got {1}", new object[]
						{
							this._host.ConcurrencyFactor,
							rowCursorSet.Length
						});
						Thread[] array = new Thread[rowCursorSet.Length];
						for (int j = 0; j < array.Length; j++)
						{
							IRowCursor cursor = rowCursorSet[j];
							array[j] = Utils.CreateBackgroundThread(delegate
							{
								cursorAction(cursor);
							});
							array[j].Start();
						}
						foreach (Thread thread in array)
						{
							thread.Join();
						}
					}
					else
					{
						IRowCursor rowCursor = view.GetRowCursor(new Func<int, bool>(selectedColumns.Contains), tauswortheHybrid);
						cursorAction(rowCursor);
					}
					sw.Stop();
					ch.Info("{0}: Read {1} total in trial {2} of {3}", new object[]
					{
						sw.Elapsed,
						count,
						i2 + 1,
						this._args.passes
					});
					if (i2 > 0 || !this._args.firstIsWarmup)
					{
						summaryStatistics.Add((double)sw.Elapsed.Ticks, 1.0, 1L);
					}
				}
				if (this._args.passes > 0)
				{
					ch.Info("Timings are:");
					ch.Info("    Mean {0} ± {1}", new object[]
					{
						DataPerformanceTestCommand.Impl.FormatTicks(summaryStatistics.Mean),
						DataPerformanceTestCommand.Impl.FormatTicks(summaryStatistics.StandardErrorMean)
					});
					ch.Info("    MinMax {0} to {1}", new object[]
					{
						DataPerformanceTestCommand.Impl.FormatTicks(summaryStatistics.Min),
						DataPerformanceTestCommand.Impl.FormatTicks(summaryStatistics.Max)
					});
				}
				if (this._args.transposePasses > 0 && view != null)
				{
					int passes = this._args.transposePasses;
					ch.Info("Beginning {0} transpose pass{1}", new object[]
					{
						passes,
						(passes > 1) ? "es" : ""
					});
					int[] selected = selectedColumns.OrderBy((int col) => col).ToArray<int>();
					sw.Restart();
					Transposer transposer = Transposer.Create(this._host, view, this._args.transposeForceSave, selected);
					sw.Stop();
					ch.Info("Constructed transposer in {0}", new object[] { sw.Elapsed });
					Action[] transOps = new Action[selected.Length];
					for (int l = 0; l < transOps.Length; l++)
					{
						transOps[l] = this.TransposeOperator(transposer, selected[l]);
					}
					Action<int, int> action = delegate(int c, int p)
					{
						sw.Restart();
						transOps[c]();
						sw.Stop();
						ch.Info("{0}: Transposed {1}[{2}] in trial {3} of {4}", new object[]
						{
							sw.Elapsed,
							view.Schema.GetColumnName(selected[c]),
							selected[c],
							p + 1,
							passes
						});
					};
					if (this._args.transposeByColumn)
					{
						for (int m = 0; m < passes; m++)
						{
							for (int n = 0; n < selected.Length; n++)
							{
								action(n, m);
							}
						}
						return;
					}
					for (int num2 = 0; num2 < selected.Length; num2++)
					{
						for (int num3 = 0; num3 < passes; num3++)
						{
							action(num2, num3);
						}
					}
				}
			}
		}
	}
}
