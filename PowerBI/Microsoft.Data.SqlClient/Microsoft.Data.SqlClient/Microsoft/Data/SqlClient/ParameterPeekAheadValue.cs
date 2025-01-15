using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200002F RID: 47
	internal class ParameterPeekAheadValue
	{
		// Token: 0x040000AA RID: 170
		internal IEnumerator<SqlDataRecord> Enumerator;

		// Token: 0x040000AB RID: 171
		internal SqlDataRecord FirstRecord;
	}
}
