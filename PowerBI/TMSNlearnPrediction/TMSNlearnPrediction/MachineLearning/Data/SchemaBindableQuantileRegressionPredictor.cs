using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000227 RID: 551
	public sealed class SchemaBindableQuantileRegressionPredictor : SchemaBindablePredictorWrapperBase
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x00043581 File Offset: 0x00041781
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("QRSCHBND", 65538U, 65538U, 65538U, "QuantileSchemaBindable", null);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x000435A2 File Offset: 0x000417A2
		public SchemaBindableQuantileRegressionPredictor(IPredictor predictor, double[] quantiles)
			: base(predictor)
		{
			this._quantiles = quantiles;
			this.CheckValid(out this._qpred);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x000435BE File Offset: 0x000417BE
		private SchemaBindableQuantileRegressionPredictor(ModelLoadContext ctx, IHostEnvironment env)
			: base(ctx, env)
		{
			this._quantiles = Utils.ReadDoubleArray(ctx.Reader);
			this.CheckValid(out this._qpred);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000435E5 File Offset: 0x000417E5
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.SetVersionInfo(SchemaBindableQuantileRegressionPredictor.GetVersionInfo());
			base.Save(ctx);
			Utils.WriteDoubleArray(ctx.Writer, this._quantiles);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00043615 File Offset: 0x00041815
		public static SchemaBindableQuantileRegressionPredictor Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<ModelLoadContext>(ctx, "ctx");
			ctx.CheckAtModel(SchemaBindableQuantileRegressionPredictor.GetVersionInfo());
			return new SchemaBindableQuantileRegressionPredictor(ctx, env);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00043634 File Offset: 0x00041834
		private void CheckValid(out IQuantileValueMapper qpred)
		{
			qpred = this._predictor as IQuantileValueMapper;
			Contracts.Check(this._qpred != null, "Predictor doesn't implement expected quantile interface");
			Contracts.CheckNonEmpty<double>(this._quantiles, "quantiles", "Quantiles must not be empty");
			Contracts.Check(this._scoreType == NumberType.Float, "Unexpected predictor output type");
			Contracts.Check(this._valueMapper != null && this._valueMapper.InputType.IsVector && this._valueMapper.InputType.ItemType == NumberType.Float, "Unexpected predictor input type");
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000436CF File Offset: 0x000418CF
		protected override ISchemaBoundMapper BindCore(IChannel ch, RoleMappedSchema schema)
		{
			return new SchemaBindablePredictorWrapperBase.SingleValueRowMapper(schema, this, new SchemaBindableQuantileRegressionPredictor.Schema(this._scoreType, this._quantiles));
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00043748 File Offset: 0x00041948
		protected override Delegate GetPredictionGetter(IRow input, int colSrc)
		{
			input.Schema.GetColumnType(colSrc);
			ValueGetter<VBuffer<float>> featureGetter = input.GetGetter<VBuffer<float>>(colSrc);
			int featureCount = ((this._valueMapper != null) ? this._valueMapper.InputType.VectorSize : 0);
			float[] array = new float[this._quantiles.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (float)this._quantiles[i];
			}
			ValueMapper<VBuffer<float>, VBuffer<float>> map = this._qpred.GetMapper(array);
			VBuffer<float> features = default(VBuffer<float>);
			return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> value)
			{
				featureGetter.Invoke(ref features);
				Contracts.Check(features.Length == featureCount || featureCount == 0);
				map.Invoke(ref features, ref value);
			});
		}

		// Token: 0x040006C1 RID: 1729
		public const string LoaderSignature = "QuantileSchemaBindable";

		// Token: 0x040006C2 RID: 1730
		private readonly IQuantileValueMapper _qpred;

		// Token: 0x040006C3 RID: 1731
		private readonly double[] _quantiles;

		// Token: 0x02000228 RID: 552
		private sealed class Schema : ScoreMapperSchemaBase
		{
			// Token: 0x06000C6C RID: 3180 RVA: 0x000437EC File Offset: 0x000419EC
			public Schema(ColumnType scoreType, double[] quantiles)
				: base(scoreType, "QuantileRegression")
			{
				this._slotNames = new string[quantiles.Length];
				for (int i = 0; i < this._slotNames.Length; i++)
				{
					this._slotNames[i] = string.Format("Quantile-{0}", quantiles[i]);
				}
				this._getSlotNames = new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetSlotNames);
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00043852 File Offset: 0x00041A52
			public override int ColumnCount
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x06000C6E RID: 3182 RVA: 0x00043858 File Offset: 0x00041A58
			public override IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				IEnumerable<KeyValuePair<string, ColumnType>> metadataTypes = base.GetMetadataTypes(col);
				return MetadataUtils.Prepend<KeyValuePair<string, ColumnType>>(metadataTypes, new KeyValuePair<string, ColumnType>[] { MetadataUtils.GetSlotNamesPair(this._slotNames.Length) });
			}

			// Token: 0x06000C6F RID: 3183 RVA: 0x000438B0 File Offset: 0x00041AB0
			public override ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				Contracts.CheckNonEmpty(kind, "kind");
				if (kind == "SlotNames")
				{
					return MetadataUtils.GetNamesType(this._slotNames.Length);
				}
				return base.GetMetadataTypeOrNull(kind, col);
			}

			// Token: 0x06000C70 RID: 3184 RVA: 0x00043908 File Offset: 0x00041B08
			public override void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				Contracts.CheckNonEmpty(kind, "kind");
				if (kind == "SlotNames")
				{
					MetadataUtils.Marshal<VBuffer<DvText>, TValue>(this._getSlotNames, col, ref value);
					return;
				}
				base.GetMetadata<TValue>(kind, col, ref value);
			}

			// Token: 0x06000C71 RID: 3185 RVA: 0x0004395F File Offset: 0x00041B5F
			public override ColumnType GetColumnType(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return new VectorType(NumberType.Float, this._slotNames.Length);
			}

			// Token: 0x06000C72 RID: 3186 RVA: 0x00043990 File Offset: 0x00041B90
			private void GetSlotNames(int iinfo, ref VBuffer<DvText> dst)
			{
				int num = Utils.Size<string>(this._slotNames);
				DvText[] array = dst.Values;
				if (Utils.Size<DvText>(array) < num)
				{
					array = new DvText[num];
				}
				for (int i = 0; i < this._slotNames.Length; i++)
				{
					array[i] = new DvText(this._slotNames[i]);
				}
				dst = new VBuffer<DvText>(num, array, dst.Indices);
			}

			// Token: 0x040006C4 RID: 1732
			private readonly string[] _slotNames;

			// Token: 0x040006C5 RID: 1733
			private readonly MetadataUtils.MetadataGetter<VBuffer<DvText>> _getSlotNames;
		}
	}
}
