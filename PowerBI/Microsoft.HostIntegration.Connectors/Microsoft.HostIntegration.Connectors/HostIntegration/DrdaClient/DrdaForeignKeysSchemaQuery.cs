using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A2E RID: 2606
	internal class DrdaForeignKeysSchemaQuery : DrdaSchemaQuery
	{
		// Token: 0x06005197 RID: 20887 RVA: 0x0014CEDC File Offset: 0x0014B0DC
		public DrdaForeignKeysSchemaQuery()
		{
			string[] array = new string[]
			{
				"PKTABLE_CAT", "PKTABLE_SCHEM", "PKTABLE_NAME", "PKCOLUMN_NAME", "FKTABLE_CAT", "FKTABLE_SCHEM", "FKTABLE_NAME", "FKCOLUMN_NAME", "KEY_SEQ", "UPDATE_RULE",
				"DELETE_RULE", "FK_NAME", "PK_NAME", "DEFERRABILITY"
			};
			Type[] array2 = new Type[]
			{
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(int),
				typeof(int),
				typeof(int),
				typeof(string),
				typeof(string),
				typeof(int)
			};
			int[] array3 = new int[] { 4, 5, 6, 8 };
			string[] array4 = new string[14];
			array4[1] = "PKTABLE_SCHEM";
			array4[2] = "PKTABLE_NAME";
			array4[3] = "PKCOLUMN_NAME";
			array4[5] = "FKTABLE_SCHEM";
			array4[6] = "FKTABLE_NAME";
			array4[7] = "FKCOLUMN_NAME";
			array4[8] = "ORDINAL_POSITION";
			array4[9] = "UPDATE_RULE";
			array4[10] = "DELETE_RULE";
			array4[11] = "FK_NAME";
			array4[12] = "PK_NAME";
			string text = "<CustomQuery6>";
			string[] array5 = new string[0];
			string text2 = "";
			string[] array6 = new string[14];
			array6[1] = "PKTABLE_SCHEM";
			array6[2] = "PKTABLE_NAME";
			array6[3] = "PKCOLUMN_NAME";
			array6[5] = "FKTABLE_SCHEM";
			array6[6] = "FKTABLE_NAME";
			array6[7] = "FKCOLUMN_NAME";
			array6[8] = "KEY_SEQ";
			array6[9] = "UPDATE_RULE";
			array6[10] = "DELETE_RULE";
			array6[11] = "FK_NAME";
			array6[12] = "PK_NAME";
			base..ctor(array, array2, array3, array4, text, array5, text2, array6, "<CustomQuery7>");
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x0014D119 File Offset: 0x0014B319
		public override string GetAS400Table(string catalog)
		{
			return string.Format("(SELECT DISTINCT PK.SYSTEM_TABLE_SCHEMA AS PKTABLE_SCHEM, PK.TABLE_NAME AS PKTABLE_NAME, PK.COLUMN_NAME AS PKCOLUMN_NAME, FK.SYSTEM_TABLE_SCHEMA AS FKTABLE_SCHEM, FK.TABLE_NAME AS FKTABLE_NAME, FK.COLUMN_NAME AS FKCOLUMN_NAME, FK.ORDINAL_POSITION, smallint( case R.UPDATE_RULE when 'CASCADE' then 0 when 'SET DEFAULT' then 1 when 'SET NULL' then 2 when 'NO ACTION' then 3 END) AS UPDATE_RULE, smallint( case R.DELETE_RULE when 'CASCADE' then 0 when 'SET DEFAULT' then 1 when 'SET NULL' then 2 when 'NO ACTION' then 3 END) AS DELETE_RULE, PK.CONSTRAINT_NAME AS PK_NAME, FK.CONSTRAINT_NAME AS FK_NAME FROM QSYS2.SYSCST C, QSYS2.SYSKEYCST PK, QSYS2.SYSREFCST R, QSYS2.SYSKEYCST FK WHERE C.CONSTRAINT_NAME = PK.CONSTRAINT_NAME AND C.CONSTRAINT_SCHEMA = PK.CONSTRAINT_SCHEMA AND C.CONSTRAINT_NAME = R.UNIQUE_CONSTRAINT_NAME AND C.CONSTRAINT_SCHEMA = R.UNIQUE_CONSTRAINT_SCHEMA AND R.CONSTRAINT_NAME = FK.CONSTRAINT_NAME AND R.CONSTRAINT_SCHEMA = FK.CONSTRAINT_SCHEMA AND PK.ORDINAL_POSITION = FK.ORDINAL_POSITION) AS TBL", catalog);
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x00077BB8 File Offset: 0x00075DB8
		public override string GetMVSTable(string catalog)
		{
			return string.Empty;
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x0014D126 File Offset: 0x0014B326
		public override string GetUDBTable(string catalog)
		{
			return string.Format("(SELECT pktabinfo.CLI_REQ_SCHEMA as PKTABLE_SCHEM, pktabinfo.CLI_REQ_TABLE as PKTABLE_NAME, pk.COLNAME as PKCOLUMN_NAME, fktabinfo.CLI_REQ_SCHEMA as FKTABLE_SCHEM, fktabinfo.CLI_REQ_TABLE as FKTABLE_NAME, fk.COLNAME as FKCOLUMN_NAME, pk.COLSEQ as KEY_SEQ, smallint( case UPDATERULE when 'C' then 0 when 'R' then 1 when 'N' then 2 when 'A' then 3 END) as UPDATE_RULE, smallint( case DELETERULE when 'C' then 0 when 'R' then 1 when 'N' then 2 when 'A' then 3 END) as DELETE_RULE, fk.CONSTNAME as FK_NAME, pk.CONSTNAME as PK_NAME from SYSIBM.SYSRELS as rels, SYSIBM.SYSKEYCOLUSE as pk, SYSIBM.SYSKEYCOLUSE as fk, (SELECT case systab.TYPE when 'A' then systab.BASE_SCHEMA else systab.CREATOR END as CLI_BASE_SCHEMA, case systab.TYPE  when 'A' then systab.BASE_NAME else systab.NAME end as CLI_BASE_TABLE, systab.CREATOR as CLI_REQ_SCHEMA, systab.NAME as CLI_REQ_TABLE from SYSIBM.SYSTABLES systab) as fktabinfo, (SELECT case systab.TYPE when 'A' then systab.BASE_SCHEMA else systab.CREATOR END as CLI_BASE_SCHEMA, case systab.TYPE when 'A' then systab.BASE_NAME else systab.NAME end as CLI_BASE_TABLE, systab.CREATOR as CLI_REQ_SCHEMA, systab.NAME as CLI_REQ_TABLE from SYSIBM.SYSTABLES systab) as pktabinfo WHERE rels.REFTBCREATOR = pktabinfo.CLI_BASE_SCHEMA and rels.REFTBNAME = pktabinfo.CLI_BASE_TABLE and rels.CREATOR = fktabinfo.CLI_BASE_SCHEMA and rels.TBNAME = fktabinfo.CLI_BASE_TABLE and pk.colseq = fk.colseq and pk.tbname = reftbname and pk.tbcreator = reftbcreator and fk.tbname = rels.tbname and fk.tbcreator = rels.creator and pk.constname = rels.refkeyname and fk.constname = rels.relname) AS TBL", catalog);
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x0014D134 File Offset: 0x0014B334
		public override string GetAS400Restriction(int restriction)
		{
			switch (restriction)
			{
			case 0:
				return base.GetAS400ColumnNames()[0];
			case 1:
				return base.GetAS400ColumnNames()[1];
			case 2:
				return base.GetAS400ColumnNames()[2];
			case 3:
				return base.GetAS400ColumnNames()[4];
			case 4:
				return base.GetAS400ColumnNames()[5];
			case 5:
				return base.GetAS400ColumnNames()[6];
			default:
				return string.Empty;
			}
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x0014D19C File Offset: 0x0014B39C
		public override string GetUDBRestriction(int restriction)
		{
			switch (restriction)
			{
			case 0:
				return base.GetUDBColumnNames()[0];
			case 1:
				return base.GetUDBColumnNames()[1];
			case 2:
				return base.GetUDBColumnNames()[2];
			case 3:
				return base.GetUDBColumnNames()[4];
			case 4:
				return base.GetUDBColumnNames()[5];
			case 5:
				return base.GetUDBColumnNames()[6];
			default:
				return string.Empty;
			}
		}
	}
}
