using System;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200002F RID: 47
	internal interface ISqlFragment
	{
		// Token: 0x06000455 RID: 1109
		void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator);
	}
}
