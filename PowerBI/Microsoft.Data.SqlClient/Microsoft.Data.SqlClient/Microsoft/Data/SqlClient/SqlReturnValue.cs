using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000113 RID: 275
	internal sealed class SqlReturnValue : SqlMetaDataPriv
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x0005EE15 File Offset: 0x0005D015
		internal SqlReturnValue()
		{
			this.value = new SqlBuffer();
		}

		// Token: 0x040008C3 RID: 2243
		internal ushort parmIndex;

		// Token: 0x040008C4 RID: 2244
		internal string parameter;

		// Token: 0x040008C5 RID: 2245
		internal readonly SqlBuffer value;
	}
}
