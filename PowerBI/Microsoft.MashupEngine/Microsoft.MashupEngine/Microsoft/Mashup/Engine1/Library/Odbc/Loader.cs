using System;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005A1 RID: 1441
	internal abstract class Loader
	{
		// Token: 0x06002D94 RID: 11668 RVA: 0x0008A970 File Offset: 0x00088B70
		protected Loader(OdbcPageReaderColumnInfo columnInfo, Column column)
		{
			this.columnInfo = columnInfo;
			this.column = column;
		}

		// Token: 0x06002D95 RID: 11669
		public unsafe abstract bool TryLoad(DBTYPE dbType, byte* buffer, int length, out string error);

		// Token: 0x040013D9 RID: 5081
		protected readonly OdbcPageReaderColumnInfo columnInfo;

		// Token: 0x040013DA RID: 5082
		protected readonly Column column;
	}
}
