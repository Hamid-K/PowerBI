using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000004 RID: 4
	public abstract class EvaluatorBase : IEvaluator
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		protected EvaluatorBase(IHostEnvironment env, string registrationName)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register(registrationName);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F0 File Offset: 0x000002F0
		public Dictionary<string, IDataView> Evaluate(RoleMappedData data)
		{
			this.CheckColumnTypes(data.Schema);
			Func<int, bool> activeCols = this.GetActiveCols(data.Schema);
			EvaluatorBase.AggregatorBase aggregator = this.GetAggregator(data.Schema);
			EvaluatorBase.AggregatorDictionaryBase[] aggregatorDictionaries = this.GetAggregatorDictionaries(data.Schema);
			Dictionary<string, IDataView> dictionary = this.ProcessData(data.Data, data.Schema, activeCols, aggregator, aggregatorDictionaries);
			aggregator.GetWarnings(dictionary, this._host);
			return dictionary;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002154 File Offset: 0x00000354
		protected void CheckColumnTypes(RoleMappedSchema schema)
		{
			if (schema.Weight != null)
			{
				EvaluateUtils.CheckWeightType(this._host, schema.Weight.Type);
			}
			this.CheckScoreAndLabelTypes(schema);
			this.CheckCustomColumnTypesCore(schema);
		}

		// Token: 0x06000007 RID: 7
		protected abstract void CheckScoreAndLabelTypes(RoleMappedSchema schema);

		// Token: 0x06000008 RID: 8 RVA: 0x00002182 File Offset: 0x00000382
		protected virtual void CheckCustomColumnTypesCore(RoleMappedSchema schema)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B4 File Offset: 0x000003B4
		private Func<int, bool> GetActiveCols(RoleMappedSchema schema)
		{
			EvaluatorBase.<>c__DisplayClass3 CS$<>8__locals1 = new EvaluatorBase.<>c__DisplayClass3();
			CS$<>8__locals1.pred = this.GetActiveColsCore(schema);
			IReadOnlyList<ColumnInfo> columns = schema.GetColumns(MamlEvaluatorBase.Strat);
			EvaluatorBase.<>c__DisplayClass3 CS$<>8__locals2 = CS$<>8__locals1;
			HashSet<int> hashSet;
			if (Utils.Size<ColumnInfo>(columns) <= 0)
			{
				hashSet = new HashSet<int>();
			}
			else
			{
				hashSet = new HashSet<int>(columns.Select((ColumnInfo col) => col.Index));
			}
			CS$<>8__locals2.stratIndices = hashSet;
			return (int i) => CS$<>8__locals1.pred(i) || CS$<>8__locals1.stratIndices.Contains(i);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002258 File Offset: 0x00000458
		protected virtual Func<int, bool> GetActiveColsCore(RoleMappedSchema schema)
		{
			ColumnInfo score = schema.GetUniqueColumn("Score");
			int label = ((schema.Label == null) ? (-1) : schema.Label.Index);
			int weight = ((schema.Weight == null) ? (-1) : schema.Weight.Index);
			return (int i) => i == score.Index || i == label || i == weight;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022C5 File Offset: 0x000004C5
		private EvaluatorBase.AggregatorBase GetAggregator(RoleMappedSchema schema)
		{
			return this.GetAggregatorCore(schema, "");
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022F0 File Offset: 0x000004F0
		private EvaluatorBase.AggregatorDictionaryBase[] GetAggregatorDictionaries(RoleMappedSchema schema)
		{
			List<EvaluatorBase.AggregatorDictionaryBase> list = new List<EvaluatorBase.AggregatorDictionaryBase>();
			IReadOnlyList<ColumnInfo> columns = schema.GetColumns(MamlEvaluatorBase.Strat);
			if (Utils.Size<ColumnInfo>(columns) > 0)
			{
				Func<string, EvaluatorBase.AggregatorBase> func = (string stratName) => this.GetAggregatorCore(schema, stratName);
				foreach (ColumnInfo columnInfo in columns)
				{
					list.Add(EvaluatorBase.AggregatorDictionaryBase.Create(schema, columnInfo.Name, columnInfo.Type, func));
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600000D RID: 13
		protected abstract EvaluatorBase.AggregatorBase GetAggregatorCore(RoleMappedSchema schema, string stratName);

		// Token: 0x0600000E RID: 14 RVA: 0x000025F0 File Offset: 0x000007F0
		private Dictionary<string, IDataView> ProcessData(IDataView data, RoleMappedSchema schema, Func<int, bool> activeCols, EvaluatorBase.AggregatorBase aggregator, EvaluatorBase.AggregatorDictionaryBase[] dictionaries)
		{
			Func<bool> func = delegate
			{
				bool flag2 = aggregator.FinishPass();
				foreach (EvaluatorBase.AggregatorBase aggregatorBase3 in dictionaries.SelectMany((EvaluatorBase.AggregatorDictionaryBase dict) => dict.GetAll()))
				{
					flag2 |= aggregatorBase3.FinishPass();
				}
				return flag2;
			};
			bool flag = aggregator.Start();
			while (flag)
			{
				using (IRowCursor rowCursor = data.GetRowCursor(activeCols, null))
				{
					if (aggregator.IsActive())
					{
						aggregator.InitializeNextPass(rowCursor, schema);
					}
					for (int i = 0; i < Utils.Size<EvaluatorBase.AggregatorDictionaryBase>(dictionaries); i++)
					{
						dictionaries[i].Reset(rowCursor);
						foreach (EvaluatorBase.AggregatorBase aggregatorBase in dictionaries[i].GetAll())
						{
							if (aggregatorBase.IsActive())
							{
								aggregatorBase.InitializeNextPass(rowCursor, schema);
							}
						}
					}
					while (rowCursor.MoveNext())
					{
						if (aggregator.IsActive())
						{
							aggregator.ProcessRow();
						}
						for (int j = 0; j < Utils.Size<EvaluatorBase.AggregatorDictionaryBase>(dictionaries); j++)
						{
							EvaluatorBase.AggregatorBase aggregatorBase2 = dictionaries[j].Get();
							if (aggregatorBase2.IsActive())
							{
								aggregatorBase2.ProcessRow();
							}
						}
					}
				}
				flag = func();
			}
			Dictionary<string, IDataView> dictionary = aggregator.Finish();
			if (Utils.Size<EvaluatorBase.AggregatorDictionaryBase>(dictionaries) == 0)
			{
				return dictionary;
			}
			int stratColKeyCount = dictionaries.Length;
			ValueGetter<VBuffer<DvText>> keyValueGetter = delegate(ref VBuffer<DvText> dst)
			{
				DvText[] array = dst.Values;
				if (Utils.Size<DvText>(array) < stratColKeyCount)
				{
					array = new DvText[stratColKeyCount];
				}
				for (int l = 0; l < stratColKeyCount; l++)
				{
					array[l] = new DvText(dictionaries[l].ColName);
				}
				dst = new VBuffer<DvText>(stratColKeyCount, array, null);
			};
			Dictionary<string, List<IDataView>> dictionary2 = dictionary.ToDictionary((KeyValuePair<string, IDataView> kvp) => kvp.Key, delegate(KeyValuePair<string, IDataView> kvp)
			{
				string columnName2 = kvp.Value.Schema.GetColumnName(0);
				ColumnType columnType2 = kvp.Value.Schema.GetColumnType(0);
				IDataView dataView2 = Utils.MarshalInvoke<IHost, IDataView, string, string, ColumnType, int, int, string, ValueGetter<VBuffer<DvText>>, IDataView>(new Func<IHost, IDataView, string, string, ColumnType, int, int, string, ValueGetter<VBuffer<DvText>>, IDataView>(EvaluateUtils.AddKeyColumn<int>), columnType2.RawType, this._host, kvp.Value, columnName2, "StratCol", columnType2, stratColKeyCount, 0, "StratColName", keyValueGetter);
				dataView2 = LambdaColumnMapper.Create<uint, DvText>(this._host, "StratValue", dataView2, "StratCol", "StratVal", new KeyType(6, 0UL, stratColKeyCount, true), TextType.Instance, delegate(ref uint src, ref DvText dst)
				{
					dst = DvText.NA;
				}, null);
				return new List<IDataView> { dataView2 };
			});
			int num = 0;
			foreach (EvaluatorBase.AggregatorDictionaryBase aggregatorDictionaryBase in dictionaries)
			{
				num++;
				using (IEnumerator<EvaluatorBase.AggregatorBase> enumerator2 = aggregatorDictionaryBase.GetAll().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						EvaluatorBase.AggregatorBase agg = enumerator2.Current;
						Dictionary<string, IDataView> dictionary3 = agg.Finish();
						foreach (KeyValuePair<string, IDataView> keyValuePair in dictionary3)
						{
							List<IDataView> list;
							if (!dictionary2.TryGetValue(keyValuePair.Key, out list))
							{
								dictionary2.Add(keyValuePair.Key, list = new List<IDataView>());
							}
							string columnName = keyValuePair.Value.Schema.GetColumnName(0);
							ColumnType columnType = keyValuePair.Value.Schema.GetColumnType(0);
							IDataView dataView = Utils.MarshalInvoke<IHost, IDataView, string, string, ColumnType, int, int, string, ValueGetter<VBuffer<DvText>>, IDataView>(new Func<IHost, IDataView, string, string, ColumnType, int, int, string, ValueGetter<VBuffer<DvText>>, IDataView>(EvaluateUtils.AddKeyColumn<int>), columnType.RawType, this._host, keyValuePair.Value, columnName, "StratCol", columnType, stratColKeyCount, num, "StratColName", keyValueGetter);
							dataView = LambdaColumnMapper.Create<uint, DvText>(this._host, "StratValue", dataView, "StratCol", "StratVal", new KeyType(6, 0UL, stratColKeyCount, true), TextType.Instance, delegate(ref uint src, ref DvText dst)
							{
								dst = new DvText(agg.StratName);
							}, null);
							list.Add(dataView);
						}
					}
				}
			}
			return dictionary2.ToDictionary((KeyValuePair<string, List<IDataView>> kvp) => kvp.Key, (KeyValuePair<string, List<IDataView>> kvp) => AppendRowsDataView.Create(this._host, kvp.Value[0].Schema, kvp.Value.ToArray()));
		}

		// Token: 0x0600000F RID: 15
		public abstract IDataTransform GetPerInstanceMetrics(RoleMappedData data);

		// Token: 0x06000010 RID: 16
		public abstract IEnumerable<MetricColumn> GetOverallMetricColumns();

		// Token: 0x04000002 RID: 2
		protected readonly IHost _host;

		// Token: 0x02000005 RID: 5
		protected abstract class AucAggregatorBase
		{
			// Token: 0x06000015 RID: 21 RVA: 0x00002A00 File Offset: 0x00000C00
			public void ProcessRow(float label, float score, float weight = 1f)
			{
				this._label = label;
				this._score = score;
				this.ProcessRowCore(weight);
			}

			// Token: 0x06000016 RID: 22
			protected abstract void ProcessRowCore(float weight);

			// Token: 0x06000017 RID: 23
			public abstract void Finish();

			// Token: 0x06000018 RID: 24
			public abstract double ComputeWeightedAuc(out double unweighted);

			// Token: 0x04000006 RID: 6
			protected float _score;

			// Token: 0x04000007 RID: 7
			protected float _label;
		}

		// Token: 0x02000006 RID: 6
		protected abstract class AucAggregatorBase<T> : EvaluatorBase.AucAggregatorBase
		{
			// Token: 0x0600001A RID: 26 RVA: 0x00002A20 File Offset: 0x00000C20
			protected AucAggregatorBase(IRandom rand, int reservoirSize)
			{
				ValueGetter<T> sampleGetter = this.GetSampleGetter();
				if (reservoirSize > 0)
				{
					this._posReservoir = new ReservoirSamplerWithoutReplacement<T>(rand, reservoirSize, sampleGetter);
					this._negReservoir = new ReservoirSamplerWithoutReplacement<T>(rand, reservoirSize, sampleGetter);
					return;
				}
				if (reservoirSize == -1)
				{
					this._posExamples = new List<T>();
					this._negExamples = new List<T>();
				}
			}

			// Token: 0x0600001B RID: 27
			protected abstract ValueGetter<T> GetSampleGetter();

			// Token: 0x0600001C RID: 28 RVA: 0x00002A78 File Offset: 0x00000C78
			protected override void ProcessRowCore(float weight)
			{
				if (this._posReservoir == null && this._posExamples == null)
				{
					return;
				}
				if (this._posReservoir != null)
				{
					if (this._label > 0f)
					{
						this._posReservoir.Sample();
						return;
					}
					this._negReservoir.Sample();
					return;
				}
				else
				{
					if (this._label > 0f)
					{
						this.AddExample(this._posExamples);
						return;
					}
					this.AddExample(this._negExamples);
					return;
				}
			}

			// Token: 0x0600001D RID: 29
			protected abstract void AddExample(List<T> examples);

			// Token: 0x0600001E RID: 30 RVA: 0x00002AEC File Offset: 0x00000CEC
			public override void Finish()
			{
				if (this._posReservoir == null && this._posExamples == null)
				{
					return;
				}
				if (this._posReservoir != null)
				{
					this._posReservoir.Lock();
					this._posSample = this._posReservoir.GetSample();
					this._negReservoir.Lock();
					this._negSample = this._negReservoir.GetSample();
					return;
				}
				this._posSample = this._posExamples;
				this._negSample = this._negExamples;
			}

			// Token: 0x0600001F RID: 31 RVA: 0x00002B64 File Offset: 0x00000D64
			public override double ComputeWeightedAuc(out double unweighted)
			{
				if (this._posReservoir == null && this._posExamples == null)
				{
					unweighted = 0.0;
					return 0.0;
				}
				Contracts.Check(this._posSample != null && this._negSample != null, "Must call Finish() before computing AUC");
				return this.ComputWeightedAucCore(out unweighted);
			}

			// Token: 0x06000020 RID: 32
			protected abstract double ComputWeightedAucCore(out double unweighted);

			// Token: 0x04000008 RID: 8
			private readonly ReservoirSamplerWithoutReplacement<T> _posReservoir;

			// Token: 0x04000009 RID: 9
			private readonly ReservoirSamplerWithoutReplacement<T> _negReservoir;

			// Token: 0x0400000A RID: 10
			private readonly List<T> _posExamples;

			// Token: 0x0400000B RID: 11
			private readonly List<T> _negExamples;

			// Token: 0x0400000C RID: 12
			protected IEnumerable<T> _posSample;

			// Token: 0x0400000D RID: 13
			protected IEnumerable<T> _negSample;
		}

		// Token: 0x02000007 RID: 7
		protected sealed class UnweightedAucAggregator : EvaluatorBase.AucAggregatorBase<float>
		{
			// Token: 0x06000021 RID: 33 RVA: 0x00002BBE File Offset: 0x00000DBE
			public UnweightedAucAggregator(IRandom rand, int reservoirSize)
				: base(rand, reservoirSize)
			{
			}

			// Token: 0x06000022 RID: 34 RVA: 0x00002BD0 File Offset: 0x00000DD0
			protected override double ComputWeightedAucCore(out double unweighted)
			{
				double num9;
				using (IEnumerator<float> enumerator = this._posSample.OrderByDescending((float x) => x).GetEnumerator())
				{
					using (IEnumerator<float> enumerator2 = this._negSample.OrderByDescending((float x) => x).GetEnumerator())
					{
						double num = 0.0;
						double num2 = 0.0;
						double num3 = 0.0;
						bool flag = enumerator.MoveNext();
						bool flag2 = enumerator2.MoveNext();
						double num4 = 0.0;
						double num5 = 0.0;
						IL_0194:
						while (flag)
						{
							if (!flag2)
							{
								break;
							}
							num5 = (double)enumerator.Current;
							float num6 = enumerator2.Current;
							if (num5 > (double)num6)
							{
								num += 1.0;
								flag = enumerator.MoveNext();
							}
							else
							{
								if (num5 >= (double)num6)
								{
									num4 = 0.0;
									double num7 = 0.0;
									double num8 = num5;
									while (num8 == num5)
									{
										num4 += 1.0;
										flag = enumerator.MoveNext();
										if (!flag)
										{
											IL_0166:
											while (num8 == (double)num6)
											{
												num7 += 1.0;
												flag2 = enumerator2.MoveNext();
												if (!flag2)
												{
													break;
												}
												num6 = enumerator2.Current;
											}
											num3 += num * num7;
											num3 += 0.5 * num4 * num7;
											num += num4;
											num2 += num7;
											goto IL_0194;
										}
										num5 = (double)enumerator.Current;
									}
									goto IL_0166;
								}
								num3 += num;
								num2 += 1.0;
								flag2 = enumerator2.MoveNext();
							}
						}
						while (flag)
						{
							num += 1.0;
							flag = enumerator.MoveNext();
						}
						while (flag2)
						{
							num3 += num;
							if (num5 == (double)enumerator2.Current)
							{
								num3 -= 0.5 * num4;
							}
							num2 += 1.0;
							flag2 = enumerator2.MoveNext();
						}
						num9 = (unweighted = num3 / (num * num2));
					}
				}
				return num9;
			}

			// Token: 0x06000023 RID: 35 RVA: 0x00002E3A File Offset: 0x0000103A
			protected override ValueGetter<float> GetSampleGetter()
			{
				return delegate(ref float dst)
				{
					dst = this._score;
				};
			}

			// Token: 0x06000024 RID: 36 RVA: 0x00002E48 File Offset: 0x00001048
			protected override void AddExample(List<float> examples)
			{
				examples.Add(this._score);
			}
		}

		// Token: 0x02000008 RID: 8
		protected sealed class WeightedAucAggregator : EvaluatorBase.AucAggregatorBase<EvaluatorBase.WeightedAucAggregator.AucInfo>
		{
			// Token: 0x06000028 RID: 40 RVA: 0x00002E56 File Offset: 0x00001056
			public WeightedAucAggregator(IRandom rand, int reservoirSize)
				: base(rand, reservoirSize)
			{
			}

			// Token: 0x06000029 RID: 41 RVA: 0x00002E74 File Offset: 0x00001074
			protected override double ComputWeightedAucCore(out double unweighted)
			{
				double num14;
				using (IEnumerator<EvaluatorBase.WeightedAucAggregator.AucInfo> enumerator = this._posSample.OrderByDescending((EvaluatorBase.WeightedAucAggregator.AucInfo x) => x.Score).GetEnumerator())
				{
					using (IEnumerator<EvaluatorBase.WeightedAucAggregator.AucInfo> enumerator2 = this._negSample.OrderByDescending((EvaluatorBase.WeightedAucAggregator.AucInfo x) => x.Score).GetEnumerator())
					{
						int num = 0;
						int num2 = 0;
						double num3 = 0.0;
						double num4 = 0.0;
						double num5 = 0.0;
						double num6 = 0.0;
						bool flag = enumerator.MoveNext();
						bool flag2 = enumerator2.MoveNext();
						double num7 = 0.0;
						int num8 = 0;
						double num9 = 0.0;
						IL_0231:
						while (flag)
						{
							if (!flag2)
							{
								break;
							}
							num9 = (double)enumerator.Current.Score;
							float num10 = enumerator2.Current.Score;
							if (num9 > (double)num10)
							{
								float weight = enumerator.Current.Weight;
								num3 += (double)weight;
								num++;
								flag = enumerator.MoveNext();
							}
							else
							{
								if (num9 >= (double)num10)
								{
									num7 = 0.0;
									num8 = 0;
									double num11 = 0.0;
									int num12 = 0;
									double num13 = num9;
									while (num13 == num9)
									{
										float weight2 = enumerator.Current.Weight;
										num7 += (double)weight2;
										num8++;
										flag = enumerator.MoveNext();
										if (!flag)
										{
											IL_01D4:
											while (num13 == (double)num10)
											{
												float weight3 = enumerator2.Current.Weight;
												num11 += (double)weight3;
												num12++;
												flag2 = enumerator2.MoveNext();
												if (!flag2)
												{
													break;
												}
												num10 = enumerator2.Current.Score;
											}
											num5 += num3 * num11;
											num5 += 0.5 * num7 * num11;
											num3 += num7;
											num4 += num11;
											num6 += (double)(num * num12);
											num6 += 0.5 * (double)num8 * (double)num12;
											num += num8;
											num2 += num12;
											goto IL_0231;
										}
										num9 = (double)enumerator.Current.Score;
									}
									goto IL_01D4;
								}
								float weight4 = enumerator2.Current.Weight;
								num5 += num3 * (double)weight4;
								num6 += (double)num;
								num4 += (double)weight4;
								num2++;
								flag2 = enumerator2.MoveNext();
							}
						}
						while (flag)
						{
							float weight5 = enumerator.Current.Weight;
							num3 += (double)weight5;
							num++;
							flag = enumerator.MoveNext();
						}
						while (flag2)
						{
							float weight6 = enumerator2.Current.Weight;
							num5 += num3 * (double)weight6;
							num6 += (double)num;
							if (num9 == (double)enumerator2.Current.Score)
							{
								num5 -= 0.5 * num7 * (double)weight6;
								num6 -= 0.5 * (double)num8;
							}
							num4 += (double)weight6;
							num2++;
							flag2 = enumerator2.MoveNext();
						}
						unweighted = num6 / (double)(num * num2);
						num14 = num5 / (num3 * num4);
					}
				}
				return num14;
			}

			// Token: 0x0600002A RID: 42 RVA: 0x000031EA File Offset: 0x000013EA
			protected override ValueGetter<EvaluatorBase.WeightedAucAggregator.AucInfo> GetSampleGetter()
			{
				return delegate(ref EvaluatorBase.WeightedAucAggregator.AucInfo dst)
				{
					dst = new EvaluatorBase.WeightedAucAggregator.AucInfo
					{
						Score = this._score,
						Weight = this._weight
					};
				};
			}

			// Token: 0x0600002B RID: 43 RVA: 0x000031F8 File Offset: 0x000013F8
			protected override void ProcessRowCore(float weight)
			{
				this._weight = weight;
				base.ProcessRowCore(weight);
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00003208 File Offset: 0x00001408
			protected override void AddExample(List<EvaluatorBase.WeightedAucAggregator.AucInfo> examples)
			{
				examples.Add(new EvaluatorBase.WeightedAucAggregator.AucInfo
				{
					Score = this._score,
					Weight = this._weight
				});
			}

			// Token: 0x04000010 RID: 16
			private float _weight;

			// Token: 0x02000009 RID: 9
			public struct AucInfo
			{
				// Token: 0x04000013 RID: 19
				public float Score;

				// Token: 0x04000014 RID: 20
				public float Weight;
			}
		}

		// Token: 0x0200000A RID: 10
		protected abstract class AuPrcAggregatorBase
		{
			// Token: 0x06000030 RID: 48 RVA: 0x0000323E File Offset: 0x0000143E
			public void ProcessRow(float label, float score, float weight = 1f)
			{
				this._label = label;
				this._score = score;
				this._weight = weight;
				this.ProcessRowCore();
			}

			// Token: 0x06000031 RID: 49
			protected abstract void ProcessRowCore();

			// Token: 0x06000032 RID: 50
			public abstract double ComputeWeightedAuPrc(out double unweighted);

			// Token: 0x04000015 RID: 21
			protected float _score;

			// Token: 0x04000016 RID: 22
			protected float _label;

			// Token: 0x04000017 RID: 23
			protected float _weight;
		}

		// Token: 0x0200000B RID: 11
		protected abstract class AuPrcAggregatorBase<T> : EvaluatorBase.AuPrcAggregatorBase
		{
			// Token: 0x06000034 RID: 52 RVA: 0x00003264 File Offset: 0x00001464
			protected AuPrcAggregatorBase(IRandom rand, int reservoirSize)
			{
				ValueGetter<T> sampleGetter = this.GetSampleGetter();
				this._reservoir = new ReservoirSamplerWithoutReplacement<T>(rand, reservoirSize, sampleGetter);
			}

			// Token: 0x06000035 RID: 53
			protected abstract ValueGetter<T> GetSampleGetter();

			// Token: 0x06000036 RID: 54 RVA: 0x0000328C File Offset: 0x0000148C
			protected override void ProcessRowCore()
			{
				this._reservoir.Sample();
			}

			// Token: 0x06000037 RID: 55 RVA: 0x0000329C File Offset: 0x0000149C
			public override double ComputeWeightedAuPrc(out double unweighted)
			{
				if (this._reservoir.Size == 0)
				{
					return unweighted = 0.0;
				}
				return this.ComputeWeightedAuPrcCore(out unweighted);
			}

			// Token: 0x06000038 RID: 56
			protected abstract double ComputeWeightedAuPrcCore(out double unweighted);

			// Token: 0x04000018 RID: 24
			protected readonly ReservoirSamplerWithoutReplacement<T> _reservoir;
		}

		// Token: 0x0200000C RID: 12
		protected sealed class UnweightedAuPrcAggregator : EvaluatorBase.AuPrcAggregatorBase<EvaluatorBase.UnweightedAuPrcAggregator.Info>
		{
			// Token: 0x06000039 RID: 57 RVA: 0x000032CC File Offset: 0x000014CC
			public UnweightedAuPrcAggregator(IRandom rand, int reservoirSize)
				: base(rand, reservoirSize)
			{
			}

			// Token: 0x0600003A RID: 58 RVA: 0x000032F4 File Offset: 0x000014F4
			protected override double ComputeWeightedAuPrcCore(out double unweighted)
			{
				this._reservoir.Lock();
				EvaluatorBase.UnweightedAuPrcAggregator.Info[] sample = this._reservoir.GetSample().ToArray<EvaluatorBase.UnweightedAuPrcAggregator.Info>();
				int num = 0;
				int num2 = 0;
				foreach (EvaluatorBase.UnweightedAuPrcAggregator.Info info in sample)
				{
					if (info.Label > 0f)
					{
						num++;
					}
					else
					{
						num2++;
					}
				}
				IOrderedEnumerable<int> orderedEnumerable = from i in Enumerable.Range(0, num + num2)
					orderby sample[i].Score descending
					select i;
				double num3 = 0.0;
				double num4 = 1.0;
				int num5 = 0;
				int num6 = 0;
				double num7 = 0.0;
				foreach (int num8 in orderedEnumerable)
				{
					if (sample[num8].Label > 0f)
					{
						num5++;
						double num9 = (double)num5 / (double)num;
						double num10 = (double)num5 / (double)(num5 + num6);
						num7 += (num9 - num3) * (num4 + num10) / 2.0;
						num4 = num10;
						num3 = num9;
					}
					else
					{
						num6++;
						num4 = (double)num5 / (double)(num5 + num6);
					}
				}
				return unweighted = num7;
			}

			// Token: 0x0600003B RID: 59 RVA: 0x0000347E File Offset: 0x0000167E
			protected override ValueGetter<EvaluatorBase.UnweightedAuPrcAggregator.Info> GetSampleGetter()
			{
				return delegate(ref EvaluatorBase.UnweightedAuPrcAggregator.Info dst)
				{
					dst.Score = this._score;
					dst.Label = this._label;
				};
			}

			// Token: 0x0200000D RID: 13
			public struct Info
			{
				// Token: 0x04000019 RID: 25
				public float Score;

				// Token: 0x0400001A RID: 26
				public float Label;
			}
		}

		// Token: 0x0200000E RID: 14
		protected sealed class WeightedAuPrcAggregator : EvaluatorBase.AuPrcAggregatorBase<EvaluatorBase.WeightedAuPrcAggregator.Info>
		{
			// Token: 0x0600003D RID: 61 RVA: 0x0000348C File Offset: 0x0000168C
			public WeightedAuPrcAggregator(IRandom rand, int reservoirSize)
				: base(rand, reservoirSize)
			{
			}

			// Token: 0x0600003E RID: 62 RVA: 0x000034B0 File Offset: 0x000016B0
			protected override double ComputeWeightedAuPrcCore(out double unweighted)
			{
				this._reservoir.Lock();
				IEnumerable<EvaluatorBase.WeightedAuPrcAggregator.Info> sample = this._reservoir.GetSample();
				int num = 0;
				int num2 = 0;
				double num3 = 0.0;
				double num4 = 0.0;
				foreach (EvaluatorBase.WeightedAuPrcAggregator.Info info2 in sample)
				{
					if (info2.Label > 0f)
					{
						num++;
						num3 += (double)info2.Weight;
					}
					else
					{
						num2++;
						num4 += (double)info2.Weight;
					}
				}
				IOrderedEnumerable<KeyValuePair<int, EvaluatorBase.WeightedAuPrcAggregator.Info>> orderedEnumerable = from kvp in sample.Select((EvaluatorBase.WeightedAuPrcAggregator.Info info, int i) => new KeyValuePair<int, EvaluatorBase.WeightedAuPrcAggregator.Info>(i, info))
					orderby kvp.Value.Score descending
					select kvp;
				double num5 = 0.0;
				double num6 = 1.0;
				double num7 = 0.0;
				double num8 = 0.0;
				double num9 = 0.0;
				double num10 = 0.0;
				double num11 = 1.0;
				double num12 = 0.0;
				double num13 = 0.0;
				unweighted = 0.0;
				foreach (KeyValuePair<int, EvaluatorBase.WeightedAuPrcAggregator.Info> keyValuePair in orderedEnumerable)
				{
					if (keyValuePair.Value.Label > 0f)
					{
						num7 += (double)keyValuePair.Value.Weight;
						num12 += 1.0;
						double num14 = num7 / num3;
						double num15 = num7 / (num7 + num8);
						double num16 = num12 / (double)num;
						double num17 = num12 / (num12 + num13);
						num9 += (num14 - num5) * (num6 + num15) / 2.0;
						num6 = num15;
						num5 = num14;
						unweighted += (num16 - num10) * (num11 + num17) / 2.0;
						num11 = num17;
						num10 = num16;
					}
					else
					{
						num8 += (double)keyValuePair.Value.Weight;
						num13 += 1.0;
						num6 = num7 / (num7 + num8);
						num11 = num12 / (num12 + num13);
					}
				}
				return num9;
			}

			// Token: 0x0600003F RID: 63 RVA: 0x00003742 File Offset: 0x00001942
			protected override ValueGetter<EvaluatorBase.WeightedAuPrcAggregator.Info> GetSampleGetter()
			{
				return delegate(ref EvaluatorBase.WeightedAuPrcAggregator.Info dst)
				{
					dst.Score = this._score;
					dst.Label = this._label;
					dst.Weight = this._weight;
				};
			}

			// Token: 0x0200000F RID: 15
			public struct Info
			{
				// Token: 0x0400001D RID: 29
				public float Score;

				// Token: 0x0400001E RID: 30
				public float Label;

				// Token: 0x0400001F RID: 31
				public float Weight;
			}
		}

		// Token: 0x02000010 RID: 16
		protected abstract class AggregatorBase
		{
			// Token: 0x06000043 RID: 67 RVA: 0x00003750 File Offset: 0x00001950
			protected AggregatorBase(IHostEnvironment env, string stratName)
			{
				this._host = env.Register("Aggregator");
				this._passNum = -1;
				this.StratName = stratName;
			}

			// Token: 0x06000044 RID: 68 RVA: 0x00003777 File Offset: 0x00001977
			public bool Start()
			{
				Contracts.Check(this._host, this._passNum == -1, "Start() should only be called before processing any data.");
				this._passNum = 0;
				return this.IsActive();
			}

			// Token: 0x06000045 RID: 69
			public abstract void InitializeNextPass(IRow row, RoleMappedSchema schema);

			// Token: 0x06000046 RID: 70
			public abstract void ProcessRow();

			// Token: 0x06000047 RID: 71 RVA: 0x0000379F File Offset: 0x0000199F
			public bool FinishPass()
			{
				this.FinishPassCore();
				this._passNum++;
				return this.IsActive();
			}

			// Token: 0x06000048 RID: 72 RVA: 0x000037BB File Offset: 0x000019BB
			public virtual bool IsActive()
			{
				return this._passNum < 1;
			}

			// Token: 0x06000049 RID: 73 RVA: 0x000037C6 File Offset: 0x000019C6
			protected virtual void FinishPassCore()
			{
			}

			// Token: 0x0600004A RID: 74
			public abstract Dictionary<string, IDataView> Finish();

			// Token: 0x0600004B RID: 75 RVA: 0x000037D0 File Offset: 0x000019D0
			public void GetWarnings(Dictionary<string, IDataView> dict, IHostEnvironment env)
			{
				List<string> warningsCore = this.GetWarningsCore();
				if (Utils.Size<string>(warningsCore) > 0)
				{
					ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(env);
					arrayDataViewBuilder.AddColumn<DvText>("WarningText", TextType.Instance, warningsCore.Select((string s) => new DvText(s)).ToArray<DvText>());
					dict.Add("Warnings", arrayDataViewBuilder.GetDataView(null));
				}
			}

			// Token: 0x0600004C RID: 76 RVA: 0x00003848 File Offset: 0x00001A48
			protected virtual List<string> GetWarningsCore()
			{
				List<string> list = new List<string>();
				if (this.NumUnlabeledInstances > 0L)
				{
					list.Add(string.Format("Encountered {0} unlabeled instances during testing.", this.NumUnlabeledInstances));
				}
				if (this.NumBadWeights > 0L)
				{
					list.Add(string.Format("Encountered {0} non-finite weights during testing. These weights have been replaced with 1.", this.NumBadWeights));
				}
				if (this.NumBadScores > 0L)
				{
					list.Add(string.Format("The predictor produced non-finite prediction values on {0} instances during testing. Possible causes: abnormal data or the predictor is numerically unstable.", this.NumBadScores));
				}
				return list;
			}

			// Token: 0x04000020 RID: 32
			public readonly string StratName;

			// Token: 0x04000021 RID: 33
			protected long NumUnlabeledInstances;

			// Token: 0x04000022 RID: 34
			protected long NumBadScores;

			// Token: 0x04000023 RID: 35
			protected long NumBadWeights;

			// Token: 0x04000024 RID: 36
			protected readonly IHost _host;

			// Token: 0x04000025 RID: 37
			protected int _passNum;
		}

		// Token: 0x02000011 RID: 17
		protected abstract class AggregatorDictionaryBase
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600004E RID: 78 RVA: 0x000038CB File Offset: 0x00001ACB
			public string ColName
			{
				get
				{
					return this._colName;
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600004F RID: 79
			public abstract int Count { get; }

			// Token: 0x06000050 RID: 80 RVA: 0x000038D3 File Offset: 0x00001AD3
			protected AggregatorDictionaryBase(RoleMappedSchema schema, string stratCol, Func<string, EvaluatorBase.AggregatorBase> createAgg)
			{
				this._schema = schema;
				this._createAgg = createAgg;
				this._colName = stratCol;
			}

			// Token: 0x06000051 RID: 81
			public abstract void Reset(IRow row);

			// Token: 0x06000052 RID: 82 RVA: 0x000038F0 File Offset: 0x00001AF0
			public static EvaluatorBase.AggregatorDictionaryBase Create(RoleMappedSchema schema, string stratCol, ColumnType stratType, Func<string, EvaluatorBase.AggregatorBase> createAgg)
			{
				if (stratType.KeyCount == 0 && !stratType.IsText)
				{
					throw Contracts.ExceptUserArg("strat", "stratification column '{0}' has type '{1}', but must be a known count key or text", new object[] { stratCol, stratType });
				}
				Func<RoleMappedSchema, string, ColumnType, Func<string, EvaluatorBase.AggregatorBase>, EvaluatorBase.AggregatorDictionaryBase.GenericAggregatorDictionary<int>> func = new Func<RoleMappedSchema, string, ColumnType, Func<string, EvaluatorBase.AggregatorBase>, EvaluatorBase.AggregatorDictionaryBase.GenericAggregatorDictionary<int>>(EvaluatorBase.AggregatorDictionaryBase.CreateDictionary<int>);
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { stratType.RawType });
				return (EvaluatorBase.AggregatorDictionaryBase)methodInfo.Invoke(null, new object[] { schema, stratCol, stratType, createAgg });
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00003986 File Offset: 0x00001B86
			private static EvaluatorBase.AggregatorDictionaryBase.GenericAggregatorDictionary<TStrat> CreateDictionary<TStrat>(RoleMappedSchema schema, string stratCol, ColumnType stratType, Func<string, EvaluatorBase.AggregatorBase> createAgg)
			{
				return new EvaluatorBase.AggregatorDictionaryBase.GenericAggregatorDictionary<TStrat>(schema, stratCol, stratType, createAgg);
			}

			// Token: 0x06000054 RID: 84
			public abstract EvaluatorBase.AggregatorBase Get();

			// Token: 0x06000055 RID: 85
			public abstract IEnumerable<EvaluatorBase.AggregatorBase> GetAll();

			// Token: 0x04000027 RID: 39
			protected IRow _row;

			// Token: 0x04000028 RID: 40
			protected readonly Func<string, EvaluatorBase.AggregatorBase> _createAgg;

			// Token: 0x04000029 RID: 41
			protected readonly string _colName;

			// Token: 0x0400002A RID: 42
			protected readonly RoleMappedSchema _schema;

			// Token: 0x02000012 RID: 18
			private sealed class GenericAggregatorDictionary<TStrat> : EvaluatorBase.AggregatorDictionaryBase
			{
				// Token: 0x17000003 RID: 3
				// (get) Token: 0x06000056 RID: 86 RVA: 0x00003991 File Offset: 0x00001B91
				public override int Count
				{
					get
					{
						return this._dict.Count;
					}
				}

				// Token: 0x06000057 RID: 87 RVA: 0x0000399E File Offset: 0x00001B9E
				public GenericAggregatorDictionary(RoleMappedSchema schema, string stratCol, ColumnType stratType, Func<string, EvaluatorBase.AggregatorBase> createAgg)
					: base(schema, stratCol, createAgg)
				{
					this._dict = new Dictionary<TStrat, EvaluatorBase.AggregatorBase>();
				}

				// Token: 0x06000058 RID: 88 RVA: 0x000039B8 File Offset: 0x00001BB8
				public override void Reset(IRow row)
				{
					this._row = row;
					int num;
					row.Schema.TryGetColumnIndex(this._colName, ref num);
					this._stratGetter = row.GetGetter<TStrat>(num);
				}

				// Token: 0x06000059 RID: 89 RVA: 0x000039F0 File Offset: 0x00001BF0
				public override EvaluatorBase.AggregatorBase Get()
				{
					this._stratGetter.Invoke(ref this._value);
					EvaluatorBase.AggregatorBase aggregatorBase;
					if (!this._dict.TryGetValue(this._value, out aggregatorBase))
					{
						aggregatorBase = this._createAgg(this._value.ToString());
						aggregatorBase.Start();
						aggregatorBase.InitializeNextPass(this._row, this._schema);
						this._dict.Add(this._value, aggregatorBase);
					}
					return aggregatorBase;
				}

				// Token: 0x0600005A RID: 90 RVA: 0x00003A75 File Offset: 0x00001C75
				public override IEnumerable<EvaluatorBase.AggregatorBase> GetAll()
				{
					return this._dict.Select((KeyValuePair<TStrat, EvaluatorBase.AggregatorBase> kvp) => kvp.Value);
				}

				// Token: 0x0400002B RID: 43
				private readonly Dictionary<TStrat, EvaluatorBase.AggregatorBase> _dict;

				// Token: 0x0400002C RID: 44
				private ValueGetter<TStrat> _stratGetter;

				// Token: 0x0400002D RID: 45
				private TStrat _value;
			}
		}
	}
}
