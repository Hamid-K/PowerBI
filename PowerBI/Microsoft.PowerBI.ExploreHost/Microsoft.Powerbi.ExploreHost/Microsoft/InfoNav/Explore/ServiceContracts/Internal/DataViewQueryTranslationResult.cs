using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x02000006 RID: 6
	public sealed class DataViewQueryTranslationResult
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000207E File Offset: 0x0000027E
		public DataViewQueryTranslationResult(string daxExpression, IReadOnlyDictionary<string, string> selectNameToDaxColumnName)
		{
			this.DaxExpression = daxExpression;
			this.SelectNameToDaxColumnName = selectNameToDaxColumnName;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002094 File Offset: 0x00000294
		public string DaxExpression { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
		public IReadOnlyDictionary<string, string> SelectNameToDaxColumnName { get; }
	}
}
