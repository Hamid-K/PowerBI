using System;
using System.Data;

namespace Dapper
{
	// Token: 0x02000014 RID: 20
	internal abstract class XmlTypeHandler<T> : SqlMapper.StringTypeHandler<T>
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00009A6F File Offset: 0x00007C6F
		public override void SetValue(IDbDataParameter parameter, T value)
		{
			base.SetValue(parameter, value);
			parameter.DbType = DbType.Xml;
		}
	}
}
