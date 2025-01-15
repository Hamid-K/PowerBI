using System;
using Microsoft.BusinessIntelligence;
using Microsoft.InfoNav.Data.Edm;

namespace Microsoft.PowerBI.ReportingServicesHost.Utils
{
	// Token: 0x02000066 RID: 102
	public static class ConceptualSchemaExtensions
	{
		// Token: 0x06000242 RID: 578 RVA: 0x000064F3 File Offset: 0x000046F3
		public static ConceptualSchemaBuilderOptions CreateConceptualSchemaBuilderOptions(FeatureSwitches featureSwitches)
		{
			return new ConceptualSchemaBuilderOptions(featureSwitches.SparklineDataEnabled);
		}
	}
}
