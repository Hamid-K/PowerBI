using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.SqlServer.Server;

namespace Dapper
{
	// Token: 0x0200000D RID: 13
	internal sealed class SqlDataRecordHandler : SqlMapper.ITypeHandler
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000039DE File Offset: 0x00001BDE
		public object Parse(Type destinationType, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000039E5 File Offset: 0x00001BE5
		public void SetValue(IDbDataParameter parameter, object value)
		{
			SqlDataRecordListTVPParameter.Set(parameter, value as IEnumerable<SqlDataRecord>, null);
		}
	}
}
