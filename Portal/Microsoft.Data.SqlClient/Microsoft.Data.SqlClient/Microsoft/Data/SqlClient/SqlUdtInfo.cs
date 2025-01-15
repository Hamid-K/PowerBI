using System;
using System.Collections.Generic;
using Microsoft.Data.Common;
using Microsoft.SqlServer.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000A3 RID: 163
	internal class SqlUdtInfo
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x00026FC4 File Offset: 0x000251C4
		private SqlUdtInfo(SqlUserDefinedTypeAttribute attr)
		{
			this.SerializationFormat = attr.Format;
			this.IsByteOrdered = attr.IsByteOrdered;
			this.IsFixedLength = attr.IsFixedLength;
			this.MaxByteSize = attr.MaxByteSize;
			this.Name = attr.Name;
			this.ValidationMethodName = attr.ValidationMethodName;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00027020 File Offset: 0x00025220
		internal static SqlUdtInfo GetFromType(Type target)
		{
			SqlUdtInfo sqlUdtInfo = SqlUdtInfo.TryGetFromType(target);
			if (sqlUdtInfo == null)
			{
				throw ADP.CreateInvalidUdtException(target, "SqlUdtReason_NoUdtAttribute");
			}
			return sqlUdtInfo;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00027044 File Offset: 0x00025244
		internal static SqlUdtInfo TryGetFromType(Type target)
		{
			if (SqlUdtInfo.s_types2UdtInfo == null)
			{
				SqlUdtInfo.s_types2UdtInfo = new Dictionary<Type, SqlUdtInfo>();
			}
			SqlUdtInfo sqlUdtInfo;
			if (!SqlUdtInfo.s_types2UdtInfo.TryGetValue(target, out sqlUdtInfo))
			{
				object[] customAttributes = target.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);
				if (customAttributes != null && customAttributes.Length == 1)
				{
					sqlUdtInfo = new SqlUdtInfo((SqlUserDefinedTypeAttribute)customAttributes[0]);
				}
				SqlUdtInfo.s_types2UdtInfo.Add(target, sqlUdtInfo);
			}
			return sqlUdtInfo;
		}

		// Token: 0x04000364 RID: 868
		internal readonly Format SerializationFormat;

		// Token: 0x04000365 RID: 869
		internal readonly bool IsByteOrdered;

		// Token: 0x04000366 RID: 870
		internal readonly bool IsFixedLength;

		// Token: 0x04000367 RID: 871
		internal readonly int MaxByteSize;

		// Token: 0x04000368 RID: 872
		internal readonly string Name;

		// Token: 0x04000369 RID: 873
		internal readonly string ValidationMethodName;

		// Token: 0x0400036A RID: 874
		[ThreadStatic]
		private static Dictionary<Type, SqlUdtInfo> s_types2UdtInfo;
	}
}
