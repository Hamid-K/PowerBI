using System;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200006A RID: 106
	internal class SqlConnectionPoolKey : DbConnectionPoolKey
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00017FED File Offset: 0x000161ED
		internal SqlCredential Credential
		{
			get
			{
				return this._credential;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00017FF5 File Offset: 0x000161F5
		internal string AccessToken
		{
			get
			{
				return this._accessToken;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00017FFD File Offset: 0x000161FD
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00018005 File Offset: 0x00016205
		internal override string ConnectionString
		{
			get
			{
				return base.ConnectionString;
			}
			set
			{
				base.ConnectionString = value;
				this.CalculateHashCode();
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00018014 File Offset: 0x00016214
		internal ServerCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				return this._serverCertificateValidationCallback;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0001801C File Offset: 0x0001621C
		internal ClientCertificateRetrievalCallback ClientCertificateRetrievalCallback
		{
			get
			{
				return this._clientCertificateRetrievalCallback;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00018024 File Offset: 0x00016224
		internal SqlClientOriginalNetworkAddressInfo OriginalNetworkAddressInfo
		{
			get
			{
				return this._originalNetworkAddressInfo;
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001802C File Offset: 0x0001622C
		internal SqlConnectionPoolKey(string connectionString, SqlCredential credential, string accessToken, ServerCertificateValidationCallback serverCertificateValidationCallback, ClientCertificateRetrievalCallback clientCertificateRetrievalCallback, SqlClientOriginalNetworkAddressInfo originalNetworkAddressInfo)
			: base(connectionString)
		{
			this._credential = credential;
			this._accessToken = accessToken;
			this._serverCertificateValidationCallback = serverCertificateValidationCallback;
			this._clientCertificateRetrievalCallback = clientCertificateRetrievalCallback;
			this._originalNetworkAddressInfo = originalNetworkAddressInfo;
			this.CalculateHashCode();
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00018061 File Offset: 0x00016261
		private SqlConnectionPoolKey(SqlConnectionPoolKey key)
			: base(key)
		{
			this._credential = key.Credential;
			this._accessToken = key.AccessToken;
			this._serverCertificateValidationCallback = key._serverCertificateValidationCallback;
			this._clientCertificateRetrievalCallback = key._clientCertificateRetrievalCallback;
			this.CalculateHashCode();
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000180A0 File Offset: 0x000162A0
		public override object Clone()
		{
			return new SqlConnectionPoolKey(this);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000180A8 File Offset: 0x000162A8
		public override bool Equals(object obj)
		{
			SqlConnectionPoolKey sqlConnectionPoolKey = obj as SqlConnectionPoolKey;
			return sqlConnectionPoolKey != null && this._credential == sqlConnectionPoolKey._credential && this.ConnectionString == sqlConnectionPoolKey.ConnectionString && string.CompareOrdinal(this._accessToken, sqlConnectionPoolKey._accessToken) == 0 && this._serverCertificateValidationCallback == sqlConnectionPoolKey._serverCertificateValidationCallback && this._clientCertificateRetrievalCallback == sqlConnectionPoolKey._clientCertificateRetrievalCallback && this._originalNetworkAddressInfo == sqlConnectionPoolKey._originalNetworkAddressInfo;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00018129 File Offset: 0x00016329
		public override int GetHashCode()
		{
			return this._hashValue;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00018134 File Offset: 0x00016334
		private void CalculateHashCode()
		{
			this._hashValue = base.GetHashCode();
			if (this._credential != null)
			{
				this._hashValue = this._hashValue * 17 + this._credential.GetHashCode();
			}
			else if (this._accessToken != null)
			{
				this._hashValue = this._hashValue * 17 + this._accessToken.GetHashCode();
			}
			if (this._originalNetworkAddressInfo != null)
			{
				this._hashValue = this._hashValue * 17 + this._originalNetworkAddressInfo.GetHashCode();
			}
		}

		// Token: 0x04000194 RID: 404
		private int _hashValue;

		// Token: 0x04000195 RID: 405
		private readonly SqlCredential _credential;

		// Token: 0x04000196 RID: 406
		private readonly string _accessToken;

		// Token: 0x04000197 RID: 407
		private readonly ServerCertificateValidationCallback _serverCertificateValidationCallback;

		// Token: 0x04000198 RID: 408
		private readonly ClientCertificateRetrievalCallback _clientCertificateRetrievalCallback;

		// Token: 0x04000199 RID: 409
		private readonly SqlClientOriginalNetworkAddressInfo _originalNetworkAddressInfo;
	}
}
