using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200017A RID: 378
	public static class DataViewUtils
	{
		// Token: 0x0600079A RID: 1946 RVA: 0x00028A54 File Offset: 0x00026C54
		public static string GetTempColumnName(this ISchema schema, string tag = null)
		{
			Contracts.CheckValue<ISchema>(schema, "schema");
			int num = 0;
			string text;
			for (;;)
			{
				text = (string.IsNullOrWhiteSpace(tag) ? string.Format("temp_{0:000}", num) : string.Format("temp_{0}_{1:000}", tag, num));
				int num2;
				if (!schema.TryGetColumnIndex(text, ref num2))
				{
					break;
				}
				num++;
			}
			return text;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00028AB0 File Offset: 0x00026CB0
		public static long ComputeRowCount(IDataView view)
		{
			long? rowCount = view.GetRowCount(false);
			if (rowCount != null)
			{
				return rowCount.Value;
			}
			long num = 0L;
			using (IRowCursor rowCursor = view.GetRowCursor((int col) => false, null))
			{
				while (rowCursor.MoveNext())
				{
					num += 1L;
				}
			}
			return num;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00028B2C File Offset: 0x00026D2C
		public static int GetThreadCount(IHost host, int num = 0, bool preferOne = false)
		{
			Contracts.CheckValue<IHost>(host, "host");
			int num2 = host.ConcurrencyFactor;
			if (num2 <= 0)
			{
				num2 = Math.Max(2, Environment.ProcessorCount - 1);
			}
			if (num > 0)
			{
				return Math.Min(num, 2 * num2);
			}
			if (preferOne)
			{
				return 1;
			}
			return num2;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00028B74 File Offset: 0x00026D74
		public static bool TryCreateConsolidatingCursor(out IRowCursor curs, IDataView view, Func<int, bool> predicate, IHost host, IRandom rand)
		{
			Contracts.CheckValue<IHost>(host, "host");
			Contracts.CheckValue<IDataView>(host, view, "view");
			Contracts.CheckValue<Func<int, bool>>(host, predicate, "predicate");
			int threadCount = DataViewUtils.GetThreadCount(host, 0, false);
			if (threadCount == 1 || !DataViewUtils.AllCachable(view.Schema, predicate))
			{
				curs = null;
				return false;
			}
			IRowCursorConsolidator rowCursorConsolidator;
			IRowCursor[] rowCursorSet = view.GetRowCursorSet(ref rowCursorConsolidator, predicate, threadCount, rand);
			Contracts.Check(host, Utils.Size<IRowCursor>(rowCursorSet) > 0);
			Contracts.Check(host, rowCursorSet.Length == 1 || rowCursorConsolidator != null);
			if (rowCursorSet.Length == 1)
			{
				curs = rowCursorSet[0];
			}
			else
			{
				curs = rowCursorConsolidator.CreateCursor(host, rowCursorSet);
			}
			return true;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00028C10 File Offset: 0x00026E10
		public static IRowCursor[] CreateSplitCursors(out IRowCursorConsolidator consolidator, IChannelProvider provider, IRowCursor input, int num)
		{
			Contracts.CheckValue<IChannelProvider>(provider, "provider");
			Contracts.CheckValue<IRowCursor>(provider, input, "input");
			consolidator = null;
			if (num <= 1)
			{
				return new IRowCursor[] { input };
			}
			if (!DataViewUtils.AllCachable(input.Schema, new Func<int, bool>(input.IsColumnActive)))
			{
				return new IRowCursor[] { input };
			}
			return DataViewUtils.Splitter.Split(out consolidator, provider, input.Schema, input, num);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00028C80 File Offset: 0x00026E80
		public static bool AllCachable(ISchema schema, Func<int, bool> predicate)
		{
			Contracts.CheckValue<ISchema>(schema, "schema");
			Contracts.CheckValue<Func<int, bool>>(predicate, "predicate");
			for (int i = 0; i < schema.ColumnCount; i++)
			{
				if (predicate(i))
				{
					ColumnType columnType = schema.GetColumnType(i);
					if (!columnType.IsCachable())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00028CD0 File Offset: 0x00026ED0
		public static bool IsCachable(this ColumnType type)
		{
			return type != null && (type.IsPrimitive || type.IsVector);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00028CE8 File Offset: 0x00026EE8
		public static bool SameSchemaAndActivity(IRowCursor[] cursors)
		{
			if (Utils.Size<IRowCursor>(cursors) == 0)
			{
				return true;
			}
			IRowCursor rowCursor = cursors[0];
			if (rowCursor == null)
			{
				return false;
			}
			if (cursors.Length == 1)
			{
				return true;
			}
			ISchema schema = rowCursor.Schema;
			for (int i = 1; i < cursors.Length; i++)
			{
				if (cursors[i] == null || cursors[i].Schema != schema)
				{
					return false;
				}
			}
			for (int j = 0; j < schema.ColumnCount; j++)
			{
				bool flag = rowCursor.IsColumnActive(j);
				for (int k = 1; k < cursors.Length; k++)
				{
					if (cursors[k].IsColumnActive(j) != flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00028D74 File Offset: 0x00026F74
		public static IRowCursor ConsolidateGeneric(IChannelProvider provider, IRowCursor[] inputs, int batchSize)
		{
			Contracts.CheckValue<IChannelProvider>(provider, "provider");
			Contracts.CheckNonEmpty<IRowCursor>(provider, inputs, "inputs");
			Contracts.CheckParam(provider, batchSize >= 0, "bufSize");
			if (inputs.Length == 1)
			{
				return inputs[0];
			}
			object[] array = null;
			return DataViewUtils.Splitter.Consolidator.Consolidate(provider, inputs, batchSize, ref array);
		}

		// Token: 0x0200017B RID: 379
		private sealed class Splitter
		{
			// Token: 0x060007A4 RID: 1956 RVA: 0x00028DC1 File Offset: 0x00026FC1
			private Splitter(ISchema schema)
			{
				this._schema = schema;
				this._cachePools = new object[this._schema.ColumnCount + 1];
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x00028DE8 File Offset: 0x00026FE8
			public static IRowCursor[] Split(out IRowCursorConsolidator consolidator, IChannelProvider provider, ISchema schema, IRowCursor input, int cthd)
			{
				DataViewUtils.Splitter splitter = new DataViewUtils.Splitter(schema);
				IRowCursor[] array2;
				using (IChannel channel = provider.Start("CursorSplitter"))
				{
					IRowCursor[] array = splitter.SplitCore(out consolidator, channel, input, cthd);
					channel.Done();
					array2 = array;
				}
				return array2;
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x000290C0 File Offset: 0x000272C0
			private IRowCursor[] SplitCore(out IRowCursorConsolidator consolidator, IChannel ch, IRowCursor input, int cthd)
			{
				int[] array;
				int[] array2;
				Utils.BuildSubsetMaps(this._schema.ColumnCount, new Func<int, bool>(input.IsColumnActive), ref array, ref array2);
				Func<IRowCursor, int, DataViewUtils.Splitter.InPipe> func = new Func<IRowCursor, int, DataViewUtils.Splitter.InPipe>(this.CreateInPipe<int>);
				MethodInfo genericMethodDefinition = func.GetMethodInfo().GetGenericMethodDefinition();
				object[] array3 = new object[] { input, 0 };
				DataViewUtils.Splitter.InPipe[] inPipes = new DataViewUtils.Splitter.InPipe[array.Length + 1];
				DataViewUtils.Splitter.OutPipe[][] outPipes = new DataViewUtils.Splitter.OutPipe[cthd][];
				for (int i = 0; i < cthd; i++)
				{
					outPipes[i] = new DataViewUtils.Splitter.OutPipe[inPipes.Length];
				}
				for (int j = 0; j < array.Length; j++)
				{
					ColumnType columnType = input.Schema.GetColumnType(array[j]);
					array3[1] = array[j];
					DataViewUtils.Splitter.InPipe inPipe = (inPipes[j] = (DataViewUtils.Splitter.InPipe)genericMethodDefinition.MakeGenericMethod(new Type[] { columnType.RawType }).Invoke(this, array3));
					for (int k = 0; k < cthd; k++)
					{
						outPipes[k][j] = inPipe.CreateOutPipe(columnType);
					}
				}
				int num = array.Length;
				inPipes[num] = this.CreateIdInPipe(input);
				for (int l = 0; l < cthd; l++)
				{
					outPipes[l][num] = inPipes[num].CreateOutPipe(NumberType.UG);
				}
				BlockingCollection<DataViewUtils.Splitter.Batch> toConsume = new BlockingCollection<DataViewUtils.Splitter.Batch>(4);
				MadeObjectPool<DataViewUtils.Splitter.BatchColumn[]> batchColumnPool = new MadeObjectPool<DataViewUtils.Splitter.BatchColumn[]>(() => new DataViewUtils.Splitter.BatchColumn[inPipes.Length]);
				bool done = false;
				int outputsRunning = cthd;
				Thread thread = Utils.CreateBackgroundThread(delegate
				{
					DataViewUtils.Splitter.Batch batch = null;
					try
					{
						using (input)
						{
							long batchId = 0L;
							int count = 0;
							Action action2 = delegate
							{
								DataViewUtils.Splitter.BatchColumn[] array5 = batchColumnPool.Get();
								for (int num3 = 0; num3 < inPipes.Length; num3++)
								{
									array5[num3] = inPipes[num3].GetBatchColumnAndReset();
								}
								MadeObjectPool<DataViewUtils.Splitter.BatchColumn[]> batchColumnPool2 = batchColumnPool;
								DataViewUtils.Splitter.BatchColumn[] array6 = array5;
								int count2 = count;
								long batchId2;
								batchId = (batchId2 = batchId) + 1L;
								DataViewUtils.Splitter.Batch batch2 = new DataViewUtils.Splitter.Batch(batchColumnPool2, array6, count2, batchId2);
								count = 0;
								toConsume.Add(batch2);
							};
							while (input.MoveNext() && !done)
							{
								foreach (DataViewUtils.Splitter.InPipe inPipe2 in inPipes)
								{
									inPipe2.Fill();
								}
								if (++count >= 128)
								{
									action2();
								}
							}
							if (count > 0)
							{
								action2();
							}
						}
					}
					catch (Exception ex)
					{
						batch = new DataViewUtils.Splitter.Batch(ex);
					}
					finally
					{
						toConsume.Add(batch);
						for (int num2 = 1; num2 < cthd; num2++)
						{
							toConsume.Add(null);
						}
						toConsume.CompleteAdding();
					}
				});
				thread.Start();
				Action action = delegate
				{
					if (Interlocked.Decrement(ref outputsRunning) == 0)
					{
						done = true;
						DataViewUtils.Splitter.OutPipe[] array7 = outPipes[0];
						foreach (DataViewUtils.Splitter.Batch batch3 in toConsume.GetConsumingEnumerable())
						{
							if (batch3 != null)
							{
								batch3.SetAll(array7);
								foreach (DataViewUtils.Splitter.OutPipe outPipe in array7)
								{
									outPipe.Unset();
								}
							}
						}
						thread.Join();
					}
				};
				DataViewUtils.Splitter.Cursor[] array4 = new DataViewUtils.Splitter.Cursor[cthd];
				for (int m = 0; m < cthd; m++)
				{
					array4[m] = new DataViewUtils.Splitter.Cursor(ch, this._schema, array, array2, outPipes[m], toConsume, action);
				}
				consolidator = new DataViewUtils.Splitter.Consolidator(this);
				return array4;
			}

			// Token: 0x060007A7 RID: 1959 RVA: 0x00029350 File Offset: 0x00027550
			private DataViewUtils.Splitter.InPipe CreateInPipe<T>(IRow input, int col)
			{
				return this.CreateInPipeCore<T>(col, input.GetGetter<T>(col));
			}

			// Token: 0x060007A8 RID: 1960 RVA: 0x00029360 File Offset: 0x00027560
			private DataViewUtils.Splitter.InPipe CreateIdInPipe(IRow input)
			{
				return this.CreateInPipeCore<UInt128>(this._schema.ColumnCount, input.GetIdGetter());
			}

			// Token: 0x060007A9 RID: 1961 RVA: 0x0002937C File Offset: 0x0002757C
			private DataViewUtils.Splitter.InPipe CreateInPipeCore<T>(int poolIdx, ValueGetter<T> getter)
			{
				Func<T[]> func = null;
				MadeObjectPool<T[]> madeObjectPool = (MadeObjectPool<T[]>)this._cachePools[poolIdx];
				if (madeObjectPool == null)
				{
					object[] cachePools = this._cachePools;
					if (func == null)
					{
						func = () => null;
					}
					Interlocked.CompareExchange(ref cachePools[poolIdx], new MadeObjectPool<T[]>(func), null);
					madeObjectPool = (MadeObjectPool<T[]>)this._cachePools[poolIdx];
				}
				return DataViewUtils.Splitter.InPipe.Create<T>(madeObjectPool, getter);
			}

			// Token: 0x040003F4 RID: 1012
			private readonly ISchema _schema;

			// Token: 0x040003F5 RID: 1013
			private readonly object[] _cachePools;

			// Token: 0x040003F6 RID: 1014
			private object[] _consolidateCachePools;

			// Token: 0x0200017C RID: 380
			private enum ExtraIndex
			{
				// Token: 0x040003F8 RID: 1016
				Id,
				// Token: 0x040003F9 RID: 1017
				_Lim
			}

			// Token: 0x0200017D RID: 381
			public sealed class Consolidator : IRowCursorConsolidator
			{
				// Token: 0x060007AB RID: 1963 RVA: 0x000293DA File Offset: 0x000275DA
				public Consolidator(DataViewUtils.Splitter splitter)
				{
					this._splitter = splitter;
				}

				// Token: 0x060007AC RID: 1964 RVA: 0x000293E9 File Offset: 0x000275E9
				public IRowCursor CreateCursor(IChannelProvider provider, IRowCursor[] inputs)
				{
					return DataViewUtils.Splitter.Consolidator.Consolidate(provider, inputs, 128, ref this._splitter._consolidateCachePools);
				}

				// Token: 0x060007AD RID: 1965 RVA: 0x00029404 File Offset: 0x00027604
				public static IRowCursor Consolidate(IChannelProvider provider, IRowCursor[] inputs, int batchSize, ref object[] ourPools)
				{
					IRowCursor rowCursor2;
					using (IChannel channel = provider.Start("Consolidate"))
					{
						IRowCursor rowCursor = DataViewUtils.Splitter.Consolidator.ConsolidateCore(provider, inputs, ref ourPools, channel);
						channel.Done();
						rowCursor2 = rowCursor;
					}
					return rowCursor2;
				}

				// Token: 0x060007AE RID: 1966 RVA: 0x000298A0 File Offset: 0x00027AA0
				private static IRowCursor ConsolidateCore(IChannelProvider provider, IRowCursor[] inputs, ref object[] ourPools, IChannel ch)
				{
					Contracts.CheckNonEmpty<IRowCursor>(ch, inputs, "inputs");
					if (inputs.Length == 1)
					{
						return inputs[0];
					}
					Contracts.CheckParam(ch, DataViewUtils.SameSchemaAndActivity(inputs), "inputs", "Inputs not compatible for consolidation");
					IRowCursor rowCursor = inputs[0];
					ISchema schema = rowCursor.Schema;
					Contracts.CheckParam(ch, DataViewUtils.AllCachable(schema, new Func<int, bool>(rowCursor.IsColumnActive)), "inputs", "Inputs had some uncachable input columns");
					int[] activeToCol;
					int[] array;
					Utils.BuildSubsetMaps(schema.ColumnCount, new Func<int, bool>(rowCursor.IsColumnActive), ref activeToCol, ref array);
					if (Utils.Size<object>(ourPools) != schema.ColumnCount)
					{
						ourPools = new object[schema.ColumnCount + 1];
					}
					DataViewUtils.Splitter.OutPipe[] outPipes = new DataViewUtils.Splitter.OutPipe[activeToCol.Length + 1];
					for (int i = 0; i < activeToCol.Length; i++)
					{
						int num = activeToCol[i];
						ColumnType columnType = schema.GetColumnType(num);
						object pool = DataViewUtils.Splitter.Consolidator.GetPool(columnType, ourPools, num);
						outPipes[i] = DataViewUtils.Splitter.OutPipe.Create(columnType, pool);
					}
					int idIdx = activeToCol.Length;
					outPipes[idIdx] = DataViewUtils.Splitter.OutPipe.Create(NumberType.UG, DataViewUtils.Splitter.Consolidator.GetPool(NumberType.UG, ourPools, idIdx));
					BlockingCollection<DataViewUtils.Splitter.Batch> toConsume = new BlockingCollection<DataViewUtils.Splitter.Batch>(4);
					MadeObjectPool<DataViewUtils.Splitter.BatchColumn[]> batchColumnPool = new MadeObjectPool<DataViewUtils.Splitter.BatchColumn[]>(() => new DataViewUtils.Splitter.BatchColumn[outPipes.Length]);
					Thread[] workers = new Thread[inputs.Length];
					MinWaiter waiter = new MinWaiter(workers.Length);
					bool done = false;
					for (int j = 0; j < workers.Length; j++)
					{
						IRowCursor localCursor = inputs[j];
						workers[j] = Utils.CreateBackgroundThread(delegate
						{
							DataViewUtils.Splitter.Batch batch = null;
							try
							{
								using (localCursor)
								{
									DataViewUtils.Splitter.InPipe[] inPipes = new DataViewUtils.Splitter.InPipe[outPipes.Length];
									for (int k = 0; k < activeToCol.Length; k++)
									{
										inPipes[k] = outPipes[k].CreateInPipe(RowCursorUtils.GetGetterAsDelegate(localCursor, activeToCol[k]));
									}
									inPipes[idIdx] = outPipes[idIdx].CreateInPipe(localCursor.GetIdGetter());
									long oldBatch = 0L;
									int count = 0;
									ManualResetEventSlim waiterEvent = null;
									Action action2 = delegate
									{
										if (count > 0)
										{
											DataViewUtils.Splitter.BatchColumn[] array2 = batchColumnPool.Get();
											for (int n = 0; n < inPipes.Length; n++)
											{
												array2[n] = inPipes[n].GetBatchColumnAndReset();
											}
											DataViewUtils.Splitter.Batch batch2 = new DataViewUtils.Splitter.Batch(batchColumnPool, array2, count, oldBatch);
											count = 0;
											waiterEvent.Wait();
											waiterEvent = null;
											toConsume.Add(batch2);
										}
									};
									if (localCursor.MoveNext() && !done)
									{
										oldBatch = localCursor.Batch;
										foreach (DataViewUtils.Splitter.InPipe inPipe in inPipes)
										{
											inPipe.Fill();
										}
										count++;
										waiterEvent = waiter.Register(oldBatch);
										while (localCursor.MoveNext() && !done)
										{
											if (oldBatch != localCursor.Batch)
											{
												action2();
												oldBatch = localCursor.Batch;
												waiterEvent = waiter.Register(oldBatch);
											}
											foreach (DataViewUtils.Splitter.InPipe inPipe2 in inPipes)
											{
												inPipe2.Fill();
											}
											count++;
										}
										action2();
									}
								}
							}
							catch (Exception ex)
							{
								batch = new DataViewUtils.Splitter.Batch(ex);
								toConsume.Add(new DataViewUtils.Splitter.Batch(ex));
							}
							finally
							{
								if (waiter.Retire() == 0)
								{
									if (batch == null)
									{
										toConsume.Add(null);
									}
									toConsume.CompleteAdding();
								}
							}
						});
						workers[j].Start();
					}
					Action action = delegate
					{
						done = true;
						DataViewUtils.Splitter.OutPipe[] outPipes2 = outPipes;
						foreach (DataViewUtils.Splitter.Batch batch3 in toConsume.GetConsumingEnumerable())
						{
							if (batch3 != null)
							{
								batch3.SetAll(outPipes2);
								foreach (DataViewUtils.Splitter.OutPipe outPipe in outPipes2)
								{
									outPipe.Unset();
								}
							}
						}
						foreach (Thread thread in workers)
						{
							thread.Join();
						}
					};
					return new DataViewUtils.Splitter.Cursor(provider, schema, activeToCol, array, outPipes, toConsume, action);
				}

				// Token: 0x060007AF RID: 1967 RVA: 0x00029AC8 File Offset: 0x00027CC8
				private static object GetPool(ColumnType type, object[] pools, int poolIdx)
				{
					Func<object[], int, object> func = new Func<object[], int, object>(DataViewUtils.Splitter.Consolidator.GetPoolCore<int>);
					MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { type.RawType });
					return methodInfo.Invoke(null, new object[] { pools, poolIdx });
				}

				// Token: 0x060007B0 RID: 1968 RVA: 0x00029B24 File Offset: 0x00027D24
				private static MadeObjectPool<T[]> GetPoolCore<T>(object[] pools, int poolIdx)
				{
					MadeObjectPool<T[]> madeObjectPool = pools[poolIdx] as MadeObjectPool<T[]>;
					if (madeObjectPool == null)
					{
						madeObjectPool = (pools[poolIdx] = new MadeObjectPool<T[]>(() => null));
					}
					return madeObjectPool;
				}

				// Token: 0x040003FA RID: 1018
				private readonly DataViewUtils.Splitter _splitter;
			}

			// Token: 0x0200017E RID: 382
			private abstract class InPipe
			{
				// Token: 0x170000A8 RID: 168
				// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00029B54 File Offset: 0x00027D54
				public int Count
				{
					get
					{
						return this._count;
					}
				}

				// Token: 0x060007B3 RID: 1971 RVA: 0x00029B5C File Offset: 0x00027D5C
				private InPipe()
				{
				}

				// Token: 0x060007B4 RID: 1972
				public abstract void Fill();

				// Token: 0x060007B5 RID: 1973
				public abstract DataViewUtils.Splitter.BatchColumn GetBatchColumnAndReset();

				// Token: 0x060007B6 RID: 1974 RVA: 0x00029B64 File Offset: 0x00027D64
				public static DataViewUtils.Splitter.InPipe Create<T>(MadeObjectPool<T[]> pool, ValueGetter<T> getter)
				{
					return new DataViewUtils.Splitter.InPipe.Impl<T>(pool, getter);
				}

				// Token: 0x060007B7 RID: 1975
				public abstract DataViewUtils.Splitter.OutPipe CreateOutPipe(ColumnType type);

				// Token: 0x040003FB RID: 1019
				protected int _count;

				// Token: 0x0200017F RID: 383
				private sealed class Impl<T> : DataViewUtils.Splitter.InPipe
				{
					// Token: 0x060007B8 RID: 1976 RVA: 0x00029B6D File Offset: 0x00027D6D
					public Impl(MadeObjectPool<T[]> pool, ValueGetter<T> getter)
					{
						this._pool = pool;
						this._getter = getter;
						this._values = this._pool.Get();
					}

					// Token: 0x060007B9 RID: 1977 RVA: 0x00029B94 File Offset: 0x00027D94
					public override void Fill()
					{
						Utils.EnsureSize<T>(ref this._values, this._count + 1, true);
						this._getter.Invoke(ref this._values[this._count++]);
					}

					// Token: 0x060007BA RID: 1978 RVA: 0x00029BE0 File Offset: 0x00027DE0
					public override DataViewUtils.Splitter.BatchColumn GetBatchColumnAndReset()
					{
						DataViewUtils.Splitter.BatchColumn.Impl<T> impl = new DataViewUtils.Splitter.BatchColumn.Impl<T>(this._values, this._count);
						this._values = this._pool.Get();
						this._count = 0;
						return impl;
					}

					// Token: 0x060007BB RID: 1979 RVA: 0x00029C18 File Offset: 0x00027E18
					public override DataViewUtils.Splitter.OutPipe CreateOutPipe(ColumnType type)
					{
						return DataViewUtils.Splitter.OutPipe.Create(type, this._pool);
					}

					// Token: 0x040003FC RID: 1020
					private readonly MadeObjectPool<T[]> _pool;

					// Token: 0x040003FD RID: 1021
					private readonly ValueGetter<T> _getter;

					// Token: 0x040003FE RID: 1022
					private T[] _values;
				}
			}

			// Token: 0x02000180 RID: 384
			private abstract class BatchColumn
			{
				// Token: 0x060007BC RID: 1980 RVA: 0x00029C26 File Offset: 0x00027E26
				private BatchColumn(int count)
				{
					this.Count = count;
				}

				// Token: 0x040003FF RID: 1023
				public readonly int Count;

				// Token: 0x02000181 RID: 385
				public sealed class Impl<T> : DataViewUtils.Splitter.BatchColumn
				{
					// Token: 0x060007BD RID: 1981 RVA: 0x00029C35 File Offset: 0x00027E35
					public Impl(T[] values, int count)
						: base(count)
					{
						this.Values = values;
					}

					// Token: 0x04000400 RID: 1024
					public readonly T[] Values;
				}
			}

			// Token: 0x02000182 RID: 386
			private sealed class Batch
			{
				// Token: 0x170000A9 RID: 169
				// (get) Token: 0x060007BE RID: 1982 RVA: 0x00029C45 File Offset: 0x00027E45
				public bool HasException
				{
					get
					{
						return this._ex != null;
					}
				}

				// Token: 0x060007BF RID: 1983 RVA: 0x00029C53 File Offset: 0x00027E53
				public Batch(MadeObjectPool<DataViewUtils.Splitter.BatchColumn[]> pool, DataViewUtils.Splitter.BatchColumn[] batchColumns, int count, long batchId)
				{
					this._pool = pool;
					this._batchColumns = batchColumns;
					this.Count = count;
					this.BatchId = batchId;
				}

				// Token: 0x060007C0 RID: 1984 RVA: 0x00029C78 File Offset: 0x00027E78
				public Batch(Exception ex)
				{
					this._ex = ex;
				}

				// Token: 0x060007C1 RID: 1985 RVA: 0x00029C88 File Offset: 0x00027E88
				public void SetAll(DataViewUtils.Splitter.OutPipe[] pipes)
				{
					if (this._ex != null)
					{
						throw Contracts.Except(this._ex, "Splitter/consolidator worker encountered exception while consuming source data");
					}
					for (int i = 0; i < this._batchColumns.Length; i++)
					{
						pipes[i].Set(this._batchColumns[i]);
						this._batchColumns[i] = null;
					}
					this._pool.Return(this._batchColumns);
				}

				// Token: 0x04000401 RID: 1025
				private readonly MadeObjectPool<DataViewUtils.Splitter.BatchColumn[]> _pool;

				// Token: 0x04000402 RID: 1026
				private readonly DataViewUtils.Splitter.BatchColumn[] _batchColumns;

				// Token: 0x04000403 RID: 1027
				public readonly int Count;

				// Token: 0x04000404 RID: 1028
				public readonly long BatchId;

				// Token: 0x04000405 RID: 1029
				private readonly Exception _ex;
			}

			// Token: 0x02000183 RID: 387
			private abstract class OutPipe
			{
				// Token: 0x170000AA RID: 170
				// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00029CEB File Offset: 0x00027EEB
				public int Remaining
				{
					get
					{
						return this._count - this._index;
					}
				}

				// Token: 0x060007C3 RID: 1987 RVA: 0x00029CFA File Offset: 0x00027EFA
				private OutPipe()
				{
				}

				// Token: 0x060007C4 RID: 1988 RVA: 0x00029D04 File Offset: 0x00027F04
				public static DataViewUtils.Splitter.OutPipe Create(ColumnType type, object pool)
				{
					Type type2;
					if (type.IsVector)
					{
						type2 = typeof(DataViewUtils.Splitter.OutPipe.ImplVec<>).MakeGenericType(new Type[] { type.ItemType.RawType });
					}
					else
					{
						type2 = typeof(DataViewUtils.Splitter.OutPipe.ImplOne<>).MakeGenericType(new Type[] { type.RawType });
					}
					ConstructorInfo constructor = type2.GetConstructor(new Type[] { typeof(object) });
					return (DataViewUtils.Splitter.OutPipe)constructor.Invoke(new object[] { pool });
				}

				// Token: 0x060007C5 RID: 1989
				public abstract DataViewUtils.Splitter.InPipe CreateInPipe(Delegate getter);

				// Token: 0x060007C6 RID: 1990
				public abstract void Set(DataViewUtils.Splitter.BatchColumn batchCol);

				// Token: 0x060007C7 RID: 1991
				public abstract void Unset();

				// Token: 0x060007C8 RID: 1992
				public abstract Delegate GetGetter();

				// Token: 0x060007C9 RID: 1993 RVA: 0x00029D9B File Offset: 0x00027F9B
				public void MoveNext()
				{
					this._index++;
				}

				// Token: 0x04000406 RID: 1030
				protected int _count;

				// Token: 0x04000407 RID: 1031
				protected int _index;

				// Token: 0x02000184 RID: 388
				private abstract class Impl<T> : DataViewUtils.Splitter.OutPipe
				{
					// Token: 0x060007CA RID: 1994 RVA: 0x00029DAB File Offset: 0x00027FAB
					public Impl(object pool)
					{
						this._pool = (MadeObjectPool<T[]>)pool;
					}

					// Token: 0x060007CB RID: 1995 RVA: 0x00029DBF File Offset: 0x00027FBF
					public override DataViewUtils.Splitter.InPipe CreateInPipe(Delegate getter)
					{
						return DataViewUtils.Splitter.InPipe.Create<T>(this._pool, (ValueGetter<T>)getter);
					}

					// Token: 0x060007CC RID: 1996 RVA: 0x00029DD4 File Offset: 0x00027FD4
					public override void Set(DataViewUtils.Splitter.BatchColumn batchCol)
					{
						DataViewUtils.Splitter.BatchColumn.Impl<T> impl = (DataViewUtils.Splitter.BatchColumn.Impl<T>)batchCol;
						if (this._values != null)
						{
							this._pool.Return(this._values);
						}
						this._values = impl.Values;
						this._count = impl.Count;
						this._index = 0;
					}

					// Token: 0x060007CD RID: 1997 RVA: 0x00029E20 File Offset: 0x00028020
					public override void Unset()
					{
						if (this._values != null)
						{
							this._pool.Return(this._values);
						}
						this._values = null;
						this._count = 0;
						this._index = 0;
					}

					// Token: 0x060007CE RID: 1998 RVA: 0x00029E50 File Offset: 0x00028050
					public override Delegate GetGetter()
					{
						return new ValueGetter<T>(this.Getter);
					}

					// Token: 0x060007CF RID: 1999
					protected abstract void Getter(ref T value);

					// Token: 0x04000408 RID: 1032
					private readonly MadeObjectPool<T[]> _pool;

					// Token: 0x04000409 RID: 1033
					protected T[] _values;
				}

				// Token: 0x02000185 RID: 389
				private sealed class ImplVec<T> : DataViewUtils.Splitter.OutPipe.Impl<VBuffer<T>>
				{
					// Token: 0x060007D0 RID: 2000 RVA: 0x00029E6C File Offset: 0x0002806C
					public ImplVec(object pool)
						: base(pool)
					{
					}

					// Token: 0x060007D1 RID: 2001 RVA: 0x00029E75 File Offset: 0x00028075
					protected override void Getter(ref VBuffer<T> value)
					{
						Contracts.Check(this._index < this._count, "Cannot get value as the cursor is not in a good state");
						this._values[this._index].CopyTo(ref value);
					}
				}

				// Token: 0x02000186 RID: 390
				private sealed class ImplOne<T> : DataViewUtils.Splitter.OutPipe.Impl<T>
				{
					// Token: 0x060007D2 RID: 2002 RVA: 0x00029EA6 File Offset: 0x000280A6
					public ImplOne(object pool)
						: base(pool)
					{
					}

					// Token: 0x060007D3 RID: 2003 RVA: 0x00029EAF File Offset: 0x000280AF
					protected override void Getter(ref T value)
					{
						Contracts.Check(this._index < this._count, "Cannot get value as the cursor is not in a good state");
						value = this._values[this._index];
					}
				}
			}

			// Token: 0x02000187 RID: 391
			private sealed class Cursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
			{
				// Token: 0x170000AB RID: 171
				// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00029EE0 File Offset: 0x000280E0
				public ISchema Schema
				{
					get
					{
						return this._schema;
					}
				}

				// Token: 0x170000AC RID: 172
				// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00029EE8 File Offset: 0x000280E8
				public override long Batch
				{
					get
					{
						return this._batch;
					}
				}

				// Token: 0x060007D6 RID: 2006 RVA: 0x00029EF0 File Offset: 0x000280F0
				public Cursor(IChannelProvider provider, ISchema schema, int[] activeToCol, int[] colToActive, DataViewUtils.Splitter.OutPipe[] pipes, BlockingCollection<DataViewUtils.Splitter.Batch> batchInputs, Action quitAction)
					: base(provider)
				{
					this._schema = schema;
					this._activeToCol = activeToCol;
					this._colToActive = colToActive;
					this._pipes = pipes;
					this._getters = new Delegate[pipes.Length];
					for (int i = 0; i < activeToCol.Length; i++)
					{
						this._getters[i] = this._pipes[i].GetGetter();
					}
					this._idGetter = (ValueGetter<UInt128>)this._pipes[activeToCol.Length].GetGetter();
					this._batchInputs = batchInputs;
					this._batch = -1L;
					this._quitAction = quitAction;
				}

				// Token: 0x060007D7 RID: 2007 RVA: 0x00029F88 File Offset: 0x00028188
				public override void Dispose()
				{
					if (!this._disposed)
					{
						foreach (DataViewUtils.Splitter.OutPipe outPipe in this._pipes)
						{
							outPipe.Unset();
						}
						this._disposed = true;
						if (this._quitAction != null)
						{
							this._quitAction();
						}
					}
					base.Dispose();
				}

				// Token: 0x060007D8 RID: 2008 RVA: 0x00029FDC File Offset: 0x000281DC
				public override ValueGetter<UInt128> GetIdGetter()
				{
					return this._idGetter;
				}

				// Token: 0x060007D9 RID: 2009 RVA: 0x00029FE4 File Offset: 0x000281E4
				protected override bool MoveNextCore()
				{
					if (--this._remaining > 0)
					{
						foreach (DataViewUtils.Splitter.OutPipe outPipe in this._pipes)
						{
							outPipe.MoveNext();
						}
					}
					else
					{
						DataViewUtils.Splitter.Batch batch = this._batchInputs.Take();
						if (batch == null)
						{
							return false;
						}
						this._batch = batch.BatchId;
						this._remaining = batch.Count;
						batch.SetAll(this._pipes);
					}
					return true;
				}

				// Token: 0x060007DA RID: 2010 RVA: 0x0002A061 File Offset: 0x00028261
				public bool IsColumnActive(int col)
				{
					Contracts.CheckParam(this._ch, 0 <= col && col < this._colToActive.Length, "col");
					return this._colToActive[col] >= 0;
				}

				// Token: 0x060007DB RID: 2011 RVA: 0x0002A094 File Offset: 0x00028294
				public ValueGetter<TValue> GetGetter<TValue>(int col)
				{
					Contracts.CheckParam(this._ch, this.IsColumnActive(col), "col", "requested column not active");
					ValueGetter<TValue> valueGetter = this._getters[this._colToActive[col]] as ValueGetter<TValue>;
					if (valueGetter == null)
					{
						throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
					}
					return valueGetter;
				}

				// Token: 0x0400040A RID: 1034
				private readonly ISchema _schema;

				// Token: 0x0400040B RID: 1035
				private readonly int[] _activeToCol;

				// Token: 0x0400040C RID: 1036
				private readonly int[] _colToActive;

				// Token: 0x0400040D RID: 1037
				private readonly DataViewUtils.Splitter.OutPipe[] _pipes;

				// Token: 0x0400040E RID: 1038
				private readonly Delegate[] _getters;

				// Token: 0x0400040F RID: 1039
				private readonly ValueGetter<UInt128> _idGetter;

				// Token: 0x04000410 RID: 1040
				private readonly BlockingCollection<DataViewUtils.Splitter.Batch> _batchInputs;

				// Token: 0x04000411 RID: 1041
				private readonly Action _quitAction;

				// Token: 0x04000412 RID: 1042
				private int _remaining;

				// Token: 0x04000413 RID: 1043
				private long _batch;

				// Token: 0x04000414 RID: 1044
				private bool _disposed;
			}
		}

		// Token: 0x02000188 RID: 392
		internal sealed class SynchronousConsolidatingCursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060007DC RID: 2012 RVA: 0x0002A0FC File Offset: 0x000282FC
			public override long Batch
			{
				get
				{
					return this._batch;
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060007DD RID: 2013 RVA: 0x0002A104 File Offset: 0x00028304
			public ISchema Schema
			{
				get
				{
					return this._schema;
				}
			}

			// Token: 0x060007DE RID: 2014 RVA: 0x0002A120 File Offset: 0x00028320
			public SynchronousConsolidatingCursor(IChannelProvider provider, IRowCursor[] cursors)
				: base(provider)
			{
				Contracts.CheckNonEmpty<IRowCursor>(this._ch, cursors, "cursors");
				this._cursors = cursors;
				this._schema = this._cursors[0].Schema;
				Utils.BuildSubsetMaps(this._schema.ColumnCount, new Func<int, bool>(this._cursors[0].IsColumnActive), ref this._activeToCol, ref this._colToActive);
				Func<int, Delegate> func = new Func<int, Delegate>(this.CreateGetter<int>);
				this._methInfo = func.GetMethodInfo().GetGenericMethodDefinition();
				this._getters = new Delegate[this._activeToCol.Length];
				for (int i = 0; i < this._activeToCol.Length; i++)
				{
					this._getters[i] = this.CreateGetter(this._activeToCol[i]);
				}
				this._icursor = -1;
				this._batch = -1L;
				this._mins = new Heap<DataViewUtils.SynchronousConsolidatingCursor.CursorStats>((DataViewUtils.SynchronousConsolidatingCursor.CursorStats s1, DataViewUtils.SynchronousConsolidatingCursor.CursorStats s2) => s1.Batch > s2.Batch);
				this.InitHeap();
			}

			// Token: 0x060007DF RID: 2015 RVA: 0x0002A228 File Offset: 0x00028428
			private void InitHeap()
			{
				for (int i = 0; i < this._cursors.Length; i++)
				{
					IRowCursor rowCursor = this._cursors[i];
					if (rowCursor.MoveNext())
					{
						this._mins.Add(new DataViewUtils.SynchronousConsolidatingCursor.CursorStats(rowCursor.Batch, i));
					}
				}
			}

			// Token: 0x060007E0 RID: 2016 RVA: 0x0002A270 File Offset: 0x00028470
			public override void Dispose()
			{
				if (!this._disposed)
				{
					this._disposed = true;
					this._batch = -1L;
					this._icursor = -1;
					this._currentCursor = null;
					foreach (IRowCursor rowCursor in this._cursors)
					{
						rowCursor.Dispose();
					}
				}
				base.Dispose();
			}

			// Token: 0x060007E1 RID: 2017 RVA: 0x0002A310 File Offset: 0x00028510
			public override ValueGetter<UInt128> GetIdGetter()
			{
				ValueGetter<UInt128>[] idGetters = new ValueGetter<UInt128>[this._cursors.Length];
				for (int i = 0; i < this._cursors.Length; i++)
				{
					idGetters[i] = this._cursors[i].GetIdGetter();
				}
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._ch, this._icursor >= 0, "Cannot call ID getter in current state");
					idGetters[this._icursor].Invoke(ref val);
				};
			}

			// Token: 0x060007E2 RID: 2018 RVA: 0x0002A374 File Offset: 0x00028574
			private Delegate CreateGetter(int col)
			{
				MethodInfo methodInfo = this._methInfo.MakeGenericMethod(new Type[] { this.Schema.GetColumnType(col).RawType });
				return (Delegate)methodInfo.Invoke(this, new object[] { col });
			}

			// Token: 0x060007E3 RID: 2019 RVA: 0x0002A410 File Offset: 0x00028610
			private Delegate CreateGetter<T>(int col)
			{
				ValueGetter<T>[] getters = new ValueGetter<T>[this._cursors.Length];
				this.Schema.GetColumnType(col);
				for (int i = 0; i < this._cursors.Length; i++)
				{
					IRowCursor rowCursor = this._cursors[i];
					getters[i] = this._cursors[i].GetGetter<T>(col);
				}
				return new ValueGetter<T>(delegate(ref T value)
				{
					Contracts.Check(this._ch, this._icursor >= 0, "Cannot get value as the cursor is not in a good state");
					getters[this._icursor].Invoke(ref value);
				});
			}

			// Token: 0x060007E4 RID: 2020 RVA: 0x0002A48C File Offset: 0x0002868C
			protected override bool MoveNextCore()
			{
				if (base.State == 1 && this._currentCursor.MoveNext())
				{
					if (this._currentCursor.Batch == this._batch)
					{
						return true;
					}
					this._mins.Add(new DataViewUtils.SynchronousConsolidatingCursor.CursorStats(this._currentCursor.Batch, this._icursor));
				}
				if (this._mins.Count == 0)
				{
					return false;
				}
				DataViewUtils.SynchronousConsolidatingCursor.CursorStats cursorStats = this._mins.Pop();
				this._icursor = cursorStats.CursorIdx;
				this._currentCursor = this._cursors[cursorStats.CursorIdx];
				this._batch = this._currentCursor.Batch;
				return true;
			}

			// Token: 0x060007E5 RID: 2021 RVA: 0x0002A533 File Offset: 0x00028733
			public bool IsColumnActive(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col < this._colToActive.Length, "col");
				return this._colToActive[col] >= 0;
			}

			// Token: 0x060007E6 RID: 2022 RVA: 0x0002A568 File Offset: 0x00028768
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.CheckParam(this._ch, this.IsColumnActive(col), "col", "requested column not active");
				ValueGetter<TValue> valueGetter = this._getters[this._colToActive[col]] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x04000415 RID: 1045
			private readonly IRowCursor[] _cursors;

			// Token: 0x04000416 RID: 1046
			private readonly Delegate[] _getters;

			// Token: 0x04000417 RID: 1047
			private readonly ISchema _schema;

			// Token: 0x04000418 RID: 1048
			private readonly Heap<DataViewUtils.SynchronousConsolidatingCursor.CursorStats> _mins;

			// Token: 0x04000419 RID: 1049
			private readonly int[] _activeToCol;

			// Token: 0x0400041A RID: 1050
			private readonly int[] _colToActive;

			// Token: 0x0400041B RID: 1051
			private readonly MethodInfo _methInfo;

			// Token: 0x0400041C RID: 1052
			private long _batch;

			// Token: 0x0400041D RID: 1053
			private int _icursor;

			// Token: 0x0400041E RID: 1054
			private IRowCursor _currentCursor;

			// Token: 0x0400041F RID: 1055
			private bool _disposed;

			// Token: 0x02000189 RID: 393
			private struct CursorStats
			{
				// Token: 0x060007E8 RID: 2024 RVA: 0x0002A5D0 File Offset: 0x000287D0
				public CursorStats(long batch, int idx)
				{
					this.Batch = batch;
					this.CursorIdx = idx;
				}

				// Token: 0x04000421 RID: 1057
				public readonly long Batch;

				// Token: 0x04000422 RID: 1058
				public readonly int CursorIdx;
			}
		}
	}
}
