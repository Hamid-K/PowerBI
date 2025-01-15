using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200039A RID: 922
	public static class ScoreUtils
	{
		// Token: 0x060013C8 RID: 5064 RVA: 0x00070A68 File Offset: 0x0006EC68
		public static IDataScorerTransform GetScorer(IPredictor predictor, RoleMappedData data, IHostEnvironment env)
		{
			ISchemaBoundMapper schemaBoundMapper;
			SubComponent<IDataScorerTransform, SignatureDataScorer> scorerComponentAndMapper = ScoreUtils.GetScorerComponentAndMapper(predictor, null, data.Schema, env, out schemaBoundMapper);
			return ComponentCatalog.CreateInstance<IDataScorerTransform, SignatureDataScorer>(scorerComponentAndMapper, new object[] { env, data.Data, schemaBoundMapper });
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00070AA8 File Offset: 0x0006ECA8
		public static IDataScorerTransform GetScorer(SubComponent<IDataScorerTransform, SignatureDataScorer> scorer, IPredictor predictor, IDataView input, string featureColName, string groupColName, IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> customColumns, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IPredictor>(env, predictor, "predictor");
			Contracts.CheckValue<IDataView>(env, input, "input");
			RoleMappedSchema roleMappedSchema = TrainUtils.CreateRoleMappedSchemaOpt(input.Schema, featureColName, groupColName, customColumns);
			ISchemaBoundMapper schemaBoundMapper;
			SubComponent<IDataScorerTransform, SignatureDataScorer> scorerComponentAndMapper = ScoreUtils.GetScorerComponentAndMapper(predictor, scorer, roleMappedSchema, env, out schemaBoundMapper);
			return ComponentCatalog.CreateInstance<IDataScorerTransform, SignatureDataScorer>(scorerComponentAndMapper, new object[] { env, input, schemaBoundMapper });
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00070B14 File Offset: 0x0006ED14
		private static SubComponent<IDataScorerTransform, SignatureDataScorer> GetScorerComponentAndMapper(IPredictor predictor, SubComponent<IDataScorerTransform, SignatureDataScorer> scorer, RoleMappedSchema schema, IHostEnvironment env, out ISchemaBoundMapper mapper)
		{
			ISchemaBindableMapper schemaBindableMapper = ScoreUtils.GetSchemaBindableMapper(env, predictor, scorer);
			mapper = schemaBindableMapper.Bind(env, schema);
			if (SubComponentExtensions.IsGood(scorer))
			{
				return scorer;
			}
			return ScoreUtils.GetScorerComponent(mapper);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00070B48 File Offset: 0x0006ED48
		public static SubComponent<IDataScorerTransform, SignatureDataScorer> GetScorerComponent(ISchemaBoundMapper mapper)
		{
			string text = null;
			DvText dvText = default(DvText);
			if (mapper.OutputSchema.ColumnCount > 0 && MetadataUtils.TryGetMetadata<DvText>(mapper.OutputSchema, TextType.Instance, "ScoreColumnKind", 0, ref dvText) && dvText.HasChars)
			{
				text = dvText.ToString();
				ComponentCatalog.LoadableClassInfo loadableClassInfo = ComponentCatalog.GetLoadableClassInfo<SignatureDataScorer>(text);
				if (loadableClassInfo == null || !PlatformUtils.IsAssignableFromEx(typeof(IDataScorerTransform), loadableClassInfo.Type))
				{
					text = null;
				}
			}
			if (text == null)
			{
				text = "GenericScorer";
			}
			return new SubComponent<IDataScorerTransform, SignatureDataScorer>(text);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00070BD0 File Offset: 0x0006EDD0
		public static ISchemaBindableMapper GetSchemaBindableMapper(IHostEnvironment env, IPredictor predictor, SubComponent<IDataScorerTransform, SignatureDataScorer> scorerSettings)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IPredictor>(env, predictor, "predictor");
			ISchemaBindableMapper schemaBindableMapper;
			if (SubComponentExtensions.IsGood(scorerSettings) && ScoreUtils.TryCreateBindableFromScorer(env, predictor, scorerSettings, out schemaBindableMapper))
			{
				return schemaBindableMapper;
			}
			schemaBindableMapper = predictor as ISchemaBindableMapper;
			if (schemaBindableMapper != null)
			{
				return schemaBindableMapper;
			}
			if (predictor.PredictionKind == 2)
			{
				return new SchemaBindableBinaryPredictorWrapper(predictor);
			}
			return new SchemaBindablePredictorWrapper(predictor);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00070C2C File Offset: 0x0006EE2C
		private static bool TryCreateBindableFromScorer(IHostEnvironment env, IPredictor predictor, SubComponent<IDataScorerTransform, SignatureDataScorer> scorerSettings, out ISchemaBindableMapper bindable)
		{
			SubComponent<ISchemaBindableMapper, SignatureBindableMapper> subComponent = new SubComponent<ISchemaBindableMapper, SignatureBindableMapper>(scorerSettings.Kind, scorerSettings.Settings);
			return ComponentCatalog.TryCreateInstance<ISchemaBindableMapper, SignatureBindableMapper>(ref bindable, subComponent, new object[] { env, predictor });
		}
	}
}
