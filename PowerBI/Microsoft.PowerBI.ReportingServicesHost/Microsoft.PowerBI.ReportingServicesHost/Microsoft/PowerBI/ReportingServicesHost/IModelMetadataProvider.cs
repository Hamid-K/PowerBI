using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Edm;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000052 RID: 82
	internal interface IModelMetadataProvider : IDisposable
	{
		// Token: 0x060001CA RID: 458
		EngineDataModel GetEngineDataModel(ModelMetadataRequest request, IFeatureSwitchProvider featureSwitchProvider, Func<Stream, IFeatureSwitchProvider, EngineDataModel> parse);

		// Token: 0x060001CB RID: 459
		ConceptualSchemaAndCapabilities GetConceptualSchema(ModelMetadataRequest request, ConceptualSchemaBuilderOptions builderOptions, ParseConceptualSchema parse);

		// Token: 0x060001CC RID: 460
		string GetModelMetadata(ModelMetadataRequest request);

		// Token: 0x060001CD RID: 461
		DataSet GetSchemaDataSet(SchemaCommandRequest request, string schemaName, IReadOnlyDictionary<string, object> restrictions);

		// Token: 0x060001CE RID: 462
		void ClearCachesForDataSource(string connectionString);

		// Token: 0x060001CF RID: 463
		void ClearAllModelCaches();
	}
}
