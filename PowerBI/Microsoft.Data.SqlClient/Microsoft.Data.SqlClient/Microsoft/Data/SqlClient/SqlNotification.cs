using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000076 RID: 118
	internal class SqlNotification : MarshalByRefObject
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x0001DD38 File Offset: 0x0001BF38
		internal SqlNotification(SqlNotificationInfo info, SqlNotificationSource source, SqlNotificationType type, string key)
		{
			this._info = info;
			this._source = source;
			this._type = type;
			this._key = key;
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0001DD5D File Offset: 0x0001BF5D
		internal SqlNotificationInfo Info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0001DD65 File Offset: 0x0001BF65
		internal string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0001DD6D File Offset: 0x0001BF6D
		internal SqlNotificationSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0001DD75 File Offset: 0x0001BF75
		internal SqlNotificationType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000239 RID: 569
		private readonly SqlNotificationInfo _info;

		// Token: 0x0400023A RID: 570
		private readonly SqlNotificationSource _source;

		// Token: 0x0400023B RID: 571
		private readonly SqlNotificationType _type;

		// Token: 0x0400023C RID: 572
		private readonly string _key;
	}
}
