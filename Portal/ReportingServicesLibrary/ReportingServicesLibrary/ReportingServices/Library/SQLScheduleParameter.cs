using System;
using System.Data;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000257 RID: 599
	internal sealed class SQLScheduleParameter
	{
		// Token: 0x040007FB RID: 2043
		public string Name = "";

		// Token: 0x040007FC RID: 2044
		public SqlDbType Type = SqlDbType.Variant;

		// Token: 0x040007FD RID: 2045
		public object Value;
	}
}
