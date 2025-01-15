using System;
using System.Data;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001C RID: 28
	internal sealed class SQLScheduleParameter
	{
		// Token: 0x04000151 RID: 337
		public string Name = "";

		// Token: 0x04000152 RID: 338
		public SqlDbType Type = SqlDbType.Variant;

		// Token: 0x04000153 RID: 339
		public object Value;
	}
}
