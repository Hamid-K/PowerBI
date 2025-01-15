using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Dracula;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000286 RID: 646
	public class CountTableTransform : OneToOneTransformBase, ITransformTemplate, IDataTransform, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x0005206C File Offset: 0x0005026C
		public CountTableTransform(CountTableTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "CountTable", Contracts.CheckRef<CountTableTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(CountTableTransform.TestColumnType))
		{
			Contracts.CheckUserArg(this._host, !string.IsNullOrWhiteSpace(args.labelColumn), "labelColumn", "Must specify the label column name");
			int num;
			if (!input.Schema.TryGetColumnIndex(args.labelColumn, ref num))
			{
				throw Contracts.ExceptUserArg(this._host, "labelColumn", "Label column '{0}' not found", new object[] { args.labelColumn });
			}
			ColumnType columnType = input.Schema.GetColumnType(num);
			this.CheckLabelType(columnType, out this._labelCardinality);
			CountTableTransform.InitLabelClassNames(this._host, this._input.Schema, args.labelColumn, this._labelCardinality, out this._labelClassNames);
			int num2 = this.Infos.Length;
			SubComponent<ICountTableBuilder, SignatureCountTableBuilder>[] array = args.column.Select((CountTableTransform.Column c) => c.countTable).ToArray<SubComponent<ICountTableBuilder, SignatureCountTableBuilder>>();
			IMultiCountTableBuilder multiCountTableBuilder;
			if (!args.sharedTable)
			{
				multiCountTableBuilder = new ParallelMultiCountTableBuilder(this._host, this.Infos, array, args.countTable, this._labelCardinality);
				if (!string.IsNullOrEmpty(args.externalCountsFile))
				{
					((ParallelMultiCountTableBuilder)multiCountTableBuilder).LoadExternalCounts(args.externalCountsFile, args.externalCountsSchema, this._labelCardinality);
				}
			}
			else
			{
				Contracts.CheckUserArg(this._host, args.column.All((CountTableTransform.Column c) => c.countTable == null), "column", "Can't have non-default count tables if the tables are shared");
				multiCountTableBuilder = new BagMultiCountTableBuilder(this._host, args.countTable, this._labelCardinality);
			}
			using (IChannel channel = this._host.Start("Training count tables"))
			{
				this.TrainTables(input, multiCountTableBuilder, num, channel);
				channel.Done();
			}
			this._multiCountTable = multiCountTableBuilder.CreateMultiCountTable();
			this._featurizers = new ICountFeaturizer[num2][];
			for (int i = 0; i < num2; i++)
			{
				int valueCount = this.Infos[i].TypeSrc.ValueCount;
				this._featurizers[i] = new ICountFeaturizer[valueCount];
				for (int j = 0; j < valueCount; j++)
				{
					this._featurizers[i][j] = this.CreateFeaturizer(this._host, args.column[i].featurizer ?? args.featurizer, this._multiCountTable.GetCountTable(i, j));
				}
			}
			this._columnTypes = this.GenerateColumnTypesAndMetadata();
			this._savedColumnFeatureNames = new string[this.Infos.Length][];
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00052328 File Offset: 0x00050528
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("CNTTBL F", 65541U, 65541U, 65541U, "CountTableTransform", "CountTableFunction");
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00052398 File Offset: 0x00050598
		public static CountTableTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("CountTable");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(CountTableTransform.GetVersionInfo());
			return HostExtensions.Apply<CountTableTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ch, num > 1);
				return new CountTableTransform(ctx, h, input, num);
			});
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00052430 File Offset: 0x00050630
		private CountTableTransform(IHostEnvironment env, CountTableTransform transform, IDataView newSource)
			: base(env, "CountTable", transform, newSource, new Func<ColumnType, string>(CountTableTransform.TestColumnType))
		{
			this._labelClassNames = transform._labelClassNames;
			this._labelCardinality = transform._labelCardinality;
			this._multiCountTable = transform._multiCountTable;
			this._featurizers = transform._featurizers;
			this._columnTypes = this.GenerateColumnTypesAndMetadata();
			this._savedColumnFeatureNames = new string[this.Infos.Length][];
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000524A8 File Offset: 0x000506A8
		private CountTableTransform(ModelLoadContext ctx, IHost host, IDataView input, int labelCardinality)
			: base(ctx, host, input, new Func<ColumnType, string>(CountTableTransform.TestColumnType))
		{
			this._labelCardinality = labelCardinality;
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 0 || num == this._labelCardinality, "unexpected label cardinality");
			if (num > 0)
			{
				this._labelClassNames = new string[num];
				for (int i = 0; i < num; i++)
				{
					this._labelClassNames[i] = ctx.LoadString();
				}
			}
			ctx.LoadModel<IMultiCountTable, SignatureLoadModel>(out this._multiCountTable, "CountTable", new object[] { this._host });
			this._featurizers = new ICountFeaturizer[this.Infos.Length][];
			for (int j = 0; j < this.Infos.Length; j++)
			{
				int num2 = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._host, num2 > 0);
				Contracts.CheckDecode(this._host, num2 == this.Infos[j].TypeSrc.ValueCount);
				this._featurizers[j] = new ICountFeaturizer[num2];
				for (int k = 0; k < num2; k++)
				{
					string text = string.Format("Feat_{0:000}_{1:000}", j, k);
					ctx.LoadModel<ICountFeaturizer, SignatureLoadCountFeaturizer>(out this._featurizers[j][k], text, new object[]
					{
						this._host,
						this._multiCountTable.GetCountTable(j, k)
					});
				}
			}
			this._columnTypes = this.GenerateColumnTypesAndMetadata();
			this._savedColumnFeatureNames = new string[this.Infos.Length][];
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00052648 File Offset: 0x00050848
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(CountTableTransform.GetVersionInfo());
			ctx.Writer.Write(this._labelCardinality);
			base.SaveBase(ctx);
			ctx.Writer.Write(Utils.Size<string>(this._labelClassNames));
			if (this._labelClassNames != null)
			{
				for (int i = 0; i < this._labelClassNames.Length; i++)
				{
					ctx.SaveString(this._labelClassNames[i]);
				}
			}
			ctx.SaveModel<IMultiCountTable>(this._multiCountTable, "CountTable");
			for (int j = 0; j < this._featurizers.Length; j++)
			{
				int num = this._featurizers[j].Length;
				ctx.Writer.Write(num);
				for (int k = 0; k < num; k++)
				{
					string text = string.Format("Feat_{0:000}_{1:000}", j, k);
					ctx.SaveModel<ICountFeaturizer>(this._featurizers[j][k], text);
				}
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0005273F File Offset: 0x0005093F
		public IDataTransform ApplyToData(IHostEnvironment env, IDataView newSource)
		{
			Contracts.CheckValue<IHostEnvironment>(this._host, env, "env");
			Contracts.CheckValue<IDataView>(this._host, newSource, "newSource");
			return new CountTableTransform(env, this, newSource);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0005276C File Offset: 0x0005096C
		private void CheckLabelType(ColumnType labelColumnType, out int labelCardinality)
		{
			if (labelColumnType.IsNumber)
			{
				labelCardinality = 2;
				return;
			}
			if (labelColumnType.IsKey)
			{
				labelCardinality = labelColumnType.KeyCount;
				Contracts.CheckUserArg(this._host, labelCardinality > 1, "labelColumn", "Label type must have known cardinality more than 1");
				return;
			}
			throw Contracts.ExceptUserArg(this._host, "labelColumn", "Incorrect label column type: expected numeric or key type");
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000527C8 File Offset: 0x000509C8
		private ICountFeaturizer CreateFeaturizer(IHostEnvironment env, SubComponent<ICountFeaturizer, SignatureCountFeaturizer> featurizerArgs, ICountTable countTable)
		{
			return ComponentCatalog.CreateInstance<ICountFeaturizer, SignatureCountFeaturizer>(featurizerArgs, new object[] { env, this._labelCardinality, countTable });
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0005280C File Offset: 0x00050A0C
		private void TrainTables(IDataView trainingData, IMultiCountTableBuilder builder, int labelColumnIndex, IChannel ch)
		{
			int num = this.Infos.Length;
			bool[] activeInput = new bool[this._input.Schema.ColumnCount];
			foreach (OneToOneTransformBase.ColInfo colInfo in this.Infos)
			{
				activeInput[colInfo.Source] = true;
			}
			activeInput[labelColumnIndex] = true;
			using (IRowCursor rowCursor = trainingData.GetRowCursor((int col) => activeInput[col], null))
			{
				ValueGetter<uint>[] array = new ValueGetter<uint>[num];
				ValueGetter<VBuffer<uint>>[] array2 = new ValueGetter<VBuffer<uint>>[num];
				for (int j = 0; j < num; j++)
				{
					if (this.Infos[j].TypeSrc.IsVector)
					{
						array2[j] = rowCursor.GetGetter<VBuffer<uint>>(this.Infos[j].Source);
					}
					else
					{
						array[j] = rowCursor.GetGetter<uint>(this.Infos[j].Source);
					}
				}
				ValueGetter<long> labelGetter = this.GetLabelGetter(rowCursor, labelColumnIndex);
				long num2 = 0L;
				uint num3 = 0U;
				VBuffer<uint> vbuffer = default(VBuffer<uint>);
				long num4 = 0L;
				while (rowCursor.MoveNext())
				{
					labelGetter.Invoke(ref num2);
					if (num2 >= 0L)
					{
						for (int k = 0; k < num; k++)
						{
							OneToOneTransformBase.ColInfo colInfo2 = this.Infos[k];
							if (colInfo2.TypeSrc.IsVector)
							{
								array2[k].Invoke(ref vbuffer);
								Contracts.Check(this._host, vbuffer.Length == colInfo2.TypeSrc.VectorSize, "value count mismatch");
								this.IncrementVec(builder, k, ref vbuffer, (uint)num2);
							}
							else
							{
								array[k].Invoke(ref num3);
								builder.IncrementOne(k, num3, (uint)num2, 1.0);
							}
						}
						num4 += 1L;
						if (num4 % 100000L == 0L)
						{
							ch.Progress((double)num4, 0.0);
						}
					}
				}
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00052A74 File Offset: 0x00050C74
		private static void InitLabelClassNames(IExceptionContext ectx, ISchema schema, string labelColumn, int labelCardinality, out string[] labelClassNames)
		{
			int num;
			Contracts.Check(schema.TryGetColumnIndex(labelColumn, ref num));
			ColumnType metadataTypeOrNull = schema.GetMetadataTypeOrNull("KeyValues", num);
			if (metadataTypeOrNull != null && metadataTypeOrNull.IsVector && metadataTypeOrNull.VectorSize == labelCardinality)
			{
				VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
				schema.GetMetadata<VBuffer<DvText>>("KeyValues", num, ref vbuffer);
				Contracts.Check(ectx, vbuffer.Length == labelCardinality);
				labelClassNames = vbuffer.Items(true).Select(delegate(KeyValuePair<int, DvText> pair)
				{
					if (pair.Value.HasChars)
					{
						return pair.Value.ToString();
					}
					return string.Format("Class{0:000}", pair.Key);
				}).ToArray<string>();
				return;
			}
			labelClassNames = null;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00052B10 File Offset: 0x00050D10
		public ColumnType[] GenerateColumnTypesAndMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			ColumnType[] array = new ColumnType[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				ICountFeaturizer featurizerForColumn = this.GetFeaturizerForColumn(i);
				int valueCount = this.Infos[i].TypeSrc.ValueCount;
				Contracts.Check(this._host, (long)valueCount * (long)featurizerForColumn.NumFeatures < 2147483647L, "Too large output size");
				array[i] = new VectorType(NumberType.R4, new int[] { valueCount, featurizerForColumn.NumFeatures });
				if (!this.Infos[i].TypeSrc.IsVector || this.IsValidSlotNameType(i))
				{
					using (MetadataDispatcher.Builder builder = metadata.BuildMetadata(i))
					{
						builder.AddGetter<VBuffer<DvText>>("SlotNames", MetadataUtils.GetNamesType(array[i].VectorSize), new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetSlotNames));
					}
				}
			}
			metadata.Seal();
			return array;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00052C24 File Offset: 0x00050E24
		private static string TestColumnType(ColumnType type)
		{
			if (type.ValueCount > 0 && type.ItemType.IsKey && type.ItemType.RawKind == 6)
			{
				return null;
			}
			return "Expected U4 Key type or vector of U4 Key type";
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00052C51 File Offset: 0x00050E51
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._columnTypes[iinfo];
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00052C5C File Offset: 0x00050E5C
		private void GetSlotNames(int iinfo, ref VBuffer<DvText> dst)
		{
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			if (this.Infos[iinfo].TypeSrc.IsVector)
			{
				this._input.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", this.Infos[iinfo].Source, ref vbuffer);
			}
			else
			{
				vbuffer = new VBuffer<DvText>(1, new DvText[]
				{
					new DvText(this._input.Schema.GetColumnName(this.Infos[iinfo].Source))
				}, null);
			}
			Contracts.Check(this._host, vbuffer.Length == this.Infos[iinfo].TypeSrc.ValueCount, "unexpected number of slot names");
			string[] array;
			this.GetColumnFeatureNames(iinfo, out array);
			int num = array.Length;
			DvText[] array2;
			if (dst.Count >= num * vbuffer.Length)
			{
				array2 = dst.Values;
			}
			else
			{
				array2 = new DvText[num * vbuffer.Length];
			}
			foreach (KeyValuePair<int, DvText> keyValuePair in vbuffer.Items(true))
			{
				int key = keyValuePair.Key;
				string text = keyValuePair.Value.ToString();
				for (int i = 0; i < num; i++)
				{
					array2[key * num + i] = new DvText(string.Format("{0}_{1}", text, array[i]));
				}
			}
			dst = new VBuffer<DvText>(num * vbuffer.Length, array2, dst.Indices);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00052E08 File Offset: 0x00051008
		private ICountFeaturizer GetFeaturizerForColumn(int iinfo)
		{
			return this._featurizers[iinfo][0];
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00052E14 File Offset: 0x00051014
		private void GetColumnFeatureNames(int iinfo, out string[] featureNames)
		{
			if (this._savedColumnFeatureNames[iinfo] == null)
			{
				ICountFeaturizer featurizerForColumn = this.GetFeaturizerForColumn(iinfo);
				int numFeatures = featurizerForColumn.NumFeatures;
				Interlocked.CompareExchange<string[]>(ref this._savedColumnFeatureNames[iinfo], featurizerForColumn.GetFeatureNames(this._labelClassNames).ToArray<string>(), null);
				Contracts.Check(this._host, Utils.Size<string>(this._savedColumnFeatureNames[iinfo]) == numFeatures, "unexpected # of feature names");
			}
			featureNames = this._savedColumnFeatureNames[iinfo];
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00052E88 File Offset: 0x00051088
		private bool IsValidSlotNameType(int iinfo)
		{
			ColumnType metadataTypeOrNull = this._input.Schema.GetMetadataTypeOrNull("SlotNames", this.Infos[iinfo].Source);
			return metadataTypeOrNull != null && metadataTypeOrNull.IsKnownSizeVector && metadataTypeOrNull.VectorSize == this.Infos[iinfo].TypeSrc.VectorSize && metadataTypeOrNull.ItemType.IsText;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00052F7C File Offset: 0x0005117C
		private ValueGetter<long> GetLabelGetter(IRow row, int col)
		{
			ColumnType columnType = row.Schema.GetColumnType(col);
			if (columnType.IsKey)
			{
				int size = columnType.KeyCount;
				ulong src2 = 0UL;
				ValueGetter<ulong> getSrc2 = RowCursorUtils.GetGetterAs<ulong>(NumberType.U8, row, col);
				return delegate(ref long dst)
				{
					getSrc2.Invoke(ref src2);
					if (src2 <= (ulong)((long)size))
					{
						dst = (long)(src2 - 1UL);
						return;
					}
					dst = -1L;
				};
			}
			double src = 0.0;
			ValueGetter<double> getSrc = RowCursorUtils.GetGetterAs<double>(NumberType.R8, row, col);
			return delegate(ref long dst)
			{
				getSrc.Invoke(ref src);
				if (src > 0.0)
				{
					dst = 1L;
					return;
				}
				if (src <= 0.0)
				{
					dst = 0L;
					return;
				}
				dst = -1L;
			};
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0005300C File Offset: 0x0005120C
		private void IncrementVec(IMultiCountTableBuilder builder, int iCol, ref VBuffer<uint> srcBuffer, uint labelKey)
		{
			int length = srcBuffer.Length;
			if (srcBuffer.IsDense)
			{
				for (int i = 0; i < length; i++)
				{
					builder.IncrementSlot(iCol, i, srcBuffer.Values[i], labelKey, 1.0);
				}
				return;
			}
			for (int j = 0; j < srcBuffer.Count; j++)
			{
				builder.IncrementSlot(iCol, srcBuffer.Indices[j], srcBuffer.Values[j], labelKey, 1.0);
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00053083 File Offset: 0x00051283
		public ICountTable GetCountTable(int columnIndex, int slotIndex)
		{
			return this._multiCountTable.GetCountTable(columnIndex, slotIndex);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00053092 File Offset: 0x00051292
		public int GetLabelCardinality()
		{
			return this._labelCardinality;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0005309A File Offset: 0x0005129A
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			if (this.Infos[iinfo].TypeSrc.IsVector)
			{
				return this.ConstructVectorGetter(input, iinfo);
			}
			return this.ConstructSingleGetter(input, iinfo);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0005314C File Offset: 0x0005134C
		private ValueGetter<VBuffer<float>> ConstructSingleGetter(IRow input, int bindingIndex)
		{
			uint src = 0U;
			ValueGetter<uint> srcGetter = base.GetSrcGetter<uint>(input, bindingIndex);
			int numFeatureColumns = this._featurizers[bindingIndex][0].NumFeatures;
			return delegate(ref VBuffer<float> dst)
			{
				srcGetter.Invoke(ref src);
				float[] array = dst.Values;
				if (Utils.Size<float>(array) < numFeatureColumns)
				{
					array = new float[numFeatureColumns];
				}
				this._featurizers[bindingIndex][0].GetFeatures((long)((ulong)src), array, 0);
				dst = new VBuffer<float>(numFeatureColumns, array, dst.Indices);
			};
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x000532EC File Offset: 0x000514EC
		private ValueGetter<VBuffer<float>> ConstructVectorGetter(IRow input, int bindingIndex)
		{
			int n = this.Infos[bindingIndex].TypeSrc.ValueCount;
			VBuffer<uint> src = default(VBuffer<uint>);
			int numFeatureColumns = this._featurizers[bindingIndex][0].NumFeatures;
			ValueGetter<VBuffer<uint>> srcGetter = base.GetSrcGetter<VBuffer<uint>>(input, bindingIndex);
			return delegate(ref VBuffer<float> dst)
			{
				srcGetter.Invoke(ref src);
				float[] array = dst.Values;
				if (Utils.Size<float>(array) < n * numFeatureColumns)
				{
					array = new float[n * numFeatureColumns];
				}
				if (src.IsDense)
				{
					for (int i = 0; i < n; i++)
					{
						this._featurizers[bindingIndex][i].GetFeatures((long)((ulong)src.Values[i]), array, i * numFeatureColumns);
					}
				}
				else
				{
					for (int j = 0; j < numFeatureColumns * n; j++)
					{
						array[j] = 0f;
					}
					for (int k = 0; k < src.Count; k++)
					{
						int num = src.Indices[k];
						this._featurizers[bindingIndex][num].GetFeatures((long)((ulong)src.Values[k]), array, num * numFeatureColumns);
					}
				}
				dst = new VBuffer<float>(n * numFeatureColumns, array, dst.Indices);
			};
		}

		// Token: 0x04000813 RID: 2067
		internal const string Summary = "Transforms the categorical column into the set of features: count of each label class, log-odds for each label class, back-off indicator. The input columns must be keys. This is a part of the Dracula transform.";

		// Token: 0x04000814 RID: 2068
		private const string RegistrationName = "CountTable";

		// Token: 0x04000815 RID: 2069
		public const string LoaderSignature = "CountTableTransform";

		// Token: 0x04000816 RID: 2070
		internal const string LoaderSignatureOld = "CountTableFunction";

		// Token: 0x04000817 RID: 2071
		private readonly ColumnType[] _columnTypes;

		// Token: 0x04000818 RID: 2072
		private readonly string[] _labelClassNames;

		// Token: 0x04000819 RID: 2073
		private readonly string[][] _savedColumnFeatureNames;

		// Token: 0x0400081A RID: 2074
		private readonly ICountFeaturizer[][] _featurizers;

		// Token: 0x0400081B RID: 2075
		private readonly int _labelCardinality;

		// Token: 0x0400081C RID: 2076
		private readonly IMultiCountTable _multiCountTable;

		// Token: 0x02000287 RID: 647
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06000F25 RID: 3877 RVA: 0x00053370 File Offset: 0x00051570
			public static CountTableTransform.Column Parse(string str)
			{
				CountTableTransform.Column column = new CountTableTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000F26 RID: 3878 RVA: 0x0005338F File Offset: 0x0005158F
			public bool TryUnparse(StringBuilder sb)
			{
				return !SubComponentExtensions.IsGood(this.countTable) && !SubComponentExtensions.IsGood(this.featurizer) && this.TryUnparseCore(sb);
			}

			// Token: 0x04000820 RID: 2080
			[Argument(4, HelpText = "Count table settings", ShortName = "table")]
			public SubComponent<ICountTableBuilder, SignatureCountTableBuilder> countTable;

			// Token: 0x04000821 RID: 2081
			[Argument(4, HelpText = "Featurizer for counts", ShortName = "feat")]
			public SubComponent<ICountFeaturizer, SignatureCountFeaturizer> featurizer;
		}

		// Token: 0x02000288 RID: 648
		public sealed class Arguments
		{
			// Token: 0x04000822 RID: 2082
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public CountTableTransform.Column[] column;

			// Token: 0x04000823 RID: 2083
			[Argument(4, HelpText = "Count table settings", ShortName = "table")]
			public SubComponent<ICountTableBuilder, SignatureCountTableBuilder> countTable = new SubComponent<ICountTableBuilder, SignatureCountTableBuilder>("CMSketch");

			// Token: 0x04000824 RID: 2084
			[Argument(4, HelpText = "Featurizer for counts", ShortName = "feat")]
			public SubComponent<ICountFeaturizer, SignatureCountFeaturizer> featurizer = new SubComponent<ICountFeaturizer, SignatureCountFeaturizer>("Dracula");

			// Token: 0x04000825 RID: 2085
			[Argument(1, HelpText = "Label column", ShortName = "label,lab", Purpose = "ColumnName")]
			public string labelColumn;

			// Token: 0x04000826 RID: 2086
			[Argument(0, HelpText = "Optional text file to load counts from", ShortName = "extfile")]
			public string externalCountsFile;

			// Token: 0x04000827 RID: 2087
			[Argument(0, HelpText = "Comma-separated list of column IDs in the external count file", ShortName = "extschema")]
			public string externalCountsSchema;

			// Token: 0x04000828 RID: 2088
			[Argument(0, HelpText = "Keep counts for all columns in one shared count table", ShortName = "shared")]
			public bool sharedTable;
		}
	}
}
