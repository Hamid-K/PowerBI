using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002A7 RID: 679
	public sealed class NgramTransform : OneToOneTransformBase
	{
		// Token: 0x06000F99 RID: 3993 RVA: 0x00055423 File Offset: 0x00053623
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("NGRAMTRN", 65538U, 65538U, 65537U, "NgramTransform", null);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00055444 File Offset: 0x00053644
		public NgramTransform(NgramTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "Ngram", Contracts.CheckRef<NgramTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(NgramTransform.TestType))
		{
			this._exes = new NgramTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i] = new NgramTransform.ColInfoEx(args.column[i], args);
			}
			this._ngramMaps = this.Train(args, input, out this._invDocFreqs);
			this.InitColumnTypeAndMetadata(out this._types, out this._slotNamesTypes);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000554DC File Offset: 0x000536DC
		private NgramTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(NgramTransform.TestType))
		{
			this._exes = new NgramTransform.ColInfoEx[this.Infos.Length];
			this._ngramMaps = new SequencePool[this.Infos.Length];
			this._invDocFreqs = new double[this.Infos.Length][];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				this._exes[i] = new NgramTransform.ColInfoEx(ctx, ctx.Header.ModelVerWritten >= 65538U);
				this._ngramMaps[i] = new SequencePool(ctx.Reader);
				if (ctx.Header.ModelVerWritten >= 65538U)
				{
					this._invDocFreqs[i] = Utils.ReadDoubleArray(ctx.Reader);
					for (int j = 0; j < Utils.Size<double>(this._invDocFreqs[i]); j++)
					{
						Contracts.CheckDecode(this._host, this._invDocFreqs[i][j] >= 0.0);
					}
				}
			}
			this.InitColumnTypeAndMetadata(out this._types, out this._slotNamesTypes);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00055640 File Offset: 0x00053840
		public static NgramTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("Ngram");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(NgramTransform.GetVersionInfo());
			return HostExtensions.Apply<NgramTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ch, num == 4);
				return new NgramTransform(ctx, h, input);
			});
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0005574C File Offset: 0x0005394C
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(NgramTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
			VBuffer<DvText> ngramsNames = default(VBuffer<DvText>);
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i].Save(ctx);
				this._ngramMaps[i].Save(ctx.Writer);
				Utils.WriteDoubleArray(ctx.Writer, this._invDocFreqs[i]);
				if (this._slotNamesTypes[i] != null)
				{
					this.GetSlotNames(i, ref ngramsNames);
					ctx.SaveTextStream(string.Format("{0}-ngrams.txt", this.Infos[i].Name), delegate(TextWriter writer)
					{
						writer.WriteLine("# Number of Ngrams terms = {0}", ngramsNames.Count);
						for (int j = 0; j < ngramsNames.Count; j++)
						{
							writer.WriteLine("{0}\t{1}", j, ngramsNames.Values[j]);
						}
					});
				}
			}
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00055834 File Offset: 0x00053A34
		private static string TestType(ColumnType type)
		{
			if (!type.IsVector)
			{
				return "Expected vector of Key type, and Key is convertable to U4";
			}
			if (!type.ItemType.IsKey)
			{
				return "Expected vector of Key type, and Key is convertable to U4";
			}
			if (type.ItemType.KeyCount == 0 && type.ItemType.RawKind > 6)
			{
				return "Expected vector of Key type, and Key is convertable to U4";
			}
			return null;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00055884 File Offset: 0x00053A84
		private void InitColumnTypeAndMetadata(out VectorType[] types, out VectorType[] slotNamesTypes)
		{
			types = new VectorType[this.Infos.Length];
			slotNamesTypes = new VectorType[this.Infos.Length];
			MetadataDispatcher metadata = base.Metadata;
			for (int i = 0; i < this._exes.Length; i++)
			{
				types[i] = new VectorType(NumberType.Float, this._ngramMaps[i].Count);
				OneToOneTransformBase.ColInfo colInfo = this.Infos[i];
				if (MetadataUtils.HasKeyNames(this._input.Schema, colInfo.Source, colInfo.TypeSrc.ItemType.KeyCount))
				{
					using (MetadataDispatcher.Builder builder = metadata.BuildMetadata(i))
					{
						if (this._ngramMaps[i].Count > 0)
						{
							slotNamesTypes[i] = new VectorType(TextType.Instance, this._ngramMaps[i].Count);
							builder.AddGetter<VBuffer<DvText>>("SlotNames", slotNamesTypes[i], new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetSlotNames));
						}
					}
				}
			}
			metadata.Seal();
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000559A4 File Offset: 0x00053BA4
		private void GetSlotNames(int iinfo, ref VBuffer<DvText> dst)
		{
			int keyCount = this.Infos[iinfo].TypeSrc.ItemType.KeyCount;
			VBuffer<DvText> unigramNames = default(VBuffer<DvText>);
			this._input.Schema.GetMetadata<VBuffer<DvText>>("KeyValues", this.Infos[iinfo].Source, ref unigramNames);
			Contracts.Check(this._host, unigramNames.Length == keyCount);
			SequencePool sequencePool = this._ngramMaps[iinfo];
			DvText[] values = dst.Values;
			int count = sequencePool.Count;
			if (Utils.Size<DvText>(values) < count)
			{
				Array.Resize<DvText>(ref values, count);
			}
			StringBuilder stringBuilder = new StringBuilder();
			uint[] array = new uint[this._exes[iinfo].NgramLength];
			for (int i = 0; i < sequencePool.Count; i++)
			{
				int byId = sequencePool.GetById(i, ref array);
				this.ComposeNgramString(array, byId, stringBuilder, keyCount, delegate(int index, ref DvText term)
				{
					unigramNames.GetItemOrDefault(index, ref term);
				});
				values[i] = new DvText(stringBuilder.ToString());
			}
			dst = new VBuffer<DvText>(count, values, dst.Indices);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00055AD8 File Offset: 0x00053CD8
		private void ComposeNgramString(uint[] ngram, int count, StringBuilder sb, int keyCount, NgramTransform.TermGetter termGetter)
		{
			sb.Clear();
			DvText dvText = default(DvText);
			string text = "";
			for (int i = 0; i < count; i++)
			{
				sb.Append(text);
				text = "|";
				uint num = ngram[i];
				if (num <= 0U || (ulong)num > (ulong)((long)keyCount))
				{
					sb.Append("*");
				}
				else
				{
					termGetter((int)(num - 1U), ref dvText);
					dvText.AddToStringBuilder(sb);
				}
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00055BC0 File Offset: 0x00053DC0
		private SequencePool[] Train(NgramTransform.Arguments args, IDataView trainingData, out double[][] invDocFreqs)
		{
			NgramTransform.<>c__DisplayClass13 CS$<>8__locals1 = new NgramTransform.<>c__DisplayClass13();
			int[][] array = new int[this.Infos.Length][];
			for (int i2 = 0; i2 < this.Infos.Length; i2++)
			{
				bool flag = args.column[i2].allLengths ?? args.allLengths;
				int ngramLength = this._exes[i2].NgramLength;
				int[] maxNumTerms = ((Utils.Size<int>(args.column[i2].maxNumTerms) > 0) ? args.column[i2].maxNumTerms : args.maxNumTerms);
				if (!flag)
				{
					Contracts.CheckUserArg(this._host, Utils.Size<int>(maxNumTerms) == 0 || (Utils.Size<int>(maxNumTerms) == 1 && maxNumTerms[0] > 0), "MaxNumTerms");
					array[i2] = new int[ngramLength];
					array[i2][ngramLength - 1] = ((Utils.Size<int>(maxNumTerms) == 0) ? 10000000 : maxNumTerms[0]);
				}
				else
				{
					Contracts.CheckUserArg(this._host, Utils.Size<int>(maxNumTerms) <= ngramLength, "MaxNumTerms");
					IExceptionContext host = this._host;
					bool flag2;
					if (Utils.Size<int>(maxNumTerms) != 0)
					{
						flag2 = maxNumTerms.All((int i) => i >= 0) && maxNumTerms[maxNumTerms.Length - 1] > 0;
					}
					else
					{
						flag2 = true;
					}
					Contracts.CheckUserArg(host, flag2, "MaxNumTerms");
					int extend = ((Utils.Size<int>(maxNumTerms) == 0) ? 10000000 : maxNumTerms[maxNumTerms.Length - 1]);
					array[i2] = Utils.BuildArray<int>(ngramLength, delegate(int i)
					{
						if (i >= Utils.Size<int>(maxNumTerms))
						{
							return extend;
						}
						return maxNumTerms[i];
					});
				}
			}
			NgramBufferBuilder[] array2 = new NgramBufferBuilder[this.Infos.Length];
			ValueGetter<VBuffer<uint>>[] array3 = new ValueGetter<VBuffer<uint>>[this.Infos.Length];
			VBuffer<uint>[] array4 = new VBuffer<uint>[this.Infos.Length];
			int[][] array5 = new int[this.Infos.Length][];
			SequencePool[] array6 = new SequencePool[this.Infos.Length];
			CS$<>8__locals1.activeInput = new bool[trainingData.Schema.ColumnCount];
			foreach (OneToOneTransformBase.ColInfo colInfo in this.Infos)
			{
				CS$<>8__locals1.activeInput[colInfo.Source] = true;
			}
			SequencePool[] array10;
			using (IRowCursor rowCursor = trainingData.GetRowCursor((int col) => CS$<>8__locals1.activeInput[col], null))
			{
				using (IProgressChannel progressChannel = this._host.StartProgressChannel("Building n-gram dictionary"))
				{
					NgramTransform.<>c__DisplayClass19 CS$<>8__locals4 = new NgramTransform.<>c__DisplayClass19();
					CS$<>8__locals4.CS$<>8__locals14 = CS$<>8__locals1;
					for (int k = 0; k < this.Infos.Length; k++)
					{
						int ngramLength2 = this._exes[k].NgramLength;
						int skipLength = this._exes[k].SkipLength;
						array3[k] = RowCursorUtils.GetVecGetterAs<uint>(NumberType.U4, rowCursor, this.Infos[k].Source);
						array4[k] = default(VBuffer<uint>);
						array5[k] = new int[ngramLength2];
						array6[k] = new SequencePool();
						array2[k] = new NgramBufferBuilder(ngramLength2, skipLength, 2146435071, NgramTransform.GetNgramIdFinderAdd(array5[k], array[k], array6[k], this._exes[k].RequireIdf(), this._host));
					}
					int num = 0;
					bool[] array7 = new bool[this.Infos.Length];
					invDocFreqs = new double[this.Infos.Length][];
					CS$<>8__locals4.totalDocs = 0;
					NgramTransform.<>c__DisplayClass19 CS$<>8__locals5 = CS$<>8__locals4;
					long? rowCount = trainingData.GetRowCount(true);
					CS$<>8__locals5.rowCount = ((rowCount != null) ? ((double)rowCount.GetValueOrDefault()) : double.NaN);
					VBuffer<float>[] array8 = new VBuffer<float>[this.Infos.Length];
					progressChannel.SetHeader(new ProgressHeader(new string[] { "Total n-grams" }, new string[] { "documents" }), delegate(IProgressEntry e)
					{
						e.SetProgress(0, (double)CS$<>8__locals4.totalDocs, CS$<>8__locals4.rowCount);
					});
					while (num < this.Infos.Length && rowCursor.MoveNext())
					{
						CS$<>8__locals4.totalDocs++;
						for (int l = 0; l < this.Infos.Length; l++)
						{
							array3[l].Invoke(ref array4[l]);
							uint num2 = (uint)this.Infos[l].TypeSrc.ItemType.KeyCount;
							if (num2 == 0U)
							{
								num2 = uint.MaxValue;
							}
							if (!array7[l])
							{
								if (this._exes[l].RequireIdf())
								{
									array2[l].Reset();
								}
								array2[l].AddNgrams(ref array4[l], 0, num2);
								if (this._exes[l].RequireIdf())
								{
									int num3 = array5[l].Sum();
									Utils.EnsureSize<double>(ref invDocFreqs[l], num3, true);
									array2[l].GetResult(ref array8[l]);
									foreach (KeyValuePair<int, float> keyValuePair in array8[l].Items(false))
									{
										if (keyValuePair.Value >= 1f)
										{
											invDocFreqs[l][keyValuePair.Key] += 1.0;
										}
									}
								}
							}
						}
					}
					IProgressChannel progressChannel2 = progressChannel;
					double?[] array9 = new double?[2];
					array9[0] = new double?((double)array5.Sum((int[] c) => c.Sum()));
					array9[1] = new double?((double)CS$<>8__locals4.totalDocs);
					progressChannel2.Checkpoint(array9);
					for (int m = 0; m < this.Infos.Length; m++)
					{
						for (int n = 0; n < Utils.Size<double>(invDocFreqs[m]); n++)
						{
							if (invDocFreqs[m][n] != 0.0)
							{
								invDocFreqs[m][n] = Math.Log((double)CS$<>8__locals4.totalDocs / invDocFreqs[m][n]);
							}
						}
					}
					for (int num4 = 0; num4 < this.Infos.Length; num4++)
					{
						int ngramLength3 = this._exes[num4].NgramLength;
						for (int num5 = 0; num5 < ngramLength3; num5++)
						{
							this._exes[num4].NonEmptyLevels[num5] = array5[num4][num5] > 0;
						}
					}
					array10 = array6;
				}
			}
			return array10;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000562E4 File Offset: 0x000544E4
		[Conditional("DEBUG")]
		private void AssertValid(int[] counts, int[] lims, SequencePool pool)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < lims.Length; i++)
			{
				if (counts[i] == lims[i])
				{
					num2++;
				}
				num += counts[i];
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000563C4 File Offset: 0x000545C4
		private static NgramIdFinder GetNgramIdFinderAdd(int[] counts, int[] lims, SequencePool pool, bool requireIdf, IHost host)
		{
			int numFull = lims.Count((int l) => l <= 0);
			int ngramLength = lims.Length;
			return delegate(uint[] ngram, int lim, int icol, ref bool more)
			{
				int num = lim - 1;
				int num2 = -1;
				if (counts[num] < lims[num] && pool.TryAdd(ngram, 0, lim, out num2) && ++counts[num] >= lims[num])
				{
					numFull++;
				}
				if (!requireIdf)
				{
					more = numFull < ngramLength;
					return -1;
				}
				if (num2 == -1)
				{
					return pool.Get(ngram, 0, lim);
				}
				return num2;
			};
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00056480 File Offset: 0x00054680
		private NgramIdFinder GetNgramIdFinder(int iinfo)
		{
			return delegate(uint[] ngram, int lim, int icol, ref bool more)
			{
				if (!this._exes[iinfo].NonEmptyLevels[lim - 1])
				{
					return -1;
				}
				return this._ngramMaps[iinfo].Get(ngram, 0, lim);
			};
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000564AD File Offset: 0x000546AD
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			Contracts.Check(this._host, (0 <= iinfo) & (iinfo < this.Infos.Length));
			return this._types[iinfo];
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000566B8 File Offset: 0x000548B8
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			ValueGetter<VBuffer<uint>> getSrc = RowCursorUtils.GetVecGetterAs<uint>(NumberType.U4, input, this.Infos[iinfo].Source);
			VBuffer<uint> src = default(VBuffer<uint>);
			NgramBufferBuilder bldr = new NgramBufferBuilder(this._exes[iinfo].NgramLength, this._exes[iinfo].SkipLength, this._ngramMaps[iinfo].Count, this.GetNgramIdFinder(iinfo));
			uint keyCount = (uint)this.Infos[iinfo].TypeSrc.ItemType.KeyCount;
			if (keyCount == 0U)
			{
				keyCount = uint.MaxValue;
			}
			ValueGetter<VBuffer<float>> valueGetter;
			switch (this._exes[iinfo].Weighting)
			{
			case NgramTransform.WeightingCriteria.Tf:
				valueGetter = delegate(ref VBuffer<float> dst)
				{
					getSrc.Invoke(ref src);
					if (!bldr.IsEmpty)
					{
						bldr.Reset();
						bldr.AddNgrams(ref src, 0, keyCount);
						bldr.GetResult(ref dst);
						return;
					}
					dst = new VBuffer<float>(0, dst.Values, dst.Indices);
				};
				break;
			case NgramTransform.WeightingCriteria.Idf:
				valueGetter = delegate(ref VBuffer<float> dst)
				{
					getSrc.Invoke(ref src);
					if (!bldr.IsEmpty)
					{
						bldr.Reset();
						bldr.AddNgrams(ref src, 0, keyCount);
						bldr.GetResult(ref dst);
						VBufferUtils.Apply<float>(ref dst, delegate(int i, ref float v)
						{
							v = ((v >= 1f) ? ((float)this._invDocFreqs[iinfo][i]) : 0f);
						});
						return;
					}
					dst = new VBuffer<float>(0, dst.Values, dst.Indices);
				};
				break;
			case NgramTransform.WeightingCriteria.TfIdf:
				valueGetter = delegate(ref VBuffer<float> dst)
				{
					getSrc.Invoke(ref src);
					if (!bldr.IsEmpty)
					{
						bldr.Reset();
						bldr.AddNgrams(ref src, 0, keyCount);
						bldr.GetResult(ref dst);
						VBufferUtils.Apply<float>(ref dst, delegate(int i, ref float v)
						{
							v = (float)((double)v * this._invDocFreqs[iinfo][i]);
						});
						return;
					}
					dst = new VBuffer<float>(0, dst.Values, dst.Indices);
				};
				break;
			default:
				throw Contracts.Except(this._host, "Unsupported weighting criteria");
			}
			return valueGetter;
		}

		// Token: 0x04000895 RID: 2197
		private const uint verTfIdfSupported = 65538U;

		// Token: 0x04000896 RID: 2198
		public const string LoaderSignature = "NgramTransform";

		// Token: 0x04000897 RID: 2199
		internal const string Summary = "Produces a bag of counts of ngrams (sequences of consecutive values of length 1-n) in a given vector of keys. It does so by building a dictionary of ngrams and using the id in the dictionary as the index in the bag.";

		// Token: 0x04000898 RID: 2200
		private const string RegistrationName = "Ngram";

		// Token: 0x04000899 RID: 2201
		private readonly VectorType[] _types;

		// Token: 0x0400089A RID: 2202
		private readonly VectorType[] _slotNamesTypes;

		// Token: 0x0400089B RID: 2203
		private readonly NgramTransform.ColInfoEx[] _exes;

		// Token: 0x0400089C RID: 2204
		private readonly SequencePool[] _ngramMaps;

		// Token: 0x0400089D RID: 2205
		private readonly double[][] _invDocFreqs;

		// Token: 0x020002A8 RID: 680
		public enum WeightingCriteria
		{
			// Token: 0x040008A2 RID: 2210
			[EnumValueDisplay("TF (Term Frequency)")]
			Tf,
			// Token: 0x040008A3 RID: 2211
			[EnumValueDisplay("IDF (Inverse Document Frequency)")]
			Idf,
			// Token: 0x040008A4 RID: 2212
			[EnumValueDisplay("TF-IDF")]
			TfIdf
		}

		// Token: 0x020002A9 RID: 681
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06000FAB RID: 4011 RVA: 0x0005681C File Offset: 0x00054A1C
			public static NgramTransform.Column Parse(string str)
			{
				NgramTransform.Column column = new NgramTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000FAC RID: 4012 RVA: 0x0005683B File Offset: 0x00054A3B
			public bool TryUnparse(StringBuilder sb)
			{
				return this.ngramLength == null && this.allLengths == null && this.skipLength == null && Utils.Size<int>(this.maxNumTerms) == 0 && this.TryUnparseCore(sb);
			}

			// Token: 0x040008A5 RID: 2213
			[Argument(0, HelpText = "Maximum ngram length", ShortName = "ngram")]
			public int? ngramLength;

			// Token: 0x040008A6 RID: 2214
			[Argument(0, HelpText = "Whether to include all ngram lengths up to ngramLength, or only ngramLength", ShortName = "all")]
			public bool? allLengths;

			// Token: 0x040008A7 RID: 2215
			[Argument(0, HelpText = "Maximum number of tokens to skip when constructing an ngram", ShortName = "skips")]
			public int? skipLength;

			// Token: 0x040008A8 RID: 2216
			[Argument(4, HelpText = "Maximum number of ngrams to store in the dictionary", ShortName = "max")]
			public int[] maxNumTerms;

			// Token: 0x040008A9 RID: 2217
			[Argument(0, HelpText = "Statistical measure used to evaluate how important a word is to a document in a corpus")]
			public NgramTransform.WeightingCriteria? weighting;
		}

		// Token: 0x020002AA RID: 682
		public sealed class Arguments
		{
			// Token: 0x040008AA RID: 2218
			internal const int DefaultMaxTerms = 10000000;

			// Token: 0x040008AB RID: 2219
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public NgramTransform.Column[] column;

			// Token: 0x040008AC RID: 2220
			[Argument(0, HelpText = "Maximum ngram length", ShortName = "ngram")]
			public int ngramLength = 2;

			// Token: 0x040008AD RID: 2221
			[Argument(0, HelpText = "Whether to store all ngram lengths up to ngramLength, or only ngramLength", ShortName = "all")]
			public bool allLengths = true;

			// Token: 0x040008AE RID: 2222
			[Argument(0, HelpText = "Maximum number of tokens to skip when constructing an ngram", ShortName = "skips")]
			public int skipLength;

			// Token: 0x040008AF RID: 2223
			[Argument(4, HelpText = "Maximum number of ngrams to store in the dictionary", ShortName = "max")]
			public int[] maxNumTerms = new int[] { 10000000 };

			// Token: 0x040008B0 RID: 2224
			[Argument(0, HelpText = "The weighting criteria")]
			public NgramTransform.WeightingCriteria weighting;
		}

		// Token: 0x020002AB RID: 683
		private sealed class ColInfoEx
		{
			// Token: 0x06000FAF RID: 4015 RVA: 0x000568BB File Offset: 0x00054ABB
			public bool RequireIdf()
			{
				return this.Weighting == NgramTransform.WeightingCriteria.Idf || this.Weighting == NgramTransform.WeightingCriteria.TfIdf;
			}

			// Token: 0x06000FB0 RID: 4016 RVA: 0x000568D4 File Offset: 0x00054AD4
			public ColInfoEx(NgramTransform.Column item, NgramTransform.Arguments args)
			{
				this.NgramLength = item.ngramLength ?? args.ngramLength;
				Contracts.CheckUserArg(0 < this.NgramLength && this.NgramLength <= 10, "ngram");
				this.SkipLength = item.skipLength ?? args.skipLength;
				Contracts.CheckUserArg(0 <= this.SkipLength && this.SkipLength <= 10, "skips");
				if (this.NgramLength + this.SkipLength > 10)
				{
					throw Contracts.ExceptUserArg("skips", "The sum of skipLength and ngramLength must be less than or equal to {0}", new object[] { 10 });
				}
				Contracts.CheckUserArg(Enum.IsDefined(typeof(NgramTransform.WeightingCriteria), args.weighting), "weighting");
				this.Weighting = item.weighting ?? args.weighting;
				this.NonEmptyLevels = new bool[this.NgramLength];
			}

			// Token: 0x06000FB1 RID: 4017 RVA: 0x00056A04 File Offset: 0x00054C04
			public ColInfoEx(ModelLoadContext ctx, bool readWeighting)
			{
				this.NgramLength = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(0 < this.NgramLength && this.NgramLength <= 10);
				this.SkipLength = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(0 <= this.SkipLength && this.SkipLength <= 10);
				if (this.NgramLength + this.SkipLength > 10)
				{
					throw Contracts.ExceptDecode("The sum of skipLength and ngramLength must be less than or equal to {0}", new object[] { 10 });
				}
				if (readWeighting)
				{
					this.Weighting = (NgramTransform.WeightingCriteria)ctx.Reader.ReadInt32();
				}
				Contracts.CheckDecode(Enum.IsDefined(typeof(NgramTransform.WeightingCriteria), this.Weighting));
				this.NonEmptyLevels = Utils.ReadBoolArray(ctx.Reader, this.NgramLength);
			}

			// Token: 0x06000FB2 RID: 4018 RVA: 0x00056AF0 File Offset: 0x00054CF0
			public void Save(ModelSaveContext ctx)
			{
				ctx.Writer.Write(this.NgramLength);
				ctx.Writer.Write(this.SkipLength);
				ctx.Writer.Write((int)this.Weighting);
				Utils.WriteBoolBytesNoCount(ctx.Writer, this.NonEmptyLevels, this.NgramLength);
			}

			// Token: 0x040008B1 RID: 2225
			public readonly bool[] NonEmptyLevels;

			// Token: 0x040008B2 RID: 2226
			public readonly int NgramLength;

			// Token: 0x040008B3 RID: 2227
			public readonly int SkipLength;

			// Token: 0x040008B4 RID: 2228
			public readonly NgramTransform.WeightingCriteria Weighting;
		}

		// Token: 0x020002AC RID: 684
		// (Invoke) Token: 0x06000FB4 RID: 4020
		private delegate void TermGetter(int index, ref DvText term);
	}
}
