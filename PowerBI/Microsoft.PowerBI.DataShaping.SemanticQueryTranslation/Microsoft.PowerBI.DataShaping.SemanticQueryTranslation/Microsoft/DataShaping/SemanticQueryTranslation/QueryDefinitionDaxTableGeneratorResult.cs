using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000012 RID: 18
	internal sealed class QueryDefinitionDaxTableGeneratorResult
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000037C4 File Offset: 0x000019C4
		internal QueryDefinitionDaxTableGeneratorResult(string daxQuery, TranslatedQuerySchema querySchema, IReadOnlyList<EngineMessageBase> messages)
		{
			this.DaxQuery = daxQuery;
			this.QuerySchema = querySchema;
			this.Messages = messages;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000037E1 File Offset: 0x000019E1
		internal string DaxQuery { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000037E9 File Offset: 0x000019E9
		internal TranslatedQuerySchema QuerySchema { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000037F1 File Offset: 0x000019F1
		internal IReadOnlyList<EngineMessageBase> Messages { get; }
	}
}
