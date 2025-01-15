using System;
using System.IO;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x0200000B RID: 11
	public static class EngineDataModelParser
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002A26 File Offset: 0x00000C26
		public static EngineDataModel Parse(Stream modelMetadata, IFeatureSwitchProvider featureSwitchProvider = null)
		{
			return EngineDataModelParser.BuildEngineModel((EntityDataModel)EntityDataModelParser.Instance.Parse(modelMetadata), featureSwitchProvider);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A3E File Offset: 0x00000C3E
		public static EngineDataModel Parse(Stream modelMetadata, IFeatureSwitchProvider featureSwitchProvider, ITracer tracer)
		{
			if (featureSwitchProvider != null && featureSwitchProvider.IsEnabled(FeatureSwitchKind.ConceptualSchema))
			{
				return new EngineDataModel(ConceptualSchemaParser.ParseConceptualSchema(modelMetadata, featureSwitchProvider, tracer));
			}
			return EngineDataModelParser.BuildEngineModel((EntityDataModel)EntityDataModelParser.Instance.Parse(modelMetadata), featureSwitchProvider);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A74 File Offset: 0x00000C74
		private static EngineDataModel BuildEngineModel(EntityDataModel edmModel, IFeatureSwitchProvider featureSwitchProvider)
		{
			IConceptualSchema schema = edmModel.AsConceptualSchema(featureSwitchProvider, false);
			schema.RegisterAnnotationProvider<ComparerAnnotation, IConceptualSchema>(new ComparerAnnotationProvider(() => ComparerAnnotationProvider.BuildComparerAnnotation(schema), schema));
			return new EngineDataModel(edmModel, schema);
		}

		// Token: 0x04000033 RID: 51
		public const string PreferredCsdlVersion = "2.0";
	}
}
