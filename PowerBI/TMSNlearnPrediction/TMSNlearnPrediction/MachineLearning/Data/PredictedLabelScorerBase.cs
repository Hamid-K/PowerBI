using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200007C RID: 124
	public abstract class PredictedLabelScorerBase : RowToRowScorerBase
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000D1BB File Offset: 0x0000B3BB
		protected override RowToRowScorerBase.BindingsBase GetBindings()
		{
			return this._bindings;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		protected PredictedLabelScorerBase(ScorerArgumentsBase args, IHostEnvironment env, IDataView data, ISchemaBoundMapper mapper, string registrationName, string scoreColKind, string scoreColName, Func<ColumnType, bool> outputTypeMatches, Func<ColumnType, ColumnType> getPredColType)
			: base(env, data, registrationName, Contracts.CheckRef<ISchemaBoundMapper>(mapper, "mapper").Bindable)
		{
			Contracts.CheckValue<ScorerArgumentsBase>(this._host, args, "args");
			Contracts.CheckNonEmpty(this._host, scoreColKind, "scoreColKind");
			Contracts.CheckNonEmpty(this._host, scoreColName, "scoreColName");
			Contracts.CheckValue<Func<ColumnType, bool>>(this._host, outputTypeMatches, "outputTypeMatches");
			Contracts.CheckValue<Func<ColumnType, ColumnType>>(this._host, getPredColType, "getPredColType");
			ISchemaBoundRowMapper schemaBoundRowMapper = mapper as ISchemaBoundRowMapper;
			Contracts.CheckUserArg(this._host, schemaBoundRowMapper != null, "mapper", "mapper should implement ISchemaBoundRowMapper");
			int num;
			if (!mapper.OutputSchema.TryGetColumnIndex(scoreColName, ref num))
			{
				throw Contracts.ExceptUserArg(this._host, "scoreColName", "mapper does not contain a column '{0}'", new object[] { scoreColName });
			}
			ColumnType columnType = mapper.OutputSchema.GetColumnType(num);
			Contracts.Check(this._host, outputTypeMatches(columnType), "Unexpected predictor output type");
			ColumnType columnType2 = getPredColType(columnType);
			this._bindings = PredictedLabelScorerBase.Bindings.Create(data.Schema, schemaBoundRowMapper, args.suffix, scoreColKind, num, columnType2);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D2EE File Offset: 0x0000B4EE
		protected PredictedLabelScorerBase(IHostEnvironment env, PredictedLabelScorerBase transform, IDataView newSource, string registrationName)
			: base(env, newSource, registrationName, transform._bindable)
		{
			this._bindings = transform._bindings.ApplyToSchema(newSource.Schema, this._bindable, env);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D31E File Offset: 0x0000B51E
		protected PredictedLabelScorerBase(ModelLoadContext ctx, IHost host, IDataView input, Func<ColumnType, bool> outputTypeMatches, Func<ColumnType, ColumnType> getPredColType)
			: base(ctx, host, input)
		{
			this._bindings = PredictedLabelScorerBase.Bindings.Create(ctx, input.Schema, host, this._bindable, outputTypeMatches, getPredColType);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D346 File Offset: 0x0000B546
		protected override void SaveCore(ModelSaveContext ctx)
		{
			this._bindings.Save(ctx);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D354 File Offset: 0x0000B554
		protected override bool WantParallelCursors(Func<int, bool> predicate)
		{
			return this._bindings.AnyNewColumnsActive(predicate);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D364 File Offset: 0x0000B564
		protected override Delegate[] GetGetters(IRow output, Func<int, bool> predicate)
		{
			Delegate[] array = new Delegate[this._bindings.InfoCount];
			int derivedColumnCount = this._bindings.DerivedColumnCount;
			Delegate @delegate = null;
			if (predicate(0))
			{
				array[0] = this.GetPredictedLabelGetter(output, out @delegate);
			}
			for (int i = derivedColumnCount; i < array.Length; i++)
			{
				if (predicate(i))
				{
					if (i == derivedColumnCount + this._bindings.ScoreColumnIndex && @delegate != null)
					{
						array[i] = @delegate;
					}
					else
					{
						array[i] = RowToRowScorerBase.GetGetterFromRow(output, i - derivedColumnCount);
					}
				}
			}
			return array;
		}

		// Token: 0x0600023A RID: 570
		protected abstract Delegate GetPredictedLabelGetter(IRow output, out Delegate scoreGetter);

		// Token: 0x0600023B RID: 571 RVA: 0x0000D3E2 File Offset: 0x0000B5E2
		protected void EnsureCachedPosition<TScore>(ref long cachedPosition, ref TScore score, IRow boundRow, ValueGetter<TScore> scoreGetter)
		{
			if (cachedPosition != boundRow.Position)
			{
				scoreGetter.Invoke(ref score);
				cachedPosition = boundRow.Position;
			}
		}

		// Token: 0x040000CA RID: 202
		protected PredictedLabelScorerBase.Bindings _bindings;

		// Token: 0x0200007E RID: 126
		public abstract class ThresholdArgumentsBase : ScorerArgumentsBase
		{
			// Token: 0x040000CC RID: 204
			[Argument(0, HelpText = "Value for classification thresholding", ShortName = "t")]
			public float threshold;

			// Token: 0x040000CD RID: 205
			[Argument(0, HelpText = "Specify which predictor output to use for classification thresholding", ShortName = "tcol")]
			public string thresholdColumn = "Score";
		}

		// Token: 0x0200007F RID: 127
		protected sealed class Bindings : RowToRowScorerBase.BindingsBase
		{
			// Token: 0x0600023E RID: 574 RVA: 0x0000D41C File Offset: 0x0000B61C
			private Bindings(ISchema input, ISchemaBoundRowMapper mapper, string suffix, string scoreColumnKind, bool user, int scoreColIndex, ColumnType predColType)
				: base(input, mapper, suffix, user, new string[] { "PredictedLabel" })
			{
				this.ScoreColumnIndex = scoreColIndex;
				this.ScoreColumnKind = scoreColumnKind;
				this.PredColType = predColType;
				this._getScoreColumnKind = new MetadataUtils.MetadataGetter<DvText>(this.GetScoreColumnKind);
				this._getScoreValueKind = new MetadataUtils.MetadataGetter<DvText>(this.GetScoreValueKind);
			}

			// Token: 0x0600023F RID: 575 RVA: 0x0000D480 File Offset: 0x0000B680
			public static PredictedLabelScorerBase.Bindings Create(ISchema input, ISchemaBoundRowMapper mapper, string suffix, string scoreColKind, int scoreColIndex, ColumnType predColType)
			{
				return new PredictedLabelScorerBase.Bindings(input, mapper, suffix, scoreColKind, true, scoreColIndex, predColType);
			}

			// Token: 0x06000240 RID: 576 RVA: 0x0000D490 File Offset: 0x0000B690
			public PredictedLabelScorerBase.Bindings ApplyToSchema(ISchema input, ISchemaBindableMapper bindable, IHostEnvironment env)
			{
				string columnName = this.RowMapper.OutputSchema.GetColumnName(this.ScoreColumnIndex);
				RoleMappedSchema roleMappedSchema = RoleMappedSchema.Create(input, this.RowMapper.GetInputColumnRoles());
				ISchemaBoundMapper schemaBoundMapper = bindable.Bind(env, roleMappedSchema);
				ISchemaBoundRowMapper schemaBoundRowMapper = schemaBoundMapper as ISchemaBoundRowMapper;
				Contracts.CheckValue<ISchemaBoundRowMapper>(env, schemaBoundRowMapper, "predictor", "Mapper must implement ISchemaBoundRowMapper");
				int num;
				bool flag = schemaBoundRowMapper.OutputSchema.TryGetColumnIndex(columnName, ref num);
				Contracts.Check(env, flag, "Mapper doesn't have expected score column");
				return new PredictedLabelScorerBase.Bindings(input, schemaBoundRowMapper, this.Suffix, this.ScoreColumnKind, true, num, this.PredColType);
			}

			// Token: 0x06000241 RID: 577 RVA: 0x0000D524 File Offset: 0x0000B724
			public static PredictedLabelScorerBase.Bindings Create(ModelLoadContext ctx, ISchema input, IHostEnvironment env, ISchemaBindableMapper bindable, Func<ColumnType, bool> outputTypeMatches, Func<ColumnType, ColumnType> getPredColType)
			{
				string text;
				KeyValuePair<RoleMappedSchema.ColumnRole, string>[] array = ScorerBindingsBase.LoadBaseInfo(ctx, out text);
				string text2 = ctx.LoadNonEmptyString();
				string text3 = ctx.LoadNonEmptyString();
				ISchemaBoundMapper schemaBoundMapper = bindable.Bind(env, RoleMappedSchema.Create(input, array));
				ISchemaBoundRowMapper schemaBoundRowMapper = schemaBoundMapper as ISchemaBoundRowMapper;
				Contracts.CheckDecode(env, schemaBoundRowMapper != null, "Predictor expected to be a RowMapper!");
				int num;
				Contracts.CheckDecode(env, schemaBoundMapper.OutputSchema.TryGetColumnIndex(text3, ref num));
				ColumnType columnType = schemaBoundMapper.OutputSchema.GetColumnType(num);
				Contracts.CheckDecode(env, outputTypeMatches(columnType));
				ColumnType columnType2 = getPredColType(columnType);
				return new PredictedLabelScorerBase.Bindings(input, schemaBoundRowMapper, text, text2, false, num, columnType2);
			}

			// Token: 0x06000242 RID: 578 RVA: 0x0000D5C2 File Offset: 0x0000B7C2
			public override void Save(ModelSaveContext ctx)
			{
				base.SaveBase(ctx);
				ctx.SaveNonEmptyString(this.ScoreColumnKind);
				ctx.SaveNonEmptyString(this.RowMapper.OutputSchema.GetColumnName(this.ScoreColumnIndex));
			}

			// Token: 0x06000243 RID: 579 RVA: 0x0000D5F3 File Offset: 0x0000B7F3
			protected override ColumnType GetColumnTypeCore(int iinfo)
			{
				if (iinfo < this.DerivedColumnCount)
				{
					return this.PredColType;
				}
				return base.GetColumnTypeCore(iinfo);
			}

			// Token: 0x06000244 RID: 580 RVA: 0x0000D844 File Offset: 0x0000BA44
			protected override IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypesCore(int iinfo)
			{
				yield return MetadataUtils.GetPair(TextType.Instance, "ScoreColumnKind");
				if (iinfo < this.DerivedColumnCount)
				{
					yield return MetadataUtils.GetPair(TextType.Instance, "ScoreValueKind");
				}
				foreach (KeyValuePair<string, ColumnType> pair in base.GetMetadataTypesCore(iinfo))
				{
					KeyValuePair<string, ColumnType> keyValuePair = pair;
					if (keyValuePair.Key != "ScoreColumnKind")
					{
						yield return pair;
					}
				}
				yield break;
			}

			// Token: 0x06000245 RID: 581 RVA: 0x0000D868 File Offset: 0x0000BA68
			protected override ColumnType GetMetadataTypeCore(string kind, int iinfo)
			{
				if (kind == "ScoreColumnKind")
				{
					return TextType.Instance;
				}
				if (iinfo < this.DerivedColumnCount && kind == "ScoreValueKind")
				{
					return TextType.Instance;
				}
				return base.GetMetadataTypeCore(kind, iinfo);
			}

			// Token: 0x06000246 RID: 582 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
			protected override void GetMetadataCore<TValue>(string kind, int iinfo, ref TValue value)
			{
				if (kind == "ScoreColumnKind")
				{
					MetadataUtils.Marshal<DvText, TValue>(this._getScoreColumnKind, iinfo, ref value);
					return;
				}
				if (iinfo < this.DerivedColumnCount && kind == "ScoreValueKind")
				{
					MetadataUtils.Marshal<DvText, TValue>(this._getScoreValueKind, iinfo, ref value);
					return;
				}
				base.GetMetadataCore<TValue>(kind, iinfo, ref value);
			}

			// Token: 0x06000247 RID: 583 RVA: 0x0000D8F9 File Offset: 0x0000BAF9
			private void GetScoreColumnKind(int iinfo, ref DvText dst)
			{
				dst = new DvText(this.ScoreColumnKind);
			}

			// Token: 0x06000248 RID: 584 RVA: 0x0000D90C File Offset: 0x0000BB0C
			private void GetScoreValueKind(int iinfo, ref DvText dst)
			{
				dst = new DvText("PredictedLabel");
			}

			// Token: 0x06000249 RID: 585 RVA: 0x0000D95C File Offset: 0x0000BB5C
			public override Func<int, bool> GetActiveMapperColumns(bool[] active)
			{
				Func<int, bool> pred = base.GetActiveMapperColumns(active);
				return (int col) => pred(col) || (col == this.ScoreColumnIndex && active[this.MapIinfoToCol(0)]);
			}

			// Token: 0x040000CE RID: 206
			public readonly int ScoreColumnIndex;

			// Token: 0x040000CF RID: 207
			public readonly ColumnType PredColType;

			// Token: 0x040000D0 RID: 208
			public readonly string ScoreColumnKind;

			// Token: 0x040000D1 RID: 209
			private readonly MetadataUtils.MetadataGetter<DvText> _getScoreColumnKind;

			// Token: 0x040000D2 RID: 210
			private readonly MetadataUtils.MetadataGetter<DvText> _getScoreValueKind;
		}
	}
}
