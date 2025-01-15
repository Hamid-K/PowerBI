using System;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x02000216 RID: 534
	internal interface IMetadataObjectWithOverrides
	{
		// Token: 0x06001E2D RID: 7725
		void WriteAllOverridenBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ReplacementPropertiesCollection newProperties);
	}
}
