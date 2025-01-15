using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000089 RID: 137
	public class SqlNotificationEventArgs : EventArgs
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x00021B55 File Offset: 0x0001FD55
		public SqlNotificationEventArgs(SqlNotificationType type, SqlNotificationInfo info, SqlNotificationSource source)
		{
			this._info = info;
			this._source = source;
			this._type = type;
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00021B72 File Offset: 0x0001FD72
		public SqlNotificationType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00021B7A File Offset: 0x0001FD7A
		public SqlNotificationInfo Info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00021B82 File Offset: 0x0001FD82
		public SqlNotificationSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x040002D5 RID: 725
		private readonly SqlNotificationType _type;

		// Token: 0x040002D6 RID: 726
		private readonly SqlNotificationInfo _info;

		// Token: 0x040002D7 RID: 727
		private readonly SqlNotificationSource _source;

		// Token: 0x040002D8 RID: 728
		internal static SqlNotificationEventArgs s_notifyError = new SqlNotificationEventArgs(SqlNotificationType.Subscribe, SqlNotificationInfo.Error, SqlNotificationSource.Object);
	}
}
