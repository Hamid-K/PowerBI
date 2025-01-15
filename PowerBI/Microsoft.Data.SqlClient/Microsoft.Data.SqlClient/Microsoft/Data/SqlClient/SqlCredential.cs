using System;
using System.Security;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000072 RID: 114
	public sealed class SqlCredential
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x0001B764 File Offset: 0x00019964
		public SqlCredential(string userId, SecureString password)
		{
			if (userId == null)
			{
				throw ADP.ArgumentNull("userId");
			}
			if (userId.Length > 128)
			{
				throw ADP.InvalidArgumentLength("userId", 128);
			}
			if (password == null)
			{
				throw ADP.ArgumentNull("password");
			}
			if (password.Length > 128)
			{
				throw ADP.InvalidArgumentLength("password", 128);
			}
			if (!password.IsReadOnly())
			{
				throw ADP.MustBeReadOnly("password");
			}
			this._userId = userId;
			this._password = password;
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0001B7EE File Offset: 0x000199EE
		public string UserId
		{
			get
			{
				return this._userId;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0001B7F6 File Offset: 0x000199F6
		public SecureString Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x04000213 RID: 531
		private string _userId;

		// Token: 0x04000214 RID: 532
		private SecureString _password;
	}
}
