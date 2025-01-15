using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core.DomainModel;
using Microsoft.Lucia.Core.DomainModel.Serialization;
using Microsoft.Lucia.Core.Packaging;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200009A RID: 154
	public interface IManagementService
	{
		// Token: 0x060002AE RID: 686
		BuildDataIndexResult BuildDataIndex(IDatabaseContext databaseContext, IDataProvider dataProvider, string workingDirectory, CancellationToken<DataIndexBuilderCancelOption> cancellationToken, Version dataIndexVersion, IReadOnlyDictionary<string, string> tags = null);

		// Token: 0x060002AF RID: 687
		OpenDataIndexResult OpenDataIndex(DataIndexPackageReader reader, IDatabaseContext databaseContext, string workingDirectory, CancellationToken cancellationToken);

		// Token: 0x060002B0 RID: 688
		ComputeDataIndexCacheKeyResult ComputeDataIndexCacheKey(IDatabaseContext databaseContext, CancellationToken cancellationToken, IReadOnlyDictionary<string, string> tags = null);

		// Token: 0x060002B1 RID: 689
		bool ValidateDomainModel(IDatabaseContext databaseContext, out IReadOnlyList<DomainModelDiagnosticMessage> diagnosticMessages);

		// Token: 0x060002B2 RID: 690
		bool TryGetConceptualSchema(IDatabaseContext databaseContext, out IConceptualSchema conceptualSchema);

		// Token: 0x060002B3 RID: 691
		bool TryGetLinguisticSchema(IDatabaseContext databaseContext, out LsdlDocument lsdlDocument, string defaultTemplateSchema = null);

		// Token: 0x060002B4 RID: 692
		bool TryGetLinguisticEntityInformation(IDatabaseContext databaseContext, ConceptualBinding binding, out LinguisticEntityInformation linguisticEntityInformation, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060002B5 RID: 693
		bool TryGetDefaultLabel(IDatabaseContext databaseContext, ConceptualEntityBinding binding, out ConceptualPropertyBinding defaultLabelProperty, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060002B6 RID: 694
		bool TryGetTextAnalyzer(IDatabaseContext databaseContext, out ITextAnalyzer textAnalyzer, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060002B7 RID: 695
		void NotifyDatabaseChanges(IEnumerable<DatabaseNotification> notifications);

		// Token: 0x060002B8 RID: 696
		void NotifyConfigurationChanges(IConfigurationProvider configProvider);

		// Token: 0x060002B9 RID: 697
		double GetEstimatedDatabaseMemoryUtilizationInMB(string databaseName);

		// Token: 0x060002BA RID: 698
		double GetEstimatedOverallMemoryUtilizationInMB();

		// Token: 0x060002BB RID: 699
		GeneratePhrasingLabelsResult GeneratePhrasingLabels(IDatabaseContext databaseContext, IConceptualSchema csdl, LsdlDocument inputLsdl);
	}
}
