using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A2D RID: 2605
	internal class DrdaPrimaryKeysSchemaQuery : DrdaSchemaQuery
	{
		// Token: 0x06005193 RID: 20883 RVA: 0x0014CD74 File Offset: 0x0014AF74
		public DrdaPrimaryKeysSchemaQuery()
		{
			string[] array = new string[] { "TABLE_CAT", "TABLE_SCHEM", "TABLE_NAME", "COLUMN_NAME", "KEY_SEQ", "PK_NAME" };
			Type[] array2 = new Type[]
			{
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(int),
				typeof(string)
			};
			int[] array3 = new int[] { 0, 1, 2, 4 };
			string[] array4 = new string[] { null, "TABLE_SCHEMA", "TABLE_NAME", "COLUMN_NAME", "ORDINAL_POSITION", "CONSTRAINT_NAME" };
			string text = "<CustomQuery4>";
			string[] array5 = new string[6];
			array5[1] = "TBCREATOR";
			array5[2] = "TBNAME";
			array5[3] = "NAME";
			array5[4] = "KEYSEQ";
			base..ctor(array, array2, array3, array4, text, array5, "<CustomQuery3>", new string[] { null, "TABSCHEMA", "TABNAME", "COLNAME", "COLSEQ", "CONSTNAME" }, "<CustomQuery5>");
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x0014CEB3 File Offset: 0x0014B0B3
		public override string GetAS400Table(string catalog)
		{
			return string.Format("(SELECT DISTINCT KEYCSTS.TABLE_SCHEMA, KEYCSTS.TABLE_NAME, KEYCSTS.COLUMN_NAME, KEYCSTS.ORDINAL_POSITION, KEYCSTS.CONSTRAINT_NAME FROM {0}.SYSKEYCST KEYCSTS, {0}.SYSCST CSTS WHERE KEYCSTS.CONSTRAINT_NAME = CSTS.CONSTRAINT_NAME AND CSTS.CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_SCHEMA, TABLE_NAME, ORDINAL_POSITION) AS TBL", catalog);
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x0014CEC0 File Offset: 0x0014B0C0
		public override string GetMVSTable(string catalog)
		{
			return string.Format("(SELECT TBCREATOR, TBNAME, NAME, KEYSEQ FROM {0}.SYSCOLUMNS WHERE KEYSEQ > 0) AS TBL", catalog);
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x0014CECD File Offset: 0x0014B0CD
		public override string GetUDBTable(string catalog)
		{
			return string.Format("(SELECT RTRIM(TABSCHEMA) AS TABSCHEMA, TABNAME, COLNAME, COLSEQ, CONSTNAME FROM {0}.KEYCOLUSE WHERE COLSEQ > 0 ORDER BY TABSCHEMA, TABNAME, COLSEQ) AS TBL", catalog);
		}
	}
}
