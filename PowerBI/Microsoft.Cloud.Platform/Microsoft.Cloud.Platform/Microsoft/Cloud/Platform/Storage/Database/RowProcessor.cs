using System;
using System.Data.SqlClient;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200003C RID: 60
	// (Invoke) Token: 0x06000173 RID: 371
	public delegate T RowProcessor<out T>(SqlDataReader reader);
}
