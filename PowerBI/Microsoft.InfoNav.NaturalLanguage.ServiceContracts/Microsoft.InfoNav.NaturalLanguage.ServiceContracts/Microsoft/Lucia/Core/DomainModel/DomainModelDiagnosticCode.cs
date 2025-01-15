using System;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000184 RID: 388
	public enum DomainModelDiagnosticCode
	{
		// Token: 0x040006EA RID: 1770
		InternalError,
		// Token: 0x040006EB RID: 1771
		ConceptualSchemaDeserializationError,
		// Token: 0x040006EC RID: 1772
		LinguisticSchemaDeserializationError,
		// Token: 0x040006ED RID: 1773
		LinguisticSchemaVersionNotSupported,
		// Token: 0x040006EE RID: 1774
		LinguisticSchemaUpgradeError,
		// Token: 0x040006EF RID: 1775
		LinguisticSchemaUpgradeWarning,
		// Token: 0x040006F0 RID: 1776
		LinguisticSchemaSyntacticValidationError,
		// Token: 0x040006F1 RID: 1777
		LinguisticSchemaSemanticValidationError,
		// Token: 0x040006F2 RID: 1778
		LinguisticSchemaLanguageNotSupported,
		// Token: 0x040006F3 RID: 1779
		LinguisticSchemaNotAvailable,
		// Token: 0x040006F4 RID: 1780
		LinguisticSchemaServicesWarning
	}
}
