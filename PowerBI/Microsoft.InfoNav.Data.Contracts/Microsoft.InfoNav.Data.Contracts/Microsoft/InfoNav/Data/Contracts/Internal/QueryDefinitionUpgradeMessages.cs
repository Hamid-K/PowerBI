using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002F0 RID: 752
	internal static class QueryDefinitionUpgradeMessages
	{
		// Token: 0x06001912 RID: 6418 RVA: 0x0002D05D File Offset: 0x0002B25D
		internal static string EntitySetReferenceWithNoSchema()
		{
			return "QueryDefinition contains an EntitySet reference and cannot be upgraded without schema.";
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0002D064 File Offset: 0x0002B264
		internal static string PropertyExpressionWithNoSchema()
		{
			return "QueryDefinition contains a Property expression and cannot be upgraded without schema.";
		}
	}
}
