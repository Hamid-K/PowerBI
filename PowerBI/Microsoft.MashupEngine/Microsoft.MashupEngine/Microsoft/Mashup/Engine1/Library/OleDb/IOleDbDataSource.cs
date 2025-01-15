using System;
using System.Data;
using Microsoft.Data.Serialization;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.OleDb
{
	// Token: 0x0200056E RID: 1390
	internal interface IOleDbDataSource
	{
		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06002C47 RID: 11335
		string Provider { get; }

		// Token: 0x06002C48 RID: 11336
		IPageReader OpenTable(string tableIdentifier);

		// Token: 0x06002C49 RID: 11337
		bool TryGetProperty(Guid propertyGroup, DBPROPID propertyId, out object value);

		// Token: 0x06002C4A RID: 11338
		DataTable GetSchemas();

		// Token: 0x06002C4B RID: 11339
		DataTable GetSchemaTable(Guid schema, params object[] restrictions);

		// Token: 0x06002C4C RID: 11340
		DataTable GetLiteralInfo();
	}
}
