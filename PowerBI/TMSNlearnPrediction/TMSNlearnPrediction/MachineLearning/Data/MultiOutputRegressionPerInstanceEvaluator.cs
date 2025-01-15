using System;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;
using Microsoft.MachineLearning.Numeric;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000201 RID: 513
	public sealed class MultiOutputRegressionPerInstanceEvaluator : PerInstanceEvaluatorBase
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x0003D8E5 File Offset: 0x0003BAE5
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("MREGINST", 65537U, 65537U, 65537U, "MultiRegPerInstance", null);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0003D906 File Offset: 0x0003BB06
		public MultiOutputRegressionPerInstanceEvaluator(IHostEnvironment env, ISchema schema, string scoreCol, string labelCol)
			: base(env, schema, scoreCol, labelCol)
		{
			this.CheckInputColumnTypes(schema, out this._labelType, out this._scoreType, out this._labelMetadata, out this._scoreMetadata);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0003D932 File Offset: 0x0003BB32
		private MultiOutputRegressionPerInstanceEvaluator(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
			: base(ctx, env, schema)
		{
			this.CheckInputColumnTypes(schema, out this._labelType, out this._scoreType, out this._labelMetadata, out this._scoreMetadata);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0003D95C File Offset: 0x0003BB5C
		public static MultiOutputRegressionPerInstanceEvaluator Create(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(MultiOutputRegressionPerInstanceEvaluator.GetVersionInfo());
			return new MultiOutputRegressionPerInstanceEvaluator(ctx, env, schema);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0003D988 File Offset: 0x0003BB88
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(MultiOutputRegressionPerInstanceEvaluator.GetVersionInfo());
			base.Save(ctx);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0003DA4C File Offset: 0x0003BC4C
		public override Func<int, bool> GetDependencies(Func<int, bool> activeOutput)
		{
			return (int col) => (activeOutput(0) && col == this._labelIndex) || (activeOutput(1) && col == this._scoreIndex) || ((activeOutput(2) || activeOutput(3) || activeOutput(4)) && (col == this._scoreIndex || col == this._labelIndex));
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0003DA7C File Offset: 0x0003BC7C
		public override RowMapperColumnInfo[] GetOutputColumns()
		{
			return new RowMapperColumnInfo[]
			{
				new RowMapperColumnInfo(this._labelCol, this._labelType, this._labelMetadata),
				new RowMapperColumnInfo(this._scoreCol, this._scoreType, this._scoreMetadata),
				new RowMapperColumnInfo("L1-loss", NumberType.R8, null),
				new RowMapperColumnInfo("L2-loss", NumberType.R8, null),
				new RowMapperColumnInfo("Euclidean-Distance", NumberType.R8, null)
			};
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0003DC00 File Offset: 0x0003BE00
		public override Delegate[] CreateGetters(IRowCursor input, Func<int, bool> activeCols)
		{
			long cachedPosition = -1L;
			VBuffer<float> label = default(VBuffer<float>);
			VBuffer<float> score = default(VBuffer<float>);
			ValueGetter<VBuffer<float>> valueGetter = delegate(ref VBuffer<float> vec)
			{
				vec = default(VBuffer<float>);
			};
			ValueGetter<VBuffer<float>> labelGetter = ((activeCols(0) || activeCols(2) || activeCols(3) || activeCols(4)) ? RowCursorUtils.GetVecGetterAs<float>(NumberType.Float, input, this._labelIndex) : valueGetter);
			ValueGetter<VBuffer<float>> scoreGetter = ((activeCols(1) || activeCols(2) || activeCols(3) || activeCols(4)) ? input.GetGetter<VBuffer<float>>(this._scoreIndex) : valueGetter);
			Action updateCacheIfNeeded = delegate
			{
				if (cachedPosition != input.Position)
				{
					labelGetter.Invoke(ref label);
					scoreGetter.Invoke(ref score);
					cachedPosition = input.Position;
				}
			};
			Delegate[] array = new Delegate[5];
			if (activeCols(0))
			{
				ValueGetter<VBuffer<float>> valueGetter2 = delegate(ref VBuffer<float> dst)
				{
					updateCacheIfNeeded();
					label.CopyTo(ref dst);
				};
				array[0] = valueGetter2;
			}
			if (activeCols(1))
			{
				ValueGetter<VBuffer<float>> valueGetter3 = delegate(ref VBuffer<float> dst)
				{
					updateCacheIfNeeded();
					score.CopyTo(ref dst);
				};
				array[1] = valueGetter3;
			}
			if (activeCols(2))
			{
				ValueGetter<double> valueGetter4 = delegate(ref double dst)
				{
					updateCacheIfNeeded();
					dst = (double)VectorUtils.L1Distance(ref label, ref score);
				};
				array[2] = valueGetter4;
			}
			if (activeCols(3))
			{
				ValueGetter<double> valueGetter5 = delegate(ref double dst)
				{
					updateCacheIfNeeded();
					dst = (double)VectorUtils.L2DistSquared(ref label, ref score);
				};
				array[3] = valueGetter5;
			}
			if (activeCols(4))
			{
				ValueGetter<double> valueGetter6 = delegate(ref double dst)
				{
					updateCacheIfNeeded();
					dst = (double)MathUtils.Sqrt(VectorUtils.L2DistSquared(ref label, ref score));
				};
				array[4] = valueGetter6;
			}
			return array;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0003DDC4 File Offset: 0x0003BFC4
		private void CheckInputColumnTypes(ISchema schema, out ColumnType labelType, out ColumnType scoreType, out ColumnMetadataInfo labelMetadata, out ColumnMetadataInfo scoreMetadata)
		{
			ColumnType columnType = schema.GetColumnType(this._labelIndex);
			if (!columnType.IsKnownSizeVector || (columnType.ItemType != NumberType.R4 && columnType.ItemType != NumberType.R8))
			{
				throw Contracts.Except(this._host, "Label column '{0}' has type '{1}' but must be a known-size vector of R4 or R8", new object[] { this._labelCol, columnType });
			}
			labelType = new VectorType(columnType.ItemType.AsPrimitive, columnType.VectorSize);
			VectorType vectorType = new VectorType(TextType.Instance, columnType.VectorSize);
			labelMetadata = new ColumnMetadataInfo(this._labelCol);
			labelMetadata.Add("SlotNames", new MetadataInfo<VBuffer<DvText>>(vectorType, this.CreateSlotNamesGetter(schema, this._labelIndex, labelType.VectorSize, "True")));
			columnType = schema.GetColumnType(this._scoreIndex);
			if (columnType.VectorSize == 0 || columnType.ItemType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type '{1}' but must be a known length vector of type R4", new object[] { this._scoreCol, columnType });
			}
			scoreType = new VectorType(columnType.ItemType.AsPrimitive, columnType.VectorSize);
			scoreMetadata = new ColumnMetadataInfo(this._scoreCol);
			scoreMetadata.Add("SlotNames", new MetadataInfo<VBuffer<DvText>>(vectorType, this.CreateSlotNamesGetter(schema, this._scoreIndex, scoreType.VectorSize, "Predicted")));
			scoreMetadata.Add("ScoreColumnKind", new MetadataInfo<DvText>(TextType.Instance, new MetadataUtils.MetadataGetter<DvText>(this.GetScoreColumnKind)));
			scoreMetadata.Add("ScoreValueKind", new MetadataInfo<DvText>(TextType.Instance, new MetadataUtils.MetadataGetter<DvText>(this.GetScoreValueKind)));
			scoreMetadata.Add("ScoreColumnSetId", new MetadataInfo<uint>(MetadataUtils.ScoreColumnSetIdType, this.GetScoreColumnSetId(schema)));
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0003DF9C File Offset: 0x0003C19C
		private MetadataUtils.MetadataGetter<uint> GetScoreColumnSetId(ISchema schema)
		{
			int num;
			uint maxMetadataKind = MetadataUtils.GetMaxMetadataKind(schema, ref num, "ScoreColumnSetId", null);
			uint id = checked(maxMetadataKind + 1U);
			return delegate(int col, ref uint dst)
			{
				dst = id;
			};
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0003DFD3 File Offset: 0x0003C1D3
		private void GetScoreColumnKind(int col, ref DvText dst)
		{
			dst = new DvText("MultiOutputRegression");
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0003DFE5 File Offset: 0x0003C1E5
		private void GetScoreValueKind(int col, ref DvText dst)
		{
			dst = new DvText("Score");
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0003E094 File Offset: 0x0003C294
		private MetadataUtils.MetadataGetter<VBuffer<DvText>> CreateSlotNamesGetter(ISchema schema, int column, int length, string prefix)
		{
			ColumnType metadataTypeOrNull = schema.GetMetadataTypeOrNull("SlotNames", column);
			if (metadataTypeOrNull != null && metadataTypeOrNull.IsText)
			{
				return delegate(int col, ref VBuffer<DvText> dst)
				{
					schema.GetMetadata<VBuffer<DvText>>("SlotNames", column, ref dst);
				};
			}
			return delegate(int col, ref VBuffer<DvText> dst)
			{
				DvText[] array = dst.Values;
				if (Utils.Size<DvText>(array) < length)
				{
					array = new DvText[length];
				}
				for (int i = 0; i < length; i++)
				{
					array[i] = new DvText(string.Format("{0}_{1}", prefix, i));
				}
				dst = new VBuffer<DvText>(length, array, null);
			};
		}

		// Token: 0x04000632 RID: 1586
		public const string LoaderSignature = "MultiRegPerInstance";

		// Token: 0x04000633 RID: 1587
		private const int LabelCol = 0;

		// Token: 0x04000634 RID: 1588
		private const int ScoreCol = 1;

		// Token: 0x04000635 RID: 1589
		private const int L1Col = 2;

		// Token: 0x04000636 RID: 1590
		private const int L2Col = 3;

		// Token: 0x04000637 RID: 1591
		private const int DistCol = 4;

		// Token: 0x04000638 RID: 1592
		public const string L1 = "L1-loss";

		// Token: 0x04000639 RID: 1593
		public const string L2 = "L2-loss";

		// Token: 0x0400063A RID: 1594
		public const string Dist = "Euclidean-Distance";

		// Token: 0x0400063B RID: 1595
		private readonly ColumnType _labelType;

		// Token: 0x0400063C RID: 1596
		private readonly ColumnType _scoreType;

		// Token: 0x0400063D RID: 1597
		private readonly ColumnMetadataInfo _labelMetadata;

		// Token: 0x0400063E RID: 1598
		private readonly ColumnMetadataInfo _scoreMetadata;
	}
}
