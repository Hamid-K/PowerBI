using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.MachineLearning.Model;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000221 RID: 545
	public abstract class SchemaBindablePredictorWrapperBase : ISchemaBindableMapper, ICanSaveModel, ICanSaveSummary
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x000427BB File Offset: 0x000409BB
		internal IPredictor Predictor
		{
			get
			{
				return this._predictor;
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000427C3 File Offset: 0x000409C3
		public SchemaBindablePredictorWrapperBase(IPredictor predictor)
		{
			Contracts.CheckValue<IPredictor>(predictor, "predictor");
			this._predictor = predictor;
			this._scoreType = SchemaBindablePredictorWrapperBase.GetScoreType(this._predictor, out this._valueMapper);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000427F4 File Offset: 0x000409F4
		private static ColumnType GetScoreType(IPredictor predictor, out IValueMapper valueMapper)
		{
			valueMapper = predictor as IValueMapper;
			if (valueMapper != null)
			{
				return valueMapper.OutputType;
			}
			throw Contracts.Except("Predictor score type cannot be determined since it doesn't implement IValueMapper");
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00042814 File Offset: 0x00040A14
		protected SchemaBindablePredictorWrapperBase(ModelLoadContext ctx, IHostEnvironment env)
		{
			ctx.LoadModel<IPredictor, SignatureLoadModel>(out this._predictor, "Predictor", new object[] { env });
			this._scoreType = SchemaBindablePredictorWrapperBase.GetScoreType(this._predictor, out this._valueMapper);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0004285B File Offset: 0x00040A5B
		public virtual void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SaveModel<IPredictor>(this._predictor, "Predictor");
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00042880 File Offset: 0x00040A80
		public ISchemaBoundMapper Bind(IHostEnvironment env, RoleMappedSchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			ISchemaBoundMapper schemaBoundMapper2;
			using (IChannel channel = env.Register("SchemaBindableWrapper").Start("Bind"))
			{
				Contracts.CheckValue<RoleMappedSchema>(channel, schema, "schema");
				Contracts.CheckParam(channel, schema.Feature != null, "schema", "Need a features column");
				ColumnType type = schema.Feature.Type;
				ColumnType columnType = ((this._valueMapper != null) ? this._valueMapper.InputType : new VectorType(NumberType.Float, 0));
				if (type != columnType)
				{
					if (!type.ItemType.Equals(columnType.ItemType))
					{
						throw Contracts.Except(channel, "Incompatible features column type item type: '{0}' vs '{1}'", new object[] { type.ItemType, columnType.ItemType });
					}
					if (type.IsVector != columnType.IsVector)
					{
						throw Contracts.Except(channel, "Incompatible features column type: '{0}' vs '{1}'", new object[] { type, columnType });
					}
					if (type.VectorSize != columnType.VectorSize && columnType.VectorSize > 0)
					{
						throw Contracts.Except(channel, "Incompatible features column type: '{0}' vs '{1}'", new object[] { type, columnType });
					}
				}
				ISchemaBoundMapper schemaBoundMapper = this.BindCore(channel, schema);
				channel.Done();
				schemaBoundMapper2 = schemaBoundMapper;
			}
			return schemaBoundMapper2;
		}

		// Token: 0x06000C34 RID: 3124
		protected abstract ISchemaBoundMapper BindCore(IChannel ch, RoleMappedSchema schema);

		// Token: 0x06000C35 RID: 3125 RVA: 0x000429E8 File Offset: 0x00040BE8
		protected virtual Delegate GetPredictionGetter(IRow input, int colSrc)
		{
			ColumnType columnType = input.Schema.GetColumnType(colSrc);
			Func<IRow, int, ValueGetter<int>> func = new Func<IRow, int, ValueGetter<int>>(this.GetValueGetter<int, int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[]
			{
				columnType.RawType,
				this._scoreType.RawType
			});
			return (Delegate)methodInfo.Invoke(this, new object[] { input, colSrc });
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00042A94 File Offset: 0x00040C94
		private ValueGetter<TDst> GetValueGetter<TSrc, TDst>(IRow input, int colSrc)
		{
			ValueGetter<TSrc> featureGetter = input.GetGetter<TSrc>(colSrc);
			ValueMapper<TSrc, TDst> map = this._valueMapper.GetMapper<TSrc, TDst>();
			TSrc features = default(TSrc);
			return delegate(ref TDst dst)
			{
				featureGetter.Invoke(ref features);
				map.Invoke(ref features, ref dst);
			};
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00042AE0 File Offset: 0x00040CE0
		public void SaveSummary(TextWriter writer, FeatureNameCollection names)
		{
			ICanSaveSummary canSaveSummary = this._predictor as ICanSaveSummary;
			if (canSaveSummary == null)
			{
				writer.WriteLine("{0} does not support saving summaries", this._predictor);
				return;
			}
			canSaveSummary.SaveSummary(writer, names);
		}

		// Token: 0x040006B2 RID: 1714
		protected readonly IPredictor _predictor;

		// Token: 0x040006B3 RID: 1715
		protected readonly IValueMapper _valueMapper;

		// Token: 0x040006B4 RID: 1716
		protected readonly ColumnType _scoreType;

		// Token: 0x02000222 RID: 546
		protected sealed class SingleValueRowMapper : ISchemaBoundRowMapper, ISchemaBoundMapper
		{
			// Token: 0x06000C38 RID: 3128 RVA: 0x00042B16 File Offset: 0x00040D16
			public SingleValueRowMapper(RoleMappedSchema schema, SchemaBindablePredictorWrapperBase parent, ISchema outputSchema)
			{
				this._parent = parent;
				this._inputSchema = schema;
				this._outputSchema = outputSchema;
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00042B33 File Offset: 0x00040D33
			public RoleMappedSchema InputSchema
			{
				get
				{
					return this._inputSchema;
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00042B3B File Offset: 0x00040D3B
			public ISchema OutputSchema
			{
				get
				{
					return this._outputSchema;
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00042B43 File Offset: 0x00040D43
			public ISchemaBindableMapper Bindable
			{
				get
				{
					return this._parent;
				}
			}

			// Token: 0x06000C3C RID: 3132 RVA: 0x00042B64 File Offset: 0x00040D64
			public Func<int, bool> GetDependencies(Func<int, bool> predicate)
			{
				for (int i = 0; i < this._outputSchema.ColumnCount; i++)
				{
					if (predicate(i))
					{
						return (int col) => col == this._inputSchema.Feature.Index;
					}
				}
				return (int col) => false;
			}

			// Token: 0x06000C3D RID: 3133 RVA: 0x00042CAC File Offset: 0x00040EAC
			public IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> GetInputColumnRoles()
			{
				yield return RoleMappedSchema.ColumnRole.Feature.Bind(this._inputSchema.Feature.Name);
				yield break;
			}

			// Token: 0x06000C3E RID: 3134 RVA: 0x00042CCC File Offset: 0x00040ECC
			public IRow GetOutputRow(IRow input, Func<int, bool> predicate)
			{
				Delegate[] array = new Delegate[1];
				if (predicate(0))
				{
					array[0] = this._parent.GetPredictionGetter(input, this._inputSchema.Feature.Index);
				}
				return new SimpleRow(this._outputSchema, input, array);
			}

			// Token: 0x040006B5 RID: 1717
			private readonly SchemaBindablePredictorWrapperBase _parent;

			// Token: 0x040006B6 RID: 1718
			private readonly RoleMappedSchema _inputSchema;

			// Token: 0x040006B7 RID: 1719
			private readonly ISchema _outputSchema;
		}
	}
}
