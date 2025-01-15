using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A2F RID: 2607
	internal class DrdaIndexesSchemaQuery : DrdaSchemaQuery
	{
		// Token: 0x0600519D RID: 20893 RVA: 0x0014D204 File Offset: 0x0014B404
		public DrdaIndexesSchemaQuery()
		{
			string[] array = new string[]
			{
				"TableCatalog", "TableSchema", "IndexName", "Type", "TableName", "IndexCatalog", "IndexSchema", "PrimaryKey", "Unique", "Clustered",
				"FillFactor", "InitialSize", "NULLS", "SortBookmarks", "AutoUpdate", "NULLCollation", "OrdinalPosition", "ColumnName", "ColumnGuid", "ColumnPropId",
				"Collation", "Cardinality", "Pages", "FilterCondition", "Integrated"
			};
			Type[] array2 = new Type[]
			{
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(ushort),
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(bool),
				typeof(bool),
				typeof(bool),
				typeof(int),
				typeof(int),
				typeof(int),
				typeof(bool),
				typeof(bool),
				typeof(int),
				typeof(uint),
				typeof(string),
				typeof(Guid),
				typeof(uint),
				typeof(short),
				typeof(ulong),
				typeof(short),
				typeof(string),
				typeof(bool)
			};
			int[] array3 = new int[] { 8, 1, 2, 16 };
			string[] array4 = new string[25];
			array4[1] = "TABLE_QUALIFIER";
			array4[2] = "INDEX_NAME";
			array4[4] = "TABLE_NAME";
			array4[6] = "INDEX_QUALIFIER";
			array4[7] = "CASE NON_UNIQUE WHEN 0 THEN 1 ELSE 0 END";
			array4[8] = "CASE NON_UNIQUE WHEN 2 THEN 0 ELSE 1 END";
			array4[16] = "ORDINAL_IN_BASE";
			array4[17] = "COLUMN_NAME";
			array4[20] = "CASE COLLATION WHEN 'D' THEN 2 ELSE 1 END";
			string text = "<AS400CustomQuery8>";
			string[] array5 = new string[25];
			array5[1] = "TABLE_QUALIFIER";
			array5[2] = "INDEX_NAME";
			array5[4] = "TABLE_NAME";
			array5[6] = "INDEX_QUALIFIER";
			array5[7] = "CASE NON_UNIQUE WHEN 0 THEN 1 ELSE 0 END";
			array5[8] = "CASE NON_UNIQUE WHEN 2 THEN 0 ELSE 1 END";
			array5[16] = "ORDINAL_IN_BASE";
			array5[17] = "COLUMN_NAME";
			array5[20] = "CASE COLLATION WHEN 'D' THEN 2 ELSE 1 END";
			string text2 = "<MVSCustomQuery9>";
			string[] array6 = new string[25];
			array6[1] = "TABLE_QUALIFIER";
			array6[2] = "INDEX_NAME";
			array6[4] = "TABLE_NAME";
			array6[6] = "INDEX_QUALIFIER";
			array6[7] = "CASE NON_UNIQUE WHEN 0 THEN 1 ELSE 0 END";
			array6[8] = "CASE NON_UNIQUE WHEN 2 THEN 0 ELSE 1 END";
			array6[17] = "COLNAMES";
			base..ctor(array, array2, array3, array4, text, array5, text2, array6, "<UDBCustomQuery10>");
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x0014D556 File Offset: 0x0014B756
		public override string GetAS400Table(string catalog)
		{
			return string.Format("(SELECT VARCHAR(RTRIM(T1.TABLE_SCHEMA), 128) as TABLE_QUALIFIER, VARCHAR(RTRIM(T1.TABLE_NAME), 128) as TABLE_NAME, smallint( CASE T1.UNIQUERULE WHEN 'P' THEN 0 WHEN 'U' THEN 1 ELSE 2 END) AS NON_UNIQUE, VARCHAR(RTRIM(T1.INDEX_SCHEMA), 128) as INDEX_QUALIFIER, VARCHAR(RTRIM(T1.INDEX_NAME), 128) as INDEX_NAME, T2.ORDINAL_POSITION as SEQ_IN_INDEX, VARCHAR(RTRIM(T2.COLUMN_NAME), 128) as COLUMN_NAME, T2.ORDERING as COLLATION, T2.COLUMN_POSITION as ORDINAL_IN_BASE FROM {0}.SYSINDEXES T1, {1}.SYSKEYS T2 WHERE T1.INDEX_NAME = T2.INDEX_NAME AND T1.INDEX_SCHEMA = T2.INDEX_SCHEMA AND T1.INDEX_OWNER = T2.INDEX_OWNER) AS TBL", catalog, catalog);
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x0014D564 File Offset: 0x0014B764
		public override string GetMVSTable(string catalog)
		{
			return "(SELECT VARCHAR (RTRIM (T1.TBCREATOR), 128) as TABLE_QUALIFIER, VARCHAR (RTRIM (T1.TBNAME), 128) as TABLE_NAME, smallint (CASE T1.UNIQUERULE WHEN 'P' THEN 0 WHEN 'U' THEN 1 ELSE 2 END) AS NON_UNIQUE, VARCHAR (RTRIM (T1.INDEXSPACE), 128) as INDEX_QUALIFIER, VARCHAR (RTRIM (T1.NAME), 128) as INDEX_NAME, T2.COLSEQ as SEQ_IN_INDEX, VARCHAR (RTRIM (T2.COLNAME), 128) as COLUMN_NAME, T2.ORDERING as COLLATION, T2.COLNO as ORDINAL_IN_BASE FROM SYSIBM.SYSINDEXES T1, SYSIBM.SYSKEYS T2 WHERE T2.IXNAME = T1.NAME AND T2.IXCREATOR = T1.CREATOR) AS TBL";
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x0014D56B File Offset: 0x0014B76B
		public override string GetUDBTable(string catalog)
		{
			return "(SELECT VARCHAR(RTRIM(TABSCHEMA), 128) as TABLE_QUALIFIER, VARCHAR(RTRIM(TABNAME), 128) as TABLE_NAME, smallint( CASE UNIQUERULE WHEN 'P' THEN 0 WHEN 'U' THEN 1 ELSE 2 END) AS NON_UNIQUE, VARCHAR(RTRIM(INDSCHEMA), 128) as INDEX_QUALIFIER, VARCHAR(RTRIM(INDNAME), 128) as INDEX_NAME, COLNAMES FROM SYSCAT.INDEXES) AS TBL";
		}
	}
}
