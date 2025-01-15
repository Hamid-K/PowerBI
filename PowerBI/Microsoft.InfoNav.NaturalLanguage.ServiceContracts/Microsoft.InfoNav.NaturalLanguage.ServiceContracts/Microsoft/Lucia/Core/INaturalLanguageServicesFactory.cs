using System;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200009B RID: 155
	public interface INaturalLanguageServicesFactory : IDisposable
	{
		// Token: 0x060002BC RID: 700
		IInterpretationService CreateInterpretationService(IDataInstanceLookup instanceLookup, IDataInstanceFilter dataInstanceFilter = null, ISchemaMetadataProvider schemaMetadataProvider = null);

		// Token: 0x060002BD RID: 701
		IManagementService CreateManagementService(IFeatureSwitchProvider featureSwitchProvider = null, LinguisticSchemaServicesBuilderOptions options = LinguisticSchemaServicesBuilderOptions.None);

		// Token: 0x060002BE RID: 702
		void VerifyRuntime();
	}
}
