using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005D1 RID: 1489
	public class OdbcConnectionInfo
	{
		// Token: 0x06002E7F RID: 11903 RVA: 0x0008DA4E File Offset: 0x0008BC4E
		public OdbcConnectionInfo(string connectionString, Dictionary<int, object> connectionAttributes)
		{
			this.connectionString = connectionString;
			this.connectionAttributes = connectionAttributes;
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x0008DA64 File Offset: 0x0008BC64
		public OdbcConnectionInfo(ConnectionInfo info)
		{
			this.connectionString = info.ConnectionString;
			this.impersonate = ((info.AlternateIdentity != null) ? info.Impersonate : null);
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x0008DA92 File Offset: 0x0008BC92
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x0008DA9A File Offset: 0x0008BC9A
		public bool RequiresImpersonation
		{
			get
			{
				return this.impersonate != null;
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x0008DAA5 File Offset: 0x0008BCA5
		public Func<IDisposable> Impersonate
		{
			get
			{
				return this.impersonate;
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x0008DAAD File Offset: 0x0008BCAD
		public Dictionary<int, object> ConnectionAttributes
		{
			get
			{
				return this.connectionAttributes;
			}
		}

		// Token: 0x0400147D RID: 5245
		private readonly string connectionString;

		// Token: 0x0400147E RID: 5246
		private readonly Func<IDisposable> impersonate;

		// Token: 0x0400147F RID: 5247
		private readonly Dictionary<int, object> connectionAttributes;
	}
}
