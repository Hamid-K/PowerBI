using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Numeric;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200008A RID: 138
	public sealed class ClusteringEvaluator : RowToRowEvaluatorBase
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
		public ClusteringEvaluator(ClusteringEvaluator.Arguments args, IHostEnvironment env)
			: base(env, "ClusteringEvaluator")
		{
			this._calculateDbi = args.calculateDbi;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000EABC File Offset: 0x0000CCBC
		protected override void CheckScoreAndLabelTypes(RoleMappedSchema schema)
		{
			ColumnType columnType;
			if (schema.Label != null && (columnType = schema.Label.Type) != NumberType.Float && columnType.KeyCount == 0)
			{
				throw Contracts.Except(this._host, "Clustering evaluator: label column '{0}' type must be {1} or Key of known cardinality. Provide a correct label column, or none: it is optional.", new object[]
				{
					schema.Label.Name,
					NumberType.Float
				});
			}
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			columnType = uniqueColumn.Type;
			if (!columnType.IsKnownSizeVector || columnType.ItemType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Scores column '{0}' type must be a float vector of known size", new object[] { uniqueColumn.Name });
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		protected override void CheckCustomColumnTypesCore(RoleMappedSchema schema)
		{
			ColumnType type = schema.Feature.Type;
			if (!type.IsKnownSizeVector || type.ItemType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Features column '{0}' type must be {1} vector of known-size", new object[]
				{
					schema.Feature.Name,
					NumberType.Float
				});
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000EC04 File Offset: 0x0000CE04
		protected override Func<int, bool> GetActiveColsCore(RoleMappedSchema schema)
		{
			Func<int, bool> pred = base.GetActiveColsCore(schema);
			return (int i) => (this._calculateDbi && i == schema.Feature.Index) || pred(i);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000EC44 File Offset: 0x0000CE44
		protected override EvaluatorBase.AggregatorBase GetAggregatorCore(RoleMappedSchema schema, string stratName)
		{
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			int vectorSize = uniqueColumn.Type.VectorSize;
			return new ClusteringEvaluator.Aggregator(this._host, schema.Feature.Type.VectorSize, vectorSize, this._calculateDbi, schema.Weight != null, stratName);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		protected override IRowMapper CreatePerInstanceRowMapper(RoleMappedSchema schema)
		{
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			int vectorSize = uniqueColumn.Type.VectorSize;
			return new ClusteringPerInstanceEvaluator(this._host, schema.Schema, uniqueColumn.Name, vectorSize);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("NMI", "NMI", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("AvgMinScore", "AvgMinScore", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("DBI", "DBI", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield break;
		}

		// Token: 0x040000F2 RID: 242
		public const string LoadName = "ClusteringEvaluator";

		// Token: 0x040000F3 RID: 243
		private const string NMI = "NMI";

		// Token: 0x040000F4 RID: 244
		private const string AvgMinScore = "AvgMinScore";

		// Token: 0x040000F5 RID: 245
		private const string DBI = "DBI";

		// Token: 0x040000F6 RID: 246
		private readonly bool _calculateDbi;

		// Token: 0x0200008B RID: 139
		public sealed class Arguments
		{
			// Token: 0x040000F7 RID: 247
			[Argument(0, HelpText = "Calculate DBI? (time-consuming unsupervised metric)", ShortName = "dbi")]
			public bool calculateDbi;
		}

		// Token: 0x0200008C RID: 140
		private sealed class Aggregator : EvaluatorBase.AggregatorBase
		{
			// Token: 0x0600028A RID: 650 RVA: 0x0000EE44 File Offset: 0x0000D044
			public Aggregator(IHostEnvironment env, int featureCount, int scoreVectorSize, bool calculateDbi, bool weighted, string stratName)
				: base(env, stratName)
			{
				this._calculateDbi = calculateDbi;
				this._scoresArr = new float[scoreVectorSize];
				this._indicesArr = new int[scoreVectorSize];
				this._counters = new ClusteringEvaluator.Aggregator.Counters(scoreVectorSize, this._calculateDbi, featureCount);
				this._weightedCounters = (weighted ? new ClusteringEvaluator.Aggregator.Counters(scoreVectorSize, this._calculateDbi, featureCount) : null);
				if (this._calculateDbi)
				{
					this._clusterCentroids = new VBuffer<float>[scoreVectorSize];
					for (int i = 0; i < scoreVectorSize; i++)
					{
						this._clusterCentroids[i] = VBufferUtils.CreateEmpty<float>(featureCount);
					}
				}
			}

			// Token: 0x0600028B RID: 651 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
			private void ProcessRowFirstPass()
			{
				float num = 0f;
				this._labelGetter.Invoke(ref num);
				if (float.IsNaN(num))
				{
					this.NumUnlabeledInstances += 1L;
					num = 0f;
				}
				int num2 = (int)num;
				if ((float)num2 != num || num2 < 0)
				{
					throw Contracts.Except(this._host, "Invalid label: {0}", new object[] { num });
				}
				this._scoreGetter.Invoke(ref this._scores);
				Contracts.Check(this._host, this._scores.Length == this._scoresArr.Length);
				if (VBufferUtils.HasNaNs(ref this._scores) || VBufferUtils.HasNonFinite(ref this._scores))
				{
					this.NumBadScores += 1L;
					return;
				}
				this._scores.CopyTo(this._scoresArr);
				float num3 = 1f;
				if (this._weightGetter != null)
				{
					this._weightGetter.Invoke(ref num3);
					if (!FloatUtils.IsFinite(num3))
					{
						this.NumBadWeights += 1L;
						num3 = 1f;
					}
				}
				int num4 = 0;
				foreach (int num5 in from i in Enumerable.Range(0, this._scoresArr.Length)
					orderby this._scoresArr[i]
					select i)
				{
					this._indicesArr[num4++] = num5;
				}
				this._counters.UpdateFirstPass(num2, this._scoresArr, 1f, this._indicesArr);
				if (this._weightedCounters != null)
				{
					this._weightedCounters.UpdateFirstPass(num2, this._scoresArr, num3, this._indicesArr);
				}
				if (this._clusterCentroids != null)
				{
					this._featGetter.Invoke(ref this._features);
					VectorUtils.Add(ref this._features, ref this._clusterCentroids[this._indicesArr[0]]);
				}
			}

			// Token: 0x0600028C RID: 652 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
			private void ProcessRowSecondPass()
			{
				this._featGetter.Invoke(ref this._features);
				this._scoreGetter.Invoke(ref this._scores);
				Contracts.Check(this._host, this._scores.Length == this._scoresArr.Length);
				if (VBufferUtils.HasNaNs(ref this._scores) || VBufferUtils.HasNonFinite(ref this._scores))
				{
					return;
				}
				this._scores.CopyTo(this._scoresArr);
				int num = 0;
				foreach (int num2 in from i in Enumerable.Range(0, this._scoresArr.Length)
					orderby this._scoresArr[i]
					select i)
				{
					this._indicesArr[num++] = num2;
				}
				this._counters.UpdateSecondPass(ref this._features, this._indicesArr);
				if (this._weightedCounters != null)
				{
					this._weightedCounters.UpdateSecondPass(ref this._features, this._indicesArr);
				}
			}

			// Token: 0x0600028D RID: 653 RVA: 0x0000F204 File Offset: 0x0000D404
			public override void InitializeNextPass(IRow row, RoleMappedSchema schema)
			{
				if (this._calculateDbi)
				{
					this._featGetter = row.GetGetter<VBuffer<float>>(schema.Feature.Index);
				}
				ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
				this._scoreGetter = row.GetGetter<VBuffer<float>>(uniqueColumn.Index);
				if (this._passNum == 0)
				{
					if (schema.Label != null)
					{
						this._labelGetter = RowCursorUtils.GetLabelGetter(row, schema.Label.Index);
					}
					else
					{
						this._labelGetter = delegate(ref float value)
						{
							value = float.NaN;
						};
					}
					if (schema.Weight != null)
					{
						this._weightGetter = row.GetGetter<float>(schema.Weight.Index);
						return;
					}
				}
				else
				{
					this._counters.InitializeSecondPass(this._clusterCentroids);
					if (this._weightedCounters != null)
					{
						this._weightedCounters.InitializeSecondPass(this._clusterCentroids);
					}
				}
			}

			// Token: 0x0600028E RID: 654 RVA: 0x0000F2E9 File Offset: 0x0000D4E9
			public override void ProcessRow()
			{
				if (this._passNum == 0)
				{
					this.ProcessRowFirstPass();
					return;
				}
				this.ProcessRowSecondPass();
			}

			// Token: 0x0600028F RID: 655 RVA: 0x0000F300 File Offset: 0x0000D500
			public override bool IsActive()
			{
				return (this._calculateDbi && this._passNum < 2) || this._passNum < 1;
			}

			// Token: 0x06000290 RID: 656 RVA: 0x0000F31E File Offset: 0x0000D51E
			protected override void FinishPassCore()
			{
			}

			// Token: 0x06000291 RID: 657 RVA: 0x0000F320 File Offset: 0x0000D520
			public override Dictionary<string, IDataView> Finish()
			{
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(this._host);
				if (this._weightedCounters == null)
				{
					arrayDataViewBuilder.AddColumn<double>("NMI", NumberType.R8, new double[] { this._counters.NMI });
					arrayDataViewBuilder.AddColumn<double>("AvgMinScore", NumberType.R8, new double[] { this._counters.AvgMinScores });
					if (this._clusterCentroids != null)
					{
						arrayDataViewBuilder.AddColumn<double>("DBI", NumberType.R8, new double[] { this._counters.DBI });
					}
				}
				else
				{
					arrayDataViewBuilder.AddColumn<DvBool>("IsWeighted", BoolType.Instance, new DvBool[]
					{
						DvBool.False,
						DvBool.True
					});
					arrayDataViewBuilder.AddColumn<double>("NMI", NumberType.R8, new double[]
					{
						this._counters.NMI,
						this._weightedCounters.NMI
					});
					arrayDataViewBuilder.AddColumn<double>("AvgMinScore", NumberType.R8, new double[]
					{
						this._counters.AvgMinScores,
						this._weightedCounters.AvgMinScores
					});
					if (this._clusterCentroids != null)
					{
						arrayDataViewBuilder.AddColumn<double>("DBI", NumberType.R8, new double[]
						{
							this._counters.DBI,
							this._weightedCounters.DBI
						});
					}
				}
				return new Dictionary<string, IDataView> { 
				{
					"OverallMetrics",
					arrayDataViewBuilder.GetDataView(null)
				} };
			}

			// Token: 0x06000292 RID: 658 RVA: 0x0000F4D9 File Offset: 0x0000D6D9
			[Conditional("DEBUG")]
			private void AssertValid(bool assertGetters)
			{
				if (assertGetters)
				{
					int passNum = this._passNum;
				}
			}

			// Token: 0x040000F8 RID: 248
			private ValueGetter<float> _labelGetter;

			// Token: 0x040000F9 RID: 249
			private ValueGetter<VBuffer<float>> _scoreGetter;

			// Token: 0x040000FA RID: 250
			private ValueGetter<float> _weightGetter;

			// Token: 0x040000FB RID: 251
			private ValueGetter<VBuffer<float>> _featGetter;

			// Token: 0x040000FC RID: 252
			private VBuffer<float> _scores;

			// Token: 0x040000FD RID: 253
			private readonly float[] _scoresArr;

			// Token: 0x040000FE RID: 254
			private readonly int[] _indicesArr;

			// Token: 0x040000FF RID: 255
			private VBuffer<float> _features;

			// Token: 0x04000100 RID: 256
			private readonly VBuffer<float>[] _clusterCentroids;

			// Token: 0x04000101 RID: 257
			private readonly ClusteringEvaluator.Aggregator.Counters _counters;

			// Token: 0x04000102 RID: 258
			private readonly ClusteringEvaluator.Aggregator.Counters _weightedCounters;

			// Token: 0x04000103 RID: 259
			private readonly bool _calculateDbi;

			// Token: 0x0200008D RID: 141
			private sealed class Counters
			{
				// Token: 0x1700001F RID: 31
				// (get) Token: 0x06000296 RID: 662 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
				public double NMI
				{
					get
					{
						double num = double.NaN;
						if (this._confusionMatrix.Count > 1)
						{
							num = 0.0;
							double num2 = 0.0;
							for (int i = 0; i < this._confusionMatrix.Count; i++)
							{
								double num3 = this._numInstancesOfClass[i] / this._numInstances;
								if (num3 > 0.0)
								{
									for (int j = 0; j < this._confusionMatrix[i].Length; j++)
									{
										double num4 = this._confusionMatrix[i][j] / this._numInstances;
										double num5 = this._numInstancesOfClstr[j] / this._numInstances;
										if (num4 > 0.0 && num5 > 0.0)
										{
											num += num4 * Math.Log(num4 / (num3 * num5));
										}
									}
									num2 += -num3 * Math.Log(num3);
								}
							}
							num /= num2;
						}
						return num;
					}
				}

				// Token: 0x17000020 RID: 32
				// (get) Token: 0x06000297 RID: 663 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
				public double AvgMinScores
				{
					get
					{
						return this._sumMinScores / this._numInstances;
					}
				}

				// Token: 0x17000021 RID: 33
				// (get) Token: 0x06000298 RID: 664 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
				public double DBI
				{
					get
					{
						if (!this._calculateDbi)
						{
							return double.NaN;
						}
						double num = 0.0;
						int num2 = this._distancesToCentroids.Length;
						for (int i = 0; i < num2; i++)
						{
							this._distancesToCentroids[i] /= this._numInstancesOfClstr[i];
						}
						for (int j = 0; j < num2; j++)
						{
							double num3 = 0.0;
							if (this._numInstancesOfClstr[j] != 0.0)
							{
								VBuffer<float> vbuffer = this._clusterCentroids[j];
								for (int k = 0; k < num2; k++)
								{
									if (j != k && this._numInstancesOfClstr[k] != 0.0)
									{
										VBuffer<float> vbuffer2 = this._clusterCentroids[k];
										double num4 = this._distancesToCentroids[j] + this._distancesToCentroids[k];
										float num5 = VectorUtils.Distance(ref vbuffer, ref vbuffer2);
										num3 = Math.Max(num3, num4 / (double)num5);
									}
								}
								num += num3;
							}
						}
						return num / (double)num2;
					}
				}

				// Token: 0x06000299 RID: 665 RVA: 0x0000F710 File Offset: 0x0000D910
				public Counters(int numClusters, bool calculateDbi, int featureCount)
				{
					this._numClusters = numClusters;
					this._calculateDbi = calculateDbi;
					this._numInstancesOfClstr = new double[this._numClusters];
					this._numInstancesOfClass = new List<double>();
					this._confusionMatrix = new List<double[]>();
					if (this._calculateDbi)
					{
						this._clusterCentroids = new VBuffer<float>[this._numClusters];
						for (int i = 0; i < this._numClusters; i++)
						{
							this._clusterCentroids[i] = VBufferUtils.CreateEmpty<float>(featureCount);
						}
						this._distancesToCentroids = new double[this._numClusters];
					}
				}

				// Token: 0x0600029A RID: 666 RVA: 0x0000F7AC File Offset: 0x0000D9AC
				public void UpdateFirstPass(int intLabel, float[] scores, float weight, int[] indices)
				{
					int num = indices[0];
					this._numInstances += (double)weight;
					this._sumMinScores += (double)(weight * scores[indices[0]]);
					while (this._numInstancesOfClass.Count <= intLabel)
					{
						this._numInstancesOfClass.Add(0.0);
					}
					List<double> numInstancesOfClass;
					(numInstancesOfClass = this._numInstancesOfClass)[intLabel] = numInstancesOfClass[intLabel] + (double)weight;
					this._numInstancesOfClstr[num] += (double)weight;
					while (this._confusionMatrix.Count <= intLabel)
					{
						this._confusionMatrix.Add(new double[scores.Length]);
					}
					this._confusionMatrix[intLabel][num] += (double)weight;
				}

				// Token: 0x0600029B RID: 667 RVA: 0x0000F880 File Offset: 0x0000DA80
				public void InitializeSecondPass(VBuffer<float>[] clusterCentroids)
				{
					for (int i = 0; i < clusterCentroids.Length; i++)
					{
						clusterCentroids[i].CopyTo(ref this._clusterCentroids[i]);
						VectorUtils.ScaleBy(ref this._clusterCentroids[i], (float)(1.0 / this._numInstancesOfClstr[i]));
					}
				}

				// Token: 0x0600029C RID: 668 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
				public void UpdateSecondPass(ref VBuffer<float> features, int[] indices)
				{
					int num = indices[0];
					float num2 = VectorUtils.Distance(ref this._clusterCentroids[num], ref features);
					this._distancesToCentroids[num] += (double)num2;
				}

				// Token: 0x04000105 RID: 261
				private double _numInstances;

				// Token: 0x04000106 RID: 262
				private double _sumMinScores;

				// Token: 0x04000107 RID: 263
				private readonly double[] _numInstancesOfClstr;

				// Token: 0x04000108 RID: 264
				private readonly List<double> _numInstancesOfClass;

				// Token: 0x04000109 RID: 265
				private readonly List<double[]> _confusionMatrix;

				// Token: 0x0400010A RID: 266
				private readonly VBuffer<float>[] _clusterCentroids;

				// Token: 0x0400010B RID: 267
				private readonly double[] _distancesToCentroids;

				// Token: 0x0400010C RID: 268
				private readonly int _numClusters;

				// Token: 0x0400010D RID: 269
				private readonly bool _calculateDbi;
			}
		}
	}
}
