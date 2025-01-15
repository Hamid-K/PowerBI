using System;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000026 RID: 38
	internal interface INameGenerator
	{
		// Token: 0x0600017C RID: 380
		string CreateName(string candidate);

		// Token: 0x0600017D RID: 381
		string CreateName(string candidate, object key);

		// Token: 0x0600017E RID: 382
		void AddExistingName(string name);
	}
}
