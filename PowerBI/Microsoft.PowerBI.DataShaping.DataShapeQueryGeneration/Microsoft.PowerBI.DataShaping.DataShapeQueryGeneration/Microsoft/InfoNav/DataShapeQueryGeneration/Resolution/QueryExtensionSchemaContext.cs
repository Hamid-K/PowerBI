using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Resolution
{
	// Token: 0x020000F4 RID: 244
	internal sealed class QueryExtensionSchemaContext
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x00020D1A File Offset: 0x0001EF1A
		internal QueryExtensionSchemaContext(QueryExtensionSchema extensionSchema, QuerySchemaMapping querySchemaMapping, NamingContext namingContext, Dictionary<QueryExtensionColumn, Expression> extensionColumnDsqExpressions = null)
			: this(new List<QueryExtensionSchema> { extensionSchema }, querySchemaMapping, namingContext, extensionColumnDsqExpressions)
		{
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00020D32 File Offset: 0x0001EF32
		internal QueryExtensionSchemaContext(IReadOnlyList<QueryExtensionSchema> extensionSchemas, QuerySchemaMapping querySchemaMapping, NamingContext namingContext, Dictionary<QueryExtensionColumn, Expression> extensionColumnDsqExpressions)
		{
			this.ExtensionSchemas = extensionSchemas;
			this.QuerySchemaMapping = querySchemaMapping;
			this.NamingContext = namingContext;
			this.ExtensionColumnDsqExpressions = extensionColumnDsqExpressions;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00020D57 File Offset: 0x0001EF57
		internal IReadOnlyList<QueryExtensionSchema> ExtensionSchemas { get; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00020D5F File Offset: 0x0001EF5F
		internal QuerySchemaMapping QuerySchemaMapping { get; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00020D67 File Offset: 0x0001EF67
		internal NamingContext NamingContext { get; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00020D6F File Offset: 0x0001EF6F
		internal Dictionary<QueryExtensionColumn, Expression> ExtensionColumnDsqExpressions { get; }
	}
}
