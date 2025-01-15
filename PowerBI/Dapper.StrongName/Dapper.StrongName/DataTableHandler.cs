using System;
using System.Data;

namespace Dapper
{
	// Token: 0x02000006 RID: 6
	internal sealed class DataTableHandler : SqlMapper.ITypeHandler
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000240A File Offset: 0x0000060A
		public object Parse(Type destinationType, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002411 File Offset: 0x00000611
		public void SetValue(IDbDataParameter parameter, object value)
		{
			TableValuedParameter.Set(parameter, value as DataTable, null);
		}
	}
}
