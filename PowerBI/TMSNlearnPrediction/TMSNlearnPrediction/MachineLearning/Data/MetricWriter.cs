using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.Data.IO;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200001F RID: 31
	public static class MetricWriter
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00005078 File Offset: 0x00003278
		public static string GetConfusionTable(IHost host, IDataView confusionDataView, out string weightedConfusionTable, bool binary = true, int sample = -1)
		{
			Contracts.CheckValue<IDataView>(host, confusionDataView, "confusionDataView");
			Contracts.CheckParam(host, sample == -1 || sample >= 2, "sample", "Should be -1 to indicate no sampling, or at least 2");
			int num;
			Contracts.Check(host, confusionDataView.Schema.TryGetColumnIndex("Count", ref num), "Did not find the count column");
			ColumnType metadataTypeOrNull = confusionDataView.Schema.GetMetadataTypeOrNull("SlotNames", num);
			Contracts.Check(host, metadataTypeOrNull != null && metadataTypeOrNull.IsKnownSizeVector && metadataTypeOrNull.ItemType.IsText, "The Count column does not have a text vector metadata of kind SlotNames.");
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			confusionDataView.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", num, ref vbuffer);
			Contracts.Check(host, vbuffer.IsDense, "Slot names vector must be dense");
			int num2 = ((sample < 0) ? vbuffer.Length : Math.Min(vbuffer.Length, sample));
			int[] labelIndexToConfIndexMap = new int[vbuffer.Length];
			if (num2 < vbuffer.Length)
			{
				int[] randomPermutation = Utils.GetRandomPermutation(host.Rand, vbuffer.Length);
				IOrderedEnumerable<int> orderedEnumerable = from i in randomPermutation.Skip(vbuffer.Length - num2)
					orderby i
					select i;
				for (int k = 0; k < labelIndexToConfIndexMap.Length; k++)
				{
					labelIndexToConfIndexMap[k] = -1;
				}
				int num3 = 0;
				using (IEnumerator<int> enumerator = orderedEnumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num4 = enumerator.Current;
						labelIndexToConfIndexMap[num4] = num3++;
					}
					goto IL_01BF;
				}
			}
			for (int j = 0; j < vbuffer.Length; j++)
			{
				labelIndexToConfIndexMap[j] = j;
			}
			IL_01BF:
			double[] array2;
			double[] array3;
			double[][] array = MetricWriter.GetConfusionTableAsArray(confusionDataView, num, vbuffer.Length, labelIndexToConfIndexMap, num2, out array2, out array3);
			string confusionTableAsString = MetricWriter.GetConfusionTableAsString(array, array3, array2, vbuffer.Values.Where((DvText t, int i) => labelIndexToConfIndexMap[i] >= 0).ToArray<DvText>(), "", num2 < vbuffer.Count, binary);
			int num5;
			if (confusionDataView.Schema.TryGetColumnIndex("Weight", ref num5))
			{
				array = MetricWriter.GetConfusionTableAsArray(confusionDataView, num5, vbuffer.Length, labelIndexToConfIndexMap, num2, out array2, out array3);
				double[][] array4 = array;
				double[] array5 = array3;
				double[] array6 = array2;
				DvText[] array7 = vbuffer.Values.Where((DvText t, int i) => labelIndexToConfIndexMap[i] >= 0).ToArray<DvText>();
				bool flag = num2 < vbuffer.Count;
				weightedConfusionTable = MetricWriter.GetConfusionTableAsString(array4, array5, array6, array7, "Weighted ", flag, binary);
			}
			else
			{
				weightedConfusionTable = null;
			}
			return confusionTableAsString;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005354 File Offset: 0x00003554
		private static double[][] GetConfusionTableAsArray(IDataView confusionDataView, int countIndex, int numClasses, int[] labelIndexToConfIndexMap, int numConfusionTableLabels, out double[] precisionSums, out double[] recallSums)
		{
			double[][] array = new double[numConfusionTableLabels][];
			for (int i = 0; i < numConfusionTableLabels; i++)
			{
				array[i] = new double[numConfusionTableLabels];
			}
			precisionSums = new double[numConfusionTableLabels];
			recallSums = new double[numConfusionTableLabels];
			int stratCol;
			bool hasStrat = confusionDataView.Schema.TryGetColumnIndex("StratCol", ref stratCol);
			using (IRowCursor rowCursor = confusionDataView.GetRowCursor((int col) => col == countIndex || (hasStrat && col == stratCol), null))
			{
				ColumnType columnType = rowCursor.Schema.GetColumnType(countIndex);
				Contracts.Check(columnType.IsKnownSizeVector && columnType.ItemType == NumberType.R8);
				ValueGetter<VBuffer<double>> getter = rowCursor.GetGetter<VBuffer<double>>(countIndex);
				ValueGetter<uint> valueGetter = null;
				if (hasStrat)
				{
					columnType = rowCursor.Schema.GetColumnType(stratCol);
					valueGetter = RowCursorUtils.GetGetterAs<uint>(columnType, rowCursor, stratCol);
				}
				VBuffer<double> vbuffer = default(VBuffer<double>);
				int num = -1;
				while (rowCursor.MoveNext())
				{
					uint num2 = 0U;
					if (valueGetter != null)
					{
						valueGetter.Invoke(ref num2);
					}
					if (num2 <= 0U)
					{
						num++;
						if (labelIndexToConfIndexMap[num] >= 0)
						{
							getter.Invoke(ref vbuffer);
							if (vbuffer.Length != numClasses)
							{
								throw Contracts.Except("Expected {0} values in 'Count' column, but got {1}.", new object[] { numClasses, vbuffer.Length });
							}
							int num3 = labelIndexToConfIndexMap[num];
							foreach (KeyValuePair<int, double> keyValuePair in vbuffer.Items(false))
							{
								int key = keyValuePair.Key;
								if (labelIndexToConfIndexMap[key] >= 0)
								{
									array[num3][labelIndexToConfIndexMap[key]] = keyValuePair.Value;
									precisionSums[labelIndexToConfIndexMap[key]] += keyValuePair.Value;
									recallSums[num3] += keyValuePair.Value;
								}
							}
						}
					}
				}
			}
			return array;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000055A8 File Offset: 0x000037A8
		public static string GetPerFoldResults(IHostEnvironment env, IDataView fold, out string weightedMetrics)
		{
			int num;
			IDataView dataView;
			int num2;
			if (fold.Schema.TryGetColumnIndex("IsWeighted", ref num))
			{
				weightedMetrics = MetricWriter.GetMetricsAsString(env, fold, false, true, out dataView, out num2);
			}
			else
			{
				weightedMetrics = null;
			}
			return MetricWriter.GetMetricsAsString(env, fold, false, false, out dataView, out num2);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005698 File Offset: 0x00003898
		private static string GetMetricsAsString(IHostEnvironment env, IDataView data, bool average, bool weighted, out IDataView avgMetricsDataView, out int numResults)
		{
			int isWeightedCol;
			bool hasWeighted = data.Schema.TryGetColumnIndex("IsWeighted", ref isWeightedCol);
			int stratCol;
			bool hasStrats = data.Schema.TryGetColumnIndex("StratCol", ref stratCol);
			int stratVal;
			bool flag = data.Schema.TryGetColumnIndex("StratVal", ref stratVal);
			int foldCol;
			bool hasFoldCol = data.Schema.TryGetColumnIndex("Fold Index", ref foldCol);
			int columnCount = data.Schema.ColumnCount;
			ValueGetter<double>[] array = new ValueGetter<double>[columnCount];
			ValueGetter<VBuffer<double>>[] array2 = new ValueGetter<VBuffer<double>>[columnCount];
			numResults = 0;
			List<string> metricNames;
			double[] array3;
			double[] array4;
			using (IRowCursor rowCursor = data.GetRowCursor((int col) => true, null))
			{
				DvBool @false = DvBool.False;
				ValueGetter<DvBool> valueGetter;
				if (hasWeighted)
				{
					valueGetter = rowCursor.GetGetter<DvBool>(isWeightedCol);
				}
				else
				{
					if (MetricWriter.CS$<>9__CachedAnonymousMethodDelegate12 == null)
					{
						MetricWriter.CS$<>9__CachedAnonymousMethodDelegate12 = delegate(ref DvBool dst)
						{
							dst = DvBool.False;
						};
					}
					valueGetter = MetricWriter.CS$<>9__CachedAnonymousMethodDelegate12;
				}
				ValueGetter<uint> valueGetter2;
				if (hasStrats)
				{
					ColumnType columnType = rowCursor.Schema.GetColumnType(stratCol);
					valueGetter2 = RowCursorUtils.GetGetterAs<uint>(columnType, rowCursor, stratCol);
				}
				else
				{
					valueGetter2 = delegate(ref uint dst)
					{
						dst = 0U;
					};
				}
				metricNames = MetricWriter.GetMetricNames(data.Schema, rowCursor, (int i) => (hasWeighted && i == isWeightedCol) || (hasStrats && (i == stratCol || i == stratVal)) || (hasFoldCol && i == foldCol), array, array2);
				double num = 0.0;
				VBuffer<double> vbuffer = default(VBuffer<double>);
				array3 = new double[metricNames.Count];
				array4 = new double[metricNames.Count];
				uint num2 = 0U;
				while (rowCursor.MoveNext())
				{
					valueGetter.Invoke(ref @false);
					if (@false.IsTrue == weighted)
					{
						valueGetter2.Invoke(ref num2);
						if (num2 <= 0U)
						{
							if (!average && numResults > 0)
							{
								throw Contracts.Except("Multiple {0} rows found in metrics data view.", new object[] { weighted ? "weighted" : "unweighted" });
							}
							numResults++;
							int num3 = 0;
							for (int l = 0; l < columnCount; l++)
							{
								if ((!hasWeighted || l != isWeightedCol) && (!hasStrats || (l != stratCol && l != stratVal)))
								{
									if (array[l] != null)
									{
										array[l].Invoke(ref num);
										array3[num3] += num;
										if (array4 != null)
										{
											array4[num3] += num * num;
										}
										num3++;
									}
									else if (array2[l] != null)
									{
										array2[l].Invoke(ref vbuffer);
										foreach (KeyValuePair<int, double> keyValuePair in vbuffer.Items(true))
										{
											array3[num3] += keyValuePair.Value;
											if (array4 != null)
											{
												array4[num3] += keyValuePair.Value * keyValuePair.Value;
											}
											num3++;
										}
									}
								}
							}
						}
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int j = 0; j < metricNames.Count; j++)
			{
				array3[j] /= (double)numResults;
				stringBuilder.Append(string.Format("{0}{1}: ", weighted ? "Weighted " : "", metricNames[j]).PadRight(20));
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0,7:N6}", new object[] { array3[j] }));
				if (average)
				{
					stringBuilder.AppendLine(string.Format(" ({0:N4})", (numResults == 1) ? 0.0 : Math.Sqrt(array4[j] / (double)numResults - array3[j] * array3[j])));
				}
				else
				{
					stringBuilder.AppendLine();
				}
			}
			if (average)
			{
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(env);
				int num4 = 0;
				for (int k = 0; k < columnCount; k++)
				{
					if (MetadataUtils.IsHidden(data.Schema, k))
					{
						ColumnType columnType2 = data.Schema.GetColumnType(k);
						if (columnType2.IsKnownSizeVector)
						{
							Action<ArrayDataViewBuilder, ISchema, int, ColumnType> action = new Action<ArrayDataViewBuilder, ISchema, int, ColumnType>(MetricWriter.AddDummyVectorColumn<int>);
							MethodInfo methodInfo = action.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType2.ItemType.RawType });
							methodInfo.Invoke(null, new object[] { arrayDataViewBuilder, data.Schema, k, columnType2 });
						}
						else
						{
							Action<ArrayDataViewBuilder, string, PrimitiveType> action2 = new Action<ArrayDataViewBuilder, string, PrimitiveType>(MetricWriter.AddDummyScalarColumn<int>);
							MethodInfo methodInfo2 = action2.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType2.ItemType.RawType });
							methodInfo2.Invoke(null, new object[]
							{
								arrayDataViewBuilder,
								data.Schema.GetColumnName(k),
								columnType2.AsPrimitive
							});
						}
					}
					if (hasStrats && k == stratCol)
					{
						ColumnType columnType3 = data.Schema.GetColumnType(k);
						VBuffer<DvText> vbuffer2 = default(VBuffer<DvText>);
						ColumnType metadataTypeOrNull = data.Schema.GetMetadataTypeOrNull("KeyValues", k);
						if (metadataTypeOrNull == null || !metadataTypeOrNull.ItemType.IsText || metadataTypeOrNull.VectorSize != columnType3.KeyCount)
						{
							throw Contracts.Except(env, "Column '{0}' must have key values metadata", new object[] { "StratCol" });
						}
						data.Schema.GetMetadata<VBuffer<DvText>>("KeyValues", k, ref vbuffer2);
						ArrayDataViewBuilder arrayDataViewBuilder2 = arrayDataViewBuilder;
						string text = "StratCol";
						ulong num5 = 0UL;
						int keyCount = columnType3.KeyCount;
						uint[] array5 = new uint[1];
						arrayDataViewBuilder2.AddColumn(text, ref vbuffer2, num5, keyCount, array5);
					}
					else if (flag && k == stratVal)
					{
						arrayDataViewBuilder.AddColumn<DvText>("StratVal", TextType.Instance, new DvText[] { DvText.NA });
					}
					else if (hasWeighted && k == isWeightedCol)
					{
						arrayDataViewBuilder.AddColumn<DvBool>("IsWeighted", BoolType.Instance, new DvBool[] { weighted ? DvBool.True : DvBool.False });
					}
					else if (hasFoldCol && k == foldCol)
					{
						DvText dvText;
						dvText..ctor("Average");
						arrayDataViewBuilder.AddColumn<DvText>("Fold Index", TextType.Instance, new DvText[] { dvText });
					}
					else if (array[k] != null)
					{
						arrayDataViewBuilder.AddColumn<double>(data.Schema.GetColumnName(k), NumberType.R8, new double[] { array3[num4] });
						num4++;
					}
					else if (array2[k] != null)
					{
						Func<int, DvText> func = null;
						ColumnType columnType4 = data.Schema.GetColumnType(k);
						double[] array6 = new double[columnType4.VectorSize];
						Array.Copy(array3, num4, array6, 0, array6.Length);
						VBuffer<DvText> vbuffer3 = default(VBuffer<DvText>);
						ColumnType metadataTypeOrNull2 = data.Schema.GetMetadataTypeOrNull("SlotNames", k);
						string name = data.Schema.GetColumnName(k);
						if (metadataTypeOrNull2 != null && metadataTypeOrNull2.ItemType.IsText && metadataTypeOrNull2.VectorSize == columnType4.VectorSize)
						{
							data.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", k, ref vbuffer3);
							vbuffer3 = new VBuffer<DvText>(vbuffer3.Length, vbuffer3.Values.Select((DvText slotName) => new DvText(name + slotName.ToString())).ToArray<DvText>(), null);
						}
						else
						{
							int vectorSize = columnType4.VectorSize;
							IEnumerable<int> enumerable = Enumerable.Range(0, columnType4.VectorSize);
							if (func == null)
							{
								func = (int slot) => new DvText(name + slot.ToString());
							}
							vbuffer3 = new VBuffer<DvText>(vectorSize, enumerable.Select(func).ToArray<DvText>(), null);
						}
						arrayDataViewBuilder.AddColumn<double>(name, ref vbuffer3, NumberType.R8, new double[][] { array6 });
						num4 += array6.Length;
					}
				}
				avgMetricsDataView = arrayDataViewBuilder.GetDataView(null);
			}
			else
			{
				avgMetricsDataView = null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005FB0 File Offset: 0x000041B0
		private static List<string> GetMetricNames(ISchema schema, IRow row, Func<int, bool> ignoreCol, ValueGetter<double>[] getters, ValueGetter<VBuffer<double>>[] vBufferGetters)
		{
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			int num = 0;
			List<string> list = new List<string>();
			for (int i = 0; i < schema.ColumnCount; i++)
			{
				if (!MetadataUtils.IsHidden(schema, i) && !ignoreCol(i))
				{
					ColumnType columnType = schema.GetColumnType(i);
					if (columnType.IsNumber)
					{
						getters[i] = RowCursorUtils.GetGetterAs<double>(NumberType.R8, row, i);
						list.Add(row.Schema.GetColumnName(i));
						num++;
					}
					else if (columnType.IsKnownSizeVector && columnType.ItemType == NumberType.R8)
					{
						vBufferGetters[i] = row.GetGetter<VBuffer<double>>(i);
						num += columnType.VectorSize;
						ColumnType metadataTypeOrNull = schema.GetMetadataTypeOrNull("SlotNames", i);
						if (metadataTypeOrNull != null && metadataTypeOrNull.VectorSize == columnType.VectorSize && metadataTypeOrNull.ItemType.IsText)
						{
							schema.GetMetadata<VBuffer<DvText>>("SlotNames", i, ref vbuffer);
						}
						else
						{
							DvText[] array = vbuffer.Values;
							if (Utils.Size<DvText>(array) < columnType.VectorSize)
							{
								array = new DvText[columnType.VectorSize];
							}
							for (int j = 0; j < columnType.VectorSize; j++)
							{
								array[j] = new DvText(string.Format("Label_{0}", j));
							}
							vbuffer = new VBuffer<DvText>(columnType.VectorSize, array, null);
						}
						foreach (KeyValuePair<int, DvText> keyValuePair in vbuffer.Items(true))
						{
							list.Add(string.Format("{0} {1}", row.Schema.GetColumnName(i), keyValuePair.Value));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000618C File Offset: 0x0000438C
		private static void AddDummyVectorColumn<T>(ArrayDataViewBuilder dvBldr, ISchema schema, int col, ColumnType type)
		{
			string columnName = schema.GetColumnName(col);
			dvBldr.AddColumn<T>(columnName, type.ItemType.AsPrimitive, new T[][] { new T[type.VectorSize] });
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000061CC File Offset: 0x000043CC
		private static void AddDummyScalarColumn<T>(ArrayDataViewBuilder dvBldr, string name, PrimitiveType type)
		{
			dvBldr.AddColumn<T>(name, type, new T[] { default(T) });
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000061FC File Offset: 0x000043FC
		private static string GetConfusionTableAsString(double[][] confusionTable, double[] rowSums, double[] columnSums, DvText[] predictedLabelNames, string prefix = "", bool sampled = false, bool binary = true)
		{
			int num = Utils.Size<double[]>(confusionTable);
			string[] array = new string[predictedLabelNames.Length];
			string[] array2 = new string[predictedLabelNames.Length];
			for (int i = 0; i < num; i++)
			{
				array[i] = MetricWriter.PadLeft(predictedLabelNames[i].ToString(), (num == 2) ? 8 : 5);
			}
			for (int j = 0; j < num; j++)
			{
				array2[j] = MetricWriter.PadLeft(predictedLabelNames[j].ToString(), (num == 2) ? 8 : 5);
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (num == 2 && binary)
			{
				string text = array[0].ToUpper();
				double num2 = confusionTable[0][0];
				double num3 = confusionTable[0][1];
				double num4 = confusionTable[1][1];
				double num5 = confusionTable[1][0];
				stringBuilder.AppendFormat("{0}TEST {1} RATIO:\t{2:N4} ({3:F1}/({3:F1}+{4:F1}))", new object[]
				{
					prefix,
					text,
					1.0 * (num2 + num3) / (num2 + num4 + num3 + num5),
					num2 + num3,
					num5 + num4
				});
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("{0}Confusion table", prefix);
			if (sampled)
			{
				stringBuilder.AppendLine(" (sampled)");
			}
			else
			{
				stringBuilder.AppendLine();
			}
			stringBuilder.Append("          ||");
			for (int k = 0; k < num; k++)
			{
				stringBuilder.Append((num > 2) ? "========" : "===========");
			}
			stringBuilder.AppendLine();
			stringBuilder.Append("PREDICTED ||");
			string text2 = string.Format(" {{0,{0}}} |", (num == 2) ? 8 : 5);
			for (int l = 0; l < num; l++)
			{
				stringBuilder.AppendFormat(text2, predictedLabelNames[l]);
			}
			stringBuilder.AppendLine(" Recall");
			stringBuilder.Append("TRUTH     ||");
			for (int m = 0; m < num; m++)
			{
				stringBuilder.Append((num > 2) ? "========" : "===========");
			}
			stringBuilder.AppendLine();
			string text3 = string.Format(" {{0,{0}:{1}}} |", (num == 2) ? 8 : 5, string.IsNullOrWhiteSpace(prefix) ? "N0" : "F1");
			for (int n = 0; n < num; n++)
			{
				stringBuilder.AppendFormat("{0,9} ||", predictedLabelNames[n]);
				for (int num6 = 0; num6 < num; num6++)
				{
					stringBuilder.AppendFormat(text3, confusionTable[n][num6]);
				}
				double num7 = ((rowSums[n] > 0.0) ? (confusionTable[n][n] / rowSums[n]) : 0.0);
				stringBuilder.AppendFormat(" {0,5:F4}", num7);
				stringBuilder.AppendLine();
			}
			stringBuilder.Append("          ||");
			for (int num8 = 0; num8 < num; num8++)
			{
				stringBuilder.Append((num > 2) ? "========" : "===========");
			}
			stringBuilder.AppendLine();
			stringBuilder.Append("Precision ||");
			text2 = string.Format("{{0,{0}:N4}} |", (num == 2) ? 9 : 6);
			for (int num9 = 0; num9 < num; num9++)
			{
				double num10 = ((columnSums[num9] > 0.0) ? (confusionTable[num9][num9] / columnSums[num9]) : 0.0);
				stringBuilder.AppendFormat(text2, num10);
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000065B8 File Offset: 0x000047B8
		public static void PrintOverallMetrics(IHostEnvironment env, IChannel ch, string filename, IDataView overall)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("OVERALL RESULTS");
			stringBuilder.AppendLine("---------------------------------------");
			IDataView dataView = null;
			int num = 0;
			int num2;
			if (overall.Schema.TryGetColumnIndex("IsWeighted", ref num2))
			{
				stringBuilder.Append(MetricWriter.GetMetricsAsString(env, overall, true, true, out dataView, out num));
			}
			IDataView dataView2;
			int num3;
			stringBuilder.AppendLine(MetricWriter.GetMetricsAsString(env, overall, true, false, out dataView2, out num3));
			stringBuilder.AppendLine("---------------------------------------");
			ch.Info(stringBuilder.ToString());
			if (!string.IsNullOrEmpty(filename))
			{
				using (IFileHandle fileHandle = env.CreateOutputFile(filename))
				{
					List<IDataView> list = new List<IDataView> { dataView2 };
					if (dataView != null)
					{
						list.Add(dataView);
					}
					int num4;
					bool flag = overall.Schema.TryGetColumnIndex("StratCol", ref num4);
					if (num3 > 1 || flag)
					{
						list.Add(overall);
					}
					IDataView dataView3 = AppendRowsDataView.Create(env, dataView2.Schema, list.ToArray());
					if (flag)
					{
						dataView3 = new KeyToValueTransform(new KeyToValueTransform.Arguments
						{
							column = new KeyToValueTransform.Column[]
							{
								new KeyToValueTransform.Column
								{
									source = "StratCol"
								}
							}
						}, env, dataView3);
					}
					TextSaver.Arguments arguments = new TextSaver.Arguments
					{
						dense = true,
						silent = true
					};
					DataSaverUtils.SaveDataView(ch, new TextSaver(arguments, env), dataView3, fileHandle, false);
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006740 File Offset: 0x00004940
		private static string PadLeft(string str, int totalLength)
		{
			if (str.Length > totalLength)
			{
				return str.Substring(0, totalLength - 1).PadRight(totalLength, '.');
			}
			return str.PadLeft(totalLength);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006778 File Offset: 0x00004978
		public static void PrintWarnings(IChannel ch, Dictionary<string, IDataView> metrics)
		{
			IDataView dataView;
			if (metrics.TryGetValue("Warnings", out dataView))
			{
				int col;
				if (dataView.Schema.TryGetColumnIndex("WarningText", ref col) && dataView.Schema.GetColumnType(col).IsText)
				{
					using (IRowCursor rowCursor = dataView.GetRowCursor((int c) => c == col, null))
					{
						DvText dvText = default(DvText);
						ValueGetter<DvText> getter = rowCursor.GetGetter<DvText>(col);
						while (rowCursor.MoveNext())
						{
							getter.Invoke(ref dvText);
							ch.Warning(dvText.ToString());
						}
					}
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006848 File Offset: 0x00004A48
		public static void SavePerInstance(IHostEnvironment env, IChannel ch, string filename, IDataView data, bool dense = true, bool saveSchema = true)
		{
			using (IFileHandle fileHandle = env.CreateOutputFile(filename))
			{
				DataSaverUtils.SaveDataView(ch, new TextSaver(new TextSaver.Arguments
				{
					outputSchema = saveSchema,
					dense = dense,
					silent = true
				}, env), data, fileHandle, false);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000068A8 File Offset: 0x00004AA8
		public static IDataView GetNonStratifiedMetrics(IHostEnvironment env, IDataView data)
		{
			int num;
			if (!data.Schema.TryGetColumnIndex("StratCol", ref num))
			{
				return data;
			}
			ColumnType columnType = data.Schema.GetColumnType(num);
			Contracts.Check(env, columnType.KeyCount > 0, "Expected a known count key type stratification column");
			data = new MissingValueFilter(new MissingValueFilter.Arguments
			{
				column = new string[] { "StratCol" },
				complement = true
			}, env, data);
			int num2;
			bool flag = data.Schema.TryGetColumnIndex("StratVal", ref num2);
			Contracts.Check(env, flag, "If stratification column exist, data view must also contain a StratVal column");
			data = new DropColumnsTransform(new DropColumnsTransform.Arguments
			{
				column = new string[]
				{
					data.Schema.GetColumnName(num),
					data.Schema.GetColumnName(num2)
				}
			}, env, data);
			return data;
		}
	}
}
