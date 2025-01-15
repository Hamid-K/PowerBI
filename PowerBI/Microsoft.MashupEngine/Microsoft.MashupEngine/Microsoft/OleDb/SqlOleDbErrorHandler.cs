using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F2C RID: 7980
	public class SqlOleDbErrorHandler : IOleDbCustomErrorHandler
	{
		// Token: 0x0600C395 RID: 50069 RVA: 0x000020FD File Offset: 0x000002FD
		private SqlOleDbErrorHandler()
		{
		}

		// Token: 0x17002FC5 RID: 12229
		// (get) Token: 0x0600C396 RID: 50070 RVA: 0x00272FA3 File Offset: 0x002711A3
		public Guid InterfaceID
		{
			get
			{
				return IID.ISQLErrorInfo;
			}
		}

		// Token: 0x0600C397 RID: 50071 RVA: 0x00272FAC File Offset: 0x002711AC
		public OleDbError GetError(string source, string message, object customObject)
		{
			string empty;
			int num;
			if ((customObject as ISQLErrorInfo).GetSQLInfo(out empty, out num) < 0)
			{
				empty = string.Empty;
				num = -1;
			}
			return new OleDbError(source, message, empty, num);
		}

		// Token: 0x04006497 RID: 25751
		public static readonly IOleDbCustomErrorHandler Instance = new SqlOleDbErrorHandler();
	}
}
