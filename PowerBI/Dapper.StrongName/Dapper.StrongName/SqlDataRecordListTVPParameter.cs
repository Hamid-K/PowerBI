using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.SqlServer.Server;

namespace Dapper
{
	// Token: 0x0200000E RID: 14
	internal sealed class SqlDataRecordListTVPParameter : SqlMapper.ICustomQueryParameter
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000039FC File Offset: 0x00001BFC
		public SqlDataRecordListTVPParameter(IEnumerable<SqlDataRecord> data, string typeName)
		{
			this.data = data;
			this.typeName = typeName;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003A14 File Offset: 0x00001C14
		void SqlMapper.ICustomQueryParameter.AddParameter(IDbCommand command, string name)
		{
			IDbDataParameter param = command.CreateParameter();
			param.ParameterName = name;
			SqlDataRecordListTVPParameter.Set(param, this.data, this.typeName);
			command.Parameters.Add(param);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003A50 File Offset: 0x00001C50
		internal static void Set(IDbDataParameter parameter, IEnumerable<SqlDataRecord> data, string typeName)
		{
			parameter.Value = ((data != null && data.Any<SqlDataRecord>()) ? data : null);
			SqlParameter sqlParam;
			if ((sqlParam = parameter as SqlParameter) != null)
			{
				sqlParam.SqlDbType = SqlDbType.Structured;
				sqlParam.TypeName = typeName;
			}
		}

		// Token: 0x04000030 RID: 48
		private readonly IEnumerable<SqlDataRecord> data;

		// Token: 0x04000031 RID: 49
		private readonly string typeName;
	}
}
