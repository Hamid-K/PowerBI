using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000224 RID: 548
	public sealed class SchemaBindableBinaryPredictorWrapper : SchemaBindablePredictorWrapperBase
	{
		// Token: 0x06000C48 RID: 3144 RVA: 0x00042E5E File Offset: 0x0004105E
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("BINSCHBD", 65538U, 65538U, 65538U, "BinarySchemaBindable", null);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00042E7F File Offset: 0x0004107F
		public SchemaBindableBinaryPredictorWrapper(IPredictor predictor)
			: base(predictor)
		{
			this.CheckValid(out this._distMapper);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00042E94 File Offset: 0x00041094
		private SchemaBindableBinaryPredictorWrapper(ModelLoadContext ctx, IHostEnvironment env)
			: base(ctx, env)
		{
			this.CheckValid(out this._distMapper);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00042EAA File Offset: 0x000410AA
		public static SchemaBindableBinaryPredictorWrapper Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<ModelLoadContext>(ctx, "ctx");
			ctx.CheckAtModel(SchemaBindableBinaryPredictorWrapper.GetVersionInfo());
			return new SchemaBindableBinaryPredictorWrapper(ctx, env);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00042EC9 File Offset: 0x000410C9
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.SetVersionInfo(SchemaBindableBinaryPredictorWrapper.GetVersionInfo());
			base.Save(ctx);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00042EE8 File Offset: 0x000410E8
		private void CheckValid(out IValueMapperDist distMapper)
		{
			Contracts.Check(this._scoreType == NumberType.Float, "Expected predictor result type to be Float");
			distMapper = this._predictor as IValueMapperDist;
			if (distMapper != null)
			{
				Contracts.Check(distMapper.InputType.IsVector && distMapper.InputType.ItemType == NumberType.Float, "Invalid input type for the IValueMapperDist");
				Contracts.Check(distMapper.DistType == NumberType.Float, "Invalid probability type for the IValueMapperDist");
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00042F64 File Offset: 0x00041164
		protected override ISchemaBoundMapper BindCore(IChannel ch, RoleMappedSchema schema)
		{
			if (this._predictor.PredictionKind != 2)
			{
				ch.Warning("Scoring predictor of kind '{0}' as '{1}'.", new object[]
				{
					this._predictor.PredictionKind,
					2
				});
			}
			if (this._distMapper != null)
			{
				return new SchemaBindableBinaryPredictorWrapper.CalibratedRowMapper(schema, this);
			}
			ch.Warning("Predictor does not provide probabilites.");
			return new SchemaBindablePredictorWrapperBase.SingleValueRowMapper(schema, this, new ScoreMapperSchema(NumberType.Float, "BinaryClassification"));
		}

		// Token: 0x040006BB RID: 1723
		public const string LoaderSignature = "BinarySchemaBindable";

		// Token: 0x040006BC RID: 1724
		private readonly IValueMapperDist _distMapper;

		// Token: 0x02000225 RID: 549
		private sealed class CalibratedRowMapper : ISchemaBoundRowMapper, ISchemaBoundMapper
		{
			// Token: 0x06000C4F RID: 3151 RVA: 0x00042FE0 File Offset: 0x000411E0
			public CalibratedRowMapper(RoleMappedSchema schema, SchemaBindableBinaryPredictorWrapper parent)
			{
				this._parent = parent;
				this._inputSchema = schema;
				this._outputSchema = new SchemaBindableBinaryPredictorWrapper.CalibratedRowMapper.Schema();
				ColumnType type = this._inputSchema.Feature.Type;
				Contracts.Check(type.IsKnownSizeVector && type.ItemType == NumberType.Float, "Invalid feature column type");
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0004303F File Offset: 0x0004123F
			public RoleMappedSchema InputSchema
			{
				get
				{
					return this._inputSchema;
				}
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00043047 File Offset: 0x00041247
			public ISchema OutputSchema
			{
				get
				{
					return this._outputSchema;
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0004304F File Offset: 0x0004124F
			public ISchemaBindableMapper Bindable
			{
				get
				{
					return this._parent;
				}
			}

			// Token: 0x06000C53 RID: 3155 RVA: 0x00043070 File Offset: 0x00041270
			public Func<int, bool> GetDependencies(Func<int, bool> predicate)
			{
				for (int i = 0; i < this.OutputSchema.ColumnCount; i++)
				{
					if (predicate(i))
					{
						return (int col) => col == this._inputSchema.Feature.Index;
					}
				}
				return (int col) => false;
			}

			// Token: 0x06000C54 RID: 3156 RVA: 0x000431B8 File Offset: 0x000413B8
			public IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> GetInputColumnRoles()
			{
				yield return RoleMappedSchema.ColumnRole.Feature.Bind(this._inputSchema.Feature.Name);
				yield break;
			}

			// Token: 0x06000C55 RID: 3157 RVA: 0x00043264 File Offset: 0x00041464
			private Delegate[] CreateGetters(IRow input, bool[] active)
			{
				Delegate[] array = new Delegate[2];
				if (active[0] || active[1])
				{
					ValueGetter<VBuffer<float>> featureGetter = input.GetGetter<VBuffer<float>>(this._inputSchema.Feature.Index);
					float prob = 0f;
					float score = 0f;
					long cachedPosition = -1L;
					VBuffer<float> features = default(VBuffer<float>);
					ValueMapper<VBuffer<float>, float, float> mapper = this._parent._distMapper.GetMapper<VBuffer<float>, float, float>();
					if (active[0])
					{
						ValueGetter<float> valueGetter = delegate(ref float dst)
						{
							SchemaBindableBinaryPredictorWrapper.CalibratedRowMapper.EnsureCachedResultValueMapper(mapper, cachedPosition, featureGetter, ref features, ref score, ref prob, input);
							dst = score;
						};
						array[0] = valueGetter;
					}
					if (active[1])
					{
						ValueGetter<float> valueGetter2 = delegate(ref float dst)
						{
							SchemaBindableBinaryPredictorWrapper.CalibratedRowMapper.EnsureCachedResultValueMapper(mapper, cachedPosition, featureGetter, ref features, ref score, ref prob, input);
							dst = prob;
						};
						array[1] = valueGetter2;
					}
				}
				return array;
			}

			// Token: 0x06000C56 RID: 3158 RVA: 0x0004334D File Offset: 0x0004154D
			private static void EnsureCachedResultValueMapper(ValueMapper<VBuffer<float>, float, float> mapper, long cachedPosition, ValueGetter<VBuffer<float>> featureGetter, ref VBuffer<float> features, ref float score, ref float prob, IRow input)
			{
				if (cachedPosition != input.Position)
				{
					featureGetter.Invoke(ref features);
					mapper.Invoke(ref features, ref score, ref prob);
					cachedPosition = input.Position;
				}
			}

			// Token: 0x06000C57 RID: 3159 RVA: 0x00043374 File Offset: 0x00041574
			public IRow GetOutputRow(IRow input, Func<int, bool> predicate)
			{
				bool[] array = Utils.BuildArray<bool>(this.OutputSchema.ColumnCount, predicate);
				Delegate[] array2 = this.CreateGetters(input, array);
				return new SimpleRow(this.OutputSchema, input, array2);
			}

			// Token: 0x040006BD RID: 1725
			private readonly SchemaBindableBinaryPredictorWrapper _parent;

			// Token: 0x040006BE RID: 1726
			private readonly RoleMappedSchema _inputSchema;

			// Token: 0x040006BF RID: 1727
			private readonly ScoreMapperSchemaBase _outputSchema;

			// Token: 0x02000226 RID: 550
			private sealed class Schema : ScoreMapperSchemaBase
			{
				// Token: 0x1700016D RID: 365
				// (get) Token: 0x06000C5A RID: 3162 RVA: 0x000433A9 File Offset: 0x000415A9
				public override int ColumnCount
				{
					get
					{
						return 2;
					}
				}

				// Token: 0x06000C5B RID: 3163 RVA: 0x000433AC File Offset: 0x000415AC
				public Schema()
					: base(NumberType.Float, "BinaryClassification")
				{
				}

				// Token: 0x06000C5C RID: 3164 RVA: 0x000433BE File Offset: 0x000415BE
				public override ColumnType GetColumnType(int col)
				{
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					if (col == 1)
					{
						return NumberType.Float;
					}
					return base.GetColumnType(col);
				}

				// Token: 0x06000C5D RID: 3165 RVA: 0x000433EB File Offset: 0x000415EB
				public override bool TryGetColumnIndex(string name, out int col)
				{
					Contracts.CheckValue<string>(name, "name");
					if (name == "Probability")
					{
						col = 1;
						return true;
					}
					return base.TryGetColumnIndex(name, out col);
				}

				// Token: 0x06000C5E RID: 3166 RVA: 0x00043412 File Offset: 0x00041612
				public override string GetColumnName(int col)
				{
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					if (col == 1)
					{
						return "Probability";
					}
					return base.GetColumnName(col);
				}

				// Token: 0x06000C5F RID: 3167 RVA: 0x00043440 File Offset: 0x00041640
				public override IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
				{
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					IEnumerable<KeyValuePair<string, ColumnType>> enumerable = base.GetMetadataTypes(col);
					if (col == 1)
					{
						enumerable = MetadataUtils.Prepend<KeyValuePair<string, ColumnType>>(enumerable, new KeyValuePair<string, ColumnType>[] { MetadataUtils.GetPair(BoolType.Instance, "IsNormalized") });
					}
					return enumerable;
				}

				// Token: 0x06000C60 RID: 3168 RVA: 0x000434A0 File Offset: 0x000416A0
				public override ColumnType GetMetadataTypeOrNull(string kind, int col)
				{
					Contracts.CheckNonEmpty(kind, "kind");
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					if (col == 1 && kind == "IsNormalized")
					{
						return BoolType.Instance;
					}
					return base.GetMetadataTypeOrNull(kind, col);
				}

				// Token: 0x06000C61 RID: 3169 RVA: 0x000434F4 File Offset: 0x000416F4
				public override void GetMetadata<TValue>(string kind, int col, ref TValue value)
				{
					Contracts.CheckNonEmpty(kind, "kind");
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					if (col == 1 && kind == "IsNormalized")
					{
						MetadataUtils.Marshal<DvBool, TValue>(new MetadataUtils.MetadataGetter<DvBool>(this.IsNormalized), col, ref value);
						return;
					}
					base.GetMetadata<TValue>(kind, col, ref value);
				}

				// Token: 0x06000C62 RID: 3170 RVA: 0x00043555 File Offset: 0x00041755
				private void IsNormalized(int col, ref DvBool dst)
				{
					dst = DvBool.True;
				}

				// Token: 0x06000C63 RID: 3171 RVA: 0x00043562 File Offset: 0x00041762
				protected override void GetScoreValueKind(int col, ref DvText dst)
				{
					if (col == 1)
					{
						dst = new DvText("Probability");
						return;
					}
					base.GetScoreValueKind(col, ref dst);
				}
			}
		}
	}
}
