using System;
using System.Data.SqlClient;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200003D RID: 61
	// (Invoke) Token: 0x06000177 RID: 375
	public delegate MonitoredException ExceptionTranslator(SqlException sqlException);
}
