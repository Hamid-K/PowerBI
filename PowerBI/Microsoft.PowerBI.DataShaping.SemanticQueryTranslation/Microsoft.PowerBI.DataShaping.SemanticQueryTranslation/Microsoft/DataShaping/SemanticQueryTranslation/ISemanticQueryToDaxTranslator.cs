using System;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x0200000F RID: 15
	internal interface ISemanticQueryToDaxTranslator
	{
		// Token: 0x06000064 RID: 100
		SemanticQueryToDaxTranslationResult Translate(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context);
	}
}
