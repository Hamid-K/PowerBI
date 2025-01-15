using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Tmdl.Converters;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200012D RID: 301
	internal interface IMetadataObjectConfiguration
	{
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600148A RID: 5258
		TmdlSchema Schema { get; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600148B RID: 5259
		IReadOnlyDictionary<ObjectType, IMetadataObjectConverter> Converters { get; }

		// Token: 0x0600148C RID: 5260
		TmdlObjectInfo GetSchema(ObjectType objectType);
	}
}
