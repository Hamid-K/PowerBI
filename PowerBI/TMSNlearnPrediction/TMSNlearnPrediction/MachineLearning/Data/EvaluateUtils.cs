using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200001D RID: 29
	public static class EvaluateUtils
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000041B8 File Offset: 0x000023B8
		public static SubComponent<IMamlEvaluator, SignatureMamlEvaluator> GetEvaluatorType(IExceptionContext ectx, ISchema schema)
		{
			DvText dvText = default(DvText);
			int num;
			MetadataUtils.GetMaxMetadataKind(schema, ref num, "ScoreColumnSetId", new Func<ISchema, int, bool>(EvaluateUtils.CheckScoreColumnKindIsKnown));
			if (num >= 0)
			{
				schema.GetMetadata<DvText>("ScoreColumnKind", num, ref dvText);
				string text = dvText.ToString();
				Dictionary<string, string> instance = EvaluateUtils.DefaultEvaluatorTable.Instance;
				return new SubComponent<IMamlEvaluator, SignatureMamlEvaluator>(instance[text]);
			}
			MetadataUtils.GetMaxMetadataKind(schema, ref num, "ScoreColumnSetId", new Func<ISchema, int, bool>(EvaluateUtils.CheckScoreColumnKind));
			if (num >= 0)
			{
				schema.GetMetadata<DvText>("ScoreColumnKind", num, ref dvText);
				throw Contracts.ExceptUserArg(ectx, "evaluator", "No default evaluator found for score column kind '{0}'.", new object[] { dvText.ToString() });
			}
			throw Contracts.ExceptUserArg(ectx, "schema", "No score columns have been automatically detected.");
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004284 File Offset: 0x00002484
		private static bool CheckScoreColumnKindIsKnown(ISchema schema, int col)
		{
			ColumnType metadataTypeOrNull = schema.GetMetadataTypeOrNull("ScoreColumnKind", col);
			if (metadataTypeOrNull == null || !metadataTypeOrNull.IsText)
			{
				return false;
			}
			DvText dvText = default(DvText);
			schema.GetMetadata<DvText>("ScoreColumnKind", col, ref dvText);
			Dictionary<string, string> instance = EvaluateUtils.DefaultEvaluatorTable.Instance;
			return instance.ContainsKey(dvText.ToString());
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000042DC File Offset: 0x000024DC
		private static bool CheckScoreColumnKind(ISchema schema, int col)
		{
			ColumnType metadataTypeOrNull = schema.GetMetadataTypeOrNull("ScoreColumnKind", col);
			return metadataTypeOrNull != null && metadataTypeOrNull.IsText;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004320 File Offset: 0x00002520
		public static ColumnInfo GetScoreColumnInfo(IExceptionContext ectx, ISchema schema, string name, string argName, string kind, string valueKind = "Score", string defName = null)
		{
			Contracts.CheckValue<ISchema>(ectx, schema, "schema");
			Contracts.CheckNonEmpty(ectx, argName, "argName");
			Contracts.CheckNonEmpty(ectx, kind, "kind");
			Contracts.CheckNonEmpty(ectx, valueKind, "valueKind");
			ColumnInfo columnInfo;
			if (string.IsNullOrWhiteSpace(name))
			{
				int num;
				uint maxMetadataKind = MetadataUtils.GetMaxMetadataKind(schema, ref num, "ScoreColumnSetId", (ISchema s, int c) => EvaluateUtils.IsScoreColumnKind(ectx, s, c, kind));
				DvText dvText = default(DvText);
				foreach (int num2 in MetadataUtils.GetColumnSet(schema, "ScoreColumnSetId", maxMetadataKind))
				{
					if (!MetadataUtils.IsHidden(schema, num2) && MetadataUtils.TryGetMetadata<DvText>(schema, TextType.Instance, "ScoreValueKind", num2, ref dvText) && dvText.EqualsStr(valueKind))
					{
						return ColumnInfo.CreateFromIndex(schema, num2);
					}
				}
				if (!string.IsNullOrWhiteSpace(defName) && ColumnInfo.TryCreateFromName(schema, defName, ref columnInfo))
				{
					return columnInfo;
				}
				throw Contracts.ExceptUserArg(ectx, argName, "Score column is missing");
			}
			if (!ColumnInfo.TryCreateFromName(schema, name, ref columnInfo))
			{
				throw Contracts.ExceptUserArg(ectx, argName, "Score column is missing");
			}
			return columnInfo;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000448C File Offset: 0x0000268C
		public static ColumnInfo GetOptAuxScoreColumnInfo(IExceptionContext ectx, ISchema schema, string name, string argName, int colScore, string valueKind, Func<ColumnType, bool> testType)
		{
			Contracts.CheckValue<ISchema>(ectx, schema, "schema");
			Contracts.CheckNonEmpty(ectx, argName, "argName");
			Contracts.CheckParam(ectx, 0 <= colScore && colScore < schema.ColumnCount, "colScore");
			Contracts.CheckNonEmpty(ectx, valueKind, "valueKind");
			if (!string.IsNullOrWhiteSpace(name))
			{
				ColumnInfo columnInfo;
				if (!ColumnInfo.TryCreateFromName(schema, name, ref columnInfo))
				{
					throw Contracts.ExceptUserArg(ectx, argName, "{0} column is missing", new object[] { valueKind });
				}
				if (!testType(columnInfo.Type))
				{
					throw Contracts.ExceptUserArg(ectx, argName, "{0} column has incompatible type", new object[] { valueKind });
				}
				return columnInfo;
			}
			else
			{
				ColumnType metadataTypeOrNull = schema.GetMetadataTypeOrNull("ScoreColumnSetId", colScore);
				if (metadataTypeOrNull == null || !metadataTypeOrNull.IsKey || metadataTypeOrNull.RawKind != 6)
				{
					return null;
				}
				uint num = 0U;
				schema.GetMetadata<uint>("ScoreColumnSetId", colScore, ref num);
				DvText dvText = default(DvText);
				foreach (int num2 in MetadataUtils.GetColumnSet(schema, "ScoreColumnSetId", num))
				{
					if (!MetadataUtils.IsHidden(schema, num2) && MetadataUtils.TryGetMetadata<DvText>(schema, TextType.Instance, "ScoreValueKind", num2, ref dvText) && dvText.EqualsStr(valueKind))
					{
						ColumnInfo columnInfo2 = ColumnInfo.CreateFromIndex(schema, num2);
						if (testType(columnInfo2.Type))
						{
							return columnInfo2;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000460C File Offset: 0x0000280C
		public static bool IsScoreColumnKind(IExceptionContext ectx, ISchema schema, int col, string kind)
		{
			Contracts.CheckValue<ISchema>(ectx, schema, "schema");
			Contracts.CheckParam(ectx, 0 <= col && col < schema.ColumnCount, "col");
			Contracts.CheckNonEmpty(ectx, kind, "kind");
			ColumnType metadataTypeOrNull = schema.GetMetadataTypeOrNull("ScoreColumnKind", col);
			if (metadataTypeOrNull == null || !metadataTypeOrNull.IsText)
			{
				return false;
			}
			DvText dvText = default(DvText);
			schema.GetMetadata<DvText>("ScoreColumnKind", col, ref dvText);
			return dvText.EqualsStr(kind);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004685 File Offset: 0x00002885
		public static string GetColName(string str, ColumnInfo info, string def)
		{
			if (!string.IsNullOrEmpty(str))
			{
				return str;
			}
			if (info != null)
			{
				return info.Name;
			}
			return def;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000469C File Offset: 0x0000289C
		public static void CheckWeightType(IExceptionContext ectx, ColumnType type)
		{
			if (type != NumberType.Float)
			{
				throw Contracts.ExceptUserArg(ectx, "Weight", "Incompatible Weight column. Weight column type must be {0}.", new object[] { NumberType.Float });
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004E5C File Offset: 0x0000305C
		public static IEnumerable<KeyValuePair<string, double>> GetMetrics(IDataView metricsView, bool getVectorMetrics = true)
		{
			Contracts.CheckValue<IDataView>(metricsView, "metricsView");
			ISchema schema = metricsView.Schema;
			int isWeightedCol;
			bool hasWeighted = schema.TryGetColumnIndex("IsWeighted", ref isWeightedCol);
			int stratVal = -1;
			int stratCol;
			bool hasStrats;
			if ((hasStrats = schema.TryGetColumnIndex("StratCol", ref stratCol)) && !schema.TryGetColumnIndex("StratVal", ref stratVal))
			{
				throw Contracts.Except("If data contains a '{0}' column, it must also contain a '{1}' column", new object[] { "StratCol", "StratVal" });
			}
			using (IRowCursor cursor = metricsView.GetRowCursor((int col) => true, null))
			{
				DvBool isWeighted = DvBool.False;
				ValueGetter<DvBool> isWeightedGetter;
				if (hasWeighted)
				{
					isWeightedGetter = cursor.GetGetter<DvBool>(isWeightedCol);
				}
				else
				{
					isWeightedGetter = delegate(ref DvBool dst)
					{
						dst = DvBool.False;
					};
				}
				ValueGetter<uint> stratColGetter;
				if (hasStrats)
				{
					ColumnType columnType = cursor.Schema.GetColumnType(stratCol);
					stratColGetter = RowCursorUtils.GetGetterAs<uint>(columnType, cursor, stratCol);
				}
				else
				{
					stratColGetter = delegate(ref uint dst)
					{
						dst = 0U;
					};
				}
				int colCount = schema.ColumnCount;
				ValueGetter<double>[] getters = new ValueGetter<double>[colCount];
				ValueGetter<VBuffer<double>>[] vBufferGetters = (getVectorMetrics ? new ValueGetter<VBuffer<double>>[colCount] : null);
				for (int j = 0; j < schema.ColumnCount; j++)
				{
					if (!MetadataUtils.IsHidden(schema, j) && (!hasWeighted || j != isWeightedCol) && (!hasStrats || (j != stratCol && j != stratVal)))
					{
						ColumnType columnType2 = schema.GetColumnType(j);
						if (columnType2 == NumberType.R8 || columnType2 == NumberType.R4)
						{
							getters[j] = RowCursorUtils.GetGetterAs<double>(NumberType.R8, cursor, j);
						}
						else if (columnType2.IsKnownSizeVector && columnType2.ItemType == NumberType.R8 && getVectorMetrics)
						{
							vBufferGetters[j] = cursor.GetGetter<VBuffer<double>>(j);
						}
					}
				}
				double metricVal = 0.0;
				VBuffer<double> metricVals = default(VBuffer<double>);
				uint strat = 0U;
				bool foundRow = false;
				while (cursor.MoveNext())
				{
					isWeightedGetter.Invoke(ref isWeighted);
					if (!isWeighted.IsTrue)
					{
						stratColGetter.Invoke(ref strat);
						if (strat <= 0U)
						{
							Contracts.Check(!foundRow, "Multiple metric rows found in metrics data view.");
							foundRow = true;
							for (int i = 0; i < colCount; i++)
							{
								if ((!hasWeighted || i != isWeightedCol) && (!hasStrats || (i != stratCol && i != stratVal)))
								{
									if (getters[i] != null)
									{
										getters[i].Invoke(ref metricVal);
										yield return new KeyValuePair<string, double>(schema.GetColumnName(i), metricVal);
									}
									else if (getVectorMetrics && vBufferGetters[i] != null)
									{
										vBufferGetters[i].Invoke(ref metricVals);
										VBuffer<DvText> names = default(VBuffer<DvText>);
										int size = schema.GetColumnType(i).VectorSize;
										ColumnType slotNamesType = schema.GetMetadataTypeOrNull("SlotNames", i);
										if (slotNamesType != null && slotNamesType.VectorSize == size && slotNamesType.ItemType.IsText)
										{
											schema.GetMetadata<VBuffer<DvText>>("SlotNames", i, ref names);
										}
										else
										{
											DvText[] array = new DvText[size];
											for (int k = 0; k < size; k++)
											{
												array[k] = new DvText(string.Format("({0})", k));
											}
											names = new VBuffer<DvText>(size, array, null);
										}
										string colName = schema.GetColumnName(i);
										foreach (KeyValuePair<int, double> metric in metricVals.Items(true))
										{
											string text = "{0} {1}";
											object obj = colName;
											KeyValuePair<int, double> keyValuePair = metric;
											string text2 = string.Format(text, obj, names.GetItemOrDefault(keyValuePair.Key));
											KeyValuePair<int, double> keyValuePair2 = metric;
											yield return new KeyValuePair<string, double>(text2, keyValuePair2.Value);
										}
									}
								}
							}
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004E9C File Offset: 0x0000309C
		public static IDataView AddTextColumn<TSrc>(IHostEnvironment env, IDataView input, string inputColName, string outputColName, ColumnType typeSrc, string value, string registrationName)
		{
			Contracts.Check(typeSrc.RawType == typeof(TSrc));
			return LambdaColumnMapper.Create<TSrc, DvText>(env, registrationName, input, inputColName, outputColName, typeSrc, TextType.Instance, delegate(ref TSrc src, ref DvText dst)
			{
				dst = new DvText(value);
			}, null);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004F20 File Offset: 0x00003120
		public static IDataView AddKeyColumn<TSrc>(IHostEnvironment env, IDataView input, string inputColName, string outputColName, ColumnType typeSrc, int keyCount, int value, string registrationName, ValueGetter<VBuffer<DvText>> keyValueGetter)
		{
			Contracts.Check(typeSrc.RawType == typeof(TSrc));
			return LambdaColumnMapper.Create<TSrc, uint>(env, registrationName, input, inputColName, outputColName, typeSrc, new KeyType(6, 0UL, keyCount, true), delegate(ref TSrc src, ref uint dst)
			{
				if (value < 0 || value > keyCount)
				{
					dst = 0U;
					return;
				}
				dst = (uint)value;
			}, keyValueGetter);
		}

		// Token: 0x0200001E RID: 30
		private static class DefaultEvaluatorTable
		{
			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000098 RID: 152 RVA: 0x00004F88 File Offset: 0x00003188
			public static Dictionary<string, string> Instance
			{
				get
				{
					if (EvaluateUtils.DefaultEvaluatorTable._knownEvaluatorLoadNames == null)
					{
						Interlocked.CompareExchange<Dictionary<string, string>>(ref EvaluateUtils.DefaultEvaluatorTable._knownEvaluatorLoadNames, new Dictionary<string, string>
						{
							{ "BinaryClassification", "BinaryClassifierEvaluator" },
							{ "MultiClassClassification", "MultiClassClassifierEvaluator" },
							{ "Regression", "RegressionEvaluator" },
							{ "MultiOutputRegression", "MultiRegressionEvaluator" },
							{ "QuantileRegression", "QuantileRegressionEvaluator" },
							{ "Ranking", "RankingEvaluator" },
							{ "Clustering", "ClusteringEvaluator" },
							{ "AnomalyDetection", "AnomalyDetectionEvaluator" },
							{ "SequenceClassification", "SequenceClassifierEvaluator" }
						}, null);
					}
					return EvaluateUtils.DefaultEvaluatorTable._knownEvaluatorLoadNames;
				}
			}

			// Token: 0x04000047 RID: 71
			private static volatile Dictionary<string, string> _knownEvaluatorLoadNames;
		}
	}
}
