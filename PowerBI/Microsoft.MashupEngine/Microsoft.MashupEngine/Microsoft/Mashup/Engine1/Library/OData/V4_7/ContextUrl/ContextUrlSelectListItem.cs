using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl
{
	// Token: 0x02000817 RID: 2071
	internal abstract class ContextUrlSelectListItem
	{
		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06003BDF RID: 15327
		public abstract SelectListItemKind Kind { get; }

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000C2893 File Offset: 0x000C0A93
		public static ContextUrlSelectListItem Wildcard()
		{
			return new ContextUrlSelectListWildcardItem();
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x000C289A File Offset: 0x000C0A9A
		public static ContextUrlSelectListItem QualifiedWildcard(string namespaceName)
		{
			return new ContextUrlSelectListQualifiedWildcardItem(namespaceName);
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x000C28A2 File Offset: 0x000C0AA2
		public static ContextUrlSelectListItem Projection(EdmPathExpression selectedPath)
		{
			return new ContextUrlSelectListProjectionItem(selectedPath);
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000C28AA File Offset: 0x000C0AAA
		public static ContextUrlSelectListItem Expansion(EdmPathExpression pathToNavigationProperty, IEnumerable<ContextUrlSelectListItem> selectList, bool recursive = false)
		{
			return new ContextUrlSelectListExpansionItem(pathToNavigationProperty, selectList, recursive);
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x000C28B4 File Offset: 0x000C0AB4
		public static ContextUrlSelectListItem FunctionOverload(EdmPathExpression pathToFunction, IEnumerable<string> parameterNames)
		{
			return new ContextUrlSelectListFunctionOverloadItem(pathToFunction, parameterNames);
		}
	}
}
