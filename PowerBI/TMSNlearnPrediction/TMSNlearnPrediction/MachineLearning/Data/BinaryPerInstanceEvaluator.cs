using System;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200024D RID: 589
	public sealed class BinaryPerInstanceEvaluator : PerInstanceEvaluatorBase
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x00048417 File Offset: 0x00046617
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("BIN INST", 65537U, 65537U, 65537U, "BinaryPerInstance", null);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00048438 File Offset: 0x00046638
		public BinaryPerInstanceEvaluator(IHostEnvironment env, ISchema schema, string scoreCol, string probCol, string labelCol, float threshold, bool useRaw)
			: base(env, schema, scoreCol, labelCol)
		{
			this._threshold = threshold;
			this._useRaw = useRaw;
			using (IChannel channel = this._host.Start("Finding Input Columns"))
			{
				this._probCol = probCol;
				this._probIndex = -1;
				if (string.IsNullOrEmpty(this._probCol) || !schema.TryGetColumnIndex(this._probCol, ref this._probIndex))
				{
					channel.Warning("Data does not contain a probability column. Will not output the Log-loss column");
				}
				this.CheckInputColumnTypes(schema);
				channel.Done();
			}
			this._types = new ColumnType[2];
			this._types[1] = NumberType.R8;
			this._types[0] = BoolType.Instance;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000484FC File Offset: 0x000466FC
		private BinaryPerInstanceEvaluator(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
			: base(ctx, env, schema)
		{
			this._probCol = ctx.LoadStringOrNull();
			this._probIndex = -1;
			if (this._probCol != null && !schema.TryGetColumnIndex(this._probCol, ref this._probIndex))
			{
				throw Contracts.ExceptDecode(this._host, "Did not find the probability column '{0}'", new object[] { this._probCol });
			}
			this.CheckInputColumnTypes(schema);
			this._threshold = Utils.ReadFloat(ctx.Reader);
			this._useRaw = Utils.ReadBoolByte(ctx.Reader);
			Contracts.CheckDecode(this._host, !string.IsNullOrEmpty(this._probCol) || this._useRaw);
			Contracts.CheckDecode(this._host, FloatUtils.IsFinite(this._threshold));
			this._types = new ColumnType[2];
			this._types[1] = NumberType.R8;
			this._types[0] = BoolType.Instance;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000485E9 File Offset: 0x000467E9
		public static BinaryPerInstanceEvaluator Create(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(BinaryPerInstanceEvaluator.GetVersionInfo());
			return new BinaryPerInstanceEvaluator(ctx, env, schema);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00048618 File Offset: 0x00046818
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(BinaryPerInstanceEvaluator.GetVersionInfo());
			base.Save(ctx);
			ctx.SaveStringOrNull(this._probCol);
			ctx.Writer.Write(this._threshold);
			Utils.WriteBoolByte(ctx.Writer, this._useRaw);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00048724 File Offset: 0x00046924
		public override Func<int, bool> GetDependencies(Func<int, bool> activeOutput)
		{
			if (this._probIndex >= 0)
			{
				return (int col) => (activeOutput(1) && (col == this._probIndex || col == this._labelIndex)) || (activeOutput(0) && ((this._useRaw && col == this._scoreIndex) || (!this._useRaw && col == this._probIndex)));
			}
			return (int col) => activeOutput(0) && col == this._scoreIndex;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000488A4 File Offset: 0x00046AA4
		public override Delegate[] CreateGetters(IRowCursor input, Func<int, bool> activeCols)
		{
			long cachedPosition = -1L;
			float label = 0f;
			float prob = 0f;
			float score = 0f;
			ValueGetter<float> valueGetter = delegate(ref float value)
			{
				value = float.NaN;
			};
			ValueGetter<float> labelGetter = ((this._probIndex >= 0 && activeCols(1)) ? RowCursorUtils.GetLabelGetter(input, this._labelIndex) : valueGetter);
			ValueGetter<float> probGetter;
			if (this._probIndex >= 0 && activeCols(1))
			{
				probGetter = input.GetGetter<float>(this._probIndex);
			}
			else
			{
				probGetter = valueGetter;
			}
			ValueGetter<float> scoreGetter;
			if (activeCols(0) && this._scoreIndex >= 0)
			{
				scoreGetter = input.GetGetter<float>(this._scoreIndex);
			}
			else
			{
				scoreGetter = valueGetter;
			}
			Action updateCacheIfNeeded;
			Func<DvBool> getPredictedLabel;
			if (this._useRaw)
			{
				updateCacheIfNeeded = delegate
				{
					if (cachedPosition != input.Position)
					{
						labelGetter.Invoke(ref label);
						probGetter.Invoke(ref prob);
						scoreGetter.Invoke(ref score);
						cachedPosition = input.Position;
					}
				};
				getPredictedLabel = () => this.GetPredictedLabel(score);
			}
			else
			{
				updateCacheIfNeeded = delegate
				{
					if (cachedPosition != input.Position)
					{
						labelGetter.Invoke(ref label);
						probGetter.Invoke(ref prob);
						cachedPosition = input.Position;
					}
				};
				getPredictedLabel = () => this.GetPredictedLabel(prob);
			}
			Delegate[] array = ((this._probIndex >= 0) ? new Delegate[2] : new Delegate[1]);
			if (activeCols(0))
			{
				ValueGetter<DvBool> valueGetter2 = delegate(ref DvBool dst)
				{
					updateCacheIfNeeded();
					dst = getPredictedLabel();
				};
				array[(this._probIndex >= 0) ? 0 : 0] = valueGetter2;
			}
			if (this._probIndex >= 0 && activeCols(1))
			{
				ValueGetter<double> valueGetter3 = delegate(ref double dst)
				{
					updateCacheIfNeeded();
					dst = this.GetLogLoss(prob, label);
				};
				array[1] = valueGetter3;
			}
			return array;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00048AAC File Offset: 0x00046CAC
		private double GetLogLoss(float prob, float label)
		{
			if (float.IsNaN(prob) || float.IsNaN(label))
			{
				return double.NaN;
			}
			if (label > 0f)
			{
				return -Math.Log((double)prob, 2.0);
			}
			return -Math.Log(1.0 - (double)prob, 2.0);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00048B08 File Offset: 0x00046D08
		private DvBool GetPredictedLabel(float val)
		{
			if (TypeUtils.IsNA(val))
			{
				return DvBool.NA;
			}
			if (val <= this._threshold)
			{
				return DvBool.False;
			}
			return DvBool.True;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00048B2C File Offset: 0x00046D2C
		public override RowMapperColumnInfo[] GetOutputColumns()
		{
			if (this._probIndex >= 0)
			{
				RowMapperColumnInfo[] array = new RowMapperColumnInfo[]
				{
					null,
					new RowMapperColumnInfo("Log-loss", this._types[1], null)
				};
				array[0] = new RowMapperColumnInfo("Assigned", this._types[0], null);
				return array;
			}
			return new RowMapperColumnInfo[]
			{
				new RowMapperColumnInfo("Assigned", this._types[0], null)
			};
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00048B98 File Offset: 0x00046D98
		private void CheckInputColumnTypes(ISchema schema)
		{
			ColumnType columnType = schema.GetColumnType(this._labelIndex);
			if (columnType != NumberType.R4 && columnType != NumberType.R8 && columnType != BoolType.Instance && columnType.KeyCount != 2)
			{
				throw Contracts.Except(this._host, "Label column '{0}' has type '{1}' but must be R4, R8, BL or a 2-value key", new object[] { this._labelCol, columnType });
			}
			columnType = schema.GetColumnType(this._scoreIndex);
			if (columnType.IsVector || columnType.ItemType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type '{1}' but must be R4", new object[] { this._scoreCol, columnType });
			}
			if (this._probIndex >= 0)
			{
				columnType = schema.GetColumnType(this._probIndex);
				if (columnType.IsVector || columnType.ItemType != NumberType.Float)
				{
					throw Contracts.Except(this._host, "Probability column '{0}' has type '{1}' but must be R4", new object[] { this._probCol, columnType });
				}
			}
			else if (!this._useRaw)
			{
				throw Contracts.Except(this._host, "Cannot compute the predicted label from the probability column because it does not exist");
			}
		}

		// Token: 0x0400075C RID: 1884
		public const string LoaderSignature = "BinaryPerInstance";

		// Token: 0x0400075D RID: 1885
		private const int AssignedCol = 0;

		// Token: 0x0400075E RID: 1886
		private const int LogLossCol = 1;

		// Token: 0x0400075F RID: 1887
		public const string LogLoss = "Log-loss";

		// Token: 0x04000760 RID: 1888
		public const string Assigned = "Assigned";

		// Token: 0x04000761 RID: 1889
		private readonly string _probCol;

		// Token: 0x04000762 RID: 1890
		private readonly int _probIndex;

		// Token: 0x04000763 RID: 1891
		private readonly float _threshold;

		// Token: 0x04000764 RID: 1892
		private readonly bool _useRaw;

		// Token: 0x04000765 RID: 1893
		private readonly ColumnType[] _types;
	}
}
