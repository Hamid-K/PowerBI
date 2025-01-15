using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A2A RID: 2602
	internal class DrdaColumnsSchemaQuery : DrdaSchemaQuery
	{
		// Token: 0x0600518E RID: 20878 RVA: 0x0014C710 File Offset: 0x0014A910
		public DrdaColumnsSchemaQuery()
		{
			string[] array = new string[]
			{
				"TABLE_CAT", "TABLE_SCHEM", "TABLE_NAME", "COLUMN_NAME", "ORDINAL_POSITION", "TYPE_NAME", "COLUMN_SIZE", "PRECISION", "DECIMAL_DIGITS", "NULLABLE",
				"REMARKS", "CCSID"
			};
			Type[] array2 = new Type[]
			{
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(int),
				typeof(string),
				typeof(int),
				typeof(int),
				typeof(int),
				typeof(string),
				typeof(string),
				typeof(int)
			};
			int[] array3 = new int[] { 0, 1, 2, 4 };
			string[] array4 = new string[]
			{
				null, "TABLE_SCHEMA", "TABLE_NAME", "COLUMN_NAME", "ORDINAL_POSITION", "DATA_TYPE", "LENGTH", "NUMERIC_PRECISION", "NUMERIC_SCALE", "IS_NULLABLE",
				"LONG_COMMENT", "CCSID"
			};
			string text = "SYSCOLUMNS";
			string[] array5 = new string[12];
			array5[1] = "TBCREATOR";
			array5[2] = "TBNAME";
			array5[3] = "NAME";
			array5[4] = "COLNO";
			array5[5] = "COLTYPE";
			array5[6] = "LENGTH";
			array5[7] = "LENGTH";
			array5[8] = "SCALE";
			array5[9] = "NULLS";
			array5[10] = "REMARKS";
			base..ctor(array, array2, array3, array4, text, array5, "SYSCOLUMNS", new string[]
			{
				null, "TABSCHEMA", "TABNAME", "COLNAME", "COLNO+1", "TYPENAME", "LENGTH", "LENGTH", "SCALE", "NULLS",
				null, "CODEPAGE"
			}, "COLUMNS");
		}
	}
}
