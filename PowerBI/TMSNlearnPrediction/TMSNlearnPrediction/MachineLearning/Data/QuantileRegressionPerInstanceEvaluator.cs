using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001E2 RID: 482
	public sealed class QuantileRegressionPerInstanceEvaluator : PerInstanceEvaluatorBase
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x00038811 File Offset: 0x00036A11
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("QREGINST", 65537U, 65537U, 65537U, "QuantileRegPerInstance", null);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00038834 File Offset: 0x00036A34
		public QuantileRegressionPerInstanceEvaluator(IHostEnvironment env, ISchema schema, string scoreCol, string labelCol, int scoreSize, DvText[] quantiles)
			: base(env, schema, scoreCol, labelCol)
		{
			Contracts.CheckParam(this._host, scoreSize > 0, "scoreSize", "must be greater than 0");
			if (Utils.Size<DvText>(quantiles) != scoreSize)
			{
				throw Contracts.ExceptParam(this._host, "quantiles", "array must be of length '{0}'", new object[] { scoreSize });
			}
			this.CheckInputColumnTypes(schema);
			this._scoreSize = scoreSize;
			this._quantiles = quantiles;
			this._outputType = new VectorType(NumberType.R8, this._scoreSize);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000388C8 File Offset: 0x00036AC8
		private QuantileRegressionPerInstanceEvaluator(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
			: base(ctx, env, schema)
		{
			this.CheckInputColumnTypes(schema);
			this._scoreSize = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._scoreSize > 0);
			this._quantiles = new DvText[this._scoreSize];
			for (int i = 0; i < this._scoreSize; i++)
			{
				this._quantiles[i] = new DvText(ctx.LoadNonEmptyString());
			}
			this._outputType = new VectorType(NumberType.R8, this._scoreSize);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0003895E File Offset: 0x00036B5E
		public static QuantileRegressionPerInstanceEvaluator Create(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(QuantileRegressionPerInstanceEvaluator.GetVersionInfo());
			return new QuantileRegressionPerInstanceEvaluator(ctx, env, schema);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0003898C File Offset: 0x00036B8C
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(QuantileRegressionPerInstanceEvaluator.GetVersionInfo());
			base.Save(ctx);
			ctx.Writer.Write(this._scoreSize);
			for (int i = 0; i < this._scoreSize; i++)
			{
				ctx.SaveNonEmptyString(this._quantiles[i].ToString());
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00038A44 File Offset: 0x00036C44
		public override Func<int, bool> GetDependencies(Func<int, bool> activeOutput)
		{
			return (int col) => (activeOutput(0) || activeOutput(1)) && (col == this._scoreIndex || col == this._labelIndex);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00038A74 File Offset: 0x00036C74
		public override RowMapperColumnInfo[] GetOutputColumns()
		{
			RowMapperColumnInfo[] array = new RowMapperColumnInfo[2];
			VectorType vectorType = new VectorType(TextType.Instance, this._scoreSize);
			ColumnMetadataInfo columnMetadataInfo = new ColumnMetadataInfo("L1-loss");
			columnMetadataInfo.Add("SlotNames", new MetadataInfo<VBuffer<DvText>>(vectorType, this.CreateSlotNamesGetter("L1-loss")));
			ColumnMetadataInfo columnMetadataInfo2 = new ColumnMetadataInfo("L2-loss");
			columnMetadataInfo2.Add("SlotNames", new MetadataInfo<VBuffer<DvText>>(vectorType, this.CreateSlotNamesGetter("L2-loss")));
			array[0] = new RowMapperColumnInfo("L1-loss", this._outputType, columnMetadataInfo);
			array[1] = new RowMapperColumnInfo("L2-loss", this._outputType, columnMetadataInfo2);
			return array;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00038BBC File Offset: 0x00036DBC
		private MetadataUtils.MetadataGetter<VBuffer<DvText>> CreateSlotNamesGetter(string prefix)
		{
			return delegate(int col, ref VBuffer<DvText> dst)
			{
				DvText[] array = dst.Values;
				if (Utils.Size<DvText>(array) < this._scoreSize)
				{
					array = new DvText[this._scoreSize];
				}
				for (int i = 0; i < this._scoreSize; i++)
				{
					array[i] = new DvText(string.Format("{0} ({1})", prefix, this._quantiles[i]));
				}
				dst = new VBuffer<DvText>(this._scoreSize, array, null);
			};
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00038D48 File Offset: 0x00036F48
		public override Delegate[] CreateGetters(IRowCursor input, Func<int, bool> activeCols)
		{
			QuantileRegressionPerInstanceEvaluator.<>c__DisplayClass10 CS$<>8__locals1 = new QuantileRegressionPerInstanceEvaluator.<>c__DisplayClass10();
			CS$<>8__locals1.input = input;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cachedPosition = -1L;
			CS$<>8__locals1.label = 0f;
			CS$<>8__locals1.score = default(VBuffer<float>);
			CS$<>8__locals1.l1 = VBufferUtils.CreateDense<double>(this._scoreSize);
			ValueGetter<float> valueGetter = delegate(ref float value)
			{
				value = float.NaN;
			};
			CS$<>8__locals1.labelGetter = ((activeCols(0) || activeCols(1)) ? RowCursorUtils.GetLabelGetter(CS$<>8__locals1.input, this._labelIndex) : valueGetter);
			if (activeCols(0) || activeCols(1))
			{
				CS$<>8__locals1.scoreGetter = CS$<>8__locals1.input.GetGetter<VBuffer<float>>(this._scoreIndex);
			}
			else
			{
				CS$<>8__locals1.scoreGetter = delegate(ref VBuffer<float> dst)
				{
					dst = default(VBuffer<float>);
				};
			}
			CS$<>8__locals1.updateCacheIfNeeded = delegate
			{
				if (CS$<>8__locals1.cachedPosition != CS$<>8__locals1.input.Position)
				{
					CS$<>8__locals1.labelGetter.Invoke(ref CS$<>8__locals1.label);
					CS$<>8__locals1.scoreGetter.Invoke(ref CS$<>8__locals1.score);
					double num = (double)CS$<>8__locals1.label;
					foreach (KeyValuePair<int, float> keyValuePair in CS$<>8__locals1.score.Items(true))
					{
						CS$<>8__locals1.l1.Values[keyValuePair.Key] = Math.Abs(num - (double)keyValuePair.Value);
					}
					CS$<>8__locals1.cachedPosition = CS$<>8__locals1.input.Position;
				}
			};
			Delegate[] array = new Delegate[2];
			if (activeCols(0))
			{
				ValueGetter<VBuffer<double>> valueGetter2 = delegate(ref VBuffer<double> dst)
				{
					CS$<>8__locals1.updateCacheIfNeeded();
					CS$<>8__locals1.l1.CopyTo(ref dst);
				};
				array[0] = valueGetter2;
			}
			if (activeCols(1))
			{
				QuantileRegressionPerInstanceEvaluator.<>c__DisplayClass12 CS$<>8__locals2 = new QuantileRegressionPerInstanceEvaluator.<>c__DisplayClass12();
				CS$<>8__locals2.CS$<>8__locals11 = CS$<>8__locals1;
				QuantileRegressionPerInstanceEvaluator.<>c__DisplayClass12 CS$<>8__locals3 = CS$<>8__locals2;
				if (QuantileRegressionPerInstanceEvaluator.CS$<>9__CachedAnonymousMethodDelegatef == null)
				{
					QuantileRegressionPerInstanceEvaluator.CS$<>9__CachedAnonymousMethodDelegatef = delegate(int slot, double x, ref double y)
					{
						y = x * x;
					};
				}
				CS$<>8__locals3.sqr = QuantileRegressionPerInstanceEvaluator.CS$<>9__CachedAnonymousMethodDelegatef;
				ValueGetter<VBuffer<double>> valueGetter3 = delegate(ref VBuffer<double> dst)
				{
					CS$<>8__locals2.CS$<>8__locals11.updateCacheIfNeeded();
					dst = new VBuffer<double>(CS$<>8__locals2.CS$<>8__locals11.<>4__this._scoreSize, 0, dst.Values, dst.Indices);
					VBufferUtils.ApplyWith<double, double>(ref CS$<>8__locals2.CS$<>8__locals11.l1, ref dst, CS$<>8__locals2.sqr);
				};
				array[1] = valueGetter3;
			}
			return array;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00038ED0 File Offset: 0x000370D0
		private void CheckInputColumnTypes(ISchema schema)
		{
			ColumnType columnType = schema.GetColumnType(this._labelIndex);
			if (columnType != NumberType.R4)
			{
				throw Contracts.Except(this._host, "Label column '{0}' has type '{1}' but must be R4", new object[] { this._labelCol, columnType });
			}
			columnType = schema.GetColumnType(this._scoreIndex);
			if (columnType.VectorSize == 0 || (columnType.ItemType != NumberType.R4 && columnType.ItemType != NumberType.R8))
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type '{1}' but must be a known length vector of type R4 or R8", new object[] { this._scoreCol, columnType });
			}
		}

		// Token: 0x04000583 RID: 1411
		public const string LoaderSignature = "QuantileRegPerInstance";

		// Token: 0x04000584 RID: 1412
		private const int L1Col = 0;

		// Token: 0x04000585 RID: 1413
		private const int L2Col = 1;

		// Token: 0x04000586 RID: 1414
		public const string L1 = "L1-loss";

		// Token: 0x04000587 RID: 1415
		public const string L2 = "L2-loss";

		// Token: 0x04000588 RID: 1416
		private readonly int _scoreSize;

		// Token: 0x04000589 RID: 1417
		private readonly DvText[] _quantiles;

		// Token: 0x0400058A RID: 1418
		private readonly ColumnType _outputType;
	}
}
