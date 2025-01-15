using System;
using System.Collections;
using System.Data;

namespace Microsoft.ReportingServices.Extensions
{
	// Token: 0x02000011 RID: 17
	internal interface ICatalogQuery
	{
		// Token: 0x0600008C RID: 140
		void ExecuteNonQuery(string query, Hashtable parameters, CommandType type);

		// Token: 0x0600008D RID: 141
		IDataReader ExecuteReader(string query, Hashtable parameters, CommandType type);

		// Token: 0x0600008E RID: 142
		void Commit();
	}
}
