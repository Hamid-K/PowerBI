using System;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x0200000E RID: 14
	internal interface IGroupingDefinitionToDaxTranslator
	{
		// Token: 0x06000062 RID: 98
		SemanticQueryToDaxTranslationResult Translate(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context);

		// Token: 0x06000063 RID: 99
		SemanticQueryToDaxTranslationResult TranslatePartitionColumn(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context);
	}
}
