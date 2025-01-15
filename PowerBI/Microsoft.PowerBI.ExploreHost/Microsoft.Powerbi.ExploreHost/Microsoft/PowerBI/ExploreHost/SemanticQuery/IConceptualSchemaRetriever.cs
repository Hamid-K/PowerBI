using System;
using System.Threading.Tasks;
using Microsoft.InfoNav;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ReportingServicesHost;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000042 RID: 66
	public interface IConceptualSchemaRetriever
	{
		// Token: 0x06000225 RID: 549
		IConceptualSchema GetConceptualSchema(ASConnectionInfo connectionInfo, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior = TranslationsBehavior.Default);

		// Token: 0x06000226 RID: 550
		IConceptualSchema GetConceptualSchema(string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior = null);

		// Token: 0x06000227 RID: 551
		Task<string> GetClientConceptualSchemaAsync(string databaseID, string jsonRequest, IModel model, bool isExtendable, TranslationsBehavior translationsBehavior);
	}
}
