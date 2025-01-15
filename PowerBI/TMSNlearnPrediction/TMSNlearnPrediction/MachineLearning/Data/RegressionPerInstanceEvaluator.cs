using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000299 RID: 665
	public sealed class RegressionPerInstanceEvaluator : PerInstanceEvaluatorBase
	{
		// Token: 0x06000F51 RID: 3921 RVA: 0x00053D0E File Offset: 0x00051F0E
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("REG INST", 65537U, 65537U, 65537U, "RegressionPerInstance", null);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00053D2F File Offset: 0x00051F2F
		public RegressionPerInstanceEvaluator(IHostEnvironment env, ISchema schema, string scoreCol, string labelCol)
			: base(env, schema, scoreCol, labelCol)
		{
			this.CheckInputColumnTypes(schema);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00053D43 File Offset: 0x00051F43
		private RegressionPerInstanceEvaluator(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
			: base(ctx, env, schema)
		{
			this.CheckInputColumnTypes(schema);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00053D55 File Offset: 0x00051F55
		public static RegressionPerInstanceEvaluator Create(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(RegressionPerInstanceEvaluator.GetVersionInfo());
			return new RegressionPerInstanceEvaluator(ctx, env, schema);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00053D81 File Offset: 0x00051F81
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(RegressionPerInstanceEvaluator.GetVersionInfo());
			base.Save(ctx);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00053DEC File Offset: 0x00051FEC
		public override Func<int, bool> GetDependencies(Func<int, bool> activeOutput)
		{
			return (int col) => (activeOutput(0) || activeOutput(1)) && (col == this._scoreIndex || col == this._labelIndex);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00053E1C File Offset: 0x0005201C
		public override RowMapperColumnInfo[] GetOutputColumns()
		{
			return new RowMapperColumnInfo[]
			{
				new RowMapperColumnInfo("L1-loss", NumberType.R8, null),
				new RowMapperColumnInfo("L2-loss", NumberType.R8, null)
			};
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00053F08 File Offset: 0x00052108
		public override Delegate[] CreateGetters(IRowCursor input, Func<int, bool> activeCols)
		{
			long cachedPosition = -1L;
			float label = 0f;
			float score = 0f;
			ValueGetter<float> valueGetter = delegate(ref float value)
			{
				value = float.NaN;
			};
			ValueGetter<float> labelGetter = ((activeCols(0) || activeCols(1)) ? RowCursorUtils.GetLabelGetter(input, this._labelIndex) : valueGetter);
			ValueGetter<float> scoreGetter;
			if (activeCols(0) || activeCols(1))
			{
				scoreGetter = input.GetGetter<float>(this._scoreIndex);
			}
			else
			{
				scoreGetter = valueGetter;
			}
			Action updateCacheIfNeeded = delegate
			{
				if (cachedPosition != input.Position)
				{
					labelGetter.Invoke(ref label);
					scoreGetter.Invoke(ref score);
					cachedPosition = input.Position;
				}
			};
			Delegate[] array = new Delegate[2];
			if (activeCols(0))
			{
				ValueGetter<double> valueGetter2 = delegate(ref double dst)
				{
					updateCacheIfNeeded();
					dst = Math.Abs((double)label - (double)score);
				};
				array[0] = valueGetter2;
			}
			if (activeCols(1))
			{
				ValueGetter<double> valueGetter3 = delegate(ref double dst)
				{
					updateCacheIfNeeded();
					dst = Math.Abs((double)label - (double)score);
					dst *= dst;
				};
				array[1] = valueGetter3;
			}
			return array;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00054030 File Offset: 0x00052230
		private void CheckInputColumnTypes(ISchema schema)
		{
			ColumnType columnType = schema.GetColumnType(this._labelIndex);
			if (columnType != NumberType.R4)
			{
				throw Contracts.Except(this._host, "Label column '{0}' has type '{1}' but must be R4", new object[] { this._labelCol, columnType });
			}
			columnType = schema.GetColumnType(this._scoreIndex);
			if (columnType.IsVector || columnType.ItemType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type '{1}' but must be R4", new object[] { this._scoreCol, columnType });
			}
		}

		// Token: 0x0400085A RID: 2138
		public const string LoaderSignature = "RegressionPerInstance";

		// Token: 0x0400085B RID: 2139
		private const int L1Col = 0;

		// Token: 0x0400085C RID: 2140
		private const int L2Col = 1;

		// Token: 0x0400085D RID: 2141
		public const string L1 = "L1-loss";

		// Token: 0x0400085E RID: 2142
		public const string L2 = "L2-loss";
	}
}
