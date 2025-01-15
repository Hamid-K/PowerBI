using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A29 RID: 2601
	internal class DrdaTablesSchemaQuery : DrdaSchemaQuery
	{
		// Token: 0x0600518D RID: 20877 RVA: 0x0014C5F4 File Offset: 0x0014A7F4
		public DrdaTablesSchemaQuery()
			: base(new string[] { "TABLE_CAT", "TABLE_SCHEM", "TABLE_NAME", "TABLE_TYPE", "REMARKS" }, new Type[]
			{
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(string)
			}, new int[] { 3, 0, 1, 2 }, new string[] { null, "TABLE_SCHEMA", "TABLE_NAME", "TABLE_TYPE", "TABLE_TEXT" }, "SYSTABLES", new string[] { null, "CREATOR", "NAME", "TYPE", "REMARKS" }, "SYSTABLES", new string[] { null, "TABSCHEMA", "TABNAME", "TYPE", "REMARKS" }, "TABLES")
		{
		}
	}
}
