using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000062 RID: 98
	internal class SqlClientSymmetricKey
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x00016D1F File Offset: 0x00014F1F
		internal SqlClientSymmetricKey(byte[] rootKey)
		{
			if (rootKey == null || rootKey.Length == 0)
			{
				throw SQL.NullColumnEncryptionKeySysErr();
			}
			this._rootKey = rootKey;
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00016D3B File Offset: 0x00014F3B
		internal virtual byte[] RootKey
		{
			get
			{
				return this._rootKey;
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00016D43 File Offset: 0x00014F43
		internal virtual string GetKeyHash()
		{
			return SqlSecurityUtility.GetSHA256Hash(this.RootKey);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00016D50 File Offset: 0x00014F50
		internal virtual int Length()
		{
			return this._rootKey.Length;
		}

		// Token: 0x0400016F RID: 367
		protected readonly byte[] _rootKey;
	}
}
