using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200011B RID: 283
	public static class MutualInformationFeatureSelectionUtils
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x0001F0B0 File Offset: 0x0001D2B0
		public static float[][] Train(IHost host, IDataView input, string labelColumnName, string[] columns, int numBins)
		{
			Contracts.CheckValue<IHost>(host, "host");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckNonWhiteSpace(host, labelColumnName, "labelColumnName");
			Contracts.CheckValue<string[]>(host, columns, "columns");
			Contracts.Check(host, columns.Length > 0, "At least one column must be specified.");
			Contracts.Check(host, numBins > 1, "numBins must be greater than 1.");
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string text in columns)
			{
				if (!hashSet.Add(text))
				{
					throw Contracts.Except(host, "Column '{0}' specified multiple times.", new object[] { text });
				}
			}
			int[] array = new int[columns.Length];
			return MutualInformationFeatureSelectionUtils.TrainCore(host, input, labelColumnName, columns, numBins, array);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001F168 File Offset: 0x0001D368
		internal static float[][] TrainCore(IHost host, IDataView input, string labelColumnName, string[] columns, int numBins, int[] colSizes)
		{
			MutualInformationFeatureSelectionUtils.Impl impl = new MutualInformationFeatureSelectionUtils.Impl(host);
			return impl.GetScores(input, labelColumnName, columns, numBins, colSizes);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001F189 File Offset: 0x0001D389
		private static ValueMapper<VBuffer<TSrc>, VBuffer<TDst>> CreateVectorMapper<TSrc, TDst>(ValueMapper<TSrc, TDst> map) where TDst : IEquatable<TDst>
		{
			return new ValueMapper<VBuffer<TSrc>, VBuffer<TDst>>(map.MapVector<TSrc, TDst>);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001F198 File Offset: 0x0001D398
		private static void MapVector<TSrc, TDst>(this ValueMapper<TSrc, TDst> map, ref VBuffer<TSrc> input, ref VBuffer<TDst> output)
		{
			TDst[] array = output.Values;
			if (Utils.Size<TDst>(array) < input.Count)
			{
				array = new TDst[input.Count];
			}
			for (int i = 0; i < input.Count; i++)
			{
				TSrc tsrc = input.Values[i];
				map.Invoke(ref tsrc, ref array[i]);
			}
			int[] array2 = output.Indices;
			if (!input.IsDense && input.Count > 0)
			{
				if (Utils.Size<int>(array2) < input.Count)
				{
					array2 = new int[input.Count];
				}
				Array.Copy(input.Indices, array2, input.Count);
			}
			output = new VBuffer<TDst>(input.Length, input.Count, array, array2);
		}

		// Token: 0x0200011C RID: 284
		private sealed class Impl
		{
			// Token: 0x060005B2 RID: 1458 RVA: 0x0001F250 File Offset: 0x0001D450
			public Impl(IHost host)
			{
				this._host = host;
				this._binFinder = new GreedyBinFinder();
				this._singles = new List<float>();
				this._doubles = new List<double>();
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x0001F2AC File Offset: 0x0001D4AC
			public float[][] GetScores(IDataView input, string labelColumnName, string[] columns, int numBins, int[] colSizes)
			{
				this._numBins = numBins;
				ISchema schema = input.Schema;
				int size = columns.Length;
				int num;
				if (!schema.TryGetColumnIndex(labelColumnName, ref num))
				{
					throw Contracts.ExceptUserArg(this._host, "label", "Label column '{0}' not found", new object[] { labelColumnName });
				}
				ColumnType columnType = schema.GetColumnType(num);
				if (!MutualInformationFeatureSelectionUtils.Impl.IsValidColumnType(columnType))
				{
					throw Contracts.ExceptUserArg(this._host, "column", "Label column '{0}' does not have compatible type.", new object[] { labelColumnName });
				}
				int[] array = new int[size + 1];
				array[size] = num;
				for (int j = 0; j < size; j++)
				{
					string text = columns[j];
					int num2;
					if (!schema.TryGetColumnIndex(text, ref num2))
					{
						throw Contracts.ExceptUserArg(this._host, "column", "Source column '{0}' not found", new object[] { text });
					}
					ColumnType columnType2 = schema.GetColumnType(num2);
					if (columnType2.IsVector && !columnType2.IsKnownSizeVector)
					{
						throw Contracts.ExceptUserArg(this._host, "column", "Variable length column '{0}' is not allowed", new object[] { text });
					}
					if (!MutualInformationFeatureSelectionUtils.Impl.IsValidColumnType(columnType2.ItemType))
					{
						throw Contracts.ExceptUserArg(this._host, "column", "Column '{0}' of type '{1}' does not have compatible type.", new object[] { text, columnType2 });
					}
					array[j] = num2;
					colSizes[j] = columnType2.VectorSize;
				}
				float[][] array2 = new float[size][];
				using (IChannel channel = this._host.Start("Computing mutual information scores"))
				{
					using (IProgressChannel progressChannel = this._host.StartProgressChannel("Computing mutual information scores"))
					{
						using (Transposer transposer = Transposer.Create(this._host, input, false, array))
						{
							int i = 0;
							ProgressHeader progressHeader = new ProgressHeader(new string[] { "columns" });
							transposer.Schema.TryGetColumnIndex(labelColumnName, ref num);
							this.GetLabels(transposer, columnType, num);
							this._contingencyTable = new int[this._numLabels][];
							this._labelSums = new int[this._numLabels];
							progressChannel.SetHeader(progressHeader, delegate(IProgressEntry e)
							{
								e.SetProgress(0, (double)i, (double)size);
							});
							for (i = 0; i < size; i++)
							{
								int num3;
								transposer.Schema.TryGetColumnIndex(columns[i], ref num3);
								channel.Trace("Computing scores for column '{0}'", new object[] { columns[i] });
								array2[i] = this.ComputeMutualInformation(transposer, num3);
								progressChannel.Checkpoint(new double?[]
								{
									new double?((double)(i + 1))
								});
							}
						}
						channel.Done();
					}
				}
				return array2;
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x0001F62C File Offset: 0x0001D82C
			private static bool IsValidColumnType(ColumnType type)
			{
				return (0 < type.KeyCount && type.KeyCount < 2146435071) || type.IsBool || type == NumberType.R4 || type == NumberType.R8;
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x0001F660 File Offset: 0x0001D860
			private void GetLabels(Transposer trans, ColumnType labelType, int labelCol)
			{
				VBuffer<int> vbuffer = default(VBuffer<int>);
				int num;
				if (labelType == NumberType.R4)
				{
					VBuffer<float> vbuffer2 = default(VBuffer<float>);
					trans.GetSingleSlotValue(labelCol, ref vbuffer2);
					int num2;
					this.BinSingles(ref vbuffer2, ref vbuffer, this._numBins, out num, out num2);
					this._numLabels = num2 - num;
				}
				else if (labelType == NumberType.R8)
				{
					VBuffer<double> vbuffer3 = default(VBuffer<double>);
					trans.GetSingleSlotValue(labelCol, ref vbuffer3);
					int num2;
					this.BinDoubles(ref vbuffer3, ref vbuffer, this._numBins, out num, out num2);
					this._numLabels = num2 - num;
				}
				else
				{
					if (!labelType.IsBool)
					{
						MutualInformationFeatureSelectionUtils.Impl.KeyLabelGetter<int> keyLabelGetter = new MutualInformationFeatureSelectionUtils.Impl.KeyLabelGetter<int>(this.GetKeyLabels<int>);
						MethodInfo methodInfo = keyLabelGetter.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { labelType.RawType });
						object[] array = new object[] { trans, labelCol, labelType };
						this._labels = (int[])methodInfo.Invoke(this, array);
						this._numLabels = labelType.KeyCount + 1;
						return;
					}
					VBuffer<DvBool> vbuffer4 = default(VBuffer<DvBool>);
					trans.GetSingleSlotValue(labelCol, ref vbuffer4);
					this.BinBools(ref vbuffer4, ref vbuffer);
					this._numLabels = 3;
					num = -1;
					int num2 = 2;
				}
				VBufferUtils.Densify<int>(ref vbuffer);
				this._labels = vbuffer.Values;
				if (vbuffer.Length < this._labels.Length)
				{
					Array.Resize<int>(ref this._labels, vbuffer.Length);
				}
				for (int i = 0; i < this._labels.Length; i++)
				{
					this._labels[i] -= num;
				}
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x0001F7FC File Offset: 0x0001D9FC
			private int[] GetKeyLabels<T>(Transposer trans, int labelCol, ColumnType labeColumnType)
			{
				VBuffer<T> vbuffer = default(VBuffer<T>);
				VBuffer<int> vbuffer2 = default(VBuffer<int>);
				trans.GetSingleSlotValue(labelCol, ref vbuffer);
				MutualInformationFeatureSelectionUtils.Impl.BinKeys<T>(labeColumnType).Invoke(ref vbuffer, ref vbuffer2);
				VBufferUtils.Densify<int>(ref vbuffer2);
				int[] values = vbuffer2.Values;
				if (vbuffer2.Length < values.Length)
				{
					Array.Resize<int>(ref values, vbuffer2.Length);
				}
				return values;
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x0001F894 File Offset: 0x0001DA94
			private float[] ComputeMutualInformation(Transposer trans, int col)
			{
				ColumnType columnType = trans.Schema.GetColumnType(col);
				if (columnType.ItemType == NumberType.R4)
				{
					return this.ComputeMutualInformation<float>(trans, col, delegate(ref VBuffer<float> src, ref VBuffer<int> dst, out int min, out int lim)
					{
						this.BinSingles(ref src, ref dst, this._numBins, out min, out lim);
					});
				}
				if (columnType.ItemType == NumberType.R8)
				{
					return this.ComputeMutualInformation<double>(trans, col, delegate(ref VBuffer<double> src, ref VBuffer<int> dst, out int min, out int lim)
					{
						this.BinDoubles(ref src, ref dst, this._numBins, out min, out lim);
					});
				}
				if (columnType.ItemType.IsBool)
				{
					return this.ComputeMutualInformation<DvBool>(trans, col, delegate(ref VBuffer<DvBool> src, ref VBuffer<int> dst, out int min, out int lim)
					{
						min = -1;
						lim = 2;
						this.BinBools(ref src, ref dst);
					});
				}
				Func<ColumnType, MutualInformationFeatureSelectionUtils.Impl.Mapper<int>> func = new Func<ColumnType, MutualInformationFeatureSelectionUtils.Impl.Mapper<int>>(MutualInformationFeatureSelectionUtils.Impl.MakeKeyMapper<int>);
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType.ItemType.RawType });
				MutualInformationFeatureSelectionUtils.Impl.ComputeMutualInformationDelegate<int> computeMutualInformationDelegate = new MutualInformationFeatureSelectionUtils.Impl.ComputeMutualInformationDelegate<int>(this.ComputeMutualInformation<int>);
				MethodInfo methodInfo2 = computeMutualInformationDelegate.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType.ItemType.RawType });
				return (float[])methodInfo2.Invoke(this, new object[]
				{
					trans,
					col,
					methodInfo.Invoke(null, new object[] { columnType.ItemType })
				});
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0001FA14 File Offset: 0x0001DC14
			private static MutualInformationFeatureSelectionUtils.Impl.Mapper<T> MakeKeyMapper<T>(ColumnType type)
			{
				ValueMapper<VBuffer<T>, VBuffer<int>> mapper = MutualInformationFeatureSelectionUtils.Impl.BinKeys<T>(type);
				return delegate(ref VBuffer<T> src, ref VBuffer<int> dst, out int min, out int lim)
				{
					min = 0;
					lim = type.KeyCount + 1;
					mapper.Invoke(ref src, ref dst);
				};
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x0001FA4C File Offset: 0x0001DC4C
			private float[] ComputeMutualInformation<T>(Transposer trans, int col, MutualInformationFeatureSelectionUtils.Impl.Mapper<T> mapper)
			{
				int valueCount = trans.Schema.GetColumnType(col).ValueCount;
				float[] array = new float[valueCount];
				int num = 0;
				VBuffer<int> vbuffer = default(VBuffer<int>);
				using (ISlotCursor slotCursor = trans.GetSlotCursor(col))
				{
					ValueGetter<VBuffer<T>> getter = slotCursor.GetGetter<T>();
					while (slotCursor.MoveNext())
					{
						VBuffer<T> vbuffer2 = default(VBuffer<T>);
						getter.Invoke(ref vbuffer2);
						int num2;
						int num3;
						mapper(ref vbuffer2, ref vbuffer, out num2, out num3);
						array[num++] = this.ComputeMutualInformation(ref vbuffer, num3 - num2, num2);
					}
				}
				return array;
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0001FAF0 File Offset: 0x0001DCF0
			private float ComputeMutualInformation(ref VBuffer<int> features, int numFeatures, int offset)
			{
				if (Utils.Size<int>(this._contingencyTable[0]) < numFeatures)
				{
					for (int i = 0; i < this._numLabels; i++)
					{
						Array.Resize<int>(ref this._contingencyTable[i], numFeatures);
					}
					Array.Resize<int>(ref this._featureSums, numFeatures);
				}
				for (int j = 0; j < this._numLabels; j++)
				{
					Array.Clear(this._contingencyTable[j], 0, numFeatures);
				}
				Array.Clear(this._labelSums, 0, this._numLabels);
				Array.Clear(this._featureSums, 0, numFeatures);
				this.FillTable(ref features, offset, numFeatures);
				for (int k = 0; k < this._numLabels; k++)
				{
					for (int l = 0; l < numFeatures; l++)
					{
						this._labelSums[k] += this._contingencyTable[k][l];
						this._featureSums[l] += this._contingencyTable[k][l];
					}
				}
				double num = 0.0;
				for (int m = 0; m < this._numLabels; m++)
				{
					for (int n = 0; n < numFeatures; n++)
					{
						if (this._contingencyTable[m][n] > 0)
						{
							num += (double)this._contingencyTable[m][n] / (double)this._labels.Length * Math.Log((double)this._contingencyTable[m][n] * (double)this._labels.Length / ((double)this._labelSums[m] * (double)this._featureSums[n]), 2.0);
						}
					}
				}
				return (float)num;
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0001FC84 File Offset: 0x0001DE84
			private void FillTable(ref VBuffer<int> features, int offset, int numFeatures)
			{
				if (features.IsDense)
				{
					for (int i = 0; i < this._labels.Length; i++)
					{
						int num = this._labels[i];
						int num2 = features.Values[i] - offset;
						this._contingencyTable[num][num2]++;
					}
					return;
				}
				int num3 = 0;
				for (int j = 0; j < this._labels.Length; j++)
				{
					int num4 = this._labels[j];
					int num5;
					if (num3 == features.Count || j < features.Indices[num3])
					{
						num5 = -offset;
					}
					else
					{
						num5 = features.Values[num3] - offset;
						num3++;
					}
					this._contingencyTable[num4][num5]++;
				}
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x0001FD78 File Offset: 0x0001DF78
			private static ValueMapper<VBuffer<T>, VBuffer<int>> BinKeys<T>(ColumnType colType)
			{
				ValueMapper<uint, int> valueMapper = null;
				bool flag;
				ValueMapper<T, uint> conv = Conversions.Instance.GetStandardConversion<T, uint>(colType, NumberType.U4, out flag);
				ValueMapper<T, int> valueMapper2;
				if (flag)
				{
					if (valueMapper == null)
					{
						valueMapper = delegate(ref uint src, ref int dst)
						{
							dst = (int)src;
						};
					}
					valueMapper2 = (ValueMapper<T, int>)valueMapper;
				}
				else
				{
					valueMapper2 = delegate(ref T src, ref int dst)
					{
						uint num = 0U;
						conv.Invoke(ref src, ref num);
						dst = (int)num;
					};
				}
				return MutualInformationFeatureSelectionUtils.CreateVectorMapper<T, int>(valueMapper2);
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0001FE14 File Offset: 0x0001E014
			private void BinSingles(ref VBuffer<float> input, ref VBuffer<int> output, int numBins, out int min, out int lim)
			{
				if (input.Values != null)
				{
					for (int i = 0; i < input.Count; i++)
					{
						float num = input.Values[i];
						if (!float.IsNaN(num))
						{
							this._singles.Add(num);
						}
					}
				}
				float[] bounds = this._binFinder.FindBins(numBins, this._singles, input.Length - input.Count);
				min = -1 - Utils.FindIndexSorted(bounds, 0f);
				lim = min + bounds.Length + 1;
				int offset = min;
				ValueMapper<float, int> valueMapper = delegate(ref float src, ref int dst)
				{
					dst = (float.IsNaN(src) ? offset : (offset + 1 + Utils.FindIndexSorted(bounds, src)));
				};
				valueMapper.MapVector(ref input, ref output);
				this._singles.Clear();
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x0001FF04 File Offset: 0x0001E104
			private void BinDoubles(ref VBuffer<double> input, ref VBuffer<int> output, int numBins, out int min, out int lim)
			{
				if (input.Values != null)
				{
					for (int i = 0; i < input.Count; i++)
					{
						double num = input.Values[i];
						if (!double.IsNaN(num))
						{
							this._doubles.Add(num);
						}
					}
				}
				double[] bounds = this._binFinder.FindBins(numBins, this._doubles, input.Length - input.Count);
				int offset = (min = -1 - Utils.FindIndexSorted(bounds, 0.0));
				lim = min + bounds.Length + 1;
				ValueMapper<double, int> valueMapper = delegate(ref double src, ref int dst)
				{
					dst = (double.IsNaN(src) ? offset : (offset + 1 + Utils.FindIndexSorted(bounds, src)));
				};
				valueMapper.MapVector(ref input, ref output);
				this._doubles.Clear();
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0001FFC7 File Offset: 0x0001E1C7
			private void BinBools(ref VBuffer<DvBool> input, ref VBuffer<int> output)
			{
				if (this._boolMapper == null)
				{
					this._boolMapper = MutualInformationFeatureSelectionUtils.CreateVectorMapper<DvBool, int>(new ValueMapper<DvBool, int>(this.BinOneBool));
				}
				this._boolMapper.Invoke(ref input, ref output);
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x0001FFF5 File Offset: 0x0001E1F5
			private void BinOneBool(ref DvBool src, ref int dst)
			{
				dst = (src.IsNA ? (-1) : (src.IsFalse ? 0 : 1));
			}

			// Token: 0x040002DB RID: 731
			private readonly IHost _host;

			// Token: 0x040002DC RID: 732
			private readonly BinFinderBase _binFinder;

			// Token: 0x040002DD RID: 733
			private int _numBins;

			// Token: 0x040002DE RID: 734
			private int[] _labels;

			// Token: 0x040002DF RID: 735
			private int _numLabels;

			// Token: 0x040002E0 RID: 736
			private int[][] _contingencyTable;

			// Token: 0x040002E1 RID: 737
			private int[] _labelSums;

			// Token: 0x040002E2 RID: 738
			private int[] _featureSums;

			// Token: 0x040002E3 RID: 739
			private readonly List<float> _singles;

			// Token: 0x040002E4 RID: 740
			private readonly List<double> _doubles;

			// Token: 0x040002E5 RID: 741
			private ValueMapper<VBuffer<DvBool>, VBuffer<int>> _boolMapper;

			// Token: 0x0200011D RID: 285
			// (Invoke) Token: 0x060005C6 RID: 1478
			private delegate int[] KeyLabelGetter<T>(Transposer trans, int labelCol, ColumnType labeColumnType);

			// Token: 0x0200011E RID: 286
			// (Invoke) Token: 0x060005CA RID: 1482
			private delegate float[] ComputeMutualInformationDelegate<T>(Transposer trans, int col, MutualInformationFeatureSelectionUtils.Impl.Mapper<T> mapper);

			// Token: 0x0200011F RID: 287
			// (Invoke) Token: 0x060005CE RID: 1486
			private delegate void Mapper<T>(ref VBuffer<T> src, ref VBuffer<int> dst, out int min, out int lim);
		}
	}
}
