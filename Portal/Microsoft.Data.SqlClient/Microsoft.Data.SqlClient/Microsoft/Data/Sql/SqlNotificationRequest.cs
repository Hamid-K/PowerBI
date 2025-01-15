using System;
using Microsoft.Data.Common;

namespace Microsoft.Data.Sql
{
	// Token: 0x0200015D RID: 349
	public sealed class SqlNotificationRequest
	{
		// Token: 0x06001A5C RID: 6748 RVA: 0x0006C013 File Offset: 0x0006A213
		public SqlNotificationRequest()
			: this(null, null, 0)
		{
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0006C01E File Offset: 0x0006A21E
		public SqlNotificationRequest(string userData, string options, int timeout)
		{
			this.UserData = userData;
			this.Timeout = timeout;
			this.Options = options;
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0006C03B File Offset: 0x0006A23B
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x0006C043 File Offset: 0x0006A243
		public string Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value != null && 65535 < value.Length)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "Options");
				}
				this._options = value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0006C06C File Offset: 0x0006A26C
		// (set) Token: 0x06001A61 RID: 6753 RVA: 0x0006C074 File Offset: 0x0006A274
		public int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (0 > value)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "Timeout");
				}
				this._timeout = value;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0006C091 File Offset: 0x0006A291
		// (set) Token: 0x06001A63 RID: 6755 RVA: 0x0006C099 File Offset: 0x0006A299
		public string UserData
		{
			get
			{
				return this._userData;
			}
			set
			{
				if (value != null && 65535 < value.Length)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "UserData");
				}
				this._userData = value;
			}
		}

		// Token: 0x04000AB1 RID: 2737
		private string _userData;

		// Token: 0x04000AB2 RID: 2738
		private string _options;

		// Token: 0x04000AB3 RID: 2739
		private int _timeout;
	}
}
