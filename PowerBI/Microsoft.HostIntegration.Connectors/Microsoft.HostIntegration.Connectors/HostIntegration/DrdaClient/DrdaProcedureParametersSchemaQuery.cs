using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A2C RID: 2604
	internal class DrdaProcedureParametersSchemaQuery : DrdaSchemaQuery
	{
		// Token: 0x06005192 RID: 20882 RVA: 0x0014CAF4 File Offset: 0x0014ACF4
		public DrdaProcedureParametersSchemaQuery()
		{
			string[] array = new string[]
			{
				"PROCEDURE_CAT", "PROCEDURE_SCHEM", "PROCEDURE_NAME", "COLUMN_NAME", "ORDINAL_POSITION", "COLUMN_TYPE", "TYPE_NAME", "COLUMN_SIZE", "PRECISION", "DECIMAL_DIGITS",
				"NULLABLE", "CCSID", "REMARKS"
			};
			Type[] array2 = new Type[]
			{
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(string),
				typeof(int),
				typeof(string),
				typeof(string),
				typeof(int),
				typeof(int),
				typeof(int),
				typeof(string),
				typeof(int),
				typeof(string)
			};
			int[] array3 = new int[] { 0, 1, 2, 4 };
			string[] array4 = new string[]
			{
				null, "SPECIFIC_SCHEMA", "SPECIFIC_NAME", "PARAMETER_NAME", "ORDINAL_POSITION", "PARAMETER_MODE", "DATA_TYPE", "CHARACTER_MAXIMUM_LENGTH", "NUMERIC_PRECISION", "NUMERIC_SCALE",
				"IS_NULLABLE", "CCSID", "LONG_COMMENT"
			};
			string text = "SYSPARMS";
			string[] array5 = new string[13];
			array5[1] = "OWNER";
			array5[2] = "NAME";
			array5[3] = "PARMNAME";
			array5[4] = "ORDINAL";
			array5[5] = "ROWTYPE";
			array5[6] = "TYPENAME";
			array5[7] = "LENGTH";
			array5[8] = "LENGTH";
			array5[9] = "SCALE";
			array5[11] = "CCSID";
			string text2 = "SYSPARMS";
			string[] array6 = new string[13];
			array6[1] = "PROCSCHEMA";
			array6[2] = "PROCNAME";
			array6[3] = "PARMNAME";
			array6[4] = "ORDINAL";
			array6[5] = "PARM_MODE";
			array6[6] = "TYPENAME";
			array6[7] = "LENGTH";
			array6[8] = "LENGTH";
			array6[9] = "SCALE";
			array6[10] = "NULLS";
			array6[11] = "CODEPAGE";
			base..ctor(array, array2, array3, array4, text, array5, text2, array6, "PROCPARMS");
		}
	}
}
