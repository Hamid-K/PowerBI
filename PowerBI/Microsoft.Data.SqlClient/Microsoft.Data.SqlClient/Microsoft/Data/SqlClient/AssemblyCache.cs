using System;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000CE RID: 206
	internal sealed class AssemblyCache
	{
		// Token: 0x06000EDC RID: 3804 RVA: 0x000027D1 File Offset: 0x000009D1
		private AssemblyCache()
		{
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0002F0A8 File Offset: 0x0002D2A8
		internal static int GetLength(object inst)
		{
			return SerializationHelperSql9.SizeInBytes(inst);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0002F0B0 File Offset: 0x0002D2B0
		internal static SqlUdtInfo GetInfoFromType(Type t)
		{
			Type type = t;
			SqlUdtInfo sqlUdtInfo;
			for (;;)
			{
				sqlUdtInfo = SqlUdtInfo.TryGetFromType(t);
				if (sqlUdtInfo != null)
				{
					break;
				}
				t = t.BaseType;
				if (!(t != null))
				{
					goto Block_2;
				}
			}
			return sqlUdtInfo;
			Block_2:
			throw SQL.UDTInvalidSqlType(type.AssemblyQualifiedName);
		}
	}
}
