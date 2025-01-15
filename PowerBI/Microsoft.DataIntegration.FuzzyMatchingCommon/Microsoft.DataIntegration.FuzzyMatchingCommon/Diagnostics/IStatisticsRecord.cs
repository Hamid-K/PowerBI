using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x0200004C RID: 76
	public interface IStatisticsRecord : IDataRecord
	{
		// Token: 0x06000261 RID: 609
		DataTable GetSchemaTable();

		// Token: 0x06000262 RID: 610
		void PrintStatisticsToConsole();

		// Token: 0x06000263 RID: 611
		void Reset();
	}
}
