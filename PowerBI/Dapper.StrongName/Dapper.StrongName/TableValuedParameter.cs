using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Dapper
{
	// Token: 0x02000010 RID: 16
	internal sealed class TableValuedParameter : SqlMapper.ICustomQueryParameter
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00009681 File Offset: 0x00007881
		public TableValuedParameter(DataTable table)
			: this(table, null)
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000968B File Offset: 0x0000788B
		public TableValuedParameter(DataTable table, string typeName)
		{
			this.table = table;
			this.typeName = typeName;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000096A4 File Offset: 0x000078A4
		static TableValuedParameter()
		{
			PropertyInfo prop = typeof(SqlParameter).GetProperty("TypeName", BindingFlags.Instance | BindingFlags.Public);
			if (prop != null && prop.PropertyType == typeof(string) && prop.CanWrite)
			{
				TableValuedParameter.setTypeName = (Action<SqlParameter, string>)Delegate.CreateDelegate(typeof(Action<SqlParameter, string>), prop.GetSetMethod());
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00009710 File Offset: 0x00007910
		void SqlMapper.ICustomQueryParameter.AddParameter(IDbCommand command, string name)
		{
			IDbDataParameter param = command.CreateParameter();
			param.ParameterName = name;
			TableValuedParameter.Set(param, this.table, this.typeName);
			command.Parameters.Add(param);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000974C File Offset: 0x0000794C
		internal static void Set(IDbDataParameter parameter, DataTable table, string typeName)
		{
			parameter.Value = SqlMapper.SanitizeParameterValue(table);
			if (string.IsNullOrEmpty(typeName) && table != null)
			{
				typeName = table.GetTypeName();
			}
			SqlParameter sqlParam;
			if (!string.IsNullOrEmpty(typeName) && (sqlParam = parameter as SqlParameter) != null)
			{
				Action<SqlParameter, string> action = TableValuedParameter.setTypeName;
				if (action != null)
				{
					action(sqlParam, typeName);
				}
				sqlParam.SqlDbType = SqlDbType.Structured;
			}
		}

		// Token: 0x0400004B RID: 75
		private readonly DataTable table;

		// Token: 0x0400004C RID: 76
		private readonly string typeName;

		// Token: 0x0400004D RID: 77
		private static readonly Action<SqlParameter, string> setTypeName;
	}
}
