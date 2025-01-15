using System;
using System.Data.SqlClient;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000084 RID: 132
	internal interface ISqlInvertedIndexBuilder : IInvertedIndexUpdate
	{
		// Token: 0x0600054A RID: 1354
		IUpdateContext BeginUpdate(SqlConnection connection);
	}
}
