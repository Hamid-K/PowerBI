using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000112 RID: 274
	internal sealed class _SqlRPC
	{
		// Token: 0x060015BB RID: 5563 RVA: 0x0005ED8D File Offset: 0x0005CF8D
		internal string GetCommandTextOrRpcName()
		{
			if (10 == this.ProcID)
			{
				return (string)this.systemParams[0].Value;
			}
			return this.rpcName;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0005EDB4 File Offset: 0x0005CFB4
		internal SqlParameter GetParameterByIndex(int index, out byte options)
		{
			SqlParameter sqlParameter;
			if (index < this.systemParamCount)
			{
				sqlParameter = this.systemParams[index];
				options = this.systemParamOptions[index];
			}
			else
			{
				long num = this.userParamMap[index - this.systemParamCount];
				int num2 = (int)(num & 2147483647L);
				options = (byte)((num >> 32) & 255L);
				sqlParameter = this.userParams[num2];
			}
			return sqlParameter;
		}

		// Token: 0x040008B1 RID: 2225
		internal string rpcName;

		// Token: 0x040008B2 RID: 2226
		internal ushort ProcID;

		// Token: 0x040008B3 RID: 2227
		internal ushort options;

		// Token: 0x040008B4 RID: 2228
		internal SqlParameter[] systemParams;

		// Token: 0x040008B5 RID: 2229
		internal byte[] systemParamOptions;

		// Token: 0x040008B6 RID: 2230
		internal int systemParamCount;

		// Token: 0x040008B7 RID: 2231
		internal SqlParameterCollection userParams;

		// Token: 0x040008B8 RID: 2232
		internal long[] userParamMap;

		// Token: 0x040008B9 RID: 2233
		internal int userParamCount;

		// Token: 0x040008BA RID: 2234
		internal int? recordsAffected;

		// Token: 0x040008BB RID: 2235
		internal int cumulativeRecordsAffected;

		// Token: 0x040008BC RID: 2236
		internal int errorsIndexStart;

		// Token: 0x040008BD RID: 2237
		internal int errorsIndexEnd;

		// Token: 0x040008BE RID: 2238
		internal SqlErrorCollection errors;

		// Token: 0x040008BF RID: 2239
		internal int warningsIndexStart;

		// Token: 0x040008C0 RID: 2240
		internal int warningsIndexEnd;

		// Token: 0x040008C1 RID: 2241
		internal SqlErrorCollection warnings;

		// Token: 0x040008C2 RID: 2242
		internal bool needsFetchParameterEncryptionMetadata;
	}
}
